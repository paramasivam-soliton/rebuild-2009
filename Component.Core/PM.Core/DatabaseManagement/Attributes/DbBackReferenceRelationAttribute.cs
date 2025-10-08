// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Attributes.DbBackReferenceRelationAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.DatabaseManagement.Attributes;

public class DbBackReferenceRelationAttribute : DbRelationAttribute
{
  public DbBackReferenceRelationAttribute(string referencedColumn, string referencingColumn)
  {
    this.OriginKey = referencedColumn;
    this.DestinationKey = referencingColumn;
    this.JoinType = JoinType.BackReferenceFromOtherEntity;
  }
}
