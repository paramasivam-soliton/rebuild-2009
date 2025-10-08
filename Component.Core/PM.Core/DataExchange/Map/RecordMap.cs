// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Map.RecordMap
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Property;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PathMedical.DataExchange.Map;

[DebuggerDisplay("From: {FromRecordDescription.Identifier} To: {ToRecordDescription.Identifier}")]
public class RecordMap
{
  private readonly List<RecordSplitColumn> recordSplitColumns;
  private readonly List<IColumnMapItem> columnMapping;

  public DataExchangeSetMap DataExchangeSetMap { get; protected set; }

  public RecordDescription FromRecordDescription { get; protected set; }

  public RecordDescription ToRecordDescription { get; protected set; }

  public ISupportDataExchangeColumnDescription IndexerSupportDataExchangeColumnDescription { get; protected set; }

  public bool HasIndexer => this.IndexerSupportDataExchangeColumnDescription != null;

  public IEnumerable<RecordSplitColumn> RecordSplitColumns
  {
    get => (IEnumerable<RecordSplitColumn>) this.recordSplitColumns.ToArray();
  }

  public IEnumerable<IColumnMapItem> Columns
  {
    get => (IEnumerable<IColumnMapItem>) this.columnMapping.ToArray();
  }

  public RecordMap(
    DataExchangeSetMap dataExchangeSetMap,
    RecordDescription fromRecordDescription,
    RecordDescription toRecordDescription,
    string indexerColumnName)
  {
    if (dataExchangeSetMap == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (dataExchangeSetMap));
    if (fromRecordDescription == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (fromRecordDescription));
    if (toRecordDescription == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (toRecordDescription));
    this.DataExchangeSetMap = dataExchangeSetMap;
    this.FromRecordDescription = fromRecordDescription;
    this.ToRecordDescription = toRecordDescription;
    if (!string.IsNullOrEmpty(indexerColumnName))
    {
      this.IndexerSupportDataExchangeColumnDescription = this.FromRecordDescription.GetColumnDescriptionByIdentifier(indexerColumnName);
      if (this.IndexerSupportDataExchangeColumnDescription == null)
        throw ExceptionFactory.Instance.CreateException<InvalidOperationException>($"The map contains an indexer column {indexerColumnName} but the record description {this.FromRecordDescription.Identifier} doesn't define this column");
    }
    this.columnMapping = new List<IColumnMapItem>();
    this.recordSplitColumns = new List<RecordSplitColumn>();
  }

  public void AddColumnMapItem(IColumnMapItem item)
  {
    if (item == null)
      return;
    this.columnMapping.Add(item);
  }

  public List<IndexerRecord> GetIndexerRecords(object record)
  {
    if (this.IndexerSupportDataExchangeColumnDescription != null)
    {
      IEnumerable propertyArray = (IEnumerable) PropertyHelper<DataExchangeColumnAttribute>.GetPropertyArray(this.IndexerSupportDataExchangeColumnDescription.UniqueFieldIdentifier, record);
      if (propertyArray != null)
        return propertyArray.Cast<object>().Select<object, IndexerRecord>((Func<object, IndexerRecord>) (o => new IndexerRecord()
        {
          Parent = record,
          Child = o,
          ChildDescription = this.IndexerSupportDataExchangeColumnDescription
        })).ToList<IndexerRecord>();
    }
    return (List<IndexerRecord>) null;
  }

  public DataExchangeTokenSet GetDataExchangeTokenSet(object record)
  {
    DataExchangeTokenSet exchangeTokenSet = new DataExchangeTokenSet(this.ToRecordDescription);
    foreach (IColumnMapItem column in this.Columns)
    {
      switch (column)
      {
        case ColumnMapItem _:
          DataExchangeTokenBase dataExchangeToken1 = RecordMap.CreateDataExchangeToken(column as ColumnMapItem, record);
          exchangeTokenSet.Add((ISupportDataExchangeToken) dataExchangeToken1);
          continue;
        case LiteralMapItem _:
          DataExchangeTokenBase dataExchangeToken2 = RecordMap.CreateDataExchangeToken(column as LiteralMapItem);
          exchangeTokenSet.Add((ISupportDataExchangeToken) dataExchangeToken2);
          continue;
        case CombinedMapItem _:
          DataExchangeTokenBase dataExchangeToken3 = RecordMap.CreateDataExchangeToken(column as CombinedMapItem, record);
          exchangeTokenSet.Add((ISupportDataExchangeToken) dataExchangeToken3);
          continue;
        default:
          continue;
      }
    }
    return exchangeTokenSet;
  }

