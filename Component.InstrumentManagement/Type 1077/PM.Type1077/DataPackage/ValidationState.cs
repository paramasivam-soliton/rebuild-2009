// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentCommunicator.DataPackage.ValidationState
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

#nullable disable
namespace PathMedical.InstrumentCommunicator.DataPackage;

public enum ValidationState
{
  NotEnoughData,
  ChecksumMismatch,
  Valid,
  InvalidData,
  NoDataAvailable,
}
