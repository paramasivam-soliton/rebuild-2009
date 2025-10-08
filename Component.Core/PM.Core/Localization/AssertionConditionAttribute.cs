// Decompiled with JetBrains decompiler
// Type: PathMedical.Localization.AssertionConditionAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.Localization;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public sealed class AssertionConditionAttribute : Attribute
{
  private readonly AssertionConditionType myConditionType;

  public AssertionConditionAttribute(AssertionConditionType conditionType)
  {
    this.myConditionType = conditionType;
  }

  public AssertionConditionType ConditionType => this.myConditionType;
}
