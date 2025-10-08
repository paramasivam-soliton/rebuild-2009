// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.RelationHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Attributes;
using PathMedical.Extensions;
using PathMedical.HistoryTracker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class RelationHelper
{
  private static readonly Dictionary<PropertyInfo, RelationHelper> relationHelpers = new Dictionary<PropertyInfo, RelationHelper>();
  private readonly bool hasHistory;
  private readonly PropertyInfo propertyInfo;
  private readonly RelationJoinInfo relationJoinInfo;

  private RelationHelper(PropertyInfo propertyInfo)
  {
    this.propertyInfo = propertyInfo;
    this.relationJoinInfo = new RelationJoinInfo(propertyInfo);
    this.hasHistory = (((IEnumerable<object>) propertyInfo.GetCustomAttributes(typeof (DbRelationAttribute), true)).Single<object>() as DbRelationAttribute).HasHistory;
  }

  public static RelationHelper For(PropertyInfo propertyInfo)
  {
    RelationHelper relationHelper;
    if (!RelationHelper.relationHelpers.TryGetValue(propertyInfo, out relationHelper))
    {
      relationHelper = new RelationHelper(propertyInfo);
      RelationHelper.relationHelpers.Add(propertyInfo, relationHelper);
    }
    return relationHelper;
  }

  public ICollection<object> GetRelatedEntities(object entity)
  {
    object source = PropertyHelper.For(this.propertyInfo).GetValue(entity);
    if (source == null)
      return (ICollection<object>) new object[0];
    if (source is IEnumerable)
      return (ICollection<object>) (source as IEnumerable).Cast<object>().ToList<object>();
    return (ICollection<object>) new object[1]{ source };
  }

  public object GetRelationValue(object entity)
  {
    string relationValueColumn = this.relationJoinInfo.RelationValueColumn;
    if (relationValueColumn == null)
      return (object) null;
    return EntityHelper.For(entity.GetType()).RelationValuePropertyMap.GetValueOrDefault<string, PropertyHelper>(relationValueColumn)?.GetValue(entity);
  }

  private DbCommand CreateRelationCommand(DBScope scope, RelationStoreInfo relationStoreInfo)
  {
    DbCommand dbCommand = scope.CreateDbCommand();
    if (relationStoreInfo.RelationChangeType == RelationChangeType.Removed)
    {
      if (this.relationJoinInfo.JoinType == JoinType.IntermediateTable)
      {
        Dictionary<string, object> storeColumnValues = this.CreateRelationStoreColumnValues(scope, relationStoreInfo);
        if (this.relationJoinInfo.RelationValueColumn != null)
          storeColumnValues.AddOrOverwriteEntry<string, object>(this.relationJoinInfo.RelationValueColumn, relationStoreInfo.RelationValueFromDb);
        string historyCommandText = this.CreateHistoryCommandText(scope, dbCommand, storeColumnValues, relationStoreInfo);
        dbCommand.CommandText = $"DELETE FROM [{this.relationJoinInfo.JoinTableName}] WHERE {this.CreateWhereCondition(scope, dbCommand, relationStoreInfo)};{historyCommandText}";
      }
      else
      {
        string str1;
        string str2;
        string primaryKeyName;
        object parameterValue1;
        object parameterValue2;
        if (this.relationJoinInfo.JoinType == JoinType.ReferenceToOtherEntity)
        {
          EntityHelper entityHelper = relationStoreInfo.LoadEntityOption.Parent.EntityHelper;
          str1 = entityHelper.TableName;
          str2 = this.relationJoinInfo.TableKey;
          primaryKeyName = entityHelper.PrimaryKeyName;
          parameterValue1 = relationStoreInfo.EntityStoreInfo.Id;
          parameterValue2 = relationStoreInfo.RelatedEntityId;
        }
        else
        {
          EntityHelper entityHelper = relationStoreInfo.LoadEntityOption.EntityHelper;
          str1 = this.relationJoinInfo.JoinTableName;
          str2 = this.relationJoinInfo.JoinTableKey;
          primaryKeyName = entityHelper.PrimaryKeyName;
          parameterValue1 = relationStoreInfo.RelatedEntityId;
          parameterValue2 = relationStoreInfo.EntityStoreInfo.Id;
        }
        dbCommand.CommandText = $"UPDATE [{str1}] SET [{str2}]=NULL WHERE [{primaryKeyName}]=@WhereId AND [{str2}]=@OriginalReferenceID;";
        scope.AddDbParameter(dbCommand, "WhereId", parameterValue1);
        scope.AddDbParameter(dbCommand, "OriginalReferenceID", parameterValue2);
      }
      return dbCommand;
    }
    if (relationStoreInfo.RelationChangeType == RelationChangeType.Added)
    {
      if (this.relationJoinInfo.JoinType == JoinType.IntermediateTable)
      {
        Dictionary<string, object> storeColumnValues = this.CreateRelationStoreColumnValues(scope, relationStoreInfo);
        string str3 = $"[{string.Join("], [", storeColumnValues.Keys.ToArray<string>())}]";
        string str4 = "@" + string.Join(", @", storeColumnValues.Keys.ToArray<string>());
        string historyCommandText = this.CreateHistoryCommandText(scope, dbCommand, storeColumnValues, relationStoreInfo);
        dbCommand.CommandText = $"INSERT INTO [{this.relationJoinInfo.JoinTableName}] ({str3}) VALUES ({str4});{historyCommandText}";
        scope.AddDbParameters(dbCommand, storeColumnValues);
      }
      else
      {
        if (this.relationJoinInfo.JoinType != JoinType.ReferenceToOtherEntity)
          return (DbCommand) null;
        EntityHelper entityHelper = EntityHelper.For(relationStoreInfo.EntityStoreInfo.Entity.GetType());
        string tableName = entityHelper.TableName;
        string tableKey = this.relationJoinInfo.TableKey;
        object relatedEntityId = relationStoreInfo.RelatedEntityId;
        string primaryKeyName = entityHelper.PrimaryKeyName;
        object id = relationStoreInfo.EntityStoreInfo.Id;
        dbCommand.CommandText = $"UPDATE [{tableName}] SET [{tableKey}]=@NewReferenceID WHERE [{primaryKeyName}]=@WhereId;";
        scope.AddDbParameter(dbCommand, "NewReferenceID", relatedEntityId);
        scope.AddDbParameter(dbCommand, "WhereId", id);
      }
      return dbCommand;
    }
    if (this.relationJoinInfo.JoinType != JoinType.IntermediateTable)
      return (DbCommand) null;
    if (relationStoreInfo.RelationChangeType != RelationChangeType.ValueModified)
      return (DbCommand) null;
    Dictionary<string, object> storeColumnValues1 = this.CreateRelationStoreColumnValues(scope, relationStoreInfo);
    StringBuilder stringBuilder = new StringBuilder();
    foreach (KeyValuePair<string, object> keyValuePair in storeColumnValues1)
    {
      stringBuilder.Append("[").Append(keyValuePair.Key).Append("]=@").Append(keyValuePair.Key).Append(", ");
      scope.AddDbParameter(dbCommand, keyValuePair.Key, keyValuePair.Value);
    }
    stringBuilder.Length -= 2;
    string historyCommandText1 = this.CreateHistoryCommandText(scope, dbCommand, storeColumnValues1, relationStoreInfo);
    dbCommand.CommandText = $"UPDATE [{this.relationJoinInfo.JoinTableName}] SET {stringBuilder} WHERE {this.CreateWhereCondition(scope, dbCommand, relationStoreInfo)};{historyCommandText1}";
    return dbCommand;
  }

  private string CreateHistoryCommandText(
    DBScope scope,
    DbCommand command,
    Dictionary<string, object> paramDict,
    RelationStoreInfo relationStoreInfo)
  {
    if (!this.hasHistory || this.relationJoinInfo.JoinType != JoinType.IntermediateTable)
      return string.Empty;
    HistoryActionType? historyAction = new HistoryActionType?();
    switch (relationStoreInfo.RelationChangeType)
    {
      case RelationChangeType.Removed:
        historyAction = new HistoryActionType?(HistoryActionType.DeleteRelation);
        break;
      case RelationChangeType.Added:
        historyAction = new HistoryActionType?(HistoryActionType.InsertRelation);
        break;
      case RelationChangeType.ValueModified:
        historyAction = new HistoryActionType?(HistoryActionType.UpdateRelation);
        break;
    }
    Dictionary<string, object> historyParameters = SqlStatementHelper.CreateHistoryParameters(scope, historyAction);
    string newValue1 = $"[{string.Join("], [", paramDict.Keys.ToArray<string>())}]";
    string newValue2 = "@" + string.Join(", @", paramDict.Keys.ToArray<string>());
    string historyCommandText = "INSERT INTO [##TABLE##History] (HistoryTimestamp, HistoryUserName, HistoryUserID, HistoryAction, ##COLUMNS##) VALUES (@pHistoryTimeStamp, @pHistoryUserName, @pHistoryUserID, @pHistoryAction, ##HISTORYVALUES##);".Replace("##COLUMNS##", newValue1).Replace("##TABLE##", this.relationJoinInfo.JoinTableName).Replace("##HISTORYVALUES##", newValue2);
    scope.AddDbParameters(command, historyParameters);
    scope.AddDbParameters(command, paramDict);
    return historyCommandText;
  }

  public Dictionary<string, object> CreateBackReferences(RelationStoreInfo activeRelation)
  {
    Dictionary<string, object> backReferences = new Dictionary<string, object>();
    if (this.relationJoinInfo.JoinType == JoinType.BackReferenceFromOtherEntity)
    {
      object entity = activeRelation.EntityStoreInfo.Entity;
      object obj = EntityHelper.For(entity.GetType()).ColumnPropertyMap[this.relationJoinInfo.TableKey].GetValue(entity);
      backReferences.Add(this.relationJoinInfo.JoinTableKey, obj);
    }
    return backReferences;
  }

  public void AddReference(object entity, Dictionary<string, object> storeExtraColumnValues)
  {
    if (this.relationJoinInfo.JoinType != JoinType.ReferenceToOtherEntity)
      return;
    object entity1 = PropertyHelper.For(this.propertyInfo).GetValue(entity);
    if (entity1 == null)
      return;
    object primaryKey = EntityHelper.GetPrimaryKey(entity1);
    storeExtraColumnValues.Add(this.relationJoinInfo.TableKey, primaryKey);
  }

  private string CreateWhereCondition(
    DBScope scope,
    DbCommand command,
    RelationStoreInfo relationStoreInfo)
  {
    string whereCondition = $"[{this.relationJoinInfo.JoinTableKey}]=@EntityKey AND [{this.relationJoinInfo.ConsecutiveJoin.TableKey}]=@RelationEntityKey";
    scope.AddDbParameter(command, "EntityKey", relationStoreInfo.EntityStoreInfo.Id);
    scope.AddDbParameter(command, "RelationEntityKey", relationStoreInfo.RelatedEntityId);
    if (relationStoreInfo.RelationValueFromDb != null)
    {
      if (relationStoreInfo.RelatedEntityStoreInfo.Entity == null)
      {
        object entityFromDb = relationStoreInfo.RelatedEntityStoreInfo.EntityFromDb;
      }
      whereCondition += $" AND [{this.relationJoinInfo.RelationValueColumn}]=@RelationValue";
      scope.AddDbParameter(command, "RelationValue", relationStoreInfo.RelationValueFromDb);
    }
    return whereCondition;
  }

  private Dictionary<string, object> CreateRelationStoreColumnValues(
    DBScope scope,
    RelationStoreInfo relationStoreInfo)
  {
    Dictionary<string, object> storeColumnValues = new Dictionary<string, object>();
    storeColumnValues.Add(this.relationJoinInfo.JoinTableKey, relationStoreInfo.EntityStoreInfo.Id);
    storeColumnValues.Add(this.relationJoinInfo.ConsecutiveJoin.TableKey, relationStoreInfo.RelatedEntityId);
    if (relationStoreInfo.RelationValue != null)
      storeColumnValues.Add(this.relationJoinInfo.RelationValueColumn, relationStoreInfo.RelationValue);
    bool flag1 = relationStoreInfo.RelationChangeType == RelationChangeType.ValueModified;
    bool flag2 = relationStoreInfo.RelationChangeType == RelationChangeType.Added;
    if (this.relationJoinInfo.InsertColumn != null & flag2)
      storeColumnValues.Add(this.relationJoinInfo.InsertColumn, (object) scope.TransactionTimestamp);
    if (this.relationJoinInfo.InsertUpdateColumn != null && flag2 | flag1)
      storeColumnValues.Add(this.relationJoinInfo.InsertUpdateColumn, (object) scope.TransactionTimestamp);
    if (this.relationJoinInfo.UpdateColumn != null & flag1)
      storeColumnValues.Add(this.relationJoinInfo.UpdateColumn, (object) scope.TransactionTimestamp);
    return storeColumnValues;
  }

  public ICollection<DbCommand> CreateRelationCommands(
    DBScope scope,
    ICollection<RelationStoreInfo> relationStoreInfos)
  {
    List<RelationStoreInfo> list1 = relationStoreInfos.Where<RelationStoreInfo>((Func<RelationStoreInfo, bool>) (rsi => rsi.RelationChangeType == RelationChangeType.Removed)).ToList<RelationStoreInfo>();
    List<RelationStoreInfo> list2 = relationStoreInfos.Where<RelationStoreInfo>((Func<RelationStoreInfo, bool>) (rsi => rsi.RelationChangeType == RelationChangeType.ValueModified)).ToList<RelationStoreInfo>();
    List<RelationStoreInfo> addRelations = relationStoreInfos.Where<RelationStoreInfo>((Func<RelationStoreInfo, bool>) (rsi => rsi.RelationChangeType == RelationChangeType.Added)).ToList<RelationStoreInfo>();
    if (this.relationJoinInfo.JoinType == JoinType.ReferenceToOtherEntity)
      list1 = list1.Where<RelationStoreInfo>((Func<RelationStoreInfo, bool>) (rrsi => rrsi.EntityStoreInfo != null && !addRelations.Any<RelationStoreInfo>((Func<RelationStoreInfo, bool>) (arsi => arsi.EntityStoreInfo != null && arsi.EntityStoreInfo.Entity == rrsi.EntityStoreInfo.Entity)))).ToList<RelationStoreInfo>();
    return (ICollection<DbCommand>) list1.Union<RelationStoreInfo>((IEnumerable<RelationStoreInfo>) list2).Union<RelationStoreInfo>((IEnumerable<RelationStoreInfo>) addRelations).Select<RelationStoreInfo, DbCommand>((Func<RelationStoreInfo, DbCommand>) (rsi => this.CreateRelationCommand(scope, rsi))).Where<DbCommand>((Func<DbCommand, bool>) (c => c != null)).ToList<DbCommand>();
  }
}
