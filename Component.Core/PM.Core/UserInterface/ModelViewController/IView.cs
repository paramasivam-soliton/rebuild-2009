// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ModelViewController.IView
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.UserInterface.ModelViewController;

public interface IView : IDisposable, ISupportControllerAction, ISupportUserInterfaceManager
{
  void RegisterSubView(IView subView);

  bool IsFormDataValid { get; }

  AnswerType DisplayQuestion(
    string questionText,
    AnswerOptionType possibleAnswerOptionTypes,
    QuestionIcon questionIcon);

  void DisplayMessage(string messageText);

  void DisplayError(string errorText);

  void CopyUIToModel();

  void CleanUpView();

  ViewModeType ViewMode { get; set; }
}
