// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.DpoaeTestInformation
// Assembly: PM.DPOAE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 38B92F02-B758-4EF7-9103-415B55783CFC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.dll

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
namespace PathMedical.DPOAE;

[DbTable(Name = "DPOAETestInformation", HasHistory = false)]
[DataExchangeRecord("DPOAE.TestInformation")]
[Serializable]
public class DpoaeTestInformation : ITestInformation, ISerializable
{
  private List<FreeTextComment> freeTextComments;
  private List<PredefinedComment> predefinedComments;

  [DbColumn]
  public Guid AudiologyTestId { get; set; }

  public Guid NativeTestTypeSignature => new Guid("44DC6D54-7FC4-4cd4-B109-9A61F9712013");

  public static Guid TestTypeSignature => new Guid("44DC6D54-7FC4-4cd4-B109-9A61F9712013");

  public Guid ProbeId { get; set; }

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

  [DbColumn]
  [DataExchangeColumn]
  public Guid? FacilityId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? LocationId { get; set; }

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
  public int? TestResult { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? Version { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? NumberOfTests { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? FrequencySamplingRate { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int[] Frequency1List { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int[] Frequency2List { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int[] NormalizedFrequency1List { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int[] NormalizedFrequency2List { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int[] SingleResultList { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public double[] DPOAEList { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public double[] NoiseList { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int[] FrameCounterList { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public double[] ReList { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public double[] ImList { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public double[] EnergyList { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? L1 { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public int? L2 { get; set; }

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
  public sbyte[] CalibrationGraph { get; set; }

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
        DpoaeTestManager.LoadTestFreeTextComments(this);
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
        DpoaeTestManager.LoadTestPredefinedComments(this);
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

  public DpoaeTestInformation()
  {
  }

  public EntityLoadInformation LoadInformation { get; set; }

  public DpoaeTestInformation(SerializationInfo info, StreamingContext context)
  {
    SerializationHelper.Create<DbColumnAttribute>(info, context, (object) this);
  }

  public void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    SerializationHelper.Serialize<DbColumnAttribute>(info, context, (object) this);
  }
}
