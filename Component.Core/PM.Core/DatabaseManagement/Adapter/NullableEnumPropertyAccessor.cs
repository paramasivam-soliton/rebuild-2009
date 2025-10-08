// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.NullableEnumPropertyAccessor
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Reflection;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class NullableEnumPropertyAccessor(PropertyInfo propertyInfo) : EnumPropertyAccessor(propertyInfo)
{
  protected override void FindEnumType(PropertyInfo propertyInfo)
  {
    this.enumType = propertyInfo.PropertyType.GetGenericArguments()[0];
  }

  public override void SetValue(object target, object value)
  {
    if (value == null || value == DBNull.Value)
    {
      this.setter(target, (object) null);
    }
    else
    {
      object obj = Enum.ToObject(this.enumType, value);
      this.setter(target, obj);
    }
  }
}