  public DataExchangeTokenSet GetDataExchangeTokenSet(IndexerRecord indexerRecord)
  {
    DataExchangeTokenSet exchangeTokenSet = new DataExchangeTokenSet(this.ToRecordDescription);
    List<IColumnMapItem> columnMapItemList1 = new List<IColumnMapItem>();
    columnMapItemList1.AddRange((IEnumerable<IColumnMapItem>) this.Columns.OfType<LiteralMapItem>().ToArray<LiteralMapItem>());
    columnMapItemList1.AddRange((IEnumerable<IColumnMapItem>) this.Columns.OfType<ColumnMapItem>().Where<ColumnMapItem>((Func<ColumnMapItem, bool>) (column => string.Compare(indexerRecord.ChildDescription.UniqueFieldIdentifier, ((IEnumerable<string>) column.FullFromColumnIdentifier.Split('.')).FirstOrDefault<string>()) != 0)).ToArray<ColumnMapItem>());
    foreach (IColumnMapItem columnMapItem in columnMapItemList1)
    {
      if (columnMapItem is ColumnMapItem)
      {
        DataExchangeTokenBase dataExchangeToken = RecordMap.CreateDataExchangeToken(columnMapItem as ColumnMapItem, indexerRecord.Parent);
        exchangeTokenSet.Add((ISupportDataExchangeToken) dataExchangeToken);
      }
      else if (columnMapItem is LiteralMapItem)
      {
        DataExchangeTokenBase dataExchangeToken = RecordMap.CreateDataExchangeToken(columnMapItem as LiteralMapItem);
        exchangeTokenSet.Add((ISupportDataExchangeToken) dataExchangeToken);
      }
    }
    List<IColumnMapItem> columnMapItemList2 = new List<IColumnMapItem>();
    columnMapItemList2.AddRange((IEnumerable<IColumnMapItem>) this.Columns.OfType<ColumnMapItem>().Where<ColumnMapItem>((Func<ColumnMapItem, bool>) (column => string.Compare(indexerRecord.ChildDescription.UniqueFieldIdentifier, ((IEnumerable<string>) column.FullFromColumnIdentifier.Split('.')).FirstOrDefault<string>()) == 0)).ToArray<ColumnMapItem>());
    columnMapItemList2.AddRange((IEnumerable<IColumnMapItem>) this.Columns.OfType<CombinedMapItem>().ToArray<CombinedMapItem>());
    foreach (IColumnMapItem columnMapItem in columnMapItemList2)
    {
      if (columnMapItem is ColumnMapItem)
      {
        DataExchangeTokenBase dataExchangeToken = RecordMap.CreateDataExchangeToken(columnMapItem as ColumnMapItem, indexerRecord.Child);
        exchangeTokenSet.Add((ISupportDataExchangeToken) dataExchangeToken);
      }
    }
    foreach (CombinedMapItem columnMapItem in this.Columns.OfType<CombinedMapItem>().ToList<CombinedMapItem>())
    {
      DataExchangeTokenBase dataExchangeToken = RecordMap.CreateDataExchangeToken(columnMapItem, indexerRecord);
      exchangeTokenSet.Add((ISupportDataExchangeToken) dataExchangeToken);
    }
    return exchangeTokenSet;
  }

  internal DataExchangeTokenSet MoveDataExchangeTokenSet(DataExchangeTokenSet tokenSet)
  {
    DataExchangeTokenSet exchangeTokenSet = new DataExchangeTokenSet(this.ToRecordDescription);
    foreach (IColumnMapItem column in this.Columns)
    {
      switch (column)
      {
        case ColumnMapItem _:
          ColumnMapItem columnMapItem = column as ColumnMapItem;
          if (tokenSet[columnMapItem.FullFromColumnIdentifier] != null)
          {
            DataExchangeTokenBase dataExchangeToken = RecordMap.CreateDataExchangeToken(columnMapItem, tokenSet);
            if (dataExchangeToken != null)
            {
              exchangeTokenSet.Add((ISupportDataExchangeToken) dataExchangeToken);
              continue;
            }
            continue;
          }
          continue;
        case LiteralMapItem _:
          DataExchangeTokenBase dataExchangeToken1 = RecordMap.CreateDataExchangeToken(column as LiteralMapItem);
          exchangeTokenSet.Add((ISupportDataExchangeToken) dataExchangeToken1);
          continue;
        case CombinedMapItem _:
          DataExchangeTokenBase dataExchangeToken2 = RecordMap.CreateDataExchangeToken(column as CombinedMapItem, tokenSet);
          exchangeTokenSet.Add((ISupportDataExchangeToken) dataExchangeToken2);
          continue;
        default:
          continue;
      }
    }
    return exchangeTokenSet;
  }

