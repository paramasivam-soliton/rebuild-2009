// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.Column.InstrumentToken
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Exception;
using PathMedical.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace PathMedical.Type1077.DataExchange.Column;

[DebuggerDisplay("InstrumentToken {UniqueFieldIdentifier} [{UniqueFieldIdentifierHex}] Length {Length} Data {RawDataHex}")]
public class InstrumentToken
{
  public string UniqueFieldIdentifier { get; set; }

  public string Description { get; set; }

  public int Length { get; set; }

  public byte[] Data { get; set; }

  public byte[] RawData { get; set; }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string RawDataHex => this.Data.ToHex();

  public InstrumentToken()
  {
  }

  public InstrumentToken(string uniqueFieldIdentifier, byte[] data)
  {
    this.UniqueFieldIdentifier = !string.IsNullOrEmpty(uniqueFieldIdentifier) ? uniqueFieldIdentifier : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (uniqueFieldIdentifier));
    this.Data = data;
    this.Length = data.Length;
    ushort uint16_1 = Convert.ToUInt16(this.UniqueFieldIdentifier);
    ushort uint16_2 = Convert.ToUInt16(this.Length);
    List<byte> byteList = new List<byte>();
    byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(uint16_1));
    byteList.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(uint16_2));
    byteList.AddRange((IEnumerable<byte>) data);
    this.RawData = byteList.ToArray();
  }
}
