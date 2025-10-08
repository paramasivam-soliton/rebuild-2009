// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.PrivateUserSettings.Viewer.PrivateUserSettingsComponentModule
// Assembly: PM.UserProfileManagement.PrivateUserSettings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E00D5CAE-3392-4F44-903A-23A515F3DC92
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.PrivateUserSettings.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement.PrivateUserSettings.Properties;
using System;

#nullable disable
namespace PathMedical.UserProfileManagement.PrivateUserSettings.Viewer;

public class PrivateUserSettingsComponentModule : 
  ApplicationComponentModuleBase<UserManager, PathMedical.UserProfileManagement.PrivateUserSettings.Viewer.PrivateUserSettings>
{
  public PrivateUserSettingsComponentModule()
  {
    this.Id = new Guid("22AC403E-A423-4fe0-8589-D59F9223DDCF");
    this.Name = Resources.PrivateUserSettingsComponentModule_ModuleName;
    this.Description = Resources.PrivateUserSettingsComponentModule_ModuleDescription;
    this.Controller = (IController<UserManager, PathMedical.UserProfileManagement.PrivateUserSettings.Viewer.PrivateUserSettings>) new PrivateUserSettingsController((IApplicationComponentModule) this);
  }
}
