// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Properties.Resources
// Assembly: PM.SiteAndFacilityManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F9CADFD-E9C9-4783-BA9E-8D20FAD3C075
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Properties;

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
      if (PathMedical.SiteAndFacilityManagement.Properties.Resources.resourceMan == null)
        PathMedical.SiteAndFacilityManagement.Properties.Resources.resourceMan = new ResourceManager("PathMedical.SiteAndFacilityManagement.Properties.Resources", typeof (PathMedical.SiteAndFacilityManagement.Properties.Resources).Assembly);
      return PathMedical.SiteAndFacilityManagement.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public static CultureInfo Culture
  {
    get => PathMedical.SiteAndFacilityManagement.Properties.Resources.resourceCulture;
    set => PathMedical.SiteAndFacilityManagement.Properties.Resources.resourceCulture = value;
  }

  public static string CannotDeleteRecordInUse
  {
    get
    {
      return PathMedical.SiteAndFacilityManagement.Properties.Resources.ResourceManager.GetString(nameof (CannotDeleteRecordInUse), PathMedical.SiteAndFacilityManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string FacilityWithSameCodeExists
  {
    get
    {
      return PathMedical.SiteAndFacilityManagement.Properties.Resources.ResourceManager.GetString(nameof (FacilityWithSameCodeExists), PathMedical.SiteAndFacilityManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string RecordWithSameCodeExists
  {
    get
    {
      return PathMedical.SiteAndFacilityManagement.Properties.Resources.ResourceManager.GetString(nameof (RecordWithSameCodeExists), PathMedical.SiteAndFacilityManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string SiteManager_Exception_PluginDataDescriptionMissing
  {
    get
    {
      return PathMedical.SiteAndFacilityManagement.Properties.Resources.ResourceManager.GetString(nameof (SiteManager_Exception_PluginDataDescriptionMissing), PathMedical.SiteAndFacilityManagement.Properties.Resources.resourceCulture);
    }
  }

  public static string SiteWithSameCodeExists
  {
    get
    {
      return PathMedical.SiteAndFacilityManagement.Properties.Resources.ResourceManager.GetString(nameof (SiteWithSameCodeExists), PathMedical.SiteAndFacilityManagement.Properties.Resources.resourceCulture);
    }
  }
}
