// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Map.ColumnMapItem
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.Property;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace PathMedical.DataExchange.Map;

[DebuggerDisplay("ColumnMapItem From: {FullFromColumnIdentifier} To: {FullToColumnIdentifier}")]
public class ColumnMapItem : IColumnMapItem
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (ColumnMapItem), "$Rev: 1187 $");
  private readonly TypeCode fromColumnTypeCode;
  private readonly TypeCode toColumnTypeCode;
  private readonly Dictionary<object, object> mapItems;
  private readonly Dictionary<object, object> conditions;

  public RecordMap RecordMap { get; protected set; }

  public string FullFromColumnIdentifier { get; private set; }

  public string FullToColumnIdentifier { get; private set; }

  public ColumnDescriptionBase FromColumnDescription { get; protected set; }

  public string FromColumnIndex { get; protected set; }

  public ColumnDescriptionBase ToColumnDescription { get; protected set; }

  public bool IsMandatory { get; set; }

  public bool HasValueMapping => this.mapItems.Count > 0;

  public bool HasCondition => this.conditions.Count > 0;

  public ColumnMapItem(RecordMap recordMap, string fromColumnName, string toColumnName)
  {
    if (recordMap == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>($"recordMap for mapping column [{fromColumnName}] to [{toColumnName}] is undefined");
    if (string.IsNullOrEmpty(fromColumnName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>($"fromColumnName for mapping to column [{toColumnName}] in map [{recordMap}] is undefined");
    if (string.IsNullOrEmpty(toColumnName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>($"toColumnName for mapping from column [{fromColumnName}] in map [{recordMap}] is undefined");
    this.RecordMap = recordMap;
    this.IsMandatory = false;
    this.mapItems = new Dictionary<object, object>();
    this.conditions = new Dictionary<object, object>();
    if (this.RecordMap == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>($"Can't add a mapping from column {fromColumnName} to {toColumnName} because the record map is null.");
    if (this.RecordMap.FromRecordDescription == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add column mapping from {0} to {1} because record description (for {0}) of the record map is undefined.", (object) fromColumnName, (object) toColumnName));
    if (this.RecordMap.ToRecordDescription == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add column mapping for column {0} to {1} because record description (for {1} of the record map is undefined.", (object) fromColumnName, (object) toColumnName));
    this.FullFromColumnIdentifier = fromColumnName;
    this.FullToColumnIdentifier = toColumnName;
    ISupportDataExchangeColumnDescription descriptionByIdentifier1 = this.RecordMap.FromRecordDescription.GetColumnDescriptionByIdentifier(fromColumnName);
    if (descriptionByIdentifier1 == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping for column {0} to {1} because there is no column description for {0} in record description {2}", (object) fromColumnName, (object) toColumnName, (object) this.RecordMap.FromRecordDescription.Identifier));
    this.FromColumnDescription = descriptionByIdentifier1 is ColumnDescriptionBase ? descriptionByIdentifier1 as ColumnDescriptionBase : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping for column {0} to {1} because the column description {0} for record description {2} is a complex column description.", (object) fromColumnName, (object) toColumnName, (object) this.RecordMap.FromRecordDescription.Identifier));
    if (this.FromColumnDescription == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping for column {0} to {1} because there is no column description for {0} in record description {2}", (object) fromColumnName, (object) toColumnName, (object) this.RecordMap.FromRecordDescription.Identifier));
    ISupportDataExchangeColumnDescription descriptionByIdentifier2 = this.RecordMap.ToRecordDescription.GetColumnDescriptionByIdentifier(toColumnName);
    if (descriptionByIdentifier2 == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping for column {0} to {1} because there is no column description for {1} in record description {2}", (object) fromColumnName, (object) toColumnName, (object) this.RecordMap.ToRecordDescription.Identifier));
    this.ToColumnDescription = descriptionByIdentifier2 is ColumnDescriptionBase ? descriptionByIdentifier2 as ColumnDescriptionBase : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping for column {0} to {1} because the column description {0} for record description {2} is a complex column description.", (object) fromColumnName, (object) toColumnName, (object) this.RecordMap.ToRecordDescription.Identifier));
    if (this.ToColumnDescription == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping for column {0} to {1} because there is no column description for {1} in record description {2}", (object) fromColumnName, (object) toColumnName, (object) this.RecordMap.ToRecordDescription.Identifier));
    this.fromColumnTypeCode = ColumnMapItem.GetTypeCode(this.FromColumnDescription.DataTypes);
    this.toColumnTypeCode = ColumnMapItem.GetTypeCode(this.ToColumnDescription.DataTypes);
  }

  public ColumnMapItem(
    RecordMap recordMap,
    string fromColumnName,
    string toColumnName,
    string fromColumnIndex)
    : this(recordMap, fromColumnName, toColumnName)
  {
    this.FromColumnIndex = fromColumnIndex;
  }

  public void AddMapping(object fromValue, object toValue)
  {
    object key;
    try
    {
      key = Convert.ChangeType(fromValue, this.fromColumnTypeCode);
    }
    catch (InvalidCastException ex)
    {
      throw ExceptionFactory.Instance.CreateException<InvalidCastException>(string.Format("The value of column {0} ({1}) in map {4} can't be casted from {2} into {3}.", (object) this.FromColumnDescription.UniqueFieldIdentifier, (object) this.FullToColumnIdentifier, (object) fromValue.GetType(), (object) this.fromColumnTypeCode, (object) this.RecordMap.DataExchangeSetMap.Name), (System.Exception) ex);
    }
    object obj;
    try
    {
      obj = Convert.ChangeType(toValue, this.toColumnTypeCode);
    }
    catch (InvalidCastException ex)
    {
      throw ExceptionFactory.Instance.CreateException<InvalidCastException>(string.Format("The value of column {0} ({1}) in map {4} can't be casted from {2} into {3}.", (object) this.ToColumnDescription.UniqueFieldIdentifier, (object) this.FullToColumnIdentifier, (object) toValue.GetType(), (object) this.toColumnTypeCode, (object) this.RecordMap.DataExchangeSetMap.Name), (System.Exception) ex);
    }
    if (this.mapItems.ContainsKey(key))
      return;
    this.mapItems.Add(key, obj);
  }

  public object GetDestinationValue(object record)
  {
    string propertyName = this.FullFromColumnIdentifier;
    if (this.RecordMap.HasIndexer)
    {
      string oldValue = this.RecordMap.IndexerSupportDataExchangeColumnDescription.UniqueFieldIdentifier + ".";
      if (propertyName.StartsWith(oldValue))
        propertyName = propertyName.Replace(oldValue, string.Empty);
    }
    object destinationValue = string.IsNullOrEmpty(this.FromColumnIndex) ? PropertyHelper<DataExchangeColumnAttribute>.GetPropertyValue(propertyName, record) : PropertyHelper<DataExchangeColumnAttribute>.GetPropertyValue(propertyName, this.FromColumnIndex, record);
    if (destinationValue == null && this.IsMandatory)
      throw ExceptionFactory.Instance.CreateException<ConstraintException>($"Column {this.FullFromColumnIdentifier} is mandatory but has no value.");
    if (this.HasValueMapping && destinationValue != null)
      this.mapItems.TryGetValue(Convert.ChangeType(destinationValue, this.fromColumnTypeCode), out destinationValue);
    if (destinationValue != null && !string.IsNullOrEmpty(this.ToColumnDescription.Format))
      destinationValue = (object) string.Format(this.ToColumnDescription.Format, destinationValue);
    if (this.ToColumnDescription is TextColumnDescription && destinationValue != null)
    {
      TextColumnDescription columnDescription = this.ToColumnDescription as TextColumnDescription;
      string str = Convert.ToString(destinationValue);
      int? minimumLength = columnDescription.MinimumLength;
      int? nullable;
      if (minimumLength.HasValue)
      {
        minimumLength = columnDescription.MinimumLength;
        int? maximumLength = columnDescription.MaximumLength;
        if (minimumLength.GetValueOrDefault() <= maximumLength.GetValueOrDefault() & minimumLength.HasValue & maximumLength.HasValue)
        {
          int length = str.Length;
          nullable = columnDescription.MinimumLength;
          int valueOrDefault = nullable.GetValueOrDefault();
          if (length < valueOrDefault & nullable.HasValue)
            str = str.PadRight(Convert.ToInt32((object) columnDescription.MinimumLength));
        }
      }
      nullable = columnDescription.MaximumLength;
      if (nullable.HasValue)
      {
        int length = str.Length;
        nullable = columnDescription.MaximumLength;
        int valueOrDefault = nullable.GetValueOrDefault();
        if (length > valueOrDefault & nullable.HasValue)
          str = str.Substring(0, Convert.ToInt32((object) columnDescription.MaximumLength));
      }
      destinationValue = (object) str;
    }
    return destinationValue;
  }

  public object GetDestinationValue(DataExchangeTokenSet tokenSet)
  {
    object obj = (object) null;
    object destinationValue = (object) null;
    string columnIdentifier = this.FullFromColumnIdentifier;
    if (tokenSet[columnIdentifier] != null)
    {
      obj = tokenSet[columnIdentifier].Data;
      destinationValue = obj;
    }
    if (destinationValue == null && this.IsMandatory)
      throw ExceptionFactory.Instance.CreateException<ConstraintException>($"Column {this.FullFromColumnIdentifier} is mandatory but has no value.");
    if (this.HasValueMapping && destinationValue != null)
      this.mapItems.TryGetValue(Convert.ChangeType(destinationValue, this.fromColumnTypeCode), out destinationValue);
    if (destinationValue != null && !string.IsNullOrEmpty(this.ToColumnDescription.Format))
      destinationValue = (object) string.Format(this.ToColumnDescription.Format, destinationValue);
    if (this.ToColumnDescription is TextColumnDescription && destinationValue != null)
    {
      TextColumnDescription columnDescription = this.ToColumnDescription as TextColumnDescription;
      string str = Convert.ToString(destinationValue);
      int? minimumLength = columnDescription.MinimumLength;
      int? nullable;
      if (minimumLength.HasValue)
      {
        minimumLength = columnDescription.MinimumLength;
        nullable = columnDescription.MaximumLength;
        if (minimumLength.GetValueOrDefault() <= nullable.GetValueOrDefault() & minimumLength.HasValue & nullable.HasValue)
        {
          int length = str.Length;
          nullable = columnDescription.MinimumLength;
          int valueOrDefault = nullable.GetValueOrDefault();
          if (length < valueOrDefault & nullable.HasValue)
            str = str.PadRight(Convert.ToInt32((object) columnDescription.MinimumLength));
        }
      }
      nullable = columnDescription.MaximumLength;
      if (nullable.HasValue)
      {
        int length = str.Length;
        nullable = columnDescription.MaximumLength;
        int valueOrDefault = nullable.GetValueOrDefault();
        if (length > valueOrDefault & nullable.HasValue)
          str = str.Substring(0, Convert.ToInt32((object) columnDescription.MaximumLength));
      }
      destinationValue = (object) str;
    }
    ColumnMapItem.Logger.Debug("Mapped column {0}[{1}] to column {2} [{3}]", (object) this.FullFromColumnIdentifier, obj, (object) this.FullToColumnIdentifier, destinationValue);
    return destinationValue;
  }

  private static TypeCode GetTypeCode(DataTypes dataTypes)
  {
    TypeCode typeCode;
    switch (dataTypes)
    {
      case DataTypes.Boolean:
        typeCode = TypeCode.Boolean;
        break;
      case DataTypes.String:
        typeCode = TypeCode.String;
        break;
      case DataTypes.Int8:
        typeCode = TypeCode.SByte;
        break;
      case DataTypes.UInt8:
        typeCode = TypeCode.Byte;
        break;
      case DataTypes.Int16:
        typeCode = TypeCode.Int16;
        break;
      case DataTypes.UInt16:
        typeCode = TypeCode.UInt16;
        break;
      case DataTypes.Int32:
        typeCode = TypeCode.Int32;
        break;
      case DataTypes.UInt32:
        typeCode = TypeCode.UInt32;
        break;
      case DataTypes.Float:
        typeCode = TypeCode.Single;
        break;
      case DataTypes.Fract16:
        typeCode = TypeCode.Single;
        break;
      default:
        typeCode = TypeCode.Object;
        break;
    }
    return typeCode;
  }
}
