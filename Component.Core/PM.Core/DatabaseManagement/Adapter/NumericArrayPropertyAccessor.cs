// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.NumericArrayPropertyAccessor
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Reflection;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class NumericArrayPropertyAccessor : PropertyAccessor
{
  protected Type elementType;

  public NumericArrayPropertyAccessor(PropertyInfo propertyInfo)
    : base(propertyInfo)
  {
    this.elementType = propertyInfo.PropertyType.GetElementType();
  }

  public override void SetValue(object target, object value)
  {
    if (value != null && value is byte[] && this.elementType != typeof (byte))
    {
      byte[] numArray = value as byte[];
      Array instance = Array.CreateInstance(this.elementType, numArray.Length / 8);
      if (this.elementType == typeof (double) || this.elementType == typeof (float))
      {
        for (int startIndex = 0; startIndex < numArray.Length; startIndex += 8)
        {
          object obj = Convert.ChangeType((object) BitConverter.ToDouble(numArray, startIndex), this.elementType);
          instance.SetValue(obj, startIndex / 8);
        }
      }
      else
      {
        for (int startIndex = 0; startIndex < numArray.Length; startIndex += 8)
        {
          object obj = Convert.ChangeType((object) BitConverter.ToInt64(numArray, startIndex), this.elementType);
          instance.SetValue(obj, startIndex / 8);
        }
      }
      value = (object) instance;
    }
    base.SetValue(target, value);
  }
}
