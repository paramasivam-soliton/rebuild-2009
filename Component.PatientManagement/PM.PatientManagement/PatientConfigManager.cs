// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.PatientConfigManager
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Exception;
using PathMedical.PatientManagement.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.PatientManagement;

public class PatientConfigManager : ISingleEditingModel, IModel
{
  private IList<PatientFieldConfigItem> patientConfigItems;

  public static PatientConfigManager Instance => PathMedical.Singleton.Singleton<PatientConfigManager>.Instance;

  private PatientConfigManager()
  {
  }

  public IEnumerable<PatientFieldConfigItem> PatientFieldConfigItems
  {
    get
    {
      if (this.patientConfigItems == null)
        this.BuildPatientConfigItems();
      return (IEnumerable<PatientFieldConfigItem>) this.patientConfigItems;
    }
  }

  private void BuildPatientConfigItems()
  {
    this.patientConfigItems = (IList<PatientFieldConfigItem>) ((IEnumerable<string>) new string[15]
    {
      "PatientRecordNumber",
      "HospitalId",
      "PatientForename",
      "PatientSurname",
      "PatientDateOfBirth",
      "PatientGender",
      "PatientBirthLocation",
      "PatientWeight",
      "MotherForename",
      "MotherSurname",
      "CaregiverForename",
      "CaregiverSurname",
      "Nicu",
      "ConsentState",
      "RiskIndicator"
    }).Select<string, PatientFieldConfigItem>((Func<string, PatientFieldConfigItem>) (f => new PatientFieldConfigItem(f, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString(f + "Field"), f + "FieldConfiguration"))).ToList<PatientFieldConfigItem>();
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void RefreshData()
  {
    this.BuildPatientConfigItems();
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<IEnumerable<PatientFieldConfigItem>>(this.PatientFieldConfigItems, ChangeType.ListLoaded));
    this.Changed((object) this, ModelChangedEventArgs.Create<PatientManagementConfiguration>(PatientManagementConfiguration.Instance, ChangeType.SelectionChanged));
  }

  public void Store()
  {
    if (this.PatientFieldConfigItems.Count<PatientFieldConfigItem>((Func<PatientFieldConfigItem, bool>) (pfci => pfci.IsActive)) > 10)
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("Only10ActiveFieldsAllowed"));
    foreach (PatientFieldConfigItem patientFieldConfigItem in this.PatientFieldConfigItems)
      patientFieldConfigItem.ApplyChanges();
    PatientManagementConfiguration.Instance.StoreFieldConfiguration();
    this.RefreshData();
  }

  public void Delete() => throw new NotImplementedException();

  public void CancelNewItem() => throw new NotImplementedException();

  public void PrepareAddItem() => throw new NotImplementedException();

  public void RevertModifications()
  {
    PatientManagementConfiguration.Instance.ResetFieldConfiguration();
    this.RefreshData();
  }
}
