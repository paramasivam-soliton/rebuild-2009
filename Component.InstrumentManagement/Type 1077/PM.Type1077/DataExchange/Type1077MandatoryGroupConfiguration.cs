// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.Type1077MandatoryGroupConfiguration
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using System;

#nullable disable
namespace PathMedical.Type1077.DataExchange;

[Flags]
[CLSCompliant(false)]
public enum Type1077MandatoryGroupConfiguration : ushort
{
  MandatoryGroup1 = 256, // 0x0100
  MandatoryGroup2 = 512, // 0x0200
  MandatoryGroup3 = 1024, // 0x0400
}
