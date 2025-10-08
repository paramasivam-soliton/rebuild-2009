// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.UserProfileManagementConfiguration
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using System;
using System.Linq;

#nullable disable
namespace PathMedical.UserProfileManagement;

public class UserProfileManagementConfiguration
{
  public bool IsUserManagementActive { get; set; }

  public Guid? DisabledUserManagementProfileId { get; set; }

  public UserProfile DisabledUserManagementProfile
  {
    get
    {
      Guid? guid = this.DisabledUserManagementProfileId;
      return !guid.HasValue ? (UserProfile) null : UserProfileManager.Instance.Profiles.FirstOrDefault<UserProfile>((Func<UserProfile, bool>) (p =>
      {
        Guid id = p.Id;
        Guid? nullable = guid;
        return nullable.HasValue && id == nullable.GetValueOrDefault();
      }));
    }
    set => this.DisabledUserManagementProfileId = value?.Id;
  }

  public string DisabledUserManagementAdminPassword { get; set; }

  public int UserAccountLockingTime { get; set; }
}
