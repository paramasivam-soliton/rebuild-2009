// Decompiled with JetBrains decompiler
// Type: GN.Otometrics.NHS.Properties.Resources
// Assembly: AccuScreenServicetool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E723BD4-2FBA-4A66-910E-0878AA53AFFA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\AccuScreenServicetool.exe

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

  internal static string ApplicationStarter_Main_AccuScreen
  {
    get
    {
      return GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetString(nameof (ApplicationStarter_Main_AccuScreen), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
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

  internal static Bitmap GNO
  {
    get => (Bitmap) GN.Otometrics.NHS.Properties.Resources.ResourceManager.GetObject(nameof (GNO), GN.Otometrics.NHS.Properties.Resources.resourceCulture);
  }
}
