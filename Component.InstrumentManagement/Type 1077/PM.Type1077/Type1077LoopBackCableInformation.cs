// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Type1077LoopBackCableInformation
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange.Description;

#nullable disable
namespace PathMedical.Type1077;

[DataExchangeRecord]
public class Type1077LoopBackCableInformation
{
  [DataExchangeColumn]
  public bool LoopBackTest1 { get; set; }

  [DataExchangeColumn]
  public bool LoopBackTest2 { get; set; }

  [DataExchangeColumn]
  public bool LoopBackTest3 { get; set; }

  [DataExchangeColumn]
  public bool LoopBackTest4 { get; set; }

  [DataExchangeColumn]
  public bool LoopBackTest5 { get; set; }

  [DataExchangeColumn]
  public bool LoopBackTest6 { get; set; }

  [DataExchangeColumn]
  public bool LoopBackTest7 { get; set; }

  [DataExchangeColumn]
  public float? MicrofoneRmsValue { get; set; }

  [DataExchangeColumn]
  public float? MicrofoneRms2Value { get; set; }

  [DataExchangeColumn]
  public float? AbrImp1Value { get; set; }

  [DataExchangeColumn]
  public float? AbrImp2Value { get; set; }

  [DataExchangeColumn]
  public bool LoopBackTestPassed { get; set; }

  [DataExchangeColumn]
  public bool TestAbrConnection { get; set; }

  [DataExchangeColumn]
  public bool CodecOutputLevel { get; set; }
}
