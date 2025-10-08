// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.Properties.Path_Resources
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Path_Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Path_Resources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (Path_Resources.resourceMan == null)
        Path_Resources.resourceMan = new ResourceManager("PathMedical.UserInterface.WindowsForms.Properties.Path.Resources", typeof (Path_Resources).Assembly);
      return Path_Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Path_Resources.resourceCulture;
    set => Path_Resources.resourceCulture = value;
  }

  internal static string CantSwitchCulture
  {
    get
    {
      return Path_Resources.ResourceManager.GetString(nameof (CantSwitchCulture), Path_Resources.resourceCulture);
    }
  }

  internal static string DatabaseConnectionFailed
  {
    get
    {
      return Path_Resources.ResourceManager.GetString(nameof (DatabaseConnectionFailed), Path_Resources.resourceCulture);
    }
  }

  internal static string ErrorMessageCaption
  {
    get
    {
      return Path_Resources.ResourceManager.GetString(nameof (ErrorMessageCaption), Path_Resources.resourceCulture);
    }
  }

  internal static Bitmap GoBack
  {
    get
    {
      return (Bitmap) Path_Resources.ResourceManager.GetObject(nameof (GoBack), Path_Resources.resourceCulture);
    }
  }

  internal static Bitmap Help
  {
    get
    {
      return (Bitmap) Path_Resources.ResourceManager.GetObject(nameof (Help), Path_Resources.resourceCulture);
    }
  }

  internal static Bitmap IdCard
  {
    get
    {
      return (Bitmap) Path_Resources.ResourceManager.GetObject(nameof (IdCard), Path_Resources.resourceCulture);
    }
  }

  internal static string InformationMessageCaption
  {
    get
    {
      return Path_Resources.ResourceManager.GetString(nameof (InformationMessageCaption), Path_Resources.resourceCulture);
    }
  }

  internal static Bitmap Key
  {
    get
    {
      return (Bitmap) Path_Resources.ResourceManager.GetObject(nameof (Key), Path_Resources.resourceCulture);
    }
  }

  internal static string LoginFailed
  {
    get
    {
      return Path_Resources.ResourceManager.GetString(nameof (LoginFailed), Path_Resources.resourceCulture);
    }
  }

  internal static string LoginFailedAccountLocked
  {
    get
    {
      return Path_Resources.ResourceManager.GetString(nameof (LoginFailedAccountLocked), Path_Resources.resourceCulture);
    }
  }

  internal static string MandatoryFieldIsEmpty
  {
    get
    {
      return Path_Resources.ResourceManager.GetString(nameof (MandatoryFieldIsEmpty), Path_Resources.resourceCulture);
    }
  }

  internal static Bitmap Paint
  {
    get
    {
      return (Bitmap) Path_Resources.ResourceManager.GetObject(nameof (Paint), Path_Resources.resourceCulture);
    }
  }

  internal static string PasswordFieldsNotEqual
  {
    get
    {
      return Path_Resources.ResourceManager.GetString(nameof (PasswordFieldsNotEqual), Path_Resources.resourceCulture);
    }
  }

  internal static Bitmap Save
  {
    get
    {
      return (Bitmap) Path_Resources.ResourceManager.GetObject(nameof (Save), Path_Resources.resourceCulture);
    }
  }

  internal static Bitmap TrashEmpty
  {
    get
    {
      return (Bitmap) Path_Resources.ResourceManager.GetObject(nameof (TrashEmpty), Path_Resources.resourceCulture);
    }
  }

  internal static Bitmap TrashFull
  {
    get
    {
      return (Bitmap) Path_Resources.ResourceManager.GetObject(nameof (TrashFull), Path_Resources.resourceCulture);
    }
  }

  internal static Bitmap Undo
  {
    get
    {
      return (Bitmap) Path_Resources.ResourceManager.GetObject(nameof (Undo), Path_Resources.resourceCulture);
    }
  }
}
