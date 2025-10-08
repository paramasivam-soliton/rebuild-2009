// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.AudiologyTestResult
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

#nullable disable
namespace PathMedical.AudiologyTest;

public enum AudiologyTestResult : short
{
  Empty = 0,
  Pass = 2,
  Refer = 4,
  Incomplete = 8,
  Diagnostic = 16, // 0x0010
}
