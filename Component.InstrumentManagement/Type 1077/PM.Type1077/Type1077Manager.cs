// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Type1077Manager
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Communication;
using PathMedical.DataExchange;
using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.DeviceCommunication.Command;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.Logging;
using PathMedical.PatientManagement;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.Permission;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement;
using PathMedical.SystemConfiguration;
using PathMedical.Type1077.Channel;
using PathMedical.Type1077.DataExchange;
using PathMedical.Type1077.DataExchange.Column;
using PathMedical.Type1077.Firmware;
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
using System.Text;
using System.Threading;

#nullable disable
namespace PathMedical.Type1077;

[CLSCompliant(false)]
public class Type1077Manager : 
  ISingleSelectionModel<Instrument>,
  IModel,
  ISupportInstrumentConfiguration,
  ISupportInstrumentFirmwareUpdate,
  ISupportInstrumentSearch,
  ISupportInstrumentDataDownload,
  ISupportInstrumentDataUpload
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (Type1077Manager), "$Rev: 1592 $");
  private List<Type1077Instrument> foundInstruments;
  private readonly double[] probeSpkNormValues = new double[8]
  {
    63.75,
    70.455,
    75.095,
    83.26,
    90.565,
    88.05,
    67.32,
    57.455
  };
  private readonly double[] eccSpkNormValues = new double[8]
  {
    67.9,
    75.1,
    79.75,
    89.37,
    84.28,
    75.78,
    66.27,
    41.95
  };
  private readonly double[] probeMicNormValues = new double[8]
  {
    98.47,
    204.15,
    335.39,
    876.88,
    1099.23,
    2200.4,
    566.2,
    952.35
  };
  private Type1077ProbeInformation type1077ProbeInformationForRmsCalculation;
  private Type1077LoopBackCableInformation type1077LoopBackInformationForRmsCalculation;
  private FirmwareImage firmwareImage;

  public static Type1077Manager Instance => PathMedical.Singleton.Singleton<Type1077Manager>.Instance;

  public bool EraseFlashDataOnInstrument { get; set; }

  private Type1077Manager() => this.LoadDataExchangeDescriptions();

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void RefreshData()
  {
  }

  public void ChangeSingleSelection(Instrument selection)
  {
  }

  public bool IsOneItemSelected<T>() where T : Instrument => true;

  public bool IsOneItemAvailable<T>() where T : Instrument => true;

  public event EventHandler<InstrumentAvailableEventArgs> InstrumentFound;

  public void SearchInstruments()
  {
    this.foundInstruments = new List<Type1077Instrument>();
    foreach (string availablePortName in UniversalSerialBusChannel.GetAvailablePortNames())
    {
      if (!string.IsNullOrEmpty(availablePortName))
      {
        try
        {
          Type1077Manager.Logger.Info("Checking {0} for a connected instrument.", (object) availablePortName);
          Type1077Manager.ExecuteCommand((ICommunicationChannel) new UniversalSerialBusChannel(availablePortName), InstrumentCommands.GetInstrumentInformation, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnSingleInstrumentFound));
        }
        catch (CommandManagerException ex)
        {
          Type1077Manager.Logger.Error((System.Exception) ex, "Failure while executing an instrument search on {0}", (object) availablePortName);
        }
        catch (System.Exception ex)
        {
          Type1077Manager.Logger.Error(ex, "Failure while executing an instrument search on {0}", (object) availablePortName);
        }
      }
    }
  }

  private void OnSingleInstrumentFound(object sender, ChannelDataReceivedEventArgs e)
  {
    if (!(sender is Type1077CommandManager) || e == null)
      return;
    Type1077CommandManager type1077CommandManager = sender as Type1077CommandManager;
    Type1077Manager.Logger.Debug("Received data package for command {0} which is in state {1}.", (object) type1077CommandManager.Command, (object) type1077CommandManager.State);
    List<DataExchangeTokenSet> dataExchangeTokenSets = new Type1077StreamReader((Stream) new MemoryStream(e.DataPackage.Elements.ToArray())).Read();
    foreach (Type1077Instrument fetchEntity in DataExchangeManager.Instance.FetchEntities<Type1077Instrument>(DataExchangeManager.Instance.GetRecordMap("Type1077", "PM", "30464", "Type1077Instrument"), (IEnumerable<DataExchangeTokenSet>) dataExchangeTokenSets))
    {
      Type1077Instrument foundInstrument = fetchEntity;
      if (this.foundInstruments.FirstOrDefault<Type1077Instrument>((Func<Type1077Instrument, bool>) (i =>
      {
        Guid? instrumentTypeSignature1 = i.InstrumentTypeSignature;
        Guid? instrumentTypeSignature2 = foundInstrument.InstrumentTypeSignature;
        return (instrumentTypeSignature1.HasValue == instrumentTypeSignature2.HasValue ? (instrumentTypeSignature1.HasValue ? (instrumentTypeSignature1.GetValueOrDefault() == instrumentTypeSignature2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && i.SerialNumber == foundInstrument.SerialNumber;
      })) == null)
      {
        this.foundInstruments.Add(foundInstrument);
        ushort? instrumentType = fetchEntity.InstrumentType;
        if (instrumentType.HasValue)
        {
          instrumentType = fetchEntity.InstrumentType;
          switch (instrumentType.Value)
          {
            case 29952:
              fetchEntity.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("AccuScreen") as Bitmap;
              break;
            case 29953:
              fetchEntity.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Senti_64x64") as Bitmap;
              break;
            case 29954:
              fetchEntity.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("Sentiero_64x64") as Bitmap;
              break;
            case 29955:
              fetchEntity.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("AccuScreen") as Bitmap;
              break;
            default:
              fetchEntity.Image = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("AccuScreen") as Bitmap;
              break;
          }
        }
        fetchEntity.CommunicationChannel = type1077CommandManager.CommunicationChannel;
        try
        {
          InstrumentManager.Instance.RegisterInstrumentConnection((Instrument) fetchEntity);
        }
        catch (System.Exception ex)
        {
        }
      }
    }
    if (this.Changed != null)
      this.Changed((object) this, ModelChangedEventArgs.Create<List<Type1077Instrument>>(this.foundInstruments, ChangeType.ItemAdded));
    if (this.InstrumentFound == null || this.foundInstruments.Count <= 0)
      return;
    this.InstrumentFound((object) this, new InstrumentAvailableEventArgs((IInstrument) this.foundInstruments.FirstOrDefault<Type1077Instrument>()));
  }

  public void DownloadInstrumentData(IInstrument instrument)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    try
    {
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.GetInstrumentFlash, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnDownloadedFlashDataAvailable));
    }
    catch (CommandManagerException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while downloading data from [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.Type1077Manager_DataDownloadFailure, (System.Exception) ex);
    }
  }

  private void OnDownloadedFlashDataAvailable(object sender, ChannelDataReceivedEventArgs e)
  {
    if (!(sender is Type1077CommandManager) || e == null)
      return;
    Type1077CommandManager type1077CommandManager = sender as Type1077CommandManager;
    Type1077Manager.Logger.Debug("Received data package for command {0} which is in state {1}.", (object) type1077CommandManager.Command, (object) type1077CommandManager.State);
    List<DataExchangeTokenSet> dataExchangeTokenSets = Type1077Manager.ProcessInstrumentFlashData(e.DataPackage.Elements.ToArray());
    if (dataExchangeTokenSets.Count <= 0)
      return;
    DataExchangeManager.Instance.Import((IEnumerable<DataExchangeTokenSet>) dataExchangeTokenSets);
  }

  private static List<DataExchangeTokenSet> ProcessInstrumentFlashData(byte[] instrumentFlashImage)
  {
    List<DataExchangeTokenSet> dataExchangeTokenSets = new Type1077StreamReader((Stream) new MemoryStream(instrumentFlashImage)).Read();
    return DataExchangeManager.Instance.MoveDataExchangeTokenSets(DataExchangeManager.Instance.GetRecordMaps("Type1077", "PM"), (IEnumerable<DataExchangeTokenSet>) dataExchangeTokenSets);
  }

  public void ConfigureInstrument(IInstrument instrument)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (!(instrument is Type1077Instrument))
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format(Resources.Type1077Manager_CantConfigureInstrument, (object) instrument.Name));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format(Resources.Type1077Manager_NoCommunicationChannelDefined, (object) instrument.Name));
    if (!(instrument is Type1077Instrument))
      return;
    Type1077Instrument type1077Instrument = instrument as Type1077Instrument;
    Instrument instrument1 = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i => string.Compare(i.SerialNumber, instrument.SerialNumber) == 0));
    Site site = (Site) null;
    User[] users = (User[]) null;
    if (instrument1 != null)
    {
      if (instrument1.Site != null)
        site = instrument1.Site;
      if (instrument1.UsersOnInstrument != null)
        users = instrument1.UsersOnInstrument.Where<User>((Func<User, bool>) (u => u.UserOnInstrumentValue)).ToArray<User>();
      if (SystemConfigurationManager.Instance.IsPluginLoaded(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554")) && (users == null || users.Length == 0))
        users = UserManager.Instance.Users.Where<User>((Func<User, bool>) (u => u.IsActive)).ToArray<User>();
    }
    else if (SystemConfigurationManager.Instance.IsPluginLoaded(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554")))
      users = UserManager.Instance.Users.Where<User>((Func<User, bool>) (u => u.IsActive)).ToArray<User>();
    bool flag1 = false;
    bool flag2 = false;
    MemoryStream configurationDataStream = new MemoryStream();
    try
    {
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.OpenMessagePanel, DataConverter.GetByteArray("Synchronizing Configuration"));
      flag1 = true;
      InstrumentToken instrumentToken = new InstrumentToken("32777", DataConverter.GetByteArray(DateTime.Now));
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.SetDateTime, instrumentToken.RawData);
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.BeginConfiguartion);
      Type1077Manager.WriteInstrumentInformation((Stream) configurationDataStream, (Instrument) type1077Instrument);
      Type1077Manager.WriteUsers((Stream) configurationDataStream, users);
      if (instrument.InstrumentTypeSignature.HasValue)
        Type1077Manager.WriteProfiles((Stream) configurationDataStream);
      Type1077Manager.WriteFacilities(site, (Stream) configurationDataStream);
      Type1077Manager.WriteLocations(site, (Stream) configurationDataStream);
      Type1077Manager.WriteRiskIndicators((Stream) configurationDataStream);
      Type1077Manager.WritePredefinedComments((Stream) configurationDataStream);
      if (instrument.InstrumentTypeSignature.HasValue)
        Type1077Manager.WriteTestPluginConfigurations(instrument.InstrumentTypeSignature.Value, (Stream) configurationDataStream);
      Type1077Manager.WritePatientFieldConfiguration((Stream) configurationDataStream);
      if (SystemConfigurationManager.Instance.Plugins.Any<IPlugin>((Func<IPlugin, bool>) (p => p.Fingerprint.Equals(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554")))))
        Type1077Manager.WriteQualityAssurancePatients(configurationDataStream);
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.AppendToInstrumentFlash, configurationDataStream.ToArray());
      if (PermissionManager.Instance.HasPermission(UserProfileManagementPermissions.ResetUsers))
        Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.ResetAllUserLocks);
      BinaryInstrumentCommand commitConfiguartion = InstrumentCommands.CommitConfiguartion;
      commitConfiguartion.ExecutionTimeout = (ushort) 10000;
      Type1077Manager.ExecuteCommand(instrument, commitConfiguartion);
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.CloseMessagePanel);
      flag1 = false;
      byte[] newFirmware = Type1077Manager.GetNewFirmware(instrument);
      if (newFirmware.Length != 0)
        this.UpdateFirmware(instrument, newFirmware);
      else
        Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.RebootInstrument);
    }
    catch (InstrumentCommunicationException ex)
    {
      flag2 = true;
      throw;
    }
    finally
    {
      if (flag1 && !flag2)
      {
        Thread.Sleep(5000);
        Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.CloseMessagePanel);
      }
    }
  }

  private static void WriteInstrumentInformation(Stream stream, Instrument instrument)
  {
    if (instrument == null)
      return;
    Instrument instrument1 = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i =>
    {
      if (!(i.SerialNumber == instrument.SerialNumber))
        return false;
      Guid? instrumentTypeSignature1 = i.InstrumentTypeSignature;
      Guid? instrumentTypeSignature2 = instrument.InstrumentTypeSignature;
      if (instrumentTypeSignature1.HasValue != instrumentTypeSignature2.HasValue)
        return false;
      return !instrumentTypeSignature1.HasValue || instrumentTypeSignature1.GetValueOrDefault() == instrumentTypeSignature2.GetValueOrDefault();
    }));
    Type1077InstrumentConfiguration instrumentConfiguration = new Type1077InstrumentConfiguration()
    {
      InstrumentName = instrument1 != null ? instrument1.Name : instrument.Name
    };
    if (instrument.GlobalInstrumentConfiguration != null)
    {
      instrumentConfiguration.DisplayTimeout = instrument.GlobalInstrumentConfiguration.DisplayTimeout * 60;
      instrumentConfiguration.PowerTimeout = instrument.GlobalInstrumentConfiguration.PowerTimeout * 60;
    }
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "Type1077InstrumentConfiguration", "41056");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, (IEnumerable<object>) new List<object>()
    {
      (object) instrumentConfiguration
    });
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter(stream, new RecordDescription[1]
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
  }

  private static void WriteUsers(Stream stream, User[] users)
  {
    if (users == null || users.Length == 0 || !SystemConfigurationManager.Instance.IsUserManagementActive)
      return;
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "User", "8447");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, (IEnumerable<object>) users);
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter(stream, new RecordDescription[1]
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
  }

  private static void WriteProfiles(Stream stream)
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
        if (accessPermission.ComponentId.Equals(new Guid("9BCBB607-BD5C-497c-9E7E-FBF4D3E9A763")) && accessPermission.AccessPermissionFlag)
          userProfile2.ProfileAccessPermissions.Add(accessPermission);
      }
      dataRecords.Add((object) userProfile2);
    }
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "UserProfile", "30976");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, (IEnumerable<object>) dataRecords);
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter(stream, new RecordDescription[1]
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
  }

  private static void WriteFacilities(Site site, Stream stream)
  {
    if (!SystemConfigurationManager.Instance.IsSiteAndFacilityManagementActive)
      return;
    FacilityManager.Instance.RefreshData();
    IEnumerable<object> dataRecords = site != null ? (IEnumerable<object>) FacilityManager.Instance.Facilities.Where<Facility>((Func<Facility, bool>) (f =>
    {
      if (f.Inactive.HasValue)
        return false;
      Guid? siteId = f.SiteId;
      Guid id = site.Id;
      return siteId.HasValue && siteId.GetValueOrDefault() == id;
    })).ToArray<Facility>() : (IEnumerable<object>) FacilityManager.Instance.Facilities.Where<Facility>((Func<Facility, bool>) (f => !f.Inactive.HasValue)).ToArray<Facility>();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "Facility", "41216");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, dataRecords);
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter(stream, new RecordDescription[1]
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
  }

  private static void WriteLocations(Site site, Stream stream)
  {
    LocationTypeManager.Instance.RefreshData();
    IEnumerable<object> array;
    if (SystemConfigurationManager.Instance.IsSiteAndFacilityManagementActive)
    {
      if (site == null)
      {
        FacilityManager.Instance.RefreshData();
        array = (IEnumerable<object>) FacilityManager.Instance.Facilities.Where<Facility>((Func<Facility, bool>) (f => !f.Inactive.HasValue)).SelectMany<Facility, LocationType>((Func<Facility, IEnumerable<LocationType>>) (f => (IEnumerable<LocationType>) f.LocationTypes)).Where<LocationType>((Func<LocationType, bool>) (l => !l.Inactive.HasValue)).Distinct<LocationType>().ToArray<LocationType>();
      }
      else
      {
        FacilityManager.Instance.RefreshData();
        array = (IEnumerable<object>) FacilityManager.Instance.Facilities.Where<Facility>((Func<Facility, bool>) (f =>
        {
          if (f.Inactive.HasValue)
            return false;
          Guid? siteId = f.SiteId;
          Guid id = site.Id;
          return siteId.HasValue && siteId.GetValueOrDefault() == id;
        })).SelectMany<Facility, LocationType>((Func<Facility, IEnumerable<LocationType>>) (f => (IEnumerable<LocationType>) f.LocationTypes)).Where<LocationType>((Func<LocationType, bool>) (l => !l.Inactive.HasValue)).Distinct<LocationType>().ToArray<LocationType>();
      }
    }
    else
      array = (IEnumerable<object>) LocationTypeManager.Instance.LocationTypes.Where<LocationType>((Func<LocationType, bool>) (l => !l.Inactive.HasValue)).ToArray<LocationType>();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "Location", "41472");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, array);
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter(stream, new RecordDescription[1]
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
  }

  private static void WriteRiskIndicators(Stream stream)
  {
    IEnumerable<object> array = (IEnumerable<object>) RiskIndicatorManager.Instance.RiskIndicatorList.Where<RiskIndicator>((Func<RiskIndicator, bool>) (r => r.IsActive.GetValueOrDefault())).ToArray<RiskIndicator>();
    foreach (RiskIndicator riskIndicator in array)
      ;
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "RiskIndicator", "30720");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, array);
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter(stream, new RecordDescription[1]
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
  }

  private static void WritePredefinedComments(Stream stream)
  {
    IEnumerable<object> array = (IEnumerable<object>) CommentManager.Instance.CommentList.Where<PredefinedComment>((Func<PredefinedComment, bool>) (c => c.IsActive.GetValueOrDefault())).ToArray<PredefinedComment>();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "PredefinedComment", "29696");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, array);
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter(stream, new RecordDescription[1]
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
  }

  private static void WriteTestPluginConfigurations(Guid instrumentTypeSignature, Stream stream)
  {
    foreach (ITestPlugin testPlugin in SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().Where<ITestPlugin>((Func<ITestPlugin, bool>) (p => p.InstrumentSignature.Equals(instrumentTypeSignature))).ToArray<ITestPlugin>())
      testPlugin.WriteConfiguration(stream);
  }

  private static void WritePatientFieldConfiguration(Stream stream)
  {
    List<PatientFieldConfigItem> list = PatientConfigManager.Instance.PatientFieldConfigItems.Where<PatientFieldConfigItem>((Func<PatientFieldConfigItem, bool>) (f => f.IsActive)).ToList<PatientFieldConfigItem>();
    if (list.Count <= 0)
      return;
    Type1077PatientConfiguration patientConfiguration = new Type1077PatientConfiguration()
    {
      NumberOfActiveFields = (ushort) list.Count<PatientFieldConfigItem>()
    };
    List<ushort> ushortList1 = new List<ushort>();
    List<ushort> ushortList2 = new List<ushort>();
    foreach (PatientFieldConfigItem patientFieldConfigItem in list)
    {
      ushort instrumentFieldId = Type1077Manager.GetInstrumentFieldId(patientFieldConfigItem.FieldId);
      if (instrumentFieldId > (ushort) 0)
      {
        ushortList1.Add(instrumentFieldId);
        ushort num = 0;
        if (string.Compare(patientFieldConfigItem.FieldId, "PatientRecordNumber") == 0)
        {
          switch (PatientManagementConfiguration.Instance.PatientIdFormat)
          {
            case MedicalRecordTypes.MRC:
              num = (ushort) 1;
              break;
            case MedicalRecordTypes.CPR_DK:
              num = (ushort) 2;
              break;
            case MedicalRecordTypes.GERMANY:
              num = (ushort) 3;
              break;
            case MedicalRecordTypes.TURKEY:
              num = (ushort) 4;
              break;
          }
        }
        if (patientFieldConfigItem.IsInMandatoryGroup1)
          num |= (ushort) 256 /*0x0100*/;
        if (patientFieldConfigItem.IsInMandatoryGroup2)
          num |= (ushort) 512 /*0x0200*/;
        if (patientFieldConfigItem.IsInMandatoryGroup3)
          num |= (ushort) 1024 /*0x0400*/;
        ushortList2.Add(num);
      }
    }
    patientConfiguration.ActiveFields = ushortList1.ToArray();
    patientConfiguration.FieldConfiguration = ushortList2.ToArray();
    ushort num1 = 0;
    List<ushort> ushortList3 = new List<ushort>();
    ushortList3.AddRange((IEnumerable<ushort>) Type1077Manager.GetSortListConfiguration(PatientManagementConfiguration.Instance.PatientBrowserListConfigurationList1Field1, PatientManagementConfiguration.Instance.PatientBrowserListConfigurationList1Field2));
    ushortList3.AddRange((IEnumerable<ushort>) Type1077Manager.GetSortListConfiguration(PatientManagementConfiguration.Instance.PatientBrowserListConfigurationList2Field1, PatientManagementConfiguration.Instance.PatientBrowserListConfigurationList2Field2));
    ushortList3.AddRange((IEnumerable<ushort>) Type1077Manager.GetSortListConfiguration(PatientManagementConfiguration.Instance.PatientBrowserListConfigurationList3Field1, PatientManagementConfiguration.Instance.PatientBrowserListConfigurationList3Field2));
    if (ushortList3.Count > 0)
      num1 = (ushort) (ushortList3.Count / 2);
    patientConfiguration.NumberOfSortLists = num1;
    patientConfiguration.SortListConfiguration = ushortList3.ToArray();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "Type1077PatientConfiguration", "41040");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, (IEnumerable<object>) new List<object>()
    {
      (object) patientConfiguration
    });
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter(stream, new RecordDescription[1]
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
  }

  private static void WriteQualityAssurancePatients(MemoryStream configurationDataStream)
  {
    IEnumerable<object> dataRecords = (IEnumerable<object>) new List<object>()
    {
      (object) new Patient()
      {
        Id = new Guid("94BEA8ED-1F53-4652-B748-1E743611F0A1"),
        HospitalId = "QA1",
        ConsentStatus = new ConsentStatus?(ConsentStatus.Full),
        NicuStatus = new NicuStatus?(NicuStatus.No),
        PatientContact = new Contact()
        {
          Id = new Guid("94BEA8ED-1F53-4652-B748-1E743611F0A1"),
          PatientId = new Guid("94BEA8ED-1F53-4652-B748-1E743611F0A1"),
          DateOfBirth = new DateTime?(new DateTime(1900, 1, 1, 0, 0, 0)),
          Surname = "QA",
          Forename1 = "CAVITY"
        }
      },
      (object) new Patient()
      {
        Id = new Guid("FC1599F1-EBFC-4608-8625-1944B6010838"),
        HospitalId = "QA2",
        ConsentStatus = new ConsentStatus?(ConsentStatus.Full),
        NicuStatus = new NicuStatus?(NicuStatus.No),
        PatientContact = new Contact()
        {
          Id = new Guid("FC1599F1-EBFC-4608-8625-1944B6010838"),
          PatientId = new Guid("FC1599F1-EBFC-4608-8625-1944B6010838"),
          DateOfBirth = new DateTime?(new DateTime(1900, 1, 1, 0, 0, 0)),
          Surname = "QA",
          Forename1 = "OCCLUSION"
        }
      },
      (object) new Patient()
      {
        Id = new Guid("60E9AA98-1B07-4e5f-AFED-47F813F65CCA"),
        HospitalId = "QA3",
        ConsentStatus = new ConsentStatus?(ConsentStatus.Full),
        NicuStatus = new NicuStatus?(NicuStatus.No),
        PatientContact = new Contact()
        {
          Id = new Guid("60E9AA98-1B07-4e5f-AFED-47F813F65CCA"),
          PatientId = new Guid("60E9AA98-1B07-4e5f-AFED-47F813F65CCA"),
          DateOfBirth = new DateTime?(new DateTime(1900, 1, 1, 0, 0, 0)),
          Surname = "QA",
          Forename1 = "EAR"
        }
      },
      (object) new Patient()
      {
        Id = new Guid("7E6E7731-0716-4859-BFD1-27C6234F7CA2"),
        HospitalId = "QA4",
        ConsentStatus = new ConsentStatus?(ConsentStatus.Full),
        NicuStatus = new NicuStatus?(NicuStatus.No),
        PatientContact = new Contact()
        {
          Id = new Guid("7E6E7731-0716-4859-BFD1-27C6234F7CA2"),
          PatientId = new Guid("7E6E7731-0716-4859-BFD1-27C6234F7CA2"),
          DateOfBirth = new DateTime?(new DateTime(1900, 1, 1, 0, 0, 0)),
          Surname = "QA",
          Forename1 = "CLICK"
        }
      }
    };
    try
    {
      List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "Patient", "28928"), dataRecords);
      RecordDescription recordDesccription = DataExchangeManager.Instance.GetRecordDesccription("Type1077", "28928");
      Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter((Stream) configurationDataStream, new RecordDescription[1]
      {
        recordDesccription
      });
      try
      {
        type1077StreamWriter.Open();
        type1077StreamWriter.Write(recordDesccription, exchangeTokenSetList.ToArray());
      }
      finally
      {
        type1077StreamWriter.Close();
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("{1}{0}<i>{2}</i>", (object) Environment.NewLine, (object) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("Type1077_FailureWhileCreatingPatientsForInstrument"), (object) ex.Message), ex);
    }
  }

  private static ushort GetInstrumentFieldId(string fieldName)
  {
    ushort instrumentFieldId = 0;
    if (string.Compare(fieldName, "PatientRecordNumber") == 0)
      instrumentFieldId = (ushort) 28929;
    else if (string.Compare(fieldName, "PatientForename") == 0)
      instrumentFieldId = (ushort) 28931;
    else if (string.Compare(fieldName, "PatientSurname") == 0)
      instrumentFieldId = (ushort) 28933;
    else if (string.Compare(fieldName, "PatientDateOfBirth") == 0)
      instrumentFieldId = (ushort) 28930;
    else if (string.Compare(fieldName, "PatientGender") == 0)
      instrumentFieldId = (ushort) 28934;
    else if (string.Compare(fieldName, "PatientBirthLocation") == 0)
      instrumentFieldId = (ushort) 28937;
    else if (string.Compare(fieldName, "PatientWeight") == 0)
      instrumentFieldId = (ushort) 28936;
    else if (string.Compare(fieldName, "MotherForename") == 0)
      instrumentFieldId = (ushort) 28963;
    else if (string.Compare(fieldName, "MotherSurname") == 0)
      instrumentFieldId = (ushort) 28964;
    else if (string.Compare(fieldName, "CaregiverForename") == 0)
      instrumentFieldId = (ushort) 28965;
    else if (string.Compare(fieldName, "CaregiverSurname") == 0)
      instrumentFieldId = (ushort) 28966;
    else if (string.Compare(fieldName, "Nicu") == 0)
      instrumentFieldId = (ushort) 28960;
    else if (string.Compare(fieldName, "ConsentState") == 0)
      instrumentFieldId = (ushort) 28961;
    else if (string.Compare(fieldName, "RiskIndicator") == 0)
      instrumentFieldId = (ushort) 28980;
    else if (string.Compare(fieldName, "HospitalId") == 0)
      instrumentFieldId = (ushort) 28982;
    return instrumentFieldId;
  }

  private static List<ushort> GetSortListConfiguration(string fieldName1, string fieldName2)
  {
    List<ushort> listConfiguration = new List<ushort>();
    if (!string.IsNullOrEmpty(fieldName1))
    {
      ushort instrumentFieldId1 = Type1077Manager.GetInstrumentFieldId(fieldName1);
      if (instrumentFieldId1 > (ushort) 0)
        listConfiguration.Add(instrumentFieldId1);
      if (!string.IsNullOrEmpty(fieldName2))
      {
        ushort instrumentFieldId2 = Type1077Manager.GetInstrumentFieldId(fieldName2);
        listConfiguration.Add(instrumentFieldId2);
      }
      else
        listConfiguration.Add((ushort) 0);
    }
    return listConfiguration;
  }

  private static byte[] GetNewFirmware(IInstrument instrument)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (!(instrument is Type1077Instrument))
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format(Resources.Type1077Manager_CantUpdateInstrument, (object) instrument.Name));
    byte[] newFirmware = new byte[0];
    FirmwareImage firmwareImage = (FirmwareImage) null;
    if (instrument is Type1077Instrument)
    {
      Type1077Instrument instrumentToConfigure = instrument as Type1077Instrument;
      FirmwareManager.Instance.RefreshData();
      firmwareImage = FirmwareManager.Instance.Firmwares.Where<FirmwareImage>((Func<FirmwareImage, bool>) (f =>
      {
        long buildNumber = f.BuildNumber;
        long? firmwareBuildNumber = instrumentToConfigure.FirmwareBuildNumber;
        long valueOrDefault = firmwareBuildNumber.GetValueOrDefault();
        return buildNumber > valueOrDefault & firmwareBuildNumber.HasValue && string.Compare(f.LanguagePackName, instrumentToConfigure.LanguagePackName, true) == 0;
      })).FirstOrDefault<FirmwareImage>();
      if (firmwareImage != null)
        Type1077Manager.Logger.Info("Found a new firmware image (build {0}) for instrument {1} (build {2}).", (object) firmwareImage.BuildNumber, (object) instrumentToConfigure.SerialNumber, (object) instrumentToConfigure.FirmwareBuildNumber);
    }
    if (firmwareImage != null && ((IEnumerable<byte>) firmwareImage.Image).Count<byte>() > 0)
      newFirmware = firmwareImage.Image;
    return newFirmware;
  }

  public void UpdateFirmware(IInstrument instrument)
  {
    byte[] newFirmware = Type1077Manager.GetNewFirmware(instrument);
    FirmwareUpdateStatus changedObject = new FirmwareUpdateStatus();
    changedObject.Status = FirmwareUpdateStatus.FirmwareUpdateStatusType.AlreadyOnInstrument;
    if (newFirmware.Length != 0)
    {
      this.UpdateFirmware(instrument, newFirmware);
      changedObject.Status = FirmwareUpdateStatus.FirmwareUpdateStatusType.Uploaded;
    }
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<FirmwareUpdateStatus>(changedObject, ChangeType.ItemEdited));
  }

  public void UpdateFirmware(IInstrument instrument, byte[] firmwareImage)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (!(instrument is Type1077Instrument))
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format(Resources.Type1077Manager_CantUpdateInstrument, (object) instrument.Name));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format(Resources.Type1077Manager_NoCommunicationChannelDefined, (object) instrument.Name));
    if (firmwareImage.Length == 0)
      return;
    try
    {
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.OpenMessagePanel, DataConverter.GetByteArray("Updating Firmware"));
      BinaryInstrumentCommand instrumentFirmware = InstrumentCommands.UpdateInstrumentFirmware;
      instrumentFirmware.ExecutionTimeout = (ushort) 20000;
      Type1077Manager.ExecuteCommand(instrument, instrumentFirmware, firmwareImage);
    }
    catch (System.Exception ex)
    {
      Type1077Manager.Logger.Error(ex, "Failure while updating firmware.");
    }
  }

  public void UploadInstrumentData(IInstrument instrument)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    MemoryStream dataStream = new MemoryStream();
    try
    {
      Type1077Manager.WriteSelectedPatientsAndRisks((Stream) dataStream);
      if (dataStream.ToArray().Length == 0)
        return;
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.AppendToInstrumentFlash, dataStream.ToArray(), (EventHandler<ChannelDataReceivedEventArgs>) null);
    }
    catch (CommandManagerException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while downloading data from [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("DataUploadFailure"), (System.Exception) ex);
    }
  }

  private static void WriteSelectedPatientsAndRisks(Stream dataStream)
  {
    if (dataStream == null)
      return;
    IEnumerable<object> array = (IEnumerable<object>) PatientManager.Instance.SelectedPatients.ToArray();
    if (array.Count<object>() == 0)
      return;
    try
    {
      List<DataExchangeTokenSet> exchangeTokenSetList1 = DataExchangeManager.Instance.MoveDataExchangeTokenSets(DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "Patient", "28928"), array);
      List<DataExchangeTokenSet> exchangeTokenSetList2 = DataExchangeManager.Instance.MoveDataExchangeTokenSets(DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "Patient", "30736"), array);
      List<DataExchangeTokenSet> exchangeTokenSetList3 = DataExchangeManager.Instance.MoveDataExchangeTokenSets(DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "Patient", "30208"), array);
      RecordDescription recordDesccription1 = DataExchangeManager.Instance.GetRecordDesccription("Type1077", "28928");
      RecordDescription recordDesccription2 = DataExchangeManager.Instance.GetRecordDesccription("Type1077", "30736");
      RecordDescription recordDesccription3 = DataExchangeManager.Instance.GetRecordDesccription("Type1077", "30208");
      Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter(dataStream, new RecordDescription[3]
      {
        recordDesccription1,
        recordDesccription2,
        recordDesccription3
      });
      try
      {
        type1077StreamWriter.Open();
        type1077StreamWriter.Write(recordDesccription1, exchangeTokenSetList1.ToArray());
        type1077StreamWriter.Write(recordDesccription2, exchangeTokenSetList2.ToArray());
        type1077StreamWriter.Write(recordDesccription3, exchangeTokenSetList3.ToArray());
      }
      finally
      {
        type1077StreamWriter.Close();
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("{1}{0}<i>{2}</i>", (object) Environment.NewLine, (object) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("Type1077_FailureWhileCreatingPatientsForInstrument"), (object) ex.Message), ex);
    }
  }

  public void DownloadProbeInformationFromInstrument(IInstrument instrument)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    try
    {
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.GetProbeInformation, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnProbeInformationDataAvailable));
    }
    catch (CommandManagerException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while downloading data from [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.Type1077Manager_DataDownloadFailure, (System.Exception) ex);
    }
  }

  private void OnProbeInformationDataAvailable(object sender, ChannelDataReceivedEventArgs e)
  {
    if (!(sender is Type1077CommandManager) || e == null)
      return;
    Type1077CommandManager type1077CommandManager = sender as Type1077CommandManager;
    List<DataExchangeTokenSet> dataExchangeTokenSets = new Type1077StreamReader((Stream) new MemoryStream(e.DataPackage.Elements.ToArray())).Read();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("Type1077", "PM", "21760", "Type1077ProbeInformation");
    Type1077Manager.Logger.Debug("Received data package for command {0} which is in state {1}.", (object) type1077CommandManager.Command, (object) type1077CommandManager.State);
    List<Type1077ProbeInformation> changedObject = DataExchangeManager.Instance.FetchEntities<Type1077ProbeInformation>(recordMap, (IEnumerable<DataExchangeTokenSet>) dataExchangeTokenSets);
    if (changedObject == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<List<Type1077ProbeInformation>>(changedObject, ChangeType.ListLoaded));
  }

  public void StartToneGenerator(IInstrument instrument, int speaker, int frequency)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    if (frequency < 1000 || frequency > 6000)
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (frequency));
    if (speaker >= 0)
    {
      if (speaker <= 1)
      {
        try
        {
          BinaryInstrumentCommand setFrequency = InstrumentCommands.SetFrequency;
          InstrumentToken instrumentToken = new InstrumentToken("21841", DataConverter.GetByteArray((uint) (frequency + speaker * 65536 /*0x010000*/)));
          setFrequency.Append(instrumentToken.RawData);
          Type1077Manager.ExecuteCommand(instrument, setFrequency, (byte[]) null, (EventHandler<ChannelDataReceivedEventArgs>) null);
          return;
        }
        catch (CommandManagerException ex)
        {
          Type1077Manager.Logger.Error((System.Exception) ex, "Failure while downloading data from [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
          throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.Type1077Manager_DataDownloadFailure, (System.Exception) ex);
        }
      }
    }
    throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (speaker));
  }

  public void StartFrequencyLevelToneGenerator(
    IInstrument instrument,
    ulong frequency1,
    ulong frequency2,
    ulong level1,
    ulong level2)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    if (frequency1 < 0UL || frequency1 > 8000UL)
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>("frequency");
    if (frequency2 < 0UL || frequency2 > 8000UL)
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>("frequency");
    if (level1 < 0UL || level1 > 50000UL)
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>("level");
    if (level2 >= 0UL)
    {
      if (level2 <= 50000UL)
      {
        try
        {
          BinaryInstrumentCommand frequencyAndLevel = InstrumentCommands.SetFrequencyAndLevel;
          InstrumentToken instrumentToken = new InstrumentToken("21847", DataConverter.GetByteArray((ulong) (0L + ((long) frequency1 << 48 /*0x30*/) + ((long) frequency2 << 32 /*0x20*/) + ((long) level1 << 16 /*0x10*/)) + level2));
          frequencyAndLevel.Append(instrumentToken.RawData);
          Type1077Manager.ExecuteCommand(instrument, frequencyAndLevel, (byte[]) null, (EventHandler<ChannelDataReceivedEventArgs>) null);
          return;
        }
        catch (CommandManagerException ex)
        {
          Type1077Manager.Logger.Error((System.Exception) ex, "Failure while downloading data from [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
          throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("DataDownloadFailure"), (System.Exception) ex);
        }
      }
    }
    throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>("level");
  }

  public void StopToneGenerator(Type1077Instrument instrument)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    try
    {
      Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.SwitchToneGeneratorOff, (byte[]) null, (EventHandler<ChannelDataReceivedEventArgs>) null);
    }
    catch (CommandManagerException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while downloading data from [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.Type1077Manager_DataDownloadFailure, (System.Exception) ex);
    }
  }

  public void GetMicrofoneRmsValue(IInstrument instrument, int microfone)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    if (microfone >= 0)
    {
      if (microfone <= 1)
      {
        try
        {
          Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.GetMicrophoneRmsValue, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnProbeRmsDataAvailable));
          return;
        }
        catch (CommandManagerException ex)
        {
          Type1077Manager.Logger.Error((System.Exception) ex, "Failure while downloading data from [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
          throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.Type1077Manager_DataDownloadFailure, (System.Exception) ex);
        }
      }
    }
    throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (microfone));
  }

  public void SendProbeConfiguration(
    Instrument instrument,
    Type1077ProbeInformation type1077ProbeInformation)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    if (type1077ProbeInformation == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (type1077ProbeInformation));
    try
    {
      type1077ProbeInformation.CalibrationDate = new DateTime?(DateTime.Now);
      BinaryInstrumentCommand calibrationValues = InstrumentCommands.SetProbeCalibrationValues;
      calibrationValues.Append(type1077ProbeInformation.ProbeRawData);
      Type1077Manager.ExecuteCommand((IInstrument) instrument, calibrationValues, (byte[]) null, (EventHandler<ChannelDataReceivedEventArgs>) null);
      type1077ProbeInformation = (Type1077ProbeInformation) null;
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<Type1077ProbeInformation>(type1077ProbeInformation, ChangeType.ListLoaded));
    }
    catch (InstrumentCommunicationException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while sending probe configuration to [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.Type1077Manager_DataDownloadFailure, (System.Exception) ex);
    }
    catch (CommandManagerException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while sending probe configuration to [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.Type1077Manager_DataDownloadFailure, (System.Exception) ex);
    }
  }

  private double GetProbeNormLevel(int frequency)
  {
    switch (frequency)
    {
      case 250:
        return this.probeSpkNormValues[0];
      case 500:
        return this.probeSpkNormValues[1];
      case 1000:
        return this.probeSpkNormValues[2];
      case 2000:
        return this.probeSpkNormValues[3];
      case 3000:
        return this.probeSpkNormValues[4];
      case 4000:
        return this.probeSpkNormValues[5];
      case 6000:
        return this.probeSpkNormValues[6];
      case 8000:
        return this.probeSpkNormValues[7];
      default:
        return 0.0;
    }
  }

  private double GetMicrofoneNormLevel(int frequency)
  {
    switch (frequency)
    {
      case 250:
        return this.probeMicNormValues[0];
      case 500:
        return this.probeMicNormValues[1];
      case 1000:
        return this.probeMicNormValues[2];
      case 2000:
        return this.probeMicNormValues[3];
      case 3000:
        return this.probeMicNormValues[4];
      case 4000:
        return this.probeMicNormValues[5];
      case 6000:
        return this.probeMicNormValues[6];
      case 8000:
        return this.probeMicNormValues[7];
      default:
        return 0.0;
    }
  }

  private double GetEccNormLevel(int frequency)
  {
    switch (frequency)
    {
      case 250:
        return this.eccSpkNormValues[0];
      case 500:
        return this.eccSpkNormValues[1];
      case 1000:
        return this.eccSpkNormValues[2];
      case 2000:
        return this.eccSpkNormValues[3];
      case 3000:
        return this.eccSpkNormValues[4];
      case 4000:
        return this.eccSpkNormValues[5];
      case 6000:
        return this.eccSpkNormValues[6];
      case 8000:
        return this.eccSpkNormValues[7];
      default:
        return 0.0;
    }
  }

  public void ComputeMicrofoneCorrectionValue(
    Instrument instrument,
    Type1077ProbeInformation probe,
    int frequency,
    double currentSoundLevel)
  {
    if (probe == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (probe));
    if (frequency != 1000)
      return;
    this.StartToneGenerator((IInstrument) instrument, 0, 1000);
    Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.GetMicrophoneRmsValue, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnProbeRmsDataAvailable));
    while (this.type1077ProbeInformationForRmsCalculation == null)
      Thread.Sleep(10);
    this.StopToneGenerator(instrument as Type1077Instrument);
    float? nullable = this.type1077ProbeInformationForRmsCalculation.MicrofoneRmsValue;
    nullable = nullable.HasValue ? this.type1077ProbeInformationForRmsCalculation.MicrofoneRmsValue : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("MicrosofenRmsValue");
    double num1 = (double) nullable.Value;
    double num2 = Math.Round(20.0 * Math.Log10(num1 / this.GetMicrofoneNormLevel(frequency)) - (currentSoundLevel - this.GetProbeNormLevel(frequency)), 0);
    if (Math.Abs(num2) > 4.0)
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format(Resources.Type1077Manager_ComputeMicrofoneCorrectionValueOutOf4dB, (object) num2));
    double num3 = Math.Round((20.0 * Math.Log10(num1 / this.GetMicrofoneNormLevel(frequency)) - (currentSoundLevel - this.GetProbeNormLevel(frequency))) * 8.0 / 3.0, 0);
    probe.Microfone1CorrectionValue1KHz = num3 >= (double) sbyte.MinValue && num3 <= (double) sbyte.MaxValue ? new sbyte?(Convert.ToSByte(num3)) : throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format(Resources.Type1077Manager_ComputeMicrofoneCorrectionValueOutOfRange, (object) num3));
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<Type1077ProbeInformation>(probe, ChangeType.ItemEdited));
  }

  private void OnProbeRmsDataAvailable(object sender, ChannelDataReceivedEventArgs e)
  {
    if (!(sender is Type1077CommandManager) || e == null)
      return;
    Type1077CommandManager type1077CommandManager = sender as Type1077CommandManager;
    List<DataExchangeTokenSet> dataExchangeTokenSets = new Type1077StreamReader((Stream) new MemoryStream(e.DataPackage.Elements.ToArray())).Read();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("Type1077", "PM", "21760", "Type1077ProbeInformation");
    Type1077Manager.Logger.Debug("Received data package for command {0} which is in state {1}.", (object) type1077CommandManager.Command, (object) type1077CommandManager.State);
    this.type1077ProbeInformationForRmsCalculation = DataExchangeManager.Instance.FetchEntities<Type1077ProbeInformation>(recordMap, (IEnumerable<DataExchangeTokenSet>) dataExchangeTokenSets).FirstOrDefault<Type1077ProbeInformation>();
  }

  public void ComputeCorrectionValue(
    Type1077ProbeInformation probe,
    int frequency,
    double currentSoundLevel,
    int speaker)
  {
    uint? nullable = probe != null ? probe.ProbeType : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (probe));
    double a;
    if (nullable.HasValue)
    {
      switch (nullable.GetValueOrDefault())
      {
        case 34304:
          a = (currentSoundLevel - this.GetEccNormLevel(frequency)) * 8.0 / 3.0;
          goto label_8;
        case 34577:
          a = (currentSoundLevel - this.GetProbeNormLevel(frequency)) * 8.0 / 3.0;
          goto label_8;
        case 34833:
          a = (currentSoundLevel - this.GetProbeNormLevel(frequency)) * 8.0 / 3.0;
          goto label_8;
      }
    }
    a = (currentSoundLevel - this.GetProbeNormLevel(frequency)) * 8.0 / 3.0;
