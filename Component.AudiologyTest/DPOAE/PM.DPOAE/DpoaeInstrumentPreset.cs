// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.DpoaeInstrumentPreset
// Assembly: PM.DPOAE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 38B92F02-B758-4EF7-9103-415B55783CFC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.dll

using PathMedical.AudiologyTest;
using PathMedical.DataExchange.Description;
using System;

#nullable disable
namespace PathMedical.DPOAE;

[DataExchangeRecord]
internal class DpoaeInstrumentPreset
{
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DataExchangeColumn]
  public string Name { get; set; }

  [DataExchangeColumn]
  public PresetCategoryType Category { get; set; }

  [DataExchangeColumn]
  public ushort L1 { get; set; }

  [DataExchangeColumn]
  public ushort L2 { get; set; }

  [DataExchangeColumn]
  public ushort[] Frequencies { get; set; }

  [DataExchangeColumn]
  public ushort[] FrequencyOrder { get; set; }

  [DataExchangeColumn]
  public ushort Criterion { get; set; }
}
