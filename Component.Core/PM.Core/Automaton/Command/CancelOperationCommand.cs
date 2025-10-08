// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.CancelOperationCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class CancelOperationCommand : CommandBase
{
  private readonly IUndoableCommand commandToUndo;

  public CancelOperationCommand() => this.Name = nameof (CancelOperationCommand);

  public CancelOperationCommand(IUndoableCommand commandToUndo)
    : this()
  {
    this.commandToUndo = commandToUndo != null ? commandToUndo : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (commandToUndo));
  }

  public override void Execute()
  {
    this.TriggerEventArgs.Cancel = true;
    if (this.commandToUndo == null)
      return;
    this.commandToUndo.Undo();
  }
}
