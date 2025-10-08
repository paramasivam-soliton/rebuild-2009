// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.DpoaeInstrumentPresets
// Assembly: PM.DPOAE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 38B92F02-B758-4EF7-9103-415B55783CFC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.dll

using PathMedical.AudiologyTest;
using System;

#nullable disable
namespace PathMedical.DPOAE;

internal abstract class DpoaeInstrumentPresets
{
  public static readonly DpoaeInstrumentPreset Protocol1 = new DpoaeInstrumentPreset()
  {
    Id = new Guid("42BF672D-E47C-4928-B0E3-0F8764CE78B5"),
    Name = "DP 2-5",
    Category = PresetCategoryType.Basic,
    L1 = 60,
    L2 = 50,
    Criterion = 259,
    Frequencies = new ushort[4]
    {
      (ushort) 2000,
      (ushort) 3000,
      (ushort) 4000,
      (ushort) 5000
    },
    FrequencyOrder = new ushort[4]
    {
      (ushort) 3,
      (ushort) 2,
      (ushort) 1,
      (ushort) 0
    }
  };
  public static readonly DpoaeInstrumentPreset Protocol2 = new DpoaeInstrumentPreset()
  {
    Id = new Guid("E2AB5F6E-1E4F-4c23-B63F-CD3A91D9D1AA"),
    Name = "DP 2-6",
    Category = PresetCategoryType.Basic,
    L1 = 60,
    L2 = 50,
    Criterion = 260,
    Frequencies = new ushort[6]
    {
      (ushort) 2000,
      (ushort) 3000,
      (ushort) 3500,
      (ushort) 4000,
      (ushort) 5000,
      (ushort) 6000
    },
    FrequencyOrder = new ushort[6]
    {
      (ushort) 5,
      (ushort) 4,
      (ushort) 3,
      (ushort) 2,
      (ushort) 1,
      (ushort) 0
    }
  };
  public static readonly DpoaeInstrumentPreset Protocol3 = new DpoaeInstrumentPreset()
  {
    Id = new Guid("EE20F4CA-BDA4-4c3b-BAB0-C4D48D2845C7"),
    Name = "DP 1-4",
    Category = PresetCategoryType.Basic,
    L1 = 60,
    L2 = 50,
    Criterion = 3,
    Frequencies = new ushort[4]
    {
      (ushort) 1000,
      (ushort) 2000,
      (ushort) 3000,
      (ushort) 4000
    },
    FrequencyOrder = new ushort[4]
    {
      (ushort) 3,
      (ushort) 2,
      (ushort) 1,
      (ushort) 0
    }
  };
  public static readonly DpoaeInstrumentPreset Protocol4 = new DpoaeInstrumentPreset()
  {
    Id = new Guid("2C70FCED-A5FF-4861-9FD2-8F0D99787251"),
    Name = "DP 1-6",
    Category = PresetCategoryType.Basic,
    L1 = 60,
    L2 = 50,
    Criterion = 4,
    Frequencies = new ushort[6]
    {
      (ushort) 1000,
      (ushort) 2000,
      (ushort) 3000,
      (ushort) 4000,
      (ushort) 5000,
      (ushort) 6000
    },
    FrequencyOrder = new ushort[6]
    {
      (ushort) 5,
      (ushort) 4,
      (ushort) 3,
      (ushort) 2,
      (ushort) 1,
      (ushort) 0
    }
  };
}
