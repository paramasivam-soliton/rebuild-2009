// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.DeleteDeviceMemoryTriggerContext
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton;
using PathMedical.Type1077;
using System;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

[CLSCompliant(false)]
public class DeleteDeviceMemoryTriggerContext : TriggerContext
{
  public Type1077Instrument Instrument { get; protected set; }

  public DeleteDeviceMemoryTriggerContext(Type1077Instrument instrument)
  {
    this.Instrument = instrument;
  }
}
