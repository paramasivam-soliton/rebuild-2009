// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.WaveFormImport.WaveFormImportController
// Assembly: PM.PatientManagement.MultipleTestViewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2267BB5-2AA5-4F30-972C-4D9F0F378D06
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.MultipleTestViewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Transition;
using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Automaton;
using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Data;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using System;

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.WaveFormImport;

[Obsolete]
public class WaveFormImportController : Controller<WaveFormDataManager, WaveFormImportAssistent>
{
  public WaveFormImportController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = WaveFormDataManager.Instance;
    this.View = new WaveFormImportAssistent((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing));
    machineConfigurator.SupportGettingHelp();
    new TransitionDefinition().AddFromTo(States.Viewing, States.Viewing).SetTrigger(WaveFileImportTriggers.StartDataImport).SetCommand((ICommand) new StartWaveFileDataImportCommand(this.Model)).ApplyTo(this.StateMachine);
  }
}
