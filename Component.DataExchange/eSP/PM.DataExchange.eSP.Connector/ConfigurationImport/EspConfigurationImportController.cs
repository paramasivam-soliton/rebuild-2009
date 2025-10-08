// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Connector.WindowsForms.ConfigurationImport.EspConfigurationImportController
// Assembly: PM.DataExchange.eSP.Connector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1934FEB7-E9C0-480F-B9F2-DEDB47DB36DA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.Connector.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Transition;
using PathMedical.DataExchange.eSP.Automaton;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.DataExchange.eSP.Connector.WindowsForms.ConfigurationImport;

public class EspConfigurationImportController : 
  Controller<EspManager, EspImportConfigurationAssistant>
{
  public EspConfigurationImportController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = EspManager.Instance;
    this.View = new EspImportConfigurationAssistant((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing));
    machineConfigurator.SupportGettingHelp();
    new TransitionDefinition().AddFromTo(States.Viewing, States.Viewing).SetTrigger(Triggers.Import).SetCommand((ICommand) new CommandComposition(new ICommand[2]
    {
      (ICommand) new ImportConfigurationCommand(this.Model),
      (ICommand) new RefreshModelCommand((IModel) this.Model)
    })).ApplyTo(this.StateMachine);
  }
}
