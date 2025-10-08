// Decompiled with JetBrains decompiler
// Type: PathMedical.HistoryTracker.HistoryFieldAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.HistoryTracker;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class HistoryFieldAttribute : Attribute
{
  public string ResourceNameTranslation { get; set; }

  public string ResourceValueTranslation { get; set; }
}
