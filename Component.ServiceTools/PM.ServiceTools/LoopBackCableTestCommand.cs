// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.LoopBackCableTestCommand
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton.Command;
using PathMedical.SystemConfiguration;
using PathMedical.Type1077;
using PathMedical.UserInterface.ModelViewController;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

public class LoopBackCableTestCommand : CommandBase
{
  private Type1077Manager type1077Manager;

  private Type1077Manager Model { get; set; }

  public LoopBackCableTestCommand(Type1077Manager model)
  {
    this.Name = nameof (LoopBackCableTestCommand);
    this.Model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || this.TriggerEventArgs.TriggerContext == null || !(this.TriggerEventArgs.TriggerContext is LoopBackCableTriggerContext))
      return;
    LoopBackCableTriggerContext triggerContext = this.TriggerEventArgs.TriggerContext as LoopBackCableTriggerContext;
    this.Model.LoopBackCableTest(triggerContext.Instrument, triggerContext.LoopBackCableInformation);
    int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("Loopback Cable test passed!\nService date reset successful.", "Send Probe Configuration", AnswerOptionType.OK, QuestionIcon.Information);
  }
}
