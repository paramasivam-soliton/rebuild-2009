// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Attributes.DbRelationAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.DatabaseManagement.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public abstract class DbRelationAttribute : Attribute
{
  public string OriginKey { get; protected set; }

  public string DestinationKey { get; protected set; }

  public string IntermediateTable { get; protected set; }

  public string JoinConditionColumn { get; protected set; }

  public object JoinConditionValue { get; protected set; }

  public JoinType JoinType { get; protected set; }

  public bool HasHistory { get; set; }

  public string InsertTimestampColumn { get; set; }

  public string UpdateTimestampColumn { get; set; }

  public string InsertUpdateTimestampColumn { get; set; }
}
