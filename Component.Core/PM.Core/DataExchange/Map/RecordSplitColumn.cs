// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Map.RecordSplitColumn
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.Exception;
using System;
using System.Linq;

#nullable disable
namespace PathMedical.DataExchange.Map;

public class RecordSplitColumn
{
  public ISupportDataExchangeColumnDescription SplitColumn { get; protected set; }

  public RecordMap RecordMap { get; protected set; }

  public RecordSplitColumn(RecordMap recordMap, ISupportDataExchangeColumnDescription splitColumn)
  {
    if (splitColumn == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (splitColumn));
    if (recordMap == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordMap));
    if (recordMap.Columns.OfType<ColumnMapItem>().First<ColumnMapItem>((Func<ColumnMapItem, bool>) (c => c.FromColumnDescription.UniqueFieldIdentifier == splitColumn.UniqueFieldIdentifier)) != null)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>($"Column {splitColumn.UniqueFieldIdentifier} is not defined in record map {recordMap.FromRecordDescription.Identifier}");
    this.SplitColumn = splitColumn;
    this.RecordMap = recordMap;
  }
}
