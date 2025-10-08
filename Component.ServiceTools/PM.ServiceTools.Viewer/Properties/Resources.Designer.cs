// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.Properties.Resources
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.Properties;

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
      if (PathMedical.ServiceTools.WindowsForms.Properties.Resources.resourceMan == null)
        PathMedical.ServiceTools.WindowsForms.Properties.Resources.resourceMan = new ResourceManager("PathMedical.ServiceTools.WindowsForms.Properties.Resources", typeof (PathMedical.ServiceTools.WindowsForms.Properties.Resources).Assembly);
      return PathMedical.ServiceTools.WindowsForms.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => PathMedical.ServiceTools.WindowsForms.Properties.Resources.resourceCulture;
    set => PathMedical.ServiceTools.WindowsForms.Properties.Resources.resourceCulture = value;
  }

  internal static Bitmap GN_OperationFailed
  {
    get
    {
      return (Bitmap) PathMedical.ServiceTools.WindowsForms.Properties.Resources.ResourceManager.GetObject(nameof (GN_OperationFailed), PathMedical.ServiceTools.WindowsForms.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap GN_OperationRunning
  {
    get
    {
      return (Bitmap) PathMedical.ServiceTools.WindowsForms.Properties.Resources.ResourceManager.GetObject(nameof (GN_OperationRunning), PathMedical.ServiceTools.WindowsForms.Properties.Resources.resourceCulture);
    }
  }

  internal static Bitmap GN_OperationSuccessfully
  {
    get
    {
      return (Bitmap) PathMedical.ServiceTools.WindowsForms.Properties.Resources.ResourceManager.GetObject(nameof (GN_OperationSuccessfully), PathMedical.ServiceTools.WindowsForms.Properties.Resources.resourceCulture);
    }
  }
}
