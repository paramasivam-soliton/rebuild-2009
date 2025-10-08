// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Communicator.WindowsForms.DataDownload.DataDownloadComponentModule
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
namespace PathMedical.Type1077.Communicator.WindowsForms.DataDownload;

public class DataDownloadComponentModule : 
  ApplicationComponentModuleBase<Type1077Manager, DownloadAssistant>
{
  public DataDownloadComponentModule()
  {
    this.Id = new Guid("C37D8A98-C7FF-4ef1-8187-FC487AC8904B");
    this.Name = Resources.DataDownloadComponentModule_ModuleName;
    this.ShortcutName = Resources.DataDownloadComponentModule_ModuleShortcutName;
    if (Application.ProductName.Equals("Mira"))
      this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("SentieroDownload") as Bitmap);
    else
      this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_AccuScreenDownload") as Bitmap);
    this.Controller = (IController<Type1077Manager, DownloadAssistant>) new DataDownloadController((IApplicationComponentModule) this);
  }
}
