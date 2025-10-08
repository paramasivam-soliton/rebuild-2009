// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.ConfigurationManager
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.UserProfileManagement;

public class ConfigurationManager : ISingleEditingModel, IModel
{
  public UserProfileManagementConfiguration UserProfileManagementConfiguration { get; protected set; }

  private ConfigurationManager() => this.RefreshData();

  public static ConfigurationManager Instance => PathMedical.Singleton.Singleton<ConfigurationManager>.Instance;

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void Store()
  {
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("0135E402-053D-4696-B68B-F574A1096246"), "IsActive", new bool?(this.UserProfileManagementConfiguration.IsUserManagementActive));
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("0135E402-053D-4696-B68B-F574A1096246"), "DisabledUserManagementAdminPassword", this.UserProfileManagementConfiguration.DisabledUserManagementAdminPassword);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("0135E402-053D-4696-B68B-F574A1096246"), "DisabledUserManagementProfileId", Convert.ToString((object) this.UserProfileManagementConfiguration.DisabledUserManagementProfileId));
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("0135E402-053D-4696-B68B-F574A1096246"), "UserAccountLockingTime", Convert.ToString(this.UserProfileManagementConfiguration.UserAccountLockingTime));
    SystemConfigurationManager.Instance.StoreConfigurationChanges();
    this.RefreshData();
  }

  public void Delete()
  {
  }

  public void CancelNewItem()
  {
  }

  public void RefreshData()
  {
    this.UserProfileManagementConfiguration = new UserProfileManagementConfiguration();
    bool? configurationValueAsBoolean = SystemConfigurationManager.Instance.GetSystemConfigurationValueAsBoolean(new Guid("0135E402-053D-4696-B68B-F574A1096246"), "IsActive");
    this.UserProfileManagementConfiguration.IsUserManagementActive = configurationValueAsBoolean.HasValue && configurationValueAsBoolean.Value;
    string configurationValue1 = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("0135E402-053D-4696-B68B-F574A1096246"), "DisabledUserManagementProfileId");
    if (!string.IsNullOrEmpty(configurationValue1))
      this.UserProfileManagementConfiguration.DisabledUserManagementProfileId = new Guid?(new Guid(configurationValue1));
    this.UserProfileManagementConfiguration.DisabledUserManagementAdminPassword = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("0135E402-053D-4696-B68B-F574A1096246"), "DisabledUserManagementAdminPassword");
    string configurationValue2 = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("0135E402-053D-4696-B68B-F574A1096246"), "UserAccountLockingTime");
    this.UserProfileManagementConfiguration.UserAccountLockingTime = string.IsNullOrEmpty(configurationValue2) ? 15 : Convert.ToInt32(configurationValue2);
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<List<UserProfile>>(UserProfileManager.Instance.Profiles, ChangeType.ListLoaded));
    this.Changed((object) this, ModelChangedEventArgs.Create<UserProfileManagementConfiguration>(this.UserProfileManagementConfiguration, ChangeType.ItemEdited));
  }

  public void PrepareAddItem()
  {
  }

  public void RevertModifications() => this.RefreshData();

  public void ChangeSelection(Type selectionType, object selection)
  {
  }
}
