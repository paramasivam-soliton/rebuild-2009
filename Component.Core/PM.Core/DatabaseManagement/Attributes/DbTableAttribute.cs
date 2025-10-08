// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Attributes.DbTableAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.ComponentModel;

#nullable disable
namespace PathMedical.DatabaseManagement.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DbTableAttribute : Attribute
{
  public bool HasHistory { get; set; }

  [Localizable(false)]
  public string Name { get; set; }
}
