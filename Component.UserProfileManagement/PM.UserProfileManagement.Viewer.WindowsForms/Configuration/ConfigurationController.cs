// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Viewer.WindowsForms.Configuration.ConfigurationController
// Assembly: PM.UserProfileManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E71651CF-B5A7-49A8-997B-808567940DCC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Transition;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using System;

#nullable disable
namespace PathMedical.UserProfileManagement.Viewer.WindowsForms.Configuration;

public class ConfigurationController : Controller<ConfigurationManager, ConfigurationEditor>
{
  private readonly Type parentModuleType;

  public ConfigurationController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = ConfigurationManager.Instance;
    this.View = new ConfigurationEditor((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(UserProfileManagementPermissions.ConfigureUserProfileManagement).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(UserProfileManagementPermissions.ConfigureUserProfileManagement));
    machineConfigurator.SupportGoBack();
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportGettingHelp();
  }
}
