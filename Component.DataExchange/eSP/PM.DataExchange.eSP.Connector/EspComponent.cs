// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Connector.WindowsForms.EspComponent
// Assembly: PM.DataExchange.eSP.Connector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1934FEB7-E9C0-480F-B9F2-DEDB47DB36DA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.Connector.dll

using PathMedical.DataExchange.eSP.Connector.WindowsForms.Configuration;
using PathMedical.DataExchange.eSP.Connector.WindowsForms.ConfigurationImport;
using PathMedical.DataExchange.eSP.Connector.WindowsForms.Export;
using PathMedical.DataExchange.eSP.Service;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Plugin;
using PathMedical.UserInterface;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.DataExchange.eSP.Connector.WindowsForms;

public class EspComponent : ISupportPluginDataExchange, IPlugin, ISupportDataExchangeModules
{
  public static EspComponent Instance => PathMedical.Singleton.Singleton<EspComponent>.Instance;

  private EspComponent()
  {
    this.Name = "eSP";
    this.Description = "Connector to exchange data with eSP";
    this.Fingerprint = new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554");
    this.RecordDescriptionSets = (List<RecordDescriptionSet>) null;
    this.RecordSetMaps = (List<DataExchangeSetMap>) null;
    this.Types = new List<Type>()
    {
      typeof (UserDetails),
      typeof (RiskFactor)
    };
    this.ExportModule = (IApplicationComponentModule) new EspExportComponentModule();
    this.ImportModule = (IApplicationComponentModule) null;
    this.ConfigurationExportModule = (IApplicationComponentModule) null;
    this.ConfigurationImportModule = (IApplicationComponentModule) new EspConfigurationImportComponentModule();
    this.ConfigurationModule = (IApplicationComponentModule) new EspConfigurationComponentModule();
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
