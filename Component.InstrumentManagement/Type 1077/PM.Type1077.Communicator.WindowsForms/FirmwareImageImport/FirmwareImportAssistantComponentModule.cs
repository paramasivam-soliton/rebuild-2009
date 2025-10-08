// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Communicator.WindowsForms.FirmwareImageImport.FirmwareImportAssistantComponentModule
// Assembly: PM.Type1077.Communicator.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4173E4B7-BF2C-4E03-A869-8E0498B48F2A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.Communicator.WindowsForms.dll

using PathMedical.ResourceManager;
using PathMedical.Type1077.Communicator.WindowsForms.Properties;
using PathMedical.Type1077.Firmware;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.Type1077.Communicator.WindowsForms.FirmwareImageImport;

public class FirmwareImportAssistantComponentModule : 
  ApplicationComponentModuleBase<FirmwareManager, FirmwareImportAssistant>
{
  public FirmwareImportAssistantComponentModule()
  {
    this.Id = new Guid("D3E2DACB-8130-4c0e-A42D-BCB59C8FA55A");
    this.Name = Resources.FirmwareImportAssistantComponentModule_ModuleName;
    this.ShortcutName = Resources.FirmwareImportAssistantComponentModule_ShortcutName;
    this.Controller = (IController<FirmwareManager, FirmwareImportAssistant>) new FirmwareImportController((IApplicationComponentModule) this);
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_SearchFirmware") as Bitmap);
  }
}
