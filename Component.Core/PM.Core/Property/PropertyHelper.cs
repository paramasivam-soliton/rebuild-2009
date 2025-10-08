// Decompiled with JetBrains decompiler
// Type: PathMedical.Property.PropertyHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PathMedical.Property;

public static class PropertyHelper
{
  public static string GetPropertyTypeName(object property)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (property != null)
    {
      Type type1 = property.GetType();
      if (type1.IsGenericType)
      {
        Type type2 = ((IEnumerable<Type>) type1.GetGenericArguments()).FirstOrDefault<Type>();
        stringBuilder.AppendFormat("{0}<{1}>", (object) type1.GetGenericTypeDefinition().Name, (object) type2);
      }
      else if (type1.IsArray)
        stringBuilder.AppendFormat(" {0}[{1}]", (object) type1.Name, (object) type1.GetElementType());
      else if (type1.IsClass)
        stringBuilder.AppendFormat(" {0}", property);
      else if (type1.IsPrimitive)
        stringBuilder.AppendFormat(" {0}", property);
      else
        stringBuilder.Append(" Unknown or null");
    }
    else
      stringBuilder.Append("null");
    return stringBuilder.ToString();
  }
}
