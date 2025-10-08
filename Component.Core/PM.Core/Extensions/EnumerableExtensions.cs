// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.EnumerableExtensions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Extensions;

public static class EnumerableExtensions
{
  public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
  {
    foreach (T obj in enumerable)
      action(obj);
  }

  public static IEnumerable<EnumerableMatchComparison<TItem, TIdentifier>> GetMatchComparison<TItem, TIdentifier>(
    this IEnumerable<TItem> enumerable1,
    IEnumerable<TItem> enumerable2,
    Func<TItem, TIdentifier> identifierSelector)
    where TItem : class
  {
    Dictionary<TIdentifier, TItem> enumerable2AndIdentifiers = enumerable2.ToDictionary<TItem, TIdentifier>((Func<TItem, TIdentifier>) (item => identifierSelector(item)));
    HashSet<TIdentifier> processedIdentifiers = new HashSet<TIdentifier>();
    foreach (TItem obj in enumerable1)
    {
      TIdentifier identifier = identifierSelector(obj);
      TItem valueOrDefault = enumerable2AndIdentifiers.GetValueOrDefault<TIdentifier, TItem>(identifier);
      processedIdentifiers.Add(identifier);
      yield return new EnumerableMatchComparison<TItem, TIdentifier>(obj, valueOrDefault, identifier);
    }
    foreach (TItem obj in enumerable2)
    {
      TIdentifier identifier = identifierSelector(obj);
      if (!processedIdentifiers.Contains(identifier))
        yield return new EnumerableMatchComparison<TItem, TIdentifier>(default (TItem), obj, identifier);
    }
  }
}
