// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProxyBase.cs" company="George Ma">
//   Copyright © George Ma. All Rights Reserved.
// </copyright>
// <summary>
//   The proxy base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SimpleProxy
{
  /// <summary>
  /// The proxy base.
  /// </summary>
  /// <typeparam name="TClassMetaData">
  /// The type for class meta data.
  /// </typeparam>
  /// <typeparam name="TPropertyMetaData">
  /// The type for property meta data.
  /// </typeparam>
  public abstract class ProxyBase<TClassMetaData, TPropertyMetaData> : IProxy<TClassMetaData, TPropertyMetaData>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ProxyBase{TClassMetaData, TPropertyMetaData}"/> class.
    /// </summary>
    /// <param name="classMetaData">
    /// The class meta data.
    /// </param>
    /// <param name="metaDataGets">
    /// The meta data gets.
    /// </param>
    /// <param name="metaDataSets">
    /// The meta data sets.
    /// </param>
    protected ProxyBase(TClassMetaData classMetaData, TPropertyMetaData[] metaDataGets, TPropertyMetaData[] metaDataSets)
    {
      this.ClassMetaData = classMetaData;
      this.MetaDataGets = metaDataGets;
      this.MetaDataSets = metaDataSets;
    }

    /// <summary>
    /// Gets the meta data gets.
    /// </summary>
    public TPropertyMetaData[] MetaDataGets { get; private set; }

    /// <summary>
    /// Gets the meta data sets.
    /// </summary>
    public TPropertyMetaData[] MetaDataSets { get; private set; }

    /// <summary>
    /// Gets the class meta data.
    /// </summary>
    public TClassMetaData ClassMetaData { get; private set; }

    /// <summary>
    /// The set values.
    /// </summary>
    /// <param name="obj">
    /// The object.
    /// </param>
    /// <param name="values">
    /// The values.
    /// </param>
    public abstract void SetValues(object obj, object[] values);

    /// <summary>
    /// The get values.
    /// </summary>
    /// <param name="obj">
    /// The object.
    /// </param>
    /// <returns>
    /// The <see cref="object[]"/>.
    /// </returns>
    public abstract object[] GetValues(object obj);

    /// <summary>
    /// The create object.
    /// </summary>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    public abstract object CreateObject();
  }
}
