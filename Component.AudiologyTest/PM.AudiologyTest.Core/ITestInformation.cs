// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.ITestInformation
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

using System;

#nullable disable
namespace PathMedical.AudiologyTest;

public interface ITestInformation
{
  Guid AudiologyTestId { get; set; }

  Guid? PatientId { get; set; }

  Guid NativeTestTypeSignature { get; }

  Guid Id { get; set; }

  int? Ear { get; set; }

  Guid? BinauralTestInformationId { get; set; }

  DateTime? TestTimeStamp { get; set; }

  Guid? UserAccountId { get; set; }

  long? ProbeSerialNumber { get; set; }

  long? InstrumentSerialNumber { get; set; }
}
