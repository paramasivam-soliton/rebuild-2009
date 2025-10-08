// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.FirmwareUpdateAssistant.FirmwareUpdateAssistantComponentModule
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
namespace PathMedical.ServiceTools.WindowsForms.FirmwareUpdateAssistant;

public class FirmwareUpdateAssistantComponentModule : 
  ApplicationComponentModuleBase<Type1077Manager, PathMedical.ServiceTools.WindowsForms.FirmwareUpdateAssistant.FirmwareUpdateAssistant>
{
  public static Guid FirmwareUpdateAssistantComponentModuleId = new Guid("9ECFEA99-9CEB-4A17-A122-480342E2BE8F");

  public FirmwareUpdateAssistantComponentModule()
  {
    this.Id = new Guid("9ECFEA99-9CEB-4A17-A122-480342E2BE8F");
    this.Name = "Firmware Update";
    this.ShortcutName = "Firmware Update";
    this.Controller = (IController<Type1077Manager, PathMedical.ServiceTools.WindowsForms.FirmwareUpdateAssistant.FirmwareUpdateAssistant>) new FirmwareUpdateAssistantController((IApplicationComponentModule) this);
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_UpdateFirmware") as Bitmap);
  }
}
