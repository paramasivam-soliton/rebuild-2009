// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.HelpCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.SystemConfiguration;

#nullable disable
namespace PathMedical.Automaton.Command;

public class HelpCommand : CommandBase
{
  public HelpCommand() => this.Name = nameof (HelpCommand);

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || this.TriggerEventArgs.TriggerContext == null || !(this.TriggerEventArgs.TriggerContext is HelpRequestTriggerContext))
      return;
    HelpRequestTriggerContext triggerContext = this.TriggerEventArgs.TriggerContext as HelpRequestTriggerContext;
    SystemConfigurationManager.Instance.UserInterfaceManager.GetHelp(triggerContext.View, triggerContext.FieldId);
  }
}
