// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Connector.WindowsForms.Configuration.EspConfigurationController
// Assembly: PM.DataExchange.eSP.Connector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1934FEB7-E9C0-480F-B9F2-DEDB47DB36DA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.Connector.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Transition;
using PathMedical.SystemConfiguration.Core;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.DataExchange.eSP.Connector.WindowsForms.Configuration;

public class EspConfigurationController : Controller<EspConfigurationManager, EspConfigurationEditor>
{
  public EspConfigurationController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = EspConfigurationManager.Instance;
    this.View = new EspConfigurationEditor((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportGoBack();
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(SystemConfigurationPermissions.ViewConfiguration, SystemConfigurationPermissions.EditConfiguration).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(SystemConfigurationPermissions.EditConfiguration));
    machineConfigurator.SupportGettingHelp();
  }
}
