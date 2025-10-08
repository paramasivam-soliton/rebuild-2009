// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewerController
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

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer;

public class TestViewerController : Controller<WaveFormDataManager, PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewer>
{
  public TestViewerController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = WaveFormDataManager.Instance;
    this.View = new PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewer((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(PatientManagementPermissions.ViewPatients, PatientManagementPermissions.AddAndEditPatients));
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Adding).SetTrigger(WaveFileImportTriggers.LoadTestDataFromFile).SetCommand((ICommand) new CommandComposition(new ICommand[4]
    {
      (ICommand) new CleanUpViewCommand((IView) this.View),
      (ICommand) new ClearUndoHistoryCommand(),
      (ICommand) new StartWaveFileDataImportCommand(this.Model),
      (ICommand) new RefreshModelCommand((IModel) this.Model)
    })).ApplyTo(this.StateMachine);
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportGettingHelp();
  }
}
