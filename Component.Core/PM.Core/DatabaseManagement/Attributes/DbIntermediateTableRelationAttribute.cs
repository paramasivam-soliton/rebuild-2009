// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Attributes.DbIntermediateTableRelationAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.DatabaseManagement.Attributes;

public class DbIntermediateTableRelationAttribute : DbRelationAttribute
{
  public DbIntermediateTableRelationAttribute(
    string entityReferenceColumn,
    string relatedEntityReferenceColumn,
    string intermediateTable)
  {
    this.OriginKey = entityReferenceColumn;
    this.DestinationKey = relatedEntityReferenceColumn;
    this.IntermediateTable = intermediateTable;
    this.JoinType = JoinType.IntermediateTable;
  }

  public DbIntermediateTableRelationAttribute(
    string entityReferenceColumn,
    string relatedEntityReferenceColumn,
    string intermediateTable,
    string joinConditionColumn,
    object joinConditionValue)
    : this(entityReferenceColumn, relatedEntityReferenceColumn, intermediateTable)
  {
    this.JoinConditionColumn = joinConditionColumn;
    this.JoinConditionValue = joinConditionValue;
  }
}
