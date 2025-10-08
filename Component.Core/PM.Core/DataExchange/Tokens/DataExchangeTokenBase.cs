// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Tokens.DataExchangeTokenBase
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using System.Diagnostics;

#nullable disable
namespace PathMedical.DataExchange.Tokens;

[DebuggerDisplay("Token {UniqueIdentifier} Data {Data}")]
public class DataExchangeTokenBase : ISupportDataExchangeToken
{
  public ISupportDataExchangeColumnDescription ColumnDescription { get; set; }

  public string UniqueIdentifier { get; set; }

  public object Data { get; set; }
}
