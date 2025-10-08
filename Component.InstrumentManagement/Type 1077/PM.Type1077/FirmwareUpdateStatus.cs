// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.FirmwareUpdateStatus
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Type1077.Firmware;

#nullable disable
namespace PathMedical.Type1077;

public class FirmwareUpdateStatus
{
  public FirmwareImage FirmwareImage { get; set; }

  public FirmwareUpdateStatus.FirmwareUpdateStatusType Status { get; set; }

  public enum FirmwareUpdateStatusType
  {
    Uploaded,
    Failed,
    AlreadyOnInstrument,
  }
}
