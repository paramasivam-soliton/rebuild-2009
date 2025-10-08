// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.UserBrowser.UserBrowserComponentModule
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement.Viewer.WindowsForms.Properties;
using System;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms.UserBrowser;

public class UserBrowserComponentModule : 
  ApplicationComponentModuleBase<UserManager, UserAccountMasterDetailBrowser>
{
  public UserBrowserComponentModule()
  {
    this.Id = new Guid("BE9DAD8D-C74F-444c-80DB-73A52DC718E3");
    this.Name = Resources.UserBrowserComponentModule_ModuleName;
    this.Description = Resources.UserBrowserComponentModule_ModuleDescription;
    this.Controller = (IController<UserManager, UserAccountMasterDetailBrowser>) new UserBrowserController((IApplicationComponentModule) this);
  }
}
