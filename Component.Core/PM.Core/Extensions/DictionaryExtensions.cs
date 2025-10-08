// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.DictionaryExtensions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.Extensions;

public static class DictionaryExtensions
{
  public static TValue GetOrCreateValue<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary,
    TKey key)
    where TValue : class, new()
  {
    if (dictionary == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (dictionary));
    TValue obj;
    if (!dictionary.TryGetValue(key, out obj))
    {
      obj = new TValue();
      dictionary.Add(key, obj);
    }
    return obj;
  }

  public static void AddOrOverwriteEntry<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary,
    TKey key,
    TValue value)
  {
    if (dictionary == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (dictionary));
    if (dictionary.TryGetValue(key, out TValue _))
      dictionary[key] = value;
    else
      dictionary.Add(key, value);
  }

  public static TValue GetValueOrDefault<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary,
    TKey key)
  {
    return dictionary.GetValueOrDefault<TKey, TValue>(key, default (TValue));
  }

  public static TValue GetValueOrDefault<TKey, TValue>(
    this IDictionary<TKey, TValue> dictionary,
    TKey key,
    TValue defaultValue)
  {
    if (dictionary == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (dictionary));
    TValue obj;
    return !dictionary.TryGetValue(key, out obj) ? defaultValue : obj;
  }
}
