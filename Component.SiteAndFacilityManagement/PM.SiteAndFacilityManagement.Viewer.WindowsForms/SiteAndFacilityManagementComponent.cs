// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteAndFacilityManagementComponent
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Configuration;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteBrowser;
using PathMedical.UserInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms;

public class SiteAndFacilityManagementComponent : 
  ApplicationComponentBase,
  ISupportPluginDataExchange,
  IPlugin
{
  public static SiteAndFacilityManagementComponent Instance
  {
    get => PathMedical.Singleton.Singleton<SiteAndFacilityManagementComponent>.Instance;
  }

  private SiteAndFacilityManagementComponent()
  {
    this.Name = PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Properties.Resources.SiteAndFacilityManagementComponent_ComponentName;
    this.Description = PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Properties.Resources.SiteAndFacilityManagementComponent_ComponentDescription;
    this.Fingerprint = new Guid("3C09F61E-4C2B-4c91-A753-90BED7D9EE41");
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<PathMedical.SiteAndFacilityManagement.Properties.Resources>.Instance.ResourceManager);
    this.Types = new List<Type>()
    {
      typeof (Facility),
      typeof (Site),
      typeof (LocationType)
    };
    this.ActiveModuleType = typeof (SiteBrowserComponentModule);
    this.ConfigurationModuleTypes = new Type[1]
    {
      typeof (ConfigurationComponentModule)
    };
    this.RecordDescriptionSets = SiteManager.Instance.RecordDescriptionSets;
    this.RecordSetMaps = SiteManager.Instance.RecordSetMaps;
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
