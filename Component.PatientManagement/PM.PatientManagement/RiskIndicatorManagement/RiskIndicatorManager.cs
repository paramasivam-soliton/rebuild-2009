// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.RiskIndicatorManagement.RiskIndicatorManager
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.Exception;
using PathMedical.HistoryTracker;
using PathMedical.PatientManagement.DataAccessLayer;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace PathMedical.PatientManagement.RiskIndicatorManagement;

public class RiskIndicatorManager : 
  ISingleEditingModel,
  IModel,
  ISingleSelectionModel<RiskIndicator>,
  ISupportInternationalization
{
  private ModelHelper<RiskIndicator, RiskIndicatorAdapter> modelHelper;
  private readonly Guid riskIndicatorMaintenanceAccessPermissionId = new Guid("90482128-10C8-45a2-AA33-84AD39BBF37B");

  public static RiskIndicatorManager Instance => PathMedical.Singleton.Singleton<RiskIndicatorManager>.Instance;

  private RiskIndicatorManager()
  {
    this.SetDefaultTranslationCulture();
    this.modelHelper = new ModelHelper<RiskIndicator, RiskIndicatorAdapter>(new EventHandler<ModelChangedEventArgs>(this.OnModelHelperChanged), new string[2]
    {
      "NameTranslationList",
      "DescriptionTranslationList"
    });
  }

  private void OnModelHelperChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, e);
  }

  public List<RiskIndicator> RiskIndicatorList => this.modelHelper.Items;

  public RiskIndicator SelectedRiskIndicator
  {
    get => this.modelHelper.SelectedItem;
    set => this.modelHelper.SelectedItem = value;
  }

  public List<RiskIndicator> ActiveRiskIndicators
  {
    get
    {
      return this.modelHelper.Items != null ? this.modelHelper.Items.Where<RiskIndicator>((Func<RiskIndicator, bool>) (r => r.IsActive.GetValueOrDefault())).ToList<RiskIndicator>() : (List<RiskIndicator>) null;
    }
  }

  public Guid MaintenanceAccessPermissionId => this.riskIndicatorMaintenanceAccessPermissionId;

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void Store()
  {
    foreach (RiskIndicator riskIndicator in this.RiskIndicatorList.Where<RiskIndicator>((Func<RiskIndicator, bool>) (r => r != null)))
    {
      if (riskIndicator.Id != this.SelectedRiskIndicator.Id)
      {
        foreach (ResourceTranslation nameTranslation in this.SelectedRiskIndicator.NameTranslationList)
        {
          if (!string.IsNullOrEmpty(nameTranslation.ResourceText))
          {
            CultureInfo cultureInfo = new CultureInfo(nameTranslation.Culture);
            string name = riskIndicator.GetName(cultureInfo);
            if (!string.IsNullOrEmpty(name) && name.Equals(nameTranslation.ResourceText))
              throw ExceptionFactory.Instance.CreateException<RiskIndicatorManagerException>($"The name \"{nameTranslation.ResourceText}\" for {cultureInfo.DisplayName} already exists (see at risk indicator {riskIndicator.GetName((CultureInfo) null)}).");
          }
        }
      }
    }
    this.modelHelper.Store();
  }

  public void Delete()
  {
    int num = PatientManager.Instance.Patients.SelectMany<Patient, RiskIndicator>((Func<Patient, IEnumerable<RiskIndicator>>) (p => (IEnumerable<RiskIndicator>) p.RiskIndicators)).Where<RiskIndicator>((Func<RiskIndicator, bool>) (r => r.Id.Equals(this.SelectedRiskIndicator.Id) && r.PatientRiskIndicatorValue != 0)).ToList<RiskIndicator>().Count<RiskIndicator>();
    if (num > 0)
      throw ExceptionFactory.Instance.CreateException<ModelException>($"Risk factor can't be deleted since it has been assigned to {num} patient(s).");
    using (DBScope dbScope = new DBScope())
    {
      using (DbCommand dbCommand = dbScope.CreateDbCommand())
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DELETE FROM PatientRiskIndicators");
        stringBuilder.AppendFormat(" WHERE RiskIndicatorId = @RiskIndicatorId");
        stringBuilder.AppendFormat(" AND   RiskIndicatorValue = 0");
        dbCommand.CommandText = stringBuilder.ToString();
        dbScope.AddDbParameter(dbCommand, "RiskIndicatorId", (object) this.SelectedRiskIndicator.Id);
        dbCommand.ExecuteNonQuery();
      }
      this.modelHelper.Delete();
      dbScope.Complete();
    }
  }

  public void CancelNewItem()
  {
    if (this.SelectedRiskIndicator == null || !this.SelectedRiskIndicator.Id.Equals(Guid.Empty))
      return;
    this.RiskIndicatorList.Remove(this.SelectedRiskIndicator);
    if (this.modelHelper != null)
      this.modelHelper.CancelAddItem();
    else
      this.SelectedRiskIndicator = (RiskIndicator) null;
  }

  public void RefreshData() => this.LoadRiskIndicators();

  public void PrepareAddItem()
  {
    RiskIndicator riskIndicator = new RiskIndicator();
    riskIndicator.IsActive = new bool?(true);
    this.RiskIndicatorList.Add(riskIndicator);
    this.SelectedRiskIndicator = riskIndicator;
  }

  public void RevertModifications()
  {
    if (this.SelectedRiskIndicator != null && this.SelectedRiskIndicator.Id == Guid.Empty && this.Changed != null)
      this.Changed((object) this, ModelChangedEventArgs.Create<RiskIndicator>(this.SelectedRiskIndicator, ChangeType.SelectionChanged));
    if (this.modelHelper == null)
      return;
    this.modelHelper.RefreshSelectedItems();
  }

  public void LoadHistory()
  {
    ModelHelper<RiskIndicator, RiskIndicatorAdapter> modelHelper = this.modelHelper;
  }

  public BindingList<HistoryChangeItem> CompareHistoryRecord(
    HistoryEntry oldItem,
    HistoryEntry newItem)
  {
    throw new NotImplementedException();
  }

  public BindingList<HistoryChangeItem> CompareHistoryRecord(HistoryEntry historyItem)
  {
    return new HistoryItemComparer<RiskIndicator>().Compare(this.SelectedRiskIndicator, historyItem);
  }

  public void LoadRiskIndicators()
  {
    if (this.modelHelper == null)
      return;
    this.modelHelper.LoadItems((Func<RiskIndicatorAdapter, ICollection<RiskIndicator>>) (adapter => adapter.All));
  }

  public void Import(List<RiskIndicator> indicators)
  {
    if (indicators == null || indicators.Count == 0)
      return;
    using (DBScope scope = new DBScope())
    {
      RiskIndicatorAdapter indicatorAdapter = new RiskIndicatorAdapter(scope);
      indicatorAdapter.LoadWithRelation("NameTranslationList", "DescriptionTranslationList");
      indicatorAdapter.Store((ICollection<RiskIndicator>) indicators);
      scope.Complete();
      this.RefreshData();
    }
  }

  public void Store(RiskIndicator risk)
  {
    if (risk == null)
      return;
    using (DBScope scope = new DBScope())
    {
      RiskIndicatorAdapter indicatorAdapter = new RiskIndicatorAdapter(scope);
      indicatorAdapter.LoadWithRelation("NameTranslationList", "DescriptionTranslationList");
      indicatorAdapter.Store(risk);
      scope.Complete();
      this.RefreshData();
    }
  }

  public void ChangeSingleSelection(RiskIndicator selection)
  {
    if (this.modelHelper == null)
      return;
    this.modelHelper.SelectedItem = selection;
  }

  public bool IsOneItemSelected<T>() where T : RiskIndicator
  {
    return this.modelHelper != null && this.modelHelper.SelectedItem != null;
  }

  bool ISingleSelectionModel<RiskIndicator>.IsOneItemAvailable<T>()
  {
    return this.modelHelper != null && this.modelHelper.Items != null && this.modelHelper.Items.Count > 0;
  }

  public CultureInfo TranslationCulture { get; private set; }

  private void SetDefaultTranslationCulture()
  {
    if (this.TranslationCulture != null)
      return;
    this.TranslationCulture = SystemConfigurationManager.Instance.CurrentLanguage.Equals((object) SystemConfigurationManager.Instance.BaseLanguage) ? SystemConfigurationManager.Instance.SupportedLanguages.Where<CultureInfo>((Func<CultureInfo, bool>) (ci => !ci.Equals((object) SystemConfigurationManager.Instance.BaseLanguage))).FirstOrDefault<CultureInfo>() : SystemConfigurationManager.Instance.CurrentLanguage;
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<RiskIndicator>(this.SelectedRiskIndicator, ChangeType.ItemEdited));
  }

  public void ChangeLanguage(CultureInfo cultureInfo)
  {
    if (cultureInfo == null)
      cultureInfo = SystemConfigurationManager.Instance.BaseLanguage;
    if (this.TranslationCulture == null || cultureInfo.Equals((object) SystemConfigurationManager.Instance.BaseLanguage))
    {
      this.SetDefaultTranslationCulture();
    }
    else
    {
      this.TranslationCulture = cultureInfo;
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<RiskIndicator>(this.SelectedRiskIndicator, ChangeType.ItemEdited));
    }
  }
}
