// Decompiled with JetBrains decompiler
// Type: PathMedical.Localization.BaseTypeRequiredAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.Localization;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
[BaseTypeRequired(new Type[] {typeof (Attribute)})]
public sealed class BaseTypeRequiredAttribute : Attribute
{
  private readonly Type[] myBaseTypes;

  public BaseTypeRequiredAttribute(params Type[] baseTypes) => this.myBaseTypes = baseTypes;

  public IEnumerable<Type> BaseTypes => (IEnumerable<Type>) this.myBaseTypes;
}
