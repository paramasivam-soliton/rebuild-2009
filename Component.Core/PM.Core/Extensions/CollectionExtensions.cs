// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.CollectionExtensions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Extensions;

public static class CollectionExtensions
{
  public static bool IsContentEqual<T>(this ICollection<T> collection1, ICollection<T> collection2)
  {
    if (collection1 == null)
      throw new ArgumentNullException(nameof (collection1));
    return collection2 != null ? collection1.ToList<T>().IsContentEqual((ICollection) collection2.ToList<T>()) : throw new ArgumentNullException(nameof (collection2));
  }

  public static bool IsContentEqual(this ICollection collection1, ICollection collection2)
  {
    if (collection1 == null)
      throw new ArgumentNullException(nameof (collection1));
    if (collection2 == null)
      throw new ArgumentNullException(nameof (collection2));
    if (collection1.Count != collection2.Count)
      return false;
    object[] array1 = collection1.Cast<object>().OrderBy<object, int>((Func<object, int>) (i => i != null ? i.GetHashCode() : 0)).ToArray<object>();
    object[] array2 = collection2.Cast<object>().OrderBy<object, int>((Func<object, int>) (i => i != null ? i.GetHashCode() : 0)).ToArray<object>();
    for (int index = 0; index < array1.Length; ++index)
    {
      if (!object.Equals(array1[index], array2[index]))
        return false;
    }
    return true;
  }
}
