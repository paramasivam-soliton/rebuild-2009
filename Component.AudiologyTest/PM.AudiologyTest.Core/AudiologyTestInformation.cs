// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.AudiologyTestInformation
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

using PathMedical.AudiologyTest.Properties;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using PathMedical.DataExchange.Description;
using PathMedical.InstrumentManagement;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserProfileManagement;
using System;
using System.Drawing;
using System.Linq;

#nullable disable
namespace PathMedical.AudiologyTest;

[DbTable(Name = "AudiologyTestInformation", HasHistory = true)]
[DataExchangeRecord("Common.TestInformation")]
public class AudiologyTestInformation
{
  [DbPrimaryKeyColumn]
  [DataExchangeColumn]
  public Guid TestDetailId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? PatientId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public TestObject TestObject { get; set; }

  public string TestObjectName
  {
    get => GlobalResourceEnquirer.Instance.GetResourceByName((Enum) this.TestObject) as string;
  }

  [DbColumn]
  [DataExchangeColumn]
  public AudiologyTestProcedure? TestProcedure { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public TestType TestType { get; set; }

  public string TestTypeName
  {
    get => GlobalResourceEnquirer.Instance.GetResourceByName((Enum) this.TestType) as string;
  }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? TestTypeSignature { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public DateTime? TestDate { get; set; }

  public AudiologyTestResult? TestResult { get; set; }

  public string TestResultName
  {
    get
    {
      return this.TestObject.Equals((object) TestObject.LeftEar) ? GlobalResourceEnquirer.Instance.GetResourceByName((Enum) (ValueType) this.LeftEarTestResult) as string : GlobalResourceEnquirer.Instance.GetResourceByName((Enum) (ValueType) this.RightEarTestResult) as string;
    }
  }

  [DbColumn]
  [DataExchangeColumn]
  public int? Duration { get; set; }

  public string DurationFormatted
  {
    get
    {
      return this.Duration.HasValue ? string.Format("{0:mm:ss}", (object) (new DateTime() + TimeSpan.FromMilliseconds((double) this.Duration.Value))) : (string) null;
    }
  }

  [DbColumn]
  [DataExchangeColumn]
  public string TestName { get; set; }

  public Bitmap TestResultImage
  {
    get
    {
      return TestResultImageBuilder.Instance.GetTestResultImage(this.RightEarTestResult, this.LeftEarTestResult);
    }
  }

  [DbColumn]
  [DataExchangeColumn]
  public AudiologyTestResult? RightEarTestResult { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public AudiologyTestResult? LeftEarTestResult { get; set; }

  [DataExchangeColumn]
  public bool IsControlTest { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? FacilityId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? FacilityLocationId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? InstrumentId { get; set; }

  public string Instrument
  {
    get
    {
      if (!this.InstrumentId.HasValue)
        return (string) null;
      return InstrumentManager.Instance.Instruments.FirstOrDefault<PathMedical.InstrumentManagement.Instrument>((Func<PathMedical.InstrumentManagement.Instrument, bool>) (i =>
      {
        Guid id = i.Id;
        Guid? instrumentId = this.InstrumentId;
        return instrumentId.HasValue && id == instrumentId.GetValueOrDefault();
      }))?.Name;
    }
  }

  public string InstrumentSerial
  {
    get
    {
      if (!this.InstrumentId.HasValue)
        return (string) null;
      return InstrumentManager.Instance.Instruments.FirstOrDefault<PathMedical.InstrumentManagement.Instrument>((Func<PathMedical.InstrumentManagement.Instrument, bool>) (i =>
      {
        Guid id = i.Id;
        Guid? instrumentId = this.InstrumentId;
        return instrumentId.HasValue && id == instrumentId.GetValueOrDefault();
      }))?.SerialNumber;
    }
  }

  [DataExchangeColumn]
  public Guid? ProbeId { get; set; }

  [DbColumn]
  [DataExchangeColumn]
  public Guid? UserAccountId { get; set; }

  public string Examiner
  {
    get
    {
      if (!this.UserAccountId.HasValue)
        return Resources.AudiologyTestInformation_Examiner_Unknown_Screener;
      return UserManager.Instance.Users.FirstOrDefault<User>((Func<User, bool>) (u =>
      {
        Guid id = u.Id;
        Guid? userAccountId = this.UserAccountId;
        return userAccountId.HasValue && id == userAccountId.GetValueOrDefault();
      }))?.LoginName;
    }
  }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  [DataExchangeColumn]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  [DataExchangeColumn]
  public DateTime? Modified { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public ITestInformation DetailInformation
  {
    get
    {
      ITestPlugin testPlugin = SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().FirstOrDefault<ITestPlugin>((Func<ITestPlugin, bool>) (p =>
      {
        Guid testTypeSignature1 = p.TestTypeSignature;
        Guid? testTypeSignature2 = this.TestTypeSignature;
        return testTypeSignature2.HasValue && testTypeSignature1 == testTypeSignature2.GetValueOrDefault();
      }));
      return testPlugin != null ? testPlugin.GetTestInformation(this.TestDetailId) as ITestInformation : (ITestInformation) null;
    }
  }

  public override bool Equals(object obj)
  {
    return obj is AudiologyTestInformation audiologyTestInformation && this.TestDetailId.Equals(audiologyTestInformation.TestDetailId);
  }

  public override int GetHashCode()
  {
    return this.GetType().GetHashCode() + this.TestDetailId.GetHashCode();
  }
}
