// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Map.CombinedMapItem
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Tokens;
using PathMedical.Encryption;
using PathMedical.Exception;
using PathMedical.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PathMedical.DataExchange.Map;

public class CombinedMapItem : IColumnMapItem
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (CombinedMapItem), "$Rev: 1034 $");
  private readonly TypeCode toColumnTypeCode;
  private readonly List<ReferenceColumnMapItem> referenceColumns = new List<ReferenceColumnMapItem>();

  public RecordMap RecordMap { get; protected set; }

  public string FullToColumnIdentifier { get; private set; }

  public ColumnDescriptionBase ToColumnDescription { get; protected set; }

  public CombinedMapType MapType { get; protected set; }

  public bool IsMandatory { get; set; }

  public string Seperator { get; set; }

  public CombinedMapItem(RecordMap recordMap, string mapType, string toColumnName)
  {
    if (recordMap == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>($"recordMap for mapping a column to [{toColumnName}] is undefined");
    if (string.IsNullOrEmpty(mapType))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>($"mapType for mapping type to column [{toColumnName}] in map [{recordMap}] is undefined");
    if (string.IsNullOrEmpty(toColumnName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>($"toColumnName for mapping in map [{recordMap}] is undefined");
    this.RecordMap = recordMap;
    this.IsMandatory = false;
    if (this.RecordMap == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>($"Can't add a combined mapping to {toColumnName} because the record map is null.");
    if (this.RecordMap.FromRecordDescription == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>($"Can't add a combined column mapping to {toColumnName} because source record description of the record map is undefined.");
    if (this.RecordMap.ToRecordDescription == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a combined column mapping to {0} because record description (for {0} of the record map is undefined.", (object) toColumnName));
    this.FullToColumnIdentifier = toColumnName;
    ISupportDataExchangeColumnDescription descriptionByIdentifier = this.RecordMap.ToRecordDescription.GetColumnDescriptionByIdentifier(toColumnName);
    if (descriptionByIdentifier == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping to column {0} because there is no column description for {0} in record description {1}", (object) toColumnName, (object) this.RecordMap.ToRecordDescription.Identifier));
    this.ToColumnDescription = descriptionByIdentifier is ColumnDescriptionBase ? descriptionByIdentifier as ColumnDescriptionBase : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping to column {0} because the column description {0} for record description {1} is a complex column description.", (object) toColumnName, (object) this.RecordMap.ToRecordDescription.Identifier));
    this.toColumnTypeCode = this.ToColumnDescription != null ? CombinedMapItem.GetTypeCode(this.ToColumnDescription.DataTypes) : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping to column to {0} because there is no column description for {0} in record description {1}", (object) toColumnName, (object) this.RecordMap.ToRecordDescription.Identifier));
    this.MapType = (CombinedMapType) Enum.Parse(typeof (CombinedMapType), mapType);
  }

  public void AddReferenceColumnMapItem(ReferenceColumnMapItem referenceColumnMapItem)
  {
    if (referenceColumnMapItem == null || this.referenceColumns.Contains(referenceColumnMapItem))
      return;
    this.referenceColumns.Add(referenceColumnMapItem);
  }

  public object GetDestinationValue(object record)
  {
    object columnValue = (object) null;
    StringBuilder stringBuilder = new StringBuilder();
    string str = string.Empty;
    foreach (ReferenceColumnMapItem referenceColumn in this.referenceColumns)
    {
      stringBuilder.AppendFormat("{0}{1}", (object) str, referenceColumn.GetDestinationValue(record));
      str = this.Seperator;
    }
    switch (this.MapType)
    {
      case CombinedMapType.KeyGeneration:
        columnValue = (object) new Guid(MD5Engine.GetMD5Hash(stringBuilder.ToString()));
        break;
      case CombinedMapType.Concatenation:
        columnValue = (object) stringBuilder.ToString();
        break;
    }
    return this.PrepareColumnValue(columnValue);
  }

  public object GetDestinationValue(IndexerRecord indexerRecord)
  {
    object columnValue = (object) null;
    StringBuilder stringBuilder = new StringBuilder();
    string str = string.Empty;
    foreach (ReferenceColumnMapItem referenceColumn in this.referenceColumns)
    {
      if (string.Compare(indexerRecord.ChildDescription.UniqueFieldIdentifier, ((IEnumerable<string>) referenceColumn.FullFromColumnIdentifier.Split('.')).FirstOrDefault<string>()) != 0)
        stringBuilder.AppendFormat("{0}{1}", (object) str, referenceColumn.GetDestinationValue(indexerRecord.Parent));
      else
        stringBuilder.AppendFormat("{0}{1}", (object) str, referenceColumn.GetDestinationValue(indexerRecord.Child));
      str = this.Seperator;
    }
    switch (this.MapType)
    {
      case CombinedMapType.KeyGeneration:
        columnValue = (object) new Guid(MD5Engine.GetMd5HashBinary(stringBuilder.ToString()));
        break;
      case CombinedMapType.Concatenation:
        columnValue = (object) stringBuilder.ToString();
        break;
    }
    return this.PrepareColumnValue(columnValue);
  }

  private object PrepareColumnValue(object columnValue)
  {
    if (columnValue == null && this.IsMandatory)
      throw ExceptionFactory.Instance.CreateException<ConstraintException>($"Column [{this.ToColumnDescription}] is mandatory but has no value.");
    if (columnValue != null && !string.IsNullOrEmpty(this.ToColumnDescription.Format))
      columnValue = (object) string.Format(this.ToColumnDescription.Format, columnValue);
    if (this.ToColumnDescription is TextColumnDescription && columnValue != null)
    {
      TextColumnDescription columnDescription = this.ToColumnDescription as TextColumnDescription;
      string str = Convert.ToString(columnValue);
      if (columnDescription.MinimumLength.HasValue)
      {
        int? minimumLength1 = columnDescription.MinimumLength;
        int? maximumLength = columnDescription.MaximumLength;
        if (minimumLength1.GetValueOrDefault() <= maximumLength.GetValueOrDefault() & minimumLength1.HasValue & maximumLength.HasValue)
        {
          int length = str.Length;
          int? minimumLength2 = columnDescription.MinimumLength;
          int valueOrDefault = minimumLength2.GetValueOrDefault();
          if (length < valueOrDefault & minimumLength2.HasValue)
            str = str.PadRight(Convert.ToInt32((object) columnDescription.MinimumLength));
        }
      }
      if (columnDescription.MaximumLength.HasValue)
      {
        int length = str.Length;
        int? maximumLength = columnDescription.MaximumLength;
        int valueOrDefault = maximumLength.GetValueOrDefault();
        if (length > valueOrDefault & maximumLength.HasValue)
          str = str.Substring(0, Convert.ToInt32((object) columnDescription.MaximumLength));
      }
      columnValue = (object) str;
    }
    return columnValue;
  }

  public object GetDestinationValue(DataExchangeTokenSet tokenSet)
  {
    object destinationValue = (object) null;
    StringBuilder stringBuilder = new StringBuilder();
    string str1 = string.Empty;
    foreach (ReferenceColumnMapItem referenceColumn in this.referenceColumns)
    {
      stringBuilder.AppendFormat("{0}{1}", (object) str1, referenceColumn.GetDestinationValue(tokenSet));
      str1 = this.Seperator;
    }
    switch (this.MapType)
    {
      case CombinedMapType.KeyGeneration:
        destinationValue = (object) new Guid(MD5Engine.GetMD5Hash(stringBuilder.ToString()));
        break;
      case CombinedMapType.Concatenation:
        destinationValue = (object) stringBuilder.ToString();
        break;
    }
    if (destinationValue == null && this.IsMandatory)
      throw ExceptionFactory.Instance.CreateException<ConstraintException>($"Column [{this.ToColumnDescription}] is mandatory but has no value.");
    if (destinationValue != null && !string.IsNullOrEmpty(this.ToColumnDescription.Format))
      destinationValue = (object) string.Format(this.ToColumnDescription.Format, destinationValue);
    if (this.ToColumnDescription is TextColumnDescription && destinationValue != null)
    {
      TextColumnDescription columnDescription = this.ToColumnDescription as TextColumnDescription;
      string str2 = Convert.ToString(destinationValue);
      int? minimumLength = columnDescription.MinimumLength;
      int? nullable;
      if (minimumLength.HasValue)
      {
        minimumLength = columnDescription.MinimumLength;
        nullable = columnDescription.MaximumLength;
        if (minimumLength.GetValueOrDefault() <= nullable.GetValueOrDefault() & minimumLength.HasValue & nullable.HasValue)
        {
          int length = str2.Length;
          nullable = columnDescription.MinimumLength;
          int valueOrDefault = nullable.GetValueOrDefault();
          if (length < valueOrDefault & nullable.HasValue)
            str2 = str2.PadRight(Convert.ToInt32((object) columnDescription.MinimumLength));
        }
      }
      nullable = columnDescription.MaximumLength;
      if (nullable.HasValue)
      {
        int length = str2.Length;
        nullable = columnDescription.MaximumLength;
        int valueOrDefault = nullable.GetValueOrDefault();
        if (length > valueOrDefault & nullable.HasValue)
          str2 = str2.Substring(0, Convert.ToInt32((object) columnDescription.MaximumLength));
      }
      destinationValue = (object) str2;
    }
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
