// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.PropertyHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class PropertyHelper
{
  protected static Dictionary<PropertyInfo, PropertyHelper> helpers = new Dictionary<PropertyInfo, PropertyHelper>();
  protected PropertyAccessor accessor;

  protected PropertyHelper(PropertyInfo propertyInfo)
  {
    this.PropertyInfo = propertyInfo;
    this.PropertyName = propertyInfo.Name;
    Type propertyType = propertyInfo.PropertyType;
    if (propertyType.IsEnum)
      this.accessor = (PropertyAccessor) new EnumPropertyAccessor(propertyInfo);
    else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof (Nullable<>) && propertyType.GetGenericArguments()[0].IsEnum)
      this.accessor = (PropertyAccessor) new NullableEnumPropertyAccessor(propertyInfo);
    else if (propertyType.IsArray && propertyType.GetElementType() != typeof (byte) && typeof (IConvertible).IsAssignableFrom(propertyType.GetElementType()))
      this.accessor = (PropertyAccessor) new NumericArrayPropertyAccessor(propertyInfo);
    else
      this.accessor = new PropertyAccessor(propertyInfo);
  }

  public PropertyInfo PropertyInfo { get; protected set; }

  public string PropertyName { get; protected set; }

  public static PropertyHelper For(PropertyInfo propertyInfo)
  {
    PropertyHelper propertyHelper;
    if (!PropertyHelper.helpers.TryGetValue(propertyInfo, out propertyHelper))
    {
      propertyHelper = new PropertyHelper(propertyInfo);
      PropertyHelper.helpers.Add(propertyInfo, propertyHelper);
    }
    return propertyHelper;
  }

  public void SetValue(object target, object value) => this.accessor.SetValue(target, value);

  public object GetValue(object target) => this.accessor.GetValue(target);
}
