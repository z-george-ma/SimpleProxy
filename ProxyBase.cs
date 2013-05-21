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
  /// <typeparam name="TMetaData">
  /// The type for meta data.
  /// </typeparam>
  public abstract class ProxyBase<TMetaData> : IProxy<TMetaData>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ProxyBase{TMetaData}"/> class.
    /// </summary>
    /// <param name="metaDataGets">
    /// The meta data gets.
    /// </param>
    /// <param name="metaDataSets">
    /// The meta data sets.
    /// </param>
    protected ProxyBase(TMetaData[] metaDataGets, TMetaData[] metaDataSets)
    {
      this.MetaDataGets = metaDataGets;
      this.MetaDataSets = metaDataSets;
    }

    /// <summary>
    /// Gets the meta data gets.
    /// </summary>
    public TMetaData[] MetaDataGets { get; private set; }

    /// <summary>
    /// Gets the meta data sets.
    /// </summary>
    public TMetaData[] MetaDataSets { get; private set; }

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
