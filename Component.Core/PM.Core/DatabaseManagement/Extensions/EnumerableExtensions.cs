// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Extensions.EnumerableExtensions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.Extensions;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.DatabaseManagement.Extensions;

public static class EnumerableExtensions
{
  [Obsolete]
  public static IEnumerable<T> GetCopyWithoutRelations<T>(this IEnumerable<T> enumerable) where T : class, new()
  {
    foreach (T original in enumerable)
    {
      T shallowPropertyCopy = original.CreateShallowPropertyCopy<T>();
      foreach (PropertyHelper relationProperty in (IEnumerable<PropertyHelper>) EntityHelper.For<T>().RelationProperties)
        relationProperty.SetValue((object) shallowPropertyCopy, (object) null);
      yield return shallowPropertyCopy;
    }
  }
}
