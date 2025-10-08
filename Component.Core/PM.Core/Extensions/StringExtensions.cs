// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.StringExtensions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PathMedical.Extensions;

public static class StringExtensions
{
  public static bool Contains(this string source, string value, bool caseSensitive)
  {
    return source.IndexOf(value, caseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase) != -1;
  }

  public static bool Contains(this string source, string[] values, bool caseSensitive)
  {
    return ((IEnumerable<string>) values).Aggregate<string, bool>(false, (Func<bool, string, bool>) ((current, value) => current | source.Contains(value, false)));
  }

  public static bool Match(this string[] source, params string[] inspectionObjects)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string inspectionObject in inspectionObjects)
    {
      if (inspectionObject != null)
        stringBuilder.Append(inspectionObject);
    }
    string dataToInspect = stringBuilder.ToString();
    return ((IEnumerable<string>) source).Aggregate<string, bool>(true, (Func<bool, string, bool>) ((current, value) => current & dataToInspect.Contains(value, false)));
  }
}
