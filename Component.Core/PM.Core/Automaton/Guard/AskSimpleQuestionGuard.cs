// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.AskSimpleQuestionGuard
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class AskSimpleQuestionGuard : GuardBase
{
  private readonly AnswerType neededAnswerType;
  private readonly AnswerOptionType possibleAnswerOptionTypes;
  private readonly string questionText;
  private readonly IView view;

  public AskSimpleQuestionGuard(
    IView view,
    string questionText,
    AnswerOptionType possibleAnswerOptionTypes,
    AnswerType neededAnswerType)
  {
    this.view = view != null ? view : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.questionText = questionText;
    this.possibleAnswerOptionTypes = possibleAnswerOptionTypes;
    this.neededAnswerType = neededAnswerType;
    this.Name = $"AskSimpleQuestionGuard [{this.questionText}] [{this.possibleAnswerOptionTypes}] [{this.neededAnswerType}]";
  }

  public override bool Execute(TriggerEventArgs e)
  {
    return this.view.DisplayQuestion(this.questionText, this.possibleAnswerOptionTypes, QuestionIcon.Question) == this.neededAnswerType;
  }
}
