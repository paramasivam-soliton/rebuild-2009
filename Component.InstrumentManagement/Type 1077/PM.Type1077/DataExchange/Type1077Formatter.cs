// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.Type1077Formatter
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Formatter;
using PathMedical.Type1077.DataExchange.Column;
using System;

#nullable disable
namespace PathMedical.Type1077.DataExchange;

[Obsolete]
[CLSCompliant(false)]
public class Type1077Formatter : FormatterBase
{
  public override object FormatColumn(
    ISupportDataExchangeColumnDescription supportDataExchangeColumnDescription,
    object columnValue)
  {
    object obj = (object) null;
    if (supportDataExchangeColumnDescription is ColumnDescriptionBase)
    {
      ColumnDescriptionBase columnDescriptionBase = supportDataExchangeColumnDescription as ColumnDescriptionBase;
      obj = (object) new InstrumentColumn(Convert.ToUInt16(columnDescriptionBase.UniqueFieldIdentifier), columnDescriptionBase.DataTypes, columnValue).ToByteArray();
    }
    return obj;
  }
}
