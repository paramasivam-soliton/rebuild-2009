// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.AbrTestInformation
// Assembly: PM.ABR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 09E7728F-8618-4147-9D4A-E38CA516B245
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.dll

using PathMedical.AudiologyTest;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PathMedical.ABR;

[DbTable(Name = "ABRTestInformation", HasHistory = false)]
[DataExchangeRecord("ABR.TestInformation")]
[Serializable]
public class AbrTestInformation : ITestInformation, ISerializable
{
  private List<FreeTextComment> freeTextComments;
  private List<PredefinedComment> predefinedComments;

  [DbColumn]
  public Guid AudiologyTestId { get; set; }

  public Guid NativeTestTypeSignature => new Guid("FA103F74-D1E2-4a38-AC09-7E585B2AC71E");

  public static Guid TestTypeSignature => new Guid("FA103F74-D1E2-4a38-AC09-7E585B2AC71E");

  public Guid? ProbeId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public long? ProbeSerialNumber { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? ProbeType { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? ProbeCounter { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? FirmwareVersion { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? ProbeCalibrationDate { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public long? InstrumentSerialNumber { get; set; }

  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid Id { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? PatientId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? Ear { get; set; }

  public Guid? BinauralTestInformationId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? TestTimeStamp { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? Duration { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? UserAccountId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public string TestName { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? FacilityId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? LocationId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? TestResult { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? Version { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? Progress { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? Eeg { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? Abr { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public double? GraphScale { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int[] Graph { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? Frames { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public double? Energy { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? SimulusLevel { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? FrequencySamplingRate { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int[] Impedances { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public double[] NotchAmplifier { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? VariableNotchFrequency { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? CalibrationFrames { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? CalibrationValues { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public double? CalibrationGraphScale { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int[] CalibrationGraph { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  [DataExchangeColumn]
  public DateTime? Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  [DataExchangeColumn]
  public DateTime? Modified { get; set; }

  [DataExchangeColumn]
  public List<FreeTextComment> FreeTextComments
  {
    get
    {
      if (this.freeTextComments == null)
        AbrTestManager.LoadTestFreeTextComments(this);
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
        AbrTestManager.LoadTestPredefinedComments(this);
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
        source.AddRange(this.PredefinedComments.OfType<Comment>());
      return (IEnumerable<Comment>) source.OrderByDescending<Comment, DateTime?>((Func<Comment, DateTime?>) (c => c.CreationDate)).ToList<Comment>();
    }
  }

  public AbrTestInformation()
  {
  }

  public EntityLoadInformation LoadInformation { get; set; }

  public AbrTestInformation(SerializationInfo info, StreamingContext context)
  {
    SerializationHelper.Create<DbColumnAttribute>(info, context, (object) this);
  }

  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    SerializationHelper.Serialize<DbColumnAttribute>(info, context, (object) this);
  }
}
