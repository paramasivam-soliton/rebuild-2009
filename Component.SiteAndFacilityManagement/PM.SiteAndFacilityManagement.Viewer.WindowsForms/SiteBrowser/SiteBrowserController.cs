// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteBrowser.SiteBrowserController
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.InstrumentManagement;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.FacilityBrowser;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.LocationTypeBrowser;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;
using System;
using System.Linq;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteBrowser;

public class SiteBrowserController : Controller<SiteManager, SiteMasterDetailBrowser>
{
  public SiteBrowserController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = SiteManager.Instance;
    this.View = new SiteMasterDetailBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportAdding().NeedsAnyPermission(SiteFacilityManagementPermissions.AddAndEditSites);
    machineConfigurator.SupportDeleting<Site>().NeedsAnyPermission(SiteFacilityManagementPermissions.DeleteSites);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(SiteFacilityManagementPermissions.ViewSites, SiteFacilityManagementPermissions.AddAndEditSites).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(SiteFacilityManagementPermissions.AddAndEditSites)).SetGuard((IGuard) new IsOneItemAvailableGuard<Site>((ISingleSelectionModel<Site>) this.Model));
    machineConfigurator.SupportChangeSelection<Site>();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportSwitchModule(SiteFacilityManagementTriggers.SwitchToFacilityBrowser, typeof (FacilityBrowserComponentModule)).NeedsAnyPermission(SiteFacilityManagementPermissions.ViewFacilities, SiteFacilityManagementPermissions.AddAndEditFacilities, SiteFacilityManagementPermissions.DeleteFacilities);
    machineConfigurator.SupportSwitchModule(SiteFacilityManagementTriggers.SwitchToLocationTypeBrowser, typeof (LocationTypeBrowserComponentModule)).NeedsAnyPermission(SiteFacilityManagementPermissions.ViewSites, SiteFacilityManagementPermissions.AddAndEditSites, SiteFacilityManagementPermissions.DeleteSites, SiteFacilityManagementPermissions.ViewFacilities, SiteFacilityManagementPermissions.AddAndEditFacilities, SiteFacilityManagementPermissions.DeleteFacilities);
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportGettingHelp();
    foreach (IInstrumentPlugin instrumentPlugin in SystemConfigurationManager.Instance.Plugins.OfType<IInstrumentPlugin>().Where<IInstrumentPlugin>((Func<IInstrumentPlugin, bool>) (p => p.ConfigurationSynchronizationModuleType != (Type) null)).ToArray<IInstrumentPlugin>())
    {
      if (instrumentPlugin.ConfigurationSynchronizationModuleType != (Type) null)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get(instrumentPlugin.ConfigurationSynchronizationModuleType);
        machineConfigurator.SupportStartAssistant(new Trigger(applicationComponentModule.Id.ToString()), instrumentPlugin.ConfigurationSynchronizationModuleType);
      }
    }
    machineConfigurator.SupportSuspending();
  }
}
