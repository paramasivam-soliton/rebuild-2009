// Decompiled with JetBrains decompiler
// Type: PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser.ComponentBrowserComponentModule
// Assembly: PM.SystemConfiguration.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0D6114BC-3807-4057-97EB-FB0AA393F8AD
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SystemConfiguration.Viewer.WindowsForms.dll

using PathMedical.SystemConfiguration.Core;
using PathMedical.SystemConfiguration.Viewer.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser;

public class ComponentBrowserComponentModule : 
  ApplicationComponentModuleBase<ComponentBrowserManager, PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser.ComponentBrowser>
{
  public ComponentBrowserComponentModule()
  {
    this.Id = new Guid("C6C748A4-432E-4984-8C70-5B43FC91C1EB");
    this.Name = Resources.ComponentBrowserComponentModule_ModuleName;
    this.Controller = (IController<ComponentBrowserManager, PathMedical.SystemConfiguration.Viewer.WindowsForms.ComponentBrowser.ComponentBrowser>) new ComponentBrowserController((IApplicationComponentModule) this);
  }
}
