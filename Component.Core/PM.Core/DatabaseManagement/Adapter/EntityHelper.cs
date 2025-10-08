// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.EntityHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Attributes;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.HistoryTracker;
using PathMedical.SystemConfiguration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class EntityHelper
{
  protected static Dictionary<Type, EntityHelper> entityHelpers = new Dictionary<Type, EntityHelper>();
  protected Type entityType;

  protected EntityHelper(Type entityType)
  {
    EntityHelper entityHelper = this;
    this.entityType = entityType;
    try
    {
      PropertyInfo propertyWithAttribute = entityType.GetPropertyWithAttribute<DbPrimaryKeyColumnAttribute>();
      this.PrimaryKeyName = this.GetColumnName(propertyWithAttribute);
      this.PrimaryKeyType = propertyWithAttribute.PropertyType;
    }
    catch
    {
      throw ExceptionFactory.Instance.CreateException<ExecutionException>(string.Format(ComponentResourceManagement.Instance.ResourceManager.GetString("PrimaryKeyForClassMissing"), (object) entityType.Name));
    }
    this.ColumnPropertyMap = entityType.GetPropertiesWithAttribute<DbColumnAttribute>().ToDictionary<PropertyInfo, string, PropertyHelper>((Func<PropertyInfo, string>) (pi => entityHelper.GetColumnName(pi)), (Func<PropertyInfo, PropertyHelper>) (pi => PropertyHelper.For(pi)));
    this.ModifiableColumnPropertyMap = this.ColumnPropertyMap.Where<KeyValuePair<string, PropertyHelper>>((Func<KeyValuePair<string, PropertyHelper>, bool>) (cp => !entityType.GetPropertiesWithAttribute<DbPrimaryKeyColumnAttribute>().Union<PropertyInfo>(entityType.GetPropertiesWithAttribute<DbGeneratedColumnAttribute>()).Union<PropertyInfo>(entityType.GetPropertiesWithAttribute<DbTimestampColumnAttribute>()).Contains<PropertyInfo>(cp.Value.PropertyInfo))).ToDictionary<KeyValuePair<string, PropertyHelper>, string, PropertyHelper>((Func<KeyValuePair<string, PropertyHelper>, string>) (m => m.Key), (Func<KeyValuePair<string, PropertyHelper>, PropertyHelper>) (m => m.Value));
    this.HistoryRelevantColumnPropertyMap = this.ModifiableColumnPropertyMap.Where<KeyValuePair<string, PropertyHelper>>((Func<KeyValuePair<string, PropertyHelper>, bool>) (cp => entityType.GetPropertiesWithAttribute<DbColumnAttribute>((Func<DbColumnAttribute, bool>) (dca => dca.IsHistoryRelevant)).Contains<PropertyInfo>(cp.Value.PropertyInfo))).ToDictionary<KeyValuePair<string, PropertyHelper>, string, PropertyHelper>((Func<KeyValuePair<string, PropertyHelper>, string>) (m => m.Key), (Func<KeyValuePair<string, PropertyHelper>, PropertyHelper>) (m => m.Value));
    this.RelationValuePropertyMap = entityType.GetPropertiesWithAttribute<DbRelationValueAttribute>().ToDictionary<PropertyInfo, string, PropertyHelper>((Func<PropertyInfo, string>) (pi => entityType.GetReflectionInfoHelper().GetAttributesForProperty(pi).Select<object, DbRelationValueAttribute>((Func<object, DbRelationValueAttribute>) (a => a as DbRelationValueAttribute)).Single<DbRelationValueAttribute>((Func<DbRelationValueAttribute, bool>) (a => a != null)).ColumnName), (Func<PropertyInfo, PropertyHelper>) (pi => PropertyHelper.For(pi)));
    this.RelationProperties = (ICollection<PropertyHelper>) entityType.GetPropertiesWithAttribute<DbRelationAttribute>().Select<PropertyInfo, PropertyHelper>((Func<PropertyInfo, PropertyHelper>) (pi => PropertyHelper.For(pi))).ToList<PropertyHelper>();
    try
    {
      foreach (PropertyInfo propertyInfo in entityType.GetPropertiesWithAttribute<DbTimestampColumnAttribute>())
      {
        Type propertyType = propertyInfo.PropertyType;
        if (propertyType != typeof (DateTime) && propertyType != typeof (DateTime?))
          throw new ExecutionException();
        DbTimestampColumnAttribute timestampColumnAttribute = entityType.GetReflectionInfoHelper().GetAttributesForProperty(propertyInfo).Single<object>((Func<object, bool>) (o => o is DbTimestampColumnAttribute)) as DbTimestampColumnAttribute;
        if (timestampColumnAttribute.TimestampSetOption == TimestampSetOption.OnInsert)
          this.InsertTimestampColumnName = this.InsertTimestampColumnName == null ? this.GetColumnName(propertyInfo) : throw new InvalidOperationException();
        else if (timestampColumnAttribute.TimestampSetOption == TimestampSetOption.OnUpdate)
          this.UpdateTimestampColumnName = this.UpdateTimestampColumnName == null ? this.GetColumnName(propertyInfo) : throw new InvalidOperationException();
        else
          this.InsertUpdateTimestampColumnName = this.InsertUpdateTimestampColumnName == null ? this.GetColumnName(propertyInfo) : throw new InvalidOperationException();
      }
    }
    catch (InvalidOperationException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ExecutionException>(string.Format(ComponentResourceManagement.Instance.ResourceManager.GetString("TimestampColumnInvalid"), (object) entityType.Name), (System.Exception) ex);
    }
    object[] customAttributes = entityType.GetCustomAttributes(typeof (DbTableAttribute), true);
    this.HasHistory = SystemConfigurationManager.Instance.ShallTrackHistory && customAttributes.Length == 1 && ((DbTableAttribute) customAttributes[0]).HasHistory;
    this.TableName = customAttributes.Length != 1 || ((DbTableAttribute) customAttributes[0]).Name == null ? entityType.Name : ((DbTableAttribute) customAttributes[0]).Name;
    List<PropertyInfo> list = ((IEnumerable<PropertyInfo>) entityType.GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (pi => pi.PropertyType == typeof (EntityLoadInformation))).ToList<PropertyInfo>();
    this.loadInformationPropertyHelper = list.Count == 1 ? PropertyHelper.For(list[0]) : throw ExceptionFactory.Instance.CreateException<ExecutionException>(string.Format(ComponentResourceManagement.Instance.ResourceManager.GetString("ExactlyOneLoadInformationPropertyNeeded"), (object) entityType.Name));
  }

  public string PrimaryKeyName { get; protected set; }

  public Type PrimaryKeyType { get; protected set; }

  public string TableName { get; protected set; }

  public Dictionary<string, PropertyHelper> ColumnPropertyMap { get; protected set; }

  public Dictionary<string, PropertyHelper> HistoryRelevantColumnPropertyMap { get; protected set; }

  public Dictionary<string, PropertyHelper> ModifiableColumnPropertyMap { get; protected set; }

  public Dictionary<string, PropertyHelper> RelationValuePropertyMap { get; protected set; }

  public ICollection<PropertyHelper> RelationProperties { get; protected set; }

  public string UpdateTimestampColumnName { get; protected set; }

  public string InsertTimestampColumnName { get; protected set; }

  public string InsertUpdateTimestampColumnName { get; protected set; }

  public bool HasHistory { get; protected set; }

  protected PropertyHelper loadInformationPropertyHelper { get; set; }

  public static EntityHelper For(Type entityType)
  {
    EntityHelper entityHelper;
    if (!EntityHelper.entityHelpers.TryGetValue(entityType, out entityHelper))
    {
      entityHelper = new EntityHelper(entityType);
      EntityHelper.entityHelpers.Add(entityType, entityHelper);
    }
    return entityHelper;
  }

  public static EntityHelper For<T>() => EntityHelper.For(typeof (T));

  public DbCommand CreateInsertCommand(
    DBScope scope,
    object entity,
    object entityId,
    Dictionary<string, object> extraColumnValues)
  {
    DbCommand dbCommand = scope.CreateDbCommand();
    Dictionary<string, object> dictionary = this.ModifiableColumnPropertyMap.ToDictionary<KeyValuePair<string, PropertyHelper>, string, object>((Func<KeyValuePair<string, PropertyHelper>, string>) (m => m.Key), (Func<KeyValuePair<string, PropertyHelper>, object>) (m => m.Value.GetValue(entity))).Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (i => !this.IsEmptyValue(i.Value))).ToDictionary<KeyValuePair<string, object>, string, object>((Func<KeyValuePair<string, object>, string>) (i => i.Key), (Func<KeyValuePair<string, object>, object>) (i => this.ConvertArrayToDB(i.Value)));
    if (!0.Equals(entityId))
      dictionary.Add(this.PrimaryKeyName, entityId);
    if (this.InsertTimestampColumnName != null)
      dictionary.Add(this.InsertTimestampColumnName, (object) scope.TransactionTimestamp);
    if (this.InsertUpdateTimestampColumnName != null)
      dictionary.Add(this.InsertUpdateTimestampColumnName, (object) scope.TransactionTimestamp);
    if (extraColumnValues != null)
    {
      foreach (KeyValuePair<string, object> extraColumnValue in extraColumnValues)
        dictionary.AddOrOverwriteEntry<string, object>(extraColumnValue.Key, extraColumnValue.Value);
    }
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    foreach (KeyValuePair<string, object> keyValuePair in dictionary)
    {
      stringBuilder1.Append("[").Append(keyValuePair.Key).Append("]").Append(", ");
      stringBuilder2.Append("@").Append(keyValuePair.Key).Append(", ");
      scope.AddDbParameter(dbCommand, keyValuePair.Key, keyValuePair.Value);
    }
    stringBuilder1.Length -= 2;
    stringBuilder2.Length -= 2;
    string historyCommandText = this.CreateHistoryCommandText(scope, dbCommand, entity, entityId, new HistoryActionType?(HistoryActionType.InsertEntity));
    scope.AddDbParameter(dbCommand, "pEntityID", entityId);
    dbCommand.CommandText = "INSERT INTO [##TABLE##]        (##COLUMNS##) VALUES (##VALUES##);\n##HISTORY##".Replace("##HISTORY##", historyCommandText).Replace("##TABLE##", this.TableName).Replace("##IDCOLUMN##", this.PrimaryKeyName).Replace("##COLUMNS##", stringBuilder1.ToString()).Replace("##VALUES##", stringBuilder2.ToString());
    return dbCommand;
  }

  public DbCommand CreateUpdateCommand(
    DBScope scope,
    object entity,
    object entityOriginal,
    Dictionary<string, object> extraColumnValues)
  {
    DbCommand dbCommand = scope.CreateDbCommand();
    Dictionary<string, object> dictionary = this.ModifiableColumnPropertyMap.Where<KeyValuePair<string, PropertyHelper>>((Func<KeyValuePair<string, PropertyHelper>, bool>) (m => !object.Equals(m.Value.GetValue(entity), m.Value.GetValue(entityOriginal)))).ToDictionary<KeyValuePair<string, PropertyHelper>, string, object>((Func<KeyValuePair<string, PropertyHelper>, string>) (m => m.Key), (Func<KeyValuePair<string, PropertyHelper>, object>) (m => this.ConvertNullAndArrayToDB(m.Value.GetValue(entity))));
    if (this.UpdateTimestampColumnName != null)
      dictionary.Add(this.UpdateTimestampColumnName, (object) scope.TransactionTimestamp);
    if (this.InsertUpdateTimestampColumnName != null)
      dictionary.Add(this.InsertUpdateTimestampColumnName, (object) scope.TransactionTimestamp);
    if (extraColumnValues != null)
    {
      foreach (KeyValuePair<string, object> extraColumnValue in extraColumnValues)
        dictionary.AddOrOverwriteEntry<string, object>(extraColumnValue.Key, extraColumnValue.Value);
    }
    StringBuilder stringBuilder = new StringBuilder();
    foreach (KeyValuePair<string, object> keyValuePair in dictionary)
    {
      stringBuilder.Append("[").Append(keyValuePair.Key).Append("]=@").Append(keyValuePair.Key).Append(", ");
      scope.AddDbParameter(dbCommand, keyValuePair.Key, keyValuePair.Value);
    }
    stringBuilder.Length -= 2;
    string newValue = EntityHelper.HasHistoryRelevantPropertyModifications(entity, entityOriginal) ? this.CreateHistoryCommandText(scope, dbCommand, entity, (object) null, new HistoryActionType?(HistoryActionType.UpdateEntity)) : string.Empty;
    dbCommand.CommandText = "UPDATE [##TABLE##] SET    ##SETS## WHERE  [##IDCOLUMN##] = @pEntityID;\n##HISTORY##".Replace("##HISTORY##", newValue).Replace("##TABLE##", this.TableName).Replace("##SETS##", stringBuilder.ToString()).Replace("##IDCOLUMN##", this.PrimaryKeyName);
    scope.AddDbParameter(dbCommand, "pEntityID", EntityHelper.GetPrimaryKey(entity));
    return dbCommand;
  }

  public DbCommand CreateDeleteCommand(DBScope scope, object entity)
  {
    DbCommand dbCommand = scope.CreateDbCommand();
    string historyCommandText = this.CreateHistoryCommandText(scope, dbCommand, entity, (object) null, new HistoryActionType?(HistoryActionType.DeleteEntity));
    dbCommand.CommandText = "##HISTORY##\nDELETE FROM [##TABLE##] WHERE  [##IDCOLUMN##] = @pEntityID;".Replace("##HISTORY##", historyCommandText).Replace("##TABLE##", this.TableName).Replace("##IDCOLUMN##", this.PrimaryKeyName);
    scope.AddDbParameter(dbCommand, "pEntityID", EntityHelper.GetPrimaryKey(entity));
    return dbCommand;
  }

  private string CreateHistoryCommandText(
    DBScope scope,
    DbCommand command,
    object entity,
    object entityId,
    HistoryActionType? historyAction)
  {
    if (!this.HasHistory)
      return string.Empty;
    string newValue1 = $"[{string.Join("], [", this.ColumnPropertyMap.Keys.ToArray<string>())}]";
    string newValue2 = "@" + string.Join(", @", this.ColumnPropertyMap.Keys.ToArray<string>());
    string historyCommandText = "INSERT INTO [##TABLE##History] (HistoryTimestamp, HistoryUserName, HistoryUserID, HistoryAction, ##COLUMNS##) VALUES (@pHistoryTimeStamp, @pHistoryUserName, @pHistoryUserID, @pHistoryAction, ##HISTORYVALUES##);".Replace("##COLUMNS##", newValue1).Replace("##HISTORYVALUES##", newValue2);
    scope.AddDbParameters(command, SqlStatementHelper.CreateHistoryParameters(scope, historyAction));
    Dictionary<string, object> dictionary = this.ColumnPropertyMap.ToDictionary<KeyValuePair<string, PropertyHelper>, string, object>((Func<KeyValuePair<string, PropertyHelper>, string>) (m => m.Key), (Func<KeyValuePair<string, PropertyHelper>, object>) (m => this.ConvertNullAndArrayToDB(m.Value.GetValue(entity))));
    HistoryActionType? nullable = historyAction;
    HistoryActionType historyActionType = HistoryActionType.InsertEntity;
    if (nullable.GetValueOrDefault() == historyActionType & nullable.HasValue)
      dictionary[this.PrimaryKeyName] = entityId;
    scope.AddDbParameters(command, dictionary);
    return historyCommandText;
  }

  protected bool IsEmptyValue(object value)
  {
    if (value == null)
      return true;
    if (value is string)
      return value.Equals((object) string.Empty);
    return !value.GetType().IsPrimitive && (value.Equals((object) Guid.Empty) || value.Equals((object) DateTime.MinValue));
  }

  protected object ConvertNullAndArrayToDB(object value)
  {
    return !this.IsEmptyValue(value) ? this.ConvertArrayToDB(value) : (object) DBNull.Value;
  }

  protected object ConvertArrayToDB(object value)
  {
    if (value != null && value.GetType().IsArray && value.GetType().GetElementType() != typeof (byte) && value.GetType().GetElementType() != typeof (double) && value.GetType().GetElementType() != typeof (float) && typeof (IConvertible).IsAssignableFrom(value.GetType().GetElementType()))
    {
      List<byte> byteList = new List<byte>();
      foreach (object obj in (Array) value)
      {
        long int64 = Convert.ToInt64(obj);
        byteList.AddRange((IEnumerable<byte>) BitConverter.GetBytes(int64));
      }
      return (object) byteList.ToArray();
    }
    if (value == null || !value.GetType().IsArray || !(value.GetType().GetElementType() == typeof (double)) && !(value.GetType().GetElementType() == typeof (float)) || !typeof (IConvertible).IsAssignableFrom(value.GetType().GetElementType()))
      return value;
    List<byte> byteList1 = new List<byte>();
    foreach (object obj in (Array) value)
    {
      double num = Convert.ToDouble(obj);
      byteList1.AddRange((IEnumerable<byte>) BitConverter.GetBytes(num));
    }
    return (object) byteList1.ToArray();
  }

  protected string GetColumnName(PropertyInfo propertyInfo)
  {
    return ((DbColumnAttribute) propertyInfo.GetCustomAttributes(typeof (DbColumnAttribute), true)[0]).Name ?? propertyInfo.Name;
  }

  public string GetColumnName(string propertyName)
  {
    return this.GetColumnName(this.entityType.GetProperty(propertyName));
  }

  public static object GetPrimaryKey(object entity)
  {
    EntityHelper entityHelper = entity != null ? EntityHelper.For(entity.GetType()) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
    return entityHelper.ColumnPropertyMap[entityHelper.PrimaryKeyName].GetValue(entity);
  }

  public static void SetPrimaryKey(object entity, object id)
  {
    EntityHelper entityHelper = entity != null ? EntityHelper.For(entity.GetType()) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
    entityHelper.ColumnPropertyMap[entityHelper.PrimaryKeyName].SetValue(entity, id);
  }

  public static bool IsNewEntity(object entity)
  {
    EntityHelper entityHelper = entity != null ? EntityHelper.For(entity.GetType()) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
    IEnumerable<string> source = ((IEnumerable<string>) new string[3]
    {
      entityHelper.InsertTimestampColumnName,
      entityHelper.InsertUpdateTimestampColumnName,
      entityHelper.UpdateTimestampColumnName
    }).Where<string>((Func<string, bool>) (tsc => !string.IsNullOrEmpty(tsc)));
    return source.Any<string>() ? !source.Select<string, object>((Func<string, object>) (utsc => entityHelper.ColumnPropertyMap[utsc].GetValue(entity))).Any<object>((Func<object, bool>) (ts => ts != null && !ts.Equals((object) DateTime.MinValue))) : !EntityHelper.HasPrimaryKeyAssigned(entity);
  }

  public static bool HasPrimaryKeyAssigned(object entity)
  {
    EntityHelper entityHelper = entity != null ? EntityHelper.For(entity.GetType()) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
    object obj = entityHelper.ColumnPropertyMap[entityHelper.PrimaryKeyName].GetValue(entity);
    object instance = Activator.CreateInstance(entityHelper.PrimaryKeyType);
    return obj != null && !obj.Equals(instance);
  }

  public static void SetLoadInformation(object entity, EntityLoadInformation loadInformation)
  {
    EntityHelper entityHelper = entity != null ? EntityHelper.For(entity.GetType()) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
    if (entityHelper.loadInformationPropertyHelper == null)
      return;
    entityHelper.loadInformationPropertyHelper.SetValue(entity, (object) loadInformation);
  }

  public static EntityLoadInformation GetLoadInformation(object entity)
  {
    return entity != null ? EntityHelper.For(entity.GetType()).loadInformationPropertyHelper.GetValue(entity) as EntityLoadInformation : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
  }

  public static bool HasPropertyModifications(object entity, object entityOriginal)
  {
    return EntityHelper.HasPropertyModificationsCore(entity, entityOriginal, false);
  }

  public static bool HasHistoryRelevantPropertyModifications(object entity, object entityOriginal)
  {
    return EntityHelper.HasPropertyModificationsCore(entity, entityOriginal, true);
  }

  private static bool HasPropertyModificationsCore(
    object entity,
    object entityOriginal,
    bool onlyHistoryRelevantProperties)
  {
    if (entity == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entity));
    if (entityOriginal == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (entityOriginal));
    if (entity.GetType() != entityOriginal.GetType())
      throw ExceptionFactory.Instance.CreateException<ArgumentException>("Arguments must be of same type.");
    EntityHelper entityHelper = EntityHelper.GetPrimaryKey(entity).Equals(EntityHelper.GetPrimaryKey(entityOriginal)) ? EntityHelper.For(entity.GetType()) : throw ExceptionFactory.Instance.CreateException<ArgumentException>("Arguments must have the same primary keys.");
    foreach (PropertyHelper propertyHelper in onlyHistoryRelevantProperties ? entityHelper.HistoryRelevantColumnPropertyMap.Values : entityHelper.ModifiableColumnPropertyMap.Values)
    {
      if (!object.Equals(propertyHelper.GetValue(entity) ?? (object) string.Empty, propertyHelper.GetValue(entityOriginal) ?? (object) string.Empty))
        return true;
    }
    return false;
  }
}
