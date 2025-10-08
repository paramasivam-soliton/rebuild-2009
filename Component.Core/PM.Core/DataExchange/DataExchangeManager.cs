// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.DataExchangeManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement;
using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Stream;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Xml.Linq;

#nullable disable
namespace PathMedical.DataExchange;

public class DataExchangeManager
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (DataExchangeManager), "$Rev: 1512 $");
  private readonly List<RecordDescriptionSet> recordDescriptionSets;
  private readonly List<DataExchangeSetMap> dataExchangeMaps;
  private bool areDataExchangeSetMapsInitialilzed;
  private readonly List<ISupportPluginDataExchange> dataExchangePlugins = new List<ISupportPluginDataExchange>();
  private readonly Dictionary<string, Type> dataExchangeSupportTypes = new Dictionary<string, Type>();

  public static DataExchangeManager Instance => PathMedical.Singleton.Singleton<DataExchangeManager>.Instance;

  private DataExchangeManager()
  {
    this.recordDescriptionSets = new List<RecordDescriptionSet>();
    this.dataExchangeMaps = new List<DataExchangeSetMap>();
  }

  public void AddRecordDescriptionSets(RecordDescriptionSet[] sets)
  {
    foreach (RecordDescriptionSet set in sets)
      this.AddRecordDescriptionSet(set);
  }

  public void AddRecordDescriptionSet(RecordDescriptionSet recordDescriptionSet)
  {
    if (recordDescriptionSet == null || this.recordDescriptionSets.Contains(recordDescriptionSet))
      return;
    RecordDescriptionSet descriptionSet = recordDescriptionSet;
    RecordDescriptionSet recordDescriptionSet1 = this.recordDescriptionSets.Where<RecordDescriptionSet>((Func<RecordDescriptionSet, bool>) (rs => rs.Identifier.Equals(descriptionSet.Identifier))).FirstOrDefault<RecordDescriptionSet>();
    if (recordDescriptionSet1 == null)
      this.recordDescriptionSets.Add(recordDescriptionSet);
    else
      recordDescriptionSet1.AddRecordDescription(recordDescriptionSet.RecordDescriptions.ToArray<RecordDescription>());
  }

  public void AddRecordDescriptionSet(string xmlFileName)
  {
    if (string.IsNullOrEmpty(xmlFileName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (xmlFileName));
    if (!File.Exists(xmlFileName))
      return;
    RecordDescriptionSet recordDescriptionSet = RecordDescriptionSet.LoadXmlFile(xmlFileName);
    if (!recordDescriptionSet.IsValid)
      return;
    this.recordDescriptionSets.Add(recordDescriptionSet);
  }

  public void AddRecordDescriptionSet(XElement xmlDataExchangeSetDescription)
  {
    this.AddRecordDescriptionSet(RecordDescriptionSet.LoadXmlFile(xmlDataExchangeSetDescription));
  }

  public RecordDescriptionSet GetRecordDescriptionSet(string dataExchangeSetIdentifier)
  {
    RecordDescriptionSet recordDescriptionSet = (RecordDescriptionSet) null;
    if (!string.IsNullOrEmpty(dataExchangeSetIdentifier))
      recordDescriptionSet = this.recordDescriptionSets.FirstOrDefault<RecordDescriptionSet>((Func<RecordDescriptionSet, bool>) (rs => rs.Identifier == dataExchangeSetIdentifier));
    return recordDescriptionSet;
  }

  public RecordDescription GetRecordDesccription(
    string recordDescriptionSetIdentifier,
    string recordDescriptionIdentifier)
  {
    if (string.IsNullOrEmpty(recordDescriptionIdentifier))
      return (RecordDescription) null;
    return string.IsNullOrEmpty(recordDescriptionIdentifier) ? (RecordDescription) null : this.recordDescriptionSets.Where<RecordDescriptionSet>((Func<RecordDescriptionSet, bool>) (descriptionSet => string.Compare(descriptionSet.Identifier, recordDescriptionSetIdentifier) == 0)).SelectMany<RecordDescriptionSet, RecordDescription>((Func<RecordDescriptionSet, IEnumerable<RecordDescription>>) (descriptionSet => descriptionSet.RecordDescriptions)).Where<RecordDescription>((Func<RecordDescription, bool>) (rd => string.Compare(rd.Identifier, recordDescriptionIdentifier) == 0)).FirstOrDefault<RecordDescription>();
  }

  private void InitializeDataExchangeSetMaps()
  {
    if (this.dataExchangeMaps == null || this.areDataExchangeSetMapsInitialilzed)
      return;
    foreach (DataExchangeSetMap dataExchangeMap in this.dataExchangeMaps)
      dataExchangeMap.Initialize();
    this.areDataExchangeSetMapsInitialilzed = true;
  }

  public void AddDataExchangeSetMap(params DataExchangeSetMap[] dataExchangeSetMapList)
  {
    foreach (DataExchangeSetMap dataExchangeSetMap in dataExchangeSetMapList)
    {
      if (dataExchangeSetMap != null && !this.dataExchangeMaps.Contains(dataExchangeSetMap))
        this.dataExchangeMaps.Add(dataExchangeSetMap);
    }
  }

  public void AddDataExchangeSetMap(string xmlFileName)
  {
    FileIOPermission fileIoPermission = !string.IsNullOrEmpty(xmlFileName) ? new FileIOPermission(FileIOPermissionAccess.AllAccess, xmlFileName) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (xmlFileName));
    try
    {
      fileIoPermission.Demand();
    }
    catch (SecurityException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeSetException>(string.Format(Resources.DataExchangeManager_FileAccessDenied, (object) xmlFileName), (System.Exception) ex);
    }
    if (!File.Exists(xmlFileName))
      return;
    foreach (XElement element in XElement.Load(xmlFileName).Elements((XName) "DataExchangeMaps"))
      this.AddDataExchangeSetMap(DataExchangeSetMap.LoadSetFromXml(element));
  }

  public void AddDataExchangeMaps(XElement xmlDataExchangeMaps)
  {
    this.AddDataExchangeSetMap(DataExchangeSetMap.LoadSetsFromXml(xmlDataExchangeMaps).ToArray());
  }

  public RecordMap GetRecordMap(
    string dataExchangeFromSetName,
    string dataExchangeToSetName,
    string fromRecordMapName,
    string toRecordMapName)
  {
    if (!this.areDataExchangeSetMapsInitialilzed)
      this.InitializeDataExchangeSetMaps();
    return this.dataExchangeMaps.Where<DataExchangeSetMap>((Func<DataExchangeSetMap, bool>) (set => string.Compare(set.FromRecordDescriptionSet.Identifier, dataExchangeFromSetName) == 0 && string.Compare(set.ToRecordDescriptionSet.Identifier, dataExchangeToSetName) == 0)).SelectMany<DataExchangeSetMap, RecordMap>((Func<DataExchangeSetMap, IEnumerable<RecordMap>>) (set => (IEnumerable<RecordMap>) set.RecordMaps)).Where<RecordMap>((Func<RecordMap, bool>) (map => string.Compare(map.FromRecordDescription.Identifier, fromRecordMapName) == 0 && string.Compare(map.ToRecordDescription.Identifier, toRecordMapName) == 0)).FirstOrDefault<RecordMap>();
  }

  public DataExchangeSetMap GetDataExchangeSetMap(
    string dataExchangeFromSetName,
    string dataExchangeToSetName)
  {
    return this.dataExchangeMaps.Where<DataExchangeSetMap>((Func<DataExchangeSetMap, bool>) (set => string.Compare(set.FromRecordDescriptionSet.Identifier, dataExchangeFromSetName) == 0 && string.Compare(set.ToRecordDescriptionSet.Identifier, dataExchangeToSetName) == 0)).FirstOrDefault<DataExchangeSetMap>();
  }

  public RecordMap[] GetRecordMaps(string dataExchangeFromSetName, string dataExchangeToSetName)
  {
    return this.dataExchangeMaps.Where<DataExchangeSetMap>((Func<DataExchangeSetMap, bool>) (set => string.Compare(set.FromRecordDescriptionSet.Identifier, dataExchangeFromSetName) == 0 && string.Compare(set.ToRecordDescriptionSet.Identifier, dataExchangeToSetName) == 0)).SelectMany<DataExchangeSetMap, RecordMap>((Func<DataExchangeSetMap, IEnumerable<RecordMap>>) (s => (IEnumerable<RecordMap>) s.RecordMaps)).ToArray<RecordMap>();
  }

  public IEnumerable<ISupportPluginDataExchange> DataExchangePlugins
  {
    get => (IEnumerable<ISupportPluginDataExchange>) this.dataExchangePlugins;
  }

  public void RegisterDataExchangePlugin(ISupportPluginDataExchange dataExchangePlugin)
  {
    if (dataExchangePlugin == null || this.dataExchangePlugins.Contains(dataExchangePlugin))
      return;
    this.dataExchangePlugins.Add(dataExchangePlugin);
    if (dataExchangePlugin.Types != null && dataExchangePlugin.Types.Count > 0)
    {
      foreach (Type type in dataExchangePlugin.Types)
      {
        DataExchangeRecordAttribute exchangeRecordAttribute = type.GetCustomAttributes(typeof (DataExchangeRecordAttribute), true).Cast<DataExchangeRecordAttribute>().FirstOrDefault<DataExchangeRecordAttribute>();
        string key = type.Name;
        if (exchangeRecordAttribute != null && exchangeRecordAttribute.Identifier is string)
          key = exchangeRecordAttribute.Identifier as string;
        if (!this.dataExchangeSupportTypes.ContainsKey(key))
        {
          this.dataExchangeSupportTypes.Add(key, type);
          DataExchangeManager.Logger.Debug($"Registered type / record [{key}] of module [{dataExchangePlugin}] for data exchange.");
        }
      }
    }
    if (dataExchangePlugin.RecordDescriptionSets != null)
      this.AddRecordDescriptionSets(dataExchangePlugin.RecordDescriptionSets.ToArray());
    if (dataExchangePlugin.RecordSetMaps == null)
      return;
    this.AddDataExchangeSetMap(dataExchangePlugin.RecordSetMaps.ToArray());
  }

  public void Export(
    IEnumerable<object> dataRecords,
    DataExchangeSetMap map,
    params IDataExchangeStreamWriter[] writers)
  {
    if (dataRecords == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("data");
    if (map == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (map));
    if (writers == null)
      return;
    foreach (object dataRecord in dataRecords)
    {
      foreach (RecordMap recordMap in map.RecordMaps)
      {
        if (recordMap.HasIndexer)
        {
          foreach (IndexerRecord indexerRecord in recordMap.GetIndexerRecords(dataRecord))
          {
            DataExchangeTokenSet exchangeTokenSet = recordMap.GetDataExchangeTokenSet(indexerRecord);
            foreach (IDataExchangeStreamWriter writer in writers)
            {
              if (writer.ContainsRecordDescription(recordMap.ToRecordDescription))
                writer.Write(recordMap.ToRecordDescription, exchangeTokenSet);
            }
          }
        }
        else
        {
          foreach (IDataExchangeStreamWriter writer in writers)
          {
            if (writer.ContainsRecordDescription(recordMap.ToRecordDescription))
            {
              DataExchangeTokenSet exchangeTokenSet = recordMap.GetDataExchangeTokenSet(dataRecord);
              writer.Write(recordMap.ToRecordDescription, exchangeTokenSet);
            }
          }
        }
      }
    }
  }

  public void Store()
  {
  }

  public T FetchEntity<T>(DataExchangeTokenSet tokenSet) where T : class
  {
    T obj = default (T);
    return tokenSet == null || tokenSet.Tokens.Count == 0 ? obj : tokenSet.RecordDescription.GetObject<T>(tokenSet);
  }

  public List<T> FetchEntities<T>(IEnumerable<DataExchangeTokenSet> tokenSets) where T : class
  {
    List<T> objList = new List<T>();
    foreach (DataExchangeTokenSet tokenSet in tokenSets)
    {
      T obj = DataExchangeManager.Instance.FetchEntity<T>(tokenSet);
      if ((object) obj != null)
        objList.Add(obj);
    }
    return objList;
  }

  public List<T> FetchEntities<T>(
    RecordMap recordMap,
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets)
    where T : class
  {
    if (recordMap == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordMap));
    try
    {
      List<T> objList = new List<T>();
      if (dataExchangeTokenSets != null && dataExchangeTokenSets.Count<DataExchangeTokenSet>() > 0)
      {
        List<DataExchangeTokenSet> exchangeTokenSetsOfType = DataExchangeManager.Instance.GetDataExchangeTokenSetsOfType<T>(recordMap, dataExchangeTokenSets);
        if (exchangeTokenSetsOfType.Count > 0)
          objList.AddRange(exchangeTokenSetsOfType.Select<DataExchangeTokenSet, T>((Func<DataExchangeTokenSet, T>) (tokenSet => recordMap.GetObject<T>(tokenSet))).Where<T>((Func<T, bool>) (newEntity => (object) newEntity != null)));
      }
      return objList;
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeException>(Resources.DataExchangeManager_FetchEntitiesFailure, ex);
    }
  }

  public List<object> FetchEntities(
    DataExchangeSetMap instrumentToSystemMaps,
    List<DataExchangeTokenSet> tokenSets)
  {
    List<object> objectList = new List<object>();
    foreach (RecordMap recordMap in instrumentToSystemMaps.RecordMaps)
    {
      foreach (DataExchangeTokenSet tokenSet in tokenSets)
      {
        if (recordMap.FromRecordDescription.Identifier.Equals(tokenSet.RecordDescription.Identifier))
        {
          Type type;
          this.dataExchangeSupportTypes.TryGetValue(recordMap.ToRecordDescription.Identifier, out type);
          if (type != (Type) null)
          {
            object obj = recordMap.GetObject(type, tokenSet);
            if (obj != null)
              objectList.Add(obj);
          }
        }
      }
    }
    return objectList;
  }

  public List<DataExchangeTokenSet> MoveDataExchangeTokenSets(
    RecordMap[] recordMaps,
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets)
  {
    if (recordMaps == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordMaps));
    List<DataExchangeTokenSet> exchangeTokenSetList = new List<DataExchangeTokenSet>();
    foreach (RecordMap recordMap in recordMaps)
      exchangeTokenSetList.AddRange((IEnumerable<DataExchangeTokenSet>) this.MoveDataExchangeTokenSets(recordMap, dataExchangeTokenSets));
    return exchangeTokenSetList;
  }

  public List<DataExchangeTokenSet> MoveDataExchangeTokenSets(
    RecordMap recordMap,
    IEnumerable<object> dataRecords)
  {
    if (recordMap == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordMap));
    List<DataExchangeTokenSet> exchangeTokenSetList = new List<DataExchangeTokenSet>();
    foreach (object dataRecord in dataRecords)
    {
      if (recordMap.HasIndexer)
      {
        foreach (IndexerRecord indexerRecord in recordMap.GetIndexerRecords(dataRecord))
        {
          DataExchangeTokenSet exchangeTokenSet = recordMap.GetDataExchangeTokenSet(indexerRecord);
          if (exchangeTokenSet != null)
            exchangeTokenSetList.Add(exchangeTokenSet);
        }
      }
      else
      {
        DataExchangeTokenSet exchangeTokenSet = recordMap.GetDataExchangeTokenSet(dataRecord);
        if (exchangeTokenSet != null)
          exchangeTokenSetList.Add(exchangeTokenSet);
      }
    }
    return exchangeTokenSetList;
  }

  public List<DataExchangeTokenSet> MoveDataExchangeTokenSets(
    DataExchangeSetMap setMap,
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets)
  {
    if (setMap == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (setMap));
    List<DataExchangeTokenSet> exchangeTokenSetList = new List<DataExchangeTokenSet>();
    foreach (RecordMap recordMap in setMap.RecordMaps)
      exchangeTokenSetList.AddRange((IEnumerable<DataExchangeTokenSet>) this.MoveDataExchangeTokenSets(recordMap, dataExchangeTokenSets));
    return exchangeTokenSetList;
  }

  public List<DataExchangeTokenSet> GetDataExchangeTokenSetsOfType<T>(
    RecordMap recordMap,
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets)
  {
    List<DataExchangeTokenSet> exchangeTokenSetsOfType = new List<DataExchangeTokenSet>();
    foreach (DataExchangeTokenSet exchangeTokenSet in dataExchangeTokenSets)
    {
      if (recordMap.FromRecordDescription.Identifier == exchangeTokenSet.RecordDescription.Identifier)
      {
        Type type;
        this.dataExchangeSupportTypes.TryGetValue(recordMap.ToRecordDescription.Identifier, out type);
        if (type != (Type) null && type.Equals(typeof (T)))
          exchangeTokenSetsOfType.Add(exchangeTokenSet);
      }
    }
    return exchangeTokenSetsOfType;
  }

  public List<DataExchangeTokenSet> MoveDataExchangeTokenSets(
    RecordMap recordMap,
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets)
  {
    List<DataExchangeTokenSet> exchangeTokenSetList = new List<DataExchangeTokenSet>();
    foreach (DataExchangeTokenSet exchangeTokenSet1 in dataExchangeTokenSets)
    {
      if (recordMap.FromRecordDescription.Identifier == exchangeTokenSet1.RecordDescription.Identifier)
      {
        DataExchangeTokenSet exchangeTokenSet2 = recordMap.MoveDataExchangeTokenSet(exchangeTokenSet1);
        if (exchangeTokenSet2 != null)
          exchangeTokenSetList.Add(exchangeTokenSet2);
      }
    }
    return exchangeTokenSetList;
  }

  public void Import(
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets)
  {
    List<ISupportPluginDataExchange> list = this.dataExchangePlugins.OrderByDescending<ISupportPluginDataExchange, int>((Func<ISupportPluginDataExchange, int>) (p => p.StorePriority)).ToList<ISupportPluginDataExchange>();
    using (DBScope dbScope = new DBScope())
    {
      try
      {
        foreach (ISupportPluginDataExchange pluginDataExchange in list)
          pluginDataExchange.Import(dataExchangeTokenSets);
        dbScope.Complete();
      }
      catch (System.Exception ex)
      {
        ExceptionFactory.Instance.CreateException<DataExchangeException>(Resources.DataExchangeManager_ImportFailure, ex);
      }
    }
  }
}
