// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.StartAssistantCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class StartAssistantCommand : CommandBase
{
  private readonly Type newModuleType;
  private readonly Trigger trigger;

  public StartAssistantCommand(Type moduleType)
    : this(moduleType, (Trigger) null)
  {
  }

  public StartAssistantCommand(Type moduleType, Trigger trigger)
  {
    this.Name = nameof (StartAssistantCommand);
    this.newModuleType = moduleType;
    this.trigger = trigger;
  }

  public override void Execute()
  {
    SystemConfigurationManager.Instance.UserInterfaceManager.StartAssistant(ApplicationComponentModuleManager.Instance.Get(this.newModuleType), this.trigger);
  }
}
