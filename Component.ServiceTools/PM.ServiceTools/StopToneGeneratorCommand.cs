// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.StopToneGeneratorCommand
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton.Command;
using PathMedical.Type1077;
using PathMedical.Type1077.Automaton;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

public class StopToneGeneratorCommand : CommandBase
{
  private Type1077Manager Model { get; set; }

  public StopToneGeneratorCommand(Type1077Manager model)
  {
    this.Name = nameof (StopToneGeneratorCommand);
    this.Model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || this.TriggerEventArgs.TriggerContext == null || !(this.TriggerEventArgs.TriggerContext is IntrumentSelectionTriggerContext))
      return;
    this.Model.StopToneGenerator((this.TriggerEventArgs.TriggerContext as IntrumentSelectionTriggerContext).Instrument);
  }
}
