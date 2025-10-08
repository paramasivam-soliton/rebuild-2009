// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.ChangeValueCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.Controls;

#nullable disable
namespace PathMedical.Automaton.Command;

public class ChangeValueCommand : CommandBase, IUndoableCommand, ICommand
{
  private object control;
  private object oldValue;
  private IPerformUndo undoPerformer;

  public ChangeValueCommand() => this.Name = nameof (ChangeValueCommand);

  public override void Execute()
  {
    ValueChangeTriggerContext triggerContext = (ValueChangeTriggerContext) this.TriggerEventArgs.TriggerContext;
    this.oldValue = triggerContext.OldValue;
    this.control = triggerContext.Control;
    this.undoPerformer = triggerContext.UndoPerformer;
  }

  public void Undo()
  {
    using (new UndoScope(this.control))
      this.undoPerformer.RestoreValue(this.control, this.oldValue);
  }
}
