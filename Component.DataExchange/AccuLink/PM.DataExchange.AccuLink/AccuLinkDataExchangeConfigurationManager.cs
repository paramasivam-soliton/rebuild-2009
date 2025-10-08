// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.AccuLink.AccuLinkDataExchangeConfigurationManager
// Assembly: PM.DataExchange.AccuLink, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 45ECC252-AAEE-4427-BE78-DF1068000887
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.AccuLink.dll

using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.DataExchange.AccuLink;

public class AccuLinkDataExchangeConfigurationManager : ISingleEditingModel, IModel
{
  public static AccuLinkDataExchangeConfigurationManager Instance
  {
    get => PathMedical.Singleton.Singleton<AccuLinkDataExchangeConfigurationManager>.Instance;
  }

  public AccuLinkDataExchangeConfiguration Configuration { get; protected set; }

  private AccuLinkDataExchangeConfigurationManager() => this.RefreshData();

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void RefreshData()
  {
    this.Configuration = new AccuLinkDataExchangeConfiguration();
    this.Configuration.ImportFolder = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ImportFolder");
    this.Configuration.ImportPredefinedComments = SystemConfigurationManager.Instance.GetSystemConfigurationValueAsBoolean(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ImportPredefinedComments");
    this.Configuration.ImportRiskIndicators = SystemConfigurationManager.Instance.GetSystemConfigurationValueAsBoolean(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ImportRiskIndicators");
    this.Configuration.ImportUsers = SystemConfigurationManager.Instance.GetSystemConfigurationValueAsBoolean(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ImportUsers");
    this.Configuration.ExportFolder = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ExportFolder");
    this.Configuration.ExportPredefinedComments = SystemConfigurationManager.Instance.GetSystemConfigurationValueAsBoolean(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ExportPredefinedComments");
    this.Configuration.ExportRiskIndicators = SystemConfigurationManager.Instance.GetSystemConfigurationValueAsBoolean(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ExportRiskIndicators");
    this.Configuration.ExportUsers = SystemConfigurationManager.Instance.GetSystemConfigurationValueAsBoolean(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ExportUsers");
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<AccuLinkDataExchangeConfiguration>(this.Configuration, ChangeType.ItemEdited));
  }

  public void Store()
  {
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ImportFolder", this.Configuration.ImportFolder);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ImportPredefinedComments", this.Configuration.ImportPredefinedComments);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ImportRiskIndicators", this.Configuration.ImportRiskIndicators);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ImportUsers", this.Configuration.ImportUsers);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ExportFolder", this.Configuration.ExportFolder);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ExportPredefinedComments", this.Configuration.ExportPredefinedComments);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ExportRiskIndicators", this.Configuration.ExportRiskIndicators);
    SystemConfigurationManager.Instance.SetSystemConfigurationValue(new Guid("1AB20022-F096-4af8-BDD9-DB9404D81205"), "ExportUsers", this.Configuration.ExportUsers);
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
