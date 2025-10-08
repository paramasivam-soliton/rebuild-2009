// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.CommandManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Automaton.Command;

public class CommandManager
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (CommandManager), "$Rev: 1480 $");
  private bool IsModificationMessageDisplayed;
  protected readonly Stack<IUndoableCommand> UndoableCommands;
  private int lastSaveStackCount = -1;

  public static CommandManager Instance => PathMedical.Singleton.Singleton<CommandManager>.Instance;

  private CommandManager()
  {
    this.UndoableCommands = new Stack<IUndoableCommand>();
    this.IsUndoEnabled = true;
    this.IsRevertEnabled = true;
  }

  public event EventHandler OperationPerformed;

  public bool CanUndo => this.UndoableCommands.Count > 0 && this.IsUndoEnabled;

  public bool IsUndoEnabled { get; set; }

  public bool IsRevertEnabled { get; set; }

  public void Add(ICommand command)
  {
    if (command == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (command));
    if (!this.IsUndoEnabled)
      return;
    if (command is IUndoableCommand undoableCommand)
    {
      this.UndoableCommands.Push(undoableCommand);
      if (this.lastSaveStackCount >= this.UndoableCommands.Count)
        this.lastSaveStackCount = -1;
      CommandManager.Logger.Info("Added command {0} to Undo stack ({1})", (object) command, (object) this.UndoableCommands.Count);
    }
    this.OnOperationPerformed();
  }

  public void Execute(ICommand command)
  {
    if (command == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (command));
    try
    {
      command.Execute();
      this.Add(command);
    }
    catch (System.Exception ex)
    {
      CommandManager.Logger.Error(ex, "Failure while executing command {0}", (object) command.Name);
      throw;
    }
  }

  public void Undo()
  {
    if (!this.CanUndo)
      return;
    this.IsUndoEnabled = false;
    IUndoableCommand undoableCommand = this.UndoableCommands.Pop();
    undoableCommand.Undo();
    CommandManager.Logger.Info("Removed command {0} from Undo stack ({1})", (object) undoableCommand, (object) this.UndoableCommands.Count);
    this.IsUndoEnabled = true;
    this.OnOperationPerformed();
  }

  public void Reset()
  {
    this.UndoableCommands.Clear();
    this.SetSaved();
  }

  public void SetSaved()
  {
    this.lastSaveStackCount = this.UndoableCommands.Count;
    this.IsModificationMessageDisplayed = false;
    this.OnOperationPerformed();
  }

  public bool IsUnsaved
  {
    get
    {
      IEnumerable<IUndoableCommand> source = this.UndoableCommands.Skip<IUndoableCommand>(this.lastSaveStackCount);
      if (source.Count<IUndoableCommand>() == 0)
        return false;
      if (!this.IsModificationMessageDisplayed && SystemConfigurationManager.Instance.InformUserAfterBeginChangingData == DataModificationWarning.Yes)
      {
        int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("You have modified data. Please take care. \nUse the undo management, if you wish to revert changes.", "Data Modification Warning", AnswerOptionType.OK, QuestionIcon.Information);
        this.IsModificationMessageDisplayed = true;
      }
      List<ISupportUndo> list = source.Select<IUndoableCommand, object>((Func<IUndoableCommand, object>) (cmd => cmd.Owner)).OfType<ISupportUndo>().Where<ISupportUndo>((Func<ISupportUndo, bool>) (c => c.IsNavigationOnly)).ToList<ISupportUndo>();
      return source.Count<IUndoableCommand>() != list.Count<ISupportUndo>();
    }
  }

  private void OnOperationPerformed()
  {
    if (this.OperationPerformed == null)
      return;
    this.OperationPerformed((object) this, EventArgs.Empty);
  }
}
