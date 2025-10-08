// Decompiled with JetBrains decompiler
// Type: PathMedical.DPOAE.Viewer.WindowsForms.Configurator.ConfigurationEditorDpoaeController
// Assembly: PM.DPOAE.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DC428797-0FB7-40AC-B9BF-F1AF7BA5D90A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.InstrumentManagement;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.DPOAE.Viewer.WindowsForms.Configurator;

internal class ConfigurationEditorDpoaeController : 
  Controller<DpoaeConfigurationManager, ConfigurationEditorDpoaeMasterDetailBrowser>
{
  public ConfigurationEditorDpoaeController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = DpoaeConfigurationManager.Instance;
    this.View = new ConfigurationEditorDpoaeMasterDetailBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportAdding().NeedsAnyPermission(InstrumentManagementPermissions.ConfigureTestModules);
    machineConfigurator.SupportDeleting<DpoaePreset>().NeedsAnyPermission(InstrumentManagementPermissions.ConfigureTestModules);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(InstrumentManagementPermissions.ConfigureTestModules).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(InstrumentManagementPermissions.ConfigureTestModules)).SetGuard((IGuard) new IsOneItemAvailableGuard<DpoaePreset>((ISingleSelectionModel<DpoaePreset>) this.Model));
    machineConfigurator.SupportChangeSelection<DpoaePreset>(Triggers.ChangeSelection);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportGoBack();
    machineConfigurator.SupportGettingHelp();
  }
}
