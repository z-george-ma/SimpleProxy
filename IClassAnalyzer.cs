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
  /// <typeparam name="TClassMetaData">
  /// The type for class meta data.
  /// </typeparam>
  /// <typeparam name="TPropertyMetaData">
  /// The type for property meta data.
  /// </typeparam>
  public interface IClassAnalyzer<TClassMetaData, TPropertyMetaData>
  {
    /// <summary>
    /// The process type.
    /// </summary>
    /// <param name="type">
    /// The type to be processed.
    /// </param>
    /// <param name="classMetaData">
    /// The class meta data.
    /// </param>
    /// <param name="metaDataGets">
    /// The meta data gets.
    /// </param>
    /// <param name="metaDataSets">
    /// The meta data sets.
    /// </param>
    void ProcessType(
      Type type,
      out TClassMetaData classMetaData,
      out MethodInfoMetaData<TPropertyMetaData>[] metaDataGets,
      out MethodInfoMetaData<TPropertyMetaData>[] metaDataSets);
  }
}