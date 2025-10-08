// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.PrivateUserSettings.PrivateUserSettingsComponent
// Assembly: PM.UserProfileManagement.PrivateUserSettings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E00D5CAE-3392-4F44-903A-23A515F3DC92
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.PrivateUserSettings.dll

using PathMedical.UserInterface;
using PathMedical.UserProfileManagement.PrivateUserSettings.Properties;
using PathMedical.UserProfileManagement.PrivateUserSettings.Viewer;
using System;

#nullable disable
namespace PathMedical.UserProfileManagement.PrivateUserSettings;

public class PrivateUserSettingsComponent : ApplicationComponentBase
{
  public static PrivateUserSettingsComponent Instance
  {
    get => PathMedical.Singleton.Singleton<PrivateUserSettingsComponent>.Instance;
  }

  private PrivateUserSettingsComponent()
  {
    this.Name = Resources.PrivateUserSettingsComponent_ComponentName;
    this.Description = Resources.PrivateUserSettingsComponent_ComponentDescription;
    this.Fingerprint = new Guid("5EE518CA-0897-48b0-8A05-8577B1F5882E");
    this.ActiveModuleType = typeof (PrivateUserSettingsComponentModule);
  }
}
