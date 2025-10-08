// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.Panels.PatientDataEditor
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;
using PathMedical.Exception;
using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.Panels;

[ToolboxItem(false)]
public class PatientDataEditor : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private ModelMapper<Patient> modelMapper;
  private readonly MandatoryValidationGroupManager mandatoryGroupManager = new MandatoryValidationGroupManager();
  private IContainer components;
  private LayoutControl layoutPatientDataEditor;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlGroup layoutPatientData;
  private EmptySpaceItem emptySpaceItem5;
  private EmptySpaceItem emptySpaceItem6;
  private LayoutControlGroup layoutPatientMedicalInformation;
  private EmptySpaceItem emptySpaceItem15;
  private EmptySpaceItem emptySpaceItem3;
  private EmptySpaceItem emptySpaceItem16;
  private EmptySpaceItem emptySpaceItem1;
  private DevExpressTextEdit patientRecordNumber;
  private LayoutControlItem layoutPatientRecordNumber;
  private DevExpressComboBoxEdit patientGender;
  private LayoutControlItem layoutPatientGender;
  private DevExpressTextEdit patientTitle;
  private LayoutControlItem layoutPatientTitle;
  private DevExpressTextEdit patientForename1;
  private LayoutControlItem layoutPatientForename1;
  private DevExpressTextEdit patientAlsoKnownAs;
  private DevExpressTextEdit patientSurname;
  private DevExpressTextEdit patientMiddleInitial;
  private DevExpressTextEdit patientForename2;
  private LayoutControlItem layoutPatientForename2;
  private LayoutControlItem layoutPatientMiddleInitial;
  private LayoutControlItem layoutPatientSurname;
  private LayoutControlItem layoutPatientAlsoKnownAs;
  private DevExpressComboBoxEdit patientNationality;
  private DevExpressTextEdit patientBirthLocation;
  private DevExpressTextEdit patientSocialSecurityNumber;
  private LayoutControlItem layoutPatientSocialSecurityNumber;
  private LayoutControlItem layoutPatientBirthLocation;
  private LayoutControlItem layoutPatientNationality;
  private DevExpressTextEdit caregiverTitle;
  private DevExpressTextEdit caregiverForename1;
  private DevExpressTextEdit caregiverAlsoKnownAs;
  private DevExpressTextEdit caregiverSurname;
  private DevExpressTextEdit caregiverMiddleInitial;
  private DevExpressTextEdit caregiverForename2;
  private LayoutControlItem layoutCaregiverForename2;
  private LayoutControlItem layoutCaregiverMiddleInitial;
  private DevExpressTextEdit caregiverSocialSecurityNumber;
  private DevExpressComboBoxEdit patientLanguageCode;
  private DevExpressComboBoxEdit caregiverLanguageCode;
  private LayoutControlItem layoutPatientLanguageCode;
  private DevExpressTextEdit caregiverCity;
  private DevExpressTextEdit caregiverZip;
  private DevExpressComboBoxEdit caregiverCountry;
  private DevExpressDateEdit patientDateOfBirth;
  private LayoutControlItem layoutPatientDateOfBirth;
  private DevExpressMemoEdit patientMedication;
  private LayoutControlItem layoutPatientMedication;
  private DevExpressComboBoxEdit patientFreeText1;
  private LayoutControlItem layoutPatientFreeText1;
  private DevExpressComboBoxEdit patientFreeText2;
  private LayoutControlItem layoutPatientFreeText2;
  private DevExpressTextEdit caregiverAddress1;
  private EmptySpaceItem emptySpaceItem2;
  private TabbedControlGroup tabbedControlGroup1;
  private LayoutControlGroup layoutMotherGroup;
  private EmptySpaceItem emptySpaceItem8;
  private LayoutControlGroup layoutCaregiverGroup;
  private EmptySpaceItem emptySpaceItem13;
  private LayoutControlItem layoutCaregiverTitle;
  private LayoutControlItem layoutCaregiverForename1;
  private LayoutControlItem layoutCaregiverSurname;
  private LayoutControlItem layoutCaregiverAlsoKnownAs;
  private LayoutControlItem layoutCaregiverLanguageCode;
  private LayoutControlItem layoutCaregiverZip;
  private LayoutControlItem layoutCaregiverCity;
  private LayoutControlItem layoutCaregiverCountry;
  private LayoutControlItem layoutCaregiverSocialSecurityNumber;
  private LayoutControlItem layoutCaregiverAddress1;
  private DevExpressComboBoxEdit motherLanguageCode;
  private DevExpressTextEdit motherAlsoKnownAs;
  private DevExpressTextEdit motherSurname;
  private DevExpressTextEdit motherTitle;
  private DevExpressTextEdit motherForename1;
  private DevExpressTextEdit motherSocialSecurityNumber;
  private LayoutControlItem layoutMotherTitle;
  private LayoutControlItem layoutMotherForename1;
  private LayoutControlItem layoutMotherSurname;
  private LayoutControlItem layoutMotherAlsoKnownAs;
  private LayoutControlItem layoutMotherSocialSecurityNumber;
  private LayoutControlItem layoutMotherLanguageCode;
  private EmptySpaceItem emptySpaceItem7;
  private DevExpressComboBoxEdit motherCountry;
  private DevExpressTextEdit motherCity;
  private DevExpressTextEdit motherZip;
  private LayoutControlItem layoutMotherZip;
  private LayoutControlItem layoutMotherCity;
  private EmptySpaceItem emptySpaceItem12;
  private LayoutControlItem layoutMotherCountry;
  private EmptySpaceItem emptySpaceItem18;
  private DevExpressTextEdit motherAddress1;
  private LayoutControlItem layoutMotherStreet;
  private EmptySpaceItem emptySpaceItem10;
  private DevExpressComboBoxEdit patientNicuState;
  private DevExpressComboBoxEdit patientConsentState;
  private LayoutControlItem layoutPatientConsentState;
  private LayoutControlItem layoutPatientNicuState;
  private DevExpressTextEdit patientFreeText3;
  private LayoutControlItem layoutControlItem1;
  private DevExpressTextEdit hospitalId;
  private LayoutControlItem layoutControlItem2;
  private DevExpressTextEdit patientWeight;
  private DevExpressTextEdit patientHeight;
  private LayoutControlItem layoutPatientWeight;
  private LayoutControlItem layoutControlItem3;
  private DevExpressTextEdit caregiverFax;
  private DevExpressTextEdit caregiverCellPhone;
  private DevExpressTextEdit caregiverPhone;
  private DevExpressTextEdit motherFax;
  private DevExpressTextEdit motherCellPhone;
  private DevExpressTextEdit motherPhone;
  private LayoutControlItem layoutMotherPhone;
  private LayoutControlItem layoutMotherCellPhone;
  private LayoutControlItem layoutMotherFax;
  private EmptySpaceItem emptySpaceItem17;
  private LayoutControlItem layoutCaregiverPhone;
  private LayoutControlItem layoutCaregiverCellPhone;
  private LayoutControlItem layoutCaregiverFax;

  public PatientDataEditor()
  {
    this.InitializeComponent();
    this.InitializeSelectionValues();
    this.InitializeModelMapper();
    this.ApplyFieldConfiguration();
  }

  public PatientDataEditor(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void InitializeSelectionValues()
  {
    this.patientGender.DataSource = (object) new Gender[2]
    {
      Gender.Female,
      Gender.Male
    };
    this.patientNicuState.DataSource = (object) new NicuStatus[2]
    {
      NicuStatus.No,
      NicuStatus.Yes
    };
    this.patientConsentState.DataSource = (object) new ConsentStatus[3]
    {
      ConsentStatus.None,
      ConsentStatus.Screening,
      ConsentStatus.Full
    };
    CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures | CultureTypes.InstalledWin32Cultures);
    if (cultures != null)
    {
      DevExpressComboBoxEdit motherCountry = this.motherCountry;
      DevExpressComboBoxEdit caregiverCountry = this.caregiverCountry;
      IEnumerable<ComboBoxEditItemWrapper> source = ((IEnumerable<CultureInfo>) cultures).Where<CultureInfo>((Func<CultureInfo, bool>) (ci => !ci.Equals((object) CultureInfo.InvariantCulture))).Select<CultureInfo, RegionInfo>((Func<CultureInfo, RegionInfo>) (ci => new RegionInfo(ci.LCID))).Distinct<RegionInfo>().Select<RegionInfo, ComboBoxEditItemWrapper>((Func<RegionInfo, ComboBoxEditItemWrapper>) (ri => new ComboBoxEditItemWrapper(ri.DisplayName == ri.EnglishName ? ri.DisplayName : $"{ri.DisplayName} ({ri.EnglishName})", (object) ri.TwoLetterISORegionName)));
      List<ComboBoxEditItemWrapper> list;
      object obj1 = (object) (list = source.OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>());
      caregiverCountry.DataSource = (object) list;
      object obj2 = obj1;
      motherCountry.DataSource = obj2;
      this.patientNationality.DataSource = (object) ((IEnumerable<CultureInfo>) cultures).Where<CultureInfo>((Func<CultureInfo, bool>) (ci => !ci.Equals((object) CultureInfo.InvariantCulture))).Select<CultureInfo, RegionInfo>((Func<CultureInfo, RegionInfo>) (ci => new RegionInfo(ci.LCID))).Distinct<RegionInfo>().Select<RegionInfo, ComboBoxEditItemWrapper>((Func<RegionInfo, ComboBoxEditItemWrapper>) (ri => new ComboBoxEditItemWrapper(ri.DisplayName == ri.EnglishName ? ri.DisplayName : $"{ri.DisplayName} ({ri.EnglishName})", (object) ri.TwoLetterISORegionName))).OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
    }
    if (SystemConfigurationManager.Instance.SupportedLanguages == null)
      return;
    DevExpressComboBoxEdit motherLanguageCode = this.motherLanguageCode;
    DevExpressComboBoxEdit caregiverLanguageCode = this.caregiverLanguageCode;
    IEnumerable<ComboBoxEditItemWrapper> source1 = SystemConfigurationManager.Instance.SupportedLanguages.Select<CultureInfo, ComboBoxEditItemWrapper>((Func<CultureInfo, ComboBoxEditItemWrapper>) (sl => new ComboBoxEditItemWrapper(sl.DisplayName == sl.EnglishName ? sl.DisplayName : $"{sl.DisplayName} ({sl.EnglishName})", (object) sl.Name))).Distinct<ComboBoxEditItemWrapper>();
    List<ComboBoxEditItemWrapper> list1;
    object obj3 = (object) (list1 = source1.OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>());
    caregiverLanguageCode.DataSource = (object) list1;
    object obj4 = obj3;
    motherLanguageCode.DataSource = obj4;
  }

  private void InitializeModelMapper()
  {
    ModelMapper<Patient> modelMapper = new ModelMapper<Patient>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientRecordNumber), (object) this.patientRecordNumber);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.HospitalId), (object) this.hospitalId);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => (object) p.ConsentStatus), (object) this.patientConsentState);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => (object) p.NicuStatus), (object) this.patientNicuState);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.SocialSecurityNumber), (object) this.patientSocialSecurityNumber);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.Title), (object) this.patientTitle);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.Forename1), (object) this.patientForename1);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.Forename2), (object) this.patientForename2);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.Initial), (object) this.patientMiddleInitial);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.Surname), (object) this.patientSurname);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.AlsoKnownAs), (object) this.patientAlsoKnownAs);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => (object) p.PatientContact.DateOfBirth), (object) this.patientDateOfBirth);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.BirthLocation), (object) this.patientBirthLocation);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.NationalityCode), (object) this.patientNationality);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => (object) p.PatientContact.Gender), (object) this.patientGender);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.Weight), (object) this.patientWeight);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.PatientContact.Height), (object) this.patientHeight);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.SocialSecurityNumber), (object) this.motherSocialSecurityNumber);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.Title), (object) this.motherTitle);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.Forename1), (object) this.motherForename1);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.Surname), (object) this.motherSurname);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.AlsoKnownAs), (object) this.motherAlsoKnownAs);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.LanguageCode), (object) this.motherLanguageCode);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.PrimaryAddress.Address1), (object) this.motherAddress1);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.PrimaryAddress.Zip), (object) this.motherZip);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.PrimaryAddress.City), (object) this.motherCity);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.PrimaryAddress.Country), (object) this.motherCountry);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.PrimaryAddress.Phone), (object) this.motherPhone);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.PrimaryAddress.Fax), (object) this.motherFax);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.MotherContact.PrimaryAddress.CellPhone), (object) this.motherCellPhone);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.SocialSecurityNumber), (object) this.caregiverSocialSecurityNumber);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.Title), (object) this.caregiverTitle);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.Forename1), (object) this.caregiverForename1);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.Forename2), (object) this.caregiverForename2);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.Initial), (object) this.caregiverMiddleInitial);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.Surname), (object) this.caregiverSurname);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.AlsoKnownAs), (object) this.caregiverAlsoKnownAs);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.LanguageCode), (object) this.caregiverLanguageCode);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.PrimaryAddress.Address1), (object) this.caregiverAddress1);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.PrimaryAddress.Zip), (object) this.caregiverZip);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.PrimaryAddress.City), (object) this.caregiverCity);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.PrimaryAddress.Country), (object) this.caregiverCountry);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.PrimaryAddress.Phone), (object) this.caregiverPhone);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.PrimaryAddress.Fax), (object) this.caregiverFax);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.CaregiverContact.PrimaryAddress.CellPhone), (object) this.caregiverCellPhone);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.Medication), (object) this.patientMedication);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.FreeText1), (object) this.patientFreeText1);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.FreeText2), (object) this.patientFreeText2);
    modelMapper.Add((Expression<Func<Patient, object>>) (p => p.FreeText3), (object) this.patientFreeText3);
    this.modelMapper = modelMapper;
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != 0);
  }

  public void FillFields(Patient patient)
  {
    this.modelMapper.SetUIEnabled(patient != null && this.ViewMode != 0);
    bool isUndoDisabled = this.patientDateOfBirth.IsUndoDisabled;
    try
    {
      this.patientDateOfBirth.IsUndoDisabled = true;
      if (patient != null && patient.PatientContact != null && patient.PatientContact.PatientId.Equals(Guid.Empty))
      {
        this.patientDateOfBirth.Properties.MaxValue = DateTime.Now;
        this.patientDateOfBirth.Properties.MinValue = DateTime.Now.Subtract(new TimeSpan(180, 0, 0, 0));
      }
      else
      {
        this.patientDateOfBirth.Properties.MinValue = new DateTime(1900, 1, 1);
        this.patientDateOfBirth.Properties.MaxValue = DateTime.Now;
      }
    }
    finally
    {
      this.patientDateOfBirth.IsUndoDisabled = isUndoDisabled;
    }
    switch (PatientManagementConfiguration.Instance.PatientIdFormat)
    {
      case MedicalRecordTypes.MRC:
        this.patientRecordNumber.FormatString = "\\d{3}\\d{3}\\d{4}";
        this.patientRecordNumber.Validator = (ICustomValidator) new MrnValidator()
        {
          ErrorText = Resources.PatientDataEditor_MRNInvalid_Message,
          MrnType = MedicalRecordTypes.MRC
        };
        break;
      case MedicalRecordTypes.CPR_DK:
        this.patientRecordNumber.FormatString = "\\d{6}-\\d{4}";
        this.patientRecordNumber.Validator = (ICustomValidator) new MrnValidator()
        {
          ErrorText = Resources.PatientDataEditor_MRNInvalid_Message,
          MrnType = MedicalRecordTypes.CPR_DK
        };
        break;
      case MedicalRecordTypes.GERMANY:
        this.patientRecordNumber.FormatString = "\\d{3} \\d{3} \\d{3} \\d{3}";
        this.patientRecordNumber.Validator = (ICustomValidator) new MrnValidator()
        {
          ErrorText = Resources.PatientDataEditor_MRNInvalid_Message,
          MrnType = MedicalRecordTypes.GERMANY
        };
        break;
      case MedicalRecordTypes.TURKEY:
        this.patientRecordNumber.Validator = (ICustomValidator) new MrnValidator()
        {
          ErrorText = Resources.PatientDataEditor_MRNInvalid_Message,
          MrnType = MedicalRecordTypes.TURKEY
        };
        break;
      default:
        this.patientRecordNumber.Validator = (ICustomValidator) null;
        this.patientRecordNumber.FormatString = (string) null;
        break;
    }
    this.modelMapper.CopyModelToUI((ICollection<Patient>) new Patient[1]
    {
      patient
    });
    this.mandatoryGroupManager.ValidateGroups();
  }

  private void UpdateCommonPatientData(ICollection<Patient> patients)
  {
    if (patients != null)
    {
      this.patientFreeText1.DataSource = (object) patients.Select<Patient, string>((Func<Patient, string>) (p => p.FreeText1)).Where<string>((Func<string, bool>) (p => !string.IsNullOrEmpty(p))).Distinct<string>().Select<string, ComboBoxEditItemWrapper>((Func<string, ComboBoxEditItemWrapper>) (p => new ComboBoxEditItemWrapper(p, (object) p))).ToList<ComboBoxEditItemWrapper>();
      this.patientFreeText2.DataSource = (object) patients.Select<Patient, string>((Func<Patient, string>) (p => p.FreeText2)).Where<string>((Func<string, bool>) (p => !string.IsNullOrEmpty(p))).Distinct<string>().Select<string, ComboBoxEditItemWrapper>((Func<string, ComboBoxEditItemWrapper>) (p => new ComboBoxEditItemWrapper(p, (object) p))).ToList<ComboBoxEditItemWrapper>();
    }
    else
    {
      this.patientFreeText1.DataSource = (object) null;
      this.patientFreeText2.DataSource = (object) null;
    }
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  public override bool ValidateView()
  {
    if (!base.ValidateView())
      return false;
    if (this.patientRecordNumber.Validator is MrnValidator && !string.IsNullOrEmpty(this.patientRecordNumber.Text))
    {
      MrnValidator validator = this.patientRecordNumber.Validator as MrnValidator;
      try
      {
        validator.Severity = true;
        if (!validator.Validate((object) this.patientRecordNumber.Text))
        {
          if (this.DisplayQuestion(Resources.PatientDataEditor_ValidateView_InvalidPatientIdContinueAnywayQuestion, AnswerOptionType.YesNo, QuestionIcon.Warning) == AnswerType.No)
            return false;
        }
      }
      finally
      {
        validator.Severity = false;
      }
    }
    return true;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.Type == typeof (Patient) && !e.IsList && (e.ChangeType == ChangeType.SelectionChanged || e.ChangeType == ChangeType.ItemEdited || e.ChangeType == ChangeType.ItemAdded))
    {
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new PatientDataEditor.UpdatePatientDataCallBack(this.FillFields), (object) (Patient) e.ChangedObject);
      else
        this.FillFields((Patient) e.ChangedObject);
    }
    if (!(e.ChangedObject is ICollection<Patient>) || e.ChangeType != ChangeType.ListLoaded)
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PatientDataEditor.UpdateCommonPatientDataCallBack(this.UpdateCommonPatientData), (object) (e.ChangedObject as ICollection<Patient>));
    else
      this.UpdateCommonPatientData(e.ChangedObject as ICollection<Patient>);
  }

  private void ApplyFieldConfiguration()
  {
    PatientManagementConfiguration instance = PatientManagementConfiguration.Instance;
    Dictionary<IControl, FieldConfigurationType> source = new Dictionary<IControl, FieldConfigurationType>()
    {
      {
        (IControl) this.patientRecordNumber,
        instance.PatientRecordNumberFieldConfiguration
      },
      {
        (IControl) this.hospitalId,
        instance.HospitalIdFieldConfiguration
      },
      {
        (IControl) this.patientForename1,
        instance.PatientForenameFieldConfiguration
      },
      {
        (IControl) this.patientSurname,
        instance.PatientSurnameFieldConfiguration
      },
      {
        (IControl) this.patientDateOfBirth,
        instance.PatientDateOfBirthFieldConfiguration
      },
      {
        (IControl) this.patientGender,
        instance.PatientGenderFieldConfiguration
      },
      {
        (IControl) this.patientBirthLocation,
        instance.PatientBirthLocationFieldConfiguration
      },
      {
        (IControl) this.patientWeight,
        instance.PatientWeightFieldConfiguration
      },
      {
        (IControl) this.motherForename1,
        instance.MotherForenameFieldConfiguration
      },
      {
        (IControl) this.motherSurname,
        instance.MotherSurnameFieldConfiguration
      },
      {
        (IControl) this.caregiverForename1,
        instance.CaregiverForenameFieldConfiguration
      },
      {
        (IControl) this.caregiverSurname,
        instance.CaregiverSurnameFieldConfiguration
      },
      {
        (IControl) this.patientNicuState,
        instance.NicuFieldConfiguration
      },
      {
        (IControl) this.patientConsentState,
        instance.ConsentStateFieldConfiguration
      }
    };
    FieldConfigurationType[] configurationTypeArray = new FieldConfigurationType[3]
    {
      FieldConfigurationType.MandatoryGroup1,
      FieldConfigurationType.MandatoryGroup2,
      FieldConfigurationType.MandatoryGroup3
    };
    foreach (FieldConfigurationType fieldConfigurationType in configurationTypeArray)
    {
      FieldConfigurationType type = fieldConfigurationType;
      List<IControl> list = source.Where<KeyValuePair<IControl, FieldConfigurationType>>((Func<KeyValuePair<IControl, FieldConfigurationType>, bool>) (cal => (cal.Value & type) != 0)).Select<KeyValuePair<IControl, FieldConfigurationType>, IControl>((Func<KeyValuePair<IControl, FieldConfigurationType>, IControl>) (cal => cal.Key)).ToList<IControl>();
      if (list.Count != 0)
      {
        MandatoryValidationGroup validationGroup = new MandatoryValidationGroup(fieldConfigurationType);
        validationGroup.RegisterControls((IEnumerable<IControl>) list);
        this.mandatoryGroupManager.Add(validationGroup);
      }
    }
    this.MandatoryGroupsValidationMessage = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("FillInMandatoryFields");
  }

  public bool ValidateMandatoryGroups() => this.mandatoryGroupManager.IsOneGroupValid;

  public string MandatoryGroupsValidationMessage { get; private set; }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PatientDataEditor));
    this.layoutPatientDataEditor = new LayoutControl();
    this.caregiverFax = new DevExpressTextEdit();
    this.caregiverCellPhone = new DevExpressTextEdit();
    this.caregiverPhone = new DevExpressTextEdit();
    this.motherFax = new DevExpressTextEdit();
    this.motherCellPhone = new DevExpressTextEdit();
    this.motherPhone = new DevExpressTextEdit();
    this.patientWeight = new DevExpressTextEdit();
    this.hospitalId = new DevExpressTextEdit();
    this.patientFreeText3 = new DevExpressTextEdit();
    this.patientNicuState = new DevExpressComboBoxEdit();
    this.patientHeight = new DevExpressTextEdit();
    this.patientConsentState = new DevExpressComboBoxEdit();
    this.motherAddress1 = new DevExpressTextEdit();
    this.motherCity = new DevExpressTextEdit();
    this.motherZip = new DevExpressTextEdit();
    this.motherLanguageCode = new DevExpressComboBoxEdit();
    this.motherAlsoKnownAs = new DevExpressTextEdit();
    this.motherCountry = new DevExpressComboBoxEdit();
    this.motherSurname = new DevExpressTextEdit();
    this.motherTitle = new DevExpressTextEdit();
    this.caregiverAddress1 = new DevExpressTextEdit();
    this.patientFreeText2 = new DevExpressComboBoxEdit();
    this.motherForename1 = new DevExpressTextEdit();
    this.motherSocialSecurityNumber = new DevExpressTextEdit();
    this.patientFreeText1 = new DevExpressComboBoxEdit();
    this.patientMedication = new DevExpressMemoEdit();
    this.patientDateOfBirth = new DevExpressDateEdit();
    this.caregiverMiddleInitial = new DevExpressTextEdit();
    this.caregiverForename2 = new DevExpressTextEdit();
    this.caregiverCountry = new DevExpressComboBoxEdit();
    this.caregiverCity = new DevExpressTextEdit();
    this.caregiverZip = new DevExpressTextEdit();
    this.patientLanguageCode = new DevExpressComboBoxEdit();
    this.caregiverLanguageCode = new DevExpressComboBoxEdit();
    this.caregiverSocialSecurityNumber = new DevExpressTextEdit();
    this.caregiverSurname = new DevExpressTextEdit();
    this.caregiverForename1 = new DevExpressTextEdit();
    this.caregiverTitle = new DevExpressTextEdit();
    this.patientNationality = new DevExpressComboBoxEdit();
    this.caregiverAlsoKnownAs = new DevExpressTextEdit();
    this.patientBirthLocation = new DevExpressTextEdit();
    this.patientSocialSecurityNumber = new DevExpressTextEdit();
    this.patientAlsoKnownAs = new DevExpressTextEdit();
    this.patientMiddleInitial = new DevExpressTextEdit();
    this.patientForename2 = new DevExpressTextEdit();
    this.patientForename1 = new DevExpressTextEdit();
    this.patientTitle = new DevExpressTextEdit();
    this.patientSurname = new DevExpressTextEdit();
    this.patientGender = new DevExpressComboBoxEdit();
    this.patientRecordNumber = new DevExpressTextEdit();
    this.layoutPatientForename2 = new LayoutControlItem();
    this.layoutPatientMiddleInitial = new LayoutControlItem();
    this.layoutCaregiverForename2 = new LayoutControlItem();
    this.layoutCaregiverMiddleInitial = new LayoutControlItem();
    this.layoutPatientSocialSecurityNumber = new LayoutControlItem();
    this.layoutPatientLanguageCode = new LayoutControlItem();
    this.layoutPatientTitle = new LayoutControlItem();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutPatientAlsoKnownAs = new LayoutControlItem();
    this.layoutMotherAlsoKnownAs = new LayoutControlItem();
    this.layoutCaregiverAlsoKnownAs = new LayoutControlItem();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutPatientData = new LayoutControlGroup();
    this.emptySpaceItem5 = new EmptySpaceItem();
    this.emptySpaceItem3 = new EmptySpaceItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutPatientGender = new LayoutControlItem();
    this.layoutPatientForename1 = new LayoutControlItem();
    this.layoutPatientSurname = new LayoutControlItem();
    this.layoutPatientBirthLocation = new LayoutControlItem();
    this.layoutPatientNationality = new LayoutControlItem();
    this.layoutPatientDateOfBirth = new LayoutControlItem();
    this.layoutPatientRecordNumber = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutPatientConsentState = new LayoutControlItem();
    this.layoutPatientNicuState = new LayoutControlItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutPatientWeight = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutPatientMedicalInformation = new LayoutControlGroup();
    this.emptySpaceItem15 = new EmptySpaceItem();
    this.emptySpaceItem16 = new EmptySpaceItem();
    this.layoutPatientMedication = new LayoutControlItem();
    this.layoutPatientFreeText1 = new LayoutControlItem();
    this.layoutPatientFreeText2 = new LayoutControlItem();
    this.tabbedControlGroup1 = new TabbedControlGroup();
    this.layoutMotherGroup = new LayoutControlGroup();
    this.layoutMotherTitle = new LayoutControlItem();
    this.layoutMotherForename1 = new LayoutControlItem();
    this.layoutMotherSurname = new LayoutControlItem();
    this.layoutMotherSocialSecurityNumber = new LayoutControlItem();
    this.layoutMotherLanguageCode = new LayoutControlItem();
    this.emptySpaceItem8 = new EmptySpaceItem();
    this.layoutMotherZip = new LayoutControlItem();
    this.layoutMotherCity = new LayoutControlItem();
    this.emptySpaceItem12 = new EmptySpaceItem();
    this.layoutMotherStreet = new LayoutControlItem();
    this.layoutMotherCountry = new LayoutControlItem();
    this.emptySpaceItem18 = new EmptySpaceItem();
    this.layoutMotherPhone = new LayoutControlItem();
    this.layoutMotherCellPhone = new LayoutControlItem();
    this.layoutMotherFax = new LayoutControlItem();
    this.layoutCaregiverGroup = new LayoutControlGroup();
    this.emptySpaceItem13 = new EmptySpaceItem();
    this.layoutCaregiverTitle = new LayoutControlItem();
    this.layoutCaregiverZip = new LayoutControlItem();
    this.layoutCaregiverCity = new LayoutControlItem();
    this.emptySpaceItem17 = new EmptySpaceItem();
    this.layoutCaregiverCountry = new LayoutControlItem();
    this.layoutCaregiverSocialSecurityNumber = new LayoutControlItem();
    this.layoutCaregiverAddress1 = new LayoutControlItem();
    this.layoutCaregiverForename1 = new LayoutControlItem();
    this.layoutCaregiverSurname = new LayoutControlItem();
    this.layoutCaregiverLanguageCode = new LayoutControlItem();
    this.emptySpaceItem10 = new EmptySpaceItem();
    this.layoutCaregiverPhone = new LayoutControlItem();
    this.layoutCaregiverCellPhone = new LayoutControlItem();
    this.layoutCaregiverFax = new LayoutControlItem();
    this.emptySpaceItem6 = new EmptySpaceItem();
    this.emptySpaceItem7 = new EmptySpaceItem();
    this.layoutPatientDataEditor.BeginInit();
    this.layoutPatientDataEditor.SuspendLayout();
    this.caregiverFax.Properties.BeginInit();
    this.caregiverCellPhone.Properties.BeginInit();
    this.caregiverPhone.Properties.BeginInit();
    this.motherFax.Properties.BeginInit();
    this.motherCellPhone.Properties.BeginInit();
    this.motherPhone.Properties.BeginInit();
    this.patientWeight.Properties.BeginInit();
    this.hospitalId.Properties.BeginInit();
    this.patientFreeText3.Properties.BeginInit();
    this.patientNicuState.Properties.BeginInit();
    this.patientHeight.Properties.BeginInit();
    this.patientConsentState.Properties.BeginInit();
    this.motherAddress1.Properties.BeginInit();
    this.motherCity.Properties.BeginInit();
    this.motherZip.Properties.BeginInit();
    this.motherLanguageCode.Properties.BeginInit();
    this.motherAlsoKnownAs.Properties.BeginInit();
    this.motherCountry.Properties.BeginInit();
    this.motherSurname.Properties.BeginInit();
    this.motherTitle.Properties.BeginInit();
    this.caregiverAddress1.Properties.BeginInit();
    this.patientFreeText2.Properties.BeginInit();
    this.motherForename1.Properties.BeginInit();
    this.motherSocialSecurityNumber.Properties.BeginInit();
    this.patientFreeText1.Properties.BeginInit();
    this.patientMedication.Properties.BeginInit();
    this.patientDateOfBirth.Properties.VistaTimeProperties.BeginInit();
    this.patientDateOfBirth.Properties.BeginInit();
    this.caregiverMiddleInitial.Properties.BeginInit();
    this.caregiverForename2.Properties.BeginInit();
    this.caregiverCountry.Properties.BeginInit();
    this.caregiverCity.Properties.BeginInit();
    this.caregiverZip.Properties.BeginInit();
    this.patientLanguageCode.Properties.BeginInit();
    this.caregiverLanguageCode.Properties.BeginInit();
    this.caregiverSocialSecurityNumber.Properties.BeginInit();
    this.caregiverSurname.Properties.BeginInit();
    this.caregiverForename1.Properties.BeginInit();
    this.caregiverTitle.Properties.BeginInit();
    this.patientNationality.Properties.BeginInit();
    this.caregiverAlsoKnownAs.Properties.BeginInit();
    this.patientBirthLocation.Properties.BeginInit();
    this.patientSocialSecurityNumber.Properties.BeginInit();
    this.patientAlsoKnownAs.Properties.BeginInit();
    this.patientMiddleInitial.Properties.BeginInit();
    this.patientForename2.Properties.BeginInit();
    this.patientForename1.Properties.BeginInit();
    this.patientTitle.Properties.BeginInit();
    this.patientSurname.Properties.BeginInit();
    this.patientGender.Properties.BeginInit();
    this.patientRecordNumber.Properties.BeginInit();
    this.layoutPatientForename2.BeginInit();
    this.layoutPatientMiddleInitial.BeginInit();
    this.layoutCaregiverForename2.BeginInit();
    this.layoutCaregiverMiddleInitial.BeginInit();
    this.layoutPatientSocialSecurityNumber.BeginInit();
    this.layoutPatientLanguageCode.BeginInit();
    this.layoutPatientTitle.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutPatientAlsoKnownAs.BeginInit();
    this.layoutMotherAlsoKnownAs.BeginInit();
    this.layoutCaregiverAlsoKnownAs.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutPatientData.BeginInit();
    this.emptySpaceItem5.BeginInit();
    this.emptySpaceItem3.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutPatientGender.BeginInit();
    this.layoutPatientForename1.BeginInit();
    this.layoutPatientSurname.BeginInit();
    this.layoutPatientBirthLocation.BeginInit();
    this.layoutPatientNationality.BeginInit();
    this.layoutPatientDateOfBirth.BeginInit();
    this.layoutPatientRecordNumber.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutPatientConsentState.BeginInit();
    this.layoutPatientNicuState.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutPatientWeight.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutPatientMedicalInformation.BeginInit();
    this.emptySpaceItem15.BeginInit();
    this.emptySpaceItem16.BeginInit();
    this.layoutPatientMedication.BeginInit();
    this.layoutPatientFreeText1.BeginInit();
    this.layoutPatientFreeText2.BeginInit();
    this.tabbedControlGroup1.BeginInit();
    this.layoutMotherGroup.BeginInit();
    this.layoutMotherTitle.BeginInit();
    this.layoutMotherForename1.BeginInit();
    this.layoutMotherSurname.BeginInit();
    this.layoutMotherSocialSecurityNumber.BeginInit();
    this.layoutMotherLanguageCode.BeginInit();
    this.emptySpaceItem8.BeginInit();
    this.layoutMotherZip.BeginInit();
    this.layoutMotherCity.BeginInit();
    this.emptySpaceItem12.BeginInit();
    this.layoutMotherStreet.BeginInit();
    this.layoutMotherCountry.BeginInit();
    this.emptySpaceItem18.BeginInit();
    this.layoutMotherPhone.BeginInit();
    this.layoutMotherCellPhone.BeginInit();
    this.layoutMotherFax.BeginInit();
    this.layoutCaregiverGroup.BeginInit();
    this.emptySpaceItem13.BeginInit();
    this.layoutCaregiverTitle.BeginInit();
    this.layoutCaregiverZip.BeginInit();
    this.layoutCaregiverCity.BeginInit();
    this.emptySpaceItem17.BeginInit();
    this.layoutCaregiverCountry.BeginInit();
    this.layoutCaregiverSocialSecurityNumber.BeginInit();
    this.layoutCaregiverAddress1.BeginInit();
    this.layoutCaregiverForename1.BeginInit();
    this.layoutCaregiverSurname.BeginInit();
    this.layoutCaregiverLanguageCode.BeginInit();
    this.emptySpaceItem10.BeginInit();
    this.layoutCaregiverPhone.BeginInit();
    this.layoutCaregiverCellPhone.BeginInit();
    this.layoutCaregiverFax.BeginInit();
    this.emptySpaceItem6.BeginInit();
    this.emptySpaceItem7.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.layoutPatientDataEditor, "layoutPatientDataEditor");
    this.layoutPatientDataEditor.Appearance.ControlDropDownHeader.Font = new Font("Tahoma", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.layoutPatientDataEditor.Appearance.ControlDropDownHeader.Options.UseFont = true;
    this.layoutPatientDataEditor.BackColor = SystemColors.Control;
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverFax);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverCellPhone);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverPhone);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherFax);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherCellPhone);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherPhone);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientWeight);
    this.layoutPatientDataEditor.Controls.Add((Control) this.hospitalId);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientFreeText3);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientNicuState);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientHeight);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientConsentState);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherAddress1);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherCity);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherZip);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherLanguageCode);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherAlsoKnownAs);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherCountry);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherSurname);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherTitle);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverAddress1);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientFreeText2);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherForename1);
    this.layoutPatientDataEditor.Controls.Add((Control) this.motherSocialSecurityNumber);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientFreeText1);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientMedication);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientDateOfBirth);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverMiddleInitial);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverForename2);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverCountry);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverCity);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverZip);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientLanguageCode);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverLanguageCode);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverSocialSecurityNumber);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverSurname);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverForename1);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverTitle);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientNationality);
    this.layoutPatientDataEditor.Controls.Add((Control) this.caregiverAlsoKnownAs);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientBirthLocation);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientSocialSecurityNumber);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientAlsoKnownAs);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientMiddleInitial);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientForename2);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientForename1);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientTitle);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientSurname);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientGender);
    this.layoutPatientDataEditor.Controls.Add((Control) this.patientRecordNumber);
    this.layoutPatientDataEditor.HiddenItems.AddRange(new BaseLayoutItem[11]
    {
      (BaseLayoutItem) this.layoutPatientForename2,
      (BaseLayoutItem) this.layoutPatientMiddleInitial,
      (BaseLayoutItem) this.layoutCaregiverForename2,
      (BaseLayoutItem) this.layoutCaregiverMiddleInitial,
      (BaseLayoutItem) this.layoutPatientSocialSecurityNumber,
      (BaseLayoutItem) this.layoutPatientLanguageCode,
      (BaseLayoutItem) this.layoutPatientTitle,
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutPatientAlsoKnownAs,
      (BaseLayoutItem) this.layoutMotherAlsoKnownAs,
      (BaseLayoutItem) this.layoutCaregiverAlsoKnownAs
    });
    this.layoutPatientDataEditor.Name = "layoutPatientDataEditor";
    this.layoutPatientDataEditor.OptionsFocus.AllowFocusGroups = false;
    this.layoutPatientDataEditor.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.caregiverFax, "caregiverFax");
    this.caregiverFax.EnterMoveNextControl = true;
    this.caregiverFax.FormatString = (string) null;
    this.caregiverFax.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverFax.IsReadOnly = false;
    this.caregiverFax.IsUndoing = false;
    this.caregiverFax.Name = "caregiverFax";
    this.caregiverFax.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverFax.Properties.AccessibleDescription");
    this.caregiverFax.Properties.AccessibleName = componentResourceManager.GetString("caregiverFax.Properties.AccessibleName");
    this.caregiverFax.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverFax.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverFax.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverFax.Properties.AutoHeight");
    this.caregiverFax.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverFax.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverFax.Properties.Mask.AutoComplete");
    this.caregiverFax.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverFax.Properties.Mask.BeepOnError");
    this.caregiverFax.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverFax.Properties.Mask.EditMask");
    this.caregiverFax.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverFax.Properties.Mask.IgnoreMaskBlank");
    this.caregiverFax.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverFax.Properties.Mask.MaskType");
    this.caregiverFax.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverFax.Properties.Mask.PlaceHolder");
    this.caregiverFax.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverFax.Properties.Mask.SaveLiteral");
    this.caregiverFax.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverFax.Properties.Mask.ShowPlaceHolders");
    this.caregiverFax.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverFax.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverFax.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverFax.Properties.NullValuePrompt");
    this.caregiverFax.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverFax.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverFax.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverFax.Validator = (ICustomValidator) null;
    this.caregiverFax.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.caregiverCellPhone, "caregiverCellPhone");
    this.caregiverCellPhone.EnterMoveNextControl = true;
    this.caregiverCellPhone.FormatString = (string) null;
    this.caregiverCellPhone.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverCellPhone.IsReadOnly = false;
    this.caregiverCellPhone.IsUndoing = false;
    this.caregiverCellPhone.Name = "caregiverCellPhone";
    this.caregiverCellPhone.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverCellPhone.Properties.AccessibleDescription");
    this.caregiverCellPhone.Properties.AccessibleName = componentResourceManager.GetString("caregiverCellPhone.Properties.AccessibleName");
    this.caregiverCellPhone.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverCellPhone.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverCellPhone.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverCellPhone.Properties.AutoHeight");
    this.caregiverCellPhone.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverCellPhone.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverCellPhone.Properties.Mask.AutoComplete");
    this.caregiverCellPhone.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverCellPhone.Properties.Mask.BeepOnError");
    this.caregiverCellPhone.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverCellPhone.Properties.Mask.EditMask");
    this.caregiverCellPhone.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverCellPhone.Properties.Mask.IgnoreMaskBlank");
    this.caregiverCellPhone.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverCellPhone.Properties.Mask.MaskType");
    this.caregiverCellPhone.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverCellPhone.Properties.Mask.PlaceHolder");
    this.caregiverCellPhone.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverCellPhone.Properties.Mask.SaveLiteral");
    this.caregiverCellPhone.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverCellPhone.Properties.Mask.ShowPlaceHolders");
    this.caregiverCellPhone.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverCellPhone.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverCellPhone.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverCellPhone.Properties.NullValuePrompt");
    this.caregiverCellPhone.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverCellPhone.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverCellPhone.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverCellPhone.Validator = (ICustomValidator) null;
    this.caregiverCellPhone.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.caregiverPhone, "caregiverPhone");
    this.caregiverPhone.EnterMoveNextControl = true;
    this.caregiverPhone.FormatString = (string) null;
    this.caregiverPhone.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverPhone.IsReadOnly = false;
    this.caregiverPhone.IsUndoing = false;
    this.caregiverPhone.Name = "caregiverPhone";
    this.caregiverPhone.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverPhone.Properties.AccessibleDescription");
    this.caregiverPhone.Properties.AccessibleName = componentResourceManager.GetString("caregiverPhone.Properties.AccessibleName");
    this.caregiverPhone.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverPhone.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverPhone.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverPhone.Properties.AutoHeight");
    this.caregiverPhone.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverPhone.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverPhone.Properties.Mask.AutoComplete");
    this.caregiverPhone.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverPhone.Properties.Mask.BeepOnError");
    this.caregiverPhone.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverPhone.Properties.Mask.EditMask");
    this.caregiverPhone.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverPhone.Properties.Mask.IgnoreMaskBlank");
    this.caregiverPhone.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverPhone.Properties.Mask.MaskType");
    this.caregiverPhone.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverPhone.Properties.Mask.PlaceHolder");
    this.caregiverPhone.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverPhone.Properties.Mask.SaveLiteral");
    this.caregiverPhone.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverPhone.Properties.Mask.ShowPlaceHolders");
    this.caregiverPhone.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverPhone.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverPhone.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverPhone.Properties.NullValuePrompt");
    this.caregiverPhone.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverPhone.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverPhone.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverPhone.Validator = (ICustomValidator) null;
    this.caregiverPhone.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.motherFax, "motherFax");
    this.motherFax.EnterMoveNextControl = true;
    this.motherFax.FormatString = (string) null;
    this.motherFax.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherFax.IsReadOnly = false;
    this.motherFax.IsUndoing = false;
    this.motherFax.Name = "motherFax";
    this.motherFax.Properties.AccessibleDescription = componentResourceManager.GetString("motherFax.Properties.AccessibleDescription");
    this.motherFax.Properties.AccessibleName = componentResourceManager.GetString("motherFax.Properties.AccessibleName");
    this.motherFax.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherFax.Properties.Appearance.Options.UseBorderColor = true;
    this.motherFax.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherFax.Properties.AutoHeight");
    this.motherFax.Properties.BorderStyle = BorderStyles.Simple;
    this.motherFax.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherFax.Properties.Mask.AutoComplete");
    this.motherFax.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherFax.Properties.Mask.BeepOnError");
    this.motherFax.Properties.Mask.EditMask = componentResourceManager.GetString("motherFax.Properties.Mask.EditMask");
    this.motherFax.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherFax.Properties.Mask.IgnoreMaskBlank");
    this.motherFax.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherFax.Properties.Mask.MaskType");
    this.motherFax.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherFax.Properties.Mask.PlaceHolder");
    this.motherFax.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherFax.Properties.Mask.SaveLiteral");
    this.motherFax.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherFax.Properties.Mask.ShowPlaceHolders");
    this.motherFax.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherFax.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherFax.Properties.NullValuePrompt = componentResourceManager.GetString("motherFax.Properties.NullValuePrompt");
    this.motherFax.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherFax.Properties.NullValuePromptShowForEmptyValue");
    this.motherFax.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherFax.Validator = (ICustomValidator) null;
    this.motherFax.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.motherCellPhone, "motherCellPhone");
    this.motherCellPhone.EnterMoveNextControl = true;
    this.motherCellPhone.FormatString = (string) null;
    this.motherCellPhone.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherCellPhone.IsReadOnly = false;
    this.motherCellPhone.IsUndoing = false;
    this.motherCellPhone.Name = "motherCellPhone";
    this.motherCellPhone.Properties.AccessibleDescription = componentResourceManager.GetString("motherCellPhone.Properties.AccessibleDescription");
    this.motherCellPhone.Properties.AccessibleName = componentResourceManager.GetString("motherCellPhone.Properties.AccessibleName");
    this.motherCellPhone.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherCellPhone.Properties.Appearance.Options.UseBorderColor = true;
    this.motherCellPhone.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherCellPhone.Properties.AutoHeight");
    this.motherCellPhone.Properties.BorderStyle = BorderStyles.Simple;
    this.motherCellPhone.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherCellPhone.Properties.Mask.AutoComplete");
    this.motherCellPhone.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherCellPhone.Properties.Mask.BeepOnError");
    this.motherCellPhone.Properties.Mask.EditMask = componentResourceManager.GetString("motherCellPhone.Properties.Mask.EditMask");
    this.motherCellPhone.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherCellPhone.Properties.Mask.IgnoreMaskBlank");
    this.motherCellPhone.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherCellPhone.Properties.Mask.MaskType");
    this.motherCellPhone.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherCellPhone.Properties.Mask.PlaceHolder");
    this.motherCellPhone.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherCellPhone.Properties.Mask.SaveLiteral");
    this.motherCellPhone.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherCellPhone.Properties.Mask.ShowPlaceHolders");
    this.motherCellPhone.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherCellPhone.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherCellPhone.Properties.NullValuePrompt = componentResourceManager.GetString("motherCellPhone.Properties.NullValuePrompt");
    this.motherCellPhone.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherCellPhone.Properties.NullValuePromptShowForEmptyValue");
    this.motherCellPhone.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherCellPhone.Validator = (ICustomValidator) null;
    this.motherCellPhone.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.motherPhone, "motherPhone");
    this.motherPhone.EnterMoveNextControl = true;
    this.motherPhone.FormatString = (string) null;
    this.motherPhone.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherPhone.IsReadOnly = false;
    this.motherPhone.IsUndoing = false;
    this.motherPhone.Name = "motherPhone";
    this.motherPhone.Properties.AccessibleDescription = componentResourceManager.GetString("motherPhone.Properties.AccessibleDescription");
    this.motherPhone.Properties.AccessibleName = componentResourceManager.GetString("motherPhone.Properties.AccessibleName");
    this.motherPhone.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherPhone.Properties.Appearance.Options.UseBorderColor = true;
    this.motherPhone.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherPhone.Properties.AutoHeight");
    this.motherPhone.Properties.BorderStyle = BorderStyles.Simple;
    this.motherPhone.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherPhone.Properties.Mask.AutoComplete");
    this.motherPhone.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherPhone.Properties.Mask.BeepOnError");
    this.motherPhone.Properties.Mask.EditMask = componentResourceManager.GetString("motherPhone.Properties.Mask.EditMask");
    this.motherPhone.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherPhone.Properties.Mask.IgnoreMaskBlank");
    this.motherPhone.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherPhone.Properties.Mask.MaskType");
    this.motherPhone.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherPhone.Properties.Mask.PlaceHolder");
    this.motherPhone.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherPhone.Properties.Mask.SaveLiteral");
    this.motherPhone.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherPhone.Properties.Mask.ShowPlaceHolders");
    this.motherPhone.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherPhone.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherPhone.Properties.NullValuePrompt = componentResourceManager.GetString("motherPhone.Properties.NullValuePrompt");
    this.motherPhone.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherPhone.Properties.NullValuePromptShowForEmptyValue");
    this.motherPhone.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherPhone.Validator = (ICustomValidator) null;
    this.motherPhone.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientWeight, "patientWeight");
    this.patientWeight.EnterMoveNextControl = true;
    this.patientWeight.FormatString = (string) null;
    this.patientWeight.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientWeight.IsReadOnly = false;
    this.patientWeight.IsUndoing = false;
    this.patientWeight.Name = "patientWeight";
    this.patientWeight.Properties.AccessibleDescription = componentResourceManager.GetString("patientWeight.Properties.AccessibleDescription");
    this.patientWeight.Properties.AccessibleName = componentResourceManager.GetString("patientWeight.Properties.AccessibleName");
    this.patientWeight.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientWeight.Properties.Appearance.Options.UseBorderColor = true;
    this.patientWeight.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientWeight.Properties.AutoHeight");
    this.patientWeight.Properties.BorderStyle = BorderStyles.Simple;
    this.patientWeight.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientWeight.Properties.Mask.AutoComplete");
    this.patientWeight.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientWeight.Properties.Mask.BeepOnError");
    this.patientWeight.Properties.Mask.EditMask = componentResourceManager.GetString("patientWeight.Properties.Mask.EditMask");
    this.patientWeight.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientWeight.Properties.Mask.IgnoreMaskBlank");
    this.patientWeight.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientWeight.Properties.Mask.MaskType");
    this.patientWeight.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientWeight.Properties.Mask.PlaceHolder");
    this.patientWeight.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientWeight.Properties.Mask.SaveLiteral");
    this.patientWeight.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientWeight.Properties.Mask.ShowPlaceHolders");
    this.patientWeight.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientWeight.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientWeight.Properties.NullValuePrompt = componentResourceManager.GetString("patientWeight.Properties.NullValuePrompt");
    this.patientWeight.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientWeight.Properties.NullValuePromptShowForEmptyValue");
    this.patientWeight.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientWeight.Validator = (ICustomValidator) null;
    this.patientWeight.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.hospitalId, "hospitalId");
    this.hospitalId.EnterMoveNextControl = true;
    this.hospitalId.FormatString = (string) null;
    this.hospitalId.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.hospitalId.IsReadOnly = false;
    this.hospitalId.IsUndoing = false;
    this.hospitalId.Name = "hospitalId";
    this.hospitalId.Properties.AccessibleDescription = componentResourceManager.GetString("hospitalId.Properties.AccessibleDescription");
    this.hospitalId.Properties.AccessibleName = componentResourceManager.GetString("hospitalId.Properties.AccessibleName");
    this.hospitalId.Properties.Appearance.BackColor = Color.White;
    this.hospitalId.Properties.Appearance.BorderColor = Color.Yellow;
    this.hospitalId.Properties.Appearance.Options.UseBackColor = true;
    this.hospitalId.Properties.Appearance.Options.UseBorderColor = true;
    this.hospitalId.Properties.AutoHeight = (bool) componentResourceManager.GetObject("hospitalId.Properties.AutoHeight");
    this.hospitalId.Properties.BorderStyle = BorderStyles.Simple;
    this.hospitalId.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("hospitalId.Properties.Mask.AutoComplete");
    this.hospitalId.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("hospitalId.Properties.Mask.BeepOnError");
    this.hospitalId.Properties.Mask.EditMask = componentResourceManager.GetString("hospitalId.Properties.Mask.EditMask");
    this.hospitalId.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("hospitalId.Properties.Mask.IgnoreMaskBlank");
    this.hospitalId.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("hospitalId.Properties.Mask.MaskType");
    this.hospitalId.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("hospitalId.Properties.Mask.PlaceHolder");
    this.hospitalId.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("hospitalId.Properties.Mask.SaveLiteral");
    this.hospitalId.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("hospitalId.Properties.Mask.ShowPlaceHolders");
    this.hospitalId.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("hospitalId.Properties.Mask.UseMaskAsDisplayFormat");
    this.hospitalId.Properties.NullValuePrompt = componentResourceManager.GetString("hospitalId.Properties.NullValuePrompt");
    this.hospitalId.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("hospitalId.Properties.NullValuePromptShowForEmptyValue");
    this.hospitalId.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.hospitalId.Validator = (ICustomValidator) null;
    this.hospitalId.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientFreeText3, "patientFreeText3");
    this.patientFreeText3.EnterMoveNextControl = true;
    this.patientFreeText3.FormatString = (string) null;
    this.patientFreeText3.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientFreeText3.IsReadOnly = false;
    this.patientFreeText3.IsUndoing = false;
    this.patientFreeText3.Name = "patientFreeText3";
    this.patientFreeText3.Properties.AccessibleDescription = componentResourceManager.GetString("patientFreeText3.Properties.AccessibleDescription");
    this.patientFreeText3.Properties.AccessibleName = componentResourceManager.GetString("patientFreeText3.Properties.AccessibleName");
    this.patientFreeText3.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientFreeText3.Properties.Appearance.Options.UseBorderColor = true;
    this.patientFreeText3.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientFreeText3.Properties.AutoHeight");
    this.patientFreeText3.Properties.BorderStyle = BorderStyles.Simple;
    this.patientFreeText3.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientFreeText3.Properties.Mask.AutoComplete");
    this.patientFreeText3.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientFreeText3.Properties.Mask.BeepOnError");
    this.patientFreeText3.Properties.Mask.EditMask = componentResourceManager.GetString("patientFreeText3.Properties.Mask.EditMask");
    this.patientFreeText3.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientFreeText3.Properties.Mask.IgnoreMaskBlank");
    this.patientFreeText3.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientFreeText3.Properties.Mask.MaskType");
    this.patientFreeText3.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientFreeText3.Properties.Mask.PlaceHolder");
    this.patientFreeText3.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientFreeText3.Properties.Mask.SaveLiteral");
    this.patientFreeText3.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientFreeText3.Properties.Mask.ShowPlaceHolders");
    this.patientFreeText3.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientFreeText3.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientFreeText3.Properties.NullValuePrompt = componentResourceManager.GetString("patientFreeText3.Properties.NullValuePrompt");
    this.patientFreeText3.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientFreeText3.Properties.NullValuePromptShowForEmptyValue");
    this.patientFreeText3.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientFreeText3.Validator = (ICustomValidator) null;
    this.patientFreeText3.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientNicuState, "patientNicuState");
    this.patientNicuState.EnterMoveNextControl = true;
    this.patientNicuState.FormatString = (string) null;
    this.patientNicuState.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientNicuState.IsReadOnly = false;
    this.patientNicuState.IsUndoing = false;
    this.patientNicuState.Name = "patientNicuState";
    this.patientNicuState.Properties.AccessibleDescription = componentResourceManager.GetString("patientNicuState.Properties.AccessibleDescription");
    this.patientNicuState.Properties.AccessibleName = componentResourceManager.GetString("patientNicuState.Properties.AccessibleName");
    this.patientNicuState.Properties.Appearance.BorderColor = Color.LightGray;
    this.patientNicuState.Properties.Appearance.Options.UseBorderColor = true;
    this.patientNicuState.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientNicuState.Properties.AutoHeight");
    this.patientNicuState.Properties.BorderStyle = BorderStyles.Simple;
    this.patientNicuState.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("patientNicuState.Properties.Buttons"))
    });
    this.patientNicuState.Properties.NullValuePrompt = componentResourceManager.GetString("patientNicuState.Properties.NullValuePrompt");
    this.patientNicuState.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientNicuState.Properties.NullValuePromptShowForEmptyValue");
    this.patientNicuState.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.patientNicuState.ShowEmptyElement = true;
    this.patientNicuState.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientNicuState.Validator = (ICustomValidator) null;
    this.patientNicuState.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.patientHeight, "patientHeight");
    this.patientHeight.EnterMoveNextControl = true;
    this.patientHeight.FormatString = (string) null;
    this.patientHeight.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientHeight.IsReadOnly = false;
    this.patientHeight.IsUndoing = false;
    this.patientHeight.Name = "patientHeight";
    this.patientHeight.Properties.AccessibleDescription = componentResourceManager.GetString("patientHeight.Properties.AccessibleDescription");
    this.patientHeight.Properties.AccessibleName = componentResourceManager.GetString("patientHeight.Properties.AccessibleName");
    this.patientHeight.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientHeight.Properties.Appearance.Options.UseBorderColor = true;
    this.patientHeight.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientHeight.Properties.AutoHeight");
    this.patientHeight.Properties.BorderStyle = BorderStyles.Simple;
    this.patientHeight.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientHeight.Properties.Mask.AutoComplete");
    this.patientHeight.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientHeight.Properties.Mask.BeepOnError");
    this.patientHeight.Properties.Mask.EditMask = componentResourceManager.GetString("patientHeight.Properties.Mask.EditMask");
    this.patientHeight.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientHeight.Properties.Mask.IgnoreMaskBlank");
    this.patientHeight.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientHeight.Properties.Mask.MaskType");
    this.patientHeight.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientHeight.Properties.Mask.PlaceHolder");
    this.patientHeight.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientHeight.Properties.Mask.SaveLiteral");
    this.patientHeight.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientHeight.Properties.Mask.ShowPlaceHolders");
    this.patientHeight.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientHeight.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientHeight.Properties.NullValuePrompt = componentResourceManager.GetString("patientHeight.Properties.NullValuePrompt");
    this.patientHeight.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientHeight.Properties.NullValuePromptShowForEmptyValue");
    this.patientHeight.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientHeight.Validator = (ICustomValidator) null;
    this.patientHeight.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientConsentState, "patientConsentState");
    this.patientConsentState.EnterMoveNextControl = true;
    this.patientConsentState.FormatString = (string) null;
    this.patientConsentState.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientConsentState.IsReadOnly = false;
    this.patientConsentState.IsUndoing = false;
    this.patientConsentState.Name = "patientConsentState";
    this.patientConsentState.Properties.AccessibleDescription = componentResourceManager.GetString("patientConsentState.Properties.AccessibleDescription");
    this.patientConsentState.Properties.AccessibleName = componentResourceManager.GetString("patientConsentState.Properties.AccessibleName");
    this.patientConsentState.Properties.Appearance.BorderColor = Color.LightGray;
    this.patientConsentState.Properties.Appearance.Options.UseBorderColor = true;
    this.patientConsentState.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientConsentState.Properties.AutoHeight");
    this.patientConsentState.Properties.BorderStyle = BorderStyles.Simple;
    this.patientConsentState.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("patientConsentState.Properties.Buttons"))
    });
    this.patientConsentState.Properties.NullValuePrompt = componentResourceManager.GetString("patientConsentState.Properties.NullValuePrompt");
    this.patientConsentState.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientConsentState.Properties.NullValuePromptShowForEmptyValue");
    this.patientConsentState.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.patientConsentState.ShowEmptyElement = true;
    this.patientConsentState.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientConsentState.Validator = (ICustomValidator) null;
    this.patientConsentState.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.motherAddress1, "motherAddress1");
    this.motherAddress1.EnterMoveNextControl = true;
    this.motherAddress1.FormatString = (string) null;
    this.motherAddress1.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherAddress1.IsReadOnly = false;
    this.motherAddress1.IsUndoing = false;
    this.motherAddress1.Name = "motherAddress1";
    this.motherAddress1.Properties.AccessibleDescription = componentResourceManager.GetString("motherAddress1.Properties.AccessibleDescription");
    this.motherAddress1.Properties.AccessibleName = componentResourceManager.GetString("motherAddress1.Properties.AccessibleName");
    this.motherAddress1.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherAddress1.Properties.Appearance.Options.UseBorderColor = true;
    this.motherAddress1.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherAddress1.Properties.AutoHeight");
    this.motherAddress1.Properties.BorderStyle = BorderStyles.Simple;
    this.motherAddress1.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherAddress1.Properties.Mask.AutoComplete");
    this.motherAddress1.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherAddress1.Properties.Mask.BeepOnError");
    this.motherAddress1.Properties.Mask.EditMask = componentResourceManager.GetString("motherAddress1.Properties.Mask.EditMask");
    this.motherAddress1.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherAddress1.Properties.Mask.IgnoreMaskBlank");
    this.motherAddress1.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherAddress1.Properties.Mask.MaskType");
    this.motherAddress1.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherAddress1.Properties.Mask.PlaceHolder");
    this.motherAddress1.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherAddress1.Properties.Mask.SaveLiteral");
    this.motherAddress1.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherAddress1.Properties.Mask.ShowPlaceHolders");
    this.motherAddress1.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherAddress1.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherAddress1.Properties.NullValuePrompt = componentResourceManager.GetString("motherAddress1.Properties.NullValuePrompt");
    this.motherAddress1.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherAddress1.Properties.NullValuePromptShowForEmptyValue");
    this.motherAddress1.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherAddress1.Validator = (ICustomValidator) null;
    this.motherAddress1.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.motherCity, "motherCity");
    this.motherCity.EnterMoveNextControl = true;
    this.motherCity.FormatString = (string) null;
    this.motherCity.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherCity.IsReadOnly = false;
    this.motherCity.IsUndoing = false;
    this.motherCity.Name = "motherCity";
    this.motherCity.Properties.AccessibleDescription = componentResourceManager.GetString("motherCity.Properties.AccessibleDescription");
    this.motherCity.Properties.AccessibleName = componentResourceManager.GetString("motherCity.Properties.AccessibleName");
    this.motherCity.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherCity.Properties.Appearance.Options.UseBorderColor = true;
    this.motherCity.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherCity.Properties.AutoHeight");
    this.motherCity.Properties.BorderStyle = BorderStyles.Simple;
    this.motherCity.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherCity.Properties.Mask.AutoComplete");
    this.motherCity.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherCity.Properties.Mask.BeepOnError");
    this.motherCity.Properties.Mask.EditMask = componentResourceManager.GetString("motherCity.Properties.Mask.EditMask");
    this.motherCity.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherCity.Properties.Mask.IgnoreMaskBlank");
    this.motherCity.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherCity.Properties.Mask.MaskType");
    this.motherCity.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherCity.Properties.Mask.PlaceHolder");
    this.motherCity.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherCity.Properties.Mask.SaveLiteral");
    this.motherCity.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherCity.Properties.Mask.ShowPlaceHolders");
    this.motherCity.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherCity.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherCity.Properties.NullValuePrompt = componentResourceManager.GetString("motherCity.Properties.NullValuePrompt");
    this.motherCity.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherCity.Properties.NullValuePromptShowForEmptyValue");
    this.motherCity.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherCity.Validator = (ICustomValidator) null;
    this.motherCity.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.motherZip, "motherZip");
    this.motherZip.EnterMoveNextControl = true;
    this.motherZip.FormatString = (string) null;
    this.motherZip.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherZip.IsReadOnly = false;
    this.motherZip.IsUndoing = false;
    this.motherZip.Name = "motherZip";
    this.motherZip.Properties.AccessibleDescription = componentResourceManager.GetString("motherZip.Properties.AccessibleDescription");
    this.motherZip.Properties.AccessibleName = componentResourceManager.GetString("motherZip.Properties.AccessibleName");
    this.motherZip.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherZip.Properties.Appearance.Options.UseBorderColor = true;
    this.motherZip.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherZip.Properties.AutoHeight");
    this.motherZip.Properties.BorderStyle = BorderStyles.Simple;
    this.motherZip.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherZip.Properties.Mask.AutoComplete");
    this.motherZip.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherZip.Properties.Mask.BeepOnError");
    this.motherZip.Properties.Mask.EditMask = componentResourceManager.GetString("motherZip.Properties.Mask.EditMask");
    this.motherZip.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherZip.Properties.Mask.IgnoreMaskBlank");
    this.motherZip.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherZip.Properties.Mask.MaskType");
    this.motherZip.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherZip.Properties.Mask.PlaceHolder");
    this.motherZip.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherZip.Properties.Mask.SaveLiteral");
    this.motherZip.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherZip.Properties.Mask.ShowPlaceHolders");
    this.motherZip.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherZip.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherZip.Properties.NullValuePrompt = componentResourceManager.GetString("motherZip.Properties.NullValuePrompt");
    this.motherZip.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherZip.Properties.NullValuePromptShowForEmptyValue");
    this.motherZip.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherZip.Validator = (ICustomValidator) null;
    this.motherZip.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.motherLanguageCode, "motherLanguageCode");
    this.motherLanguageCode.EnterMoveNextControl = true;
    this.motherLanguageCode.FormatString = (string) null;
    this.motherLanguageCode.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherLanguageCode.IsActive = false;
    this.motherLanguageCode.IsReadOnly = false;
    this.motherLanguageCode.IsUndoing = false;
    this.motherLanguageCode.Name = "motherLanguageCode";
    this.motherLanguageCode.Properties.AccessibleDescription = componentResourceManager.GetString("motherLanguageCode.Properties.AccessibleDescription");
    this.motherLanguageCode.Properties.AccessibleName = componentResourceManager.GetString("motherLanguageCode.Properties.AccessibleName");
    this.motherLanguageCode.Properties.Appearance.BorderColor = Color.LightGray;
    this.motherLanguageCode.Properties.Appearance.Options.UseBorderColor = true;
    this.motherLanguageCode.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherLanguageCode.Properties.AutoHeight");
    this.motherLanguageCode.Properties.BorderStyle = BorderStyles.Simple;
    this.motherLanguageCode.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("motherLanguageCode.Properties.Buttons"))
    });
    this.motherLanguageCode.Properties.NullValuePrompt = componentResourceManager.GetString("motherLanguageCode.Properties.NullValuePrompt");
    this.motherLanguageCode.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherLanguageCode.Properties.NullValuePromptShowForEmptyValue");
    this.motherLanguageCode.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.motherLanguageCode.ShowEmptyElement = true;
    this.motherLanguageCode.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherLanguageCode.Validator = (ICustomValidator) null;
    this.motherLanguageCode.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.motherAlsoKnownAs, "motherAlsoKnownAs");
    this.motherAlsoKnownAs.EnterMoveNextControl = true;
    this.motherAlsoKnownAs.FormatString = (string) null;
    this.motherAlsoKnownAs.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherAlsoKnownAs.IsReadOnly = false;
    this.motherAlsoKnownAs.IsUndoing = false;
    this.motherAlsoKnownAs.Name = "motherAlsoKnownAs";
    this.motherAlsoKnownAs.Properties.AccessibleDescription = componentResourceManager.GetString("motherAlsoKnownAs.Properties.AccessibleDescription");
    this.motherAlsoKnownAs.Properties.AccessibleName = componentResourceManager.GetString("motherAlsoKnownAs.Properties.AccessibleName");
    this.motherAlsoKnownAs.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherAlsoKnownAs.Properties.Appearance.Options.UseBorderColor = true;
    this.motherAlsoKnownAs.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherAlsoKnownAs.Properties.AutoHeight");
    this.motherAlsoKnownAs.Properties.BorderStyle = BorderStyles.Simple;
    this.motherAlsoKnownAs.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherAlsoKnownAs.Properties.Mask.AutoComplete");
    this.motherAlsoKnownAs.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherAlsoKnownAs.Properties.Mask.BeepOnError");
    this.motherAlsoKnownAs.Properties.Mask.EditMask = componentResourceManager.GetString("motherAlsoKnownAs.Properties.Mask.EditMask");
    this.motherAlsoKnownAs.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherAlsoKnownAs.Properties.Mask.IgnoreMaskBlank");
    this.motherAlsoKnownAs.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherAlsoKnownAs.Properties.Mask.MaskType");
    this.motherAlsoKnownAs.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherAlsoKnownAs.Properties.Mask.PlaceHolder");
    this.motherAlsoKnownAs.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherAlsoKnownAs.Properties.Mask.SaveLiteral");
    this.motherAlsoKnownAs.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherAlsoKnownAs.Properties.Mask.ShowPlaceHolders");
    this.motherAlsoKnownAs.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherAlsoKnownAs.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherAlsoKnownAs.Properties.NullValuePrompt = componentResourceManager.GetString("motherAlsoKnownAs.Properties.NullValuePrompt");
    this.motherAlsoKnownAs.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherAlsoKnownAs.Properties.NullValuePromptShowForEmptyValue");
    this.motherAlsoKnownAs.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherAlsoKnownAs.Validator = (ICustomValidator) null;
    this.motherAlsoKnownAs.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.motherCountry, "motherCountry");
    this.motherCountry.EnterMoveNextControl = true;
    this.motherCountry.FormatString = (string) null;
    this.motherCountry.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherCountry.IsActive = false;
    this.motherCountry.IsReadOnly = false;
    this.motherCountry.IsUndoing = false;
    this.motherCountry.Name = "motherCountry";
    this.motherCountry.Properties.AccessibleDescription = componentResourceManager.GetString("motherCountry.Properties.AccessibleDescription");
    this.motherCountry.Properties.AccessibleName = componentResourceManager.GetString("motherCountry.Properties.AccessibleName");
    this.motherCountry.Properties.Appearance.BorderColor = Color.LightGray;
    this.motherCountry.Properties.Appearance.Options.UseBorderColor = true;
    this.motherCountry.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherCountry.Properties.AutoHeight");
    this.motherCountry.Properties.BorderStyle = BorderStyles.Simple;
    this.motherCountry.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("motherCountry.Properties.Buttons"))
    });
    this.motherCountry.Properties.NullValuePrompt = componentResourceManager.GetString("motherCountry.Properties.NullValuePrompt");
    this.motherCountry.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherCountry.Properties.NullValuePromptShowForEmptyValue");
    this.motherCountry.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.motherCountry.ShowEmptyElement = true;
    this.motherCountry.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherCountry.Validator = (ICustomValidator) null;
    this.motherCountry.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.motherSurname, "motherSurname");
    this.motherSurname.EnterMoveNextControl = true;
    this.motherSurname.FormatString = (string) null;
    this.motherSurname.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherSurname.IsReadOnly = false;
    this.motherSurname.IsUndoing = false;
    this.motherSurname.Name = "motherSurname";
    this.motherSurname.Properties.AccessibleDescription = componentResourceManager.GetString("motherSurname.Properties.AccessibleDescription");
    this.motherSurname.Properties.AccessibleName = componentResourceManager.GetString("motherSurname.Properties.AccessibleName");
    this.motherSurname.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherSurname.Properties.Appearance.Options.UseBorderColor = true;
    this.motherSurname.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherSurname.Properties.AutoHeight");
    this.motherSurname.Properties.BorderStyle = BorderStyles.Simple;
    this.motherSurname.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherSurname.Properties.Mask.AutoComplete");
    this.motherSurname.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherSurname.Properties.Mask.BeepOnError");
    this.motherSurname.Properties.Mask.EditMask = componentResourceManager.GetString("motherSurname.Properties.Mask.EditMask");
    this.motherSurname.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherSurname.Properties.Mask.IgnoreMaskBlank");
    this.motherSurname.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherSurname.Properties.Mask.MaskType");
    this.motherSurname.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherSurname.Properties.Mask.PlaceHolder");
    this.motherSurname.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherSurname.Properties.Mask.SaveLiteral");
    this.motherSurname.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherSurname.Properties.Mask.ShowPlaceHolders");
    this.motherSurname.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherSurname.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherSurname.Properties.NullValuePrompt = componentResourceManager.GetString("motherSurname.Properties.NullValuePrompt");
    this.motherSurname.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherSurname.Properties.NullValuePromptShowForEmptyValue");
    this.motherSurname.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherSurname.Validator = (ICustomValidator) null;
    this.motherSurname.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.motherTitle, "motherTitle");
    this.motherTitle.EnterMoveNextControl = true;
    this.motherTitle.FormatString = (string) null;
    this.motherTitle.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherTitle.IsReadOnly = false;
    this.motherTitle.IsUndoing = false;
    this.motherTitle.Name = "motherTitle";
    this.motherTitle.Properties.AccessibleDescription = componentResourceManager.GetString("motherTitle.Properties.AccessibleDescription");
    this.motherTitle.Properties.AccessibleName = componentResourceManager.GetString("motherTitle.Properties.AccessibleName");
    this.motherTitle.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherTitle.Properties.Appearance.Options.UseBorderColor = true;
    this.motherTitle.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherTitle.Properties.AutoHeight");
    this.motherTitle.Properties.BorderStyle = BorderStyles.Simple;
    this.motherTitle.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherTitle.Properties.Mask.AutoComplete");
    this.motherTitle.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherTitle.Properties.Mask.BeepOnError");
    this.motherTitle.Properties.Mask.EditMask = componentResourceManager.GetString("motherTitle.Properties.Mask.EditMask");
    this.motherTitle.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherTitle.Properties.Mask.IgnoreMaskBlank");
    this.motherTitle.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherTitle.Properties.Mask.MaskType");
    this.motherTitle.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherTitle.Properties.Mask.PlaceHolder");
    this.motherTitle.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherTitle.Properties.Mask.SaveLiteral");
    this.motherTitle.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherTitle.Properties.Mask.ShowPlaceHolders");
    this.motherTitle.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherTitle.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherTitle.Properties.NullValuePrompt = componentResourceManager.GetString("motherTitle.Properties.NullValuePrompt");
    this.motherTitle.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherTitle.Properties.NullValuePromptShowForEmptyValue");
    this.motherTitle.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherTitle.Validator = (ICustomValidator) null;
    this.motherTitle.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.caregiverAddress1, "caregiverAddress1");
    this.caregiverAddress1.EnterMoveNextControl = true;
    this.caregiverAddress1.FormatString = (string) null;
    this.caregiverAddress1.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverAddress1.IsReadOnly = false;
    this.caregiverAddress1.IsUndoing = false;
    this.caregiverAddress1.Name = "caregiverAddress1";
    this.caregiverAddress1.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverAddress1.Properties.AccessibleDescription");
    this.caregiverAddress1.Properties.AccessibleName = componentResourceManager.GetString("caregiverAddress1.Properties.AccessibleName");
    this.caregiverAddress1.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverAddress1.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverAddress1.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverAddress1.Properties.AutoHeight");
    this.caregiverAddress1.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverAddress1.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverAddress1.Properties.Mask.AutoComplete");
    this.caregiverAddress1.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverAddress1.Properties.Mask.BeepOnError");
    this.caregiverAddress1.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverAddress1.Properties.Mask.EditMask");
    this.caregiverAddress1.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverAddress1.Properties.Mask.IgnoreMaskBlank");
    this.caregiverAddress1.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverAddress1.Properties.Mask.MaskType");
    this.caregiverAddress1.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverAddress1.Properties.Mask.PlaceHolder");
    this.caregiverAddress1.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverAddress1.Properties.Mask.SaveLiteral");
    this.caregiverAddress1.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverAddress1.Properties.Mask.ShowPlaceHolders");
    this.caregiverAddress1.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverAddress1.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverAddress1.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverAddress1.Properties.NullValuePrompt");
    this.caregiverAddress1.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverAddress1.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverAddress1.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverAddress1.Validator = (ICustomValidator) null;
    this.caregiverAddress1.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientFreeText2, "patientFreeText2");
    this.patientFreeText2.EnterMoveNextControl = true;
    this.patientFreeText2.FormatString = (string) null;
    this.patientFreeText2.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientFreeText2.IsReadOnly = false;
    this.patientFreeText2.IsUndoing = false;
    this.patientFreeText2.Name = "patientFreeText2";
    this.patientFreeText2.Properties.AccessibleDescription = componentResourceManager.GetString("patientFreeText2.Properties.AccessibleDescription");
    this.patientFreeText2.Properties.AccessibleName = componentResourceManager.GetString("patientFreeText2.Properties.AccessibleName");
    this.patientFreeText2.Properties.Appearance.BorderColor = Color.LightGray;
    this.patientFreeText2.Properties.Appearance.Options.UseBorderColor = true;
    this.patientFreeText2.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientFreeText2.Properties.AutoHeight");
    this.patientFreeText2.Properties.BorderStyle = BorderStyles.Simple;
    this.patientFreeText2.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("patientFreeText2.Properties.Buttons"))
    });
    this.patientFreeText2.Properties.NullValuePrompt = componentResourceManager.GetString("patientFreeText2.Properties.NullValuePrompt");
    this.patientFreeText2.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientFreeText2.Properties.NullValuePromptShowForEmptyValue");
    this.patientFreeText2.ShowEmptyElement = true;
    this.patientFreeText2.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientFreeText2.Validator = (ICustomValidator) null;
    this.patientFreeText2.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.motherForename1, "motherForename1");
    this.motherForename1.EnterMoveNextControl = true;
    this.motherForename1.FormatString = (string) null;
    this.motherForename1.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherForename1.IsReadOnly = false;
    this.motherForename1.IsUndoing = false;
    this.motherForename1.Name = "motherForename1";
    this.motherForename1.Properties.AccessibleDescription = componentResourceManager.GetString("motherForename1.Properties.AccessibleDescription");
    this.motherForename1.Properties.AccessibleName = componentResourceManager.GetString("motherForename1.Properties.AccessibleName");
    this.motherForename1.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherForename1.Properties.Appearance.Options.UseBorderColor = true;
    this.motherForename1.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherForename1.Properties.AutoHeight");
    this.motherForename1.Properties.BorderStyle = BorderStyles.Simple;
    this.motherForename1.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherForename1.Properties.Mask.AutoComplete");
    this.motherForename1.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherForename1.Properties.Mask.BeepOnError");
    this.motherForename1.Properties.Mask.EditMask = componentResourceManager.GetString("motherForename1.Properties.Mask.EditMask");
    this.motherForename1.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherForename1.Properties.Mask.IgnoreMaskBlank");
    this.motherForename1.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherForename1.Properties.Mask.MaskType");
    this.motherForename1.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherForename1.Properties.Mask.PlaceHolder");
    this.motherForename1.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherForename1.Properties.Mask.SaveLiteral");
    this.motherForename1.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherForename1.Properties.Mask.ShowPlaceHolders");
    this.motherForename1.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherForename1.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherForename1.Properties.NullValuePrompt = componentResourceManager.GetString("motherForename1.Properties.NullValuePrompt");
    this.motherForename1.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherForename1.Properties.NullValuePromptShowForEmptyValue");
    this.motherForename1.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherForename1.Validator = (ICustomValidator) null;
    this.motherForename1.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.motherSocialSecurityNumber, "motherSocialSecurityNumber");
    this.motherSocialSecurityNumber.EnterMoveNextControl = true;
    this.motherSocialSecurityNumber.FormatString = (string) null;
    this.motherSocialSecurityNumber.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.motherSocialSecurityNumber.IsReadOnly = false;
    this.motherSocialSecurityNumber.IsUndoing = false;
    this.motherSocialSecurityNumber.Name = "motherSocialSecurityNumber";
    this.motherSocialSecurityNumber.Properties.AccessibleDescription = componentResourceManager.GetString("motherSocialSecurityNumber.Properties.AccessibleDescription");
    this.motherSocialSecurityNumber.Properties.AccessibleName = componentResourceManager.GetString("motherSocialSecurityNumber.Properties.AccessibleName");
    this.motherSocialSecurityNumber.Properties.Appearance.BorderColor = Color.Yellow;
    this.motherSocialSecurityNumber.Properties.Appearance.Options.UseBorderColor = true;
    this.motherSocialSecurityNumber.Properties.AutoHeight = (bool) componentResourceManager.GetObject("motherSocialSecurityNumber.Properties.AutoHeight");
    this.motherSocialSecurityNumber.Properties.BorderStyle = BorderStyles.Simple;
    this.motherSocialSecurityNumber.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("motherSocialSecurityNumber.Properties.Mask.AutoComplete");
    this.motherSocialSecurityNumber.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("motherSocialSecurityNumber.Properties.Mask.BeepOnError");
    this.motherSocialSecurityNumber.Properties.Mask.EditMask = componentResourceManager.GetString("motherSocialSecurityNumber.Properties.Mask.EditMask");
    this.motherSocialSecurityNumber.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("motherSocialSecurityNumber.Properties.Mask.IgnoreMaskBlank");
    this.motherSocialSecurityNumber.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("motherSocialSecurityNumber.Properties.Mask.MaskType");
    this.motherSocialSecurityNumber.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("motherSocialSecurityNumber.Properties.Mask.PlaceHolder");
    this.motherSocialSecurityNumber.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("motherSocialSecurityNumber.Properties.Mask.SaveLiteral");
    this.motherSocialSecurityNumber.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("motherSocialSecurityNumber.Properties.Mask.ShowPlaceHolders");
    this.motherSocialSecurityNumber.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("motherSocialSecurityNumber.Properties.Mask.UseMaskAsDisplayFormat");
    this.motherSocialSecurityNumber.Properties.NullValuePrompt = componentResourceManager.GetString("motherSocialSecurityNumber.Properties.NullValuePrompt");
    this.motherSocialSecurityNumber.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("motherSocialSecurityNumber.Properties.NullValuePromptShowForEmptyValue");
    this.motherSocialSecurityNumber.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.motherSocialSecurityNumber.Validator = (ICustomValidator) null;
    this.motherSocialSecurityNumber.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientFreeText1, "patientFreeText1");
    this.patientFreeText1.EnterMoveNextControl = true;
    this.patientFreeText1.FormatString = (string) null;
    this.patientFreeText1.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientFreeText1.IsReadOnly = false;
    this.patientFreeText1.IsUndoing = false;
    this.patientFreeText1.Name = "patientFreeText1";
    this.patientFreeText1.Properties.AccessibleDescription = componentResourceManager.GetString("patientFreeText1.Properties.AccessibleDescription");
    this.patientFreeText1.Properties.AccessibleName = componentResourceManager.GetString("patientFreeText1.Properties.AccessibleName");
    this.patientFreeText1.Properties.Appearance.BorderColor = Color.LightGray;
    this.patientFreeText1.Properties.Appearance.Options.UseBorderColor = true;
    this.patientFreeText1.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientFreeText1.Properties.AutoHeight");
    this.patientFreeText1.Properties.BorderStyle = BorderStyles.Simple;
    this.patientFreeText1.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("patientFreeText1.Properties.Buttons"))
    });
    this.patientFreeText1.Properties.NullValuePrompt = componentResourceManager.GetString("patientFreeText1.Properties.NullValuePrompt");
    this.patientFreeText1.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientFreeText1.Properties.NullValuePromptShowForEmptyValue");
    this.patientFreeText1.ShowEmptyElement = true;
    this.patientFreeText1.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientFreeText1.Validator = (ICustomValidator) null;
    this.patientFreeText1.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.patientMedication, "patientMedication");
    this.patientMedication.EnterMoveNextControl = true;
    this.patientMedication.FormatString = (string) null;
    this.patientMedication.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientMedication.IsReadOnly = false;
    this.patientMedication.IsUndoing = false;
    this.patientMedication.Name = "patientMedication";
    this.patientMedication.Properties.AccessibleDescription = componentResourceManager.GetString("patientMedication.Properties.AccessibleDescription");
    this.patientMedication.Properties.AccessibleName = componentResourceManager.GetString("patientMedication.Properties.AccessibleName");
    this.patientMedication.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientMedication.Properties.Appearance.Options.UseBorderColor = true;
    this.patientMedication.Properties.BorderStyle = BorderStyles.Simple;
    this.patientMedication.Properties.LinesCount = 3;
    this.patientMedication.Properties.NullValuePrompt = componentResourceManager.GetString("patientMedication.Properties.NullValuePrompt");
    this.patientMedication.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientMedication.Properties.NullValuePromptShowForEmptyValue");
    this.patientMedication.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientMedication.Validator = (ICustomValidator) null;
    this.patientMedication.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientDateOfBirth, "patientDateOfBirth");
    this.patientDateOfBirth.EnterMoveNextControl = true;
    this.patientDateOfBirth.FormatString = (string) null;
    this.patientDateOfBirth.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientDateOfBirth.IsActive = false;
    this.patientDateOfBirth.IsReadOnly = false;
    this.patientDateOfBirth.IsUndoing = false;
    this.patientDateOfBirth.Name = "patientDateOfBirth";
    this.patientDateOfBirth.Properties.AccessibleDescription = componentResourceManager.GetString("patientDateOfBirth.Properties.AccessibleDescription");
    this.patientDateOfBirth.Properties.AccessibleName = componentResourceManager.GetString("patientDateOfBirth.Properties.AccessibleName");
    this.patientDateOfBirth.Properties.Appearance.BackColor = Color.White;
    this.patientDateOfBirth.Properties.Appearance.BorderColor = Color.LightGray;
    this.patientDateOfBirth.Properties.Appearance.Options.UseBackColor = true;
    this.patientDateOfBirth.Properties.Appearance.Options.UseBorderColor = true;
    this.patientDateOfBirth.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.AutoHeight");
    this.patientDateOfBirth.Properties.BorderStyle = BorderStyles.Simple;
    this.patientDateOfBirth.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("patientDateOfBirth.Properties.Buttons"))
    });
    this.patientDateOfBirth.Properties.DisplayFormat.FormatString = "d";
    this.patientDateOfBirth.Properties.DisplayFormat.FormatType = FormatType.Custom;
    this.patientDateOfBirth.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientDateOfBirth.Properties.Mask.AutoComplete");
    this.patientDateOfBirth.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.Mask.BeepOnError");
    this.patientDateOfBirth.Properties.Mask.EditMask = componentResourceManager.GetString("patientDateOfBirth.Properties.Mask.EditMask");
    this.patientDateOfBirth.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.Mask.IgnoreMaskBlank");
    this.patientDateOfBirth.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientDateOfBirth.Properties.Mask.MaskType");
    this.patientDateOfBirth.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientDateOfBirth.Properties.Mask.PlaceHolder");
    this.patientDateOfBirth.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.Mask.SaveLiteral");
    this.patientDateOfBirth.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.Mask.ShowPlaceHolders");
    this.patientDateOfBirth.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientDateOfBirth.Properties.NullValuePrompt = componentResourceManager.GetString("patientDateOfBirth.Properties.NullValuePrompt");
    this.patientDateOfBirth.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.NullValuePromptShowForEmptyValue");
    this.patientDateOfBirth.Properties.VistaTimeProperties.AccessibleDescription = componentResourceManager.GetString("patientDateOfBirth.Properties.VistaTimeProperties.AccessibleDescription");
    this.patientDateOfBirth.Properties.VistaTimeProperties.AccessibleName = componentResourceManager.GetString("patientDateOfBirth.Properties.VistaTimeProperties.AccessibleName");
    this.patientDateOfBirth.Properties.VistaTimeProperties.AutoHeight = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.VistaTimeProperties.AutoHeight");
    this.patientDateOfBirth.Properties.VistaTimeProperties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.patientDateOfBirth.Properties.VistaTimeProperties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientDateOfBirth.Properties.VistaTimeProperties.Mask.AutoComplete");
    this.patientDateOfBirth.Properties.VistaTimeProperties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.VistaTimeProperties.Mask.BeepOnError");
    this.patientDateOfBirth.Properties.VistaTimeProperties.Mask.EditMask = componentResourceManager.GetString("patientDateOfBirth.Properties.VistaTimeProperties.Mask.EditMask");
    this.patientDateOfBirth.Properties.VistaTimeProperties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.VistaTimeProperties.Mask.IgnoreMaskBlank");
    this.patientDateOfBirth.Properties.VistaTimeProperties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientDateOfBirth.Properties.VistaTimeProperties.Mask.MaskType");
    this.patientDateOfBirth.Properties.VistaTimeProperties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientDateOfBirth.Properties.VistaTimeProperties.Mask.PlaceHolder");
    this.patientDateOfBirth.Properties.VistaTimeProperties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.VistaTimeProperties.Mask.SaveLiteral");
    this.patientDateOfBirth.Properties.VistaTimeProperties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.VistaTimeProperties.Mask.ShowPlaceHolders");
    this.patientDateOfBirth.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.VistaTimeProperties.Mask.UseMaskAsDisplayFormat");
    this.patientDateOfBirth.Properties.VistaTimeProperties.NullValuePrompt = componentResourceManager.GetString("patientDateOfBirth.Properties.VistaTimeProperties.NullValuePrompt");
    this.patientDateOfBirth.Properties.VistaTimeProperties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientDateOfBirth.Properties.VistaTimeProperties.NullValuePromptShowForEmptyValue");
    this.patientDateOfBirth.ShowModified = false;
    this.patientDateOfBirth.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientDateOfBirth.Validator = (ICustomValidator) null;
    this.patientDateOfBirth.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.caregiverMiddleInitial, "caregiverMiddleInitial");
    this.caregiverMiddleInitial.EnterMoveNextControl = true;
    this.caregiverMiddleInitial.FormatString = (string) null;
    this.caregiverMiddleInitial.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverMiddleInitial.IsReadOnly = false;
    this.caregiverMiddleInitial.IsUndoing = false;
    this.caregiverMiddleInitial.Name = "caregiverMiddleInitial";
    this.caregiverMiddleInitial.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverMiddleInitial.Properties.AccessibleDescription");
    this.caregiverMiddleInitial.Properties.AccessibleName = componentResourceManager.GetString("caregiverMiddleInitial.Properties.AccessibleName");
    this.caregiverMiddleInitial.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverMiddleInitial.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverMiddleInitial.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverMiddleInitial.Properties.AutoHeight");
    this.caregiverMiddleInitial.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverMiddleInitial.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverMiddleInitial.Properties.Mask.AutoComplete");
    this.caregiverMiddleInitial.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverMiddleInitial.Properties.Mask.BeepOnError");
    this.caregiverMiddleInitial.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverMiddleInitial.Properties.Mask.EditMask");
    this.caregiverMiddleInitial.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverMiddleInitial.Properties.Mask.IgnoreMaskBlank");
    this.caregiverMiddleInitial.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverMiddleInitial.Properties.Mask.MaskType");
    this.caregiverMiddleInitial.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverMiddleInitial.Properties.Mask.PlaceHolder");
    this.caregiverMiddleInitial.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverMiddleInitial.Properties.Mask.SaveLiteral");
    this.caregiverMiddleInitial.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverMiddleInitial.Properties.Mask.ShowPlaceHolders");
    this.caregiverMiddleInitial.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverMiddleInitial.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverMiddleInitial.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverMiddleInitial.Properties.NullValuePrompt");
    this.caregiverMiddleInitial.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverMiddleInitial.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverMiddleInitial.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverMiddleInitial.Validator = (ICustomValidator) null;
    this.caregiverMiddleInitial.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.caregiverForename2, "caregiverForename2");
    this.caregiverForename2.EnterMoveNextControl = true;
    this.caregiverForename2.FormatString = (string) null;
    this.caregiverForename2.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverForename2.IsReadOnly = false;
    this.caregiverForename2.IsUndoing = false;
    this.caregiverForename2.Name = "caregiverForename2";
    this.caregiverForename2.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverForename2.Properties.AccessibleDescription");
    this.caregiverForename2.Properties.AccessibleName = componentResourceManager.GetString("caregiverForename2.Properties.AccessibleName");
    this.caregiverForename2.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverForename2.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverForename2.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverForename2.Properties.AutoHeight");
    this.caregiverForename2.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverForename2.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverForename2.Properties.Mask.AutoComplete");
    this.caregiverForename2.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverForename2.Properties.Mask.BeepOnError");
    this.caregiverForename2.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverForename2.Properties.Mask.EditMask");
    this.caregiverForename2.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverForename2.Properties.Mask.IgnoreMaskBlank");
    this.caregiverForename2.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverForename2.Properties.Mask.MaskType");
    this.caregiverForename2.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverForename2.Properties.Mask.PlaceHolder");
    this.caregiverForename2.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverForename2.Properties.Mask.SaveLiteral");
    this.caregiverForename2.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverForename2.Properties.Mask.ShowPlaceHolders");
    this.caregiverForename2.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverForename2.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverForename2.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverForename2.Properties.NullValuePrompt");
    this.caregiverForename2.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverForename2.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverForename2.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverForename2.Validator = (ICustomValidator) null;
    this.caregiverForename2.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.caregiverCountry, "caregiverCountry");
    this.caregiverCountry.EnterMoveNextControl = true;
    this.caregiverCountry.FormatString = (string) null;
    this.caregiverCountry.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverCountry.IsReadOnly = false;
    this.caregiverCountry.IsUndoing = false;
    this.caregiverCountry.Name = "caregiverCountry";
    this.caregiverCountry.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverCountry.Properties.AccessibleDescription");
    this.caregiverCountry.Properties.AccessibleName = componentResourceManager.GetString("caregiverCountry.Properties.AccessibleName");
    this.caregiverCountry.Properties.Appearance.BorderColor = Color.LightGray;
    this.caregiverCountry.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverCountry.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverCountry.Properties.AutoHeight");
    this.caregiverCountry.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverCountry.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("caregiverCountry.Properties.Buttons"))
    });
    this.caregiverCountry.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverCountry.Properties.NullValuePrompt");
    this.caregiverCountry.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverCountry.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverCountry.ShowEmptyElement = true;
    this.caregiverCountry.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverCountry.Validator = (ICustomValidator) null;
    this.caregiverCountry.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.caregiverCity, "caregiverCity");
    this.caregiverCity.EnterMoveNextControl = true;
    this.caregiverCity.FormatString = (string) null;
    this.caregiverCity.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverCity.IsReadOnly = false;
    this.caregiverCity.IsUndoing = false;
    this.caregiverCity.Name = "caregiverCity";
    this.caregiverCity.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverCity.Properties.AccessibleDescription");
    this.caregiverCity.Properties.AccessibleName = componentResourceManager.GetString("caregiverCity.Properties.AccessibleName");
    this.caregiverCity.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverCity.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverCity.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverCity.Properties.AutoHeight");
    this.caregiverCity.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverCity.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverCity.Properties.Mask.AutoComplete");
    this.caregiverCity.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverCity.Properties.Mask.BeepOnError");
    this.caregiverCity.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverCity.Properties.Mask.EditMask");
    this.caregiverCity.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverCity.Properties.Mask.IgnoreMaskBlank");
    this.caregiverCity.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverCity.Properties.Mask.MaskType");
    this.caregiverCity.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverCity.Properties.Mask.PlaceHolder");
    this.caregiverCity.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverCity.Properties.Mask.SaveLiteral");
    this.caregiverCity.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverCity.Properties.Mask.ShowPlaceHolders");
    this.caregiverCity.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverCity.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverCity.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverCity.Properties.NullValuePrompt");
    this.caregiverCity.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverCity.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverCity.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverCity.Validator = (ICustomValidator) null;
    this.caregiverCity.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.caregiverZip, "caregiverZip");
    this.caregiverZip.EnterMoveNextControl = true;
    this.caregiverZip.FormatString = (string) null;
    this.caregiverZip.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverZip.IsReadOnly = false;
    this.caregiverZip.IsUndoing = false;
    this.caregiverZip.Name = "caregiverZip";
    this.caregiverZip.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverZip.Properties.AccessibleDescription");
    this.caregiverZip.Properties.AccessibleName = componentResourceManager.GetString("caregiverZip.Properties.AccessibleName");
    this.caregiverZip.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverZip.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverZip.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverZip.Properties.AutoHeight");
    this.caregiverZip.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverZip.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverZip.Properties.Mask.AutoComplete");
    this.caregiverZip.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverZip.Properties.Mask.BeepOnError");
    this.caregiverZip.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverZip.Properties.Mask.EditMask");
    this.caregiverZip.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverZip.Properties.Mask.IgnoreMaskBlank");
    this.caregiverZip.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverZip.Properties.Mask.MaskType");
    this.caregiverZip.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverZip.Properties.Mask.PlaceHolder");
    this.caregiverZip.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverZip.Properties.Mask.SaveLiteral");
    this.caregiverZip.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverZip.Properties.Mask.ShowPlaceHolders");
    this.caregiverZip.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverZip.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverZip.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverZip.Properties.NullValuePrompt");
    this.caregiverZip.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverZip.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverZip.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverZip.Validator = (ICustomValidator) null;
    this.caregiverZip.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientLanguageCode, "patientLanguageCode");
    this.patientLanguageCode.EnterMoveNextControl = true;
    this.patientLanguageCode.FormatString = (string) null;
    this.patientLanguageCode.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientLanguageCode.IsActive = false;
    this.patientLanguageCode.IsReadOnly = false;
    this.patientLanguageCode.IsUndoing = false;
    this.patientLanguageCode.Name = "patientLanguageCode";
    this.patientLanguageCode.Properties.AccessibleDescription = componentResourceManager.GetString("patientLanguageCode.Properties.AccessibleDescription");
    this.patientLanguageCode.Properties.AccessibleName = componentResourceManager.GetString("patientLanguageCode.Properties.AccessibleName");
    this.patientLanguageCode.Properties.Appearance.BorderColor = Color.LightGray;
    this.patientLanguageCode.Properties.Appearance.Options.UseBorderColor = true;
    this.patientLanguageCode.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientLanguageCode.Properties.AutoHeight");
    this.patientLanguageCode.Properties.BorderStyle = BorderStyles.Simple;
    this.patientLanguageCode.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("patientLanguageCode.Properties.Buttons"))
    });
    this.patientLanguageCode.Properties.NullValuePrompt = componentResourceManager.GetString("patientLanguageCode.Properties.NullValuePrompt");
    this.patientLanguageCode.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientLanguageCode.Properties.NullValuePromptShowForEmptyValue");
    this.patientLanguageCode.ShowEmptyElement = true;
    this.patientLanguageCode.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientLanguageCode.Validator = (ICustomValidator) null;
    this.patientLanguageCode.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.caregiverLanguageCode, "caregiverLanguageCode");
    this.caregiverLanguageCode.EnterMoveNextControl = true;
    this.caregiverLanguageCode.FormatString = (string) null;
    this.caregiverLanguageCode.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverLanguageCode.IsReadOnly = false;
    this.caregiverLanguageCode.IsUndoing = false;
    this.caregiverLanguageCode.Name = "caregiverLanguageCode";
    this.caregiverLanguageCode.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverLanguageCode.Properties.AccessibleDescription");
    this.caregiverLanguageCode.Properties.AccessibleName = componentResourceManager.GetString("caregiverLanguageCode.Properties.AccessibleName");
    this.caregiverLanguageCode.Properties.Appearance.BorderColor = Color.LightGray;
    this.caregiverLanguageCode.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverLanguageCode.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverLanguageCode.Properties.AutoHeight");
    this.caregiverLanguageCode.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverLanguageCode.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("caregiverLanguageCode.Properties.Buttons"))
    });
    this.caregiverLanguageCode.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverLanguageCode.Properties.NullValuePrompt");
    this.caregiverLanguageCode.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverLanguageCode.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverLanguageCode.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.caregiverLanguageCode.ShowEmptyElement = true;
    this.caregiverLanguageCode.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverLanguageCode.Validator = (ICustomValidator) null;
    this.caregiverLanguageCode.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.caregiverSocialSecurityNumber, "caregiverSocialSecurityNumber");
    this.caregiverSocialSecurityNumber.EnterMoveNextControl = true;
    this.caregiverSocialSecurityNumber.FormatString = (string) null;
    this.caregiverSocialSecurityNumber.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverSocialSecurityNumber.IsReadOnly = false;
    this.caregiverSocialSecurityNumber.IsUndoing = false;
    this.caregiverSocialSecurityNumber.Name = "caregiverSocialSecurityNumber";
    this.caregiverSocialSecurityNumber.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverSocialSecurityNumber.Properties.AccessibleDescription");
    this.caregiverSocialSecurityNumber.Properties.AccessibleName = componentResourceManager.GetString("caregiverSocialSecurityNumber.Properties.AccessibleName");
    this.caregiverSocialSecurityNumber.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverSocialSecurityNumber.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverSocialSecurityNumber.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverSocialSecurityNumber.Properties.AutoHeight");
    this.caregiverSocialSecurityNumber.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverSocialSecurityNumber.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverSocialSecurityNumber.Properties.Mask.AutoComplete");
    this.caregiverSocialSecurityNumber.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverSocialSecurityNumber.Properties.Mask.BeepOnError");
    this.caregiverSocialSecurityNumber.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverSocialSecurityNumber.Properties.Mask.EditMask");
    this.caregiverSocialSecurityNumber.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverSocialSecurityNumber.Properties.Mask.IgnoreMaskBlank");
    this.caregiverSocialSecurityNumber.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverSocialSecurityNumber.Properties.Mask.MaskType");
    this.caregiverSocialSecurityNumber.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverSocialSecurityNumber.Properties.Mask.PlaceHolder");
    this.caregiverSocialSecurityNumber.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverSocialSecurityNumber.Properties.Mask.SaveLiteral");
    this.caregiverSocialSecurityNumber.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverSocialSecurityNumber.Properties.Mask.ShowPlaceHolders");
    this.caregiverSocialSecurityNumber.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverSocialSecurityNumber.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverSocialSecurityNumber.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverSocialSecurityNumber.Properties.NullValuePrompt");
    this.caregiverSocialSecurityNumber.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverSocialSecurityNumber.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverSocialSecurityNumber.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverSocialSecurityNumber.Validator = (ICustomValidator) null;
    this.caregiverSocialSecurityNumber.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.caregiverSurname, "caregiverSurname");
    this.caregiverSurname.EnterMoveNextControl = true;
    this.caregiverSurname.FormatString = (string) null;
    this.caregiverSurname.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverSurname.IsReadOnly = false;
    this.caregiverSurname.IsUndoing = false;
    this.caregiverSurname.Name = "caregiverSurname";
    this.caregiverSurname.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverSurname.Properties.AccessibleDescription");
    this.caregiverSurname.Properties.AccessibleName = componentResourceManager.GetString("caregiverSurname.Properties.AccessibleName");
    this.caregiverSurname.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverSurname.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverSurname.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverSurname.Properties.AutoHeight");
    this.caregiverSurname.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverSurname.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverSurname.Properties.Mask.AutoComplete");
    this.caregiverSurname.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverSurname.Properties.Mask.BeepOnError");
    this.caregiverSurname.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverSurname.Properties.Mask.EditMask");
    this.caregiverSurname.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverSurname.Properties.Mask.IgnoreMaskBlank");
    this.caregiverSurname.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverSurname.Properties.Mask.MaskType");
    this.caregiverSurname.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverSurname.Properties.Mask.PlaceHolder");
    this.caregiverSurname.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverSurname.Properties.Mask.SaveLiteral");
    this.caregiverSurname.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverSurname.Properties.Mask.ShowPlaceHolders");
    this.caregiverSurname.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverSurname.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverSurname.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverSurname.Properties.NullValuePrompt");
    this.caregiverSurname.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverSurname.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverSurname.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverSurname.Validator = (ICustomValidator) null;
    this.caregiverSurname.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.caregiverForename1, "caregiverForename1");
    this.caregiverForename1.EnterMoveNextControl = true;
    this.caregiverForename1.FormatString = (string) null;
    this.caregiverForename1.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverForename1.IsReadOnly = false;
    this.caregiverForename1.IsUndoing = false;
    this.caregiverForename1.Name = "caregiverForename1";
    this.caregiverForename1.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverForename1.Properties.AccessibleDescription");
    this.caregiverForename1.Properties.AccessibleName = componentResourceManager.GetString("caregiverForename1.Properties.AccessibleName");
    this.caregiverForename1.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverForename1.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverForename1.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverForename1.Properties.AutoHeight");
    this.caregiverForename1.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverForename1.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverForename1.Properties.Mask.AutoComplete");
    this.caregiverForename1.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverForename1.Properties.Mask.BeepOnError");
    this.caregiverForename1.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverForename1.Properties.Mask.EditMask");
    this.caregiverForename1.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverForename1.Properties.Mask.IgnoreMaskBlank");
    this.caregiverForename1.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverForename1.Properties.Mask.MaskType");
    this.caregiverForename1.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverForename1.Properties.Mask.PlaceHolder");
    this.caregiverForename1.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverForename1.Properties.Mask.SaveLiteral");
    this.caregiverForename1.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverForename1.Properties.Mask.ShowPlaceHolders");
    this.caregiverForename1.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverForename1.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverForename1.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverForename1.Properties.NullValuePrompt");
    this.caregiverForename1.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverForename1.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverForename1.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverForename1.Validator = (ICustomValidator) null;
    this.caregiverForename1.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.caregiverTitle, "caregiverTitle");
    this.caregiverTitle.EnterMoveNextControl = true;
    this.caregiverTitle.FormatString = (string) null;
    this.caregiverTitle.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverTitle.IsReadOnly = false;
    this.caregiverTitle.IsUndoing = false;
    this.caregiverTitle.Name = "caregiverTitle";
    this.caregiverTitle.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverTitle.Properties.AccessibleDescription");
    this.caregiverTitle.Properties.AccessibleName = componentResourceManager.GetString("caregiverTitle.Properties.AccessibleName");
    this.caregiverTitle.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverTitle.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverTitle.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverTitle.Properties.AutoHeight");
    this.caregiverTitle.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverTitle.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverTitle.Properties.Mask.AutoComplete");
    this.caregiverTitle.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverTitle.Properties.Mask.BeepOnError");
    this.caregiverTitle.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverTitle.Properties.Mask.EditMask");
    this.caregiverTitle.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverTitle.Properties.Mask.IgnoreMaskBlank");
    this.caregiverTitle.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverTitle.Properties.Mask.MaskType");
    this.caregiverTitle.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverTitle.Properties.Mask.PlaceHolder");
    this.caregiverTitle.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverTitle.Properties.Mask.SaveLiteral");
    this.caregiverTitle.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverTitle.Properties.Mask.ShowPlaceHolders");
    this.caregiverTitle.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverTitle.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverTitle.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverTitle.Properties.NullValuePrompt");
    this.caregiverTitle.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverTitle.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverTitle.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverTitle.Validator = (ICustomValidator) null;
    this.caregiverTitle.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientNationality, "patientNationality");
    this.patientNationality.EnterMoveNextControl = true;
    this.patientNationality.FormatString = (string) null;
    this.patientNationality.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientNationality.IsReadOnly = false;
    this.patientNationality.IsUndoing = false;
    this.patientNationality.Name = "patientNationality";
    this.patientNationality.Properties.AccessibleDescription = componentResourceManager.GetString("patientNationality.Properties.AccessibleDescription");
    this.patientNationality.Properties.AccessibleName = componentResourceManager.GetString("patientNationality.Properties.AccessibleName");
    this.patientNationality.Properties.Appearance.BorderColor = Color.LightGray;
    this.patientNationality.Properties.Appearance.Options.UseBorderColor = true;
    this.patientNationality.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientNationality.Properties.AutoHeight");
    this.patientNationality.Properties.BorderStyle = BorderStyles.Simple;
    this.patientNationality.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("patientNationality.Properties.Buttons"))
    });
    this.patientNationality.Properties.NullValuePrompt = componentResourceManager.GetString("patientNationality.Properties.NullValuePrompt");
    this.patientNationality.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientNationality.Properties.NullValuePromptShowForEmptyValue");
    this.patientNationality.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.patientNationality.ShowEmptyElement = true;
    this.patientNationality.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientNationality.Validator = (ICustomValidator) null;
    this.patientNationality.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.caregiverAlsoKnownAs, "caregiverAlsoKnownAs");
    this.caregiverAlsoKnownAs.EnterMoveNextControl = true;
    this.caregiverAlsoKnownAs.FormatString = (string) null;
    this.caregiverAlsoKnownAs.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.caregiverAlsoKnownAs.IsReadOnly = false;
    this.caregiverAlsoKnownAs.IsUndoing = false;
    this.caregiverAlsoKnownAs.Name = "caregiverAlsoKnownAs";
    this.caregiverAlsoKnownAs.Properties.AccessibleDescription = componentResourceManager.GetString("caregiverAlsoKnownAs.Properties.AccessibleDescription");
    this.caregiverAlsoKnownAs.Properties.AccessibleName = componentResourceManager.GetString("caregiverAlsoKnownAs.Properties.AccessibleName");
    this.caregiverAlsoKnownAs.Properties.Appearance.BorderColor = Color.Yellow;
    this.caregiverAlsoKnownAs.Properties.Appearance.Options.UseBorderColor = true;
    this.caregiverAlsoKnownAs.Properties.AutoHeight = (bool) componentResourceManager.GetObject("caregiverAlsoKnownAs.Properties.AutoHeight");
    this.caregiverAlsoKnownAs.Properties.BorderStyle = BorderStyles.Simple;
    this.caregiverAlsoKnownAs.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("caregiverAlsoKnownAs.Properties.Mask.AutoComplete");
    this.caregiverAlsoKnownAs.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("caregiverAlsoKnownAs.Properties.Mask.BeepOnError");
    this.caregiverAlsoKnownAs.Properties.Mask.EditMask = componentResourceManager.GetString("caregiverAlsoKnownAs.Properties.Mask.EditMask");
    this.caregiverAlsoKnownAs.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("caregiverAlsoKnownAs.Properties.Mask.IgnoreMaskBlank");
    this.caregiverAlsoKnownAs.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("caregiverAlsoKnownAs.Properties.Mask.MaskType");
    this.caregiverAlsoKnownAs.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("caregiverAlsoKnownAs.Properties.Mask.PlaceHolder");
    this.caregiverAlsoKnownAs.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("caregiverAlsoKnownAs.Properties.Mask.SaveLiteral");
    this.caregiverAlsoKnownAs.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("caregiverAlsoKnownAs.Properties.Mask.ShowPlaceHolders");
    this.caregiverAlsoKnownAs.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("caregiverAlsoKnownAs.Properties.Mask.UseMaskAsDisplayFormat");
    this.caregiverAlsoKnownAs.Properties.NullValuePrompt = componentResourceManager.GetString("caregiverAlsoKnownAs.Properties.NullValuePrompt");
    this.caregiverAlsoKnownAs.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("caregiverAlsoKnownAs.Properties.NullValuePromptShowForEmptyValue");
    this.caregiverAlsoKnownAs.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.caregiverAlsoKnownAs.Validator = (ICustomValidator) null;
    this.caregiverAlsoKnownAs.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientBirthLocation, "patientBirthLocation");
    this.patientBirthLocation.EnterMoveNextControl = true;
    this.patientBirthLocation.FormatString = (string) null;
    this.patientBirthLocation.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientBirthLocation.IsReadOnly = false;
    this.patientBirthLocation.IsUndoing = false;
    this.patientBirthLocation.Name = "patientBirthLocation";
    this.patientBirthLocation.Properties.AccessibleDescription = componentResourceManager.GetString("patientBirthLocation.Properties.AccessibleDescription");
    this.patientBirthLocation.Properties.AccessibleName = componentResourceManager.GetString("patientBirthLocation.Properties.AccessibleName");
    this.patientBirthLocation.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientBirthLocation.Properties.Appearance.Options.UseBorderColor = true;
    this.patientBirthLocation.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientBirthLocation.Properties.AutoHeight");
    this.patientBirthLocation.Properties.BorderStyle = BorderStyles.Simple;
    this.patientBirthLocation.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientBirthLocation.Properties.Mask.AutoComplete");
    this.patientBirthLocation.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientBirthLocation.Properties.Mask.BeepOnError");
    this.patientBirthLocation.Properties.Mask.EditMask = componentResourceManager.GetString("patientBirthLocation.Properties.Mask.EditMask");
    this.patientBirthLocation.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientBirthLocation.Properties.Mask.IgnoreMaskBlank");
    this.patientBirthLocation.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientBirthLocation.Properties.Mask.MaskType");
    this.patientBirthLocation.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientBirthLocation.Properties.Mask.PlaceHolder");
    this.patientBirthLocation.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientBirthLocation.Properties.Mask.SaveLiteral");
    this.patientBirthLocation.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientBirthLocation.Properties.Mask.ShowPlaceHolders");
    this.patientBirthLocation.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientBirthLocation.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientBirthLocation.Properties.NullValuePrompt = componentResourceManager.GetString("patientBirthLocation.Properties.NullValuePrompt");
    this.patientBirthLocation.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientBirthLocation.Properties.NullValuePromptShowForEmptyValue");
    this.patientBirthLocation.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientBirthLocation.Validator = (ICustomValidator) null;
    this.patientBirthLocation.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientSocialSecurityNumber, "patientSocialSecurityNumber");
    this.patientSocialSecurityNumber.EnterMoveNextControl = true;
    this.patientSocialSecurityNumber.FormatString = (string) null;
    this.patientSocialSecurityNumber.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientSocialSecurityNumber.IsReadOnly = false;
    this.patientSocialSecurityNumber.IsUndoing = false;
    this.patientSocialSecurityNumber.Name = "patientSocialSecurityNumber";
    this.patientSocialSecurityNumber.Properties.AccessibleDescription = componentResourceManager.GetString("patientSocialSecurityNumber.Properties.AccessibleDescription");
    this.patientSocialSecurityNumber.Properties.AccessibleName = componentResourceManager.GetString("patientSocialSecurityNumber.Properties.AccessibleName");
    this.patientSocialSecurityNumber.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientSocialSecurityNumber.Properties.Appearance.Options.UseBorderColor = true;
    this.patientSocialSecurityNumber.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientSocialSecurityNumber.Properties.AutoHeight");
    this.patientSocialSecurityNumber.Properties.BorderStyle = BorderStyles.Simple;
    this.patientSocialSecurityNumber.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientSocialSecurityNumber.Properties.Mask.AutoComplete");
    this.patientSocialSecurityNumber.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientSocialSecurityNumber.Properties.Mask.BeepOnError");
    this.patientSocialSecurityNumber.Properties.Mask.EditMask = componentResourceManager.GetString("patientSocialSecurityNumber.Properties.Mask.EditMask");
    this.patientSocialSecurityNumber.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientSocialSecurityNumber.Properties.Mask.IgnoreMaskBlank");
    this.patientSocialSecurityNumber.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientSocialSecurityNumber.Properties.Mask.MaskType");
    this.patientSocialSecurityNumber.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientSocialSecurityNumber.Properties.Mask.PlaceHolder");
    this.patientSocialSecurityNumber.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientSocialSecurityNumber.Properties.Mask.SaveLiteral");
    this.patientSocialSecurityNumber.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientSocialSecurityNumber.Properties.Mask.ShowPlaceHolders");
    this.patientSocialSecurityNumber.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientSocialSecurityNumber.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientSocialSecurityNumber.Properties.NullValuePrompt = componentResourceManager.GetString("patientSocialSecurityNumber.Properties.NullValuePrompt");
    this.patientSocialSecurityNumber.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientSocialSecurityNumber.Properties.NullValuePromptShowForEmptyValue");
    this.patientSocialSecurityNumber.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientSocialSecurityNumber.Validator = (ICustomValidator) null;
    this.patientSocialSecurityNumber.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientAlsoKnownAs, "patientAlsoKnownAs");
    this.patientAlsoKnownAs.EnterMoveNextControl = true;
    this.patientAlsoKnownAs.FormatString = (string) null;
    this.patientAlsoKnownAs.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientAlsoKnownAs.IsReadOnly = false;
    this.patientAlsoKnownAs.IsUndoing = false;
    this.patientAlsoKnownAs.Name = "patientAlsoKnownAs";
    this.patientAlsoKnownAs.Properties.AccessibleDescription = componentResourceManager.GetString("patientAlsoKnownAs.Properties.AccessibleDescription");
    this.patientAlsoKnownAs.Properties.AccessibleName = componentResourceManager.GetString("patientAlsoKnownAs.Properties.AccessibleName");
    this.patientAlsoKnownAs.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientAlsoKnownAs.Properties.Appearance.Options.UseBorderColor = true;
    this.patientAlsoKnownAs.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientAlsoKnownAs.Properties.AutoHeight");
    this.patientAlsoKnownAs.Properties.BorderStyle = BorderStyles.Simple;
    this.patientAlsoKnownAs.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientAlsoKnownAs.Properties.Mask.AutoComplete");
    this.patientAlsoKnownAs.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientAlsoKnownAs.Properties.Mask.BeepOnError");
    this.patientAlsoKnownAs.Properties.Mask.EditMask = componentResourceManager.GetString("patientAlsoKnownAs.Properties.Mask.EditMask");
    this.patientAlsoKnownAs.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientAlsoKnownAs.Properties.Mask.IgnoreMaskBlank");
    this.patientAlsoKnownAs.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientAlsoKnownAs.Properties.Mask.MaskType");
    this.patientAlsoKnownAs.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientAlsoKnownAs.Properties.Mask.PlaceHolder");
    this.patientAlsoKnownAs.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientAlsoKnownAs.Properties.Mask.SaveLiteral");
    this.patientAlsoKnownAs.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientAlsoKnownAs.Properties.Mask.ShowPlaceHolders");
    this.patientAlsoKnownAs.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientAlsoKnownAs.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientAlsoKnownAs.Properties.NullValuePrompt = componentResourceManager.GetString("patientAlsoKnownAs.Properties.NullValuePrompt");
    this.patientAlsoKnownAs.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientAlsoKnownAs.Properties.NullValuePromptShowForEmptyValue");
    this.patientAlsoKnownAs.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientAlsoKnownAs.Validator = (ICustomValidator) null;
    this.patientAlsoKnownAs.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientMiddleInitial, "patientMiddleInitial");
    this.patientMiddleInitial.EnterMoveNextControl = true;
    this.patientMiddleInitial.FormatString = (string) null;
    this.patientMiddleInitial.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientMiddleInitial.IsReadOnly = false;
    this.patientMiddleInitial.IsUndoing = false;
    this.patientMiddleInitial.Name = "patientMiddleInitial";
    this.patientMiddleInitial.Properties.AccessibleDescription = componentResourceManager.GetString("patientMiddleInitial.Properties.AccessibleDescription");
    this.patientMiddleInitial.Properties.AccessibleName = componentResourceManager.GetString("patientMiddleInitial.Properties.AccessibleName");
    this.patientMiddleInitial.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientMiddleInitial.Properties.Appearance.Options.UseBorderColor = true;
    this.patientMiddleInitial.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientMiddleInitial.Properties.AutoHeight");
    this.patientMiddleInitial.Properties.BorderStyle = BorderStyles.Simple;
    this.patientMiddleInitial.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientMiddleInitial.Properties.Mask.AutoComplete");
    this.patientMiddleInitial.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientMiddleInitial.Properties.Mask.BeepOnError");
    this.patientMiddleInitial.Properties.Mask.EditMask = componentResourceManager.GetString("patientMiddleInitial.Properties.Mask.EditMask");
    this.patientMiddleInitial.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientMiddleInitial.Properties.Mask.IgnoreMaskBlank");
    this.patientMiddleInitial.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientMiddleInitial.Properties.Mask.MaskType");
    this.patientMiddleInitial.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientMiddleInitial.Properties.Mask.PlaceHolder");
    this.patientMiddleInitial.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientMiddleInitial.Properties.Mask.SaveLiteral");
    this.patientMiddleInitial.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientMiddleInitial.Properties.Mask.ShowPlaceHolders");
    this.patientMiddleInitial.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientMiddleInitial.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientMiddleInitial.Properties.NullValuePrompt = componentResourceManager.GetString("patientMiddleInitial.Properties.NullValuePrompt");
    this.patientMiddleInitial.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientMiddleInitial.Properties.NullValuePromptShowForEmptyValue");
    this.patientMiddleInitial.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientMiddleInitial.Validator = (ICustomValidator) null;
    this.patientMiddleInitial.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientForename2, "patientForename2");
    this.patientForename2.EnterMoveNextControl = true;
    this.patientForename2.FormatString = (string) null;
    this.patientForename2.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientForename2.IsReadOnly = false;
    this.patientForename2.IsUndoing = false;
    this.patientForename2.Name = "patientForename2";
    this.patientForename2.Properties.AccessibleDescription = componentResourceManager.GetString("patientForename2.Properties.AccessibleDescription");
    this.patientForename2.Properties.AccessibleName = componentResourceManager.GetString("patientForename2.Properties.AccessibleName");
    this.patientForename2.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientForename2.Properties.Appearance.Options.UseBorderColor = true;
    this.patientForename2.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientForename2.Properties.AutoHeight");
    this.patientForename2.Properties.BorderStyle = BorderStyles.Simple;
    this.patientForename2.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientForename2.Properties.Mask.AutoComplete");
    this.patientForename2.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientForename2.Properties.Mask.BeepOnError");
    this.patientForename2.Properties.Mask.EditMask = componentResourceManager.GetString("patientForename2.Properties.Mask.EditMask");
    this.patientForename2.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientForename2.Properties.Mask.IgnoreMaskBlank");
    this.patientForename2.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientForename2.Properties.Mask.MaskType");
    this.patientForename2.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientForename2.Properties.Mask.PlaceHolder");
    this.patientForename2.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientForename2.Properties.Mask.SaveLiteral");
    this.patientForename2.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientForename2.Properties.Mask.ShowPlaceHolders");
    this.patientForename2.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientForename2.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientForename2.Properties.NullValuePrompt = componentResourceManager.GetString("patientForename2.Properties.NullValuePrompt");
    this.patientForename2.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientForename2.Properties.NullValuePromptShowForEmptyValue");
    this.patientForename2.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientForename2.Validator = (ICustomValidator) null;
    this.patientForename2.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientForename1, "patientForename1");
    this.patientForename1.EnterMoveNextControl = true;
    this.patientForename1.FormatString = (string) null;
    this.patientForename1.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientForename1.IsReadOnly = false;
    this.patientForename1.IsUndoing = false;
    this.patientForename1.Name = "patientForename1";
    this.patientForename1.Properties.AccessibleDescription = componentResourceManager.GetString("patientForename1.Properties.AccessibleDescription");
    this.patientForename1.Properties.AccessibleName = componentResourceManager.GetString("patientForename1.Properties.AccessibleName");
    this.patientForename1.Properties.Appearance.BackColor = Color.White;
    this.patientForename1.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientForename1.Properties.Appearance.Options.UseBackColor = true;
    this.patientForename1.Properties.Appearance.Options.UseBorderColor = true;
    this.patientForename1.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientForename1.Properties.AutoHeight");
    this.patientForename1.Properties.BorderStyle = BorderStyles.Simple;
    this.patientForename1.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientForename1.Properties.Mask.AutoComplete");
    this.patientForename1.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientForename1.Properties.Mask.BeepOnError");
    this.patientForename1.Properties.Mask.EditMask = componentResourceManager.GetString("patientForename1.Properties.Mask.EditMask");
    this.patientForename1.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientForename1.Properties.Mask.IgnoreMaskBlank");
    this.patientForename1.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientForename1.Properties.Mask.MaskType");
    this.patientForename1.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientForename1.Properties.Mask.PlaceHolder");
    this.patientForename1.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientForename1.Properties.Mask.SaveLiteral");
    this.patientForename1.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientForename1.Properties.Mask.ShowPlaceHolders");
    this.patientForename1.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientForename1.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientForename1.Properties.NullValuePrompt = componentResourceManager.GetString("patientForename1.Properties.NullValuePrompt");
    this.patientForename1.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientForename1.Properties.NullValuePromptShowForEmptyValue");
    this.patientForename1.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientForename1.Validator = (ICustomValidator) null;
    this.patientForename1.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientTitle, "patientTitle");
    this.patientTitle.EnterMoveNextControl = true;
    this.patientTitle.FormatString = (string) null;
    this.patientTitle.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientTitle.IsReadOnly = false;
    this.patientTitle.IsUndoing = false;
    this.patientTitle.Name = "patientTitle";
    this.patientTitle.Properties.AccessibleDescription = componentResourceManager.GetString("patientTitle.Properties.AccessibleDescription");
    this.patientTitle.Properties.AccessibleName = componentResourceManager.GetString("patientTitle.Properties.AccessibleName");
    this.patientTitle.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientTitle.Properties.Appearance.Options.UseBorderColor = true;
    this.patientTitle.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientTitle.Properties.AutoHeight");
    this.patientTitle.Properties.BorderStyle = BorderStyles.Simple;
    this.patientTitle.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientTitle.Properties.Mask.AutoComplete");
    this.patientTitle.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientTitle.Properties.Mask.BeepOnError");
    this.patientTitle.Properties.Mask.EditMask = componentResourceManager.GetString("patientTitle.Properties.Mask.EditMask");
    this.patientTitle.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientTitle.Properties.Mask.IgnoreMaskBlank");
    this.patientTitle.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientTitle.Properties.Mask.MaskType");
    this.patientTitle.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientTitle.Properties.Mask.PlaceHolder");
    this.patientTitle.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientTitle.Properties.Mask.SaveLiteral");
    this.patientTitle.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientTitle.Properties.Mask.ShowPlaceHolders");
    this.patientTitle.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientTitle.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientTitle.Properties.NullValuePrompt = componentResourceManager.GetString("patientTitle.Properties.NullValuePrompt");
    this.patientTitle.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientTitle.Properties.NullValuePromptShowForEmptyValue");
    this.patientTitle.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientTitle.Validator = (ICustomValidator) null;
    this.patientTitle.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientSurname, "patientSurname");
    this.patientSurname.EnterMoveNextControl = true;
    this.patientSurname.FormatString = (string) null;
    this.patientSurname.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientSurname.IsReadOnly = false;
    this.patientSurname.IsUndoing = false;
    this.patientSurname.Name = "patientSurname";
    this.patientSurname.Properties.AccessibleDescription = componentResourceManager.GetString("patientSurname.Properties.AccessibleDescription");
    this.patientSurname.Properties.AccessibleName = componentResourceManager.GetString("patientSurname.Properties.AccessibleName");
    this.patientSurname.Properties.Appearance.BackColor = Color.White;
    this.patientSurname.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientSurname.Properties.Appearance.Options.UseBackColor = true;
    this.patientSurname.Properties.Appearance.Options.UseBorderColor = true;
    this.patientSurname.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientSurname.Properties.AutoHeight");
    this.patientSurname.Properties.BorderStyle = BorderStyles.Simple;
    this.patientSurname.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientSurname.Properties.Mask.AutoComplete");
    this.patientSurname.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientSurname.Properties.Mask.BeepOnError");
    this.patientSurname.Properties.Mask.EditMask = componentResourceManager.GetString("patientSurname.Properties.Mask.EditMask");
    this.patientSurname.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientSurname.Properties.Mask.IgnoreMaskBlank");
    this.patientSurname.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientSurname.Properties.Mask.MaskType");
    this.patientSurname.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientSurname.Properties.Mask.PlaceHolder");
    this.patientSurname.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientSurname.Properties.Mask.SaveLiteral");
    this.patientSurname.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientSurname.Properties.Mask.ShowPlaceHolders");
    this.patientSurname.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientSurname.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientSurname.Properties.NullValuePrompt = componentResourceManager.GetString("patientSurname.Properties.NullValuePrompt");
    this.patientSurname.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientSurname.Properties.NullValuePromptShowForEmptyValue");
    this.patientSurname.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientSurname.Validator = (ICustomValidator) null;
    this.patientSurname.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.patientGender, "patientGender");
    this.patientGender.EnterMoveNextControl = true;
    this.patientGender.FormatString = (string) null;
    this.patientGender.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.patientGender.IsReadOnly = false;
    this.patientGender.IsUndoing = false;
    this.patientGender.Name = "patientGender";
    this.patientGender.Properties.AccessibleDescription = componentResourceManager.GetString("patientGender.Properties.AccessibleDescription");
    this.patientGender.Properties.AccessibleName = componentResourceManager.GetString("patientGender.Properties.AccessibleName");
    this.patientGender.Properties.Appearance.BorderColor = Color.LightGray;
    this.patientGender.Properties.Appearance.Options.UseBorderColor = true;
    this.patientGender.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientGender.Properties.AutoHeight");
    this.patientGender.Properties.BorderStyle = BorderStyles.Simple;
    this.patientGender.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("patientGender.Properties.Buttons"))
    });
    this.patientGender.Properties.NullValuePrompt = componentResourceManager.GetString("patientGender.Properties.NullValuePrompt");
    this.patientGender.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientGender.Properties.NullValuePromptShowForEmptyValue");
    this.patientGender.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.patientGender.ShowEmptyElement = true;
    this.patientGender.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientGender.Validator = (ICustomValidator) null;
    this.patientGender.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.patientRecordNumber, "patientRecordNumber");
    this.patientRecordNumber.EnterMoveNextControl = true;
    this.patientRecordNumber.FormatString = "";
    this.patientRecordNumber.Id = new Guid("500e7e50-6fe7-4c6d-b182-886fe819ab1b");
    this.patientRecordNumber.IsReadOnly = false;
    this.patientRecordNumber.IsUndoing = false;
    this.patientRecordNumber.Name = "patientRecordNumber";
    this.patientRecordNumber.Properties.AccessibleDescription = componentResourceManager.GetString("patientRecordNumber.Properties.AccessibleDescription");
    this.patientRecordNumber.Properties.AccessibleName = componentResourceManager.GetString("patientRecordNumber.Properties.AccessibleName");
    this.patientRecordNumber.Properties.Appearance.BackColor = Color.White;
    this.patientRecordNumber.Properties.Appearance.BackColor2 = Color.White;
    this.patientRecordNumber.Properties.Appearance.BorderColor = Color.Yellow;
    this.patientRecordNumber.Properties.Appearance.Options.UseBackColor = true;
    this.patientRecordNumber.Properties.Appearance.Options.UseBorderColor = true;
    this.patientRecordNumber.Properties.AutoHeight = (bool) componentResourceManager.GetObject("patientRecordNumber.Properties.AutoHeight");
    this.patientRecordNumber.Properties.BorderStyle = BorderStyles.Simple;
    this.patientRecordNumber.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("patientRecordNumber.Properties.Mask.AutoComplete");
    this.patientRecordNumber.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("patientRecordNumber.Properties.Mask.BeepOnError");
    this.patientRecordNumber.Properties.Mask.EditMask = componentResourceManager.GetString("patientRecordNumber.Properties.Mask.EditMask");
    this.patientRecordNumber.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("patientRecordNumber.Properties.Mask.IgnoreMaskBlank");
    this.patientRecordNumber.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("patientRecordNumber.Properties.Mask.MaskType");
    this.patientRecordNumber.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("patientRecordNumber.Properties.Mask.PlaceHolder");
    this.patientRecordNumber.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("patientRecordNumber.Properties.Mask.SaveLiteral");
    this.patientRecordNumber.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("patientRecordNumber.Properties.Mask.ShowPlaceHolders");
    this.patientRecordNumber.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("patientRecordNumber.Properties.Mask.UseMaskAsDisplayFormat");
    this.patientRecordNumber.Properties.NullValuePrompt = componentResourceManager.GetString("patientRecordNumber.Properties.NullValuePrompt");
    this.patientRecordNumber.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("patientRecordNumber.Properties.NullValuePromptShowForEmptyValue");
    this.patientRecordNumber.StyleController = (IStyleController) this.layoutPatientDataEditor;
    this.patientRecordNumber.Validator = (ICustomValidator) null;
    this.patientRecordNumber.Value = (object) "";
    this.layoutPatientForename2.Control = (Control) this.patientForename2;
    componentResourceManager.ApplyResources((object) this.layoutPatientForename2, "layoutPatientForename2");
    this.layoutPatientForename2.Location = new Point(0, 517);
    this.layoutPatientForename2.Name = "layoutPatientForename2";
    this.layoutPatientForename2.Size = new Size(855, 31 /*0x1F*/);
    this.layoutPatientForename2.TextSize = new Size(50, 20);
    this.layoutPatientForename2.TextToControlDistance = 5;
    this.layoutPatientMiddleInitial.Control = (Control) this.patientMiddleInitial;
    componentResourceManager.ApplyResources((object) this.layoutPatientMiddleInitial, "layoutPatientMiddleInitial");
    this.layoutPatientMiddleInitial.Location = new Point(0, 541);
    this.layoutPatientMiddleInitial.Name = "layoutPatientMiddleInitial";
    this.layoutPatientMiddleInitial.Size = new Size(872, 31 /*0x1F*/);
    this.layoutPatientMiddleInitial.TextSize = new Size(50, 20);
    this.layoutPatientMiddleInitial.TextToControlDistance = 5;
    this.layoutCaregiverForename2.Control = (Control) this.caregiverForename2;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverForename2, "layoutCaregiverForename2");
    this.layoutCaregiverForename2.Location = new Point(0, 541);
    this.layoutCaregiverForename2.Name = "layoutCaregiverForename2";
    this.layoutCaregiverForename2.Size = new Size(872, 31 /*0x1F*/);
    this.layoutCaregiverForename2.TextSize = new Size(50, 20);
    this.layoutCaregiverForename2.TextToControlDistance = 5;
    this.layoutCaregiverMiddleInitial.Control = (Control) this.caregiverMiddleInitial;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverMiddleInitial, "layoutCaregiverMiddleInitial");
    this.layoutCaregiverMiddleInitial.Location = new Point(0, 541);
    this.layoutCaregiverMiddleInitial.Name = "layoutCaregiverMiddleInitial";
    this.layoutCaregiverMiddleInitial.Size = new Size(872, 31 /*0x1F*/);
    this.layoutCaregiverMiddleInitial.TextSize = new Size(50, 20);
    this.layoutCaregiverMiddleInitial.TextToControlDistance = 5;
    this.layoutPatientSocialSecurityNumber.Control = (Control) this.patientSocialSecurityNumber;
    componentResourceManager.ApplyResources((object) this.layoutPatientSocialSecurityNumber, "layoutPatientSocialSecurityNumber");
    this.layoutPatientSocialSecurityNumber.Location = new Point(697, 446);
    this.layoutPatientSocialSecurityNumber.Name = "layoutPatientSocialSecurityNumber";
    this.layoutPatientSocialSecurityNumber.Size = new Size(175, 126);
    this.layoutPatientSocialSecurityNumber.TextSize = new Size(50, 20);
    this.layoutPatientSocialSecurityNumber.TextToControlDistance = 5;
    this.layoutPatientLanguageCode.Control = (Control) this.patientLanguageCode;
    componentResourceManager.ApplyResources((object) this.layoutPatientLanguageCode, "layoutPatientLanguageCode");
    this.layoutPatientLanguageCode.Location = new Point(0, 541);
    this.layoutPatientLanguageCode.Name = "layoutPatientLanguageCode";
    this.layoutPatientLanguageCode.Size = new Size(872, 31 /*0x1F*/);
    this.layoutPatientLanguageCode.TextSize = new Size(50, 20);
    this.layoutPatientLanguageCode.TextToControlDistance = 5;
    this.layoutPatientTitle.Control = (Control) this.patientTitle;
    componentResourceManager.ApplyResources((object) this.layoutPatientTitle, "layoutPatientTitle");
    this.layoutPatientTitle.Location = new Point(0, 30);
    this.layoutPatientTitle.Name = "layoutPatientTitle";
    this.layoutPatientTitle.Size = new Size(168, 24);
    this.layoutPatientTitle.TextSize = new Size(65, 13);
    this.layoutPatientTitle.TextToControlDistance = 5;
    this.layoutControlItem1.Control = (Control) this.patientFreeText3;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(217, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(335, 24);
    this.layoutControlItem1.TextSize = new Size(79, 13);
    this.layoutControlItem1.TextToControlDistance = 5;
    this.layoutPatientAlsoKnownAs.Control = (Control) this.patientAlsoKnownAs;
    componentResourceManager.ApplyResources((object) this.layoutPatientAlsoKnownAs, "layoutPatientAlsoKnownAs");
    this.layoutPatientAlsoKnownAs.Location = new Point(557, 24);
    this.layoutPatientAlsoKnownAs.Name = "layoutPatientAlsoKnownAs";
    this.layoutPatientAlsoKnownAs.Size = new Size(263, 24);
    this.layoutPatientAlsoKnownAs.TextSize = new Size(98, 13);
    this.layoutPatientAlsoKnownAs.TextToControlDistance = 5;
    this.layoutMotherAlsoKnownAs.Control = (Control) this.motherAlsoKnownAs;
    componentResourceManager.ApplyResources((object) this.layoutMotherAlsoKnownAs, "layoutMotherAlsoKnownAs");
    this.layoutMotherAlsoKnownAs.Location = new Point(465, 24);
    this.layoutMotherAlsoKnownAs.Name = "layoutMotherAlsoKnownAs";
    this.layoutMotherAlsoKnownAs.Size = new Size(164, 48 /*0x30*/);
    this.layoutMotherAlsoKnownAs.TextSize = new Size(69, 13);
    this.layoutMotherAlsoKnownAs.TextToControlDistance = 5;
    this.layoutCaregiverAlsoKnownAs.Control = (Control) this.caregiverAlsoKnownAs;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverAlsoKnownAs, "layoutCaregiverAlsoKnownAs");
    this.layoutCaregiverAlsoKnownAs.Location = new Point(465, 24);
    this.layoutCaregiverAlsoKnownAs.Name = "layoutCaregiverAlsoKnownAs";
    this.layoutCaregiverAlsoKnownAs.Size = new Size(164, 48 /*0x30*/);
    this.layoutCaregiverAlsoKnownAs.TextSize = new Size(69, 13);
    this.layoutCaregiverAlsoKnownAs.TextToControlDistance = 5;
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutPatientData,
      (BaseLayoutItem) this.layoutPatientMedicalInformation,
      (BaseLayoutItem) this.tabbedControlGroup1
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "Root";
    this.layoutControlGroup1.Size = new Size(874, 574);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutPatientData, "layoutPatientData");
    this.layoutPatientData.Items.AddRange(new BaseLayoutItem[16 /*0x10*/]
    {
      (BaseLayoutItem) this.emptySpaceItem5,
      (BaseLayoutItem) this.emptySpaceItem3,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutPatientGender,
      (BaseLayoutItem) this.layoutPatientForename1,
      (BaseLayoutItem) this.layoutPatientSurname,
      (BaseLayoutItem) this.layoutPatientBirthLocation,
      (BaseLayoutItem) this.layoutPatientNationality,
      (BaseLayoutItem) this.layoutPatientDateOfBirth,
      (BaseLayoutItem) this.layoutPatientRecordNumber,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutPatientConsentState,
      (BaseLayoutItem) this.layoutPatientNicuState,
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutPatientWeight,
      (BaseLayoutItem) this.layoutControlItem3
    });
    this.layoutPatientData.Location = new Point(0, 0);
    this.layoutPatientData.Name = "layoutPatientData";
    this.layoutPatientData.Size = new Size(854, 164);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem5, "emptySpaceItem5");
    this.emptySpaceItem5.Location = new Point(558, 24);
    this.emptySpaceItem5.Name = "emptySpaceItem5";
    this.emptySpaceItem5.Size = new Size(272, 24);
    this.emptySpaceItem5.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem3, "emptySpaceItem3");
    this.emptySpaceItem3.Location = new Point(820, 48 /*0x30*/);
    this.emptySpaceItem3.Name = "emptySpaceItem3";
    this.emptySpaceItem3.Size = new Size(10, 24);
    this.emptySpaceItem3.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(558, 72);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(272, 48 /*0x30*/);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.layoutPatientGender.Control = (Control) this.patientGender;
    componentResourceManager.ApplyResources((object) this.layoutPatientGender, "layoutPatientGender");
    this.layoutPatientGender.Location = new Point(0, 72);
    this.layoutPatientGender.Name = "layoutPatientGender";
    this.layoutPatientGender.Size = new Size(216, 24);
    this.layoutPatientGender.TextSize = new Size(113, 13);
    this.layoutPatientForename1.Control = (Control) this.patientForename1;
    componentResourceManager.ApplyResources((object) this.layoutPatientForename1, "layoutPatientForename1");
    this.layoutPatientForename1.Location = new Point(0, 24);
    this.layoutPatientForename1.Name = "layoutPatientForename1";
    this.layoutPatientForename1.Size = new Size(216, 24);
    this.layoutPatientForename1.TextSize = new Size(113, 13);
    this.layoutPatientSurname.Control = (Control) this.patientSurname;
    componentResourceManager.ApplyResources((object) this.layoutPatientSurname, "layoutPatientSurname");
    this.layoutPatientSurname.Location = new Point(216, 24);
    this.layoutPatientSurname.Name = "layoutPatientSurname";
    this.layoutPatientSurname.Size = new Size(342, 24);
    this.layoutPatientSurname.TextSize = new Size(113, 13);
    this.layoutPatientBirthLocation.Control = (Control) this.patientBirthLocation;
    componentResourceManager.ApplyResources((object) this.layoutPatientBirthLocation, "layoutPatientBirthLocation");
    this.layoutPatientBirthLocation.Location = new Point(216, 48 /*0x30*/);
    this.layoutPatientBirthLocation.Name = "layoutPatientBirthLocation";
    this.layoutPatientBirthLocation.Size = new Size(342, 24);
    this.layoutPatientBirthLocation.TextSize = new Size(113, 13);
    this.layoutPatientNationality.Control = (Control) this.patientNationality;
    componentResourceManager.ApplyResources((object) this.layoutPatientNationality, "layoutPatientNationality");
    this.layoutPatientNationality.Location = new Point(558, 48 /*0x30*/);
    this.layoutPatientNationality.Name = "layoutPatientNationality";
    this.layoutPatientNationality.Size = new Size(262, 24);
    this.layoutPatientNationality.TextSize = new Size(113, 13);
    this.layoutPatientDateOfBirth.Control = (Control) this.patientDateOfBirth;
    componentResourceManager.ApplyResources((object) this.layoutPatientDateOfBirth, "layoutPatientDateOfBirth");
    this.layoutPatientDateOfBirth.Location = new Point(0, 48 /*0x30*/);
    this.layoutPatientDateOfBirth.Name = "layoutPatientDateOfBirth";
    this.layoutPatientDateOfBirth.Size = new Size(216, 24);
    this.layoutPatientDateOfBirth.TextSize = new Size(113, 13);
    this.layoutPatientRecordNumber.Control = (Control) this.patientRecordNumber;
    componentResourceManager.ApplyResources((object) this.layoutPatientRecordNumber, "layoutPatientRecordNumber");
    this.layoutPatientRecordNumber.Location = new Point(0, 0);
    this.layoutPatientRecordNumber.Name = "layoutPatientRecordNumber";
    this.layoutPatientRecordNumber.Size = new Size(216, 24);
    this.layoutPatientRecordNumber.TextSize = new Size(113, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(438, 0);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(392, 24);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    this.layoutPatientConsentState.Control = (Control) this.patientConsentState;
    componentResourceManager.ApplyResources((object) this.layoutPatientConsentState, "layoutPatientConsentState");
    this.layoutPatientConsentState.Location = new Point(0, 96 /*0x60*/);
    this.layoutPatientConsentState.Name = "layoutPatientConsentState";
    this.layoutPatientConsentState.Size = new Size(216, 24);
    this.layoutPatientConsentState.TextSize = new Size(113, 13);
    this.layoutPatientNicuState.Control = (Control) this.patientNicuState;
    componentResourceManager.ApplyResources((object) this.layoutPatientNicuState, "layoutPatientNicuState");
    this.layoutPatientNicuState.Location = new Point(216, 96 /*0x60*/);
    this.layoutPatientNicuState.Name = "layoutPatientNicuState";
    this.layoutPatientNicuState.Size = new Size(171, 24);
    this.layoutPatientNicuState.TextSize = new Size(113, 13);
    this.layoutControlItem2.Control = (Control) this.hospitalId;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(216, 0);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(222, 24);
    this.layoutControlItem2.TextSize = new Size(113, 13);
    this.layoutPatientWeight.Control = (Control) this.patientWeight;
    componentResourceManager.ApplyResources((object) this.layoutPatientWeight, "layoutPatientWeight");
    this.layoutPatientWeight.Location = new Point(216, 72);
    this.layoutPatientWeight.Name = "layoutPatientWeight";
    this.layoutPatientWeight.Size = new Size(171, 24);
    this.layoutPatientWeight.TextSize = new Size(113, 13);
    this.layoutControlItem3.Control = (Control) this.patientHeight;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(387, 72);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(171, 48 /*0x30*/);
    this.layoutControlItem3.TextSize = new Size(113, 13);
    componentResourceManager.ApplyResources((object) this.layoutPatientMedicalInformation, "layoutPatientMedicalInformation");
    this.layoutPatientMedicalInformation.ExpandButtonLocation = GroupElementLocation.AfterText;
    this.layoutPatientMedicalInformation.ExpandButtonVisible = true;
    this.layoutPatientMedicalInformation.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.emptySpaceItem15,
      (BaseLayoutItem) this.emptySpaceItem16,
      (BaseLayoutItem) this.layoutPatientMedication,
      (BaseLayoutItem) this.layoutPatientFreeText1,
      (BaseLayoutItem) this.layoutPatientFreeText2
    });
    this.layoutPatientMedicalInformation.Location = new Point(0, 354);
    this.layoutPatientMedicalInformation.Name = "layoutPatientMedicalInformation";
    this.layoutPatientMedicalInformation.Size = new Size(854, 200);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem15, "emptySpaceItem15");
    this.emptySpaceItem15.Location = new Point(471, 48 /*0x30*/);
    this.emptySpaceItem15.Name = "emptySpaceItem15";
    this.emptySpaceItem15.Size = new Size(359, 108);
    this.emptySpaceItem15.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem16, "emptySpaceItem16");
    this.emptySpaceItem16.Location = new Point(397, 0);
    this.emptySpaceItem16.Name = "emptySpaceItem16";
    this.emptySpaceItem16.Size = new Size(74, 156);
    this.emptySpaceItem16.TextSize = new Size(0, 0);
    this.layoutPatientMedication.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutPatientMedication.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    this.layoutPatientMedication.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Top;
    this.layoutPatientMedication.Control = (Control) this.patientMedication;
    componentResourceManager.ApplyResources((object) this.layoutPatientMedication, "layoutPatientMedication");
    this.layoutPatientMedication.Location = new Point(0, 0);
    this.layoutPatientMedication.Name = "layoutPatientMedication";
    this.layoutPatientMedication.Size = new Size(397, 156);
    this.layoutPatientMedication.TextSize = new Size(113, 13);
    this.layoutPatientFreeText1.Control = (Control) this.patientFreeText1;
    componentResourceManager.ApplyResources((object) this.layoutPatientFreeText1, "layoutPatientFreeText1");
    this.layoutPatientFreeText1.Location = new Point(471, 0);
    this.layoutPatientFreeText1.Name = "layoutPatientFreeText1";
    this.layoutPatientFreeText1.Size = new Size(359, 24);
    this.layoutPatientFreeText1.TextSize = new Size(113, 13);
    this.layoutPatientFreeText2.Control = (Control) this.patientFreeText2;
    componentResourceManager.ApplyResources((object) this.layoutPatientFreeText2, "layoutPatientFreeText2");
    this.layoutPatientFreeText2.Location = new Point(471, 24);
    this.layoutPatientFreeText2.Name = "layoutPatientFreeText2";
    this.layoutPatientFreeText2.Size = new Size(359, 24);
    this.layoutPatientFreeText2.TextSize = new Size(113, 13);
    componentResourceManager.ApplyResources((object) this.tabbedControlGroup1, "tabbedControlGroup1");
    this.tabbedControlGroup1.Location = new Point(0, 164);
    this.tabbedControlGroup1.Name = "tabbedControlGroup1";
    this.tabbedControlGroup1.SelectedTabPage = (LayoutGroup) this.layoutCaregiverGroup;
    this.tabbedControlGroup1.SelectedTabPageIndex = 1;
    this.tabbedControlGroup1.Size = new Size(854, 190);
    this.tabbedControlGroup1.TabPages.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutMotherGroup,
      (BaseLayoutItem) this.layoutCaregiverGroup
    });
    componentResourceManager.ApplyResources((object) this.layoutMotherGroup, "layoutMotherGroup");
    this.layoutMotherGroup.Items.AddRange(new BaseLayoutItem[15]
    {
      (BaseLayoutItem) this.layoutMotherTitle,
      (BaseLayoutItem) this.layoutMotherForename1,
      (BaseLayoutItem) this.layoutMotherSurname,
      (BaseLayoutItem) this.layoutMotherSocialSecurityNumber,
      (BaseLayoutItem) this.layoutMotherLanguageCode,
      (BaseLayoutItem) this.emptySpaceItem8,
      (BaseLayoutItem) this.layoutMotherZip,
      (BaseLayoutItem) this.layoutMotherCity,
      (BaseLayoutItem) this.emptySpaceItem12,
      (BaseLayoutItem) this.layoutMotherStreet,
      (BaseLayoutItem) this.layoutMotherCountry,
      (BaseLayoutItem) this.emptySpaceItem18,
      (BaseLayoutItem) this.layoutMotherPhone,
      (BaseLayoutItem) this.layoutMotherCellPhone,
      (BaseLayoutItem) this.layoutMotherFax
    });
    this.layoutMotherGroup.Location = new Point(0, 0);
    this.layoutMotherGroup.Name = "layoutMotherGroup";
    this.layoutMotherGroup.Size = new Size(830, 144 /*0x90*/);
    this.layoutMotherTitle.Control = (Control) this.motherTitle;
    componentResourceManager.ApplyResources((object) this.layoutMotherTitle, "layoutMotherTitle");
    this.layoutMotherTitle.Location = new Point(0, 0);
    this.layoutMotherTitle.Name = "layoutMotherTitle";
    this.layoutMotherTitle.Size = new Size(221, 24);
    this.layoutMotherTitle.TextSize = new Size(113, 13);
    this.layoutMotherForename1.Control = (Control) this.motherForename1;
    componentResourceManager.ApplyResources((object) this.layoutMotherForename1, "layoutMotherForename1");
    this.layoutMotherForename1.Location = new Point(0, 24);
    this.layoutMotherForename1.Name = "layoutMotherForename1";
    this.layoutMotherForename1.Size = new Size(221, 24);
    this.layoutMotherForename1.TextSize = new Size(113, 13);
    this.layoutMotherSurname.Control = (Control) this.motherSurname;
    componentResourceManager.ApplyResources((object) this.layoutMotherSurname, "layoutMotherSurname");
    this.layoutMotherSurname.Location = new Point(221, 24);
    this.layoutMotherSurname.Name = "layoutMotherSurname";
    this.layoutMotherSurname.Size = new Size(337, 48 /*0x30*/);
    this.layoutMotherSurname.TextSize = new Size(113, 13);
    this.layoutMotherSocialSecurityNumber.Control = (Control) this.motherSocialSecurityNumber;
    componentResourceManager.ApplyResources((object) this.layoutMotherSocialSecurityNumber, "layoutMotherSocialSecurityNumber");
    this.layoutMotherSocialSecurityNumber.Location = new Point(221, 0);
    this.layoutMotherSocialSecurityNumber.Name = "layoutMotherSocialSecurityNumber";
    this.layoutMotherSocialSecurityNumber.Size = new Size(337, 24);
    this.layoutMotherSocialSecurityNumber.TextSize = new Size(113, 13);
    this.layoutMotherLanguageCode.Control = (Control) this.motherLanguageCode;
    componentResourceManager.ApplyResources((object) this.layoutMotherLanguageCode, "layoutMotherLanguageCode");
    this.layoutMotherLanguageCode.Location = new Point(0, 48 /*0x30*/);
    this.layoutMotherLanguageCode.Name = "layoutMotherLanguageCode";
    this.layoutMotherLanguageCode.Size = new Size(221, 24);
    this.layoutMotherLanguageCode.TextSize = new Size(113, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem8, "emptySpaceItem8");
    this.emptySpaceItem8.Location = new Point(558, 0);
    this.emptySpaceItem8.MinSize = new Size(110, 30);
    this.emptySpaceItem8.Name = "emptySpaceItem8";
    this.emptySpaceItem8.Size = new Size(272, 72);
    this.emptySpaceItem8.SizeConstraintsType = SizeConstraintsType.Custom;
    this.emptySpaceItem8.TextSize = new Size(0, 0);
    this.layoutMotherZip.Control = (Control) this.motherZip;
    componentResourceManager.ApplyResources((object) this.layoutMotherZip, "layoutMotherZip");
    this.layoutMotherZip.Location = new Point(0, 96 /*0x60*/);
    this.layoutMotherZip.Name = "layoutMotherZip";
    this.layoutMotherZip.Size = new Size(171, 24);
    this.layoutMotherZip.TextSize = new Size(113, 13);
    this.layoutMotherCity.Control = (Control) this.motherCity;
    componentResourceManager.ApplyResources((object) this.layoutMotherCity, "layoutMotherCity");
    this.layoutMotherCity.Location = new Point(171, 96 /*0x60*/);
    this.layoutMotherCity.Name = "layoutMotherCity";
    this.layoutMotherCity.Size = new Size(233, 24);
    this.layoutMotherCity.TextSize = new Size(113, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem12, "emptySpaceItem12");
    this.emptySpaceItem12.Location = new Point(404, 72);
    this.emptySpaceItem12.Name = "emptySpaceItem12";
    this.emptySpaceItem12.Size = new Size(153, 48 /*0x30*/);
    this.emptySpaceItem12.TextSize = new Size(0, 0);
    this.layoutMotherStreet.Control = (Control) this.motherAddress1;
    componentResourceManager.ApplyResources((object) this.layoutMotherStreet, "layoutMotherStreet");
    this.layoutMotherStreet.Location = new Point(0, 72);
    this.layoutMotherStreet.Name = "layoutMotherStreet";
    this.layoutMotherStreet.Size = new Size(404, 24);
    this.layoutMotherStreet.TextSize = new Size(113, 13);
    this.layoutMotherCountry.Control = (Control) this.motherCountry;
    componentResourceManager.ApplyResources((object) this.layoutMotherCountry, "layoutMotherCountry");
    this.layoutMotherCountry.Location = new Point(0, 120);
    this.layoutMotherCountry.Name = "layoutMotherCountry";
    this.layoutMotherCountry.Size = new Size(404, 24);
    this.layoutMotherCountry.TextSize = new Size(113, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem18, "emptySpaceItem18");
    this.emptySpaceItem18.Location = new Point(404, 120);
    this.emptySpaceItem18.Name = "emptySpaceItem18";
    this.emptySpaceItem18.Size = new Size(153, 24);
    this.emptySpaceItem18.TextSize = new Size(0, 0);
    this.layoutMotherPhone.Control = (Control) this.motherPhone;
    componentResourceManager.ApplyResources((object) this.layoutMotherPhone, "layoutMotherPhone");
    this.layoutMotherPhone.Location = new Point(557, 72);
    this.layoutMotherPhone.Name = "layoutMotherPhone";
    this.layoutMotherPhone.Size = new Size(273, 24);
    this.layoutMotherPhone.TextSize = new Size(113, 13);
    this.layoutMotherCellPhone.Control = (Control) this.motherCellPhone;
    componentResourceManager.ApplyResources((object) this.layoutMotherCellPhone, "layoutMotherCellPhone");
    this.layoutMotherCellPhone.Location = new Point(557, 96 /*0x60*/);
    this.layoutMotherCellPhone.Name = "layoutMotherCellPhone";
    this.layoutMotherCellPhone.Size = new Size(273, 24);
    this.layoutMotherCellPhone.TextSize = new Size(113, 13);
    this.layoutMotherFax.Control = (Control) this.motherFax;
    componentResourceManager.ApplyResources((object) this.layoutMotherFax, "layoutMotherFax");
    this.layoutMotherFax.Location = new Point(557, 120);
    this.layoutMotherFax.Name = "layoutMotherFax";
    this.layoutMotherFax.Size = new Size(273, 24);
    this.layoutMotherFax.TextSize = new Size(113, 13);
    componentResourceManager.ApplyResources((object) this.layoutCaregiverGroup, "layoutCaregiverGroup");
    this.layoutCaregiverGroup.ExpandButtonLocation = GroupElementLocation.AfterText;
    this.layoutCaregiverGroup.ExpandButtonVisible = true;
    this.layoutCaregiverGroup.Items.AddRange(new BaseLayoutItem[15]
    {
      (BaseLayoutItem) this.emptySpaceItem13,
      (BaseLayoutItem) this.layoutCaregiverTitle,
      (BaseLayoutItem) this.layoutCaregiverZip,
      (BaseLayoutItem) this.layoutCaregiverCity,
      (BaseLayoutItem) this.emptySpaceItem17,
      (BaseLayoutItem) this.layoutCaregiverCountry,
      (BaseLayoutItem) this.layoutCaregiverSocialSecurityNumber,
      (BaseLayoutItem) this.layoutCaregiverAddress1,
      (BaseLayoutItem) this.layoutCaregiverForename1,
      (BaseLayoutItem) this.layoutCaregiverSurname,
      (BaseLayoutItem) this.layoutCaregiverLanguageCode,
      (BaseLayoutItem) this.emptySpaceItem10,
      (BaseLayoutItem) this.layoutCaregiverPhone,
      (BaseLayoutItem) this.layoutCaregiverCellPhone,
      (BaseLayoutItem) this.layoutCaregiverFax
    });
    this.layoutCaregiverGroup.Location = new Point(0, 0);
    this.layoutCaregiverGroup.Name = "layoutCaregiverGroup";
    this.layoutCaregiverGroup.Size = new Size(830, 144 /*0x90*/);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem13, "emptySpaceItem13");
    this.emptySpaceItem13.Location = new Point(404, 72);
    this.emptySpaceItem13.Name = "emptySpaceItem13";
    this.emptySpaceItem13.Size = new Size(154, 48 /*0x30*/);
    this.emptySpaceItem13.TextSize = new Size(0, 0);
    this.layoutCaregiverTitle.Control = (Control) this.caregiverTitle;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverTitle, "layoutCaregiverTitle");
    this.layoutCaregiverTitle.Location = new Point(0, 0);
    this.layoutCaregiverTitle.Name = "layoutCaregiverTitle";
    this.layoutCaregiverTitle.Size = new Size(221, 24);
    this.layoutCaregiverTitle.TextSize = new Size(113, 13);
    this.layoutCaregiverZip.Control = (Control) this.caregiverZip;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverZip, "layoutCaregiverZip");
    this.layoutCaregiverZip.Location = new Point(0, 96 /*0x60*/);
    this.layoutCaregiverZip.Name = "layoutCaregiverZip";
    this.layoutCaregiverZip.Size = new Size(171, 24);
    this.layoutCaregiverZip.TextSize = new Size(113, 13);
    this.layoutCaregiverCity.Control = (Control) this.caregiverCity;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverCity, "layoutCaregiverCity");
    this.layoutCaregiverCity.Location = new Point(171, 96 /*0x60*/);
    this.layoutCaregiverCity.Name = "layoutCaregiverCity";
    this.layoutCaregiverCity.Size = new Size(233, 24);
    this.layoutCaregiverCity.TextSize = new Size(113, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem17, "emptySpaceItem17");
    this.emptySpaceItem17.Location = new Point(404, 120);
    this.emptySpaceItem17.Name = "emptySpaceItem17";
    this.emptySpaceItem17.Size = new Size(154, 24);
    this.emptySpaceItem17.TextSize = new Size(0, 0);
    this.layoutCaregiverCountry.Control = (Control) this.caregiverCountry;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverCountry, "layoutCaregiverCountry");
    this.layoutCaregiverCountry.Location = new Point(0, 120);
    this.layoutCaregiverCountry.Name = "layoutCaregiverCountry";
    this.layoutCaregiverCountry.Size = new Size(404, 24);
    this.layoutCaregiverCountry.TextSize = new Size(113, 13);
    this.layoutCaregiverSocialSecurityNumber.Control = (Control) this.caregiverSocialSecurityNumber;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverSocialSecurityNumber, "layoutCaregiverSocialSecurityNumber");
    this.layoutCaregiverSocialSecurityNumber.Location = new Point(221, 0);
    this.layoutCaregiverSocialSecurityNumber.Name = "layoutCaregiverSocialSecurityNumber";
    this.layoutCaregiverSocialSecurityNumber.Size = new Size(337, 24);
    this.layoutCaregiverSocialSecurityNumber.TextSize = new Size(113, 13);
    this.layoutCaregiverAddress1.Control = (Control) this.caregiverAddress1;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverAddress1, "layoutCaregiverAddress1");
    this.layoutCaregiverAddress1.Location = new Point(0, 72);
    this.layoutCaregiverAddress1.Name = "layoutCaregiverAddress1";
    this.layoutCaregiverAddress1.Size = new Size(404, 24);
    this.layoutCaregiverAddress1.TextSize = new Size(113, 13);
    this.layoutCaregiverForename1.Control = (Control) this.caregiverForename1;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverForename1, "layoutCaregiverForename1");
    this.layoutCaregiverForename1.Location = new Point(0, 24);
    this.layoutCaregiverForename1.Name = "layoutCaregiverForename1";
    this.layoutCaregiverForename1.Size = new Size(221, 24);
    this.layoutCaregiverForename1.TextSize = new Size(113, 13);
    this.layoutCaregiverSurname.Control = (Control) this.caregiverSurname;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverSurname, "layoutCaregiverSurname");
    this.layoutCaregiverSurname.Location = new Point(221, 24);
    this.layoutCaregiverSurname.Name = "layoutCaregiverSurname";
    this.layoutCaregiverSurname.Size = new Size(337, 48 /*0x30*/);
    this.layoutCaregiverSurname.TextSize = new Size(113, 13);
    this.layoutCaregiverLanguageCode.Control = (Control) this.caregiverLanguageCode;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverLanguageCode, "layoutCaregiverLanguageCode");
    this.layoutCaregiverLanguageCode.Location = new Point(0, 48 /*0x30*/);
    this.layoutCaregiverLanguageCode.Name = "layoutCaregiverLanguageCode";
    this.layoutCaregiverLanguageCode.Size = new Size(221, 24);
    this.layoutCaregiverLanguageCode.TextSize = new Size(113, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem10, "emptySpaceItem10");
    this.emptySpaceItem10.Location = new Point(558, 0);
    this.emptySpaceItem10.Name = "emptySpaceItem10";
    this.emptySpaceItem10.Size = new Size(272, 72);
    this.emptySpaceItem10.TextSize = new Size(0, 0);
    this.layoutCaregiverPhone.Control = (Control) this.caregiverPhone;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverPhone, "layoutCaregiverPhone");
    this.layoutCaregiverPhone.Location = new Point(558, 72);
    this.layoutCaregiverPhone.Name = "layoutCaregiverPhone";
    this.layoutCaregiverPhone.Size = new Size(272, 24);
    this.layoutCaregiverPhone.TextSize = new Size(113, 13);
    this.layoutCaregiverCellPhone.Control = (Control) this.caregiverCellPhone;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverCellPhone, "layoutCaregiverCellPhone");
    this.layoutCaregiverCellPhone.Location = new Point(558, 96 /*0x60*/);
    this.layoutCaregiverCellPhone.Name = "layoutCaregiverCellPhone";
    this.layoutCaregiverCellPhone.Size = new Size(272, 24);
    this.layoutCaregiverCellPhone.TextSize = new Size(113, 13);
    this.layoutCaregiverFax.Control = (Control) this.caregiverFax;
    componentResourceManager.ApplyResources((object) this.layoutCaregiverFax, "layoutCaregiverFax");
    this.layoutCaregiverFax.Location = new Point(558, 120);
    this.layoutCaregiverFax.Name = "layoutCaregiverFax";
    this.layoutCaregiverFax.Size = new Size(272, 24);
    this.layoutCaregiverFax.TextSize = new Size(113, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem6, "emptySpaceItem6");
    this.emptySpaceItem6.Location = new Point(376, 41);
    this.emptySpaceItem6.Name = "emptySpaceItem2";
    this.emptySpaceItem6.Size = new Size(490, 31 /*0x1F*/);
    this.emptySpaceItem6.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem7, "emptySpaceItem7");
    this.emptySpaceItem7.Location = new Point(415, 72);
    this.emptySpaceItem7.Name = "emptySpaceItem7";
    this.emptySpaceItem7.Size = new Size(415, 72);
    this.emptySpaceItem7.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutPatientDataEditor);
    this.Name = nameof (PatientDataEditor);
    this.layoutPatientDataEditor.EndInit();
    this.layoutPatientDataEditor.ResumeLayout(false);
    this.caregiverFax.Properties.EndInit();
    this.caregiverCellPhone.Properties.EndInit();
    this.caregiverPhone.Properties.EndInit();
    this.motherFax.Properties.EndInit();
    this.motherCellPhone.Properties.EndInit();
    this.motherPhone.Properties.EndInit();
    this.patientWeight.Properties.EndInit();
    this.hospitalId.Properties.EndInit();
    this.patientFreeText3.Properties.EndInit();
    this.patientNicuState.Properties.EndInit();
    this.patientHeight.Properties.EndInit();
    this.patientConsentState.Properties.EndInit();
    this.motherAddress1.Properties.EndInit();
    this.motherCity.Properties.EndInit();
    this.motherZip.Properties.EndInit();
    this.motherLanguageCode.Properties.EndInit();
    this.motherAlsoKnownAs.Properties.EndInit();
    this.motherCountry.Properties.EndInit();
    this.motherSurname.Properties.EndInit();
    this.motherTitle.Properties.EndInit();
    this.caregiverAddress1.Properties.EndInit();
    this.patientFreeText2.Properties.EndInit();
    this.motherForename1.Properties.EndInit();
    this.motherSocialSecurityNumber.Properties.EndInit();
    this.patientFreeText1.Properties.EndInit();
    this.patientMedication.Properties.EndInit();
    this.patientDateOfBirth.Properties.VistaTimeProperties.EndInit();
    this.patientDateOfBirth.Properties.EndInit();
    this.caregiverMiddleInitial.Properties.EndInit();
    this.caregiverForename2.Properties.EndInit();
    this.caregiverCountry.Properties.EndInit();
    this.caregiverCity.Properties.EndInit();
    this.caregiverZip.Properties.EndInit();
    this.patientLanguageCode.Properties.EndInit();
    this.caregiverLanguageCode.Properties.EndInit();
    this.caregiverSocialSecurityNumber.Properties.EndInit();
    this.caregiverSurname.Properties.EndInit();
    this.caregiverForename1.Properties.EndInit();
    this.caregiverTitle.Properties.EndInit();
    this.patientNationality.Properties.EndInit();
    this.caregiverAlsoKnownAs.Properties.EndInit();
    this.patientBirthLocation.Properties.EndInit();
    this.patientSocialSecurityNumber.Properties.EndInit();
    this.patientAlsoKnownAs.Properties.EndInit();
    this.patientMiddleInitial.Properties.EndInit();
    this.patientForename2.Properties.EndInit();
    this.patientForename1.Properties.EndInit();
    this.patientTitle.Properties.EndInit();
    this.patientSurname.Properties.EndInit();
    this.patientGender.Properties.EndInit();
    this.patientRecordNumber.Properties.EndInit();
    this.layoutPatientForename2.EndInit();
    this.layoutPatientMiddleInitial.EndInit();
    this.layoutCaregiverForename2.EndInit();
    this.layoutCaregiverMiddleInitial.EndInit();
    this.layoutPatientSocialSecurityNumber.EndInit();
    this.layoutPatientLanguageCode.EndInit();
    this.layoutPatientTitle.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutPatientAlsoKnownAs.EndInit();
    this.layoutMotherAlsoKnownAs.EndInit();
    this.layoutCaregiverAlsoKnownAs.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutPatientData.EndInit();
    this.emptySpaceItem5.EndInit();
    this.emptySpaceItem3.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutPatientGender.EndInit();
    this.layoutPatientForename1.EndInit();
    this.layoutPatientSurname.EndInit();
    this.layoutPatientBirthLocation.EndInit();
    this.layoutPatientNationality.EndInit();
    this.layoutPatientDateOfBirth.EndInit();
    this.layoutPatientRecordNumber.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutPatientConsentState.EndInit();
    this.layoutPatientNicuState.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutPatientWeight.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutPatientMedicalInformation.EndInit();
    this.emptySpaceItem15.EndInit();
    this.emptySpaceItem16.EndInit();
    this.layoutPatientMedication.EndInit();
    this.layoutPatientFreeText1.EndInit();
    this.layoutPatientFreeText2.EndInit();
    this.tabbedControlGroup1.EndInit();
    this.layoutMotherGroup.EndInit();
    this.layoutMotherTitle.EndInit();
    this.layoutMotherForename1.EndInit();
    this.layoutMotherSurname.EndInit();
    this.layoutMotherSocialSecurityNumber.EndInit();
    this.layoutMotherLanguageCode.EndInit();
    this.emptySpaceItem8.EndInit();
    this.layoutMotherZip.EndInit();
    this.layoutMotherCity.EndInit();
    this.emptySpaceItem12.EndInit();
    this.layoutMotherStreet.EndInit();
    this.layoutMotherCountry.EndInit();
    this.emptySpaceItem18.EndInit();
    this.layoutMotherPhone.EndInit();
    this.layoutMotherCellPhone.EndInit();
    this.layoutMotherFax.EndInit();
    this.layoutCaregiverGroup.EndInit();
    this.emptySpaceItem13.EndInit();
    this.layoutCaregiverTitle.EndInit();
    this.layoutCaregiverZip.EndInit();
    this.layoutCaregiverCity.EndInit();
    this.emptySpaceItem17.EndInit();
    this.layoutCaregiverCountry.EndInit();
    this.layoutCaregiverSocialSecurityNumber.EndInit();
    this.layoutCaregiverAddress1.EndInit();
    this.layoutCaregiverForename1.EndInit();
    this.layoutCaregiverSurname.EndInit();
    this.layoutCaregiverLanguageCode.EndInit();
    this.emptySpaceItem10.EndInit();
    this.layoutCaregiverPhone.EndInit();
    this.layoutCaregiverCellPhone.EndInit();
    this.layoutCaregiverFax.EndInit();
    this.emptySpaceItem6.EndInit();
    this.emptySpaceItem7.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdatePatientDataCallBack(Patient patient);

  private delegate void UpdateCommonPatientDataCallBack(ICollection<Patient> patients);
}
