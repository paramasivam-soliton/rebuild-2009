// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Formatter.FormatterBase
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.DataExchange.Formatter;

[Obsolete]
public abstract class FormatterBase : IDataExchangeFormatter
{
  protected Dictionary<RecordDescription, List<Dictionary<string, object>>> DataStore = new Dictionary<RecordDescription, List<Dictionary<string, object>>>();

  public Dictionary<RecordDescription, List<object>> FormattedRecords { get; protected set; }

  public void AddRow(RecordDescription recordDescription, Dictionary<string, object> columns)
  {
    List<Dictionary<string, object>> dictionaryList;
    if (!this.DataStore.TryGetValue(recordDescription, out dictionaryList))
    {
      dictionaryList = new List<Dictionary<string, object>>();
      this.DataStore.Add(recordDescription, dictionaryList);
    }
    dictionaryList.Add(columns);
  }

  public void Execute()
  {
    this.FormattedRecords = new Dictionary<RecordDescription, List<object>>();
    foreach (RecordDescription key in this.DataStore.Keys)
    {
      List<Dictionary<string, object>> dictionaryList;
      if (this.DataStore.TryGetValue(key, out dictionaryList))
      {
        foreach (Dictionary<string, object> dictionary in dictionaryList)
        {
          List<object> columnValues = new List<object>();
          foreach (ISupportDataExchangeColumnDescription column in key.Columns)
          {
            object columnValue;
            dictionary.TryGetValue(column.UniqueFieldIdentifier, out columnValue);
            columnValues.Add(this.FormatColumn(column, columnValue));
          }
          object obj = this.FormatRow(key, columnValues);
          List<object> objectList;
          if (!this.FormattedRecords.TryGetValue(key, out objectList))
          {
            objectList = new List<object>();
            this.FormattedRecords.Add(key, objectList);
          }
          objectList.Add(obj);
        }
      }
    }
  }

  public virtual object FormatColumn(
    ISupportDataExchangeColumnDescription supportDataExchangeColumnDescription,
    object columnValue)
  {
    return columnValue;
  }

  public virtual object FormatRow(RecordDescription recordDescription, List<object> columnValues)
  {
    return (object) columnValues;
  }
}
