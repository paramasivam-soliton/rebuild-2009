// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.Type1077StreamWriter
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Stream;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Type1077.DataExchange.Column;
using PathMedical.Type1077.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PathMedical.Type1077.DataExchange;

[CLSCompliant(false)]
public class Type1077StreamWriter : IDataExchangeStreamWriter
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (Type1077StreamWriter), "$Rev: 1403 $");
  private readonly List<RecordDescription> recordDescriptions;

  protected BinaryWriter Writer { get; set; }

  public System.IO.Stream BaseStream
  {
    get => this.Writer != null ? this.Writer.BaseStream : (System.IO.Stream) null;
  }

  private Type1077StreamWriter() => this.recordDescriptions = new List<RecordDescription>();

  public Type1077StreamWriter(System.IO.Stream stream, params RecordDescription[] recordDescription)
    : this()
  {
    if (stream == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (stream));
    if (recordDescription == null || recordDescription.Length == 0)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>(nameof (recordDescription));
    this.recordDescriptions.AddRange((IEnumerable<RecordDescription>) recordDescription);
    try
    {
      this.Writer = new BinaryWriter(stream);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(Resources.Type1077StreamWriter_WriteProtectedDestination, (System.Exception) ex);
    }
  }

  public void Open()
  {
  }

  public void Close() => this.Writer.Flush();

  public void Write(
    RecordDescription recordDescription,
    DataExchangeTokenSet[] dataExchangeTokenSets)
  {
    foreach (DataExchangeTokenSet exchangeTokenSet in dataExchangeTokenSets)
      this.Write(recordDescription, exchangeTokenSet);
  }

  public void Write(RecordDescription recordDescription, DataExchangeTokenSet dataExchangeTokenSet)
  {
    if (recordDescription == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordDescription));
    InstrumentRecord instrumentRecord = new InstrumentRecord();
    foreach (ColumnDescriptionBase columnDescriptionBase in recordDescription.Columns.OfType<ColumnDescriptionBase>())
    {
      ColumnDescriptionBase description = columnDescriptionBase;
      int uint16 = (int) Convert.ToUInt16(columnDescriptionBase.UniqueFieldIdentifier);
      DataTypes dataTypes = columnDescriptionBase.DataTypes;
      ISupportDataExchangeToken dataExchangeToken = dataExchangeTokenSet.Tokens.FirstOrDefault<ISupportDataExchangeToken>((Func<ISupportDataExchangeToken, bool>) (t => t.UniqueIdentifier.Equals(description.UniqueFieldIdentifier)));
      object obj1 = (object) null;
      if (dataExchangeToken != null)
        obj1 = dataExchangeToken.Data;
      int num = (int) dataTypes;
      object obj2 = obj1;
      InstrumentColumn instrumentColumn = new InstrumentColumn((ushort) uint16, (DataTypes) num, obj2);
      instrumentRecord.Add(instrumentColumn);
    }
    byte[] byteArray = instrumentRecord.ToByteArray();
    Type1077StreamWriter.Logger.Debug("Create raw data for for instrument record [{0}] [{1}] [{2}]. Dump [{3}]", (object) recordDescription.Identifier, (object) Convert.ToUInt16(recordDescription.Identifier).ToHex(), (object) recordDescription.Description, (object) byteArray.ToHex());
    if (byteArray.Length == 0)
      return;
    this.Writer.Write(byteArray);
  }

  public bool ContainsRecordDescription(RecordDescription recordDescription)
  {
    bool flag = false;
    if (this.recordDescriptions != null && this.recordDescriptions.Contains(recordDescription))
      flag = true;
    return flag;
  }

  private byte[] FormatColumn(
    ISupportDataExchangeColumnDescription supportDataExchangeColumnDescription,
    object columnValue)
  {
    byte[] numArray = new byte[0];
    if (supportDataExchangeColumnDescription is ColumnDescriptionBase)
    {
      ColumnDescriptionBase columnDescriptionBase = supportDataExchangeColumnDescription as ColumnDescriptionBase;
      if (columnValue != null)
        numArray = new InstrumentColumn(Convert.ToUInt16(columnDescriptionBase.UniqueFieldIdentifier), columnDescriptionBase.DataTypes, columnValue).ToByteArray();
    }
    return numArray;
  }
}
