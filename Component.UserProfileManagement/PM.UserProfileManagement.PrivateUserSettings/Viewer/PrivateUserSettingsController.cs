// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.PrivateUserSettings.Viewer.PrivateUserSettingsController
// Assembly: PM.UserProfileManagement.PrivateUserSettings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E00D5CAE-3392-4F44-903A-23A515F3DC92
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.PrivateUserSettings.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using PathMedical.UserProfileManagement.Command;

#nullable disable
namespace PathMedical.UserProfileManagement.PrivateUserSettings.Viewer;

public class PrivateUserSettingsController : Controller<UserManager, PathMedical.UserProfileManagement.PrivateUserSettings.Viewer.PrivateUserSettings>
{
  public PrivateUserSettingsController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = UserManager.Instance;
    this.View = new PathMedical.UserProfileManagement.PrivateUserSettings.Viewer.PrivateUserSettings((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportRevertModification();
    new TransitionDefinition().AddFromTo(States.Idle, States.Editing).SetTrigger(Triggers.StartModule).SetCommand((ICommand) new CommandComposition(new ICommand[5]
    {
      (ICommand) new CleanUpViewCommand((IView) this.View),
      (ICommand) new ClearUndoHistoryCommand(),
      (ICommand) new LoadUserSettingsCommand(this.Model),
      (ICommand) new RefreshModelCommand((IModel) this.Model),
      (ICommand) new ChangeViewModeCommand((IView) this.View, ViewModeType.Editing)
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Editing).SetTrigger(Triggers.Save).SetGuard((IGuard) new IsViewValidGuard((IView) this.View)).SetCommand((ICommand) new CommandComposition(new ICommand[3]
    {
      (ICommand) new CopyUIToModelCommand((IView) this.View),
      (ICommand) new ChangePrivateSettingCommand(this.Model),
      (ICommand) new SetSavedCommand()
    })).ApplyTo(this.StateMachine);
    machineConfigurator.SupportGettingHelp();
  }
}
