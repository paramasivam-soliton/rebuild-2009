// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.CreateCommandCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class CreateCommandCommand : CommandBase
{
  private readonly Func<ICommand> createCommandFunction;

  public CreateCommandCommand(Func<ICommand> createCommandFunction)
  {
    this.createCommandFunction = createCommandFunction != null ? createCommandFunction : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (createCommandFunction));
    this.Name = nameof (CreateCommandCommand);
  }

  public override void Execute()
  {
    ICommand command = this.createCommandFunction();
    command.TriggerEventArgs = this.TriggerEventArgs;
    CommandManager.Instance.Execute(command);
  }
}
