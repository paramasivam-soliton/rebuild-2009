// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.CommandComposition
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Automaton.Command;

public class CommandComposition : CommandBase
{
  public List<ICommand> Commands { get; protected set; }

  public CommandComposition(params ICommand[] commands)
  {
    this.Commands = commands != null ? new List<ICommand>((IEnumerable<ICommand>) commands) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (commands));
    this.Name = $"CommandComposition ({string.Join(", ", this.Commands.Select<ICommand, string>((Func<ICommand, string>) (c => c.Name)).ToArray<string>())})";
  }

  public override TriggerEventArgs TriggerEventArgs
  {
    get => base.TriggerEventArgs;
    set
    {
      base.TriggerEventArgs = value;
      foreach (ICommand command in this.Commands)
        command.TriggerEventArgs = value;
    }
  }

  public override void Execute()
  {
    foreach (ICommand command in this.Commands)
      command.Execute();
  }

  public override string ToString() => this.Name;
}
