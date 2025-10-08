// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.IUserInterfaceManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.UserInterface;

public interface IUserInterfaceManager
{
  void AddApplicationComponent(IApplicationComponent applicationComponent);

  void RemoveApplicationComponent(IApplicationComponent applicationComponent);

  event EventHandler<ApplicationComponentChangingEventArgs> ApplicationComponentChanging;

  event EventHandler<EventArgs> ApplicationComponentChanged;

  bool GoBackToLastApplicationComponentModule();

  bool ChangeApplicationComponentModule(IApplicationComponentModule module, Trigger trigger);

  bool ChangeApplicationComponentModule(
    IApplicationComponentModule module,
    Trigger additionalModuleStartTrigger,
    Guid applicationComponentId);

  event EventHandler<ApplicationComponentModuleChangingEventArgs> ApplicationComponentModuleChanging;

  event EventHandler<EventArgs> ApplicationComponentModuleChanged;

  IEnumerable<IApplicationComponent> RegisteredApplicationComponents { get; }

  void ToggleToolbarElement(Trigger trigger, bool active);

  void StartAssistant(IApplicationComponentModule module, Trigger trigger);

  string HelpUrl { get; set; }

  void GetHelp(IView view, string helpContext);

  void SetFormSubmissionControl(object control);

  AnswerType ShowMessageBox(
    string text,
    string caption,
    AnswerOptionType possibleAnswerOptionTypes,
    QuestionIcon questionIcon);
}
