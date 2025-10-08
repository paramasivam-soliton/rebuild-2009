// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.PatientManagementConfiguration
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Logging;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.Fields;
using System;

#nullable disable
namespace PathMedical.PatientManagement;

public class PatientManagementConfiguration
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (PatientManagementConfiguration), "$Rev: 1211 $");
  private readonly Guid configurationAccessPermissionId = new Guid("0A0CF43C-0485-45ce-8FC5-4B0F9729014C");
  public static readonly Guid ComponentId = new Guid("0BF386E1-2DA1-423a-9D1E-658F24B753E4");

  public static PatientManagementConfiguration Instance
  {
    get => PathMedical.Singleton.Singleton<PatientManagementConfiguration>.Instance;
  }

  public Guid ConfigurationAccessPermissionId => this.configurationAccessPermissionId;

  public CreationTimeStampFilterType DateOfBirthRange { get; set; }

  private PatientManagementConfiguration()
  {
    this.DateOfBirthRange = CreationTimeStampFilterType.Last4Weeks;
  }

  public FieldConfigurationType PatientRecordNumberFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (PatientRecordNumberFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (PatientRecordNumberFieldConfiguration), value);
  }

  public FieldConfigurationType PatientForenameFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (PatientForenameFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (PatientForenameFieldConfiguration), value);
  }

  public FieldConfigurationType PatientSurnameFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (PatientSurnameFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (PatientSurnameFieldConfiguration), value);
  }

  public FieldConfigurationType PatientDateOfBirthFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (PatientDateOfBirthFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (PatientDateOfBirthFieldConfiguration), value);
  }

  public FieldConfigurationType PatientGenderFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (PatientGenderFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (PatientGenderFieldConfiguration), value);
  }

  public FieldConfigurationType PatientBirthLocationFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (PatientBirthLocationFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (PatientBirthLocationFieldConfiguration), value);
  }

  public FieldConfigurationType PatientWeightFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (PatientWeightFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (PatientWeightFieldConfiguration), value);
  }

  public FieldConfigurationType MotherForenameFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (MotherForenameFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (MotherForenameFieldConfiguration), value);
  }

  public FieldConfigurationType MotherSurnameFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (MotherSurnameFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (MotherSurnameFieldConfiguration), value);
  }

  public FieldConfigurationType CaregiverForenameFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (CaregiverForenameFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (CaregiverForenameFieldConfiguration), value);
  }

  public FieldConfigurationType CaregiverSurnameFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (CaregiverSurnameFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (CaregiverSurnameFieldConfiguration), value);
  }

  public FieldConfigurationType NicuFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (NicuFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (NicuFieldConfiguration), value);
  }

  public FieldConfigurationType ConsentStateFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (ConsentStateFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (ConsentStateFieldConfiguration), value);
  }

  public FieldConfigurationType RiskIndicatorFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (RiskIndicatorFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (RiskIndicatorFieldConfiguration), value);
  }

  public FieldConfigurationType HospitalIdFieldConfiguration
  {
    get => this.GetFieldConfigurationType(nameof (HospitalIdFieldConfiguration));
    set => this.SetFieldConfigurationType(nameof (HospitalIdFieldConfiguration), value);
  }

  public string PatientBrowserListConfigurationList1Field1
  {
    get => this.GetPatientBrowserListConfiguration(1, 1);
    set => this.SetPatientBrowserListConfiguration(1, 1, value);
  }

  public string PatientBrowserListConfigurationList1Field2
  {
    get => this.GetPatientBrowserListConfiguration(1, 2);
    set => this.SetPatientBrowserListConfiguration(1, 2, value);
  }

  public string PatientBrowserListConfigurationList2Field1
  {
    get => this.GetPatientBrowserListConfiguration(2, 1);
    set => this.SetPatientBrowserListConfiguration(2, 1, value);
  }

  public string PatientBrowserListConfigurationList2Field2
  {
    get => this.GetPatientBrowserListConfiguration(2, 2);
    set => this.SetPatientBrowserListConfiguration(2, 2, value);
  }

  public string PatientBrowserListConfigurationList3Field1
  {
    get => this.GetPatientBrowserListConfiguration(3, 1);
    set => this.SetPatientBrowserListConfiguration(3, 1, value);
  }

  public string PatientBrowserListConfigurationList3Field2
  {
    get => this.GetPatientBrowserListConfiguration(3, 2);
    set => this.SetPatientBrowserListConfiguration(3, 2, value);
  }

  public MedicalRecordTypes PatientIdFormat
  {
    get
    {
      MedicalRecordTypes patientIdFormat = MedicalRecordTypes.None;
      string str = string.Empty;
      try
      {
        str = SystemConfigurationManager.Instance.GetSystemConfigurationValue(PatientManagementConfiguration.ComponentId, nameof (PatientIdFormat));
        patientIdFormat = (MedicalRecordTypes) Enum.Parse(typeof (MedicalRecordTypes), str);
      }
      catch (ArgumentException ex)
      {
        PatientManagementConfiguration.Logger.Error((Exception) ex, "Failure while loading reading patient record number format [{0}].", (object) str);
      }
      return patientIdFormat;
    }
    set
    {
      SystemConfigurationManager.Instance.SetSystemConfigurationValue(PatientManagementConfiguration.ComponentId, nameof (PatientIdFormat), Convert.ToString((object) value));
    }
  }

  public void StoreFieldConfiguration()
  {
    SystemConfigurationManager.Instance.StoreConfigurationChanges();
  }

  public void ResetFieldConfiguration()
  {
    SystemConfigurationManager.Instance.ResetConfigurationChanges();
  }

  private FieldConfigurationType GetFieldConfigurationType(string fieldName)
  {
    string configurationValue = SystemConfigurationManager.Instance.GetSystemConfigurationValue(PatientManagementConfiguration.ComponentId, fieldName);
    return string.IsNullOrEmpty(configurationValue) ? FieldConfigurationType.Default : (FieldConfigurationType) Enum.Parse(typeof (FieldConfigurationType), configurationValue);
  }

  private void SetFieldConfigurationType(string fieldName, FieldConfigurationType value)
  {
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(PatientManagementConfiguration.ComponentId, fieldName, value.ToString());
  }

  private string GetPatientBrowserListConfiguration(int list, int field)
  {
    string key = $"PatientBrowserListConfigurationList{list}Field{field}";
    return SystemConfigurationManager.Instance.GetSystemConfigurationValue(PatientManagementConfiguration.ComponentId, key);
  }

  private void SetPatientBrowserListConfiguration(int list, int field, string value)
  {
    string key = $"PatientBrowserListConfigurationList{list}Field{field}";
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(PatientManagementConfiguration.ComponentId, key, value);
  }
}
