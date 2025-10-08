// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.PatientManager
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.AudiologyTest;
using PathMedical.AudiologyTest.DataAccessLayer;
using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Login;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.PatientManagement.DataAccessLayer;
using PathMedical.PatientManagement.Properties;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

#nullable disable
namespace PathMedical.PatientManagement;

public class PatientManager : 
  IMultiSelectionModel<Patient>,
  IModel,
  ISingleSelectionModel<AudiologyTestInformation>,
  ISingleSelectionModel<RiskIndicator>,
  ISingleEditingModel
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (PatientManager), "$Rev: 1647 $");
  private readonly ModelHelper<Patient, PatientAdapter> patientDataModelHelper;
  private readonly ModelHelper<AudiologyTestInformation, TestInformationAdapter> audiologyTestDataHelper;
  private readonly ModelHelper<RiskIndicator, RiskIndicatorAdapter> riskIndicatorModelHelper;
  private CreationTimeStampFilterType creationTimeStampFilterType;
  private Dictionary<RiskIndicator, RiskIndicatorValueType> rememberedRiskIndicatorsAndValues;

  public static PatientManager Instance => PathMedical.Singleton.Singleton<PatientManager>.Instance;

  public Guid ViewPermissionId => new Guid("880BE1E3-093E-4662-822C-84C5222DCBB0");

  public Guid AddPermissionId => new Guid("F4BE3C68-6202-4753-8C5E-B4178BF89B3A");

  public Guid EditPermssionId => new Guid("F4BE3C68-6202-4753-8C5E-B4178BF89B3A");

  public Guid DeletionPermissionId => new Guid("0454AAB5-F713-4432-82E7-2CB15A06B95B");

  public Guid ConfigurationPermissionId => new Guid("83EC6F27-0A70-41ec-BD0E-5B0289F73D34");

  public Guid ReassignTestPermissionId => new Guid("BC6434E6-D4B4-425f-89C3-CA7C14CF477E");

  private PatientManager()
  {
    this.patientDataModelHelper = new ModelHelper<Patient, PatientAdapter>(new EventHandler<ModelChangedEventArgs>(this.OnPatientModelHelperChanged), new string[8]
    {
      "PatientContact",
      "PatientContact.PrimaryAddress",
      "CaregiverContact.PrimaryAddress",
      "MotherContact.PrimaryAddress",
      "OverallTestInformation",
      "AudiologyTests",
      "RiskIndicators.NameTranslationList",
      "RiskIndicators.DescriptionTranslationList"
    })
    {
      SelectionModel = SelectionModelType.MultiSelectionModel
    };
    this.audiologyTestDataHelper = new ModelHelper<AudiologyTestInformation, TestInformationAdapter>(new EventHandler<ModelChangedEventArgs>(this.OnAudiologyTestInformationChanged), Array.Empty<string>());
    this.riskIndicatorModelHelper = new ModelHelper<RiskIndicator, RiskIndicatorAdapter>(new EventHandler<ModelChangedEventArgs>(this.OnRiskIndicatorChanged), Array.Empty<string>());
    RiskIndicatorManager.Instance.Changed += new EventHandler<ModelChangedEventArgs>(this.RiskIndicatorsChanged);
    this.CreationTimeStampFilterType = PatientManagementConfiguration.Instance.DateOfBirthRange;
    this.LoadDataExchangeDescriptions();
  }

  public List<Patient> Patients { get; private set; }

  public List<Patient> AllPatients
  {
    get
    {
      using (DBScope scope = new DBScope())
      {
        AdapterBase<Patient> adapterBase = new AdapterBase<Patient>(scope);
        adapterBase.LoadWithRelation(this.patientDataModelHelper.AdapterRelationNames);
        return adapterBase.All.ToList<Patient>();
      }
    }
  }

  public List<Patient> SelectedPatients
  {
    get
    {
      return this.patientDataModelHelper != null && this.patientDataModelHelper.SelectedItems != null ? this.patientDataModelHelper.SelectedItems.ToList<Patient>() : (List<Patient>) null;
    }
  }

  public Patient SelectedPatient
  {
    get
    {
      return this.patientDataModelHelper != null ? this.patientDataModelHelper.SelectedItem : (Patient) null;
    }
  }

  public AudiologyTestInformation SelectedAudiologyTest
  {
    get
    {
      return this.audiologyTestDataHelper != null ? this.audiologyTestDataHelper.SelectedItem : (AudiologyTestInformation) null;
    }
  }

  public AudiologyTestInformation TestToCopy { get; set; }

  public event EventHandler<ModelChangedEventArgs> Changed
  {
    add
    {
      value((object) this, ModelChangedEventArgs.Create<List<Patient>>(this.patientDataModelHelper.Items, ChangeType.ListLoaded));
      value((object) this, ModelChangedEventArgs.Create<Patient>(this.patientDataModelHelper.SelectedItem, ChangeType.SelectionChanged));
      value((object) this, ModelChangedEventArgs.Create<ICollection<Patient>>(this.patientDataModelHelper.SelectedItems, ChangeType.SelectionChanged));
      if (this.patientDataModelHelper.SelectedItem != null)
      {
        value((object) this, ModelChangedEventArgs.Create<List<RiskIndicator>>(this.patientDataModelHelper.SelectedItem.RiskIndicators, ChangeType.ListLoaded));
        value((object) this, ModelChangedEventArgs.Create<IEnumerable<Comment>>(this.patientDataModelHelper.SelectedItem.Comments, ChangeType.ListLoaded));
      }
      value((object) this, ModelChangedEventArgs.Create<AudiologyTestInformation>(this.audiologyTestDataHelper.SelectedItem, ChangeType.SelectionChanged));
      value((object) this, ModelChangedEventArgs.Create<List<RiskIndicator>>(this.riskIndicatorModelHelper.Items, ChangeType.ListLoaded));
      value((object) this, ModelChangedEventArgs.Create<RiskIndicator>(this.riskIndicatorModelHelper.SelectedItem, ChangeType.SelectionChanged));
      this.ModelChanged += value;
    }
    remove => this.ModelChanged -= value;
  }

  private event EventHandler<ModelChangedEventArgs> ModelChanged;

  public CreationTimeStampFilterType CreationTimeStampFilterType
  {
    get => this.creationTimeStampFilterType;
    set
    {
      if (this.creationTimeStampFilterType == value)
        return;
      this.creationTimeStampFilterType = value;
      if (this.ModelChanged == null)
        return;
      this.ModelChanged((object) this, ModelChangedEventArgs.Create<CreationTimeStampFilterType>(this.creationTimeStampFilterType, ChangeType.ItemEdited));
      this.patientDataModelHelper.SelectedItem = (Patient) null;
      this.patientDataModelHelper.SelectedItems = (ICollection<Patient>) null;
      this.patientDataModelHelper.LoadItems(new Func<PatientAdapter, ICollection<Patient>>(this.DoLoadPatients));
    }
  }

  public AudiologyTestInformation GetBestAudiologyTestResult(
    Patient patient,
    TestType testType,
    TestObject testObject)
  {
    AudiologyTestInformation audiologyTestResult = (AudiologyTestInformation) null;
    if (patient != null)
    {
      if (patient.OverallTestInformation != null)
      {
        try
        {
          OverallTestInformation bestTest = patient.OverallTestInformation.FirstOrDefault<OverallTestInformation>((Func<OverallTestInformation, bool>) (ot => ot.PatientId == patient.Id && ot.TestType == testType && ot.TestObject == testObject));
          if (bestTest != null)
            audiologyTestResult = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (at =>
            {
              Guid testDetailId = at.TestDetailId;
              Guid? referenceToTestId = bestTest.ReferenceToTestId;
              return referenceToTestId.HasValue && testDetailId == referenceToTestId.GetValueOrDefault();
            }));
        }
        catch (ArgumentNullException ex)
        {
          throw ExceptionFactory.Instance.CreateException<PatientManagerException>(Resources.PatientManager_FailureGetBestTest, (System.Exception) ex);
        }
      }
    }
    return audiologyTestResult;
  }

  private static void BuildPatientRiskIndicators(Patient patient)
  {
    if (RiskIndicatorManager.Instance.RiskIndicatorList == null || patient == null)
      return;
    if (patient.RiskIndicators == null)
      patient.RiskIndicators = new List<RiskIndicator>();
    List<RiskIndicator> riskIndicatorList = new List<RiskIndicator>();
    foreach (RiskIndicator riskIndicator in patient.RiskIndicators)
    {
      bool? isActive = riskIndicator.IsActive;
      bool flag = false;
      if (isActive.GetValueOrDefault() == flag & isActive.HasValue && riskIndicator.PatientRiskIndicatorValue == RiskIndicatorValueType.Unknown)
        riskIndicatorList.Add(riskIndicator);
    }
    foreach (RiskIndicator riskIndicator in riskIndicatorList)
      patient.RiskIndicators.Remove(riskIndicator);
    foreach (RiskIndicator riskIndicator in RiskIndicatorManager.Instance.RiskIndicatorList.Where<RiskIndicator>((Func<RiskIndicator, bool>) (ri =>
    {
      if (!ri.IsActive.GetValueOrDefault())
        return false;
      return patient.RiskIndicators == null || !patient.RiskIndicators.Contains(ri);
    })).Select<RiskIndicator, RiskIndicator>((Func<RiskIndicator, RiskIndicator>) (ri => ri.Clone() as RiskIndicator)).OrderBy<RiskIndicator, int?>((Func<RiskIndicator, int?>) (ri => ri.OrderNumber)).ToList<RiskIndicator>())
    {
      riskIndicator.PatientRiskIndicatorValue = RiskIndicatorValueType.Unknown;
      patient.RiskIndicators.Add(riskIndicator);
    }
    patient.RiskIndicators = patient.RiskIndicators.OrderBy<RiskIndicator, int?>((Func<RiskIndicator, int?>) (r => r.OrderNumber)).ToList<RiskIndicator>();
  }

  public ICollection<RiskIndicator> SelectedRiskIndicators
  {
    get => this.riskIndicatorModelHelper.SelectedItems;
    set => this.riskIndicatorModelHelper.SelectedItems = value;
  }

  public void ReportRiskIndicatorModification()
  {
    if (this.ModelChanged == null)
      return;
    this.ModelChanged((object) this, ModelChangedEventArgs.Create<List<RiskIndicator>>(this.patientDataModelHelper.SelectedItem.RiskIndicators, ChangeType.ListLoaded));
    this.ModelChanged((object) this, ModelChangedEventArgs.Create<RiskIndicator>(this.riskIndicatorModelHelper.SelectedItem, ChangeType.SelectionChanged));
  }

  private void RiskIndicatorsChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.patientDataModelHelper.SelectedItem == null || e.ChangeType == ChangeType.SelectionChanged)
      return;
    PatientManager.BuildPatientRiskIndicators(this.patientDataModelHelper.SelectedItem);
    this.ModelChanged((object) this, ModelChangedEventArgs.Create<List<RiskIndicator>>(this.patientDataModelHelper.SelectedItem.RiskIndicators, ChangeType.ListLoaded));
  }

  public void Filter(string search)
  {
    this.patientDataModelHelper.ResetFilter();
    if (string.IsNullOrEmpty(search))
      return;
    string[] searchTerms = search.Split(' ');
    this.patientDataModelHelper.Filter(this.patientDataModelHelper.Items.Where<Patient>((Func<Patient, bool>) (p =>
    {
      if (p.PatientContact == null)
        return false;
      return searchTerms.Match(p.PatientRecordNumber, p.PatientContact.Forename1, p.PatientContact.Forename2, p.PatientContact.Surname, p.PatientContact.DateOfBirth.ToString());
    })).ToList<Patient>());
  }

  public void ResetFilter()
  {
    if (this.patientDataModelHelper == null)
      return;
    this.patientDataModelHelper.ResetFilter();
  }

  private ICollection<Patient> DoLoadPatients(PatientAdapter adapter)
  {
    DateTime creationTimeStampFilter = new DateTime(1900, 1, 1);
    switch (this.creationTimeStampFilterType)
    {
      case CreationTimeStampFilterType.ThisWeek:
        creationTimeStampFilter = DateTime.Today.Subtract(TimeSpan.FromDays(7.0));
        break;
      case CreationTimeStampFilterType.Last4Weeks:
        creationTimeStampFilter = DateTime.Today.Subtract(TimeSpan.FromDays(30.0));
        break;
      case CreationTimeStampFilterType.Last3Months:
        creationTimeStampFilter = DateTime.Today.Subtract(TimeSpan.FromDays(90.0));
        break;
    }
    this.Patients = adapter.FetchEntities((Expression<Func<Patient, bool>>) (p => p.Created >= creationTimeStampFilter)).ToList<Patient>();
    foreach (Patient patient in this.Patients)
      PatientManager.BuildPatientRiskIndicators(patient);
    return (ICollection<Patient>) this.Patients;
  }

  public void Store()
  {
    if (this.patientDataModelHelper.SelectedItem == null)
      return;
    Patient patient = this.patientDataModelHelper.SelectedItem;
    PatientManager.BuildPatientRiskIndicators(patient);
    using (DBScope scope = new DBScope())
    {
      this.patientDataModelHelper.Store();
      AdapterBase<PredefinedPatientCommentAssociation> adapterBase1 = new AdapterBase<PredefinedPatientCommentAssociation>(scope);
      adapterBase1.LoadWithRelation("PredefinedComment");
      List<PredefinedPatientCommentAssociation> list1 = adapterBase1.FetchEntities((Expression<Func<PredefinedPatientCommentAssociation, bool>>) (pdca => pdca.ReferenceId == patient.Id)).ToList<PredefinedPatientCommentAssociation>();
      List<PredefinedComment> predefinedCommentList = new List<PredefinedComment>();
      if (patient.PredefinedComments != null)
      {
        foreach (PredefinedComment predefinedComment in patient.PredefinedComments)
        {
          PredefinedComment comment = predefinedComment;
          if (list1.Where<PredefinedPatientCommentAssociation>((Func<PredefinedPatientCommentAssociation, bool>) (pdc => pdc.AssociationId.Equals((object) comment.AssociationId))).Count<PredefinedPatientCommentAssociation>() == 0)
            predefinedCommentList.Add(comment);
        }
        foreach (PredefinedComment predefinedComment in predefinedCommentList)
        {
          predefinedComment.AssociationId = new Guid?(Guid.NewGuid());
          PredefinedPatientCommentAssociation commentAssociation = new PredefinedPatientCommentAssociation();
          commentAssociation.AssociationId = predefinedComment.AssociationId.Value;
          commentAssociation.PredefinedCommentId = predefinedComment.PredefinedCommentId;
          commentAssociation.CreationDate = predefinedComment.CreationDate;
          commentAssociation.UserAccountId = predefinedComment.UserAccountId;
          commentAssociation.ReferenceId = patient.Id;
          PredefinedPatientCommentAssociation entity = commentAssociation;
          adapterBase1.Store(entity);
          list1.Add(entity);
        }
      }
      if (patient.PredefinedComments != null)
      {
        List<PredefinedPatientCommentAssociation> commentAssociationList = new List<PredefinedPatientCommentAssociation>();
        foreach (PredefinedPatientCommentAssociation commentAssociation1 in list1)
        {
          PredefinedPatientCommentAssociation commentAssociation = commentAssociation1;
          if (!patient.PredefinedComments.Any<PredefinedComment>((Func<PredefinedComment, bool>) (pdc => pdc.AssociationId.Equals((object) commentAssociation.AssociationId))))
            commentAssociationList.Add(commentAssociation1);
        }
        foreach (PredefinedPatientCommentAssociation entity in commentAssociationList)
          adapterBase1.Delete(entity);
      }
      AdapterBase<FreeTextComment> adapterBase2 = new AdapterBase<FreeTextComment>(scope);
      List<FreeTextComment> list2 = adapterBase2.FetchEntities((Expression<Func<FreeTextComment, bool>>) (ft => ft.ReferenceId == patient.Id)).ToList<FreeTextComment>();
      List<FreeTextComment> freeTextCommentList1 = new List<FreeTextComment>();
      if (patient.FreeTextComments != null)
      {
        foreach (FreeTextComment freeTextComment in patient.FreeTextComments)
        {
          FreeTextComment comment = freeTextComment;
          if (!list2.Any<FreeTextComment>((Func<FreeTextComment, bool>) (ft => ft.Id == comment.Id)))
            freeTextCommentList1.Add(comment);
        }
        foreach (FreeTextComment entity in freeTextCommentList1)
        {
          entity.Id = Guid.NewGuid();
          adapterBase2.Store(entity);
          list2.Add(entity);
        }
      }
      List<FreeTextComment> freeTextCommentList2 = new List<FreeTextComment>();
      if (patient.FreeTextComments != null)
      {
        foreach (FreeTextComment freeTextComment in list2)
        {
          FreeTextComment comment = freeTextComment;
          if (!patient.FreeTextComments.Any<FreeTextComment>((Func<FreeTextComment, bool>) (ft => ft.Id == comment.Id)))
            freeTextCommentList2.Add(comment);
        }
        foreach (FreeTextComment entity in freeTextCommentList2)
          adapterBase2.Delete(entity);
      }
      scope.Complete();
    }
    this.ReportRiskIndicatorModification();
  }

  public void Delete()
  {
    if (this.patientDataModelHelper.SelectedItem == null)
      return;
    this.DeletePatient(this.patientDataModelHelper.SelectedItem);
  }

  public void CancelNewItem() => this.patientDataModelHelper.CancelAddItem();

  public void PrepareAddItem()
  {
    Patient patient = new Patient();
    PatientManager.BuildPatientRiskIndicators(patient);
    this.patientDataModelHelper.Items.Add(patient);
    this.patientDataModelHelper.PrepareAddItem(patient);
  }

  public void RefreshData()
  {
    RiskIndicatorManager.Instance.RefreshData();
    this.patientDataModelHelper.LoadItems(new Func<PatientAdapter, ICollection<Patient>>(this.DoLoadPatients));
  }

  public void RevertModifications()
  {
    if (this.patientDataModelHelper.SelectedItem != null && this.patientDataModelHelper.SelectedItem.Id == Guid.Empty)
    {
      this.patientDataModelHelper.SelectedItem.RiskIndicators = (List<RiskIndicator>) null;
      PatientManager.BuildPatientRiskIndicators(this.patientDataModelHelper.SelectedItem);
      this.ReportRiskIndicatorModification();
      if (this.ModelChanged == null)
        return;
      this.ModelChanged((object) this, ModelChangedEventArgs.Create<Patient>(this.patientDataModelHelper.SelectedItem, ChangeType.SelectionChanged));
    }
    else
    {
      if (this.patientDataModelHelper.SelectedItem == null)
        return;
      this.patientDataModelHelper.DoInAdapter((Action<PatientAdapter, Patient>) ((adapter, item) => adapter.RefreshEntity(item)), this.patientDataModelHelper.SelectedItem);
      PatientManager.BuildPatientRiskIndicators(this.patientDataModelHelper.SelectedItem);
      this.ReportRiskIndicatorModification();
      if (this.ModelChanged == null)
        return;
      this.ModelChanged((object) this, ModelChangedEventArgs.Create<Patient>(this.patientDataModelHelper.SelectedItem, ChangeType.SelectionChanged));
    }
  }

  public Dictionary<RiskIndicator, RiskIndicatorValueType> SetUnkownRiskIndicatorsToNo()
  {
    Dictionary<RiskIndicator, RiskIndicatorValueType> dictionary = this.patientDataModelHelper.SelectedItem.RiskIndicators.Where<RiskIndicator>((Func<RiskIndicator, bool>) (ri => ri.IsActive.GetValueOrDefault() && ri.PatientRiskIndicatorValue == RiskIndicatorValueType.Unknown)).ToDictionary<RiskIndicator, RiskIndicator, RiskIndicatorValueType>((Func<RiskIndicator, RiskIndicator>) (ri => ri), (Func<RiskIndicator, RiskIndicatorValueType>) (ri => ri.PatientRiskIndicatorValue));
    dictionary.Keys.ForEach<RiskIndicator>((Action<RiskIndicator>) (ri => ri.PatientRiskIndicatorValue = RiskIndicatorValueType.No));
    this.ReportRiskIndicatorModification();
    return dictionary;
  }

  public void AssignRiskIndicators(
    Dictionary<RiskIndicator, RiskIndicatorValueType> riskIndicatorAssignments)
  {
    if (riskIndicatorAssignments == null)
      return;
    riskIndicatorAssignments.Keys.ForEach<RiskIndicator>((Action<RiskIndicator>) (ri => ri.PatientRiskIndicatorValue = RiskIndicatorValueType.No));
    this.ReportRiskIndicatorModification();
  }

  private void OnPatientModelHelperChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.ModelChanged == null)
      return;
    this.ModelChanged((object) this, e);
  }

  private void OnAudiologyTestInformationChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.ModelChanged == null)
      return;
    this.ModelChanged((object) this, e);
  }

  private void OnRiskIndicatorChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.ModelChanged == null)
      return;
    this.ModelChanged((object) this, e);
  }

  public void ChangeSelectedItems(ICollection<Patient> selection)
  {
    this.patientDataModelHelper.SelectedItems = selection;
  }

  bool IMultiSelectionModel<Patient>.IsOneItemSelected<T>()
  {
    return this.patientDataModelHelper.SelectedItem != null;
  }

  public void ChangeFocusedItem(Patient item) => this.patientDataModelHelper.SelectedItem = item;

  bool IMultiSelectionModel<Patient>.IsOneItemFocused<T>()
  {
    return this.patientDataModelHelper.SelectedItem != null;
  }

  public void ChangeSingleSelection(AudiologyTestInformation selection)
  {
    this.audiologyTestDataHelper.SelectedItem = selection;
  }

  bool ISingleSelectionModel<AudiologyTestInformation>.IsOneItemSelected<T>()
  {
    return this.audiologyTestDataHelper.SelectedItem != null;
  }

  bool ISingleSelectionModel<AudiologyTestInformation>.IsOneItemAvailable<T>()
  {
    return this.audiologyTestDataHelper != null && this.audiologyTestDataHelper.Items != null && this.audiologyTestDataHelper.Items.Count > 0;
  }

  public void ChangeSingleSelection(RiskIndicator selection)
  {
    this.riskIndicatorModelHelper.SelectedItem = selection;
  }

  bool ISingleSelectionModel<RiskIndicator>.IsOneItemSelected<T>()
  {
    return this.riskIndicatorModelHelper.SelectedItem != null;
  }

  bool ISingleSelectionModel<RiskIndicator>.IsOneItemAvailable<T>()
  {
    return this.riskIndicatorModelHelper != null && this.riskIndicatorModelHelper.Items != null && this.riskIndicatorModelHelper.Items.Count > 0;
  }

  public List<DataExchangeSetMap> RecordSetMaps { get; protected set; }

  public List<RecordDescriptionSet> RecordDescriptionSets { get; protected set; }

  private void LoadDataExchangeDescriptions()
  {
    this.RecordDescriptionSets = new List<RecordDescriptionSet>();
    this.RecordSetMaps = new List<DataExchangeSetMap>();
    this.RecordDescriptionSets.Add(RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.PatientManagement.DataExchange.PluginDataDescription.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("Can't find the XML document that describes the structure for data exchange.")));
  }

  public static void LoadPatientFreeTextComments(Patient patient)
  {
    if (patient == null || patient.Id.Equals(Guid.Empty))
      return;
    using (DBScope scope = new DBScope())
    {
      List<FreeTextComment> list = new AdapterBase<FreeTextComment>(scope).FetchEntities((Expression<Func<FreeTextComment, bool>>) (pdca => pdca.ReferenceId == patient.Id)).ToList<FreeTextComment>();
      patient.FreeTextComments = list.ToList<FreeTextComment>();
      scope.Complete();
    }
  }

  public static void LoadPatientPredefinedComments(Patient patient)
  {
    if (patient == null || patient.Id.Equals(Guid.Empty))
      return;
    using (DBScope scope = new DBScope())
    {
      AdapterBase<PredefinedPatientCommentAssociation> adapterBase = new AdapterBase<PredefinedPatientCommentAssociation>(scope);
      adapterBase.LoadWithRelation("PredefinedComment.CommentTranslationList");
      List<PredefinedPatientCommentAssociation> list = adapterBase.FetchEntities((Expression<Func<PredefinedPatientCommentAssociation, bool>>) (pdca => pdca.ReferenceId == patient.Id)).ToList<PredefinedPatientCommentAssociation>();
      patient.PredefinedComments = list.Select<PredefinedPatientCommentAssociation, PredefinedComment>((Func<PredefinedPatientCommentAssociation, PredefinedComment>) (association => PatientManager.UpdatePredefinedCommentRelationValues(association))).ToList<PredefinedComment>();
      scope.Complete();
    }
  }

  private static PredefinedComment UpdatePredefinedCommentRelationValues(
    PredefinedPatientCommentAssociation association)
  {
    if (association == null)
      return (PredefinedComment) null;
    if (association.PredefinedComment == null)
      return association.PredefinedComment;
    association.PredefinedComment.AssociationId = new Guid?(association.AssociationId);
    association.PredefinedComment.ReferenceId = new Guid?(association.ReferenceId);
    association.PredefinedComment.CreationDate = association.CreationDate;
    association.PredefinedComment.UserAccountId = association.UserAccountId;
    return association.PredefinedComment;
  }

  public void AddComment(FreeTextComment freeTextComment)
  {
    if (freeTextComment == null || this.patientDataModelHelper.SelectedItem == null)
      return;
    Patient selectedItem = this.patientDataModelHelper.SelectedItem;
    if (selectedItem.FreeTextComments == null)
      selectedItem.FreeTextComments = new List<FreeTextComment>();
    freeTextComment.ReferenceId = selectedItem.Id;
    freeTextComment.CreationDate = new DateTime?(DateTime.Now);
    if (LoginManager.Instance.LoggedInUserData != null)
      freeTextComment.UserAccountId = new Guid?(LoginManager.Instance.LoggedInUserData.Id);
    selectedItem.FreeTextComments.Add(freeTextComment);
    this.ReportPatientCommentsModification();
  }

  public void DeleteComment(FreeTextComment freeTextComment)
  {
    if (freeTextComment == null || this.patientDataModelHelper.SelectedItem == null)
      return;
    Patient selectedItem = this.patientDataModelHelper.SelectedItem;
    if (selectedItem.FreeTextComments == null)
    {
      selectedItem.FreeTextComments = new List<FreeTextComment>();
    }
    else
    {
      if (!selectedItem.FreeTextComments.Contains(freeTextComment))
        return;
      selectedItem.FreeTextComments.Remove(freeTextComment);
      this.ReportPatientCommentsModification();
    }
  }

  public void AddPredefinedComment(PredefinedComment comment)
  {
    if (comment == null || this.patientDataModelHelper.SelectedItem == null)
      return;
    Patient selectedItem = this.patientDataModelHelper.SelectedItem;
    if (selectedItem.PredefinedComments == null)
      selectedItem.PredefinedComments = new List<PredefinedComment>();
    comment.AssociationId = new Guid?(Guid.Empty);
    comment.CreationDate = new DateTime?(DateTime.Now);
    if (LoginManager.Instance.LoggedInUserData != null)
      comment.UserAccountId = new Guid?(LoginManager.Instance.LoggedInUserData.Id);
    selectedItem.PredefinedComments.Add(comment);
    this.ReportPatientCommentsModification();
  }

  public void DeletePredefinedComment(PredefinedComment comment)
  {
    if (comment == null || this.patientDataModelHelper.SelectedItem == null)
      return;
    Patient selectedItem = this.patientDataModelHelper.SelectedItem;
    if (selectedItem.PredefinedComments == null)
    {
      selectedItem.PredefinedComments = new List<PredefinedComment>();
    }
    else
    {
      if (selectedItem.PredefinedComments.Contains(comment))
        selectedItem.PredefinedComments.Remove(comment);
      this.ReportPatientCommentsModification();
    }
  }

  public void ReportPatientCommentsModification()
  {
    if (this.ModelChanged == null)
      return;
    this.ModelChanged((object) this, ModelChangedEventArgs.Create<IEnumerable<Comment>>(this.patientDataModelHelper.SelectedItem.Comments, ChangeType.ListLoaded));
  }

  public void Import(
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets)
  {
    if (dataExchangeTokenSets == null || dataExchangeTokenSets.Count<DataExchangeTokenSet>() == 0)
      return;
    DateTime now = DateTime.Now;
    List<Patient> patientList = DataExchangeManager.Instance.FetchEntities<Patient>(dataExchangeTokenSets);
    List<PatientRiskIndicator> source1 = DataExchangeManager.Instance.FetchEntities<PatientRiskIndicator>(dataExchangeTokenSets);
    using (DBScope scope = new DBScope())
    {
      try
      {
        PatientAdapter adapter = new PatientAdapter(scope);
        adapter.LoadWithRelation("PatientContact.PrimaryAddress", "MotherContact.PrimaryAddress", "CaregiverContact.PrimaryAddress", "RiskIndicators.NameTranslationList", "RiskIndicators.DescriptionTranslationList");
        foreach (Patient patientToStore1 in patientList)
        {
          Patient patientToStore = PatientManager.Instance.ImportPatient(patientToStore1, (AdapterBase<Patient>) adapter);
          List<PatientRiskIndicator> list = source1.Where<PatientRiskIndicator>((Func<PatientRiskIndicator, bool>) (ri => ri.PatientId == patientToStore.Id && ri.PatientRiskIndicatorValue > (ushort) 0)).ToList<PatientRiskIndicator>();
          if (list.Count > 0)
          {
            if (patientToStore.RiskIndicators == null)
              patientToStore.RiskIndicators = new List<RiskIndicator>();
            foreach (PatientRiskIndicator patientRiskIndicator in list)
            {
              if (patientRiskIndicator != null)
              {
                PatientRiskIndicator store = patientRiskIndicator;
                RiskIndicator riskIndicator1 = patientToStore.RiskIndicators.FirstOrDefault<RiskIndicator>((Func<RiskIndicator, bool>) (pr => pr.Id == store.RiskIndicatorId));
                if (riskIndicator1 == null)
                {
                  PatientRiskIndicator toStore = patientRiskIndicator;
                  RiskIndicator riskIndicator2 = RiskIndicatorManager.Instance.RiskIndicatorList.Where<RiskIndicator>((Func<RiskIndicator, bool>) (ri => ri.Id == toStore.RiskIndicatorId && ri.IsActive.GetValueOrDefault())).Select<RiskIndicator, RiskIndicator>((Func<RiskIndicator, RiskIndicator>) (ri => ri.Clone() as RiskIndicator)).FirstOrDefault<RiskIndicator>();
                  if (riskIndicator2 != null)
                  {
                    riskIndicator2.PatientRiskIndicatorValue = (RiskIndicatorValueType) patientRiskIndicator.PatientRiskIndicatorValue;
                    patientToStore.RiskIndicators.Add(riskIndicator2);
                  }
                }
                else
                  riskIndicator1.PatientRiskIndicatorValue = (RiskIndicatorValueType) patientRiskIndicator.PatientRiskIndicatorValue;
              }
            }
          }
          adapter.Store(patientToStore);
        }
        scope.Complete();
      }
      catch (System.Exception ex)
      {
        PatientManager.Logger.Error(ex, "Failure while storing patients.");
        throw;
      }
    }
    List<FreeTextComment> freeTextCommentList = DataExchangeManager.Instance.FetchEntities<FreeTextComment>(dataExchangeTokenSets);
    if (freeTextCommentList != null)
    {
      if (freeTextCommentList.Count<FreeTextComment>() > 0)
      {
        try
        {
          using (DBScope scope = new DBScope())
          {
            FreeTextCommentAdapter textCommentAdapter = new FreeTextCommentAdapter(scope);
            ICollection<FreeTextComment> existingFreeTextComments = textCommentAdapter.All;
            freeTextCommentList = freeTextCommentList.Where<FreeTextComment>((Func<FreeTextComment, bool>) (p => !existingFreeTextComments.Any<FreeTextComment>((Func<FreeTextComment, bool>) (ep => ep.Id == p.Id)))).ToList<FreeTextComment>();
            textCommentAdapter.Store((ICollection<FreeTextComment>) freeTextCommentList);
            scope.Complete();
          }
        }
        catch (System.Exception ex)
        {
          PatientManager.Logger.Error(ex, "Failure while storing free text comments.");
          throw;
        }
      }
    }
    List<PredefinedCommentAssociation> source2 = DataExchangeManager.Instance.FetchEntities<PredefinedCommentAssociation>(dataExchangeTokenSets);
    List<PredefinedComment> commentList = CommentManager.Instance.CommentList;
    if (commentList != null && commentList.Count > 0 && source2 != null)
    {
      if (source2.Count<PredefinedCommentAssociation>() > 0)
      {
        try
        {
          using (DBScope dbScope = new DBScope())
          {
            foreach (PredefinedCommentAssociation commentAssociation in source2)
            {
              PredefinedCommentAssociation pca1 = commentAssociation;
              if (commentList.Where<PredefinedComment>((Func<PredefinedComment, bool>) (c => c.PredefinedCommentId.Equals(pca1.PredefinedCommentId))).FirstOrDefault<PredefinedComment>() != null)
              {
                num = 0;
                using (DbCommand dbCommand = dbScope.CreateDbCommand())
                {
                  StringBuilder stringBuilder = new StringBuilder();
                  stringBuilder.Append("SELECT COUNT(*) FROM PredefinedCommentAssociation");
                  stringBuilder.Append(" WHERE [AssociationID] = @AssociationId");
                  stringBuilder.Append(" AND [ReferenceId] = @ReferenceId");
                  stringBuilder.Append(" AND [PredefinedCommentId] = @PredefinedCommentId");
                  dbCommand.CommandText = stringBuilder.ToString();
                  dbScope.AddDbParameter(dbCommand, "AssociationId", (object) pca1.AssociationId);
                  dbScope.AddDbParameter(dbCommand, "ReferenceId", (object) pca1.ReferenceId);
                  dbScope.AddDbParameter(dbCommand, "PredefinedCommentId", (object) pca1.PredefinedCommentId);
                  if (!(dbCommand.ExecuteScalar() is int num))
                    ;
                }
                if (num == 0)
                {
                  using (DbCommand dbCommand = dbScope.CreateDbCommand())
                  {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("INSERT INTO PredefinedCommentAssociation");
                    stringBuilder.Append(" ( [AssociationID], [ReferenceId], [PredefinedCommentId], [CreationDate], [UserAccountID], [Created], [Modified])");
                    stringBuilder.Append(" VALUES (@AssociationId, @ReferenceId, @PredefinedCommentId, @Created,  @UserAccountId, @Created, @Modified)");
                    dbCommand.CommandText = stringBuilder.ToString();
                    dbScope.AddDbParameter(dbCommand, "AssociationId", (object) pca1.AssociationId);
                    dbScope.AddDbParameter(dbCommand, "ReferenceId", (object) pca1.ReferenceId);
                    dbScope.AddDbParameter(dbCommand, "PredefinedCommentId", (object) pca1.PredefinedCommentId);
                    dbScope.AddDbParameter(dbCommand, "UserAccountId", (object) pca1.UserAccountId);
                    dbScope.AddDbParameter(dbCommand, "Created", (object) pca1.CreationDate);
                    dbScope.AddDbParameter(dbCommand, "Modified", (object) DateTime.Now);
                    dbCommand.ExecuteNonQuery();
                  }
                }
              }
            }
            dbScope.Complete();
          }
        }
        catch (System.Exception ex)
        {
          PatientManager.Logger.Error(ex, "Failure while storing predefined comments.");
          throw;
        }
      }
    }
    TimeSpan timeSpan = DateTime.Now.Subtract(now);
    if (freeTextCommentList == null)
      return;
    PatientManager.Logger.Info("Patient data has been imported. [{0}] patients / [{1}] patient risks assignments / [{2}] free text comments / processing time [{3}] seconds.", (object) patientList.Count, (object) source1.Count, (object) freeTextCommentList.Count, (object) timeSpan.TotalSeconds);
  }

  private Patient ImportPatient(Patient patientToStore, AdapterBase<Patient> adapter)
  {
    if (!this.DoesPatientExist(patientToStore.Id))
      return patientToStore;
    Patient patient = adapter.FetchEntities((Expression<Func<Patient, bool>>) (ep => ep.Id == patientToStore.Id)).FirstOrDefault<Patient>();
    if (patient == null)
      return patientToStore;
    DateTime.Compare(patientToStore.ExternalCreationTimeStamp, patient.Modified);
    return patient;
  }

  public void Print(Type reportType, params PrintParameter[] printParameters)
  {
    if (reportType == (Type) null || SystemConfigurationManager.Instance.DocumentPrintManager == null)
      return;
    SystemConfigurationManager.Instance.DocumentPrintManager.DataSource = (object) this.patientDataModelHelper.SelectedItem;
    SystemConfigurationManager.Instance.DocumentPrintManager.PrintParameters = new List<PrintParameter>();
    SystemConfigurationManager.Instance.DocumentPrintManager.PrintParameters.AddRange((IEnumerable<PrintParameter>) printParameters);
    SystemConfigurationManager.Instance.DocumentPrintManager.ShowPreviewDialog(reportType);
  }

  public void DeletePatient(Patient patient)
  {
    try
    {
      using (DBScope scope = new DBScope())
      {
        List<Contact> list = ((IEnumerable<Contact>) new Contact[3]
        {
          patient.CaregiverContact,
          patient.MotherContact,
          patient.PatientContact
        }).Where<Contact>((Func<Contact, bool>) (c => c != null && c.Id != Guid.Empty)).ToList<Contact>();
        AudiologyTestManager.Instance.DeleteTests(patient.AudiologyTests);
        foreach (Contact contact in list)
          PatientManager.DeleteContact(scope, contact);
        PatientManager.DeleteRiskFactorAssociations(scope, patient);
        PatientManager.DeleteComments(scope, patient);
        this.patientDataModelHelper.Delete(patient);
        scope.Complete();
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("PatientManager_DeletePatient_CantDelete"), ex);
    }
  }

  private static void DeleteComments(DBScope scope, Patient patient)
  {
    if (scope == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (scope));
    if (patient == null)
      return;
    if (patient.PredefinedComments != null && patient.PredefinedComments.Count > 0)
    {
      using (DbCommand dbCommand = scope.CreateDbCommand())
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DELETE FROM PredefinedCommentAssociation");
        stringBuilder.AppendFormat(" WHERE ReferenceId = @PatientId");
        dbCommand.CommandText = stringBuilder.ToString();
        scope.AddDbParameter(dbCommand, "PatientId", (object) patient.Id);
        dbCommand.ExecuteNonQuery();
      }
    }
    if (patient.FreeTextComments == null || patient.FreeTextComments.Count <= 0)
      return;
    using (DbCommand dbCommand = scope.CreateDbCommand())
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DELETE FROM FreeTextComment");
      stringBuilder.AppendFormat(" WHERE ReferenceId = @PatientId");
      dbCommand.CommandText = stringBuilder.ToString();
      scope.AddDbParameter(dbCommand, "PatientId", (object) patient.Id);
      dbCommand.ExecuteNonQuery();
    }
  }

  private static void DeleteRiskFactorAssociations(DBScope scope, Patient patient)
  {
    if (scope == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (scope));
    if (patient == null || patient.RiskIndicators == null || patient.RiskIndicators.Count <= 0)
      return;
    foreach (RiskIndicator riskIndicator in patient.RiskIndicators)
    {
      if (riskIndicator.Id != Guid.Empty)
      {
        using (DbCommand dbCommand = scope.CreateDbCommand())
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("DELETE FROM PatientRiskIndicators");
          stringBuilder.AppendFormat(" WHERE PatientId = @PatientId");
          stringBuilder.AppendFormat(" AND RiskIndicatorId = @RiskIndicatorId");
          dbCommand.CommandText = stringBuilder.ToString();
          scope.AddDbParameter(dbCommand, "PatientId", (object) patient.Id);
          scope.AddDbParameter(dbCommand, "RiskIndicatorId", (object) riskIndicator.Id);
          dbCommand.ExecuteNonQuery();
        }
      }
    }
  }

  private static void DeleteContact(DBScope scope, Contact contact)
  {
    if (scope == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (scope));
    if (contact == null)
      return;
    if (contact.PrimaryAddress != null)
      PatientManager.DeleteContactAddress(scope, contact.Id, contact.PrimaryAddress.Id);
    if (contact.AdditionalAddresses != null)
    {
      foreach (Address additionalAddress in contact.AdditionalAddresses)
      {
        if (additionalAddress != null)
          PatientManager.DeleteContactAddress(scope, contact.Id, additionalAddress.Id);
      }
    }
    if (contact.PatientId != Guid.Empty)
    {
      using (DbCommand dbCommand = scope.CreateDbCommand())
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DELETE FROM PatientContactAssociation");
        stringBuilder.AppendFormat(" WHERE PatientId = @PatientId");
        stringBuilder.AppendFormat(" AND ContactId = @ContactId");
        dbCommand.CommandText = stringBuilder.ToString();
        scope.AddDbParameter(dbCommand, "PatientId", (object) contact.PatientId);
        scope.AddDbParameter(dbCommand, "ContactId", (object) contact.Id);
        dbCommand.ExecuteNonQuery();
      }
    }
    num = int.MaxValue;
    using (DbCommand dbCommand = scope.CreateDbCommand())
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SELECT COUNT(*) FROM PatientContactAssociation");
      stringBuilder.AppendFormat(" WHERE ContactId = @ContactId");
      dbCommand.CommandText = stringBuilder.ToString();
      scope.AddDbParameter(dbCommand, "ContactId", (object) contact.Id);
      if (!(dbCommand.ExecuteScalar() is int num))
        ;
    }
    if (num != 0)
      return;
    new AdapterBase<Contact>(scope).Delete(contact);
  }

  private static void DeleteContactAddress(DBScope scope, Guid contactId, Guid addressId)
  {
    if (scope == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (scope));
    if (contactId == Guid.Empty || addressId == Guid.Empty)
      return;
    using (DbCommand dbCommand = scope.CreateDbCommand())
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DELETE FROM ContactToContactAddressAssociation");
      stringBuilder.AppendFormat(" WHERE ContactId = @ContactId");
      stringBuilder.AppendFormat(" AND ContactAddressId = @ContactAddressId");
      dbCommand.CommandText = stringBuilder.ToString();
      scope.AddDbParameter(dbCommand, "ContactId", (object) contactId);
      scope.AddDbParameter(dbCommand, "ContactAddressId", (object) addressId);
      dbCommand.ExecuteNonQuery();
    }
    num = int.MaxValue;
    using (DbCommand dbCommand = scope.CreateDbCommand())
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SELECT COUNT(*) FROM ContactToContactAddressAssociation");
      stringBuilder.AppendFormat(" WHERE ContactAddressId = @ContactAddressId");
      dbCommand.CommandText = stringBuilder.ToString();
      scope.AddDbParameter(dbCommand, "ContactAddressId", (object) addressId);
      if (!(dbCommand.ExecuteScalar() is int num))
        ;
    }
    if (num != 0)
      return;
    using (DbCommand dbCommand = scope.CreateDbCommand())
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("DELETE FROM ContactAddress");
      stringBuilder.AppendFormat(" WHERE Id = @ContactId");
      dbCommand.CommandText = stringBuilder.ToString();
      scope.AddDbParameter(dbCommand, "ContactId", (object) contactId);
      dbCommand.ExecuteNonQuery();
    }
  }

  public bool DoesPatientExist(Guid patientId)
  {
    if (patientId == Guid.Empty)
      return false;
    bool flag = false;
    using (DBScope dbScope = new DBScope())
    {
      using (DbCommand dbCommand = dbScope.CreateDbCommand())
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SELECT COUNT(*) FROM Patient");
        stringBuilder.AppendFormat(" WHERE Id = @PatientId");
        dbCommand.CommandText = stringBuilder.ToString();
        dbScope.AddDbParameter(dbCommand, "PatientId", (object) patientId);
        object obj = dbCommand.ExecuteScalar();
        num = 0;
        if (!(obj is int num))
          ;
        if (num > 0)
          flag = true;
        dbScope.Complete();
      }
    }
    return flag;
  }

  public void DeleteTest(Patient patient, AudiologyTestInformation audiologyTestInformation)
  {
    if (patient == null || patient.AudiologyTests == null || patient.AudiologyTests.Count == 0 || audiologyTestInformation == null || !patient.AudiologyTests.Contains(audiologyTestInformation))
      return;
    AudiologyTestManager.Instance.DeleteTests(new List<AudiologyTestInformation>()
    {
      audiologyTestInformation
    });
    this.RefreshData();
  }

  public void AssignCopiedTestToNewPatient(
    Patient patient,
    AudiologyTestInformation audiologyTestInformation)
  {
    if (patient == null || audiologyTestInformation == null)
      return;
    if (patient.AudiologyTests == null)
      AudiologyTestManager.Instance.AssignTestToNewPatient(patient.Id, this.TestToCopy);
    else if (!patient.AudiologyTests.Contains(audiologyTestInformation))
      AudiologyTestManager.Instance.AssignTestToNewPatient(patient.Id, this.TestToCopy);
    this.RefreshData();
    this.TestToCopy = (AudiologyTestInformation) null;
  }
}
