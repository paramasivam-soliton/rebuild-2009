// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.UserProfileManagementComponent
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserProfileManagement.Viewer.WindowsForms.Configuration;
using PathMedical.UserProfileManagement.Viewer.WindowsForms.UserBrowser;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms;

public class UserProfileManagementComponent : 
  ApplicationComponentBase,
  ISupportPluginDataExchange,
  IPlugin
{
  public static UserProfileManagementComponent Instance
  {
    get => PathMedical.Singleton.Singleton<UserProfileManagementComponent>.Instance;
  }

  private UserProfileManagementComponent()
  {
    this.Name = ComponentResourceManagementBase<PathMedical.UserProfileManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetString("UserProfileManagementComponent_ComponentName");
    this.Description = PathMedical.UserProfileManagement.Viewer.WindowsForms.Properties.Resources.UserProfileManagementComponent_ComponentDescription;
    this.Fingerprint = new Guid("0135E402-053D-4696-B68B-F574A1096246");
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<PathMedical.UserProfileManagement.Properties.Resources>.Instance.ResourceManager);
    this.RecordDescriptionSets = UserProfileManager.Instance.RecordDescriptionSets;
    this.RecordSetMaps = UserProfileManager.Instance.RecordSetMaps;
    this.Types = new List<Type>() { typeof (User) };
    this.ActiveModuleType = typeof (UserBrowserComponentModule);
    this.ConfigurationModuleTypes = new Type[1]
    {
      typeof (ConfigurationComponentModule)
    };
  }

  public List<DataExchangeSetMap> RecordSetMaps { get; protected set; }

  public List<RecordDescriptionSet> RecordDescriptionSets { get; protected set; }

  public List<Type> Types { get; protected set; }

  public void Import(
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets)
  {
  }

  public object Export(ExportType exportType, Guid testDetailId) => (object) null;

  public int StorePriority => 200;

  public DataExchangeTokenSet GetDataExchangeTokenSet(object id) => (DataExchangeTokenSet) null;

  public List<DataExchangeTokenSet> GetDataExchangeTokenSets(
    object id,
    params object[] additionalIds)
  {
    return (List<DataExchangeTokenSet>) null;
  }
}
