// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Patient
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.AudiologyTest;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.Property;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#nullable disable
namespace PathMedical.PatientManagement;

[DbTable(HasHistory = true)]
[DebuggerDisplay("Patient {PatientRecordNumber} {Id}")]
[DataExchangeRecord]
public class Patient
{
  private List<FreeTextComment> freeTextComments;
  private List<PredefinedComment> predefinedComments;

  [DbPrimaryKeyColumn]
  [DataExchangeColumn("PatientId")]
  public Guid Id { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string PatientRecordNumber { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string HospitalId { get; set; }

  public string PatientRecordNumberOrHospitalId
  {
    get
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("{0}", string.IsNullOrEmpty(this.PatientRecordNumber) ? (object) "-" : (object) this.PatientRecordNumber);
      stringBuilder.AppendFormat(" / {0}", string.IsNullOrEmpty(this.HospitalId) ? (object) "-" : (object) this.HospitalId);
      return stringBuilder.ToString();
    }
  }

  [DbColumn]
  [DataExchangeColumn]
  public PathMedical.PatientManagement.NicuStatus? NicuStatus { get; set; }

  [DbColumn(Name = "ConsentState")]
  [DataExchangeColumn]
  public PathMedical.PatientManagement.ConsentStatus? ConsentStatus { get; set; }

  [DbIntermediateTableRelation("PatientId", "ContactId", "PatientContactAssociation", "RelationToPatient", ContactRelationType.Patient, HasHistory = true, InsertTimestampColumn = "Created", InsertUpdateTimestampColumn = "Modified")]
  [DataExchangeColumn]
  public Contact PatientContact { get; set; }

  [DbIntermediateTableRelation("PatientId", "ContactId", "PatientContactAssociation", "RelationToPatient", ContactRelationType.BirthMother, HasHistory = true, InsertTimestampColumn = "Created", InsertUpdateTimestampColumn = "Modified")]
  [DataExchangeColumn]
  public Contact MotherContact { get; set; }

  [DbIntermediateTableRelation("PatientId", "ContactId", "PatientContactAssociation", "RelationToPatient", ContactRelationType.LegalGuardian, HasHistory = true, InsertTimestampColumn = "Created", InsertUpdateTimestampColumn = "Modified")]
  [DataExchangeColumn]
  public Contact CaregiverContact { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string Medication { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? DischargeTimeStamp { get; set; }

  [DbColumn]
  public DateTime? ArchiveTimeStamp { get; set; }

  [DbColumn]
  public DateTime? ExportTimeStamp { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  [DataExchangeColumn]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  [DataExchangeColumn]
  public DateTime Modified { get; set; }

  [DbIntermediateTableRelation("PatientId", "RiskIndicatorId", "PatientRiskIndicators", HasHistory = true, InsertTimestampColumn = "Created", InsertUpdateTimestampColumn = "Modified")]
  [DataExchangeColumn]
  public List<RiskIndicator> RiskIndicators { get; set; }

  public RiskIndicatorValueType HasActiveRiskIndicator
  {
    get
    {
      RiskIndicatorValueType activeRiskIndicator = RiskIndicatorValueType.Unknown;
      if (this.RiskIndicators != null)
      {
        RiskIndicator riskIndicator1 = this.RiskIndicators.FirstOrDefault<RiskIndicator>((Func<RiskIndicator, bool>) (r => r.IsActive.GetValueOrDefault() && r.PatientRiskIndicatorValue == RiskIndicatorValueType.Yes));
        RiskIndicator riskIndicator2 = this.RiskIndicators.FirstOrDefault<RiskIndicator>((Func<RiskIndicator, bool>) (r => r.IsActive.GetValueOrDefault() && r.PatientRiskIndicatorValue == RiskIndicatorValueType.No));
        RiskIndicator riskIndicator3 = this.RiskIndicators.FirstOrDefault<RiskIndicator>((Func<RiskIndicator, bool>) (r => r.IsActive.GetValueOrDefault() && r.PatientRiskIndicatorValue == RiskIndicatorValueType.Unknown));
        if (riskIndicator2 != null)
          activeRiskIndicator = RiskIndicatorValueType.No;
        if (riskIndicator3 != null)
          activeRiskIndicator = RiskIndicatorValueType.Unknown;
        if (riskIndicator1 != null)
          activeRiskIndicator = RiskIndicatorValueType.Yes;
      }
      return activeRiskIndicator;
    }
  }

  public int ActiveRiskIndicatorCount
  {
    get
    {
      return this.RiskIndicators != null ? this.RiskIndicators.Count<RiskIndicator>((Func<RiskIndicator, bool>) (r => r.IsActive.GetValueOrDefault() && r.PatientRiskIndicatorValue == RiskIndicatorValueType.Yes)) : 0;
    }
  }

  [DbBackReferenceRelation("Id", "PatientId")]
  [DataExchangeColumn]
  public List<PathMedical.AudiologyTest.OverallTestInformation> OverallTestInformation { get; set; }

  public PathMedical.AudiologyTest.OverallTestInformation OverallResultLeftEarAbr
  {
    get => this.GetOverallTestInformation(TestType.ABR, TestObject.LeftEar);
  }

  public PathMedical.AudiologyTest.OverallTestInformation OverallResultRightEarAbr
  {
    get => this.GetOverallTestInformation(TestType.ABR, TestObject.RightEar);
  }

  public PathMedical.AudiologyTest.OverallTestInformation OverallResultLeftEarDpoae
  {
    get => this.GetOverallTestInformation(TestType.DPOAE, TestObject.LeftEar);
  }

  public PathMedical.AudiologyTest.OverallTestInformation OverallResultRightEarDpoae
  {
    get => this.GetOverallTestInformation(TestType.DPOAE, TestObject.RightEar);
  }

  public PathMedical.AudiologyTest.OverallTestInformation OverallResultLeftEarTeoae
  {
    get => this.GetOverallTestInformation(TestType.TEOAE, TestObject.LeftEar);
  }

  public PathMedical.AudiologyTest.OverallTestInformation OverallResultRightEarTeoae
  {
    get => this.GetOverallTestInformation(TestType.TEOAE, TestObject.RightEar);
  }

  public PathMedical.AudiologyTest.OverallTestInformation OverallResultLeftEarDiagnostic
  {
    get => this.GetOverallTestInformation(TestType.DpoaeDiagnostic, TestObject.LeftEar);
  }

  public PathMedical.AudiologyTest.OverallTestInformation OverallResultRightEarDiagnostic
  {
    get => this.GetOverallTestInformation(TestType.DpoaeDiagnostic, TestObject.RightEar);
  }

  private PathMedical.AudiologyTest.OverallTestInformation GetOverallTestInformation(
    TestType testType,
    TestObject testObject)
  {
    return this.OverallTestInformation == null ? (PathMedical.AudiologyTest.OverallTestInformation) null : this.OverallTestInformation.SingleOrDefault<PathMedical.AudiologyTest.OverallTestInformation>((Func<PathMedical.AudiologyTest.OverallTestInformation, bool>) (oti => oti.TestType == testType && oti.TestObject == testObject));
  }

  [DbBackReferenceRelation("Id", "PatientId")]
  [DataExchangeColumn]
  public List<AudiologyTestInformation> AudiologyTests { get; set; }

  [DbColumn]
  public string FreeText1 { get; set; }

  [DbColumn]
  public string FreeText2 { get; set; }

  [DbColumn]
  public string FreeText3 { get; set; }

  [DataExchangeColumn]
  public List<FreeTextComment> FreeTextComments
  {
    get
    {
      if (this.freeTextComments == null)
        PatientManager.LoadPatientFreeTextComments(this);
      return this.freeTextComments;
    }
    set => this.freeTextComments = value;
  }

  [DataExchangeColumn]
  public List<PredefinedComment> PredefinedComments
  {
    get
    {
      if (this.predefinedComments == null)
        PatientManager.LoadPatientPredefinedComments(this);
      return this.predefinedComments;
    }
    set => this.predefinedComments = value;
  }

  public bool HasComments
  {
    get
    {
      return this.FreeTextComments != null && this.FreeTextComments.Count<FreeTextComment>() > 0 || this.PredefinedComments != null && this.PredefinedComments.Count<PredefinedComment>() > 0;
    }
  }

  public IEnumerable<Comment> Comments
  {
    get
    {
      List<Comment> source = new List<Comment>();
      if (this.FreeTextComments != null && this.FreeTextComments.Count > 0)
        source.AddRange(this.FreeTextComments.Cast<Comment>());
      if (this.PredefinedComments != null && this.PredefinedComments.Count > 0)
      {
        this.PredefinedComments.OfType<Comment>().ToList<Comment>();
        source.AddRange(this.PredefinedComments.OfType<Comment>());
      }
      return (IEnumerable<Comment>) source.OrderBy<Comment, DateTime?>((Func<Comment, DateTime?>) (c => c.CreationDate)).ToList<Comment>();
    }
  }

  [DataExchangeColumn]
  public DateTime ExternalCreationTimeStamp { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj) => obj is Patient patient && this.Id.Equals(patient.Id);

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();

  public override string ToString() => $"{base.ToString()} Data [{this.Id}]";

  public object this[string propertyName]
  {
    get
    {
      return PropertyHelper<DataExchangeColumnAttribute>.GetPropertyValue(propertyName, (object) this);
    }
    set
    {
      object target = (object) this;
      PropertyHelper<DataExchangeColumnAttribute>.SetValue(propertyName, value, ref target);
    }
  }
}
