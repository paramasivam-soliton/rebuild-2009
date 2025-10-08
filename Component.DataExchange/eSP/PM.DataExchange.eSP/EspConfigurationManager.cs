// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.EspConfigurationManager
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.DataExchange.eSP;

public class EspConfigurationManager : ISingleEditingModel, IModel
{
  public static EspConfigurationManager Instance => PathMedical.Singleton.Singleton<EspConfigurationManager>.Instance;

  public EspConfiguration EspConfiguration { get; protected set; }

  private EspConfigurationManager() => this.RefreshData();

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void RefreshData()
  {
    this.EspConfiguration = new EspConfiguration();
    this.EspConfiguration.HomeSite = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "HomeSite");
    this.EspConfiguration.EspRemoteAddress = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "EspRemoteAddress");
    this.EspConfiguration.DefaultUserPassword = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DefaultUserPassword");
    this.EspConfiguration.BackupFolder = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("4A7C8559-064D-4271-B001-953A0B01C5F3"), "BackupFolder");
    string configurationValue = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DefaultUserProfileId");
    if (!string.IsNullOrEmpty(configurationValue))
      this.EspConfiguration.DefaultUserProfileId = new Guid(configurationValue);
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<List<UserProfile>>(UserProfileManager.Instance.Profiles, ChangeType.ListLoaded));
    this.Changed((object) this, ModelChangedEventArgs.Create<EspConfiguration>(this.EspConfiguration, ChangeType.ItemEdited));
  }

  public void Store()
  {
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "HomeSite", this.EspConfiguration.HomeSite);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "EspRemoteAddress", this.EspConfiguration.EspRemoteAddress);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DefaultUserPassword", this.EspConfiguration.DefaultUserPassword);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("4A7C8559-064D-4271-B001-953A0B01C5F3"), "BackupFolder", this.EspConfiguration.BackupFolder);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DefaultUserProfileId", Convert.ToString((object) this.EspConfiguration.DefaultUserProfileId));
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
