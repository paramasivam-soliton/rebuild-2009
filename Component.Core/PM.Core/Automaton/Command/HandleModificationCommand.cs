// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.HandleModificationCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Guard;
using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class HandleModificationCommand : CommandBase
{
  private readonly IView view;

  public HandleModificationCommand(IView view)
  {
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.Name = "HandleChangeCommand";
    this.view = view;
    this.StoreSelectedGuard = new IsModificationHandlingChosenGuard(this, HandleModificationType.Store);
    this.AbortOperationGuard = new IsModificationHandlingChosenGuard(this, HandleModificationType.Abort);
    this.DiscardModificationsGuard = new IsModificationHandlingChosenGuard(this, HandleModificationType.Discard);
  }

  public HandleModificationType HandleModificationType { get; private set; }

  public IsModificationHandlingChosenGuard StoreSelectedGuard { get; private set; }

  public IsModificationHandlingChosenGuard DiscardModificationsGuard { get; private set; }

  public IsModificationHandlingChosenGuard AbortOperationGuard { get; private set; }

  public override void Execute()
  {
    this.HandleModificationType = HandleModificationType.Abort;
    if (this.view == null)
      return;
    switch (this.view.DisplayQuestion(ComponentResourceManagement.Instance.ResourceManager.GetString("StoreUnsavedChanges"), AnswerOptionType.YesNoCancel, QuestionIcon.Question))
    {
      case AnswerType.Yes:
        this.HandleModificationType = HandleModificationType.Store;
        break;
      case AnswerType.No:
        this.HandleModificationType = HandleModificationType.Discard;
        break;
    }
  }
}
