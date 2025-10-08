// Decompiled with JetBrains decompiler
// Type: PathMedical.TEOAE.TeoaeTestManager
// Assembly: PM.TEOAE, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7328F97-8442-4910-9451-35D76FF019BE
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.TEOAE.dll

using PathMedical.AudiologyTest;
using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.Login;
using PathMedical.PatientManagement;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.PatientManagement.DataAccessLayer;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PathMedical.TEOAE;

public class TeoaeTestManager : IModel
{
  private readonly ModelHelper<TeoaeTestInformation, TestInformationAdapter> detailDataModelHelper;

  public static TeoaeTestManager Instance => PathMedical.Singleton.Singleton<TeoaeTestManager>.Instance;

  private TeoaeTestManager()
  {
    this.detailDataModelHelper = new ModelHelper<TeoaeTestInformation, TestInformationAdapter>(new EventHandler<ModelChangedEventArgs>(this.OnModelChanged), Array.Empty<string>());
    this.LoadDataExchangeDefinitions();
    PatientManager.Instance.Changed += new EventHandler<ModelChangedEventArgs>(this.OnPatientManagerChanged);
  }

  private void OnPatientManagerChanged(object sender, ModelChangedEventArgs e)
  {
    if (!(e.ChangedObject is AudiologyTestInformation))
      return;
    AudiologyTestInformation audiologyTest = e.ChangedObject as AudiologyTestInformation;
    Guid? testTypeSignature1 = audiologyTest.TestTypeSignature;
    Guid testTypeSignature2 = TeoaeTestInformation.TestTypeSignature;
    if ((testTypeSignature1.HasValue ? (testTypeSignature1.GetValueOrDefault() == testTypeSignature2 ? 1 : 0) : 0) == 0)
      return;
    // ISSUE: reference to a compiler-generated field
    this.detailDataModelHelper.LoadItems((Func<TestInformationAdapter, ICollection<TeoaeTestInformation>>) (adapter => adapter.FetchEntities((Expression<Func<TeoaeTestInformation, bool>>) (t => t.AudiologyTestId == this.audiologyTest.TestDetailId))));
    this.detailDataModelHelper.SelectedItem = this.detailDataModelHelper.Items.FirstOrDefault<TeoaeTestInformation>();
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.ModelChanged == null || !(e.ChangedObject is TeoaeTestInformation))
      return;
    this.ModelChanged(sender, ModelChangedEventArgs.Create<TeoaeTestInformation>(this.detailDataModelHelper.SelectedItem, ChangeType.SelectionChanged));
  }

  public event EventHandler<ModelChangedEventArgs> Changed
  {
    add
    {
      value((object) this, ModelChangedEventArgs.Create<TeoaeTestInformation>(this.detailDataModelHelper.SelectedItem, ChangeType.ListLoaded));
      this.ModelChanged += value;
    }
    remove => this.ModelChanged -= value;
  }

  private event EventHandler<ModelChangedEventArgs> ModelChanged;

  public void RefreshData()
  {
  }

  public static AudiologyTestInformation CreateAudiologyTestInformation(
    TeoaeTestInformation testInformation)
  {
    int? testResult = testInformation.TestResult;
    AudiologyTestResult audiologyTestResult;
    if (testResult.HasValue)
    {
      switch (testResult.GetValueOrDefault())
      {
        case 13175:
          audiologyTestResult = AudiologyTestResult.Pass;
          goto label_5;
        case 30583 /*0x7777*/:
          audiologyTestResult = AudiologyTestResult.Refer;
          goto label_5;
      }
    }
    audiologyTestResult = AudiologyTestResult.Incomplete;
label_5:
    AudiologyTestResult? nullable1 = new AudiologyTestResult?();
    AudiologyTestResult? nullable2 = new AudiologyTestResult?();
    int? nullable3 = testInformation.Ear;
    TestObject testObject;
    if (nullable3.GetValueOrDefault() == 7)
    {
      nullable1 = new AudiologyTestResult?(audiologyTestResult);
      testObject = TestObject.LeftEar;
    }
    else
    {
      nullable2 = new AudiologyTestResult?(audiologyTestResult);
      testObject = TestObject.RightEar;
    }
    nullable3 = testInformation.Duration;
    int num1;
    if (!nullable3.HasValue)
    {
      num1 = 0;
    }
    else
    {
      nullable3 = testInformation.Duration;
      num1 = nullable3.Value * 1000;
    }
    int num2 = num1;
    AudiologyTestInformation audiologyTestInformation = new AudiologyTestInformation()
    {
      TestDetailId = Guid.NewGuid(),
      TestType = TestType.TEOAE,
      TestTypeSignature = new Guid?(testInformation.NativeTestTypeSignature),
      TestObject = testObject,
      PatientId = testInformation.PatientId,
      TestDate = testInformation.TestTimeStamp,
      RightEarTestResult = nullable2,
      LeftEarTestResult = nullable1,
      Duration = new int?(num2)
    };
    Guid? nullable4 = testInformation.UserAccountId;
    Guid empty1 = Guid.Empty;
    if ((nullable4.HasValue ? (nullable4.GetValueOrDefault() != empty1 ? 1 : 0) : 1) != 0)
      audiologyTestInformation.UserAccountId = testInformation.UserAccountId;
    nullable4 = testInformation.LocationId;
    Guid empty2 = Guid.Empty;
    if ((nullable4.HasValue ? (nullable4.GetValueOrDefault() != empty2 ? 1 : 0) : 1) != 0)
      audiologyTestInformation.FacilityLocationId = testInformation.LocationId;
    nullable4 = testInformation.FacilityId;
    Guid empty3 = Guid.Empty;
    if ((nullable4.HasValue ? (nullable4.GetValueOrDefault() != empty3 ? 1 : 0) : 1) != 0)
      audiologyTestInformation.FacilityId = testInformation.FacilityId;
    nullable4 = testInformation.LocationId;
    Guid empty4 = Guid.Empty;
    if ((nullable4.HasValue ? (nullable4.GetValueOrDefault() != empty4 ? 1 : 0) : 1) != 0)
      audiologyTestInformation.FacilityLocationId = testInformation.LocationId;
    testInformation.AudiologyTestId = audiologyTestInformation.TestDetailId;
    Instrument instrument = InstrumentManager.Instance.Instruments.FirstOrDefault<Instrument>((Func<Instrument, bool>) (i => string.Compare(i.SerialNumber, Convert.ToString((object) testInformation.InstrumentSerialNumber)) == 0));
    if (instrument != null)
      audiologyTestInformation.InstrumentId = new Guid?(instrument.Id);
    if (!string.IsNullOrEmpty(testInformation.TestName))
      audiologyTestInformation.TestName = testInformation.TestName;
    return audiologyTestInformation;
  }

  public List<DataExchangeSetMap> RecordSetMaps { get; protected set; }

  public List<RecordDescriptionSet> RecordDescriptionSets { get; protected set; }

  private void LoadDataExchangeDefinitions()
  {
    try
    {
      this.RecordDescriptionSets = new List<RecordDescriptionSet>();
      this.RecordSetMaps = new List<DataExchangeSetMap>();
      this.RecordDescriptionSets.Add(RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.TEOAE.DataExchange.PluginDataDescription.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.TeoaeTestManager_DataExchange_StructureFileNotFound)));
      this.RecordDescriptionSets.Add(RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.TEOAE.DataExchange.InstrumentDataDescription.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.TeoaeTestManager_DataExchange_StructureFileNotFound)));
      this.RecordSetMaps.AddRange((IEnumerable<DataExchangeSetMap>) DataExchangeSetMap.LoadSetsFromXml(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.TEOAE.DataExchange.PluginDataMapping.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.TeoaeTestManager_DataExchange_MappingFileNotFound)));
    }
    catch (System.Exception ex)
    {
      throw;
    }
  }

  public static void LoadTestFreeTextComments(TeoaeTestInformation teoaeTestInformation)
  {
    if (teoaeTestInformation == null || teoaeTestInformation.Id.Equals(Guid.Empty))
      return;
    using (DBScope scope = new DBScope())
    {
      List<FreeTextComment> list = new AdapterBase<FreeTextComment>(scope).FetchEntities((Expression<Func<FreeTextComment, bool>>) (pdca => pdca.ReferenceId == teoaeTestInformation.Id)).ToList<FreeTextComment>();
      teoaeTestInformation.FreeTextComments = list.ToList<FreeTextComment>();
      scope.Complete();
    }
  }

  public static void LoadTestPredefinedComments(TeoaeTestInformation teoaeTestInformation)
  {
    if (teoaeTestInformation == null || teoaeTestInformation.Id.Equals(Guid.Empty))
      return;
    using (DBScope scope = new DBScope())
    {
      AdapterBase<PredefinedTestCommentAssociation> adapterBase = new AdapterBase<PredefinedTestCommentAssociation>(scope);
      adapterBase.LoadWithRelation("PredefinedComment.CommentTranslationList");
      List<PredefinedTestCommentAssociation> list = adapterBase.FetchEntities((Expression<Func<PredefinedTestCommentAssociation, bool>>) (pdca => pdca.ReferenceId == teoaeTestInformation.Id)).ToList<PredefinedTestCommentAssociation>();
      teoaeTestInformation.PredefinedComments = list.Select<PredefinedTestCommentAssociation, PredefinedComment>((Func<PredefinedTestCommentAssociation, PredefinedComment>) (association => TeoaeTestManager.UpdatePredefinedCommentRelationValues(association))).ToList<PredefinedComment>();
      scope.Complete();
    }
  }

  private static PredefinedComment UpdatePredefinedCommentRelationValues(
    PredefinedTestCommentAssociation association)
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
    if (freeTextComment == null || this.detailDataModelHelper.SelectedItem == null)
      return;
    TeoaeTestInformation selectedItem = this.detailDataModelHelper.SelectedItem;
    if (selectedItem.FreeTextComments == null)
      selectedItem.FreeTextComments = new List<FreeTextComment>();
    freeTextComment.ReferenceId = selectedItem.Id;
    freeTextComment.CreationDate = new DateTime?(DateTime.Now);
    if (LoginManager.Instance.LoggedInUserData != null)
      freeTextComment.UserAccountId = new Guid?(LoginManager.Instance.LoggedInUserData.Id);
    selectedItem.FreeTextComments.Add(freeTextComment);
    try
    {
      using (DBScope scope = new DBScope())
      {
        new FreeTextCommentAdapter(scope).Store((ICollection<FreeTextComment>) selectedItem.FreeTextComments);
        scope.Complete();
      }
      TeoaeTestManager.LoadTestFreeTextComments(this.detailDataModelHelper.SelectedItem);
      if (this.ModelChanged == null)
        return;
      this.ModelChanged((object) this, ModelChangedEventArgs.Create<TeoaeTestInformation>(this.detailDataModelHelper.SelectedItem, ChangeType.SelectionChanged));
    }
    catch (System.Exception ex)
    {
      throw;
    }
  }

  public void AddPredefinedComment(PredefinedComment predefinedComment)
  {
    if (predefinedComment == null || this.detailDataModelHelper.SelectedItem == null)
      return;
    TeoaeTestInformation selectedItem = this.detailDataModelHelper.SelectedItem;
    if (selectedItem.PredefinedComments == null)
      selectedItem.PredefinedComments = new List<PredefinedComment>();
    predefinedComment.AssociationId = new Guid?(Guid.NewGuid());
    predefinedComment.CreationDate = new DateTime?(DateTime.Now);
    if (LoginManager.Instance.LoggedInUserData != null)
      predefinedComment.UserAccountId = new Guid?(LoginManager.Instance.LoggedInUserData.Id);
    selectedItem.PredefinedComments.Add(predefinedComment);
    try
    {
      using (DBScope scope = new DBScope())
      {
        AdapterBase<PredefinedTestCommentAssociation> adapterBase = new AdapterBase<PredefinedTestCommentAssociation>(scope);
        PredefinedTestCommentAssociation commentAssociation = new PredefinedTestCommentAssociation();
        commentAssociation.AssociationId = predefinedComment.AssociationId.Value;
        commentAssociation.PredefinedCommentId = predefinedComment.PredefinedCommentId;
        commentAssociation.CreationDate = predefinedComment.CreationDate;
        commentAssociation.UserAccountId = predefinedComment.UserAccountId;
        commentAssociation.ReferenceId = selectedItem.Id;
        PredefinedTestCommentAssociation entity = commentAssociation;
        adapterBase.Store(entity);
        scope.Complete();
      }
      TeoaeTestManager.LoadTestPredefinedComments(this.detailDataModelHelper.SelectedItem);
      if (this.ModelChanged == null)
        return;
      this.ModelChanged((object) this, ModelChangedEventArgs.Create<TeoaeTestInformation>(this.detailDataModelHelper.SelectedItem, ChangeType.SelectionChanged));
    }
    catch (System.Exception ex)
    {
      throw;
    }
  }
}
