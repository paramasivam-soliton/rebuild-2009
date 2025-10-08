// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.LocationTypeBrowser.LocationTypeBrowserComponentModule
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.LocationTypeBrowser;

public class LocationTypeBrowserComponentModule : 
  ApplicationComponentModuleBase<LocationTypeManager, LocationTypeMasterDetailBrowser>
{
  public LocationTypeBrowserComponentModule()
  {
    this.Id = new Guid("E5F7936B-F61E-41bd-90AE-B66F0207CC92");
    this.Name = Resources.LocationTypeBrowserComponentModule_ModuleName;
    this.Description = Resources.LocationTypeBrowserComponentModule_ModuleDescription;
    this.Controller = (IController<LocationTypeManager, LocationTypeMasterDetailBrowser>) new LocationTypeBrowserController((IApplicationComponentModule) this);
  }
}
