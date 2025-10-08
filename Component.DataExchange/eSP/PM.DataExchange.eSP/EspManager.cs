// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.EspManager
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using PathMedical.AudiologyTest;
using PathMedical.DatabaseManagement;
using PathMedical.DataExchange.eSP.EspWebService;
using PathMedical.DataExchange.eSP.Properties;
using PathMedical.DataExchange.eSP.Service;
using PathMedical.Encryption;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.Logging;
using PathMedical.Login;
using PathMedical.PatientManagement;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

#nullable disable
namespace PathMedical.DataExchange.eSP;

public class EspManager : ISingleEditingModel, IModel
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (EspManager), "$Rev$");
  private Patient[] exportedPatients;
  private readonly XmlHelper xmlHelper = new XmlHelper();
  private Dictionary<Guid, int> espTestIds = new Dictionary<Guid, int>();

  public static EspManager Instance => PathMedical.Singleton.Singleton<EspManager>.Instance;

  private EspManager()
  {
    ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) ((_param1, _param2, _param3, _param4) => true);
  }

  public void ImportConfiguration()
  {
    EspConfiguration espConfiguration = EspConfigurationManager.Instance.EspConfiguration;
    if (espConfiguration == null)
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_EspConfigurationMissing"));
    if (string.IsNullOrEmpty(espConfiguration.HomeSite))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_EspConfigurationSiteMissing"));
    if (string.IsNullOrEmpty(espConfiguration.EspRemoteAddress))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_EspRemoteAddressMissing"));
    this.ImportConfiguration(espConfiguration);
  }

  private void ImportConfiguration(EspConfiguration espConfiguration)
  {
    DataUpload dataUpload = new DataUpload()
    {
      Url = espConfiguration.EspRemoteAddress
    };
    CSPCHD cspchd = new CSPCHD()
    {
      id = Guid.NewGuid().ToString()
    };
    string str1 = EspManager.SerializeEntity((object) new EspSyncAllRequest()
    {
      ManufacturerId = "5",
      SiteCode = espConfiguration.HomeSite,
      MessageRef = cspchd.id
    });
    if (!this.IsMessageValid(str1, "PathMedical.DataExchange.eSP.Service.eSPSyncAllRequest.xsd"))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_EspConfigurationValidationFailed"));
    try
    {
      downloadSyncDataResponseDownloadSyncDataResult downloadSyncDataResult = dataUpload.downloadSyncData(str1);
      if (downloadSyncDataResult == null || downloadSyncDataResult.Text.Length == 0)
        return;
      string str2 = downloadSyncDataResult.Text[0];
      if (this.IsMessageValid(str2, "PathMedical.DataExchange.eSP.Service.eSPSyncAllResponse.xsd"))
      {
        EspSyncAllResponse syncAllResponse = EspManager.DeserializeEntity<EspSyncAllResponse>(str2);
        if (syncAllResponse == null)
          return;
        EspManager.ProcessConfiguration(espConfiguration, syncAllResponse);
      }
      else
      {
        EspManager.Logger.Error("Failure while receiving SyncAllRequest response: ", (object) str2);
        throw ExceptionFactory.Instance.CreateException<ModelException>($"{"eSP didn't accept the configuration request."}");
      }
    }
    catch (FaultException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("{1}{0}<i>{2}</i>", (object) Environment.NewLine, (object) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_EspCommunicationFailure"), (object) ex.Message), (System.Exception) ex);
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("{1}{0}<i>{2}</i>", (object) Environment.NewLine, (object) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_EspCommunicationFailure"), (object) ex.Message), ex);
    }
  }

  private static void ProcessConfiguration(
    EspConfiguration espConfiguration,
    EspSyncAllResponse syncAllResponse)
  {
    if (syncAllResponse == null)
      return;
    Site site = (Site) null;
    try
    {
      SiteManager.Instance.RefreshData();
      site = SiteManager.Instance.Sites.FirstOrDefault<Site>((Func<Site, bool>) (s => string.Compare(s.Code, espConfiguration.HomeSite) == 0 && !s.Inactive.HasValue));
      if (site == null)
      {
        Guid guid = new Guid(MD5Engine.GetMd5HashBinary(espConfiguration.HomeSite));
        site = new Site()
        {
          Id = guid,
          Name = espConfiguration.HomeSite,
          Code = espConfiguration.HomeSite
        };
        SiteManager.Instance.Import(new List<Site>()
        {
          site
        });
      }
      foreach (Site site2 in SiteManager.Instance.Sites.Where<Site>((Func<Site, bool>) (s => !s.Inactive.HasValue && s.Id != site.Id)))
      {
        site2.Inactive = new DateTime?(DateTime.Now);
        SiteManager.Instance.Store(site2);
        Site site1 = site2;
        FacilityManager.Instance.RefreshData();
        foreach (Facility facility in FacilityManager.Instance.Facilities.Where<Facility>((Func<Facility, bool>) (f =>
        {
          Guid? siteId = f.SiteId;
          Guid id = site1.Id;
          return siteId.HasValue && siteId.GetValueOrDefault() == id;
        })).ToList<Facility>())
        {
          facility.LocationTypes = (List<LocationType>) null;
          facility.Inactive = new DateTime?(DateTime.Now);
          FacilityManager.Instance.Store(facility);
        }
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("SiteImportFailure"), (object) ex.Message), ex);
    }
    try
    {
      Dictionary<string, Facility> dictionary = new Dictionary<string, Facility>();
      List<Facility> second = new List<Facility>();
      FacilityManager.Instance.RefreshData();
      foreach (FacilityDetails facilityDetails in (IEnumerable<FacilityDetails>) ((IEnumerable<FacilityDetails>) syncAllResponse.SiteFacilityData).OrderBy<FacilityDetails, string>((Func<FacilityDetails, string>) (f => f.FacilityId)))
      {
        string facilityName = facilityDetails.FacilityName;
        Facility facility = FacilityManager.Instance.Facilities.FirstOrDefault<Facility>((Func<Facility, bool>) (f => string.Compare(f.Name, facilityName) == 0));
        if (facility == null)
        {
          if (dictionary.ContainsKey(facilityDetails.FacilityId))
          {
            facility = dictionary[facilityDetails.FacilityId];
          }
          else
          {
            Guid guid = Guid.Empty;
            if (!string.IsNullOrEmpty(facilityDetails.FacilityId))
              guid = new Guid(MD5Engine.GetMd5HashBinary(facilityDetails.FacilityId));
            facility = new Facility()
            {
              Id = guid,
              Name = facilityDetails.FacilityName,
              Code = facilityDetails.FacilityId,
              Site = site,
              LocationTypes = new List<LocationType>()
            };
            if (!string.IsNullOrEmpty(facilityDetails.FacilityId))
              dictionary.Add(facilityDetails.FacilityId, facility);
            second.Add(facility);
          }
        }
        else if (!second.Contains(facility))
          second.Add(facility);
        if (!string.IsNullOrEmpty(facilityDetails.FacilityType))
        {
          FacilityDetails details = facilityDetails;
          LocationType locationType = LocationTypeManager.Instance.LocationTypes.FirstOrDefault<LocationType>((Func<LocationType, bool>) (l => string.Compare(l.Name, details.FacilityType) == 0));
          if (locationType != null && facility.LocationTypes != null && !facility.LocationTypes.Contains(locationType))
            facility.LocationTypes.Add(locationType);
        }
      }
      if (dictionary.Values.Count > 0)
        FacilityManager.Instance.Import(dictionary.Values.ToList<Facility>());
      FacilityManager.Instance.RefreshData();
      List<Facility> list = FacilityManager.Instance.Facilities.Except<Facility>((IEnumerable<Facility>) second).Where<Facility>((Func<Facility, bool>) (f => !f.Inactive.HasValue)).ToList<Facility>();
      if (list.Count > 0)
      {
        foreach (Facility facility in list)
          FacilityManager.Instance.Delete(facility);
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("FacilityImportFailure"), (object) ex.Message), ex);
    }
    try
    {
      List<User> users = new List<User>();
      List<User> second = new List<User>();
      foreach (UserDetails deviceUser in syncAllResponse.DeviceUsers)
      {
        UserDetails detail = deviceUser;
        User user1 = UserManager.Instance.Users.FirstOrDefault<User>((Func<User, bool>) (u => string.Compare(u.LoginId, detail.EspUserID) == 0));
        if (user1 == null)
        {
          Guid guid = Guid.Empty;
          if (!string.IsNullOrEmpty(detail.EspUserID))
            guid = new Guid(MD5Engine.GetMd5HashBinary(detail.EspUserID));
          User user2 = new User()
          {
            Id = guid,
            LoginId = detail.EspUserID,
            LoginName = detail.ScreenerId,
            Profile = espConfiguration.DefaultUserProfile,
            PasswordSalt = new Guid?(Guid.NewGuid())
          };
          user2.Password = LoginManager.Instance.EncryptPassword(espConfiguration.DefaultUserPassword, user2.PasswordSalt);
          users.Add(user2);
          second.Add(user2);
        }
        else
          second.Add(user1);
      }
      if (users.Count > 0)
        UserManager.Instance.Import(users);
      UserManager.Instance.RefreshData();
      List<User> list = UserManager.Instance.Users.Except<User>((IEnumerable<User>) second).Where<User>((Func<User, bool>) (f => f.IsActive)).ToList<User>();
      if (list.Count > 0)
      {
        foreach (User user in list)
        {
          user.IsActive = false;
          UserManager.Instance.Store(user);
        }
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("UserImportFailure"), (object) ex.Message), ex);
    }
    try
    {
      List<Instrument> instruments = new List<Instrument>();
      List<Instrument> second = new List<Instrument>();
      foreach (DeviceDetails screeningDevice in syncAllResponse.ScreeningDevices)
      {
        DeviceDetails details = screeningDevice;
        Instrument instrument1 = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i => string.Compare(i.Code, details.EspDeviceCode) == 0));
        if (instrument1 == null)
        {
          uint result = 0;
          uint.TryParse(details.SerialNumber, out result);
          string str = !uint.TryParse(details.SerialNumber, out result) ? details.SerialNumber : Convert.ToString(result);
          Guid guid = new Guid(MD5Engine.GetMd5HashBinary(details.EspDeviceCode));
          Instrument instrument2 = new Instrument()
          {
            Id = guid,
            Code = details.EspDeviceCode,
            Name = details.DeviceName,
            SerialNumber = str,
            InstrumentType = new ushort?((ushort) 29952),
            InstrumentTypeSignature = new Guid?(new Guid("BAC087B8-331E-4cd3-8F6E-12A9C13E7E61")),
            Site = site
          };
          instruments.Add(instrument2);
          second.Add(instrument2);
        }
        else
          second.Add(instrument1);
      }
      if (instruments.Count > 0)
        InstrumentManager.Instance.Import(instruments);
      InstrumentManager.Instance.RefreshData();
      List<Instrument> list = InstrumentManager.Instance.Instruments.Except<Instrument>((IEnumerable<Instrument>) second).ToList<Instrument>();
      if (list.Count > 0)
      {
        foreach (Instrument instrument in list)
          InstrumentManager.Instance.Delete(instrument);
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("InstrumentImportFailure"), (object) ex.Message), ex);
    }
    try
    {
      LocationTypeManager.Instance.RefreshData();
      IEnumerable<string> strings = ((IEnumerable<FacilityDetails>) syncAllResponse.SiteFacilityData).Select<FacilityDetails, string>((Func<FacilityDetails, string>) (f => f.FacilityType)).Distinct<string>();
      List<LocationType> second = new List<LocationType>();
      List<LocationType> import = new List<LocationType>();
      foreach (string text in strings)
      {
        string name = text;
        LocationType locationType1 = LocationTypeManager.Instance.LocationTypes.FirstOrDefault<LocationType>((Func<LocationType, bool>) (f => string.Compare(f.Name, name) == 0));
        if (locationType1 == null)
        {
          Guid guid = new Guid(MD5Engine.GetMD5Hash(text));
          LocationType locationType2 = new LocationType()
          {
            Id = guid,
            Name = text,
            Code = text
          };
          import.Add(locationType2);
          second.Add(locationType2);
        }
        else
          second.Add(locationType1);
      }
      if (import.Count > 0)
        LocationTypeManager.Instance.Import(import);
      LocationTypeManager.Instance.RefreshData();
      List<LocationType> list = LocationTypeManager.Instance.LocationTypes.Except<LocationType>((IEnumerable<LocationType>) second).ToList<LocationType>();
      if (list.Count > 0)
      {
        foreach (LocationType location in list)
          LocationTypeManager.Instance.Delete(location);
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("LocationImportFailure"), (object) ex.Message), ex);
    }
    try
    {
      List<RiskIndicator> indicators = new List<RiskIndicator>();
      List<RiskIndicator> second = new List<RiskIndicator>();
      RiskIndicatorManager.Instance.RefreshData();
      CultureInfo baseLanguage = SystemConfigurationManager.Instance.BaseLanguage;
      int num = 0;
      foreach (RiskFactor riskFactor in syncAllResponse.RiskFactors)
      {
        RiskFactor risk = riskFactor;
        RiskIndicator riskIndicator1 = RiskIndicatorManager.Instance.RiskIndicatorList.FirstOrDefault<RiskIndicator>((Func<RiskIndicator, bool>) (r => string.Compare(r.Name, risk.RiskFactorValue) == 0));
        if (riskIndicator1 == null)
        {
          RiskIndicator riskIndicator2 = new RiskIndicator();
          riskIndicator2.Id = new Guid(MD5Engine.GetMd5HashBinary(riskFactor.RiskFactorValue));
          riskIndicator2.SetName(baseLanguage, riskFactor.RiskFactorValue);
          riskIndicator2.SetDescription(baseLanguage, riskFactor.RiskFactorValue);
          riskIndicator2.PreventScreening = new bool?(false);
          riskIndicator2.IsActive = new bool?(true);
          riskIndicator2.OrderNumber = new int?(num);
          indicators.Add(riskIndicator2);
          second.Add(riskIndicator2);
        }
        else
        {
          riskIndicator1.IsActive = new bool?(true);
          riskIndicator1.OrderNumber = new int?(num);
          indicators.Add(riskIndicator1);
          second.Add(riskIndicator1);
        }
        ++num;
      }
      if (indicators.Count > 0)
        RiskIndicatorManager.Instance.Import(indicators);
      RiskIndicatorManager.Instance.RefreshData();
      List<RiskIndicator> list = RiskIndicatorManager.Instance.RiskIndicatorList.Except<RiskIndicator>((IEnumerable<RiskIndicator>) second).Where<RiskIndicator>((Func<RiskIndicator, bool>) (r => r.IsActive.GetValueOrDefault())).ToList<RiskIndicator>();
      if (list.Count <= 0)
        return;
      foreach (RiskIndicator risk in list)
      {
        risk.IsActive = new bool?(false);
        risk.OrderNumber = new int?(int.MaxValue);
        RiskIndicatorManager.Instance.Store(risk);
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RiskIndictorImportFailure"), (object) ex.Message), ex);
    }
  }

  public void Export()
  {
    EspConfiguration espConfiguration = EspConfigurationManager.Instance.EspConfiguration;
    if (espConfiguration == null)
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_EspConfigurationMissing"));
    if (string.IsNullOrEmpty(espConfiguration.HomeSite))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_EspConfigurationSiteMissing"));
    if (string.IsNullOrEmpty(espConfiguration.EspRemoteAddress))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_EspRemoteAddressMissing"));
    this.ExportTests(espConfiguration);
    this.ImportConfiguration(espConfiguration);
  }

  private void ExportTests(EspConfiguration espConfiguration)
  {
    DataUpload dataUpload = new DataUpload()
    {
      Url = espConfiguration.EspRemoteAddress
    };
    Guid transmissionId = Guid.NewGuid();
    new CSPCHD().id = Convert.ToString((object) transmissionId);
    string str = EspManager.SerializeEntity((object) this.GenerateEspTests(transmissionId));
    if (!string.IsNullOrEmpty(str))
      str = str.Replace(" xsi:nil=\"true\"", string.Empty).Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", " xmlns=\"http://tempuri.org/XMLSchema.xsd\"");
    if (!this.IsMessageValid(str, "PathMedical.DataExchange.eSP.Service.TestResult.xsd"))
      throw ExceptionFactory.Instance.CreateException<ModelException>($"{"Tests can't be exported to eSP since the validation of the eSP test result failed."}{Environment.NewLine}{this.xmlHelper.ValidationErrorMessage}");
    try
    {
      uploadDataResponseUploadDataResult uploadDataResult = dataUpload.uploadData(str);
      if (uploadDataResult == null || uploadDataResult.Text.Length == 0)
        return;
      foreach (string entitySerialization in uploadDataResult.Text)
      {
        ESpAcknowledgement espAcknowledgement = EspManager.DeserializeEntity<ESpAcknowledgement>(entitySerialization);
        if (string.Compare(espAcknowledgement.ResultStatus, "pass") != 0)
          throw ExceptionFactory.Instance.CreateException<ModelException>($"{"Tests results haven't been sent to eSP."}{Environment.NewLine}{espAcknowledgement.FailureDetails}");
      }
      using (DBScope dbScope = new DBScope())
      {
        foreach (Patient exportedPatient in this.exportedPatients)
          PatientManager.Instance.DeletePatient(exportedPatient);
        dbScope.Complete();
      }
    }
    catch (FaultException ex)
    {
      EspManager.Logger.Error((System.Exception) ex, Resources.EspManager_EspCommunicationFailure);
      throw;
    }
    catch (ModelException ex)
    {
      throw;
    }
    catch (System.Exception ex)
    {
      EspManager.Logger.Error(ex, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_EspCommunicationFailure"));
      throw;
    }
  }

  private EspTestResults GenerateEspTests(Guid transmissionId)
  {
    Patient[] array = PatientManager.Instance.AllPatients.Where<Patient>((Func<Patient, bool>) (p => p.AudiologyTests != null && p.AudiologyTests.Count > 0)).ToArray<Patient>();
    this.exportedPatients = array;
    this.espTestIds = new Dictionary<Guid, int>();
    List<EspTestResultType> espTestResultTypeList = new List<EspTestResultType>();
    foreach (Patient patient in array)
    {
      if (patient.Id == new Guid("94BEA8ED-1F53-4652-B748-1E743611F0A1") || patient.Id == new Guid("FC1599F1-EBFC-4608-8625-1944B6010838") || patient.Id == new Guid("60E9AA98-1B07-4e5f-AFED-47F813F65CCA") || patient.Id == new Guid("7E6E7731-0716-4859-BFD1-27C6234F7CA2"))
        patient.PatientContact.DateOfBirth = new DateTime?(new DateTime(1899, 12, 31 /*0x1F*/, 0, 0, 0));
      Patient patient1 = patient;
      espTestResultTypeList.AddRange(patient.AudiologyTests.Select<AudiologyTestInformation, EspTestResultType>((Func<AudiologyTestInformation, EspTestResultType>) (audiologyTest => this.CreateEspResultType(transmissionId, patient1, audiologyTest))).Where<EspTestResultType>((Func<EspTestResultType, bool>) (resultType => resultType != null)));
    }
    return new EspTestResults()
    {
      EspTestResult = espTestResultTypeList.ToArray()
    };
  }

  private EspTestResultType CreateEspResultType(
    Guid transmissionId,
    Patient patient,
    AudiologyTestInformation audiologyTestInformation)
  {
    if (audiologyTestInformation == null)
      return (EspTestResultType) null;
    if (patient == null)
      return (EspTestResultType) null;
    EspTestResultType espResultType = new EspTestResultType()
    {
      MessageRef = Convert.ToString((object) transmissionId)
    };
    PatientType patientType = new PatientType();
    espResultType.Patient = patientType;
    espResultType.Patient.Forename = patient.PatientContact.Forename1;
    espResultType.Patient.Surname = patient.PatientContact.Surname;
    espResultType.Patient.FullName = patient.PatientContact.FullName;
    espResultType.Patient.Title = patient.PatientContact.Title;
    espResultType.Patient.Dob = patient.PatientContact.DateOfBirth;
    espResultType.Patient.PatientId = new PatientIdType();
    espResultType.Patient.PatientId.NhsId = patient.PatientRecordNumber;
    if (!string.IsNullOrEmpty(patient.HospitalId))
      espResultType.Patient.PatientId.LocalId = patient.HospitalId;
    if (patient.ConsentStatus.HasValue)
    {
      ConsentStatus? consentStatus = patient.ConsentStatus;
      if (consentStatus.HasValue)
      {
        switch (consentStatus.GetValueOrDefault())
        {
          case ConsentStatus.Screening:
            espResultType.Patient.Consent = new ConsentType?(ConsentType.Screening);
            break;
          case ConsentStatus.Full:
            espResultType.Patient.Consent = new ConsentType?(ConsentType.Full);
            break;
        }
      }
    }
    NicuStatus? nicuStatus = patient.NicuStatus;
    if (nicuStatus.HasValue)
    {
      switch (nicuStatus.GetValueOrDefault())
      {
        case NicuStatus.No:
          espResultType.Patient.Nicu = new YesNoType?(YesNoType.No);
          break;
        case NicuStatus.Yes:
          espResultType.Patient.Nicu = new YesNoType?(YesNoType.Yes);
          break;
      }
    }
    List<RiskFactorType> riskFactorTypeList = new List<RiskFactorType>();
    if (patient.RiskIndicators != null)
    {
      foreach (RiskIndicator riskIndicator in patient.RiskIndicators.Where<RiskIndicator>((Func<RiskIndicator, bool>) (r => r.IsActive.GetValueOrDefault() && r.PatientRiskIndicatorValue != 0)))
      {
        RiskFactorType riskFactorType = new RiskFactorType()
        {
          RiskFactorId = riskIndicator.Name
        };
        switch (riskIndicator.PatientRiskIndicatorValue)
        {
          case RiskIndicatorValueType.Unknown:
            riskFactorType.RiskFactorValue = "U";
            break;
          case RiskIndicatorValueType.No:
            riskFactorType.RiskFactorValue = "N";
            break;
          case RiskIndicatorValueType.Yes:
            riskFactorType.RiskFactorValue = "Y";
            break;
        }
        riskFactorTypeList.Add(riskFactorType);
      }
    }
    patientType.RiskFactors = riskFactorTypeList.ToArray();
    byte[] testImage = new byte[0];
    ITestPlugin testPlugin = SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().FirstOrDefault<ITestPlugin>((Func<ITestPlugin, bool>) (p =>
    {
      Guid testTypeSignature1 = p.TestTypeSignature;
      Guid? testTypeSignature2 = audiologyTestInformation.TestTypeSignature;
      return testTypeSignature2.HasValue && testTypeSignature1 == testTypeSignature2.GetValueOrDefault();
    }));
    if (testPlugin != null && testPlugin is ISupportPluginDataExchange)
    {
      object obj = (testPlugin as ISupportPluginDataExchange).Export(ExportType.BinaryImage, audiologyTestInformation.TestDetailId);
      if (obj is Array)
        testImage = (byte[]) obj;
    }
    ITestInformation testInformation = (ITestInformation) null;
    if (testPlugin != null)
      testInformation = testPlugin.GetTestInformation(audiologyTestInformation.TestDetailId) as ITestInformation;
    PathMedical.DataExchange.eSP.Service.TestType espTestResult = this.CreateEspTestResult(audiologyTestInformation, testImage);
    if (espTestResult != null)
      espResultType.Patient.Tests = new PathMedical.DataExchange.eSP.Service.TestType[1]
      {
        espTestResult
      };
    User user = UserManager.Instance.Users.FirstOrDefault<User>((Func<User, bool>) (u =>
    {
      Guid id = u.Id;
      Guid? userAccountId = audiologyTestInformation.UserAccountId;
      return userAccountId.HasValue && id == userAccountId.GetValueOrDefault();
    }));
    int result;
    if (user != null && int.TryParse(user.LoginId, out result))
      espResultType.Screener = new ScreenerType()
      {
        EspUserID = result,
        ScreenerId = user.LoginName
      };
    if (testInformation != null)
    {
      Instrument instrument = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i =>
      {
        long uint32 = (long) Convert.ToUInt32(i.SerialNumber);
        long? instrumentSerialNumber = testInformation.InstrumentSerialNumber;
        long valueOrDefault = instrumentSerialNumber.GetValueOrDefault();
        return uint32 == valueOrDefault & instrumentSerialNumber.HasValue;
      }));
      if (instrument != null)
        espResultType.Equipment = new EquipmentType()
        {
          EspDeviceCode = Convert.ToInt32(instrument.Code),
          ProbeId = Convert.ToString((object) testInformation.ProbeSerialNumber)
        };
    }
    if (audiologyTestInformation.FacilityId.HasValue)
    {
      Facility facility = FacilityManager.Instance.Facilities.FirstOrDefault<Facility>((Func<Facility, bool>) (f =>
      {
        Guid id = f.Id;
        Guid? facilityId = audiologyTestInformation.FacilityId;
        return facilityId.HasValue && id == facilityId.GetValueOrDefault();
      }));
      if (facility != null)
      {
        espResultType.TestLocation = new TestLocationType()
        {
          SiteCode = facility.Site.Code,
          FacilityId = facility.Code
        };
        LocationType locationType = LocationTypeManager.Instance.LocationTypes.FirstOrDefault<LocationType>((Func<LocationType, bool>) (l =>
        {
          Guid id = l.Id;
          Guid? facilityLocationId = audiologyTestInformation.FacilityLocationId;
          return facilityLocationId.HasValue && id == facilityLocationId.GetValueOrDefault();
        }));
        if (locationType != null)
        {
          try
          {
            FacilityLocationType facilityLocationType = (FacilityLocationType) System.Enum.Parse(typeof (FacilityLocationType), locationType.Name);
            espResultType.TestLocation.FacilityLocation = new FacilityLocationType?(facilityLocationType);
          }
          catch (InvalidCastException ex)
          {
            EspManager.Logger.Error((System.Exception) ex, "The location {0} could not be assigned to the eSP facility {1}", (object) locationType.Name, (object) facility.Code);
          }
        }
      }
    }
    return espResultType;
  }

  private PathMedical.DataExchange.eSP.Service.TestType CreateEspTestResult(
    AudiologyTestInformation audiologyTest,
    byte[] testImage)
  {
    if (audiologyTest == null)
      return (PathMedical.DataExchange.eSP.Service.TestType) null;
    PathMedical.DataExchange.eSP.Service.TestType espTestResult = new PathMedical.DataExchange.eSP.Service.TestType()
    {
      TestId = this.GetEspTestId(audiologyTest.TestDetailId),
      StartDate = audiologyTest.TestDate
    };
    DateTime? testDate = audiologyTest.TestDate;
    if (testDate.HasValue)
    {
      int? duration = audiologyTest.Duration;
      if (duration.HasValue)
      {
        duration = audiologyTest.Duration;
        int result;
        if (int.TryParse(duration.ToString(), out result))
        {
          if (result == 0)
            result = 1000;
          testDate = audiologyTest.TestDate;
          DateTime dateTime = testDate.Value;
          espTestResult.EndDate = new DateTime?(dateTime.AddMilliseconds((double) result));
          int num = result / 1000;
          espTestResult.Duration = Convert.ToString(num);
        }
      }
    }
    switch (audiologyTest.TestObject)
    {
      case TestObject.LeftEar:
        espTestResult.Ear = new EarType?(EarType.Left);
        break;
      case TestObject.RightEar:
        espTestResult.Ear = new EarType?(EarType.Right);
        break;
    }
    switch (audiologyTest.TestType)
    {
      case PathMedical.AudiologyTest.TestType.TEOAE:
        espTestResult.TestType1 = new TestTypeType?(TestTypeType.Transient);
        break;
      case PathMedical.AudiologyTest.TestType.ABR:
        espTestResult.TestType1 = new TestTypeType?(TestTypeType.Aabr);
        break;
      case PathMedical.AudiologyTest.TestType.DPOAE:
        espTestResult.TestType1 = new TestTypeType?(TestTypeType.Dp);
        break;
    }
    AudiologyTestResult? nullable = audiologyTest.TestObject == TestObject.RightEar ? audiologyTest.RightEarTestResult : audiologyTest.LeftEarTestResult;
    if (nullable.HasValue && nullable.HasValue)
    {
      switch (nullable.GetValueOrDefault())
      {
        case AudiologyTestResult.Pass:
          espTestResult.TestOutcome = new TestTypeTestOutcome?(TestTypeTestOutcome.Cr);
          break;
        case AudiologyTestResult.Refer:
          espTestResult.TestOutcome = new TestTypeTestOutcome?(TestTypeTestOutcome.Ncr);
          break;
        case AudiologyTestResult.Incomplete:
          espTestResult.TestOutcome = new TestTypeTestOutcome?(TestTypeTestOutcome.Incomplete);
          break;
      }
    }
    ITestPlugin testPlugin = SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().FirstOrDefault<ITestPlugin>((Func<ITestPlugin, bool>) (p =>
    {
      Guid testTypeSignature1 = p.TestTypeSignature;
      Guid? testTypeSignature2 = audiologyTest.TestTypeSignature;
      return testTypeSignature2.HasValue && testTypeSignature1 == testTypeSignature2.GetValueOrDefault();
    }));
    if (testPlugin != null && testPlugin is ISupportPluginDataExchange)
    {
      object obj = (testPlugin as ISupportPluginDataExchange).Export(ExportType.BinaryImage, audiologyTest.TestDetailId);
      if (obj is Array)
        espTestResult.TestImageArray = (byte[]) obj;
    }
    espTestResult.TestImageArray = testImage;
    return espTestResult;
  }

  private void ProcessEspExportResponse(CSPCHD session, string espResult)
  {
  }

  private static string SerializeEntity(object entity)
  {
    try
    {
      XmlSerializer xmlSerializer = new XmlSerializer(entity.GetType());
      XmlTextWriter xmlTextWriter1 = new XmlTextWriter((Stream) new MemoryStream(), (Encoding) new UTF8Encoding(false));
      XmlTextWriter xmlTextWriter2 = xmlTextWriter1;
      object o = entity;
      xmlSerializer.Serialize((XmlWriter) xmlTextWriter2, o);
      return new UTF8Encoding().GetString(((MemoryStream) xmlTextWriter1.BaseStream).ToArray());
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_SerializationFailure"), ex);
    }
  }

  private static T DeserializeEntity<T>(string entitySerialization) where T : class
  {
    try
    {
      return new XmlSerializer(typeof (T)).Deserialize((XmlReader) new XmlTextReader((TextReader) new StringReader(entitySerialization))) as T;
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_DeserializationFailure"), ex);
    }
  }

  private bool IsMessageValid(string xmlMessage, string xmlSchemaName)
  {
    try
    {
      XmlSchema resourceEmbeddedSchema = XmlHelper.GetResourceEmbeddedSchema(Assembly.GetExecutingAssembly(), xmlSchemaName);
      return this.xmlHelper.IsXmlDocumentValid(XDocument.Parse(xmlMessage), resourceEmbeddedSchema);
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("EspManager_ValidationFailure"), ex);
    }
  }

  private string GetEspTestId(Guid id)
  {
    int num1;
    if (this.espTestIds.TryGetValue(id, out num1))
      return Convert.ToString(num1);
    int num2 = 1;
    if (this.espTestIds.Values.Count<int>() > 0)
      num2 = this.espTestIds.Values.Max() + 1;
    this.espTestIds.Add(id, num2);
    return Convert.ToString(num2);
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void RefreshData()
  {
    RiskIndicatorManager.Instance.RefreshData();
    InstrumentManager.Instance.RefreshData();
    UserManager.Instance.RefreshData();
    FacilityManager.Instance.RefreshData();
    SiteManager.Instance.RefreshData();
  }

  public void Store()
  {
  }

  public void Delete()
  {
  }

  public void CancelNewItem()
  {
  }

  public void PrepareAddItem()
  {
  }

  public void RevertModifications()
  {
  }

  private string ReadTestMessageFromNorthgate()
  {
    return XElement.Load(XmlReader.Create((Stream) new FileStream("C:\\Daten\\MRC\\V2\\espTestResult-27-Apr.xml", FileMode.Open))).ToString();
  }
}
