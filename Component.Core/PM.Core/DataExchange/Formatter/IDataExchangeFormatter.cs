// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Formatter.IDataExchangeFormatter
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.DataExchange.Formatter;

[Obsolete]
public interface IDataExchangeFormatter
{
  Dictionary<RecordDescription, List<object>> FormattedRecords { get; }

  void AddRow(RecordDescription recordDescription, Dictionary<string, object> columns);

  void Execute();
}
