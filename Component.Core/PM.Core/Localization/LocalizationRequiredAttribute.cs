// Decompiled with JetBrains decompiler
// Type: PathMedical.Localization.LocalizationRequiredAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.Localization;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
public sealed class LocalizationRequiredAttribute : Attribute
{
  public LocalizationRequiredAttribute(bool required) => this.Required = required;

  public bool Required { get; set; }

  public override bool Equals(object obj)
  {
    return obj is LocalizationRequiredAttribute requiredAttribute && requiredAttribute.Required == this.Required;
  }

  public override int GetHashCode() => base.GetHashCode();
}
