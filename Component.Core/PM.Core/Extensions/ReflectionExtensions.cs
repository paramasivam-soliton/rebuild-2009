// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.ReflectionExtensions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PathMedical.Extensions;

public static class ReflectionExtensions
{
  public static PropertyInfo GetPropertyWithAttribute<T>(this Type type) where T : Attribute
  {
    return type.GetPropertyWithAttribute<T>((Func<T, bool>) null);
  }

  public static PropertyInfo GetPropertyWithAttribute<T>(this Type type, Func<T, bool> condition) where T : Attribute
  {
    return type.GetPropertiesWithAttribute<T>(condition).SingleOrDefault<PropertyInfo>();
  }

  public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(this Type type) where T : Attribute
  {
    return type.GetPropertiesWithAttribute<T>((Func<T, bool>) null);
  }

  public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(
    this Type type,
    Func<T, bool> condition)
    where T : Attribute
  {
    IReflectionInfoHelper rih = type.GetReflectionInfoHelper();
    IEnumerable<PropertyInfo> propertiesWithAttribute = rih.GetPropertiesWithAttribute(typeof (T));
    return condition == null ? propertiesWithAttribute : propertiesWithAttribute.Where<PropertyInfo>((Func<PropertyInfo, bool>) (pi => rih.GetAttributesForProperty(pi).Any<object>((Func<object, bool>) (a => a is T && condition(a as T)))));
  }

  public static IReflectionInfoHelper GetReflectionInfoHelper(this Type type)
  {
    return typeof (ReflectionInfoHelper<>).MakeGenericType(type).GetProperty("Instance").GetValue((object) null, (object[]) null) as IReflectionInfoHelper;
  }
}
