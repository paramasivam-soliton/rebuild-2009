// Decompiled with JetBrains decompiler
// Type: PathMedical.ComponentResourceManagement
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.ResourceManager;

#nullable disable
namespace PathMedical;

public class ComponentResourceManagement
{
  private System.Resources.ResourceManager resourceManager;

  public static ComponentResourceManagement Instance
  {
    get => PathMedical.Singleton.Singleton<ComponentResourceManagement>.Instance;
  }

  private ComponentResourceManagement()
  {
    ResourceManagerProvider.GetResourceManager(typeof (PathMedical.Properties.Resources), out this.resourceManager);
  }

  public System.Resources.ResourceManager ResourceManager
  {
    get => this.resourceManager;
    set => this.resourceManager = value;
  }
}
