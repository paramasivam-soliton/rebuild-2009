// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.Viewer.WindowsForms.Configurator.ConfigurationEditorAbrController
// Assembly: PM.ABR.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EEE17F79-1FE4-481B-886E-5CF81EC38810
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.InstrumentManagement;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.ABR.Viewer.WindowsForms.Configurator;

internal class ConfigurationEditorAbrController : 
  Controller<AbrConfigurationManager, ConfigurationEditorAbrMasterDetailBrowser>
{
  public ConfigurationEditorAbrController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = AbrConfigurationManager.Instance;
    this.View = new ConfigurationEditorAbrMasterDetailBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportAdding().NeedsAnyPermission(InstrumentManagementPermissions.ConfigureTestModules);
    machineConfigurator.SupportDeleting<AbrPreset>().NeedsAnyPermission(InstrumentManagementPermissions.ConfigureTestModules);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(InstrumentManagementPermissions.ConfigureTestModules).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(InstrumentManagementPermissions.ConfigureTestModules)).SetGuard((IGuard) new IsOneItemAvailableGuard<AbrPreset>((ISingleSelectionModel<AbrPreset>) this.Model));
    machineConfigurator.SupportChangeSelection<AbrPreset>();
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportGoBack();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportGettingHelp();
  }
}
