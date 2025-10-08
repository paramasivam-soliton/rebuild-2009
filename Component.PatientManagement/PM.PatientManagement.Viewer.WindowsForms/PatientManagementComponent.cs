// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientManagementComponent
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.PatientManagement.Viewer.WindowsForms.Configuration;
using PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser;
using PathMedical.PatientManagement.Viewer.WindowsForms.PredefinedCommentManagement;
using PathMedical.PatientManagement.Viewer.WindowsForms.RiskFactorManagement;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms;

public class PatientManagementComponent : 
  ApplicationComponentBase,
  ISupportPluginDataExchange,
  IPlugin
{
  public static PatientManagementComponent Instance
  {
    get => PathMedical.Singleton.Singleton<PatientManagementComponent>.Instance;
  }

  private PatientManagementComponent()
  {
    this.Name = ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetString("PatientManagementComponent_ComponentName");
    this.Description = ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetString("PatientManagementComponent_ComponentDescription");
    this.Fingerprint = new Guid("0BF386E1-2DA1-423a-9D1E-658F24B753E4");
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<PathMedical.PatientManagement.Viewer.WindowsForms.Properties.Resources>.Instance.ResourceManager);
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<PathMedical.PatientManagement.Properties.Resources>.Instance.ResourceManager);
    this.ConfigurationModuleTypes = new Type[3]
    {
      typeof (RiskFactorManagementComponentModule),
      typeof (PredefinedCommentComponentModule),
      typeof (ConfigurationComponentModule)
    };
    this.ActiveModuleType = typeof (PatientBrowserComponentModule);
    this.Types = new List<Type>()
    {
      typeof (Patient),
      typeof (RiskIndicator),
      typeof (FreeTextComment),
      typeof (PredefinedCommentAssociation),
      typeof (PatientRiskIndicator)
    };
    this.RecordDescriptionSets = PatientManager.Instance.RecordDescriptionSets;
    this.RecordSetMaps = PatientManager.Instance.RecordSetMaps;
  }

  public List<DataExchangeSetMap> RecordSetMaps { get; protected set; }

  public List<RecordDescriptionSet> RecordDescriptionSets { get; protected set; }

  public List<Type> Types { get; protected set; }

  public void Import(
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets)
  {
    PatientManager.Instance.Import(dataExchangeTokenSets);
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
