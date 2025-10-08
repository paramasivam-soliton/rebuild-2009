// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.InstrumentData.InstrumentDataManager
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.InstrumentManagement.Properties;
using PathMedical.Logging;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PathMedical.InstrumentManagement.InstrumentData;

public class InstrumentDataManager : ISingleSelectionModel<Instrument>, IModel
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (InstrumentDataManager), "$Rev: 1448 $");
  private List<IInstrument> connectedInstruments;
  private RecordDescriptionSet recordDescriptionSet;

  public static InstrumentDataManager Instance => PathMedical.Singleton.Singleton<InstrumentDataManager>.Instance;

  private Instrument SelectedInstrument { get; set; }

  public event EventHandler<OperationStateEventArgs> OperationStateEventHander;

  private InstrumentDataManager()
  {
  }

  public RecordDescriptionSet RecordDescriptionSet
  {
    get
    {
      if (this.recordDescriptionSet == null)
        this.recordDescriptionSet = RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.InstrumentManagement.DataExchange.InstrumentDataExchangeSet.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.InstrumentDataManager_StructureFileNotFound));
      return this.recordDescriptionSet;
    }
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void RefreshData()
  {
  }

  public void ChangeSingleSelection(Instrument selection) => this.SelectedInstrument = selection;

  public bool IsOneItemSelected<T>() where T : Instrument => this.SelectedInstrument != null;

  bool ISingleSelectionModel<Instrument>.IsOneItemAvailable<T>()
  {
    return this.connectedInstruments != null && this.connectedInstruments.Count > 0;
  }

  internal void SearchInstruments(ISupportInstrumentDataExchange instrumentService)
  {
    if (this.connectedInstruments == null)
      this.connectedInstruments = new List<IInstrument>();
    else
      this.connectedInstruments.Clear();
    instrumentService.InstrumentConnectedEventHander += (EventHandler<InstrumentAvailableEventArgs>) ((e, eventArgs) =>
    {
      if (e == null || this.connectedInstruments.Contains(eventArgs.Instrument))
        return;
      this.connectedInstruments.Add(eventArgs.Instrument);
      InstrumentManager.Instance.RegisterInstrumentConnection(eventArgs.Instrument as Instrument);
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<List<IInstrument>>(this.connectedInstruments, ChangeType.ItemAdded));
    });
    instrumentService.EvaluateConnectedInstruments();
  }

  private void OnOperationStateChanged(object sender, OperationStateEventArgs e)
  {
  }

  internal void StartDataDownload()
  {
  }

  internal void StoreData(List<ISupportDataExchangeToken> data)
  {
  }

  internal void SetClock()
  {
  }

  internal void UpdateUsers()
  {
  }

  internal void UpdateProfiles()
  {
  }

  internal void UpdateSiteAndFacilities()
  {
  }

  internal void UpdateLocations()
  {
  }

  internal void UpdateRiskIndicators()
  {
  }

  internal void UpdateConfiguration()
  {
  }

  internal void EraseFlash()
  {
  }

  internal void UploadFlash()
  {
    Instrument selectedInstrument = this.SelectedInstrument;
  }

  internal void UploadFirmware()
  {
  }

  internal void RebootInstrument()
  {
  }

  internal void BeginConfiguration()
  {
  }

  internal void CommitConfiguration()
  {
  }
}
