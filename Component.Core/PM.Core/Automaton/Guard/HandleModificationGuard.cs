// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.HandleModificationGuard
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class HandleModificationGuard : GuardBase
{
  private readonly ICommand discardCommand;
  private readonly ICommand saveCommand;
  private readonly IView view;
  private int handledTriggerRequestId;
  private bool shallProceed;

  public HandleModificationGuard(IView view, ICommand saveCommand)
  {
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.saveCommand = saveCommand != null ? saveCommand : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (saveCommand));
    this.view = view;
    this.Name = this.GetType().Name;
  }

  public HandleModificationGuard(IView view, ICommand saveCommand, ICommand discardCommand)
    : this(view, saveCommand)
  {
    this.discardCommand = discardCommand != null ? discardCommand : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (discardCommand));
  }

  public override bool Execute(TriggerEventArgs e)
  {
    try
    {
      if (this.handledTriggerRequestId != TriggerRequestScope.CurrentId)
        this.DetermineProceeding();
      if (!this.shallProceed)
        e.Cancel = true;
      return this.shallProceed;
    }
    finally
    {
      this.handledTriggerRequestId = TriggerRequestScope.CurrentId;
    }
  }

  private void DetermineProceeding()
  {
    if (!CommandManager.Instance.IsUnsaved && this.view.ViewMode == ViewModeType.Adding)
    {
      if (this.discardCommand != null)
        CommandManager.Instance.Execute(this.discardCommand);
      this.shallProceed = true;
    }
    if (!CommandManager.Instance.IsUnsaved)
    {
      this.shallProceed = true;
    }
    else
    {
      switch (this.view.DisplayQuestion(ComponentResourceManagement.Instance.ResourceManager.GetString("StoreUnsavedChanges"), AnswerOptionType.YesNoCancel, QuestionIcon.Question))
      {
        case AnswerType.Cancel:
          this.shallProceed = false;
          break;
        case AnswerType.Yes:
          if (this.view.IsFormDataValid)
          {
            CommandManager.Instance.Execute(this.saveCommand);
            this.shallProceed = true;
            break;
          }
          this.view.DisplayMessage(ComponentResourceManagement.Instance.ResourceManager.GetString("DataNotValidCorrectFirst"));
          this.shallProceed = false;
          break;
        case AnswerType.No:
          if (this.discardCommand != null)
            CommandManager.Instance.Execute(this.discardCommand);
          this.shallProceed = true;
          break;
      }
    }
  }
}
