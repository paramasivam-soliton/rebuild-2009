// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Command.AddCommentCommand
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.PatientManagement.CommentManagement;
using System;

#nullable disable
namespace PathMedical.PatientManagement.Command;

public class AddCommentCommand : CommandBase, IUndoableCommand, ICommand
{
  private readonly PatientManager model;
  private object comment;

  public AddCommentCommand(PatientManager model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (AddCommentCommand);
    this.model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || !(this.TriggerEventArgs.TriggerContext is CommentTriggerContext))
      return;
    CommentTriggerContext triggerContext = this.TriggerEventArgs.TriggerContext as CommentTriggerContext;
    if (triggerContext.Comment is string)
    {
      FreeTextComment freeTextComment = new FreeTextComment();
      freeTextComment.Text = triggerContext.Comment as string;
      this.comment = (object) freeTextComment;
      this.model.AddComment(this.comment as FreeTextComment);
    }
    if (!(triggerContext.Comment is PredefinedComment))
      return;
    this.comment = ((PredefinedComment) triggerContext.Comment).Clone();
    this.model.AddPredefinedComment(this.comment as PredefinedComment);
  }

  public void Undo()
  {
    if (this.comment == null)
      return;
    if (this.comment is FreeTextComment)
      this.model.DeleteComment(this.comment as FreeTextComment);
    if (!(this.comment is PredefinedComment))
      return;
    this.model.DeletePredefinedComment(this.comment as PredefinedComment);
  }
}
