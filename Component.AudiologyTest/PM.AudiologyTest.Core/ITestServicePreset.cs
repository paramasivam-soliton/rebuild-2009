// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.ITestServicePreset
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

using System;

#nullable disable
namespace PathMedical.AudiologyTest;

public interface ITestServicePreset
{
  Guid Id { get; }

  string Name { get; }

  string Description { get; }

  Guid TestServiceSignature { get; }
}
