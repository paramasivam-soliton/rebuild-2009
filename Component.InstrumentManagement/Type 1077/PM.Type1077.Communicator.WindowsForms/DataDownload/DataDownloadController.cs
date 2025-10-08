// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Communicator.WindowsForms.DataDownload.DataDownloadController
// Assembly: PM.Type1077.Communicator.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4173E4B7-BF2C-4E03-A869-8E0498B48F2A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.Communicator.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Transition;
using PathMedical.InstrumentManagement;
using PathMedical.Type1077.Automaton;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.Type1077.Communicator.WindowsForms.DataDownload;

public class DataDownloadController : Controller<Type1077Manager, DownloadAssistant>
{
  public DataDownloadController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = Type1077Manager.Instance;
    this.View = new DownloadAssistant((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing));
    machineConfigurator.SupportGettingHelp();
    new TransitionDefinition().AddFromTo(States.Viewing, States.Viewing).SetTrigger(InstrumentManagementTriggers.StartInstrumentSearch).SetCommand((ICommand) new StartSearchInstrumentsCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing, States.Viewing).SetTrigger(Triggers.ChangeSelection).SetCommand((ICommand) new ChangeSelectionCommand<Instrument>((IModel) this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing, States.Viewing).SetTrigger(InstrumentManagementTriggers.StartDataDownload).SetCommand((ICommand) new StartInstrumentDataDownloadCommand(this.Model, true)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing, States.Idle).SetTrigger(Triggers.AbortAndClose).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing, States.Viewing).SetTrigger(InstrumentManagementTriggers.StartConfigurationSynchronization).SetCommand((ICommand) new ConfigureInstrumentCommand(this.Model, true)).ApplyTo(this.StateMachine);
  }
}
