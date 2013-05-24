// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProxyFactory.cs" company="George Ma">
//   Copyright © George Ma. All Rights Reserved.
// </copyright>
// <summary>
//   Defines the IProxyFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace SimpleProxy
{
  /// <summary>
  /// The ProxyFactory interface.
  /// </summary>
  /// <typeparam name="TClassMetaData">
  /// The type of class meta data.
  /// </typeparam>
  /// <typeparam name="TPropertyMetaData">
  /// The type of property meta data.
  /// </typeparam>
  public interface IProxyFactory<TClassMetaData, TPropertyMetaData>
  {
    /// <summary>
    /// Create or get the proxy.
    /// </summary>
    /// <param name="type">
    /// The type of the class.
    /// </param>
    /// <returns>
    /// The <see cref="IProxy{TClassMetaData, TPropertyMetaData}"/>.
    /// </returns>
    IProxy<TClassMetaData, TPropertyMetaData> GetProxy(Type type);
  }
}