// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.EntityFiller
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.HistoryTracker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

internal class EntityFiller
{
  private readonly EntityLoadInformation entityLoadInformation;
  private readonly LoadEntityOption loadEntityOptionRoot;
  private readonly DbDataReader reader;
  private readonly Dictionary<string, int> readerColumnMap = new Dictionary<string, int>();
  private Dictionary<int, object> attachCache;
  private int currentColumnOrdinal;
  private string currentFillPropertyName;
  private object currentFillValue;
  private Type entityType;
  private Dictionary<int, object> loadCache;
  private Dictionary<int, object> presentCache;

  internal EntityFiller(LoadEntityOption loadEntityOptionRoot, DbDataReader reader)
  {
    if (reader == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (reader));
    this.entityType = loadEntityOptionRoot != null ? loadEntityOptionRoot.LoadType : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (loadEntityOptionRoot));
    this.reader = reader;
    this.loadEntityOptionRoot = loadEntityOptionRoot;
    this.entityLoadInformation = new EntityLoadInformation(loadEntityOptionRoot);
    this.InitReaderColumnMap(loadEntityOptionRoot);
  }

  internal ICollection CreateItemsFromReader()
  {
    this.ResetCache();
    List<object> parentItem = new List<object>();
    try
    {
      while (this.reader.Read())
        this.Visit((object) parentItem, 0, this.loadEntityOptionRoot);
    }
    catch (System.Exception ex)
    {
      throw this.CreateExecutionException(ex);
    }
    return (ICollection) parentItem;
  }

  internal ICollection<HistoryEntry> CreateHistoryItemsFromReader()
  {
    this.ResetCache();
    List<HistoryEntry> historyItemsFromReader = new List<HistoryEntry>();
    try
    {
      while (this.reader.Read())
      {
        HistoryEntry historyEntry = new HistoryEntry();
        historyItemsFromReader.Add(historyEntry);
        this.Visit((object) historyEntry, 0, this.loadEntityOptionRoot);
        this.FillHistoryEntry(historyEntry);
      }
    }
    catch (System.Exception ex)
    {
      throw this.CreateExecutionException(ex);
    }
    return (ICollection<HistoryEntry>) historyItemsFromReader;
  }

  internal void RefillOneItemFromReader(object itemToRefill)
  {
    this.ResetCache();
    try
    {
      this.RememberPresentItems(itemToRefill, this.loadEntityOptionRoot, (object) null, 0);
      EntityFiller.CleanCurrentRelations(itemToRefill, this.loadEntityOptionRoot);
      bool flag1 = true;
      bool flag2 = false;
      int uniqueParentEntityKey = 0;
      while (this.reader.Read())
      {
        if (flag1)
        {
          this.FillItemFromReader(itemToRefill, this.loadEntityOptionRoot);
          flag1 = false;
          flag2 = true;
          uniqueParentEntityKey = this.CreateUniqueEntityKey(EntityHelper.GetPrimaryKey(itemToRefill), this.loadEntityOptionRoot, (object) null, 0);
        }
        foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) this.loadEntityOptionRoot.Children)
          this.Visit(itemToRefill, uniqueParentEntityKey, child);
      }
      if (!flag2)
        throw ExceptionHelper.CreateException<RecordNotFoundException>("RecordDoesNotExist", (object) itemToRefill.GetType().Name, EntityHelper.GetPrimaryKey(itemToRefill));
      EntityHelper.SetLoadInformation(itemToRefill, this.entityLoadInformation);
    }
    catch (System.Exception ex)
    {
      throw this.CreateExecutionException(ex);
    }
  }

  private void Visit(
    object parentItem,
    int uniqueParentEntityKey,
    LoadEntityOption loadEntityOption)
  {
    this.currentColumnOrdinal = this.readerColumnMap[loadEntityOption.PrefixedPrimaryKeyName];
    if (this.currentColumnOrdinal == -1)
      return;
    object id = this.reader.GetValue(this.currentColumnOrdinal);
    if (id == null || id == DBNull.Value)
      return;
    int uniqueEntityKey = this.CreateUniqueEntityKey(id, loadEntityOption, parentItem, uniqueParentEntityKey);
    object obj = this.GetAlreadyAttachedItem(uniqueParentEntityKey, loadEntityOption, uniqueEntityKey);
    if (obj == null)
    {
      obj = this.GetAlreadyLoadedItem(uniqueEntityKey);
      if (obj == null)
      {
        obj = this.GetAlreadyPresentItem(uniqueEntityKey) ?? Activator.CreateInstance(loadEntityOption.LoadType);
        this.FillItemFromReader(obj, loadEntityOption);
        if (loadEntityOption.Parent == null)
          EntityHelper.SetLoadInformation(obj, this.entityLoadInformation);
        this.RememberLoadedItem(uniqueEntityKey, obj);
      }
      EntityFiller.AttachItem(parentItem, obj, loadEntityOption);
      this.RememberAttachedItem(uniqueParentEntityKey, loadEntityOption, uniqueEntityKey, obj);
    }
    foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) loadEntityOption.Children)
      this.Visit(obj, uniqueEntityKey, child);
  }

  private int CreateUniqueEntityKey(
    object id,
    LoadEntityOption loadEntityOption,
    object parentItem,
    int uniqueParentEntityKey)
  {
    int uniqueEntityKey1 = id.GetHashCode() + loadEntityOption.LoadType.GetHashCode();
    if (parentItem == null)
      return uniqueEntityKey1;
    int uniqueEntityKey2 = uniqueEntityKey1 + uniqueParentEntityKey;
    if (parentItem is HistoryEntry)
      uniqueEntityKey2 += (int) this.reader["HistoryVersion"];
    if (loadEntityOption.HasRelationValue)
      uniqueEntityKey2 += parentItem.GetHashCode();
    return uniqueEntityKey2;
  }

  private void FillHistoryEntry(HistoryEntry historyEntry)
  {
    historyEntry.HistoryId = (int) this.reader["HistoryVersion"];
    historyEntry.Timestamp = (DateTime) this.reader["HistoryTimestamp"];
    historyEntry.UserName = (string) this.reader["HistoryUserName"];
    historyEntry.UserId = (Guid?) this.reader["HistoryUserID"];
    object obj = this.reader["HistoryAction"];
    if (obj == null)
      return;
    historyEntry.HistoryAction = new HistoryActionType?((HistoryActionType) obj);
  }

  private void FillItemFromReader(object item, LoadEntityOption loadEntityOption)
  {
    foreach (KeyValuePair<string, PropertyHelper> fillColumnProperty in loadEntityOption.PrefixedFillColumnPropertyMap)
    {
      this.currentFillPropertyName = fillColumnProperty.Key;
      this.currentColumnOrdinal = this.readerColumnMap[this.currentFillPropertyName];
      if (this.currentColumnOrdinal >= 0)
      {
        this.currentFillValue = this.reader.GetValue(this.currentColumnOrdinal);
        fillColumnProperty.Value.SetValue(item, this.currentFillValue);
      }
    }
  }

  private static void CleanCurrentRelations(object entityToClean, LoadEntityOption loadEntityOption)
  {
    loadEntityOption.DoInRelationHierarchy(entityToClean, (Action<object, object, LoadEntityOption>) ((parent, o, lro) => lro.PropertyHelper.SetValue(parent, (object) null)), ProcessRelationHierarchy.BottomToTop);
  }

  private void InitReaderColumnMap(LoadEntityOption loadEntityOption)
  {
    Dictionary<string, int> dictionary = new Dictionary<string, int>();
    for (int ordinal = 0; ordinal < this.reader.FieldCount; ++ordinal)
      dictionary.Add(this.reader.GetName(ordinal), ordinal);
    foreach (KeyValuePair<string, PropertyHelper> fillColumnProperty in loadEntityOption.PrefixedFillColumnPropertyMap)
    {
      string key = fillColumnProperty.Key;
      int valueOrDefault = dictionary.GetValueOrDefault<string, int>(key, int.MinValue);
      this.readerColumnMap.Add(key, valueOrDefault);
      if (valueOrDefault >= 0)
      {
        Type fieldType = this.reader.GetFieldType(valueOrDefault);
        Type type = fillColumnProperty.Value.PropertyInfo.PropertyType;
        if (type.IsEnum)
          type = Enum.GetUnderlyingType(type);
        else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>) && type.GetGenericArguments()[0].IsEnum)
          type = Enum.GetUnderlyingType(type.GetGenericArguments()[0]);
        else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
          type = type.GetGenericArguments()[0];
        if (fieldType != type && !typeof (IConvertible).IsAssignableFrom(fieldType) && !typeof (IConvertible).IsAssignableFrom(type) && (!type.IsArray || !type.GetElementType().IsPrimitive || !fieldType.IsArray || !(fieldType.GetElementType() == typeof (byte))))
          throw ExceptionFactory.Instance.CreateException<ExecutionException>(string.Format(ComponentResourceManagement.Instance.ResourceManager.GetString("DbAndEntityTypesDoNotMatch") ?? string.Empty, (object) fillColumnProperty.Value.PropertyName, (object) loadEntityOption.LoadType, (object) fieldType, (object) type));
      }
    }
    foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) loadEntityOption.Children)
      this.InitReaderColumnMap(child);
  }

  private ExecutionException CreateExecutionException(System.Exception ex)
  {
    return ExceptionFactory.Instance.CreateException<ExecutionException>(this.currentFillPropertyName != null ? string.Format(ComponentResourceManagement.Instance.ResourceManager.GetString("ErrorWhenAssigningValueToField") ?? string.Empty, this.currentFillValue == null ? (object) "null" : (object) this.currentFillValue.ToString(), (object) this.currentFillPropertyName) : ComponentResourceManagement.Instance.ResourceManager.GetString("ErrorWhenLoadingRecords"), ex);
  }

  private void ResetCache()
  {
    this.attachCache = new Dictionary<int, object>();
    this.loadCache = new Dictionary<int, object>();
    this.presentCache = new Dictionary<int, object>();
  }

  private static void AttachItem(
    object parentItem,
    object itemToAttach,
    LoadEntityOption loadEntityOption)
  {
    if (parentItem is HistoryEntry)
      ((HistoryEntry) parentItem).Entity = itemToAttach;
    else if (loadEntityOption.IsAttachTypeList)
    {
      if (loadEntityOption.Parent == null)
        list = parentItem as IList;
      else if (!(loadEntityOption.PropertyHelper.GetValue(parentItem) is IList list))
      {
        if (loadEntityOption.AttachType.IsInterface)
          list = Activator.CreateInstance(typeof (List<>).MakeGenericType(loadEntityOption.LoadType)) as IList;
        else
          list = Activator.CreateInstance(loadEntityOption.AttachType) as IList;
        loadEntityOption.PropertyHelper.SetValue(parentItem, (object) list);
      }
      list?.Add(itemToAttach);
    }
    else
      loadEntityOption.PropertyHelper.SetValue(parentItem, itemToAttach);
  }

  private object GetAlreadyAttachedItem(
    int uniqueParentEntityKey,
    LoadEntityOption loadEntityOption,
    int uniqueEntityKey)
  {
    return this.attachCache.GetValueOrDefault<int, object>(uniqueParentEntityKey + loadEntityOption.Prefix.GetHashCode() + uniqueEntityKey);
  }

  private void RememberAttachedItem(
    int uniqueParentEntityKey,
    LoadEntityOption loadEntityOption,
    int uniqueEntityKey,
    object itemToRemember)
  {
    this.attachCache.Add(uniqueParentEntityKey + loadEntityOption.Prefix.GetHashCode() + uniqueEntityKey, itemToRemember);
  }

  private object GetAlreadyLoadedItem(int uniqueEntityKey)
  {
    return this.loadCache.GetValueOrDefault<int, object>(uniqueEntityKey);
  }

  private void RememberLoadedItem(int uniqueEntityKey, object itemToRemember)
  {
    this.loadCache.Add(uniqueEntityKey, itemToRemember);
  }

  private object GetAlreadyPresentItem(int uniqueEntityKey)
  {
    return this.presentCache.GetValueOrDefault<int, object>(uniqueEntityKey);
  }

  private void RememberPresentItems(
    object entity,
    LoadEntityOption loadEntityOption,
    object parentEntity,
    int uniqueParentEntityKey)
  {
    int uniqueEntityKey = this.CreateUniqueEntityKey(EntityHelper.GetPrimaryKey(entity), loadEntityOption, parentEntity, uniqueParentEntityKey);
    if (!this.presentCache.ContainsKey(uniqueEntityKey))
      this.presentCache.Add(uniqueEntityKey, entity);
    foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) loadEntityOption.Children)
    {
      foreach (object relatedEntity in (IEnumerable<object>) child.RelationHelper.GetRelatedEntities(entity))
        this.RememberPresentItems(relatedEntity, child, entity, uniqueEntityKey);
    }
  }
}
