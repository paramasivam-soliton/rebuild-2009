// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Communicator.WindowsForms.ConfigurationSynchronization.ConfigurationSynchronizationComponentModule
// Assembly: PM.Type1077.Communicator.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4173E4B7-BF2C-4E03-A869-8E0498B48F2A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.Communicator.WindowsForms.dll

using PathMedical.ResourceManager;
using PathMedical.Type1077.Communicator.WindowsForms.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.Type1077.Communicator.WindowsForms.ConfigurationSynchronization;

public class ConfigurationSynchronizationComponentModule : 
  ApplicationComponentModuleBase<Type1077Manager, ConfigurationSynchronizationAssistant>
{
  public ConfigurationSynchronizationComponentModule()
  {
    this.Id = new Guid("5CDB76C7-1F3C-4cd3-BB67-62C085167EDB");
    this.Name = Resources.ConfigurationSynchronizationComponentModule_ModuleName;
    this.ShortcutName = Resources.ConfigurationSynchronizationComponentModule_ShortcutName;
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_InstrumentSync") as Bitmap);
    this.Controller = (IController<Type1077Manager, ConfigurationSynchronizationAssistant>) new ConfigurationSynchronizationController((IApplicationComponentModule) this);
  }
}
