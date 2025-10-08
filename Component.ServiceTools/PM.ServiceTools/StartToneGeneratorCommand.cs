// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.StartToneGeneratorCommand
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton.Command;
using PathMedical.InstrumentManagement;
using PathMedical.Type1077;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

public class StartToneGeneratorCommand : CommandBase
{
  private Type1077Manager Model { get; set; }

  public StartToneGeneratorCommand(Type1077Manager model)
  {
    this.Name = nameof (StartToneGeneratorCommand);
    this.Model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || this.TriggerEventArgs.TriggerContext == null || !(this.TriggerEventArgs.TriggerContext is ToneGeneratorTriggerContext))
      return;
    ToneGeneratorTriggerContext triggerContext = this.TriggerEventArgs.TriggerContext as ToneGeneratorTriggerContext;
    this.Model.StartToneGenerator((IInstrument) triggerContext.Instrument, triggerContext.Speaker, triggerContext.Frequency);
  }
}
