// Decompiled with JetBrains decompiler
// Type: PathMedical.SystemConfiguration.Core.ComponentBrowserManager
// Assembly: PM.SystemConfiguration.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 48D3969F-7C1B-4635-8312-733FCC6A2713
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SystemConfiguration.Core.dll

using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.SystemConfiguration.Core;

public class ComponentBrowserManager : ISingleEditingModel, IModel
{
  public static ComponentBrowserManager Instance => PathMedical.Singleton.Singleton<ComponentBrowserManager>.Instance;

  private ComponentBrowserManager() => this.RefreshData();

  public GlobalSystemConfiguration GlobalSystemConfiguration { get; protected set; }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void RefreshData()
  {
    this.GlobalSystemConfiguration = new GlobalSystemConfiguration();
    DeletionConfirmation deletionConfirmation = DeletionConfirmation.No;
    string configurationValue1 = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayMessageAfterSuccessfullyDeletion");
    if (!string.IsNullOrEmpty(configurationValue1))
    {
      try
      {
        deletionConfirmation = (DeletionConfirmation) Enum.Parse(typeof (DeletionConfirmation), configurationValue1);
      }
      catch (ArgumentException ex)
      {
      }
    }
    this.GlobalSystemConfiguration.DisplayMessageAfterSuccessfullyDeletion = deletionConfirmation;
    StorageConfirmation storageConfirmation = StorageConfirmation.No;
    string configurationValue2 = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayMessageAfterSuccessfullyStorage");
    if (!string.IsNullOrEmpty(configurationValue2))
    {
      try
      {
        storageConfirmation = (StorageConfirmation) Enum.Parse(typeof (StorageConfirmation), configurationValue2);
      }
      catch (ArgumentException ex)
      {
      }
    }
    this.GlobalSystemConfiguration.DisplayMessageAfterSuccessfullyStorage = storageConfirmation;
    DataModificationWarning modificationWarning = DataModificationWarning.No;
    string configurationValue3 = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayDataModificationWarning");
    if (!string.IsNullOrEmpty(configurationValue3))
    {
      try
      {
        modificationWarning = (DataModificationWarning) Enum.Parse(typeof (DataModificationWarning), configurationValue3);
      }
      catch (ArgumentException ex)
      {
      }
    }
    this.GlobalSystemConfiguration.DisplayDataModificationWarning = modificationWarning;
    TrackingSytem trackingSytem = TrackingSytem.None;
    string configurationValue4 = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "TrackingSystem");
    if (!string.IsNullOrEmpty(configurationValue4))
    {
      try
      {
        trackingSytem = (TrackingSytem) Enum.Parse(typeof (TrackingSytem), configurationValue4);
      }
      catch (ArgumentException ex)
      {
      }
    }
    this.GlobalSystemConfiguration.TrackingSystem = trackingSytem;
    this.GlobalSystemConfiguration.DefaultSystemLanguage = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DefaultSystemLanguage");
    this.GlobalSystemConfiguration.reportPictureString = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "ReportPicture");
    if (string.IsNullOrEmpty(this.GlobalSystemConfiguration.reportPictureString))
    {
      this.GlobalSystemConfiguration.reportPictureString = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("8B5B064B-EC5F-4524-96DD-0E88BD3FA952"), "DefaultReportPicture");
      this.GlobalSystemConfiguration.defaultPicture = true;
    }
    else
      this.GlobalSystemConfiguration.defaultPicture = false;
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<GlobalSystemConfiguration>(this.GlobalSystemConfiguration, ChangeType.ItemEdited));
  }

  public void Store()
  {
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayMessageAfterSuccessfullyDeletion", this.GlobalSystemConfiguration.DisplayMessageAfterSuccessfullyDeletion.ToString());
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayMessageAfterSuccessfullyStorage", this.GlobalSystemConfiguration.DisplayMessageAfterSuccessfullyStorage.ToString());
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "TrackingSystem", this.GlobalSystemConfiguration.TrackingSystem.ToString());
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DefaultSystemLanguage", this.GlobalSystemConfiguration.DefaultSystemLanguage);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "DisplayDataModificationWarning", this.GlobalSystemConfiguration.DisplayDataModificationWarning.ToString());
    if (string.Compare(this.GlobalSystemConfiguration.reportPictureString, SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("8B5B064B-EC5F-4524-96DD-0E88BD3FA952"), "DefaultReportPicture")) != 0)
    {
      SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "ReportPicture", this.GlobalSystemConfiguration.reportPictureString);
      this.GlobalSystemConfiguration.defaultPicture = false;
    }
    else
    {
      SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "ReportPicture", string.Empty);
      this.GlobalSystemConfiguration.defaultPicture = true;
    }
    SystemConfigurationManager.Instance.StoreConfigurationChanges();
    this.RefreshData();
    ApplicationComponentModuleManager.Instance.DestroyCache();
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
