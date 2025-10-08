// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Type1077InstrumentConfiguration
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange.Description;

#nullable disable
namespace PathMedical.Type1077;

[DataExchangeRecord]
internal class Type1077InstrumentConfiguration
{
  [DataExchangeColumn]
  public int DisplayTimeout { get; set; }

  [DataExchangeColumn]
  public int PowerTimeout { get; set; }

  [DataExchangeColumn]
  public string InstrumentName { get; set; }
}
