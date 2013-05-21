// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyFactory.cs" company="George Ma">
//   Copyright © George Ma. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the ProxyFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace SimpleProxy
{
  /// <summary>
  /// The proxy factory.
  /// </summary>
  /// <typeparam name="TClassMetaData">
  /// The type for class meta data.
  /// </typeparam>
  /// <typeparam name="TPropertyMetaData">
  /// Type for meta data.
  /// </typeparam>
  public class ProxyFactory<TClassMetaData, TPropertyMetaData>
  {
    /// <summary>
    /// The class analyzer.
    /// </summary>
    private readonly IClassAnalyzer<TClassMetaData, TPropertyMetaData> _classAnalyzer;

    /// <summary>
    /// The proxy cache.
    /// </summary>
    private readonly Dictionary<Type, IProxy<TClassMetaData, TPropertyMetaData>> _proxyCache = new Dictionary<Type, IProxy<TClassMetaData, TPropertyMetaData>>();

    /// <summary>
    /// The type lock.
    /// </summary>
    private readonly object _typeLock = new object();

    /// <summary>
    /// The module builder lock.
    /// </summary>
    private readonly object _moduleBuilderLock = new object();

    /// <summary>
    /// The base type.
    /// </summary>
    private readonly Type _baseType;

    /// <summary>
    /// The base ctor.
    /// </summary>
    private readonly ConstructorInfo _baseCtor;

    /// <summary>
    /// The meta data class name.
    /// </summary>
    private readonly string _metaDataClassName;

    /// <summary>
    /// The module builder.
    /// </summary>
    private volatile ModuleBuilder _moduleBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProxyFactory{TClassMetaData, TPropertyMetaData}"/> class.
    /// </summary>
    /// <param name="classAnalyzer">
    /// The class analyzer.
    /// </param>
    public ProxyFactory(IClassAnalyzer<TClassMetaData, TPropertyMetaData> classAnalyzer)
    {
      this._classAnalyzer = classAnalyzer;
      this._baseType = typeof(ProxyBase<TClassMetaData, TPropertyMetaData>);
      this._baseCtor = this._baseType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)[0];
      this._metaDataClassName = typeof(TPropertyMetaData).Name;
    }

    /// <summary>
    /// Gets the module builder.
    /// </summary>
    private ModuleBuilder ModuleBuilder
    {
      get
      {
        if (this._moduleBuilder == null)
        {
          lock (this._moduleBuilderLock)
          {
            if (this._moduleBuilder == null)
            {
              var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName("SimpleProxy.Dynamic"), AssemblyBuilderAccess.Run);

              string assemblyName = assemblyBuilder.GetName().Name;

              return this._moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName);
            }
          }
        }

        return this._moduleBuilder;
      }
    }

    /// <summary>
    /// Create or get the proxy.
    /// </summary>
    /// <typeparam name="T">
    /// The type used to generate the proxy.
    /// </typeparam>
    /// <returns>
    /// The <see cref="IProxy{TClassMetaData, TPropertyMetaData}"/>.
    /// </returns>
    public IProxy<TClassMetaData, TPropertyMetaData> GetProxy<T>()
      where T : class
    {
      var type = typeof(T);

      IProxy<TClassMetaData, TPropertyMetaData> value = null;

      if (!this._proxyCache.ContainsKey(type))
      {
        lock (this._typeLock)
        {
          if (!this._proxyCache.ContainsKey(type))
          {
            var typeBuilder =
              this.ModuleBuilder.DefineType(
                string.Format("{0}_{1}_SimpleProxy_proxy", this._metaDataClassName, type.Name),
                TypeAttributes.Class | TypeAttributes.Public,
                this._baseType);

            this.GenerateConstructor(typeBuilder);

            TClassMetaData classMetaData;
            MethodInfoMetaData<TPropertyMetaData>[] metaDataGets, metaDataSets;

            this._classAnalyzer.ProcessType(type, out classMetaData, out metaDataGets, out metaDataSets);

            this.GenerateGetValuesMethod(typeBuilder, metaDataGets);

            this.GenerateSetValuesMethod(typeBuilder, metaDataSets);

            this.GenerateCreateObjectMethod(typeBuilder, type);

            var proxyType = typeBuilder.CreateType();

            value =
              this._proxyCache[type] =
              (IProxy<TClassMetaData, TPropertyMetaData>)
              Activator.CreateInstance(
                proxyType,
                classMetaData,
                metaDataGets.Select(x => x.MetaData).ToArray(),
                metaDataSets.Select(x => x.MetaData).ToArray());
          }
          else
          {
            value = this._proxyCache[type];
          }
        }
      }
      else
      {
        value = this._proxyCache[type];
      }

      return value;
    }

    /// <summary>
    /// The generate constructor.
    /// </summary>
    /// <param name="typeBuilder">
    /// The type builder.
    /// </param>
    private void GenerateConstructor(TypeBuilder typeBuilder)
    {
      ConstructorBuilder ctorBuilder = typeBuilder.DefineConstructor(
        MethodAttributes.Public,
        CallingConventions.HasThis,
        new[] { typeof(TClassMetaData), typeof(TPropertyMetaData[]), typeof(TPropertyMetaData[]) });

      ILGenerator cIl = ctorBuilder.GetILGenerator();

      cIl.Emit(OpCodes.Ldarg_0);
      cIl.Emit(OpCodes.Ldarg_1);
      cIl.Emit(OpCodes.Ldarg_2);
      cIl.Emit(OpCodes.Ldarg_3);
      cIl.Emit(OpCodes.Call, this._baseCtor);
      cIl.Emit(OpCodes.Ret);      
    }

    /// <summary>
    /// The generate get values method.
    /// </summary>
    /// <param name="typeBuilder">
    /// The type builder.
    /// </param>
    /// <param name="metaDataGets">
    /// The meta data gets.
    /// </param>
    private void GenerateGetValuesMethod(TypeBuilder typeBuilder, MethodInfoMetaData<TPropertyMetaData>[] metaDataGets)
    {
      MethodBuilder getValuesBuilder = typeBuilder.DefineMethod(
                    "GetValues",
                    MethodAttributes.Public | MethodAttributes.Virtual,
                    typeof(object[]),
                    new[] { typeof(object) });

      ILGenerator gIl = getValuesBuilder.GetILGenerator();

      LocalBuilder array = gIl.DeclareLocal(typeof(object[]));
      gIl.Emit(OpCodes.Ldc_I4, metaDataGets.Length);
      gIl.Emit(OpCodes.Newarr, typeof(object));
      gIl.Emit(OpCodes.Stloc, array);

      var i = 0;

      foreach (var metaDataGet in metaDataGets)
      {
        var method = metaDataGet.MethodInfo;
        gIl.Emit(OpCodes.Ldloc, array);
        gIl.Emit(OpCodes.Ldc_I4, i++);
        gIl.Emit(OpCodes.Ldarg_1);
        gIl.Emit(OpCodes.Call, method);

        if (method.ReturnType.IsPrimitive || method.ReturnType.IsValueType)
        {
          gIl.Emit(OpCodes.Box, method.ReturnType);
        }

        gIl.Emit(OpCodes.Stelem_Ref);
      }

      gIl.Emit(OpCodes.Ldloc, array);

      gIl.Emit(OpCodes.Ret);
    }

    /// <summary>
    /// The generate set values method.
    /// </summary>
    /// <param name="typeBuilder">
    /// The type builder.
    /// </param>
    /// <param name="metaDataSets">
    /// The meta data sets.
    /// </param>
    private void GenerateSetValuesMethod(TypeBuilder typeBuilder, MethodInfoMetaData<TPropertyMetaData>[] metaDataSets)
    {
      MethodBuilder setValuesBuilder = typeBuilder.DefineMethod(
        "SetValues",
        MethodAttributes.Public | MethodAttributes.Virtual,
        typeof(void),
        new[] { typeof(object), typeof(object[]) });

      ILGenerator sIl = setValuesBuilder.GetILGenerator();

      var i = 0;

      foreach (var metaDataSet in metaDataSets)
      {
        var method = metaDataSet.MethodInfo;
        var parameterType = method.GetParameters()[0].ParameterType;

        sIl.Emit(OpCodes.Ldarg_1);

        sIl.Emit(OpCodes.Ldarg_2);
        sIl.Emit(OpCodes.Ldc_I4, i++);

        sIl.Emit(OpCodes.Ldelem_Ref);

        if (parameterType.IsPrimitive || parameterType.IsValueType)
        {
          sIl.Emit(OpCodes.Unbox_Any, parameterType);
        }

        sIl.Emit(OpCodes.Call, method);
      }

      sIl.Emit(OpCodes.Ret);
    }

    /// <summary>
    /// The generate create object method.
    /// </summary>
    /// <param name="typeBuilder">
    /// The type builder.
    /// </param>
    /// <param name="type">
    /// The object type.
    /// </param>
    private void GenerateCreateObjectMethod(TypeBuilder typeBuilder, Type type)
    {
      var typeCtor = type.GetConstructor(new Type[0]);

      MethodBuilder createObjectBuilder = typeBuilder.DefineMethod(
        "CreateObject",
        MethodAttributes.Public | MethodAttributes.Virtual,
        typeof(object),
        null);

      ILGenerator coIl = createObjectBuilder.GetILGenerator();
      coIl.Emit(OpCodes.Newobj, typeCtor);
      coIl.Emit(OpCodes.Ret);
    }
  }
}