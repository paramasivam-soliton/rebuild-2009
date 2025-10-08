// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.RelationStoreInfo
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Diagnostics;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

[DebuggerDisplay("RelationStoreInfo for {EntityStoreInfo.Entity} - {RelatedEntityStoreInfo.Entity}")]
public class RelationStoreInfo
{
  private readonly object relatedEntityFromDbId;

  public RelationStoreInfo(
    LoadEntityOption loadEntityOption,
    EntityStoreInfo entityStoreInfo,
    object relatedEntity,
    object relatedEntityFromDb)
  {
    this.LoadEntityOption = loadEntityOption;
    this.EntityStoreInfo = entityStoreInfo;
    this.EntityStoreInfo.RelationStoreInfos.Add(this);
    this.RelatedEntityStoreInfo = new EntityStoreInfo(relatedEntity, relatedEntityFromDb, this);
    if (relatedEntity != null)
      this.RelationValue = relatedEntityFromDb == null ? (loadEntityOption.RelationJoinInfo.JoinConditionValue == null ? loadEntityOption.RelationHelper.GetRelationValue(relatedEntity) : loadEntityOption.RelationJoinInfo.JoinConditionValue) : loadEntityOption.RelationHelper.GetRelationValue(relatedEntity);
    if (relatedEntityFromDb != null)
    {
      this.RelationValueFromDb = loadEntityOption.RelationHelper.GetRelationValue(relatedEntityFromDb);
      this.relatedEntityFromDbId = EntityHelper.GetPrimaryKey(relatedEntityFromDb);
    }
    if (relatedEntityFromDb == null)
      this.RelationChangeType = RelationChangeType.Added;
    else if (relatedEntity == null)
      this.RelationChangeType = RelationChangeType.Removed;
    else
      this.RelationChangeType = object.Equals(this.RelationValue, this.RelationValueFromDb) ? RelationChangeType.None : RelationChangeType.ValueModified;
  }

  public EntityStoreInfo EntityStoreInfo { get; private set; }

  public EntityStoreInfo RelatedEntityStoreInfo { get; private set; }

  public object RelatedEntityId
  {
    get
    {
      return this.RelatedEntityStoreInfo != null ? this.RelatedEntityStoreInfo.Id : this.relatedEntityFromDbId;
    }
  }

  public LoadEntityOption LoadEntityOption { get; private set; }

  public RelationChangeType RelationChangeType { get; private set; }

  public object RelationValue { get; private set; }

  public object RelationValueFromDb { get; private set; }
}
