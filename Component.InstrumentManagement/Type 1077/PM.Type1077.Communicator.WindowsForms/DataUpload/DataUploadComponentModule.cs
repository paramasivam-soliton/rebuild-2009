// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Communicator.WindowsForms.DataUpload.DataUploadComponentModule
// Assembly: PM.Type1077.Communicator.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4173E4B7-BF2C-4E03-A869-8E0498B48F2A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.Communicator.WindowsForms.dll

using PathMedical.ResourceManager;
using PathMedical.Type1077.Communicator.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.Type1077.Communicator.WindowsForms.DataUpload;

public class DataUploadComponentModule : 
  ApplicationComponentModuleBase<Type1077Manager, UploadAssistant>
{
  public DataUploadComponentModule()
  {
    this.Id = new Guid("8EEE2D19-1FC4-455d-A5A1-6723DEB2B0E9");
    this.Name = Resources.DataUploadComponentModule_ModuleName;
    this.ShortcutName = Resources.DataUploadComponentModule_ShortcutName;
    this.Controller = (IController<Type1077Manager, UploadAssistant>) new DataUploadController((IApplicationComponentModule) this);
    if (Application.ProductName.Equals("Mira"))
      this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("SentieroUpload") as Bitmap);
    else
      this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_AccuScreenUpload") as Bitmap);
  }
}
