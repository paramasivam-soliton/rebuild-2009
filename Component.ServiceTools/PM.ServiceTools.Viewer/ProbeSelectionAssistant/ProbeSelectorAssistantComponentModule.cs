// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.ProbeSelectionAssistant.ProbeSelectorAssistantComponentModule
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using PathMedical.ResourceManager;
using PathMedical.ServiceTools.WindowsForms.Properties;
using PathMedical.Type1077;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.ProbeSelectionAssistant;

internal class ProbeSelectorAssistantComponentModule : 
  ApplicationComponentModuleBase<Type1077Manager, ProbeSelectorAssistant>
{
  public static Guid ProbeSelectorComponentModuleId = new Guid("242C66DC-8730-4014-89D9-62C4C1048A43");

  public ProbeSelectorAssistantComponentModule()
  {
    this.Id = new Guid("242C66DC-8730-4014-89D9-62C4C1048A43");
    this.Name = "Get Probe Data";
    this.ShortcutName = "Get Probe Data";
    this.Controller = (IController<Type1077Manager, ProbeSelectorAssistant>) new ProbeSelectorController((IApplicationComponentModule) this);
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_SelectProbe") as Bitmap);
  }
}
