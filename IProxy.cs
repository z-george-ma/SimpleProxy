// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProxy.cs" company="George Ma">
//   Copyright © George Ma. All Rights Reserved.
// </copyright>
// <summary>
//   The Proxy interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SimpleProxy
{
  /// <summary>
  /// The Proxy interface.
  /// </summary>
  /// <typeparam name="TMetaData">
  /// The type for meta data.
  /// </typeparam>
  public interface IProxy<TMetaData>
  {
    /// <summary>
    /// Gets the meta data gets.
    /// </summary>
    TMetaData[] MetaDataGets { get; }

    /// <summary>
    /// Gets the meta data sets.
    /// </summary>
    TMetaData[] MetaDataSets { get; }

    /// <summary>
    /// The get values.
    /// </summary>
    /// <param name="obj">
    /// The object.
    /// </param>
    /// <returns>
    /// The <see cref="object[]"/>.
    /// </returns>
    object[] GetValues(object obj);

    /// <summary>
    /// The set values.
    /// </summary>
    /// <param name="obj">
    /// The object.
    /// </param>
    /// <param name="values">
    /// The values.
    /// </param>
    void SetValues(object obj, object[] values);

    /// <summary>
    /// The create object.
    /// </summary>
    /// <returns>
    /// The <see cref="object"/>.
    /// </returns>
    object CreateObject();
  }
}