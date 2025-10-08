// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.UserRightsManager
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.Login;
using PathMedical.Permission;
using PathMedical.Plugin;
using PathMedical.UserProfileManagement.Properties;
using System;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PathMedical.UserProfileManagement;

public class UserRightsManager : IPermissionManager, IPlugin
{
  public static UserRightsManager Instance => PathMedical.Singleton.Singleton<UserRightsManager>.Instance;

  private UserRightsManager()
  {
    this.Fingerprint = new Guid("1EFCE113-5D0B-4cfe-AFE8-BE65C8D5C9E8");
    this.Name = Resources.UserRightsManager_ModuleName;
    this.Description = Resources.UserRightsManager_ModuleDescription;
  }

  [Localizable(true)]
  public string Name { get; private set; }

  [Localizable(true)]
  public string Description { get; private set; }

  public Guid Fingerprint { get; private set; }

  public int LoadPriority { get; protected set; }

  public bool HasPermission(Guid accessPermissionId)
  {
    bool flag = false;
    User user = (User) null;
    if (LoginManager.Instance.LoggedInUserData != null)
      user = LoginManager.Instance.LoggedInUserData.Entity as User;
    if (user != null && user.Profile != null && user.Profile.ProfileAccessPermissions != null)
    {
      AccessPermission accessPermission = user.Profile.ProfileAccessPermissions.Where<AccessPermission>((Func<AccessPermission, bool>) (pap => pap.Id == accessPermissionId)).FirstOrDefault<AccessPermission>();
      if (accessPermission != null)
        flag = accessPermission.AccessPermissionFlag;
    }
    return flag;
  }
}
