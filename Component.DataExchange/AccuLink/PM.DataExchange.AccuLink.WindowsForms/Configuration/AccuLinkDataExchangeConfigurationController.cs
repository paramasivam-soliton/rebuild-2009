// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.AccuLink.WindowsForms.Configuration.AccuLinkDataExchangeConfigurationController
// Assembly: PM.DataExchange.AccuLink.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7B9FC5AD-6EE7-4FA1-8083-412FCFB9EB4F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.AccuLink.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Transition;
using PathMedical.SystemConfiguration.Core;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.DataExchange.AccuLink.WindowsForms.Configuration;

internal class AccuLinkDataExchangeConfigurationController : 
  Controller<AccuLinkDataExchangeConfigurationManager, AccuLinkDataExchangeConfigurationEditor>
{
  public AccuLinkDataExchangeConfigurationController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = AccuLinkDataExchangeConfigurationManager.Instance;
    this.View = new AccuLinkDataExchangeConfigurationEditor((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(SystemConfigurationPermissions.ViewConfiguration, SystemConfigurationPermissions.EditConfiguration).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(SystemConfigurationPermissions.EditConfiguration));
    machineConfigurator.SupportGoBack();
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportGettingHelp();
  }
}
