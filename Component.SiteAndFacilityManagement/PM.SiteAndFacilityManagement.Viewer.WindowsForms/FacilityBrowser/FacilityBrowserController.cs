// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.FacilityBrowser.FacilityBrowserController
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Guard;
using PathMedical.Automaton.Transition;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteBrowser;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.ModelViewController;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.FacilityBrowser;

public class FacilityBrowserController : Controller<FacilityManager, FacilityMasterDetailBrowser>
{
  public FacilityBrowserController(
    IApplicationComponentModule applicationComponentModule)
    : base(applicationComponentModule)
  {
    this.Model = FacilityManager.Instance;
    this.View = new FacilityMasterDetailBrowser((IModel) this.Model);
    this.StateMachine = new StateMachine(this.GetType().Name, States.Idle);
    this.SetupStateMachine();
  }

  private void SetupStateMachine()
  {
    StateMachineConfigurator machineConfigurator = new StateMachineConfigurator(this.StateMachine, (IModel) this.Model, (IView) this.View);
    machineConfigurator.SupportAdding().NeedsAnyPermission(SiteFacilityManagementPermissions.AddAndEditFacilities);
    machineConfigurator.SupportDeleting<Facility>().NeedsAnyPermission(SiteFacilityManagementPermissions.DeleteFacilities);
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Viewing, States.Viewing).NeedsAnyPermission(SiteFacilityManagementPermissions.ViewFacilities, SiteFacilityManagementPermissions.AddAndEditFacilities).PreventedByViewMode(ViewModeType.Editing));
    machineConfigurator.SupportViewMode(new EnterViewModeOptions(ViewModeType.Editing, States.Editing).NeedsAnyPermission(SiteFacilityManagementPermissions.AddAndEditFacilities)).SetGuard((IGuard) new IsOneItemAvailableGuard<Facility>((ISingleSelectionModel<Facility>) this.Model));
    machineConfigurator.SupportChangeSelection<Facility>();
    machineConfigurator.SupportUndoing();
    machineConfigurator.SupportRevertModification();
    machineConfigurator.SupportValidationAndSaving();
    machineConfigurator.SupportSwitchModule(Triggers.GoBack, typeof (SiteBrowserComponentModule));
    machineConfigurator.SupportGettingHelp();
    machineConfigurator.SupportSuspending();
  }
}
