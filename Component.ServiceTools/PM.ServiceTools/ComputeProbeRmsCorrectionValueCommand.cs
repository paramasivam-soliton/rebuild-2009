// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.ComputeProbeRmsCorrectionValueCommand
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton.Command;
using PathMedical.Type1077;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

public class ComputeProbeRmsCorrectionValueCommand : CommandBase
{
  private Type1077Manager Model { get; set; }

  public ComputeProbeRmsCorrectionValueCommand(Type1077Manager model)
  {
    this.Name = nameof (ComputeProbeRmsCorrectionValueCommand);
    this.Model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || this.TriggerEventArgs.TriggerContext == null || !(this.TriggerEventArgs.TriggerContext is ComputeProbeRmsCorrectionTriggerContext))
      return;
    ComputeProbeRmsCorrectionTriggerContext triggerContext = this.TriggerEventArgs.TriggerContext as ComputeProbeRmsCorrectionTriggerContext;
    this.Model.ComputeMicrofoneCorrectionValue(triggerContext.Instrument, triggerContext.Probe, triggerContext.Frequency, triggerContext.FrequencySpeakerCorrectionValue);
  }
}
