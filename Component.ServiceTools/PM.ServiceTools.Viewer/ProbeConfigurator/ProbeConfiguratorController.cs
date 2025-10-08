// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.ProbeConfigurator.ProbeConfiguratorController
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Transition;
using PathMedical.InstrumentManagement;
using PathMedical.ServiceTools.Automaton;
using PathMedical.ServiceTools.WindowsForms.Automaton;
using PathMedical.ServiceTools.WindowsForms.DeviceSelectorAsistant;
using PathMedical.ServiceTools.WindowsForms.FirmwareUpdateAssistant;
using PathMedical.ServiceTools.WindowsForms.FirmwareUploadAssistant;
using PathMedical.ServiceTools.WindowsForms.ProbeSelectionAssistant;
using PathMedical.SystemConfiguration;
using PathMedical.Type1077;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using System;
using System.Linq;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.ProbeConfigurator;

internal class ProbeConfiguratorController : Controller<Type1077Manager, ProbeConfiguratorEditor>
{
  public ProbeConfiguratorController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = Type1077Manager.Instance;
    this.View = new ProbeConfiguratorEditor((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing));
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportGettingHelp();
    machineConfigurator.SupportStartAssistant(new Trigger(ProbeSelectorAssistantComponentModule.ProbeSelectorComponentModuleId.ToString()), typeof (ProbeSelectorAssistantComponentModule));
    machineConfigurator.SupportStartAssistant(new Trigger(DeviceSelectorAssistantComponentModule.DeviceSelectorComponentModuleId.ToString()), typeof (DeviceSelectorAssistantComponentModule));
    foreach (IInstrumentPlugin instrumentPlugin in SystemConfigurationManager.Instance.Plugins.OfType<IInstrumentPlugin>().Where<IInstrumentPlugin>((Func<IInstrumentPlugin, bool>) (p => p.FirmwareSearchModuleType != (Type) null)))
    {
      if (instrumentPlugin.FirmwareSearchModuleType != (Type) null)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin.FirmwareSearchModuleType);
        machineConfigurator.SupportStartAssistant(new Trigger(applicationComponentModule.Id.ToString()), instrumentPlugin.FirmwareSearchModuleType).NeedsAnyPermission(InstrumentManagementPermissions.ConfigureSettings);
      }
    }
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.StartToneGenerator).SetCommand((ICommand) new StartToneGeneratorCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.StopToneGenerator).SetCommand((ICommand) new StopToneGeneratorCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.ComputeCorrectionValueTrigger).SetCommand((ICommand) new ComputeProbeCorrectionValueCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.ComputeMicrofoneCorrectionValueTrigger).SetCommand((ICommand) new ComputeProbeRmsCorrectionValueCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.SendProbeConfigurationTrigger).SetCommand((ICommand) new SendProbeConfigurationCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.LoopBackCableTestTrigger).SetCommand((ICommand) new LoopBackCableTestCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.DeleteDeviceMemoryTrigger).SetCommand((ICommand) new DeleteDeviceMemoryCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.SelectFirmwareFileTrigger).SetCommand((ICommand) new SelectFirmwareFileCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.UploadFirmwareFileTrigger).SetCommand((ICommand) new StartAssistantCommand(typeof (FirmwareUploadAssistantComponentModule))).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).AddFromTo(States.Viewing).SetTrigger(ServiceToolsTriggers.UpdateFirmwareTrigger).SetCommand((ICommand) new StartAssistantCommand(typeof (FirmwareUpdateAssistantComponentModule))).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.SetFirmwareLicenceTrigger).SetCommand((ICommand) new SetFirmwareLicenceCommand(this.Model)).ApplyTo(this.StateMachine);
  }
}
