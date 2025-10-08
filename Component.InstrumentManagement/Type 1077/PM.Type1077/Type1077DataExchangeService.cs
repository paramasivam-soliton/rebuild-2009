// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Type1077DataExchangeService
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Communication;
using PathMedical.DataExchange;
using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Tokens;
using PathMedical.DeviceCommunication.Channel;
using PathMedical.DeviceCommunication.Command;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.Logging;
using PathMedical.PatientManagement;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement;
using PathMedical.SystemConfiguration;
using PathMedical.Type1077.Channel;
using PathMedical.Type1077.DataExchange;
using PathMedical.Type1077.DataExchange.Column;
using PathMedical.Type1077.InstrumentCommand;
using PathMedical.Type1077.Properties;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PathMedical.Type1077;

[CLSCompliant(false)]
public class Type1077DataExchangeService
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (Type1077DataExchangeService), "$Rev: 1403 $");
  private OperationStateType operationState;

  public Guid InstrumentSignature => new Guid("9BCBB607-BD5C-497c-9E7E-FBF4D3E9A763");

  public event EventHandler<OperationStateEventArgs> InstrumentEvaluationEventHander;

  public event EventHandler<InstrumentAvailableEventArgs> InstrumentConnectedEventHander;

  public event EventHandler<DataExchangeTokenSetAvailableEventArgs> DataAvailableForDataExchangeEventHandler;

  public event EventHandler<OperationStateEventArgs> OperationStateChanged;

  public OperationStateType OperationState
  {
    get => this.operationState;
    set
    {
      if (value == this.operationState)
        return;
      this.operationState = value;
      if (this.OperationStateChanged == null)
        return;
      this.OperationStateChanged((object) this, new OperationStateEventArgs(this.operationState));
    }
  }

  public Type1077DataExchangeService()
  {
    DataExchangeManager.Instance.AddRecordDescriptionSet(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.Type1077.DataExchange.Type1077DataExchangeSet.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.Type1077Manager_LoadDataExchangeDescriptions_StructureFileNotFound));
    DataExchangeManager.Instance.AddDataExchangeMaps(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.Type1077.DataExchange.Type1077DataExchangeMapping.xml"));
  }

  public void EvaluateConnectedInstruments()
  {
    foreach (string availablePortName in SerialCommunicationChannel.GetAvailablePortNames())
    {
      try
      {
        Type1077DataExchangeService.Logger.Info("Checking {0} for a connected instrument.", (object) availablePortName);
        SerialCommunicationChannel communicationChannel = new SerialCommunicationChannel(availablePortName);
        Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
        type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
        type1077CommandManager.CommandDataAvailableEventHandler += new EventHandler<ChannelDataReceivedEventArgs>(this.SingleInstrumentFoundEventHandler);
        type1077CommandManager.CommunicationChannel = (ICommunicationChannel) communicationChannel;
        type1077CommandManager.Connect();
        type1077CommandManager.Execute(InstrumentCommands.GetInstrumentInformation);
      }
      catch (CommandManagerException ex)
      {
        Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while executing an instrument search on {0}", (object) availablePortName);
      }
    }
    foreach (string availablePortName in UniversalSerialBusChannel.GetAvailablePortNames())
    {
      if (!string.IsNullOrEmpty(availablePortName))
      {
        try
        {
          Type1077DataExchangeService.Logger.Info("Checking {0} for a connected instrument.", (object) availablePortName);
          UniversalSerialBusChannel serialBusChannel = new UniversalSerialBusChannel(availablePortName);
          Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
          type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
          type1077CommandManager.CommandDataAvailableEventHandler += new EventHandler<ChannelDataReceivedEventArgs>(this.SingleInstrumentFoundEventHandler);
          type1077CommandManager.CommunicationChannel = (ICommunicationChannel) serialBusChannel;
          type1077CommandManager.Connect();
          type1077CommandManager.Execute(InstrumentCommands.GetInstrumentInformation);
        }
        catch (CommandManagerException ex)
        {
          Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while executing an instrument search on {0}", (object) availablePortName);
        }
      }
    }
    if (this.InstrumentEvaluationEventHander == null)
      return;
    this.InstrumentEvaluationEventHander((object) this, new OperationStateEventArgs(OperationStateType.Completed));
  }

  private void SingleInstrumentFoundEventHandler(object sender, ChannelDataReceivedEventArgs e)
  {
    if (!(sender is Type1077CommandManager) || e == null)
      return;
    Type1077CommandManager type1077CommandManager = sender as Type1077CommandManager;
    Type1077DataExchangeService.Logger.Debug("Received data package for command {0} which is in state {1}.", (object) type1077CommandManager.Command, (object) type1077CommandManager.State);
    List<DataExchangeTokenSet> dataExchangeTokenSets = new Type1077StreamReader((Stream) new MemoryStream(e.DataPackage.Elements.ToArray())).Read();
    List<Instrument> source = DataExchangeManager.Instance.FetchEntities<Instrument>(DataExchangeManager.Instance.GetRecordMap("Type1077", "PM-InstrumentManagement", "30464", "Instrument"), (IEnumerable<DataExchangeTokenSet>) dataExchangeTokenSets);
    foreach (Instrument instrument in source)
    {
      ushort? instrumentType = instrument.InstrumentType;
      if (instrumentType.HasValue)
      {
        switch (instrumentType.GetValueOrDefault())
        {
          case 29952:
            instrument.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("AccuScreen") as Bitmap;
            goto label_9;
          case 29953:
            instrument.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Senti_64x64") as Bitmap;
            goto label_9;
          case 29954:
            instrument.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Sentiero_64x64") as Bitmap;
            goto label_9;
        }
      }
      instrument.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("AccuScreen") as Bitmap;
label_9:
      instrument.CommunicationChannel = type1077CommandManager.CommunicationChannel;
    }
    if (this.InstrumentConnectedEventHander == null || source.Count <= 0)
      return;
    this.InstrumentConnectedEventHander((object) this, new InstrumentAvailableEventArgs((IInstrument) source.FirstOrDefault<Instrument>()));
  }

  private void RawFlashDataAvailableEventHandler(object sender, ChannelDataReceivedEventArgs e)
  {
    if (!(sender is Type1077CommandManager) || e == null)
      return;
    Type1077CommandManager type1077CommandManager = sender as Type1077CommandManager;
    Type1077DataExchangeService.Logger.Debug("Received data package for command {0} which is in state {1}.", (object) type1077CommandManager.Command, (object) type1077CommandManager.State);
    List<DataExchangeTokenSet> dataExchangeTokenSets = new Type1077StreamReader((Stream) new MemoryStream(e.DataPackage.Elements.ToArray())).Read();
    List<DataExchangeTokenSet> tokenSets = DataExchangeManager.Instance.MoveDataExchangeTokenSets(DataExchangeManager.Instance.GetDataExchangeSetMap("Type1077", "PM-PatientManagement"), (IEnumerable<DataExchangeTokenSet>) dataExchangeTokenSets);
    if (this.DataAvailableForDataExchangeEventHandler == null)
      return;
    this.DataAvailableForDataExchangeEventHandler((object) this, new DataExchangeTokenSetAvailableEventArgs((IEnumerable<DataExchangeTokenSet>) tokenSets));
  }

  private void CommandManagerStateChangingEventHandler(object sender, CommandStateEventArgs e)
  {
    if (!(sender is Type1077CommandManager) || e == null)
      return;
    Type1077CommandManager type1077CommandManager = sender as Type1077CommandManager;
    Type1077DataExchangeService.Logger.Debug("Data service registered a state change [{0}] for command {1}.", (object) e.NewState, (object) type1077CommandManager.Command);
    switch (e.NewState)
    {
      case CommandState.Unknown:
        break;
      case CommandState.SendingOut:
        break;
      case CommandState.SentOut:
        break;
      case CommandState.Acknowledged:
        break;
      case CommandState.NotAcknowledged:
        type1077CommandManager.Disconnect();
        this.operationState = OperationStateType.Failed;
        break;
      case CommandState.Performing:
        this.operationState = OperationStateType.Running;
        break;
      case CommandState.Performed:
        type1077CommandManager.Disconnect();
        this.operationState = OperationStateType.Completed;
        break;
      case CommandState.Failed:
        type1077CommandManager.Disconnect();
        this.operationState = OperationStateType.Failed;
        break;
      case CommandState.TransmissionError:
        type1077CommandManager.Disconnect();
        this.operationState = OperationStateType.Failed;
        break;
      case CommandState.TimedOut:
        type1077CommandManager.Disconnect();
        this.operationState = OperationStateType.Failed;
        break;
      default:
        type1077CommandManager.Disconnect();
        this.operationState = OperationStateType.Failed;
        break;
    }
  }

  public void LoadData(ICommunicationChannel communicationChannel)
  {
    if (communicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (communicationChannel));
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      type1077CommandManager.CommandDataAvailableEventHandler += new EventHandler<ChannelDataReceivedEventArgs>(this.RawFlashDataAvailableEventHandler);
      BinaryInstrumentCommand getInstrumentFlash = InstrumentCommands.GetInstrumentFlash;
      type1077CommandManager.CommunicationChannel = communicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(getInstrumentFlash);
      switch (type1077CommandManager.State)
      {
        case CommandState.Failed:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_LoadData_FailureDownloadingData);
        case CommandState.InstrumentAbort:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_LoadData_Abort);
        case CommandState.TransmissionError:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_LoadData_TransmissionError);
        case CommandState.TimedOut:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_LoadData_Timeout);
      }
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while executing an instrument data download on {0}", (object) communicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  private void ExecuteInstrumentCommand(IInstrument instrument, BinaryInstrumentCommand command)
  {
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      type1077CommandManager.CommandDataAvailableEventHandler += new EventHandler<ChannelDataReceivedEventArgs>(this.RawFlashDataAvailableEventHandler);
      type1077CommandManager.CommunicationChannel = instrument.CommunicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(command);
      switch (type1077CommandManager.State)
      {
        case CommandState.Failed:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_LoadData_FailureDownloadingData);
        case CommandState.InstrumentAbort:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_LoadData_Abort);
        case CommandState.TransmissionError:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_LoadData_TransmissionError);
        case CommandState.TimedOut:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_LoadData_Timeout);
      }
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while executing an instrument data download on {0}", (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void StoreData(IInstrument instrument, List<ISupportDataExchangeToken> data)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    IEnumerable<object> patients = (IEnumerable<object>) null;
    EventHandler<ModelChangedEventArgs> eventHandler = (EventHandler<ModelChangedEventArgs>) ((sender, e) =>
    {
      if (!(e.ChangedObject is ICollection<Patient>) || e.ChangeType != ChangeType.SelectionChanged)
        return;
      patients = (IEnumerable<object>) (e.ChangedObject as ICollection<Patient>).ToArray<Patient>();
    });
    PatientManager.Instance.Changed += eventHandler;
    PatientManager.Instance.Changed -= eventHandler;
    if (patients == null)
      return;
    List<DataExchangeTokenSet> exchangeTokenSetList1 = DataExchangeManager.Instance.MoveDataExchangeTokenSets(DataExchangeManager.Instance.GetRecordMap("PM-PatientManagement", "Type1077", "Patient", "28928"), patients);
    List<DataExchangeTokenSet> exchangeTokenSetList2 = DataExchangeManager.Instance.MoveDataExchangeTokenSets(DataExchangeManager.Instance.GetRecordMap("PM-PatientManagement", "Type1077", "Patient", "30736"), patients);
    MemoryStream memoryStream = new MemoryStream();
    RecordDescription recordDesccription1 = DataExchangeManager.Instance.GetRecordDesccription("Type1077", "28928");
    RecordDescription recordDesccription2 = DataExchangeManager.Instance.GetRecordDesccription("Type1077", "30736");
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter((Stream) memoryStream, new RecordDescription[2]
    {
      recordDesccription1,
      recordDesccription2
    });
    try
    {
      type1077StreamWriter.Open();
      type1077StreamWriter.Write(recordDesccription1, exchangeTokenSetList1.ToArray());
      type1077StreamWriter.Write(recordDesccription2, exchangeTokenSetList2.ToArray());
    }
    finally
    {
      type1077StreamWriter.Close();
    }
    new Type1077CommandManager().CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
    BinaryInstrumentCommand toInstrumentFlash = InstrumentCommands.AppendToInstrumentFlash;
    toInstrumentFlash.Append(memoryStream.ToArray());
    this.ExecuteInstrumentCommand(instrument, toInstrumentFlash);
  }

  public void EraseData(IInstrument instrument)
  {
    if (!(instrument is Instrument instrument1))
      return;
    BinaryInstrumentCommand eraseInstrumentFlash = InstrumentCommands.EraseInstrumentFlash;
    int num = 12;
    if (instrument1.UsedStorage > 0UL)
      num = (int) (Math.Ceiling((double) instrument1.UsedStorage / (double) ushort.MaxValue) * 12.0);
    eraseInstrumentFlash.ExecutionTimeout = eraseInstrumentFlash.AcknowledgeTimeout = (ushort) (num * 1000);
    this.ExecuteInstrumentCommand((IInstrument) instrument1, eraseInstrumentFlash);
  }

  public void SetClock(IInstrument instrument)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      InstrumentToken instrumentToken = new InstrumentToken("32777", DataConverter.GetByteArray(DateTime.Now));
      BinaryInstrumentCommand setDateTime = InstrumentCommands.SetDateTime;
      setDateTime.Append(instrumentToken.RawData);
      type1077CommandManager.CommunicationChannel = instrument.CommunicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(setDateTime);
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while setting date and time on {0}", (object) instrument.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void UpdateFirmware(ICommunicationChannel communicationChannel, byte[] instrumentImage)
  {
    if (communicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (communicationChannel));
    if (instrumentImage == null)
      return;
    if (instrumentImage.Length == 0)
      return;
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      type1077CommandManager.CommunicationChannel = communicationChannel;
      type1077CommandManager.Connect();
      BinaryInstrumentCommand instrumentFirmware = InstrumentCommands.UpdateInstrumentFirmware;
      instrumentFirmware.Append(instrumentImage);
      instrumentFirmware.BlockSize = 8192 /*0x2000*/;
      type1077CommandManager.Execute(instrumentFirmware);
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while updating the firmware on instrument connected to {0}", (object) communicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void Reboot(ICommunicationChannel communicationChannel)
  {
    if (communicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (communicationChannel));
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      type1077CommandManager.CommunicationChannel = communicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(InstrumentCommands.RebootInstrument);
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while rebooting the instrument connected on {0}", (object) communicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void BeginConfiguration(ICommunicationChannel communicationChannel)
  {
    if (communicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (communicationChannel));
    this.OpenMessagePanel(communicationChannel, Resources.Type1077DataExchangeService_BeginConfiguration_SynchronizeConfiguration);
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      type1077CommandManager.CommunicationChannel = communicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(InstrumentCommands.BeginConfiguartion);
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while rebooting the instrument connected on {0}", (object) communicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void CommitConfiguration(ICommunicationChannel communicationChannel)
  {
    if (communicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (communicationChannel));
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      type1077CommandManager.CommunicationChannel = communicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(InstrumentCommands.CommitConfiguartion);
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while storing data on the instrument connected at {0}", (object) communicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("InstrumentStorageFailure"), (System.Exception) ex);
    }
    this.CloseMessagePanel(communicationChannel);
  }

  public void OpenMessagePanel(ICommunicationChannel communicationChannel, string message)
  {
    if (communicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (communicationChannel));
    try
    {
      byte[] byteArray = DataConverter.GetByteArray(message);
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      type1077CommandManager.CommunicationChannel = communicationChannel;
      type1077CommandManager.Connect();
      BinaryInstrumentCommand openMessagePanel = InstrumentCommands.OpenMessagePanel;
      openMessagePanel.Append(byteArray);
      type1077CommandManager.Execute(openMessagePanel);
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while instructing the instrument connected on {0} to open the message panel.", (object) communicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void DisplayMessage(ICommunicationChannel communicationChannel, string message)
  {
    if (communicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (communicationChannel));
    try
    {
      byte[] byteArray = DataConverter.GetByteArray(message);
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      type1077CommandManager.CommunicationChannel = communicationChannel;
      type1077CommandManager.Connect();
      BinaryInstrumentCommand displayMessage = InstrumentCommands.DisplayMessage;
      displayMessage.Append(byteArray);
      type1077CommandManager.Execute(displayMessage);
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while instructing the instrument connected on {0} to display the message panel.", (object) communicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void CloseMessagePanel(ICommunicationChannel communicationChannel)
  {
    if (communicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (communicationChannel));
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      type1077CommandManager.CommunicationChannel = communicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(InstrumentCommands.CloseMessagePanel);
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while instructing the instrument connected on {0} to close the message panel.", (object) communicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void UpdateUsers(ICommunicationChannel communicationChannel)
  {
    if (!SystemConfigurationManager.Instance.IsUserManagementActive)
      return;
    IEnumerable<object> array = (IEnumerable<object>) UserManager.Instance.Users.Where<User>((Func<User, bool>) (u => u.IsActive)).ToArray<User>();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM-UserProfileManagement", "Type1077", "User", "8447");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, array);
    MemoryStream memoryStream = new MemoryStream();
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter((Stream) memoryStream, new RecordDescription[1]
    {
      recordDescription
    });
    try
    {
      type1077StreamWriter.Open();
      type1077StreamWriter.Write(recordDescription, exchangeTokenSetList.ToArray());
    }
    finally
    {
      type1077StreamWriter.Close();
    }
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      BinaryInstrumentCommand toInstrumentFlash = InstrumentCommands.AppendToInstrumentFlash;
      toInstrumentFlash.Append(memoryStream.ToArray());
      type1077CommandManager.CommunicationChannel = communicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(toInstrumentFlash);
      switch (type1077CommandManager.State)
      {
        case CommandState.Failed:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateUsers_Failure);
        case CommandState.InstrumentAbort:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateUsers_Abort);
        case CommandState.TransmissionError:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateUsers_TransmissionError);
        case CommandState.TimedOut:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateUsers_Timeout);
      }
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while updating the users on {0}", (object) communicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void UpdateProfiles(IInstrument instrument)
  {
    if (!SystemConfigurationManager.Instance.IsUserManagementActive)
      return;
    UserProfile[] array = UserProfileManager.Instance.Profiles.ToArray();
    List<object> dataRecords = new List<object>();
    foreach (UserProfile userProfile1 in (IEnumerable<object>) array)
    {
      UserProfile userProfile2 = new UserProfile()
      {
        Id = userProfile1.Id,
        ProfileAccessPermissions = new List<AccessPermission>()
      };
      foreach (AccessPermission accessPermission in userProfile1.ProfileAccessPermissions)
      {
        if (accessPermission.ComponentId.Equals(this.InstrumentSignature) && accessPermission.AccessPermissionFlag)
          userProfile2.ProfileAccessPermissions.Add(accessPermission);
      }
      dataRecords.Add((object) userProfile2);
    }
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM-UserProfileManagement", "Type1077", "UserProfile", "30976");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, (IEnumerable<object>) dataRecords);
    MemoryStream memoryStream = new MemoryStream();
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter((Stream) memoryStream, new RecordDescription[1]
    {
      recordDescription
    });
    try
    {
      type1077StreamWriter.Open();
      type1077StreamWriter.Write(recordDescription, exchangeTokenSetList.ToArray());
    }
    finally
    {
      type1077StreamWriter.Close();
    }
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      BinaryInstrumentCommand toInstrumentFlash = InstrumentCommands.AppendToInstrumentFlash;
      toInstrumentFlash.Append(memoryStream.ToArray());
      type1077CommandManager.CommunicationChannel = instrument.CommunicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(toInstrumentFlash);
      switch (type1077CommandManager.State)
      {
        case CommandState.Failed:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateProfiles_Failed);
        case CommandState.InstrumentAbort:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateProfiles_Abort);
        case CommandState.TransmissionError:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateProfiles_TransmissionError);
        case CommandState.TimedOut:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateProfiles_Timeout);
      }
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while updating the users on {0}", (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void UpdateFacilities(IInstrument instrument)
  {
    if (!SystemConfigurationManager.Instance.IsSiteAndFacilityManagementActive)
      return;
    FacilityManager.Instance.RefreshData();
    IEnumerable<object> array = (IEnumerable<object>) FacilityManager.Instance.Facilities.Where<Facility>((Func<Facility, bool>) (f => !f.Inactive.HasValue)).ToArray<Facility>();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM-SiteAndFacilityManagement", "Type1077", "Facility", "41216");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, array);
    MemoryStream memoryStream = new MemoryStream();
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter((Stream) memoryStream, new RecordDescription[1]
    {
      recordDescription
    });
    try
    {
      type1077StreamWriter.Open();
      type1077StreamWriter.Write(recordDescription, exchangeTokenSetList.ToArray());
    }
    finally
    {
      type1077StreamWriter.Close();
    }
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      BinaryInstrumentCommand toInstrumentFlash = InstrumentCommands.AppendToInstrumentFlash;
      toInstrumentFlash.Append(memoryStream.ToArray());
      type1077CommandManager.CommunicationChannel = instrument.CommunicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(toInstrumentFlash);
      switch (type1077CommandManager.State)
      {
        case CommandState.Failed:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateFacilities_Failed);
        case CommandState.InstrumentAbort:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateFacilities_Abort);
        case CommandState.TransmissionError:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateFacilities_TransmissionError);
        case CommandState.TimedOut:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateFacilities_Timeout);
      }
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while updating the facilities on {0}", (object) instrument.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>($"Failure while configuring {instrument.Name}.", (System.Exception) ex);
    }
  }

  public void UpdateLocations(IInstrument instrument)
  {
    if (!SystemConfigurationManager.Instance.IsSiteAndFacilityManagementActive)
      return;
    LocationTypeManager.Instance.RefreshData();
    IEnumerable<object> array = (IEnumerable<object>) LocationTypeManager.Instance.LocationTypes.Where<LocationType>((Func<LocationType, bool>) (l => !l.Inactive.HasValue)).ToArray<LocationType>();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM-SiteAndFacilityManagement", "Type1077", "Location", "41472");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, array);
    MemoryStream memoryStream = new MemoryStream();
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter((Stream) memoryStream, new RecordDescription[1]
    {
      recordDescription
    });
    try
    {
      type1077StreamWriter.Open();
      type1077StreamWriter.Write(recordDescription, exchangeTokenSetList.ToArray());
    }
    finally
    {
      type1077StreamWriter.Close();
    }
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      BinaryInstrumentCommand toInstrumentFlash = InstrumentCommands.AppendToInstrumentFlash;
      toInstrumentFlash.Append(memoryStream.ToArray());
      type1077CommandManager.CommunicationChannel = instrument.CommunicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(toInstrumentFlash);
      switch (type1077CommandManager.State)
      {
        case CommandState.Failed:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateLocations_Failed);
        case CommandState.InstrumentAbort:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateLocations_Abort);
        case CommandState.TransmissionError:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateLocations_TransmissionError);
        case CommandState.TimedOut:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateLocations_Timeout);
      }
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while updating the facilities on {0}", (object) instrument.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>($"Failure while configuring {instrument.Name}.", (System.Exception) ex);
    }
  }

  public void UpdatePredefinedComments(ICommunicationChannel communicationChannel)
  {
  }

  public void UpdateRiskIndicators(IInstrument instrument)
  {
    if (!SystemConfigurationManager.Instance.IsUserManagementActive)
      return;
    IEnumerable<object> array = (IEnumerable<object>) RiskIndicatorManager.Instance.RiskIndicatorList.ToArray();
    foreach (RiskIndicator riskIndicator in array)
      ;
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM-PatientManagement", "Type1077", "RiskIndicator", "30720");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, array);
    MemoryStream memoryStream = new MemoryStream();
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter((Stream) memoryStream, new RecordDescription[1]
    {
      recordDescription
    });
    try
    {
      type1077StreamWriter.Open();
      type1077StreamWriter.Write(recordDescription, exchangeTokenSetList.ToArray());
    }
    finally
    {
      type1077StreamWriter.Close();
    }
    try
    {
      Type1077CommandManager type1077CommandManager = new Type1077CommandManager();
      type1077CommandManager.CommandStateChangingEventHandler += new EventHandler<CommandStateEventArgs>(this.CommandManagerStateChangingEventHandler);
      BinaryInstrumentCommand toInstrumentFlash = InstrumentCommands.AppendToInstrumentFlash;
      toInstrumentFlash.Append(memoryStream.ToArray());
      type1077CommandManager.CommunicationChannel = instrument.CommunicationChannel;
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(toInstrumentFlash);
      switch (type1077CommandManager.State)
      {
        case CommandState.Failed:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateRiskIndicators_Failed);
        case CommandState.InstrumentAbort:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateRiskIndicators_Abort);
        case CommandState.TransmissionError:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateRiskIndicators_TransmissionError);
        case CommandState.TimedOut:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(Resources.Type1077DataExchangeService_UpdateRiskIndicators_Timeout);
      }
    }
    catch (CommandManagerException ex)
    {
      Type1077DataExchangeService.Logger.Error((System.Exception) ex, "Failure while updating the users on {0}", (object) instrument.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentDownloadFailure, (System.Exception) ex);
    }
  }

  public void UpdateConfiguration(IInstrument instrument)
  {
  }
}
