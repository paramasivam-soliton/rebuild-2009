// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.LoopBackCableTriggerContext
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton;
using PathMedical.InstrumentManagement;
using PathMedical.Type1077;
using System;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

[CLSCompliant(false)]
public class LoopBackCableTriggerContext : TriggerContext
{
  public Instrument Instrument { get; protected set; }

  public Type1077LoopBackCableInformation LoopBackCableInformation { get; protected set; }

  public LoopBackCableTriggerContext(
    Instrument instrument,
    Type1077LoopBackCableInformation loopBackCableInformation)
  {
    this.Instrument = instrument;
    this.LoopBackCableInformation = loopBackCableInformation;
  }
}
