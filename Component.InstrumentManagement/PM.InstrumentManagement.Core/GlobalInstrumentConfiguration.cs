// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.GlobalInstrumentConfiguration
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using PathMedical.SystemConfiguration;
using System;

#nullable disable
namespace PathMedical.InstrumentManagement;

public class GlobalInstrumentConfiguration
{
  private readonly Guid fingerprint = new Guid("A4465EA9-02B7-4b12-A530-7946D4813552");

  public static GlobalInstrumentConfiguration Instance
  {
    get => PathMedical.Singleton.Singleton<GlobalInstrumentConfiguration>.Instance;
  }

  private GlobalInstrumentConfiguration() => this.LoadData();

  public int DisplayTimeout { get; set; }

  public int PowerTimeout { get; set; }

  public InstrumentDataDeletionRule DeletionRule { get; set; }

  public void Store()
  {
    SystemConfigurationManager instance = SystemConfigurationManager.Instance;
    if (this.DisplayTimeout > this.PowerTimeout)
      this.DisplayTimeout = this.PowerTimeout;
    instance.SetSystemConfigurationValue(this.fingerprint, "InstrumentDisplayTimeout", this.DisplayTimeout.ToString());
    instance.SetSystemConfigurationValue(this.fingerprint, "InstrumentPowerTimeout", this.PowerTimeout.ToString());
    instance.SetSystemConfigurationValue(this.fingerprint, "InstrumentDeletionRule", Convert.ToString((int) this.DeletionRule));
    instance.StoreConfigurationChanges();
  }

  public void ResetChanges()
  {
    SystemConfigurationManager.Instance.ResetConfigurationChanges();
    this.LoadData();
  }

  private void LoadData()
  {
    SystemConfigurationManager instance = SystemConfigurationManager.Instance;
    int result1;
    if (!int.TryParse(instance.GetSystemConfigurationValue(this.fingerprint, "InstrumentDisplayTimeout"), out result1))
      result1 = 2;
    this.DisplayTimeout = result1;
    int result2;
    if (!int.TryParse(instance.GetSystemConfigurationValue(this.fingerprint, "InstrumentPowerTimeout"), out result2))
      result2 = 5;
    this.PowerTimeout = result2;
    int result3;
    if (int.TryParse(instance.GetSystemConfigurationValue(this.fingerprint, "InstrumentDeletionRule"), out result3))
      this.DeletionRule = (InstrumentDataDeletionRule) result3;
    else
      this.DeletionRule = InstrumentDataDeletionRule.Manual;
  }
}
