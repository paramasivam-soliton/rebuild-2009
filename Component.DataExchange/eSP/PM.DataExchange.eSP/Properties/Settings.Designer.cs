// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Properties.Settings
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace PathMedical.DataExchange.eSP.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
internal sealed class Settings : ApplicationSettingsBase
{
  private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

  public static Settings Default => Settings.defaultInstance;

  [ApplicationScopedSetting]
  [DebuggerNonUserCode]
  [SpecialSetting(SpecialSetting.WebServiceUrl)]
  [DefaultSettingValue("https://192.168.112.4:443/csp/sedqgn/northgate.esp.sedq.bserv.ProxySEDQ.cls")]
  public string PM_DataExchange_eSP_EspWebService_DataUpload
  {
    get => (string) this[nameof (PM_DataExchange_eSP_EspWebService_DataUpload)];
  }
}
