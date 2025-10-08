// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.FirmwareUploadAssistant.FirmwareUploadAssistantComponentModule
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
namespace PathMedical.ServiceTools.WindowsForms.FirmwareUploadAssistant;

public class FirmwareUploadAssistantComponentModule : 
  ApplicationComponentModuleBase<Type1077Manager, PathMedical.ServiceTools.WindowsForms.FirmwareUploadAssistant.FirmwareUploadAssistant>
{
  public static Guid FirmwareUploadAssistantComponentModuleId = new Guid("6B4A84D5-549E-49FF-9C3A-644AD7074333");

  public FirmwareUploadAssistantComponentModule()
  {
    this.Id = new Guid("6B4A84D5-549E-49FF-9C3A-644AD7074333");
    this.Name = "Firmware Upload";
    this.ShortcutName = "Firmware Upload";
    this.Controller = (IController<Type1077Manager, PathMedical.ServiceTools.WindowsForms.FirmwareUploadAssistant.FirmwareUploadAssistant>) new FirmwareUploadAssistantController((IApplicationComponentModule) this);
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_UploadFirmware") as Bitmap);
  }
}
