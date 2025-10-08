// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.AccuLink.WindowsForms.AccuLinkExchangeComponent
// Assembly: PM.DataExchange.AccuLink.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7B9FC5AD-6EE7-4FA1-8083-412FCFB9EB4F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.AccuLink.WindowsForms.dll

using PathMedical.DataExchange.AccuLink.WindowsForms.Configuration;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Plugin;
using PathMedical.UserInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.DataExchange.AccuLink.WindowsForms;

public class AccuLinkExchangeComponent : 
  ISupportPluginDataExchange,
  IPlugin,
  ISupportDataExchangeModules
{
  public static AccuLinkExchangeComponent Instance => PathMedical.Singleton.Singleton<AccuLinkExchangeComponent>.Instance;

  private AccuLinkExchangeComponent()
  {
    this.Name = "AccuLink";
    this.Description = "AccuLink Data Exchange";
    this.Fingerprint = new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205");
    this.RecordDescriptionSets = (List<RecordDescriptionSet>) null;
    this.RecordSetMaps = (List<DataExchangeSetMap>) null;
    this.Types = new List<Type>();
    this.ExportModule = (IApplicationComponentModule) null;
    this.ImportModule = (IApplicationComponentModule) null;
    this.ConfigurationExportModule = (IApplicationComponentModule) null;
    this.ConfigurationImportModule = (IApplicationComponentModule) null;
    this.ConfigurationModule = (IApplicationComponentModule) new AccuLinkExchangeConfigurationComponentModule();
  }

  public string Name { get; protected set; }

  public string Description { get; protected set; }

  public Guid Fingerprint { get; protected set; }

  public int LoadPriority { get; protected set; }

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

  public IApplicationComponentModule ImportModule { get; private set; }

  public IApplicationComponentModule ExportModule { get; private set; }

  public IApplicationComponentModule ConfigurationImportModule { get; private set; }

  public IApplicationComponentModule ConfigurationExportModule { get; private set; }

  public IApplicationComponentModule ConfigurationModule { get; private set; }
}
