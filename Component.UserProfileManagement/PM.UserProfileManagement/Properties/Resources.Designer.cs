// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.Properties.Resources
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace PathMedical.UserProfileManagement.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
public class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Resources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public static ResourceManager ResourceManager
  {
    get
    {
      if (PathMedical.UserProfileManagement.Properties.Resources.resourceMan == null)
        PathMedical.UserProfileManagement.Properties.Resources.resourceMan = new ResourceManager("PathMedical.UserProfileManagement.Properties.Resources", typeof (PathMedical.UserProfileManagement.Properties.Resources).Assembly);
      return PathMedical.UserProfileManagement.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public static CultureInfo Culture
  {
    get => PathMedical.UserProfileManagement.Properties.Resources.resourceCulture;
    set => PathMedical.UserProfileManagement.Properties.Resources.resourceCulture = value;
  }

  public static string CannotDeleteProfileInUse
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (CannotDeleteProfileInUse), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string CannotDeleteUser
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (CannotDeleteUser), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string LoginVerifier_ModuleDescription
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (LoginVerifier_ModuleDescription), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string LoginVerifier_ModuleName
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (LoginVerifier_ModuleName), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string ProfileWithSameNameExists
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (ProfileWithSameNameExists), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string UnlockUserAccountCommand_ConfirmationUnlockMessage
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (UnlockUserAccountCommand_ConfirmationUnlockMessage), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string UnlockUserAccountCommand_ConfirmationUnlockMessageWindowTitle
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (UnlockUserAccountCommand_ConfirmationUnlockMessageWindowTitle), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string UserManager_Exception_PluginDataDescriptionMissing
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (UserManager_Exception_PluginDataDescriptionMissing), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string UserProfileManager_Exception_PluginDataDescriptionMissing
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (UserProfileManager_Exception_PluginDataDescriptionMissing), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string UserRightsManager_ModuleDescription
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (UserRightsManager_ModuleDescription), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string UserRightsManager_ModuleName
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (UserRightsManager_ModuleName), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string UserWithSameNameExists
  {
    get
    {
      return PathMedical.UserProfileManagement.Properties.Resources.ResourceManager.GetString(nameof (UserWithSameNameExists), PathMedical.UserProfileManagement.Properties.Resources.resourceCulture);
    }
  }
}
