// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Automaton.IntrumentSelectionTriggerContext
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Automaton;
using System;

#nullable disable
namespace PathMedical.Type1077.Automaton;

[CLSCompliant(false)]
public class IntrumentSelectionTriggerContext : TriggerContext
{
  public Type1077Instrument Instrument { get; protected set; }

  public IntrumentSelectionTriggerContext(Type1077Instrument instrument)
  {
    this.Instrument = instrument;
  }
}
