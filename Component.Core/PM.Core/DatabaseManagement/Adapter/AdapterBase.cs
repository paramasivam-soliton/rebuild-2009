// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.AdapterBase`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Attributes;
using PathMedical.Exception;
using PathMedical.HistoryTracker;
using PathMedical.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class AdapterBase<T> where T : class, new()
{
  protected const string Whereid = "EntityID";
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (AdapterBase<T>), "$Rev: 1276 $");
  protected LoadEntityOption CustomLoadEntityOption;
  protected LoadEntityOption EmptyLoadEntityOption;
  protected DBScope Scope;

  public AdapterBase(DBScope scope)
  {
    this.Scope = scope != null ? scope : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (scope));
    this.CustomLoadEntityOption = LoadEntityOption.CreateRootLoadOption(typeof (T));
    this.EmptyLoadEntityOption = LoadEntityOption.CreateRootLoadOption(typeof (T));
  }

  public ICollection<T> All
  {
    get
    {
      return this.FetchEntitiesCore(QueryBuilder.GetCompleteSelectClause(this.CustomLoadEntityOption, (string) null, false), (Dictionary<string, object>) null);
    }
  }

  protected static string SelectClause => $"SELECT * FROM [{EntityHelper.For<T>().TableName}] ";

  protected static string SelectCountClause
  {
    get => $"SELECT COUNT(*) FROM [{EntityHelper.For<T>().TableName}] ";
  }

  protected static string WhereIdClause
  {
    get => $" WHERE T1.[{EntityHelper.For<T>().PrimaryKeyName}]=@{"EntityID"}";
  }

  public T GetEntityById(object entityId)
  {
    if (entityId == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entityId));
    if (entityId.GetType() != EntityHelper.For<T>().PrimaryKeyType)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entityId));
    this.EnsureActiveScope();
    T entityToFill = new T();
    this.FillEntityById((object) entityToFill, entityId, this.CustomLoadEntityOption);
    return entityToFill;
  }

  public void RefreshEntity(T entity)
  {
    if ((object) entity == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
    this.EnsureActiveScope();
    object primaryKey = EntityHelper.GetPrimaryKey((object) entity);
    this.FillEntityById((object) entity, primaryKey, this.CustomLoadEntityOption);
  }

  private void FillEntityById(
    object entityToFill,
    object entityId,
    LoadEntityOption loadEntityOption)
  {
    try
    {
      using (DbCommand dbCommand = this.Scope.CreateDbCommand())
      {
        dbCommand.CommandText = QueryBuilder.GetCompleteSelectClause(loadEntityOption, AdapterBase<T>.WhereIdClause, false);
        this.Scope.AddDbParameter(dbCommand, "EntityID", entityId);
        using (DbDataReader reader = this.ExecuteReader(dbCommand))
          new EntityFiller(loadEntityOption, reader).RefillOneItemFromReader(entityToFill);
      }
    }
    catch (RecordNotFoundException ex)
    {
      throw;
    }
    catch (System.Exception ex)
    {
      throw ExceptionHelper.CreateException<ExecutionException>(ex, "ErrorWhenLoadingRecords");
    }
  }

  public ICollection<T> FetchEntities(Expression<Func<T, bool>> expression)
  {
    TranslatorResult translatorResult = ExpressionTranslator.Translate((Expression) expression);
    return this.FetchEntitiesCore(QueryBuilder.GetCompleteSelectClause(this.CustomLoadEntityOption, translatorResult.WhereClause, false), translatorResult.Parameters);
  }

  public int Count()
  {
    using (DbCommand dbCommand = this.Scope.CreateDbCommand())
    {
      dbCommand.CommandText = AdapterBase<T>.SelectCountClause;
      return (int) this.ExecuteScalar(dbCommand);
    }
  }

  public int Count(Expression<Func<T, bool>> expression)
  {
    TranslatorResult translatorResult = ExpressionTranslator.Translate((Expression) expression);
    using (DbCommand dbCommand = this.Scope.CreateDbCommand())
    {
      dbCommand.CommandText = $"{AdapterBase<T>.SelectCountClause} WHERE {translatorResult.WhereClause}";
      this.Scope.AddDbParameters(dbCommand, translatorResult.Parameters);
      return (int) this.ExecuteScalar(dbCommand);
    }
  }

  public virtual void Delete(T entity)
  {
    if ((object) entity == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
    this.EnsureActiveScope();
    object primaryKey = EntityHelper.GetPrimaryKey((object) entity);
    using (DbCommand deleteCommand = EntityHelper.For<T>().CreateDeleteCommand(this.Scope, (object) entity))
    {
      int num;
      try
      {
        num = this.ExecuteNonQuery(deleteCommand);
      }
      catch (System.Exception ex)
      {
        object[] objArray = new object[2]
        {
          (object) typeof (T).Name,
          primaryKey
        };
        throw ExceptionHelper.CreateException<ExecutionException>(ex, "CouldNotDeleteRecordFromDatabase", objArray);
      }
      if (num == 0)
        throw ExceptionHelper.CreateException<RecordDeletedException>("CouldNotDeleteRecordFromDatabase", (object) typeof (T).Name, primaryKey);
    }
  }

  public virtual void Store(ICollection<T> entities)
  {
    if (entities == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entities));
    foreach (T entity in (IEnumerable<T>) entities)
      this.Store(entity);
  }

  public virtual void Store(T entity)
  {
    if ((object) entity == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
    try
    {
      this.EnsureActiveScope();
      LoadEntityOption loadEntityOption;
      if (EntityHelper.IsNewEntity((object) entity))
      {
        loadEntityOption = this.CustomLoadEntityOption;
      }
      else
      {
        EntityLoadInformation loadInformation = EntityHelper.GetLoadInformation((object) entity);
        loadEntityOption = loadInformation != null ? loadInformation.LoadEntityOption : this.EmptyLoadEntityOption;
      }
      this.StoreWithRelations(EntityRelationChangeHelper.GetStoreInfo((object) entity, loadEntityOption, this.Scope), loadEntityOption, (Dictionary<string, object>) null);
      object primaryKey = EntityHelper.GetPrimaryKey((object) entity);
      this.FillEntityById((object) entity, primaryKey, loadEntityOption);
    }
    catch (ExecutionException ex)
    {
      throw;
    }
    catch (System.Exception ex)
    {
      object[] objArray = new object[2]
      {
        (object) typeof (T).Name,
        EntityHelper.GetPrimaryKey((object) entity)
      };
      throw ExceptionHelper.CreateException<ExecutionException>(ex, "CouldNotWriteRecordToDatabase", objArray);
    }
  }

  private void StoreWithRelations(
    EntityStoreInfo storeInfo,
    LoadEntityOption loadEntityOption,
    Dictionary<string, object> storeExtraColumnValues)
  {
    bool flag = loadEntityOption.RelationJoinInfo != null && loadEntityOption.RelationJoinInfo.JoinType == JoinType.BackReferenceFromOtherEntity && storeInfo.ParentRelationStoreInfo != null && storeInfo.ParentRelationStoreInfo.RelationChangeType == RelationChangeType.Added;
    if (((storeInfo.IsNew ? 1 : (storeInfo.IsChanged ? 1 : 0)) | (flag ? 1 : 0)) != 0)
    {
      if (storeExtraColumnValues == null)
        storeExtraColumnValues = new Dictionary<string, object>();
      foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) loadEntityOption.Children)
        child.RelationHelper.AddReference(storeInfo.Entity, storeExtraColumnValues);
      EntityHelper entityHelper = EntityHelper.For(storeInfo.Entity.GetType());
      using (DbCommand command = storeInfo.IsNew ? entityHelper.CreateInsertCommand(this.Scope, storeInfo.Entity, storeInfo.Id, storeExtraColumnValues) : entityHelper.CreateUpdateCommand(this.Scope, storeInfo.Entity, storeInfo.EntityFromDb, storeExtraColumnValues))
      {
        if (this.ExecuteNonQuery(command) == 0)
          throw ExceptionHelper.CreateException<ExecutionException>("CouldNotWriteRecordToDatabase", (object) typeof (T).Name, EntityHelper.GetPrimaryKey(storeInfo.Entity));
        if (object.Equals(EntityHelper.GetPrimaryKey(storeInfo.Entity), (object) 0))
          storeInfo.Id = (object) this.GetLastInsertedIntId();
        if (storeInfo.IsNew)
          EntityHelper.SetPrimaryKey(storeInfo.Entity, storeInfo.Id);
      }
    }
    foreach (LoadEntityOption child in (IEnumerable<LoadEntityOption>) loadEntityOption.Children)
    {
      RelationHelper relationHelper = child.RelationHelper;
      LoadEntityOption option = child;
      List<RelationStoreInfo> list = storeInfo.RelationStoreInfos.Where<RelationStoreInfo>((Func<RelationStoreInfo, bool>) (rsi => rsi.LoadEntityOption == option)).ToList<RelationStoreInfo>();
      foreach (RelationStoreInfo activeRelation in list.Where<RelationStoreInfo>((Func<RelationStoreInfo, bool>) (er => er.RelatedEntityStoreInfo != null)).ToList<RelationStoreInfo>())
      {
        Dictionary<string, object> backReferences = relationHelper.CreateBackReferences(activeRelation);
        this.StoreWithRelations(activeRelation.RelatedEntityStoreInfo, child, backReferences);
      }
      ICollection<DbCommand> relationCommands = relationHelper.CreateRelationCommands(this.Scope, (ICollection<RelationStoreInfo>) list);
      if (relationCommands.Count > 0)
      {
        foreach (DbCommand command in (IEnumerable<DbCommand>) relationCommands)
        {
          if (this.ExecuteNonQuery(command) == 0)
            throw ExceptionHelper.CreateException<ExecutionException>("CouldNotChangeRelationInDatabase");
        }
      }
    }
  }

  public ICollection<HistoryEntry> GetHistoryEntries(T entity)
  {
    return this.GetHistoryEntries(EntityHelper.GetPrimaryKey((object) entity));
  }

  public ICollection<HistoryEntry> GetHistoryEntries(object entityId)
  {
    if (!EntityHelper.For<T>().HasHistory)
      throw ExceptionHelper.CreateException<ExecutionException>("No history present for type {0}", (object) typeof (T).Name);
    this.EnsureActiveScope();
    using (DbCommand dbCommand = this.Scope.CreateDbCommand())
    {
      dbCommand.CommandText = QueryBuilder.GetCompleteSelectClause(this.CustomLoadEntityOption, AdapterBase<T>.WhereIdClause, true);
      this.Scope.AddDbParameter(dbCommand, "EntityID", entityId);
      using (DbDataReader reader = this.ExecuteReader(dbCommand))
        return new EntityFiller(this.CustomLoadEntityOption, reader).CreateHistoryItemsFromReader();
    }
  }

  public void LoadWithRelation([Localizable(false)] params string[] propertyNames)
  {
    if (propertyNames == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (propertyNames));
    foreach (string propertyName in propertyNames)
      this.CustomLoadEntityOption.AddNestedLoadEntityOption(propertyName);
  }

  protected ICollection<T> FetchEntitiesCore(
    string queryText,
    Dictionary<string, object> parameters)
  {
    this.EnsureActiveScope();
    using (DbCommand dbCommand = this.Scope.CreateDbCommand())
    {
      dbCommand.CommandText = queryText;
      if (parameters != null)
        this.Scope.AddDbParameters(dbCommand, parameters);
      using (DbDataReader reader = this.ExecuteReader(dbCommand))
        return (ICollection<T>) new EntityFiller(this.CustomLoadEntityOption, reader).CreateItemsFromReader().Cast<T>().ToList<T>();
    }
  }

  protected virtual object GetPrimaryKeyOrGenerateOne(object entity)
  {
    object keyOrGenerateOne = EntityHelper.GetPrimaryKey(entity);
    if (Guid.Empty.Equals(keyOrGenerateOne))
      keyOrGenerateOne = (object) Guid.NewGuid();
    return keyOrGenerateOne;
  }

  protected virtual int GetLastInsertedIntId()
  {
    using (DbCommand dbCommand = this.Scope.CreateDbCommand())
    {
      dbCommand.CommandText = "SELECT @@IDENTITY";
      return Convert.ToInt32(this.ExecuteScalar(dbCommand));
    }
  }

  protected void EnsureActiveScope()
  {
    if (!this.Scope.IsActive)
      throw ExceptionHelper.CreateException<ExecutionException>("DatabaseTransactionIsNotActive");
  }

  protected int ExecuteNonQuery(DbCommand command)
  {
    int num = 0;
    this.LogCommand(command);
    string commandText = command.CommandText;
    IEnumerable<string> strings = ((IEnumerable<string>) command.CommandText.Split(';')).Select<string, string>((Func<string, string>) (ct => ct.Trim())).Where<string>((Func<string, bool>) (ct => ct != string.Empty));
    try
    {
      foreach (string str in strings)
      {
        command.CommandText = str;
        num += command.ExecuteNonQuery();
      }
    }
    finally
    {
      command.CommandText = commandText;
    }
    if (AdapterBase<T>.Logger.IsDebugEnabled)
      AdapterBase<T>.Logger.Debug("Result: {0} affected rows", (object) num);
    return num;
  }

  protected object ExecuteScalar(DbCommand command)
  {
    this.LogCommand(command);
    object obj = command.ExecuteScalar();
    if (AdapterBase<T>.Logger.IsDebugEnabled)
      AdapterBase<T>.Logger.Debug("Result: {0}", obj);
    return obj;
  }

  protected DbDataReader ExecuteReader(DbCommand command)
  {
    this.LogCommand(command);
    return command.ExecuteReader();
  }

  protected void LogCommand(DbCommand command)
  {
    if (!AdapterBase<T>.Logger.IsDebugEnabled)
      return;
    string message = $"Database command:\nText:\n{command.CommandText}\nParameters:\n{string.Join("\n", command.Parameters.Cast<DbParameter>().Select<DbParameter, string>((Func<DbParameter, string>) (p => $"Parameter {p.ParameterName}={p.Value}")).ToArray<string>())}";
    AdapterBase<T>.Logger.Debug(message);
  }
}
