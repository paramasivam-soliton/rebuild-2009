// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.SplashScreen.RegistryAccess
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using Microsoft.Win32;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.SplashScreen;

public class RegistryAccess
{
  private const string SoftwareKey = "Software";
  private const string CompanyName = "GN Otometrics";

  public static string GetStringRegistryValue(string key, string defaultValue)
  {
    RegistryKey registryKey1 = Registry.CurrentUser.OpenSubKey("Software", false).OpenSubKey("GN Otometrics", false);
    if (registryKey1 != null)
    {
      RegistryKey registryKey2 = registryKey1.OpenSubKey(Application.ProductName, true);
      if (registryKey2 != null)
      {
        foreach (string valueName in registryKey2.GetValueNames())
        {
          if (valueName == key)
            return (string) registryKey2.GetValue(valueName);
        }
      }
    }
    return defaultValue;
  }

  public static void SetStringRegistryValue(string key, string stringValue)
  {
    RegistryKey registryKey1 = (RegistryKey) null;
    RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software", true);
    if (registryKey2 != null)
      registryKey1 = registryKey2.CreateSubKey("GN Otometrics");
    registryKey1?.CreateSubKey(Application.ProductName)?.SetValue(key, (object) stringValue);
  }
}
