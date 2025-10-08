// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.FirmwareUpdateAssistant.FirmwareUpdateAssistantController
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Transition;
using PathMedical.ServiceTools.Automaton;
using PathMedical.ServiceTools.WindowsForms.Automaton;
using PathMedical.Type1077;
using PathMedical.Type1077.Automaton;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.FirmwareUpdateAssistant;

internal class FirmwareUpdateAssistantController : 
  Controller<Type1077Manager, PathMedical.ServiceTools.WindowsForms.FirmwareUpdateAssistant.FirmwareUpdateAssistant>
{
  public FirmwareUpdateAssistantController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = Type1077Manager.Instance;
    this.View = new PathMedical.ServiceTools.WindowsForms.FirmwareUpdateAssistant.FirmwareUpdateAssistant((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing));
    new TransitionDefinition().AddFromTo(States.Adding).AddFromTo(States.Editing).AddFromTo(States.Viewing).SetTrigger(InstrumentManagementTriggers.StartInstrumentSearch).SetCommand((ICommand) new StartSearchInstrumentsCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.SelectFirmwareFileTrigger).SetCommand((ICommand) new SelectFirmwareFileCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(ServiceToolsTriggers.UpdateFirmwareTrigger).SetCommand((ICommand) new UpdateFirmwareCommand(this.Model)).ApplyTo(this.StateMachine);
    machineConfigurator.SupportGettingHelp();
  }
}
