// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.ServiceToolsTriggers
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

public abstract class ServiceToolsTriggers
{
  public static readonly Trigger StartToneGenerator = new Trigger(nameof (StartToneGenerator));
  public static readonly Trigger StopToneGenerator = new Trigger(nameof (StopToneGenerator));
  public static readonly Trigger ComputeCorrectionValueTrigger = new Trigger(nameof (ComputeCorrectionValueTrigger));
  public static readonly Trigger GetMicrofoneRmsTrigger = new Trigger(nameof (GetMicrofoneRmsTrigger));
  public static readonly Trigger ComputeMicrofoneCorrectionValueTrigger = new Trigger(nameof (ComputeMicrofoneCorrectionValueTrigger));
  public static readonly Trigger SendProbeConfigurationTrigger = new Trigger(nameof (SendProbeConfigurationTrigger));
  public static readonly Trigger SendDeviceConfigurationTrigger = new Trigger(nameof (SendDeviceConfigurationTrigger));
  public static readonly Trigger LoopBackCableTestTrigger = new Trigger(nameof (LoopBackCableTestTrigger));
  public static readonly Trigger DeleteDeviceMemoryTrigger = new Trigger(nameof (DeleteDeviceMemoryTrigger));
  public static readonly Trigger SelectFirmwareFileTrigger = new Trigger(nameof (SelectFirmwareFileTrigger));
  public static readonly Trigger UploadFirmwareFileTrigger = new Trigger(nameof (UploadFirmwareFileTrigger));
  public static readonly Trigger SetFirmwareLicenceTrigger = new Trigger(nameof (SetFirmwareLicenceTrigger));
  public static Trigger UpdateFirmwareTrigger = new Trigger(nameof (UpdateFirmwareTrigger));
}
