// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.SendProbeConfigurationCommand
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton.Command;
using PathMedical.SystemConfiguration;
using PathMedical.Type1077;
using PathMedical.UserInterface.ModelViewController;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

public class SendProbeConfigurationCommand : CommandBase
{
  private Type1077Manager Model { get; set; }

  public SendProbeConfigurationCommand(Type1077Manager model)
  {
    this.Name = nameof (SendProbeConfigurationCommand);
    this.Model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || this.TriggerEventArgs.TriggerContext == null || !(this.TriggerEventArgs.TriggerContext is SendProbeConfigurationTriggerContext))
      return;
    SendProbeConfigurationTriggerContext triggerContext = this.TriggerEventArgs.TriggerContext as SendProbeConfigurationTriggerContext;
    this.Model.SendProbeConfiguration(triggerContext.Instrument, triggerContext.Probe);
    int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("Sending probe configuration finished", "Send Probe Configuration", AnswerOptionType.OK, QuestionIcon.Information);
  }
}
