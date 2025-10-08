// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Formatter.FlatFileFormatter
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.Exception;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#nullable disable
namespace PathMedical.DataExchange.Formatter;

[Obsolete]
public class FlatFileFormatter : FormatterBase
{
  public Encoding Encoding { get; set; }

  public string QuotationMark { get; set; }

  public bool UnquoteQuotationMark { get; set; }

  public bool UnquoteMark { get; set; }

  public string Separator { get; set; }

  public string RecordSeparator { get; set; }

  public string EndOfFileMarker { get; set; }

  public string IsFixedLength { get; set; }

  public string LineFeed { get; set; }

  public string TargetFileName { get; protected set; }

  public FlatFileFormatter(string targetFileName)
  {
    this.TargetFileName = !string.IsNullOrEmpty(targetFileName) ? targetFileName : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (targetFileName));
    this.Encoding = Encoding.GetEncoding(850);
    this.LineFeed = Environment.NewLine;
    this.Separator = ",";
    this.QuotationMark = "\"";
  }

  public override object FormatColumn(
    ISupportDataExchangeColumnDescription supportDataExchangeColumnDescription,
    object columnValue)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (supportDataExchangeColumnDescription is TextColumnDescription)
    {
      string str = columnValue as string;
      if (!string.IsNullOrEmpty(str))
      {
        str.Replace(this.QuotationMark, "\\" + this.QuotationMark);
        stringBuilder.AppendFormat("{0}{1}{0}", (object) this.QuotationMark, (object) str);
      }
      else
        stringBuilder.AppendFormat("{0}", (object) string.Empty);
    }
    else
      stringBuilder.AppendFormat("{0}", columnValue);
    return (object) stringBuilder.ToString();
  }

  public override object FormatRow(RecordDescription recordDescription, List<object> columnValues)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string str = this.Separator;
    for (int index = 0; index < columnValues.Count; ++index)
    {
      if (index == columnValues.Count - 1)
        str = string.Empty;
      stringBuilder.AppendFormat("{0}{1}", columnValues[index], (object) str);
    }
    if (stringBuilder.Length > 0 && !string.IsNullOrEmpty(this.LineFeed))
      stringBuilder.AppendFormat("{0}", (object) this.LineFeed);
    using (StreamWriter streamWriter = new StreamWriter(this.TargetFileName, true, this.Encoding))
      streamWriter.Write(stringBuilder.ToString());
    return (object) stringBuilder.ToString();
  }
}
