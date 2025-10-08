// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.ConfigurationManager
// Assembly: PM.SiteAndFacilityManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F9CADFD-E9C9-4783-BA9E-8D20FAD3C075
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.dll

using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement;

public class ConfigurationManager : ISingleEditingModel, IModel
{
  public static ConfigurationManager Instance => PathMedical.Singleton.Singleton<ConfigurationManager>.Instance;

  private ConfigurationManager() => this.RefreshData();

  public SiteFacilityConfiguration Configuration { get; protected set; }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void RefreshData()
  {
    this.Configuration = new SiteFacilityConfiguration();
    bool? configurationValueAsBoolean = SystemConfigurationManager.Instance.GetSystemConfigurationValueAsBoolean(new Guid("3C09F61E-4C2B-4c91-A753-90BED7D9EE41"), "IsActive");
    this.Configuration.IsSiteFacilityManagementEnabled = configurationValueAsBoolean.HasValue && configurationValueAsBoolean.Value;
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<SiteFacilityConfiguration>(this.Configuration, ChangeType.ItemEdited));
  }

  public void Store()
  {
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("3C09F61E-4C2B-4c91-A753-90BED7D9EE41"), "IsActive", new bool?(this.Configuration.IsSiteFacilityManagementEnabled));
    SystemConfigurationManager.Instance.StoreConfigurationChanges();
    this.RefreshData();
  }

  public void Delete()
  {
  }

  public void CancelNewItem()
  {
  }

  public void PrepareAddItem()
  {
  }

  public void RevertModifications() => this.RefreshData();
}
