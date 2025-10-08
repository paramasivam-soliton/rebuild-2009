// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PredefinedCommentManagement.PredefinedCommentMaintenanceController
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.PatientManagement.Command;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PredefinedCommentManagement;

public class PredefinedCommentMaintenanceController : 
  Controller<CommentManager, PredefinedCommentMasterDetailBrowser>
{
  public PredefinedCommentMaintenanceController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = CommentManager.Instance;
    this.View = new PredefinedCommentMasterDetailBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportAdding().NeedsAnyPermission(PatientManagementPermissions.MaintainComments);
    machineConfigurator.SupportDeleting<PredefinedComment>().NeedsAnyPermission(PatientManagementPermissions.MaintainComments);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(PatientManagementPermissions.MaintainComments).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(PatientManagementPermissions.MaintainComments)).SetGuard((IGuard) new IsOneItemAvailableGuard<PredefinedComment>((ISingleSelectionModel<PredefinedComment>) this.Model));
    machineConfigurator.SupportChangeSelection<PredefinedComment>();
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportGoBack();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportGettingHelp();
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).SetTrigger(Triggers.ChangeLanguage).SetCommand((ICommand) new CommandComposition(new ICommand[2]
    {
      (ICommand) new CopyUIToModelCommand((IView) this.View),
      (ICommand) new ChangeTranslationCommand((ISupportInternationalization) this.Model, (IView) this.View)
    })).ApplyTo(this.StateMachine);
  }
}
