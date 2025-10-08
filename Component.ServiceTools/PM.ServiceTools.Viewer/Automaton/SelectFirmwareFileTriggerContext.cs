// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.Automaton.SelectFirmwareFileTriggerContext
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using PathMedical.Automaton;
using PathMedical.Type1077;
using PathMedical.Type1077.Firmware;
using System;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.Automaton;

[CLSCompliant(false)]
public class SelectFirmwareFileTriggerContext : TriggerContext
{
  public Type1077Instrument Instrument { get; protected set; }

  public FirmwareImage Firmware { get; protected set; }

  public SelectFirmwareFileTriggerContext(Type1077Instrument instrument)
  {
    this.Instrument = instrument;
  }
}
