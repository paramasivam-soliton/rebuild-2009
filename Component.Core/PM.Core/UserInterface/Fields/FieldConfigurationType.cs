// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Fields.FieldConfigurationType
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.UserInterface.Fields;

[Flags]
public enum FieldConfigurationType
{
  Default = 0,
  MandatoryGroup1 = 1,
  MandatoryGroup2 = 2,
  MandatoryGroup3 = 4,
  Inactive = 128, // 0x00000080
}
