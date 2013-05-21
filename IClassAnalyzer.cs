// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClassAnalyzer.cs" company="George Ma">
//   Copyright © George Ma. All Rights Reserved.
// </copyright>
// <summary>
//   The ClassAnalyzer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace SimpleProxy
{
  /// <summary>
  /// The ClassAnalyzer interface.
  /// </summary>
  /// <typeparam name="TMetaData">
  /// The type for meta data.
  /// </typeparam>
  public interface IClassAnalyzer<TMetaData>
  {
    /// <summary>
    /// The process type.
    /// </summary>
    /// <param name="type">
    /// The type to be processed.
    /// </param>
    /// <param name="metaDataGets">
    /// The meta data gets.
    /// </param>
    /// <param name="metaDataSets">
    /// The meta data sets.
    /// </param>
    void ProcessType(Type type, out MethodInfoMetaData<TMetaData>[] metaDataGets, out MethodInfoMetaData<TMetaData>[] metaDataSets);
  }
}