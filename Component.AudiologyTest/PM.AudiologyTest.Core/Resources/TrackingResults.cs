// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.TrackingResults
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

#nullable disable
namespace PathMedical.AudiologyTest;

public enum TrackingResults : short
{
  Invalid = 2,
  NotRequired = 4,
  Missed = 8,
  Refused = 16, // 0x0010
  Transferred = 32, // 0x0020
  Deceased = 64, // 0x0040
  Scheduled = 128, // 0x0080
  BrokenAppointment = 256, // 0x0100
  LocateLost = 512, // 0x0200
  FollowUpDiscountinued = 1024, // 0x0400
  NoScreening = 2048, // 0x0800
  TechnicalError = 4096, // 0x1000
  Other = 8192, // 0x2000
}
