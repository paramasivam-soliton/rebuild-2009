// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Communicator.WindowsForms.Type1077CommunicatorComponent
// Assembly: PM.Type1077.Communicator.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4173E4B7-BF2C-4E03-A869-8E0498B48F2A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.Communicator.WindowsForms.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.InstrumentManagement;
using PathMedical.Plugin;
using PathMedical.Type1077.Communicator.WindowsForms.ConfigurationSynchronization;
using PathMedical.Type1077.Communicator.WindowsForms.DataDownload;
using PathMedical.Type1077.Communicator.WindowsForms.DataUpload;
using PathMedical.Type1077.Communicator.WindowsForms.FirmwareImageImport;
using PathMedical.Type1077.Communicator.WindowsForms.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.Type1077.Communicator.WindowsForms;

public class Type1077CommunicatorComponent : IInstrumentPlugin, IPlugin, ISupportPluginDataExchange
{
  public static Type1077CommunicatorComponent Instance
  {
    get => PathMedical.Singleton.Singleton<Type1077CommunicatorComponent>.Instance;
  }

  private Type1077CommunicatorComponent()
  {
    if (Application.ProductName.Equals("Mira"))
    {
      this.Name = "Sentiero";
      this.Description = "Application to communicate with Sentiero";
    }
    else
    {
      this.Name = Resources.Type1077CommunicatorComponent_ComponentName;
      this.Description = Resources.Type1077CommunicatorComponent_ComponentDescription;
    }
    this.Fingerprint = new Guid("9BCBB607-BD5C-497c-9E7E-FBF4D3E9A763");
    this.DataDownloadModuleType = typeof (DataDownloadComponentModule);
    this.DataUploadModuleType = typeof (DataUploadComponentModule);
    this.FirmwareSearchModuleType = typeof (FirmwareImportAssistantComponentModule);
    this.ConfigurationSynchronizationModuleType = !Application.ProductName.Equals("Mira") ? typeof (ConfigurationSynchronizationComponentModule) : (Type) null;
    this.Types = new List<Type>()
    {
      typeof (Type1077Instrument),
      typeof (Type1077ProbeInformation),
      typeof (Type1077LoopBackCableInformation)
    };
    this.RecordDescriptionSets = Type1077Manager.Instance.RecordDescriptionSets;
    this.RecordSetMaps = Type1077Manager.Instance.RecordSetMaps;
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

  public Type DataDownloadModuleType { get; protected set; }

  public Type DataUploadModuleType { get; protected set; }

  public Type ConfigurationSynchronizationModuleType { get; protected set; }

  public Type FirmwareSearchModuleType { get; protected set; }
}
