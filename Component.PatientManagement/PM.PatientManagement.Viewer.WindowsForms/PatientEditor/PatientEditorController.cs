// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.PatientEditorController
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.PatientManagement.Command;
using PathMedical.PatientManagement.RiskIndicatorManagement;
using PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.Panels;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using System;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor;

public class PatientEditorController : Controller<PatientManager, PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.PatientEditor>
{
  private readonly PatientDataEditor patientDataEditor;
  private readonly PatientRiskIndicatorEditor patientRiskIndicatorEditor;
  private readonly PatientCommentEditor patientCommentEditor;

  public PatientEditorController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = PatientManager.Instance;
    this.patientDataEditor = new PatientDataEditor((IModel) this.Model);
    this.patientRiskIndicatorEditor = new PatientRiskIndicatorEditor((IModel) this.Model);
    this.patientCommentEditor = new PatientCommentEditor((IModel) this.Model);
    this.View = new PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.PatientEditor((IModel) this.Model, this.patientDataEditor, this.patientRiskIndicatorEditor, this.patientCommentEditor);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportChangeSelectionSimple<Patient>();
    machineConfigurator.SupportChangeSelectionSimple<RiskIndicator>(PatientManagementTriggers.SelectRiskFactor);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportRevertModification();
    IsViewValidGuard isViewValidGuard1 = new IsViewValidGuard((IView) this.View);
    IsNotGuard isViewInvalidGuard = new IsNotGuard((IGuard) isViewValidGuard1);
    IsFuncTrueGuard isFuncTrueGuard = new IsFuncTrueGuard((Func<bool>) (() => this.View.ValidateEditorMandatoryGroups()));
    GuardComposition isViewValidGuard2 = new GuardComposition(new IGuard[2]
    {
      (IGuard) isViewValidGuard1,
      (IGuard) isFuncTrueGuard
    });
    GuardComposition transitionGuard = new GuardComposition(new IGuard[2]
    {
      (IGuard) isViewValidGuard1,
      (IGuard) new IsNotGuard((IGuard) isFuncTrueGuard)
    });
    machineConfigurator.SupportValidationAndSaving((IGuard) isViewValidGuard2, (IGuard) isViewInvalidGuard);
    new TransitionDefinition().AddFromTo(States.Editing).AddFromTo(States.Adding).SetTrigger(Triggers.Save).SetGuard((IGuard) transitionGuard).SetCommand((ICommand) new CreateCommandCommand((Func<ICommand>) (() => (ICommand) new DisplayMessageCommand((IView) this.View, this.View.EditorMandatoryGroupsValidationMessage)))).ApplyTo(this.StateMachine);
    machineConfigurator.SupportGoBack();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(PatientManagementPermissions.ViewPatients, PatientManagementPermissions.AddAndEditPatients).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(PatientManagementPermissions.AddAndEditPatients)).SetGuard((IGuard) new IsOneItemAvailableGuard<Patient>((IMultiSelectionModel<Patient>) this.Model));
    new TransitionDefinition().AddFromTo(States.Editing, States.Adding).SetTrigger(Triggers.Add).SetCommand((ICommand) new ChangeViewModeCommand((IView) this.View, ViewModeType.Adding)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.AssignUnknownRiskToNoRisk).SetCommand((ICommand) new CreateCommandCommand((Func<ICommand>) (() => (ICommand) new SetUnknownRiskIndicatorsToNoCommand(this.Model)))).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).SetTrigger(Triggers.RefreshDataFromForm).SetCommand((ICommand) new CreateCommandCommand((Func<ICommand>) (() => (ICommand) new ChangeRiskIndicatorAssignmentCommand(this.Model, (IView) this.patientRiskIndicatorEditor)))).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.AddComment).SetCommand((ICommand) new AddCommentCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.DeleteComment).SetCommand((ICommand) new DeleteCommentCommand(this.Model)).ApplyTo(this.StateMachine);
    machineConfigurator.SupportGettingHelp();
  }
}
