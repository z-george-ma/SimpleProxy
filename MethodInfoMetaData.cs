// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodInfoMetaData.cs" company="George Ma">
//   Copyright © George Ma. All Rights Reserved.
// </copyright>
// <summary>
//   The method info meta data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection;

namespace SimpleProxy
{
  /// <summary>
  /// The method info meta data.
  /// </summary>
  /// <typeparam name="TPropertyMetaData">
  /// The type for meta data.
  /// </typeparam>
  public struct MethodInfoMetaData<TPropertyMetaData>
  {
    /// <summary>
    /// The method info.
    /// </summary>
    public MethodInfo MethodInfo;

    /// <summary>
    /// The meta data.
    /// </summary>
    public TPropertyMetaData MetaData;
  }
}