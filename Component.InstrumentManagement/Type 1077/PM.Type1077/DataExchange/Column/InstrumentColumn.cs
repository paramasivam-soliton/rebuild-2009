// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.Column.InstrumentColumn
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Type1077.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PathMedical.Type1077.DataExchange.Column;

[DebuggerDisplay("UniqueFieldIdentifier: {UniqueFieldIdentifier} Size: {DataSize}")]
[CLSCompliant(false)]
public class InstrumentColumn
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (InstrumentColumn), "$Rev: 1369 $");
  private readonly List<byte> rawData;

  public ushort UniqueFieldIdentifier { get; set; }

  public DataTypes DataTypes { get; set; }

  public ushort DataSize { get; set; }

  public object SystemValue
  {
    get
    {
      object systemValue = (object) null;
      if (this.DataTypes != DataTypes.Empty && this.rawData.Count<byte>() > 4)
        systemValue = DataConverter.GetValue(this.DataTypes, this.rawData.Skip<byte>(4).ToArray<byte>());
      return systemValue;
    }
  }

  public InstrumentColumn(byte[] binaryData)
  {
    List<byte> byteList = new List<byte>();
    this.rawData = new List<byte>();
    if (binaryData.Length < 4)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>(string.Format(Resources.InstrumentColumn_InstrumentCreationErrorDump, (object) binaryData.ToHex()));
    this.rawData.AddRange(((IEnumerable<byte>) binaryData).Take<byte>(4));
    this.UniqueFieldIdentifier = DataConverter.ToUInt16(binaryData, 0);
    this.DataSize = DataConverter.ToUInt16(binaryData, 2);
    this.DataTypes = DataTypes.Empty;
    if (binaryData.Length < 4 + (int) this.DataSize)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>(string.Format(Resources.InstrumentColumn_InstrumentCreationError, (object) this.UniqueFieldIdentifier, (object) this.DataSize, (object) (binaryData.Length - 4)));
    for (int index = 4; index < (int) this.DataSize + 4; ++index)
      this.rawData.Add(binaryData[index]);
  }

  public InstrumentColumn(ushort fieldNumber, DataTypes dataTypes, object value)
  {
    this.UniqueFieldIdentifier = fieldNumber;
    this.DataTypes = dataTypes;
    this.rawData = new List<byte>();
    if (value == null)
      return;
    this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray((short) this.UniqueFieldIdentifier));
    switch (this.DataTypes)
    {
      case DataTypes.Guid:
        switch (value)
        {
          case Guid guid1:
            this.DataSize = (ushort) 16 /*0x10*/;
            this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToUInt16(this.DataSize)));
            this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(guid1));
            goto label_29;
          case string _:
            string g = value as string;
            if (!string.IsNullOrEmpty(g) && g.Length == 32 /*0x20*/)
            {
              int length = g.Length;
              byte[] source = new byte[length / 2];
              for (int startIndex = 0; startIndex < length; startIndex += 2)
                source[startIndex / 2] = Convert.ToByte(g.Substring(startIndex, 2), 16 /*0x10*/);
              this.DataSize = (ushort) ((IEnumerable<byte>) source).Count<byte>();
              this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
              this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray((object) ((IEnumerable<byte>) source).ToArray<byte>()));
              goto label_29;
            }
            if (!string.IsNullOrEmpty(g) && g.Length == 36)
            {
              Guid guid = new Guid(g);
              this.DataSize = (ushort) 16 /*0x10*/;
              this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToUInt16(this.DataSize)));
              this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(guid));
              goto label_29;
            }
            this.DataSize = (ushort) 0;
            this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToUInt16(this.DataSize)));
            goto label_29;
          default:
            this.DataSize = (ushort) 0;
            this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToUInt16(this.DataSize)));
            goto label_29;
        }
      case DataTypes.String:
        string str = value as string;
        if (!string.IsNullOrEmpty(str))
        {
          this.DataSize = Convert.ToUInt16(str.Length * 2);
          this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
          this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(str));
          goto case DataTypes.Fract16;
        }
        this.DataSize = (ushort) 0;
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray((short) this.DataSize));
        goto case DataTypes.Fract16;
      case DataTypes.Int8:
        this.DataSize = (ushort) 1;
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToByte(value)));
        goto case DataTypes.Fract16;
      case DataTypes.UInt8:
        this.DataSize = (ushort) 1;
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToSByte(value)));
        goto case DataTypes.Fract16;
      case DataTypes.Int16:
        this.DataSize = Convert.ToUInt16(2);
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToInt16(value)));
        goto case DataTypes.Fract16;
      case DataTypes.UInt16:
        this.DataSize = Convert.ToUInt16(2);
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToUInt16(value)));
        goto case DataTypes.Fract16;
      case DataTypes.UInt16Array:
        byte[] byteArray = DataConverter.GetByteArray(value);
        if (byteArray.Length != 0)
        {
          this.DataSize = Convert.ToUInt16(byteArray.Length);
          this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
          this.rawData.AddRange((IEnumerable<byte>) byteArray);
          goto case DataTypes.Fract16;
        }
        this.DataSize = (ushort) 0;
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
        goto case DataTypes.Fract16;
      case DataTypes.Int32:
        this.DataSize = Convert.ToUInt16(4);
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToInt32(value)));
        goto case DataTypes.Fract16;
      case DataTypes.UInt32:
        this.DataSize = Convert.ToUInt16(4);
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToUInt32(value)));
        goto case DataTypes.Fract16;
      case DataTypes.Float:
        this.DataSize = Convert.ToUInt16(4);
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToSingle(value)));
        goto case DataTypes.Fract16;
      case DataTypes.Fract16:
label_29:
        InstrumentColumn.Logger.Debug("Create instrument raw data. Field [{0}] [{1}] Datatype [{2}] Value [{3}] Field Dump [{4}] ", (object) fieldNumber, (object) Convert.ToUInt16(fieldNumber).ToHex(), (object) dataTypes, value, (object) this.rawData.ToArray().ToHex());
        break;
      case DataTypes.DateTime:
        this.DataSize = Convert.ToUInt16(6);
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(Convert.ToDateTime(value)));
        goto case DataTypes.Fract16;
      default:
        this.DataSize = Convert.ToUInt16(0);
        this.rawData.AddRange((IEnumerable<byte>) DataConverter.GetByteArray(this.DataSize));
        InstrumentColumn.Logger.Error("Processing invalid datatype for field number {0} / Datatype {1}", (object) fieldNumber, (object) dataTypes);
        goto case DataTypes.Fract16;
    }
  }

  public byte[] ToByteArray() => this.rawData.ToArray();

  public override string ToString()
  {
    return string.Format(Resources.InstrumentColumn_UniqueFieldIdentifier, (object) this.UniqueFieldIdentifier, (object) this.DataTypes, (object) this.DataSize, this.SystemValue, (object) this.rawData.ToArray().ToHex());
  }
}
