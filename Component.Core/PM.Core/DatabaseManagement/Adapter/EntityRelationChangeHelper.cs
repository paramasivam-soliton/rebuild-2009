// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.EntityRelationChangeHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class EntityRelationChangeHelper
{
  private readonly LoadEntityOption rootLoadEntityOption;
  private readonly DBScope scope;
  private EntityStoreInfo resultEntityStoreInfo;

  private EntityRelationChangeHelper(
    object entity,
    LoadEntityOption loadEntityOption,
    DBScope scope)
  {
    this.rootLoadEntityOption = loadEntityOption;
    this.scope = scope;
  }

  public static EntityStoreInfo GetStoreInfo(
    object entity,
    LoadEntityOption loadEntityOption,
    DBScope scope)
  {
    EntityRelationChangeHelper relationChangeHelper = new EntityRelationChangeHelper(entity, loadEntityOption, scope);
    relationChangeHelper.DoWork(entity);
    return relationChangeHelper.resultEntityStoreInfo;
  }

  private void DoWork(object entity)
  {
    this.AssertRelationSanity(entity, this.rootLoadEntityOption);
    object primaryKey = EntityHelper.GetPrimaryKey(entity);
    object obj = this.ReloadEntities(LoadEntityOption.CreateRootLoadOption(entity.GetType()), primaryKey).FirstOrDefault<object>();
    this.resultEntityStoreInfo = new EntityStoreInfo(entity, obj);
    if (!this.resultEntityStoreInfo.IsNew)
      this.AssertNoConcurrentEntityModification(entity, obj);
    this.VisitRelations(this.rootLoadEntityOption);
  }

  private void VisitRelations(LoadEntityOption loadEntityOption)
  {
    EntityStoreInfo[] entityStoreInfoArray = new EntityStoreInfo[1]
    {
      this.resultEntityStoreInfo
    };
    foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) loadEntityOption.Children)
      this.VisitRelation((IEnumerable<EntityStoreInfo>) entityStoreInfoArray, child);
  }

  private void VisitRelation(
    IEnumerable<EntityStoreInfo> entityStoreInfos,
    LoadEntityOption loadEntityOption)
  {
    LoadEntityOption rootLoadOption = LoadEntityOption.CreateRootLoadOption(loadEntityOption.Parent.LoadType);
    rootLoadOption.AddNestedLoadEntityOption(loadEntityOption.PropertyInfo.Name);
    object[] array1 = entityStoreInfos.Select<EntityStoreInfo, object>((Func<EntityStoreInfo, object>) (esi => esi.Id)).ToArray<object>();
    Dictionary<object, object> dictionary1 = this.ReloadEntities(rootLoadOption, array1).ToDictionary<object, object>((Func<object, object>) (re => EntityHelper.GetPrimaryKey(re)));
    object[] array2 = entityStoreInfos.Where<EntityStoreInfo>((Func<EntityStoreInfo, bool>) (esi => esi.Entity != null)).SelectMany<EntityStoreInfo, object>((Func<EntityStoreInfo, IEnumerable<object>>) (esi => (IEnumerable<object>) loadEntityOption.RelationHelper.GetRelatedEntities(esi.Entity))).Select<object, object>((Func<object, object>) (reNow => EntityHelper.GetPrimaryKey(reNow))).ToList<object>().Except<object>((IEnumerable<object>) dictionary1.Values.SelectMany<object, object>((Func<object, IEnumerable<object>>) (entity => (IEnumerable<object>) loadEntityOption.RelationHelper.GetRelatedEntities(entity))).Select<object, object>((Func<object, object>) (reDb => EntityHelper.GetPrimaryKey(reDb))).ToList<object>()).ToArray<object>();
    Dictionary<object, object> dictionary2 = this.ReloadEntities(LoadEntityOption.CreateRootLoadOption(loadEntityOption.LoadType), array2).ToDictionary<object, object>((Func<object, object>) (nre => EntityHelper.GetPrimaryKey(nre)));
    List<EntityStoreInfo> entityStoreInfoList = new List<EntityStoreInfo>();
    foreach (EntityStoreInfo entityStoreInfo in entityStoreInfos.Where<EntityStoreInfo>((Func<EntityStoreInfo, bool>) (esi => esi.Entity != null)))
    {
      object valueOrDefault = dictionary1.GetValueOrDefault<object, object>(entityStoreInfo.Id);
      foreach (EnumerableMatchComparison<object, object> enumerableMatchComparison in loadEntityOption.RelationHelper.GetRelatedEntities(entityStoreInfo.Entity).GetMatchComparison<object, object>(valueOrDefault != null ? (IEnumerable<object>) loadEntityOption.RelationHelper.GetRelatedEntities(valueOrDefault) : (IEnumerable<object>) new object[0], (Func<object, object>) (re => EntityHelper.GetPrimaryKey(re))).ToList<EnumerableMatchComparison<object, object>>())
      {
        object inFirstEnumerable = enumerableMatchComparison.ItemInFirstEnumerable;
        object secondEnumerable = enumerableMatchComparison.ItemInSecondEnumerable;
        EntityStoreInfo relatedEntityStoreInfo = new RelationStoreInfo(loadEntityOption, entityStoreInfo, inFirstEnumerable, secondEnumerable).RelatedEntityStoreInfo;
        entityStoreInfoList.Add(relatedEntityStoreInfo);
        if (relatedEntityStoreInfo != null && !relatedEntityStoreInfo.IsNew)
        {
          object reloadedEntity = secondEnumerable ?? dictionary2.GetValueOrDefault<object, object>(relatedEntityStoreInfo.Id);
          this.AssertNoConcurrentEntityModification(relatedEntityStoreInfo.Entity, reloadedEntity);
        }
      }
    }
    foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) loadEntityOption.Children)
      this.VisitRelation((IEnumerable<EntityStoreInfo>) entityStoreInfoList, child);
  }

  private ICollection<object> ReloadEntities(
    LoadEntityOption loadEntityOption,
    params object[] entityIds)
  {
    if (entityIds.Length == 0)
      return (ICollection<object>) new object[0];
    Dictionary<string, object> parameters;
    string completeSelectClause = QueryBuilder.GetCompleteSelectClause(loadEntityOption, (IEnumerable<object>) entityIds, out parameters);
    ICollection itemsFromReader;
    using (DbCommand dbCommand = this.scope.CreateDbCommand())
    {
      dbCommand.CommandText = completeSelectClause;
      this.scope.AddDbParameters(dbCommand, parameters);
      using (DbDataReader reader = dbCommand.ExecuteReader())
        itemsFromReader = new EntityFiller(loadEntityOption, reader).CreateItemsFromReader();
    }
    return (ICollection<object>) itemsFromReader.Cast<object>().ToList<object>();
  }

  private void AssertNoConcurrentEntityModification(object entity, object reloadedEntity)
  {
    if (entity == null)
      return;
    object primaryKey = EntityHelper.GetPrimaryKey(entity);
    if (reloadedEntity == null)
      throw ExceptionHelper.CreateException<RecordDeletedException>("RecordDeletedMeantime", (object) entity.GetType(), primaryKey);
    EntityHelper entityHelper = EntityHelper.For(entity.GetType());
    string key = entityHelper.UpdateTimestampColumnName ?? entityHelper.InsertUpdateTimestampColumnName;
    if (key == null)
      return;
    PropertyHelper columnProperty = entityHelper.ColumnPropertyMap[key];
    if ((DateTime) columnProperty.GetValue(reloadedEntity) > (DateTime) columnProperty.GetValue(entity))
      throw ExceptionHelper.CreateException<RecordModifiedException>("RecordModifiedMeantime", (object) entity.GetType(), primaryKey);
  }

  private void AssertRelationSanity(object entity, LoadEntityOption loadEntityOption)
  {
    foreach (PropertyHelper relationProperty in (IEnumerable<PropertyHelper>) EntityHelper.For(entity.GetType()).RelationProperties)
    {
      PropertyHelper ph = relationProperty;
      ICollection<object> relatedEntities = RelationHelper.For(ph.PropertyInfo).GetRelatedEntities(entity);
      if (relatedEntities.Count != 0)
      {
        LoadEntityOption loadEntityOption1 = loadEntityOption.Children.FirstOrDefault<LoadEntityOption>((Func<LoadEntityOption, bool>) (c => c.PropertyHelper.PropertyName == ph.PropertyName));
        if (loadEntityOption1 == null)
          throw ExceptionHelper.CreateException<StoreUninitializedRelationException>("CantStoreUnloadedRelations", (object) ph.PropertyName, (object) entity.GetType(), EntityHelper.GetPrimaryKey(entity));
        foreach (object entity1 in (IEnumerable<object>) relatedEntities)
          this.AssertRelationSanity(entity1, loadEntityOption1);
      }
    }
  }
}
