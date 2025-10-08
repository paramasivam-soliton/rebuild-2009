// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteBrowser.SiteBrowserComponentModule
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteBrowser;

public class SiteBrowserComponentModule : 
  ApplicationComponentModuleBase<SiteManager, SiteMasterDetailBrowser>
{
  public SiteBrowserComponentModule()
  {
    this.Id = new Guid("6395D488-9A6D-4f3c-A5DD-B21030C3A896");
    this.Name = Resources.SiteBrowserComponentModule_ModuleName;
    this.Description = Resources.SiteBrowserComponentModule_ModuleDescription;
    this.Controller = (IController<SiteManager, SiteMasterDetailBrowser>) new SiteBrowserController((IApplicationComponentModule) this);
  }
}
