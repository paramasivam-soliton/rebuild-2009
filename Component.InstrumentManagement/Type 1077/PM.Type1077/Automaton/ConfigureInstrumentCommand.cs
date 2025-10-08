// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Automaton.ConfigureInstrumentCommand
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Automaton.Command;
using PathMedical.InstrumentManagement;
using System;

#nullable disable
namespace PathMedical.Type1077.Automaton;

[CLSCompliant(false)]
public class ConfigureInstrumentCommand : CommandBase
{
  private readonly Type1077Manager instrumentDataManager;

  public bool EraseFlash { get; private set; }

  public ConfigureInstrumentCommand(Type1077Manager instrumentDataManager, bool eraseFlash)
  {
    this.instrumentDataManager = instrumentDataManager;
    this.EraseFlash = eraseFlash;
  }

  public override void Execute()
  {
    if (this.instrumentDataManager == null || this.TriggerEventArgs == null || !(this.TriggerEventArgs.TriggerContext is IntrumentSelectionTriggerContext))
      return;
    Type1077Instrument instrument = ((IntrumentSelectionTriggerContext) this.TriggerEventArgs.TriggerContext).Instrument;
    if (instrument.InstrumentType.HasValue && instrument.InstrumentType.Value == (ushort) 29954)
      return;
    this.instrumentDataManager.EraseFlashDataOnInstrument = this.EraseFlash;
    this.instrumentDataManager.EraseInstrumentFlash(instrument, false);
    this.instrumentDataManager.ConfigureInstrument((IInstrument) instrument);
  }
}
