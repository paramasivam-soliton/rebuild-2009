// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.UndoableCommandComposition
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Automaton.Command;

public class UndoableCommandComposition : CommandBase, IUndoableCommand, ICommand
{
  private readonly List<IUndoableCommand> undoableCommands;

  public UndoableCommandComposition(params IUndoableCommand[] undoableCommands)
  {
    this.undoableCommands = undoableCommands != null ? new List<IUndoableCommand>((IEnumerable<IUndoableCommand>) undoableCommands) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (undoableCommands));
    this.Name = $"UndoableCommandComposition ({string.Join(", ", this.undoableCommands.Select<IUndoableCommand, string>((Func<IUndoableCommand, string>) (c => c.Name)).ToArray<string>())})";
  }

  public override void Execute()
  {
    foreach (ICommand undoableCommand in this.undoableCommands)
      undoableCommand.Execute();
  }

  public override TriggerEventArgs TriggerEventArgs
  {
    get => base.TriggerEventArgs;
    set
    {
      base.TriggerEventArgs = value;
      foreach (ICommand undoableCommand in this.undoableCommands)
        undoableCommand.TriggerEventArgs = value;
    }
  }

  public void Undo()
  {
    foreach (IUndoableCommand undoableCommand in this.undoableCommands)
      undoableCommand.Undo();
  }
}
