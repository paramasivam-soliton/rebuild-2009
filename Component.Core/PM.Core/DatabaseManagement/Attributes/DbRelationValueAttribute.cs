// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Attributes.DbRelationValueAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.DatabaseManagement.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class DbRelationValueAttribute : Attribute
{
  public DbRelationValueAttribute(string columnName) => this.ColumnName = columnName;

  public string ColumnName { get; protected set; }
}
