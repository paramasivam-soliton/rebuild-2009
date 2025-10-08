// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.ActionCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class ActionCommand : CommandBase, IUndoableCommand, ICommand
{
  private readonly Action executeAction;
  private readonly Action undoAction;

  public ActionCommand(Action executeAction)
  {
    if (executeAction == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (executeAction));
    this.Name = $"ActionCommand {executeAction.Method.Name}";
    this.executeAction = executeAction;
  }

  public ActionCommand(Action executeAction, Action undoAction)
    : this(executeAction)
  {
    this.undoAction = undoAction != null ? undoAction : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (undoAction));
  }

  public override void Execute()
  {
    if (this.executeAction == null)
      return;
    this.executeAction();
  }

  public void Undo()
  {
    if (this.undoAction == null)
      return;
    this.undoAction();
  }
}
