// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.EspConfiguration
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using PathMedical.UserProfileManagement;
using System;
using System.Linq;

#nullable disable
namespace PathMedical.DataExchange.eSP;

public class EspConfiguration
{
  public string BackupFolder { get; set; }

  public string HomeSite { get; set; }

  public string EspRemoteAddress { get; set; }

  public UserProfile DefaultUserProfile { get; set; }

  public Guid DefaultUserProfileId
  {
    get => this.DefaultUserProfile != null ? this.DefaultUserProfile.Id : Guid.Empty;
    set
    {
      this.DefaultUserProfile = UserProfileManager.Instance.Profiles.FirstOrDefault<UserProfile>((Func<UserProfile, bool>) (p => p.Id == value));
    }
  }

  public string DefaultUserPassword { get; set; }
}
