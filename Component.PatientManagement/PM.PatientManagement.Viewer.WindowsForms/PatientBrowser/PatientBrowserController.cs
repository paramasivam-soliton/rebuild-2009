// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.PatientBrowserController
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.AudiologyTest;
using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.DataExchange;
using PathMedical.InstrumentManagement;
using PathMedical.PatientManagement.Automaton.Guard;
using PathMedical.PatientManagement.Command;
using PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.Reports;
using PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using System;
using System.Linq;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser;

public class PatientBrowserController : Controller<PatientManager, PatientTestBrowser>
{
  public PatientBrowserController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = PatientManager.Instance;
    this.View = new PatientTestBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportAddingWithDifferentModule(typeof (PatientEditorComponentModule), Triggers.Add).NeedsAnyPermission(PatientManagementPermissions.AddAndEditPatients);
    machineConfigurator.SupportEditingWithDifferentModule<Patient>(typeof (PatientEditorComponentModule), Triggers.Edit).NeedsAnyPermission(PatientManagementPermissions.ViewPatients, PatientManagementPermissions.AddAndEditPatients);
    machineConfigurator.SupportDeleting<Patient>().NeedsAnyPermission(PatientManagementPermissions.DeletePatients);
    machineConfigurator.SupportChangeSelection<Patient>();
    machineConfigurator.SupportChangeSelection<AudiologyTestInformation>(PatientManagementTriggers.SelectTest);
    machineConfigurator.SupportChangeCurrent<Patient>();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(PatientManagementPermissions.ViewPatients, PatientManagementPermissions.AddAndEditPatients).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(PatientManagementPermissions.AddAndEditPatients)).SetGuard((IGuard) new IsOneItemAvailableGuard<Patient>((IMultiSelectionModel<Patient>) this.Model));
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(Triggers.Search).SetCommand((ICommand) new SearchPatientCommand(this.Model)).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(Triggers.DeleteSubItem).NeedsAnyPermission(PatientManagementPermissions.AddAndEditPatients).SetGuard((IGuard) new IsOneItemSelectedGuard<AudiologyTestInformation>((ISingleSelectionModel<AudiologyTestInformation>) this.Model)).SetGuard((IGuard) new GuardComposition(new IGuard[1]
    {
      (IGuard) new AskSimpleQuestionGuard((IView) this.View, PathMedical.ComponentResourceManagement.Instance.ResourceManager.GetString("ReallyDeleteRecord"), AnswerOptionType.YesNo, AnswerType.Yes)
    })).SetCommand((ICommand) new CommandComposition(new ICommand[2]
    {
      (ICommand) new DeleteTestCommand(this.Model),
      (ICommand) new RefreshModelCommand((IModel) this.Model)
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(Triggers.Cut).NeedsAnyPermission(PatientManagementPermissions.ReAssignTests).SetGuard((IGuard) new IsOneItemSelectedGuard<AudiologyTestInformation>((ISingleSelectionModel<AudiologyTestInformation>) this.Model)).SetCommand((ICommand) new CommandComposition(new ICommand[1]
    {
      (ICommand) new CutTestCommand(this.Model)
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(Triggers.Paste).NeedsAnyPermission(PatientManagementPermissions.ReAssignTests).SetGuard((IGuard) new GuardComposition(new IGuard[3]
    {
      (IGuard) new IsOneItemAvailableGuard<Patient>((IMultiSelectionModel<Patient>) this.Model),
      (IGuard) new IsOneItemInCacheGuard(this.Model),
      (IGuard) new AskSimpleQuestionGuard((IView) this.View, PathMedical.ComponentResourceManagement.Instance.ResourceManager.GetString("ReallyReAssignRecord"), AnswerOptionType.YesNo, AnswerType.Yes)
    })).SetCommand((ICommand) new CommandComposition(new ICommand[1]
    {
      (ICommand) new PasteTestCommand(this.Model)
    })).ApplyTo(this.StateMachine);
    machineConfigurator.SupportGettingHelp();
    foreach (IInstrumentPlugin instrumentPlugin in SystemConfigurationManager.Instance.Plugins.OfType<IInstrumentPlugin>().Where<IInstrumentPlugin>((Func<IInstrumentPlugin, bool>) (p => p.DataDownloadModuleType != (Type) null || p.DataUploadModuleType != (Type) null || p.ConfigurationSynchronizationModuleType != (Type) null)).ToArray<IInstrumentPlugin>())
    {
      if (instrumentPlugin.DataUploadModuleType != (Type) null)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin.DataUploadModuleType);
        machineConfigurator.SupportStartAssistant(new Trigger(applicationComponentModule.Id.ToString()), instrumentPlugin.DataUploadModuleType);
      }
      if (instrumentPlugin.DataDownloadModuleType != (Type) null)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin.DataDownloadModuleType);
        machineConfigurator.SupportStartAssistant(new Trigger(applicationComponentModule.Id.ToString()), instrumentPlugin.DataDownloadModuleType);
      }
      if (instrumentPlugin.ConfigurationSynchronizationModuleType != (Type) null)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin.ConfigurationSynchronizationModuleType);
        machineConfigurator.SupportStartAssistant(new Trigger(applicationComponentModule.Id.ToString()), instrumentPlugin.ConfigurationSynchronizationModuleType);
      }
    }
    foreach (ISupportDataExchangeModules dataExchangeModules in SystemConfigurationManager.Instance.Plugins.OfType<ISupportDataExchangeModules>().Where<ISupportDataExchangeModules>((Func<ISupportDataExchangeModules, bool>) (p => p.ExportModule != null || p.ImportModule != null || p.ConfigurationExportModule != null || p.ConfigurationImportModule != null)).OrderBy<ISupportDataExchangeModules, string>((Func<ISupportDataExchangeModules, string>) (p => p.Name)).ToArray<ISupportDataExchangeModules>())
    {
      if (dataExchangeModules != null)
      {
        if (dataExchangeModules.ExportModule != null)
          machineConfigurator.SupportStartAssistant(new Trigger(dataExchangeModules.ExportModule.Id.ToString()), dataExchangeModules.ExportModule.GetType());
        if (dataExchangeModules.ImportModule != null)
          machineConfigurator.SupportStartAssistant(new Trigger(dataExchangeModules.ImportModule.Id.ToString()), dataExchangeModules.ImportModule.GetType());
        if (dataExchangeModules.ConfigurationImportModule != null)
          machineConfigurator.SupportStartAssistant(new Trigger(dataExchangeModules.ConfigurationImportModule.Id.ToString()), dataExchangeModules.ConfigurationImportModule.GetType());
        if (dataExchangeModules.ConfigurationExportModule != null)
          machineConfigurator.SupportStartAssistant(new Trigger(dataExchangeModules.ConfigurationExportModule.Id.ToString()), dataExchangeModules.ConfigurationExportModule.GetType());
      }
    }
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.PrintBasicOverallReport).SetGuard((IGuard) new IsOneItemAvailableGuard(this.Model, new TestType[3]
    {
      TestType.TEOAE,
      TestType.DPOAE,
      TestType.ABR
    })).SetCommand((ICommand) new PrintSinglePatientReportCommand(this.Model, typeof (DetailTestReport), ReportFormat.Basic, new TestType[3]
    {
      TestType.TEOAE,
      TestType.DPOAE,
      TestType.ABR
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.PrintBasicReportTeoae).SetGuard((IGuard) new IsOneItemAvailableGuard(this.Model, new TestType[1]
    {
      TestType.TEOAE
    })).SetCommand((ICommand) new PrintSinglePatientReportCommand(this.Model, typeof (DetailTestReport), ReportFormat.Basic, new TestType[1]
    {
      TestType.TEOAE
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.PrintBasicReportDpoae).SetGuard((IGuard) new IsOneItemAvailableGuard(this.Model, new TestType[1]
    {
      TestType.DPOAE
    })).SetCommand((ICommand) new PrintSinglePatientReportCommand(this.Model, typeof (DetailTestReport), ReportFormat.Basic, new TestType[1]
    {
      TestType.DPOAE
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.PrintBasicReportAbr).SetGuard((IGuard) new IsOneItemAvailableGuard(this.Model, new TestType[1]
    {
      TestType.ABR
    })).SetCommand((ICommand) new PrintSinglePatientReportCommand(this.Model, typeof (DetailTestReport), ReportFormat.Basic, new TestType[1]
    {
      TestType.ABR
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.PrintDetailOverallReport).SetGuard((IGuard) new IsOneItemAvailableGuard(this.Model, new TestType[3]
    {
      TestType.TEOAE,
      TestType.DPOAE,
      TestType.ABR
    })).SetCommand((ICommand) new PrintSinglePatientReportCommand(this.Model, typeof (DetailTestReport), ReportFormat.Detail, new TestType[3]
    {
      TestType.TEOAE,
      TestType.DPOAE,
      TestType.ABR
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.PrintDetailReportTeoae).SetGuard((IGuard) new IsOneItemAvailableGuard(this.Model, new TestType[1]
    {
      TestType.TEOAE
    })).SetCommand((ICommand) new PrintSinglePatientReportCommand(this.Model, typeof (DetailTestReport), ReportFormat.Detail, new TestType[1]
    {
      TestType.TEOAE
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.PrintDetailReportDpoae).SetGuard((IGuard) new IsOneItemAvailableGuard(this.Model, new TestType[1]
    {
      TestType.DPOAE
    })).SetCommand((ICommand) new PrintSinglePatientReportCommand(this.Model, typeof (DetailTestReport), ReportFormat.Detail, new TestType[1]
    {
      TestType.DPOAE
    })).ApplyTo(this.StateMachine);
    new TransitionDefinition().AddFromTo(States.Viewing).AddFromTo(States.Editing).SetTrigger(PatientManagementTriggers.PrintDetailReportAbr).SetGuard((IGuard) new IsOneItemAvailableGuard(this.Model, new TestType[1]
    {
      TestType.ABR
    })).SetCommand((ICommand) new PrintSinglePatientReportCommand(this.Model, typeof (DetailTestReport), ReportFormat.Detail, new TestType[1]
    {
      TestType.ABR
    })).ApplyTo(this.StateMachine);
    machineConfigurator.SupportSuspending();
  }
}
