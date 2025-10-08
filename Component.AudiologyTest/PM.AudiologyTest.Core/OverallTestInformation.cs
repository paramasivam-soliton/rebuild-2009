// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.OverallTestInformation
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DatabaseManagement.Attributes;
using System;

#nullable disable
namespace PathMedical.AudiologyTest;

[DbTable(HasHistory = true)]
public class OverallTestInformation
{
  [DbPrimaryKeyColumn]
  public Guid Id { get; set; }

  [DbColumn]
  public Guid PatientId { get; set; }

  [DbColumn]
  public TestType TestType { get; set; }

  [DbColumn]
  public TestObject TestObject { get; set; }

  [DbColumn]
  public AudiologyTestResult OverallTestResult { get; set; }

  [DbColumn]
  public Guid? ReferenceToTestId { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsert)]
  public DateTime Created { get; set; }

  [DbTimestampColumn(TimestampSetOption.OnInsertAndUpdate)]
  public DateTime Modified { get; set; }

  public EntityLoadInformation LoadInformation { get; set; }

  public override bool Equals(object obj)
  {
    return obj is OverallTestInformation overallTestInformation && this.Id.Equals(overallTestInformation.Id);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.Id.GetHashCode();
}
