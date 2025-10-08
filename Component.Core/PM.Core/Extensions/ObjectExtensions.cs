// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.ObjectExtensions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;
using System.Reflection;

#nullable disable
namespace PathMedical.Extensions;

public static class ObjectExtensions
{
  public static T CreateShallowPropertyCopy<T>(this T original) where T : class, new()
  {
    if ((object) original == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (original));
    T shallowPropertyCopy = new T();
    foreach (PropertyInfo property in typeof (T).GetProperties())
    {
      if (property.CanRead && property.CanWrite)
      {
        object obj = property.GetValue((object) original, (object[]) null);
        property.SetValue((object) shallowPropertyCopy, obj, (object[]) null);
      }
    }
    return shallowPropertyCopy;
  }
}