  public object GetObject(Type type, DataExchangeTokenSet tokenSet)
  {
    object instance = Activator.CreateInstance(type);
    foreach (IColumnMapItem column in this.Columns)
    {
      if (column is ColumnMapItem)
      {
        ColumnMapItem columnMapItem = column as ColumnMapItem;
        PropertyHelper<DataExchangeColumnAttribute>.SetValue(columnMapItem.FullToColumnIdentifier, columnMapItem.GetDestinationValue(tokenSet), ref instance);
      }
      else if (column is LiteralMapItem)
      {
        LiteralMapItem literalMapItem = column as LiteralMapItem;
        PropertyHelper<DataExchangeColumnAttribute>.SetValue(literalMapItem.ToColumnName, literalMapItem.Literal, ref instance);
      }
    }
    return instance;
  }

  public T GetObject<T>(DataExchangeTokenSet tokenSet)
  {
    object instance = Activator.CreateInstance(typeof (T));
    foreach (IColumnMapItem column in this.Columns)
    {
      if (column is ColumnMapItem)
      {
        ColumnMapItem columnMapItem = column as ColumnMapItem;
        PropertyHelper<DataExchangeColumnAttribute>.SetValue(columnMapItem.FullToColumnIdentifier, columnMapItem.GetDestinationValue(tokenSet), ref instance);
      }
      else if (column is LiteralMapItem)
      {
        LiteralMapItem literalMapItem = column as LiteralMapItem;
        PropertyHelper<DataExchangeColumnAttribute>.SetValue(literalMapItem.ToColumnName, literalMapItem.Literal, ref instance);
      }
    }
    return (T) instance;
  }

  private static DataExchangeTokenBase CreateDataExchangeToken(
    ColumnMapItem columnMapItem,
    object record)
  {
    return new DataExchangeTokenBase()
    {
      ColumnDescription = (ISupportDataExchangeColumnDescription) columnMapItem.ToColumnDescription,
      UniqueIdentifier = columnMapItem.ToColumnDescription.UniqueFieldIdentifier,
      Data = columnMapItem.GetDestinationValue(record)
    };
  }

  private static DataExchangeTokenBase CreateDataExchangeToken(
    ColumnMapItem columnMapItem,
    DataExchangeTokenSet tokenSet)
  {
    return new DataExchangeTokenBase()
    {
      ColumnDescription = (ISupportDataExchangeColumnDescription) columnMapItem.ToColumnDescription,
      UniqueIdentifier = columnMapItem.FullToColumnIdentifier,
      Data = columnMapItem.GetDestinationValue(tokenSet)
    };
  }

  private static DataExchangeTokenBase CreateDataExchangeToken(LiteralMapItem columnMapItem)
  {
    return new DataExchangeTokenBase()
    {
      UniqueIdentifier = columnMapItem.ToColumnName,
      Data = columnMapItem.Literal
    };
  }

  private static DataExchangeTokenBase CreateDataExchangeToken(
    CombinedMapItem columnMapItem,
    IndexerRecord indexerRecord)
  {
    return new DataExchangeTokenBase()
    {
      ColumnDescription = (ISupportDataExchangeColumnDescription) columnMapItem.ToColumnDescription,
      UniqueIdentifier = columnMapItem.FullToColumnIdentifier,
      Data = columnMapItem.GetDestinationValue(indexerRecord)
    };
  }

  private static DataExchangeTokenBase CreateDataExchangeToken(
    CombinedMapItem columnMapItem,
    object record)
  {
    return new DataExchangeTokenBase()
    {
      ColumnDescription = (ISupportDataExchangeColumnDescription) columnMapItem.ToColumnDescription,
      UniqueIdentifier = columnMapItem.FullToColumnIdentifier,
      Data = columnMapItem.GetDestinationValue(record)
    };
  }

  private static DataExchangeTokenBase CreateDataExchangeToken(
    CombinedMapItem columnMapItem,
    DataExchangeTokenSet tokenSet)
  {
    return new DataExchangeTokenBase()
    {
      ColumnDescription = (ISupportDataExchangeColumnDescription) columnMapItem.ToColumnDescription,
      UniqueIdentifier = columnMapItem.FullToColumnIdentifier,
      Data = columnMapItem.GetDestinationValue(tokenSet)
    };
  }
}
