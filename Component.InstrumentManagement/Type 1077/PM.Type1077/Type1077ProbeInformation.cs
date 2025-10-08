// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Type1077ProbeInformation
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Description;
using PathMedical.Type1077.DataExchange;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Type1077;

[DataExchangeRecord]
public class Type1077ProbeInformation
{
  [DataExchangeColumn]
  public byte[] ProbeRawData { get; set; }

  public uint? ProbeType
  {
    get => new uint?((uint) this.GetValue(DataTypes.UInt32, 12));
    set => this.SetValue(DataTypes.UInt32, 12, (object) value);
  }

  public string ProbeTypeString
  {
    get => this.GetProbeTypeString(DataTypes.UInt32, 12);
    set => this.SetValue(DataTypes.UInt32, 12, (object) value);
  }

  public uint? SerialNumber => new uint?((uint) this.GetValue(DataTypes.UInt32, 8));

  public DateTime? CalibrationDate
  {
    get => new DateTime?(this.GetDate(DataTypes.Int8, 96 /*0x60*/));
    set
    {
      if (!value.HasValue)
        return;
      byte[] byteArray = DataConverter.GetByteArray((object) value);
      for (int index = 0; index < byteArray.Length || index < 3; ++index)
        this.SetValue(DataTypes.DateTime, 96 /*0x60*/ + index, (object) byteArray[index]);
      for (int index = 0; index < 3; ++index)
        this.SetValue(DataTypes.DateTime, 99 + index, (object) 0);
    }
  }

  public sbyte? Speaker1CorrectionValue1KHz
  {
    get => new sbyte?((sbyte) this.GetValue(DataTypes.Int8, 50));
    set => this.SetValue(DataTypes.Int8, 50, (object) value);
  }

  public sbyte? Speaker1CorrectionValue2KHz
  {
    get => new sbyte?((sbyte) this.GetValue(DataTypes.Int8, 51));
    set => this.SetValue(DataTypes.Int8, 51, (object) value);
  }

  public sbyte? Speaker1CorrectionValue4KHz
  {
    get => new sbyte?((sbyte) this.GetValue(DataTypes.Int8, 53));
    set => this.SetValue(DataTypes.Int8, 53, (object) value);
  }

  public sbyte? Speaker1CorrectionValue6KHz
  {
    get => new sbyte?((sbyte) this.GetValue(DataTypes.Int8, 54));
    set => this.SetValue(DataTypes.Int8, 54, (object) value);
  }

  public sbyte? Speaker2CorrectionValue1KHz
  {
    get => new sbyte?((sbyte) this.GetValue(DataTypes.Int8, 58));
    set => this.SetValue(DataTypes.Int8, 58, (object) value);
  }

  public sbyte? Speaker2CorrectionValue2KHz
  {
    get => new sbyte?((sbyte) this.GetValue(DataTypes.Int8, 59));
    set => this.SetValue(DataTypes.Int8, 59, (object) value);
  }

  public sbyte? Speaker2CorrectionValue4KHz
  {
    get => new sbyte?((sbyte) this.GetValue(DataTypes.Int8, 61));
    set => this.SetValue(DataTypes.Int8, 61, (object) value);
  }

  public sbyte? Speaker2CorrectionValue6KHz
  {
    get => new sbyte?((sbyte) this.GetValue(DataTypes.Int8, 62));
    set => this.SetValue(DataTypes.Int8, 62, (object) value);
  }

  public sbyte? Microfone1CorrectionValue1KHz
  {
    get => new sbyte?((sbyte) this.GetValue(DataTypes.Int8, 34));
    set => this.SetValue(DataTypes.Int8, 34, (object) value);
  }

  [DataExchangeColumn]
  public float? MicrofoneRmsValue { get; set; }

  [DataExchangeColumn]
  public float? MicrofoneRms2Value { get; set; }

  [DataExchangeColumn]
  public float? AbrImp1Value { get; set; }

  [DataExchangeColumn]
  public float? AbrImp2Value { get; set; }

  [DataExchangeColumn]
  public bool LoopBackTest1 { get; set; }

  private object GetValue(DataTypes dataType, int offSet)
  {
    object obj = (object) null;
    if (this.ProbeRawData != null && ((IEnumerable<byte>) this.ProbeRawData).Count<byte>() == 128 /*0x80*/)
    {
      IEnumerable<byte> source = ((IEnumerable<byte>) this.ProbeRawData).Skip<byte>(offSet);
      obj = DataConverter.GetValue(dataType, source.ToArray<byte>());
    }
    return obj;
  }

  private string GetProbeTypeString(DataTypes dataType, int offSet)
  {
    uint num = 0;
    string probeTypeString = (string) null;
    if (this.ProbeRawData != null && ((IEnumerable<byte>) this.ProbeRawData).Count<byte>() == 128 /*0x80*/)
    {
      IEnumerable<byte> source = ((IEnumerable<byte>) this.ProbeRawData).Skip<byte>(offSet);
      num = (uint) DataConverter.GetValue(dataType, source.ToArray<byte>());
    }
    if (34833U == num)
      probeTypeString = "EP-DP";
    if (34577U == num)
      probeTypeString = "EP-TE";
    if (34304U == num)
      probeTypeString = "ECC Cable";
    return probeTypeString;
  }

  private DateTime GetDate(DataTypes dataType, int offSet)
  {
    return new DateTime(Convert.ToInt32(this.ProbeRawData.GetValue(offSet)) + 1900, Convert.ToInt32(this.ProbeRawData.GetValue(offSet + 1)), Convert.ToInt32(this.ProbeRawData.GetValue(offSet + 2)));
  }

  private void SetValue(DataTypes dataType, int offSet, object value)
  {
    if (this.ProbeRawData == null || ((IEnumerable<byte>) this.ProbeRawData).Count<byte>() != 128 /*0x80*/)
      return;
    byte[] byteArray = DataConverter.GetByteArray(value);
    if (byteArray.Length == 0)
      return;
    this.ProbeRawData.SetValue((object) byteArray[0], offSet);
  }
}
