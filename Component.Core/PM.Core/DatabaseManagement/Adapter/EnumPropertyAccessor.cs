// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.EnumPropertyAccessor
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Reflection;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class EnumPropertyAccessor : PropertyAccessor
{
  protected Type enumType;

  public EnumPropertyAccessor(PropertyInfo propertyInfo)
    : base(propertyInfo)
  {
    this.FindEnumType(propertyInfo);
    if (!this.enumType.IsEnum)
      throw new ArgumentException($"{this.enumType} is not an enum");
  }

  protected virtual void FindEnumType(PropertyInfo propertyInfo)
  {
    this.enumType = propertyInfo.PropertyType;
  }

  public override void SetValue(object target, object value)
  {
    object obj = Enum.ToObject(this.enumType, value);
    this.setter(target, obj);
  }
}
