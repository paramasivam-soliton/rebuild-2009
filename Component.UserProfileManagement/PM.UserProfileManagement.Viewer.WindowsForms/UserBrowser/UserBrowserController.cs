// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.UserBrowser.UserBrowserController
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.InstrumentManagement;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using PathMedical.UserProfileManagement.Automaton;
using PathMedical.UserProfileManagement.Viewer.WindowsForms.ProfileBrowser;
using System;
using System.Linq;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms.UserBrowser;

public class UserBrowserController : Controller<UserManager, UserAccountMasterDetailBrowser>
{
  public UserBrowserController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = UserManager.Instance;
    this.View = new UserAccountMasterDetailBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportAdding().NeedsAnyPermission(this.Model.MaintainUsersAccessPermissionId);
    machineConfigurator.SupportDeleting<User>().NeedsAnyPermission(this.Model.DeleteUserAccountPermissionId);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(UserProfileManagementPermissions.ViewUsers, UserProfileManagementPermissions.AddAndEditUsers).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(UserProfileManagementPermissions.AddAndEditUsers)).SetGuard((IGuard) new IsOneItemAvailableGuard<User>((ISingleSelectionModel<User>) this.Model));
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(UserProfileManagementTriggers.UnlockUserAccount).SetGuard((IGuard) new IsOneItemSelectedGuard<User>((ISingleSelectionModel<User>) this.Model)).NeedsAnyPermission(UserManager.Instance.UnlockUserAccountPermissionId).SetCommand((ICommand) new UnlockUserAccountCommand(this.Model)).ApplyTo(this.StateMachine);
    machineConfigurator.SupportChangeSelection<User>();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportSwitchModule(new SwitchModuleOptions(UserProfileManagementTriggers.SwitchToProfileBrowser, typeof (ProfileBrowserComponentModule)).NeedsAnyPermission(UserProfileManagementPermissions.ViewProfiles, UserProfileManagementPermissions.AddAndEditProfiles));
    machineConfigurator.SupportGettingHelp();
    machineConfigurator.SupportSuspending();
    foreach (IInstrumentPlugin instrumentPlugin in SystemConfigurationManager.Instance.Plugins.OfType<IInstrumentPlugin>().Where<IInstrumentPlugin>((Func<IInstrumentPlugin, bool>) (p => p.ConfigurationSynchronizationModuleType != (Type) null)).ToArray<IInstrumentPlugin>())
    {
      if (instrumentPlugin.ConfigurationSynchronizationModuleType != (Type) null)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin.ConfigurationSynchronizationModuleType);
        machineConfigurator.SupportStartAssistant(new Trigger(applicationComponentModule.Id.ToString()), instrumentPlugin.ConfigurationSynchronizationModuleType);
      }
    }
  }
}
