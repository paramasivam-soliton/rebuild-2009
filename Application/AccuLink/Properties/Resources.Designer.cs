// Decompiled with JetBrains decompiler
// Type: GN.Otometrics.NHS.Properties.Resources
// Assembly: AccuLink, Version=0.1.9.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0C33E5-E7AA-4F2A-B1D7-6F85B2D56646
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\AccuLink.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace GN.Otometrics.NHS.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Resources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (GN.Otometrics.NHS.Properties.Resources.resourceMan == null)
        GN.Otometrics.NHS.Properties.Resources.resourceMan = new ResourceManager("GN.Otometrics.NHS.Properties.Resources", typeof (GN.Otometrics.NHS.Properties.Resources).Assembly);
      return GN.Otometrics.NHS.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => GN.Otometrics.NHS.Properties.Resources.resourceCulture;
    set => GN.Otometrics.NHS.Properties.Resources.resourceCulture = value;
  }

  internal static string ApplicationStarter_InformAboutUnsendData_Text
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_InformAboutUnsendData_Text), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
    }
  }

  internal static string ApplicationStarter_InformAboutUnsendData_WindowTitle
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_InformAboutUnsendData_WindowTitle), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
    }
  }

  internal static string ApplicationStarter_Main_AccuLink
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_Main_AccuLink), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
    }
  }

  internal static string ApplicationStarter_Main_Application_already_active_
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_Main_Application_already_active_), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
    }
  }

  internal static string ApplicationStarter_Main_ApplicationException
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_Main_ApplicationException), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
    }
  }

  internal static string ApplicationStarter_PerformDatabaseBackup_AccuLink_has_been_backed_up_successfully_
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_PerformDatabaseBackup_AccuLink_has_been_backed_up_successfully_), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
    }
  }

  internal static string ApplicationStarter_PerformDatabaseBackup_AccuLink_has_not_been_backed_up__
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_PerformDatabaseBackup_AccuLink_has_not_been_backed_up__), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
    }
  }

  internal static string ApplicationStarter_PerformDatabaseBackup_Configure_BackupFolder_Message
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_PerformDatabaseBackup_Configure_BackupFolder_Message), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
    }
  }

  internal static string ApplicationStarter_PerformDatabaseBackup_MessageBoxTitle
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_PerformDatabaseBackup_MessageBoxTitle), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
    }
  }

  internal static string ApplicationStarter_PerformDatabaseBackup_Shall_the_database_now_be_backed_up_
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_PerformDatabaseBackup_Shall_the_database_now_be_backed_up_), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap GNO
  {
    get => (Bitmap) GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetObject(nameof (GNO), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
  }

  internal static Icon icon
  {
    get => (Icon) GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetObject(nameof (icon), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
  }
}
