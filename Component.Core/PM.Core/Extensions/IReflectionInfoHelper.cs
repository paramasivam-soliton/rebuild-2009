// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.IReflectionInfoHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PathMedical.Extensions;

public interface IReflectionInfoHelper
{
  IEnumerable<PropertyInfo> GetPropertiesWithAttribute(Type attribute);

  IEnumerable<object> GetAttributesForProperty(PropertyInfo propertyInfo);
}
