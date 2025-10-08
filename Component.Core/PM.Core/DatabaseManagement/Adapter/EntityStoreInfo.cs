// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.EntityStoreInfo
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

[DebuggerDisplay("EntityStoreInfo for {Entity}")]
public class EntityStoreInfo
{
  public EntityStoreInfo(object entity, object entityFromDb)
  {
    this.Entity = entity;
    this.EntityFromDb = entityFromDb;
    this.IsNew = EntityHelper.IsNewEntity(entity ?? entityFromDb);
    int num = EntityHelper.HasPrimaryKeyAssigned(entity ?? entityFromDb) ? 1 : 0;
    object primaryKey = EntityHelper.GetPrimaryKey(entity ?? entityFromDb);
    if (num != 0 || 0.Equals(primaryKey))
      this.Id = primaryKey;
    else if (EntityHelper.For(entity.GetType()).PrimaryKeyType == typeof (Guid))
      this.Id = (object) Guid.NewGuid();
    this.IsChanged = this.EntityFromDb != null && this.Entity != null && EntityHelper.HasPropertyModifications(this.Entity, this.EntityFromDb);
    this.RelationStoreInfos = (ICollection<RelationStoreInfo>) new List<RelationStoreInfo>();
  }

  public EntityStoreInfo(
    object entity,
    object entityFromDb,
    RelationStoreInfo parentRelationStoreInfo)
    : this(entity, entityFromDb)
  {
    this.ParentRelationStoreInfo = parentRelationStoreInfo;
  }

  public object Id { get; internal set; }

  public bool IsNew { get; private set; }

  public bool IsChanged { get; private set; }

  public object Entity { get; private set; }

  public object EntityFromDb { get; private set; }

  public RelationStoreInfo ParentRelationStoreInfo { get; private set; }

  public ICollection<RelationStoreInfo> RelationStoreInfos { get; private set; }
}
