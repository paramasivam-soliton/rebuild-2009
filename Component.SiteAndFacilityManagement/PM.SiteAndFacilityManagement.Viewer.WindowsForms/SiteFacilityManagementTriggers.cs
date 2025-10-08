// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.SiteFacilityManagementTriggers
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using PathMedical.Automaton;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms;

public abstract class SiteFacilityManagementTriggers
{
  public static readonly Trigger SwitchToFacilityBrowser = new Trigger(nameof (SwitchToFacilityBrowser));
  public static readonly Trigger SwitchToLocationTypeBrowser = new Trigger(nameof (SwitchToLocationTypeBrowser));
}
