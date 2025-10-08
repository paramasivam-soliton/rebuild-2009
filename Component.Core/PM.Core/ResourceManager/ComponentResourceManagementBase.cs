// Decompiled with JetBrains decompiler
// Type: PathMedical.ResourceManager.ComponentResourceManagementBase`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.ResourceManager;

public class ComponentResourceManagementBase<T> where T : class
{
  private System.Resources.ResourceManager resourceManager;

  public static ComponentResourceManagementBase<T> Instance
  {
    get => PathMedical.Singleton.Singleton<ComponentResourceManagementBase<T>>.Instance;
  }

  protected ComponentResourceManagementBase()
  {
    ResourceManagerProvider.GetResourceManager(typeof (T), out this.resourceManager);
    GlobalResourceEnquirer.Instance.RegisterResourceManager(this.resourceManager);
  }

  public System.Resources.ResourceManager ResourceManager
  {
    get => this.resourceManager;
    set => this.resourceManager = value;
  }
}
