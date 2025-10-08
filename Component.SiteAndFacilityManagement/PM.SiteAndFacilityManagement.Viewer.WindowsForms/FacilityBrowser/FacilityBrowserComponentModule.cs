// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.FacilityBrowser.FacilityBrowserComponentModule
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.FacilityBrowser;

public class FacilityBrowserComponentModule : 
  ApplicationComponentModuleBase<FacilityManager, FacilityMasterDetailBrowser>
{
  public FacilityBrowserComponentModule()
  {
    this.Id = new Guid("EEEBC22D-F978-4d7f-B7FA-F3A71999A8FD");
    this.Name = Resources.FacilityBrowserComponentModule_ModuleName;
    this.Description = Resources.FacilityBrowserComponentModule_ModuleDescription;
    this.Controller = (IController<FacilityManager, FacilityMasterDetailBrowser>) new FacilityBrowserController((IApplicationComponentModule) this);
  }
}
