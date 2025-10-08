// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Map.ReferenceColumnMapItem
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.Property;
using System;

#nullable disable
namespace PathMedical.DataExchange.Map;

public class ReferenceColumnMapItem
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (ReferenceColumnMapItem), "$Rev: 1033 $");

  public RecordMap RecordMap { get; protected set; }

  public string FullFromColumnIdentifier { get; private set; }

  public ColumnDescriptionBase FromColumnDescription { get; protected set; }

  public string FromColumnIndex { get; protected set; }

  public ReferenceColumnMapItem(RecordMap recordMap, string fromColumnName)
  {
    if (recordMap == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>($"recordMap of source column [{fromColumnName}] is undefined");
    if (string.IsNullOrEmpty(fromColumnName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>($"fromColumnName for mapping map [{recordMap}] is undefined");
    this.RecordMap = recordMap;
    if (this.RecordMap == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>($"Can't add a reference from column {fromColumnName} because the record map is null.");
    if (this.RecordMap.FromRecordDescription == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a reference mapping from column {0} because record description (for {0}) of the record map is undefined.", (object) fromColumnName));
    this.FullFromColumnIdentifier = fromColumnName;
    ISupportDataExchangeColumnDescription descriptionByIdentifier = this.RecordMap.FromRecordDescription.GetColumnDescriptionByIdentifier(fromColumnName);
    if (descriptionByIdentifier == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a reference mapping for column {0} because there is no column description for {0} in record description {1}", (object) fromColumnName, (object) this.RecordMap.FromRecordDescription.Identifier));
    this.FromColumnDescription = descriptionByIdentifier is ColumnDescriptionBase ? descriptionByIdentifier as ColumnDescriptionBase : throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping for column {0} because the column description {0} for record description {1} is a complex column description.", (object) fromColumnName, (object) this.RecordMap.FromRecordDescription.Identifier));
    if (this.FromColumnDescription == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format("Can't add a mapping for column {0} because there is no column description for {0} in record description {1}", (object) fromColumnName, (object) this.RecordMap.FromRecordDescription.Identifier));
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
    return string.IsNullOrEmpty(this.FromColumnIndex) ? PropertyHelper<DataExchangeColumnAttribute>.GetPropertyValue(propertyName, record) : PropertyHelper<DataExchangeColumnAttribute>.GetPropertyValue(propertyName, this.FromColumnIndex, record);
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
    ReferenceColumnMapItem.Logger.Debug("Mapped column [{0}/{1}] to value [{2}]", (object) this.FullFromColumnIdentifier, obj, destinationValue);
    return destinationValue;
  }
}
