// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.DeviceSelectorAsistant.DeviceSelectorAssistantComponentModule
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
namespace PathMedical.ServiceTools.WindowsForms.DeviceSelectorAsistant;

public class DeviceSelectorAssistantComponentModule : 
  ApplicationComponentModuleBase<Type1077Manager, DeviceSelectorAssistant>
{
  public static Guid DeviceSelectorComponentModuleId = new Guid("3CECF9FB-8B14-49D6-986D-6B5AC55CA621");

  public DeviceSelectorAssistantComponentModule()
  {
    this.Id = new Guid("3CECF9FB-8B14-49D6-986D-6B5AC55CA621");
    this.Name = "Select Instrument";
    this.ShortcutName = "Select Instrument";
    this.Controller = (IController<Type1077Manager, DeviceSelectorAssistant>) new DeviceSelectorController((IApplicationComponentModule) this);
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_SelectDevice") as Bitmap);
  }
}