label_8:
    double num1 = Math.Round(a * 3.0 / 8.0);
    if (Math.Abs(num1) > 4.0)
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format(Resources.Type1077Manager_ComputeSpeakerCorrectionValueOutOf4dB, (object) num1));
    double num2 = Math.Round(a);
    if (num2 < (double) sbyte.MinValue || num2 > (double) sbyte.MaxValue)
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format(Resources.Type1077Manager_ComputeSpeakerCorrectionValueDifferenceTooLarge, (object) currentSoundLevel, (object) this.GetProbeNormLevel(frequency)));
    if (speaker == 0)
    {
      switch (frequency)
      {
        case 1000:
          probe.Speaker1CorrectionValue1KHz = new sbyte?(Convert.ToSByte(num2));
          break;
        case 2000:
          probe.Speaker1CorrectionValue2KHz = new sbyte?(Convert.ToSByte(num2));
          break;
        case 4000:
          probe.Speaker1CorrectionValue4KHz = new sbyte?(Convert.ToSByte(num2));
          break;
        case 6000:
          probe.Speaker1CorrectionValue6KHz = new sbyte?(Convert.ToSByte(num2));
          break;
      }
    }
    if (speaker == 1)
    {
      switch (frequency)
      {
        case 1000:
          probe.Speaker2CorrectionValue1KHz = new sbyte?(Convert.ToSByte(num2));
          break;
        case 2000:
          probe.Speaker2CorrectionValue2KHz = new sbyte?(Convert.ToSByte(num2));
          break;
        case 4000:
          probe.Speaker2CorrectionValue4KHz = new sbyte?(Convert.ToSByte(num2));
          break;
        case 6000:
          probe.Speaker2CorrectionValue6KHz = new sbyte?(Convert.ToSByte(num2));
          break;
      }
    }
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<Type1077ProbeInformation>(probe, ChangeType.ItemEdited));
  }

  public void LoopBackCableTest(
    Instrument instrument,
    Type1077LoopBackCableInformation loopBackCableInformation)
  {
    double num1 = 0.0;
    double num2 = 0.9;
    double num3 = 1.1;
    Type1077LoopBackCableInformation cableInformation = new Type1077LoopBackCableInformation();
    if ((ushort) 29952 == instrument.InstrumentType.Value && instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    try
    {
      this.StartFrequencyLevelToneGenerator((IInstrument) instrument, 1000UL, 1000UL, 10000UL, 0UL);
      Thread.Sleep(2000);
      double num4 = 685.5606;
      double num5 = num4 * num2;
      double num6 = num4 * num3;
      double num7 = 18205.83;
      double num8 = num7 * num2;
      double num9 = num7 * num3;
      Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.GetLoopBackCableRmsValue, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnMicRmsDataAvailable));
      float? nullable1 = this.type1077LoopBackInformationForRmsCalculation.MicrofoneRmsValue;
      double num10 = (double) nullable1.Value;
      nullable1 = this.type1077LoopBackInformationForRmsCalculation.MicrofoneRms2Value;
      double num11 = (double) nullable1.Value;
      if (!loopBackCableInformation.TestAbrConnection)
        num11 = num7;
      cableInformation.LoopBackTest2 = num10 >= num5 && num10 <= num6 && num11 >= num8 && num11 <= num9;
      double num12 = 685.42555;
      double num13 = num12 * num2;
      double num14 = num12 * num3;
      double num15 = 18202.045;
      double num16 = num15 * num2;
      double num17 = num15 * num3;
      this.StartFrequencyLevelToneGenerator((IInstrument) instrument, 1000UL, 1000UL, 0UL, 10000UL);
      Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.GetLoopBackCableRmsValue, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnMicRmsDataAvailable));
      float? nullable2 = this.type1077LoopBackInformationForRmsCalculation.MicrofoneRmsValue;
      double num18 = (double) nullable2.Value;
      nullable2 = this.type1077LoopBackInformationForRmsCalculation.MicrofoneRms2Value;
      double num19 = (double) nullable2.Value;
      if (!loopBackCableInformation.TestAbrConnection)
        num19 = num15;
      cableInformation.LoopBackTest3 = num18 >= num13 && num18 <= num14 && num19 >= num16 && num19 <= num17;
      cableInformation.LoopBackTestPassed = cableInformation.LoopBackTest2 && cableInformation.LoopBackTest3 && loopBackCableInformation.CodecOutputLevel;
      if (loopBackCableInformation.TestAbrConnection)
      {
        double num20 = 9812.771;
        double num21 = num20 * num2;
        double num22 = num20 * num3;
        double num23 = 9731.927;
        double num24 = num23 * num2;
        double num25 = num23 * num3;
        Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.GetImpedanceInformation, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnMicRmsDataAvailable));
        nullable2 = this.type1077LoopBackInformationForRmsCalculation.AbrImp1Value;
        double num26 = (double) nullable2.Value;
        nullable2 = this.type1077LoopBackInformationForRmsCalculation.AbrImp2Value;
        double num27 = (double) nullable2.Value;
        cableInformation.LoopBackTest4 = num26 >= num21 && num26 <= num22 && num27 >= num24 && num27 <= num25;
        double num28 = 468.9555;
        double num29 = num28 * num2;
        double num30 = num28 * num3;
        double num31 = 16625.7075;
        double num32 = num31 * num2;
        double num33 = num31 * num3;
        this.StartFrequencyLevelToneGenerator((IInstrument) instrument, 200UL, 200UL, 10000UL, 0UL);
        Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.GetLoopBackCableRmsValue, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnMicRmsDataAvailable));
        nullable2 = this.type1077LoopBackInformationForRmsCalculation.MicrofoneRmsValue;
        double num34 = (double) nullable2.Value;
        nullable2 = this.type1077LoopBackInformationForRmsCalculation.MicrofoneRms2Value;
        double num35 = (double) nullable2.Value;
        cableInformation.LoopBackTest5 = num34 >= num29 && num34 <= num30 && num35 >= num32 && num35 <= num33;
        double num36 = 459.54705;
        double num37 = num36 * num2;
        double num38 = num36 * num3;
        double num39 = 16572.30125;
        double num40 = num39 * num2;
        double num41 = num39 * num3;
        this.StartFrequencyLevelToneGenerator((IInstrument) instrument, 200UL, 200UL, 0UL, 10000UL);
        Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.GetLoopBackCableRmsValue, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnMicRmsDataAvailable));
        nullable2 = this.type1077LoopBackInformationForRmsCalculation.MicrofoneRmsValue;
        double num42 = (double) nullable2.Value;
        nullable2 = this.type1077LoopBackInformationForRmsCalculation.MicrofoneRms2Value;
        double num43 = (double) nullable2.Value;
        cableInformation.LoopBackTest6 = num42 >= num37 && num42 <= num38 && num43 >= num40 && num43 <= num41;
        num1 = 150.0;
        double num44 = 0.0;
        double num45 = 500.0;
        this.StartFrequencyLevelToneGenerator((IInstrument) instrument, 200UL, 200UL, 10000UL, 10000UL);
        Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.GetLoopBackCableRmsValue, (byte[]) null, new EventHandler<ChannelDataReceivedEventArgs>(this.OnMicRmsDataAvailable));
        nullable2 = this.type1077LoopBackInformationForRmsCalculation.MicrofoneRms2Value;
        double num46 = (double) nullable2.Value;
        cableInformation.LoopBackTest7 = num46 >= num44 && num46 <= num45;
        cableInformation.LoopBackTestPassed = cableInformation.LoopBackTest2 && cableInformation.LoopBackTest3 && cableInformation.LoopBackTest4 && cableInformation.LoopBackTest5 && cableInformation.LoopBackTest6 && cableInformation.LoopBackTest7 && loopBackCableInformation.CodecOutputLevel;
      }
      this.type1077LoopBackInformationForRmsCalculation = cableInformation;
      loopBackCableInformation = cableInformation;
      if (this.Changed != null)
        this.Changed((object) this, ModelChangedEventArgs.Create<Type1077LoopBackCableInformation>(this.type1077LoopBackInformationForRmsCalculation, ChangeType.ItemEdited));
      this.StopToneGenerator(instrument as Type1077Instrument);
      if (!cableInformation.LoopBackTestPassed)
        throw ExceptionFactory.Instance.CreateException<ModelException>("The loop back cable test didn't pass complete!!");
      this.SetInstrumentServiceDate((IInstrument) instrument);
      Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.RebootInstrument);
      loopBackCableInformation = (Type1077LoopBackCableInformation) null;
    }
    catch (CommandManagerException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while downloading data from [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("DataDownloadFailure"), (System.Exception) ex);
    }
  }

  private void OnMicRmsDataAvailable(object sender, ChannelDataReceivedEventArgs e)
  {
    if (!(sender is Type1077CommandManager) || e == null)
      return;
    Type1077CommandManager type1077CommandManager = sender as Type1077CommandManager;
    List<DataExchangeTokenSet> dataExchangeTokenSets = new Type1077StreamReader((Stream) new MemoryStream(e.DataPackage.Elements.ToArray())).Read();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("Type1077", "PM", "21761", "Type1077LoopBackCableInformation");
    Type1077Manager.Logger.Debug("Received data package for command {0} which is in state {1}.", (object) type1077CommandManager.Command, (object) type1077CommandManager.State);
    this.type1077LoopBackInformationForRmsCalculation = DataExchangeManager.Instance.FetchEntities<Type1077LoopBackCableInformation>(recordMap, (IEnumerable<DataExchangeTokenSet>) dataExchangeTokenSets).FirstOrDefault<Type1077LoopBackCableInformation>();
  }

  public void SetInstrumentServiceDate(IInstrument instrument)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    try
    {
      BinaryInstrumentCommand setServiceDate = InstrumentCommands.SetServiceDate;
      DateTime dateTime = new DateTime();
      DateTime now = DateTime.Now;
      InstrumentToken instrumentToken = new InstrumentToken("21848", DataConverter.GetByteArray((uint) (0 + (now.Year - 1900 << 24) + (now.Month << 16 /*0x10*/) + (now.Day << 8))));
      setServiceDate.Append(instrumentToken.RawData);
      Type1077Manager.ExecuteCommand(instrument, setServiceDate, (byte[]) null, (EventHandler<ChannelDataReceivedEventArgs>) null);
    }
    catch (CommandManagerException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while downloading data from [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("DataDownloadFailure"), (System.Exception) ex);
    }
  }

  public void EraseInstrumentFlash(Type1077Instrument instrument, bool force)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    try
    {
      this.DoEraseFlash((IInstrument) instrument, force);
    }
    catch (CommandManagerException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while deleting memory of [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("InstrumentFlashDeletionError"), (System.Exception) ex);
    }
  }

  public void DeleteDeviceMemory(Type1077Instrument instrument)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    try
    {
      Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.EraseInstrumentFlash);
      Type1077Manager.ExecuteCommand((IInstrument) instrument, InstrumentCommands.RebootInstrument);
    }
    catch (CommandManagerException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while deleting memory of [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("DataDownloadFailure"), (System.Exception) ex);
    }
  }

  public void selectedFirmwareFile(IInstrument Instrument, string File)
  {
    if (Instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument");
    if (Instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    if (File != null)
      this.firmwareImage = FirmwareManager.Instance.LoadFirmwareFromFile(File);
    else
      this.firmwareImage = (FirmwareImage) null;
  }

  public void uploadSelectedfirmware(IInstrument Instrument)
  {
    if (Instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument");
    if (Instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    if (this.firmwareImage == null)
      return;
    this.UploadFirmware(Instrument, this.firmwareImage.Image);
  }

  public void UploadFirmware(IInstrument instrument, byte[] firmwareImage)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (!(instrument is Type1077Instrument))
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format(Resources.Type1077Manager_CantUpdateInstrument, (object) instrument.Name));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format(Resources.Type1077Manager_NoCommunicationChannelDefined, (object) instrument.Name));
    if (firmwareImage.Length == 0)
      return;
    try
    {
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.OpenMessagePanel, DataConverter.GetByteArray("Uploading Firmware"));
      BinaryInstrumentCommand instrumentFirmware = InstrumentCommands.UpdateInstrumentFirmware;
      instrumentFirmware.ExecutionTimeout = (ushort) 20000;
      Type1077Manager.ExecuteCommand(instrument, instrumentFirmware, firmwareImage);
    }
    catch (System.Exception ex)
    {
      Type1077Manager.Logger.Error(ex, "Failure while uploading firmware.");
    }
  }

  public void setFirmwareLicence(IInstrument instrument, string licence)
  {
    if (instrument == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (instrument));
    if (instrument.CommunicationChannel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("instrument.CommunicationChannel");
    try
    {
      if (licence.Length != 16 /*0x10*/)
        return;
      byte[] bytes = new ASCIIEncoding().GetBytes(licence);
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.SetFirmwareLicence, bytes);
      Type1077Manager.ExecuteCommand(instrument, InstrumentCommands.RebootInstrument);
    }
    catch (CommandManagerException ex)
    {
      Type1077Manager.Logger.Error((System.Exception) ex, "Failure while downloading data from [{0}] connected to {1}", (object) instrument.Name, (object) instrument.CommunicationChannel.Name);
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("DataDownloadFailure"), (System.Exception) ex);
    }
  }

  private void DoEraseFlash(IInstrument instrument, bool force)
  {
    if (!(instrument is Type1077Instrument type1077Instrument) || type1077Instrument.UsedStorage == 0UL)
      return;
    if (type1077Instrument.GlobalInstrumentConfiguration.DeletionRule == InstrumentDataDeletionRule.Manual)
    {
      if (!this.EraseFlashDataOnInstrument || type1077Instrument.NumberOfPatientsOnInstrument != 0 || type1077Instrument.NumberOfTestsOnInstrument != 0)
        return;
    }
    else if (type1077Instrument.GlobalInstrumentConfiguration.DeletionRule != InstrumentDataDeletionRule.AfterSuccessfullDownload || (!this.EraseFlashDataOnInstrument || type1077Instrument.NumberOfPatientsOnInstrument != 0 || type1077Instrument.NumberOfTestsOnInstrument != 0) && !force)
      return;
    BinaryInstrumentCommand eraseInstrumentFlash = InstrumentCommands.EraseInstrumentFlash;
    int num = 12;
    if (type1077Instrument.UsedStorage > 0UL)
      num = (int) (Math.Ceiling((double) type1077Instrument.UsedStorage / (double) ushort.MaxValue) * 12.0);
    eraseInstrumentFlash.ExecutionTimeout = eraseInstrumentFlash.AcknowledgeTimeout = (ushort) (num * 1000);
    Type1077Manager.ExecuteCommand(instrument, eraseInstrumentFlash, (byte[]) null, (EventHandler<ChannelDataReceivedEventArgs>) null);
    type1077Instrument.NumberOfPatientsOnInstrument = 0;
    type1077Instrument.NumberOfTestsOnInstrument = 0;
    type1077Instrument.UsedStorage = 0UL;
  }

  private static void ExecuteCommand(IInstrument instrument, BinaryInstrumentCommand command)
  {
    Type1077Manager.ExecuteCommand(instrument, command, (byte[]) null);
  }

  private static void ExecuteCommand(
    IInstrument instrument,
    BinaryInstrumentCommand command,
    byte[] commandData)
  {
    Type1077Manager.ExecuteCommand(instrument, command, commandData, (EventHandler<ChannelDataReceivedEventArgs>) null);
  }

  private static void ExecuteCommand(
    ICommunicationChannel channel,
    BinaryInstrumentCommand command,
    byte[] commandData,
    EventHandler<ChannelDataReceivedEventArgs> dataAvailableEventHandler)
  {
    Type1077Manager.ExecuteCommand((IInstrument) new Instrument()
    {
      Name = "Dummy",
      CommunicationChannel = channel
    }, command, commandData, dataAvailableEventHandler);
  }

  private static void ExecuteCommand(
    IInstrument instrument,
    BinaryInstrumentCommand command,
    byte[] commandData,
    EventHandler<ChannelDataReceivedEventArgs> dataAvailableEventHandler)
  {
    Type1077CommandManager type1077CommandManager = new Type1077CommandManager()
    {
      CommunicationChannel = instrument.CommunicationChannel,
      Instrument = instrument as Type1077Instrument
    };
    if (dataAvailableEventHandler != null)
      type1077CommandManager.CommandDataAvailableEventHandler += dataAvailableEventHandler;
    if (commandData != null && commandData.Length != 0)
      command.Append(commandData);
    try
    {
      type1077CommandManager.Connect();
      type1077CommandManager.Execute(command);
      switch (type1077CommandManager.State)
      {
        case CommandState.Failed:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(string.Format(Resources.Type1077Manager_ExecuteCommand_Failed, (object) command.Name));
        case CommandState.InstrumentAbort:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(string.Format(Resources.Type1077Manager_ExecuteCommand_InstrumentAbort, (object) instrument.CommunicationChannel.Name, (object) command.Name));
        case CommandState.TransmissionError:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(string.Format(Resources.Type1077Manager_ExecuteCommand_TransmissionError, (object) command.Name));
        case CommandState.TimedOut:
          throw ExceptionFactory.Instance.CreateException<InstrumentCommunicationException>(string.Format(Resources.Type1077Manager_ExecuteCommand_Timeout, (object) command.Name));
      }
    }
    finally
    {
      type1077CommandManager.Disconnect();
    }
  }

  public List<DataExchangeSetMap> RecordSetMaps { get; protected set; }

  public List<RecordDescriptionSet> RecordDescriptionSets { get; protected set; }

  private void LoadDataExchangeDescriptions()
  {
    this.RecordDescriptionSets = new List<RecordDescriptionSet>();
    this.RecordSetMaps = new List<DataExchangeSetMap>();
    this.RecordDescriptionSets.Add(RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.Type1077.DataExchange.InstrumentDataDescription.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.Type1077Manager_LoadDataExchangeDescriptions_StructureFileNotFound)));
    this.RecordDescriptionSets.Add(RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.Type1077.DataExchange.PluginDataDescription.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.Type1077Manager_LoadDataExchangeDescriptions_StructureFileNotFound)));
    this.RecordSetMaps.AddRange((IEnumerable<DataExchangeSetMap>) DataExchangeSetMap.LoadSetsFromXml(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.Type1077.DataExchange.PluginDataMapping.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.Type1077Manager_LoadDataExchangeDescriptions_MappingFileNotFound)));
  }
}
