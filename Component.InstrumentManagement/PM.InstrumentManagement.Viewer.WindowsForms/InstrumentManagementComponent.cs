// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.Viewer.WindowsForms.InstrumentManagementComponent
// Assembly: PM.InstrumentManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 377C3581-C34D-4673-97B7-CC091DEDB55A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Viewer.WindowsForms.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.InstrumentManagement.Viewer.WindowsForms.InstrumentBrowser;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.InstrumentManagement.Viewer.WindowsForms;

public class InstrumentManagementComponent : 
  ApplicationComponentBase,
  ISupportPluginDataExchange,
  IPlugin
{
  public static InstrumentManagementComponent Instance
  {
    get => PathMedical.Singleton.Singleton<InstrumentManagementComponent>.Instance;
  }

  private InstrumentManagementComponent()
  {
    this.Name = PathMedical.InstrumentManagement.Viewer.WindowsForms.Properties.Resources.InstrumentManagementComponent_ComponentName;
    this.Description = PathMedical.InstrumentManagement.Viewer.WindowsForms.Properties.Resources.InstrumentManagementComponent_ComponentDescription;
    this.Fingerprint = new Guid("A4465EA9-02B7-4b12-A530-7946D4813552");
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<PathMedical.InstrumentManagement.Properties.Resources>.Instance.ResourceManager);
    this.RecordDescriptionSets = InstrumentManager.Instance.RecordDescriptionSets;
    this.RecordSetMaps = InstrumentManager.Instance.RecordSetMaps;
    this.Types = new List<Type>() { typeof (Instrument) };
    this.ActiveModuleType = typeof (InstrumentBrowserComponentModule);
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
