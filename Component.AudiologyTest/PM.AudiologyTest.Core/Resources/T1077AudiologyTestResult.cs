// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.T1077AudiologyTestResult
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

#nullable disable
namespace PathMedical.AudiologyTest;

public enum T1077AudiologyTestResult : short
{
  Pass = 13175, // 0x3377
  Abort = 16706, // 0x4142
  CalibrationAbort = 16707, // 0x4143
  CalibrationError = 17221, // 0x4345
  CalibrationTimeout = 17236, // 0x4354
  ElectrodeLoose = 17740, // 0x454C
  CalibrationLeaky = 19526, // 0x4C46
  CalibrationNoisy = 20035, // 0x4E43
  ProbeError = 29555, // 0x7373
  Referred = 30583, // 0x7777
}
