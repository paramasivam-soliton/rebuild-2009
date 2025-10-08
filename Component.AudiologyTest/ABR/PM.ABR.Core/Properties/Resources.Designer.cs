// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.Properties.Resources
// Assembly: PM.ABR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 09E7728F-8618-4147-9D4A-E38CA516B245
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace PathMedical.ABR.Properties;

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
      if (PathMedical.ABR.Properties.Resources.resourceMan == null)
        PathMedical.ABR.Properties.Resources.resourceMan = new ResourceManager("PathMedical.ABR.Properties.Resources", typeof (PathMedical.ABR.Properties.Resources).Assembly);
      return PathMedical.ABR.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => PathMedical.ABR.Properties.Resources.resourceCulture;
    set => PathMedical.ABR.Properties.Resources.resourceCulture = value;
  }

  internal static string AbrTestManager_DataExchange_MappingFileNotFound
  {
    get
    {
      return PathMedical.ABR.Properties.Resources.ResourceManager.GetString(nameof (AbrTestManager_DataExchange_MappingFileNotFound), PathMedical.ABR.Properties.Resources.resourceCulture);
    }
  }

  internal static string AbrTestManager_DataExchange_StructureFileNotFound
  {
    get
    {
      return PathMedical.ABR.Properties.Resources.ResourceManager.GetString(nameof (AbrTestManager_DataExchange_StructureFileNotFound), PathMedical.ABR.Properties.Resources.resourceCulture);
    }
  }

  internal static string PresetWithSameNameExists
  {
    get
    {
      return PathMedical.ABR.Properties.Resources.ResourceManager.GetString(nameof (PresetWithSameNameExists), PathMedical.ABR.Properties.Resources.resourceCulture);
    }
  }
}
