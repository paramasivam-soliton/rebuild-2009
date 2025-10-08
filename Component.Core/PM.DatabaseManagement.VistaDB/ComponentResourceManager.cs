// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.VistaDB.ComponentResourceManager
// Assembly: PM.DatabaseManagement.VistaDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 07F3111D-3061-4F48-BD47-8636F088222C
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DatabaseManagement.VistaDB.dll

using PathMedical.ResourceManager;

#nullable disable
namespace PathMedical.DatabaseManagement.VistaDB;

public class ComponentResourceManager
{
  private System.Resources.ResourceManager resourceManager;

  public static ComponentResourceManager Instance => PathMedical.Singleton.Singleton<ComponentResourceManager>.Instance;

  private ComponentResourceManager()
  {
    ResourceManagerProvider.GetResourceManager(typeof (PathMedical.DatabaseManagement.VistaDB.Properties.Resources), out this.resourceManager);
  }

  public System.Resources.ResourceManager ResourceManager
  {
    get => this.resourceManager;
    set => this.resourceManager = value;
  }
}
