// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.InstrumentManagementTriggers
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Automaton;
using System;

#nullable disable
namespace PathMedical.Type1077;

[CLSCompliant(false)]
public abstract class InstrumentManagementTriggers
{
  public static readonly Trigger SwitchInstrumentSoftwareImport = new Trigger(nameof (SwitchInstrumentSoftwareImport));
  public static readonly Trigger StartInstrumentSearch = new Trigger(nameof (StartInstrumentSearch));
  public static readonly Trigger StartDataDownload = new Trigger(nameof (StartDataDownload));
  public static readonly Trigger StartDataUpload = new Trigger(nameof (StartDataUpload));
  public static readonly Trigger StartConfigurationSynchronization = new Trigger(nameof (StartConfigurationSynchronization));
  public static readonly Trigger StartDownloadProbeInformation = new Trigger(nameof (StartDownloadProbeInformation));
}
