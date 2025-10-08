// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.ProfileBrowser.ProfileBrowserController
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using PathMedical.UserProfileManagement.Command;
using PathMedical.UserProfileManagement.Viewer.WindowsForms.UserBrowser;
using System;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms.ProfileBrowser;

public class ProfileBrowserController : Controller<UserProfileManager, ProfileMasterDetailBrowser>
{
  public ProfileBrowserController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = UserProfileManager.Instance;
    this.View = new ProfileMasterDetailBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportAdding().NeedsAnyPermission(UserProfileManagementPermissions.AddAndEditProfiles);
    machineConfigurator.SupportDeleting<UserProfile>().NeedsAnyPermission(UserProfileManagementPermissions.DeleteProfiles);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(UserProfileManagementPermissions.ViewProfiles, UserProfileManagementPermissions.AddAndEditProfiles).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(UserProfileManagementPermissions.AddAndEditProfiles)).SetGuard((IGuard) new IsOneItemAvailableGuard<UserProfile>((ISingleSelectionModel<UserProfile>) this.Model));
    machineConfigurator.SupportChangeSelection<UserProfile>();
    machineConfigurator.SupportChangeSelectionSimple<AccessPermission>(UserProfileManagementTriggers.ChangeSelectedAccessPermission);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportSwitchModule(Triggers.GoBack, typeof (UserBrowserComponentModule));
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportGettingHelp();
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).SetTrigger(Triggers.RefreshDataFromForm).SetCommand((ICommand) new CreateCommandCommand((Func<ICommand>) (() => (ICommand) new ChangePermissionRightCommand(this.Model, (IView) this.View)))).ApplyTo(this.StateMachine);
  }
}
