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
  /// <typeparam name="TClassMetaData">
  /// The type for class meta data.
  /// </typeparam>
  /// <typeparam name="TPropertyMetaData">
  /// The type for property meta data.
  /// </typeparam>
  public interface IProxy<TClassMetaData, TPropertyMetaData>
  {
    /// <summary>
    /// Gets the meta data gets.
    /// </summary>
    TPropertyMetaData[] MetaDataGets { get; }

    /// <summary>
    /// Gets the meta data sets.
    /// </summary>
    TPropertyMetaData[] MetaDataSets { get; }

    /// <summary>
    /// Gets the class meta data.
    /// </summary>
    TClassMetaData ClassMetaData { get; }

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