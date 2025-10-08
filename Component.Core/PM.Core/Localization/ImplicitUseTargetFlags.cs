// Decompiled with JetBrains decompiler
// Type: PathMedical.Localization.ImplicitUseTargetFlags
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.Localization;

[Flags]
public enum ImplicitUseTargetFlags
{
  Default = 1,
  Itself = Default, // 0x00000001
  Members = 2,
  WithMembers = Members | Itself, // 0x00000003
}
