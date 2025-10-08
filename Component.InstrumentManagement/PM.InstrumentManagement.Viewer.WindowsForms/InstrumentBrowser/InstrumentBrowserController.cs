// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.Viewer.WindowsForms.InstrumentBrowser.InstrumentBrowserController
// Assembly: PM.InstrumentManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 377C3581-C34D-4673-97B7-CC091DEDB55A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.InstrumentManagement.Automaton;
using PathMedical.Plugin;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using PathMedical.UserProfileManagement;
using System;
using System.Linq;

#nullable disable
namespace PathMedical.InstrumentManagement.Viewer.WindowsForms.InstrumentBrowser;

public class InstrumentBrowserController : 
  Controller<InstrumentManager, InstrumentMasterDetailBrowser>
{
  private StateMachineConfigurator stateMachineConfigurator;

  public InstrumentBrowserController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = InstrumentManager.Instance;
    this.View = new InstrumentMasterDetailBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    this.stateMachineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    this.stateMachineConfigurator.SupportAdding().NeedsAnyPermission(InstrumentManagementPermissions.AddAndEditInstruments);
    this.stateMachineConfigurator.SupportDeleting<Instrument>().NeedsAnyPermission(InstrumentManagementPermissions.DeleteInstruments);
    this.stateMachineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(InstrumentManagementPermissions.ViewInstruments, InstrumentManagementPermissions.AddAndEditInstruments).PreventedByViewMode(ViewModeType.Editing));
    this.stateMachineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(InstrumentManagementPermissions.AddAndEditInstruments)).SetGuard((IGuard) new IsOneItemAvailableGuard<Instrument>((ISingleSelectionModel<Instrument>) this.Model));
    this.stateMachineConfigurator.SupportChangeSelection<Instrument>();
    this.stateMachineConfigurator.SupportChangeSelectionSimple<User>(InstrumentManagementTriggers.SelectUser);
    this.stateMachineConfigurator.SupportSuspending();
    this.stateMachineConfigurator.SupportRevertModification();
    this.stateMachineConfigurator.SupportValidationAndSaving();
    this.stateMachineConfigurator.SupportUndoing();
    foreach (ITestPlugin testPlugin in SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().Where<ITestPlugin>((Func<ITestPlugin, bool>) (p => p.ConfigurationModuleType != (Type) null)))
      this.stateMachineConfigurator.SupportSwitchModule(new SwitchModuleOptions(new Trigger(ApplicationComponentModuleManager.Instance.Get(testPlugin.ConfigurationModuleType).Id.ToString()), testPlugin.ConfigurationModuleType).NeedsAnyPermission(InstrumentManagementPermissions.ConfigureSettings));
    foreach (IInstrumentPlugin instrumentPlugin in SystemConfigurationManager.Instance.Plugins.OfType<IInstrumentPlugin>().Where<IInstrumentPlugin>((Func<IInstrumentPlugin, bool>) (p => p.FirmwareSearchModuleType != (Type) null || p.ConfigurationSynchronizationModuleType != (Type) null)))
    {
      Guid id;
      if (instrumentPlugin.FirmwareSearchModuleType != (Type) null)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin.FirmwareSearchModuleType);
        StateMachineConfigurator machineConfigurator = this.stateMachineConfigurator;
        id = applicationComponentModule.Id;
        Trigger trigger = new Trigger(id.ToString());
        Type searchModuleType = instrumentPlugin.FirmwareSearchModuleType;
        machineConfigurator.SupportStartAssistant(trigger, searchModuleType).NeedsAnyPermission(InstrumentManagementPermissions.ConfigureSettings);
      }
      if (instrumentPlugin.ConfigurationSynchronizationModuleType != (Type) null)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin.ConfigurationSynchronizationModuleType);
        StateMachineConfigurator machineConfigurator = this.stateMachineConfigurator;
        id = applicationComponentModule.Id;
        Trigger trigger = new Trigger(id.ToString());
        Type synchronizationModuleType = instrumentPlugin.ConfigurationSynchronizationModuleType;
        machineConfigurator.SupportStartAssistant(trigger, synchronizationModuleType);
      }
    }
    this.stateMachineConfigurator.SupportGettingHelp();
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).AddFromTo(States.Viewing).SetTrigger(Triggers.RefreshDataFromForm).SetCommand((ICommand) new CreateCommandCommand((Func<ICommand>) (() => (ICommand) new ChangeUserAssignmentCommand(this.Model, (IView) this.View)))).ApplyTo(this.StateMachine);
  }
}
