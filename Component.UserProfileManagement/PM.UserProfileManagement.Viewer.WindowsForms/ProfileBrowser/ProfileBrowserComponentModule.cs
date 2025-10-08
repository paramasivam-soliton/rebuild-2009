// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.ProfileBrowser.ProfileBrowserComponentModule
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement.Viewer.WindowsForms.Properties;
using System;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms.ProfileBrowser;

public class ProfileBrowserComponentModule : 
  ApplicationComponentModuleBase<UserProfileManager, ProfileMasterDetailBrowser>
{
  public ProfileBrowserComponentModule()
  {
    this.Id = new Guid("4AEFEA27-E80B-46e9-B881-E9418A9CD022");
    this.Name = Resources.ProfileBrowserComponentModule_ModuleName;
    this.Description = Resources.ProfileBrowserComponentModule_ModuleDescription;
    this.Controller = (IController<UserProfileManager, ProfileMasterDetailBrowser>) new ProfileBrowserController((IApplicationComponentModule) this);
  }
}
