// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.SQLServer.ComponentResourceManager
// Assembly: PM.DatabaseManagement.SQLServer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2B7FE44B-84F0-46A5-B0C2-93A6A55264BB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DatabaseManagement.SQLServer.dll

using PathMedical.ResourceManager;

#nullable disable
namespace PathMedical.DatabaseManagement.SQLServer;

public class ComponentResourceManager
{
  private System.Resources.ResourceManager resourceManager;

  public static ComponentResourceManager Instance => PathMedical.Singleton.Singleton<ComponentResourceManager>.Instance;

  private ComponentResourceManager()
  {
    ResourceManagerProvider.GetResourceManager(typeof (PathMedical.DatabaseManagement.SQLServer.Properties.Resources), out this.resourceManager);
  }

  public System.Resources.ResourceManager ResourceManager
  {
    get => this.resourceManager;
    set => this.resourceManager = value;
  }
}
