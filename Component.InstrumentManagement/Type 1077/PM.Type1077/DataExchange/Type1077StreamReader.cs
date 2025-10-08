// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.Type1077StreamReader
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange.Stream;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Logging;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.Type1077.DataExchange;

[CLSCompliant(false)]
public class Type1077StreamReader : IDataExchangeStreamReader
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (Type1077StreamReader), "$Rev: 1187 $");

  protected System.IO.Stream BaseStream { get; set; }

  public Type1077StreamReader()
  {
  }

  public Type1077StreamReader(System.IO.Stream stream)
    : this()
  {
    this.BaseStream = stream != null ? stream : throw ExceptionFactory.Instance.CreateException<ArgumentException>(nameof (stream));
  }

  public void Open()
  {
  }

  public void Close()
  {
  }

  public List<DataExchangeTokenSet> Read()
  {
    return DataImportLexer.Instance.RecordDescriptionSet != null ? DataImportLexer.Instance.Parse(this.BaseStream) : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("Unable to read since the lexer has no description for data structure.");
  }
}
