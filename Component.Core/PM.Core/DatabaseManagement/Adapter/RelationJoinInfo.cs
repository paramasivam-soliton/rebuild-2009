// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.RelationJoinInfo
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class RelationJoinInfo
{
  private RelationJoinInfo()
  {
  }

  public RelationJoinInfo(PropertyInfo propertyInfo)
  {
    DbRelationAttribute customAttribute = (DbRelationAttribute) propertyInfo.GetCustomAttributes(typeof (DbRelationAttribute), true)[0];
    EntityHelper entityHelper1 = !propertyInfo.PropertyType.IsGenericType || !typeof (IList).IsAssignableFrom(propertyInfo.PropertyType) && !(propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof (IList<>)) ? EntityHelper.For(propertyInfo.PropertyType) : EntityHelper.For(propertyInfo.PropertyType.GetGenericArguments()[0]);
    this.JoinType = customAttribute.JoinType;
    this.InsertColumn = customAttribute.InsertTimestampColumn;
    this.InsertUpdateColumn = customAttribute.InsertUpdateTimestampColumn;
    this.UpdateColumn = customAttribute.UpdateTimestampColumn;
    switch (this.JoinType)
    {
      case JoinType.ReferenceToOtherEntity:
        this.JoinTableName = entityHelper1.TableName;
        this.JoinTableKey = entityHelper1.PrimaryKeyName;
        this.TableKey = customAttribute.OriginKey;
        break;
      case JoinType.BackReferenceFromOtherEntity:
        this.JoinTableName = entityHelper1.TableName;
        this.JoinTableKey = customAttribute.DestinationKey;
        this.TableKey = customAttribute.OriginKey;
        break;
      case JoinType.IntermediateTable:
        EntityHelper entityHelper2 = EntityHelper.For(propertyInfo.DeclaringType);
        this.JoinTableName = customAttribute.IntermediateTable;
        this.JoinTableKey = customAttribute.OriginKey;
        this.TableKey = entityHelper2.PrimaryKeyName;
        this.ConsecutiveJoin = new RelationJoinInfo()
        {
          JoinTableName = entityHelper1.TableName,
          JoinTableKey = entityHelper1.PrimaryKeyName,
          TableKey = customAttribute.DestinationKey
        };
        this.JoinConditionColumn = customAttribute.JoinConditionColumn;
        this.JoinConditionValue = customAttribute.JoinConditionValue;
        if (this.JoinConditionValue is Enum)
        {
          this.JoinConditionValue = Convert.ChangeType(this.JoinConditionValue, Enum.GetUnderlyingType(this.JoinConditionValue.GetType()));
          break;
        }
        break;
    }
    if (this.JoinConditionColumn != null)
    {
      this.RelationValueColumn = this.JoinConditionColumn;
    }
    else
    {
      if (entityHelper1.RelationValuePropertyMap.Count != 1)
        return;
      this.RelationValueColumn = entityHelper1.RelationValuePropertyMap.First<KeyValuePair<string, PropertyHelper>>().Key;
    }
  }

  public string JoinTableName { get; private set; }

  public string TableKey { get; private set; }

  public string JoinTableKey { get; private set; }

  public string JoinConditionColumn { get; private set; }

  public string RelationValueColumn { get; private set; }

  public object JoinConditionValue { get; private set; }

  public RelationJoinInfo ConsecutiveJoin { get; private set; }

  public JoinType JoinType { get; private set; }

  public string InsertColumn { get; set; }

  public string UpdateColumn { get; set; }

  public string InsertUpdateColumn { get; set; }
}
