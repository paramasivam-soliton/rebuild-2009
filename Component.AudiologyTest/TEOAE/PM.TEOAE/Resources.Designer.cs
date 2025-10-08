// Decompiled with JetBrains decompiler
// Type: PathMedical.TEOAE.Resources
// Assembly: PM.TEOAE, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B7328F97-8442-4910-9451-35D76FF019BE
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.TEOAE.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace PathMedical.TEOAE;

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
      if (PathMedical.TEOAE.Resources.resourceMan == null)
        PathMedical.TEOAE.Resources.resourceMan = new ResourceManager("PathMedical.TEOAE.Resources", typeof (PathMedical.TEOAE.Resources).Assembly);
      return PathMedical.TEOAE.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public static CultureInfo Culture
  {
    get => PathMedical.TEOAE.Resources.resourceCulture;
    set => PathMedical.TEOAE.Resources.resourceCulture = value;
  }

  public static string TeoaeTestManager_DataExchange_MappingFileNotFound
  {
    get
    {
      return PathMedical.TEOAE.Resources.ResourceManager.GetString(nameof (TeoaeTestManager_DataExchange_MappingFileNotFound), PathMedical.TEOAE.Resources.resourceCulture);
    }
  }

  public static string TeoaeTestManager_DataExchange_StructureFileNotFound
  {
    get
    {
      return PathMedical.TEOAE.Resources.ResourceManager.GetString(nameof (TeoaeTestManager_DataExchange_StructureFileNotFound), PathMedical.TEOAE.Resources.resourceCulture);
    }
  }
}
