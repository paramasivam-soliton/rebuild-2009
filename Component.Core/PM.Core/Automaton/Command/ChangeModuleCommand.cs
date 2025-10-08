// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.ChangeModuleCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class ChangeModuleCommand : CommandBase
{
  private readonly Type hostingAppComponentType;
  private readonly Type newModuleType;
  private readonly Trigger trigger;

  public ChangeModuleCommand(Type module)
  {
    this.Name = nameof (ChangeModuleCommand);
    this.newModuleType = module;
  }

  public ChangeModuleCommand(Type module, Trigger trigger)
    : this(module)
  {
    this.trigger = trigger;
  }

  public ChangeModuleCommand(Type module, Trigger trigger, Type hostingAppComponentType)
    : this(module, trigger)
  {
    this.hostingAppComponentType = hostingAppComponentType;
  }

  public override void Execute()
  {
    SystemConfigurationManager.Instance.UserInterfaceManager.ChangeApplicationComponentModule(ApplicationComponentModuleManager.Instance.Get(this.newModuleType), this.trigger);
  }
}
