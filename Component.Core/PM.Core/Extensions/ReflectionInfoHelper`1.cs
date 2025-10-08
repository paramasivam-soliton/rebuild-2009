// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.ReflectionInfoHelper`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PathMedical.Extensions;

internal class ReflectionInfoHelper<T> : IReflectionInfoHelper
{
  private Dictionary<Type, HashSet<PropertyInfo>> attributeTypesAndProperties;
  private Dictionary<PropertyInfo, HashSet<object>> propertiesAndAttributes;

  public static ReflectionInfoHelper<T> Instance => PathMedical.Singleton.Singleton<ReflectionInfoHelper<T>>.Instance;

  private ReflectionInfoHelper() => this.Setup();

  private void Setup()
  {
    this.attributeTypesAndProperties = new Dictionary<Type, HashSet<PropertyInfo>>();
    this.propertiesAndAttributes = new Dictionary<PropertyInfo, HashSet<object>>();
    foreach (PropertyInfo property in typeof (T).GetProperties())
    {
      foreach (object customAttribute in property.GetCustomAttributes(true))
      {
        Type key = customAttribute.GetType();
        do
        {
          this.attributeTypesAndProperties.GetOrCreateValue<Type, HashSet<PropertyInfo>>(key).Add(property);
          key = key.BaseType;
        }
        while (key != typeof (Attribute));
        this.propertiesAndAttributes.GetOrCreateValue<PropertyInfo, HashSet<object>>(property).Add(customAttribute);
      }
    }
  }

  public IEnumerable<PropertyInfo> GetPropertiesWithAttribute(Type attribute)
  {
    HashSet<PropertyInfo> propertiesWithAttribute;
    if (!this.attributeTypesAndProperties.TryGetValue(attribute, out propertiesWithAttribute))
      propertiesWithAttribute = new HashSet<PropertyInfo>();
    return (IEnumerable<PropertyInfo>) propertiesWithAttribute;
  }

  public IEnumerable<object> GetAttributesForProperty(PropertyInfo propertyInfo)
  {
    HashSet<object> attributesForProperty;
    if (!this.propertiesAndAttributes.TryGetValue(propertyInfo, out attributesForProperty))
      attributesForProperty = new HashSet<object>();
    return (IEnumerable<object>) attributesForProperty;
  }
}
