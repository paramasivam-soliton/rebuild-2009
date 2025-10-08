// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.DataExchangeTokenSetOld99
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.DataExchange;

public class DataExchangeTokenSetOld99
{
  private readonly List<ISupportDataExchangeColumnDescription> columnDescriptions;
  private Dictionary<string, object> columnValues;

  public RecordDescription RecordDescription { get; protected set; }

  public DataExchangeTokenSetOld99(RecordDescription recordDescription)
  {
    this.RecordDescription = recordDescription != null ? recordDescription : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordDescription));
    this.columnValues = new Dictionary<string, object>();
    this.columnDescriptions = this.RecordDescription.Columns;
  }

  public DataExchangeTokenSetOld99 AddColumn(string name, object value)
  {
    if (string.IsNullOrEmpty(name))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (name));
    if (this.columnValues == null)
      this.columnValues = new Dictionary<string, object>();
    if (this.columnDescriptions.FirstOrDefault<ISupportDataExchangeColumnDescription>((Func<ISupportDataExchangeColumnDescription, bool>) (c => c.UniqueFieldIdentifier.Equals(name))) == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>($"Column {name} shall be added but there is no defintion of this column in the record description");
    if (this.columnValues.ContainsKey(name))
      this.columnValues.Remove(name);
    this.columnValues.Add(name, value);
    return this;
  }

  public List<object> GetColumns()
  {
    List<object> columns = new List<object>();
    foreach (ISupportDataExchangeColumnDescription column in this.RecordDescription.Columns)
    {
      object obj;
      this.columnValues.TryGetValue(column.UniqueFieldIdentifier, out obj);
      columns.Add(obj);
    }
    return columns;
  }

  public object GetColumnValue(string columnName)
  {
    if (string.IsNullOrEmpty(columnName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (columnName));
    if (this.columnValues == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("No column values available.");
    object columnValue;
    this.columnValues.TryGetValue(columnName, out columnValue);
    return columnValue;
  }
}
