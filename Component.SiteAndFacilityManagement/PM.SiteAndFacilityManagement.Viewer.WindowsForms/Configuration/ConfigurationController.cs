// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Configuration.ConfigurationController
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Transition;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using System;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Configuration;

public class ConfigurationController : Controller<ConfigurationManager, ConfigurationEditor>
{
  private readonly Type parentModuleType;

  public ConfigurationController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = ConfigurationManager.Instance;
    this.View = new ConfigurationEditor((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportGoBack();
    machineConfigurator.SupportSuspending();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(SiteFacilityManagementPermissions.ConfigureSiteFacilityManagement).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(SiteFacilityManagementPermissions.ConfigureSiteFacilityManagement));
    machineConfigurator.SupportGettingHelp();
  }
}
