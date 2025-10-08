// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Map.DataExchangeSetMap
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Set;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Xml.Linq;

#nullable disable
namespace PathMedical.DataExchange.Map;

[DebuggerDisplay("DataExchangeSetMap [{Identifier}] from [{fromRecordDescriptionSetIdentifier}] to [{toRecordDescriptionSetIdentifier}]")]
public class DataExchangeSetMap
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (DataExchangeSetMap), "$Rev: 1278 $");
  private string fromRecordDescriptionSetIdentifier;
  private string toRecordDescriptionSetIdentifier;
  private XElement xmlRecordMapsDefinition;
  private readonly XmlHelper xmlHelper;

  public object Identifier { get; protected set; }

  public string Name { get; protected set; }

  public DataExchangeType DataExchangeType { get; protected set; }

  public RecordDescriptionSet FromRecordDescriptionSet { get; protected set; }

  public RecordDescriptionSet ToRecordDescriptionSet { get; protected set; }

  public List<RecordMap> RecordMaps { get; set; }

  public DataExchangeSetMap()
  {
    this.xmlHelper = new XmlHelper();
    this.RecordMaps = new List<RecordMap>();
  }

  public static DataExchangeSetMap LoadSetsFromXml(string xmlFileName)
  {
    if (string.IsNullOrEmpty(xmlFileName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (xmlFileName));
    XElement xmlMapElement = (XElement) null;
    FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, xmlFileName);
    try
    {
      fileIoPermission.Demand();
    }
    catch (SecurityException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeSetException>(string.Format(Resources.DataExchangeSetMap_FileAccessDenied, (object) xmlFileName), (System.Exception) ex);
    }
    if (File.Exists(xmlFileName))
      xmlMapElement = XElement.Load(xmlFileName);
    return DataExchangeSetMap.LoadSetFromXml(xmlMapElement);
  }

  public static List<DataExchangeSetMap> LoadSetsFromXml(XElement xmlDataExchangeSetElement)
  {
    return xmlDataExchangeSetElement.Elements((XName) "DataExchangeMap").Select<XElement, DataExchangeSetMap>((Func<XElement, DataExchangeSetMap>) (xmlDataExchangeMap => DataExchangeSetMap.LoadSetFromXml(xmlDataExchangeMap))).Where<DataExchangeSetMap>((Func<DataExchangeSetMap, bool>) (map => map != null)).ToList<DataExchangeSetMap>();
  }

  public static DataExchangeSetMap LoadSetFromXml(XElement xmlMapElement)
  {
    DataExchangeSetMap dataExchangeSetMap = xmlMapElement != null ? new DataExchangeSetMap()
    {
      Identifier = (object) xmlMapElement.SafeAttribute("Identifier").Value,
      Name = xmlMapElement.SafeAttribute("Name").Value,
      xmlRecordMapsDefinition = xmlMapElement.Element((XName) "RecordMaps"),
      fromRecordDescriptionSetIdentifier = xmlMapElement.SafeAttribute("FromDataSetIdentifier").Value,
      toRecordDescriptionSetIdentifier = xmlMapElement.SafeAttribute("ToDataSetIdentifier").Value
    } : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("xmlDocument");
    dataExchangeSetMap.DataExchangeType = dataExchangeSetMap.xmlHelper.LoadEnum<DataExchangeType>(xmlMapElement.SafeAttribute("Type"));
    return dataExchangeSetMap;
  }

  private void LoadRecordMaps(XContainer xmlRecordMaps)
  {
    if (xmlRecordMaps == null)
      return;
    foreach (XElement element1 in xmlRecordMaps.Elements((XName) "RecordMap"))
    {
      RecordDescription fromRecordDescription = (RecordDescription) null;
      if (element1.Attribute((XName) "FromRecordDescriptionIdentifier") != null)
        fromRecordDescription = this.FromRecordDescriptionSet[this.xmlHelper.LoadString(element1.Attribute((XName) "FromRecordDescriptionIdentifier"))];
      RecordDescription toRecordDescription = (RecordDescription) null;
      if (element1.Attribute((XName) "ToRecordDescriptionIdentifier") != null)
        toRecordDescription = this.ToRecordDescriptionSet[this.xmlHelper.LoadString(element1.Attribute((XName) "ToRecordDescriptionIdentifier"))];
      string indexerColumnName = string.Empty;
      if (element1.Attribute((XName) "IndexerColumn") != null)
        indexerColumnName = this.xmlHelper.LoadString(element1.Attribute((XName) "IndexerColumn"));
      RecordMap recordMap = new RecordMap(this, fromRecordDescription, toRecordDescription, indexerColumnName);
      foreach (XElement element2 in element1.Elements())
      {
        if (element2.Name.LocalName.Equals("LiteralMapItem", StringComparison.InvariantCultureIgnoreCase))
        {
          LiteralMapItem literalMapItem = new LiteralMapItem((object) this.xmlHelper.LoadString(element2.Attribute((XName) "FromValue")), this.xmlHelper.LoadString(element2.Attribute((XName) "ToColumn")));
          recordMap.AddColumnMapItem((IColumnMapItem) literalMapItem);
        }
        if (element2.Name.LocalName.Equals("CombinedMapItem", StringComparison.InvariantCultureIgnoreCase))
        {
          string mapType = this.xmlHelper.LoadString(element2.SafeAttribute("Type"));
          string toColumnName = this.xmlHelper.LoadString(element2.SafeAttribute("ToColumn"));
          CombinedMapItem combinedMapItem = new CombinedMapItem(recordMap, mapType, toColumnName);
          foreach (XElement element3 in element2.Elements())
          {
            string fromColumnName = this.xmlHelper.LoadString(element3.SafeAttribute("FromColumn"));
            ReferenceColumnMapItem referenceColumnMapItem = new ReferenceColumnMapItem(recordMap, fromColumnName);
            combinedMapItem.AddReferenceColumnMapItem(referenceColumnMapItem);
          }
          recordMap.AddColumnMapItem((IColumnMapItem) combinedMapItem);
        }
        if (element2.Name.LocalName.Equals("ColumnMapItem", StringComparison.InvariantCultureIgnoreCase))
        {
          string fromColumnName = this.xmlHelper.LoadString(element2.SafeAttribute("FromColumn"));
          string toColumnName = this.xmlHelper.LoadString(element2.SafeAttribute("ToColumn"));
          string fromColumnIndex = this.xmlHelper.LoadString(element2.SafeAttribute("FromColumnIndex"));
          try
          {
            ColumnMapItem columnMapItem = !string.IsNullOrEmpty(fromColumnIndex) ? new ColumnMapItem(recordMap, fromColumnName, toColumnName, fromColumnIndex) : new ColumnMapItem(recordMap, fromColumnName, toColumnName);
            foreach (XElement element4 in element2.Elements((XName) "ValueMapping"))
            {
              if (element4 != null)
              {
                object obj1 = (object) element4.SafeElement("Source").Value;
                object obj2 = (object) element4.SafeElement("Destination").Value;
                columnMapItem.AddMapping(DataExchangeSetMap.ChangeType(columnMapItem.FromColumnDescription.DataTypes, obj1), DataExchangeSetMap.ChangeType(columnMapItem.ToColumnDescription.DataTypes, obj2));
              }
            }
            recordMap.AddColumnMapItem((IColumnMapItem) columnMapItem);
          }
          catch (InvalidOperationException ex)
          {
            DataExchangeSetMap.Logger.Error((System.Exception) ex, "Failure while creating column map from {0} to {1}", (object) fromColumnName, (object) toColumnName);
          }
        }
      }
      this.RecordMaps.Add(recordMap);
    }
  }

  private static object ChangeType(DataTypes datatype, object value)
  {
    object obj = (object) null;
    switch (datatype)
    {
      case DataTypes.Guid:
        obj = Convert.ChangeType(value, TypeCode.Object);
        break;
      case DataTypes.Boolean:
        obj = Convert.ChangeType(value, TypeCode.Boolean);
        break;
      case DataTypes.String:
        obj = Convert.ChangeType(value, TypeCode.String);
        break;
      case DataTypes.Int8:
        obj = Convert.ChangeType(value, TypeCode.SByte);
        break;
      case DataTypes.Int8Array:
        obj = Convert.ChangeType(value, TypeCode.Object);
        break;
      case DataTypes.UInt8:
        obj = Convert.ChangeType(value, TypeCode.Byte);
        break;
      case DataTypes.UInt8Array:
        obj = Convert.ChangeType(value, TypeCode.Object);
        break;
      case DataTypes.Int16:
        obj = Convert.ChangeType(value, TypeCode.Int16);
        break;
      case DataTypes.UInt16:
        obj = Convert.ChangeType(value, TypeCode.UInt16);
        break;
      case DataTypes.Int32:
        obj = Convert.ChangeType(value, TypeCode.Int32);
        break;
      case DataTypes.UInt32:
        obj = Convert.ChangeType(value, TypeCode.UInt32);
        break;
      case DataTypes.Float:
        obj = Convert.ChangeType(value, TypeCode.Single);
        break;
      case DataTypes.Fract16:
        obj = Convert.ChangeType(value, TypeCode.Single);
        break;
      case DataTypes.DateTime:
        obj = Convert.ChangeType(value, TypeCode.Object);
        break;
    }
    return obj;
  }

  public void Initialize()
  {
    if (this.FromRecordDescriptionSet == null && !string.IsNullOrEmpty(this.fromRecordDescriptionSetIdentifier))
    {
      this.FromRecordDescriptionSet = DataExchangeManager.Instance.GetRecordDescriptionSet(this.fromRecordDescriptionSetIdentifier);
      if (this.FromRecordDescriptionSet == null)
        throw ExceptionFactory.Instance.CreateException<InvalidOperationException>($"The map {this.Identifier} contains record description {this.fromRecordDescriptionSetIdentifier} which has not been registered so far.");
    }
    if (this.ToRecordDescriptionSet == null && !string.IsNullOrEmpty(this.toRecordDescriptionSetIdentifier))
    {
      this.ToRecordDescriptionSet = DataExchangeManager.Instance.GetRecordDescriptionSet(this.toRecordDescriptionSetIdentifier);
      if (this.ToRecordDescriptionSet == null)
        throw ExceptionFactory.Instance.CreateException<InvalidOperationException>($"The map {this.Identifier} contains record description {this.toRecordDescriptionSetIdentifier} which has not been registered so far.");
    }
    if (this.RecordMaps.Count != 0)
      return;
    this.LoadRecordMaps((XContainer) this.xmlRecordMapsDefinition);
  }

  public RecordMap this[string destinationRecordIdentifier]
  {
    get
    {
      this.Initialize();
      try
      {
        return this.RecordMaps.FirstOrDefault<RecordMap>((Func<RecordMap, bool>) (map => string.Compare(map.ToRecordDescription.Identifier, destinationRecordIdentifier) == 0));
      }
      catch
      {
        return (RecordMap) null;
      }
    }
  }
}
