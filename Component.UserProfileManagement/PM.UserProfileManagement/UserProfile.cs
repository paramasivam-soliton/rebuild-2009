// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.UserProfile
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.UserProfileManagement;

[DataExchangeRecord]
public class UserProfile
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Name { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Description { get; set; }

  [DbIntermediateTableRelation("ProfileID", "AccessPermissionID", "ProfileAccessPermissionAssociation")]
  [DataExchangeColumn]
  public List<AccessPermission> ProfileAccessPermissions { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  [DataExchangeColumn(IsReadOnly = true)]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  [DataExchangeColumn(IsReadOnly = true)]
  public DateTime Modified { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is UserProfile userProfile && this.Id.Equals(userProfile.Id);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();

  public override string ToString() => this.Name;
}
