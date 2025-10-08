// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ResourceManager.ResXResourceManager
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using System;
using System.Reflection;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.ResourceManager;

public class ResXResourceManager : ResourcesResourceManager
{
  public ResXResourceManager(string baseName, Assembly assembly)
    : base(baseName, assembly)
  {
  }

  public ResXResourceManager(string baseName)
    : base(baseName)
  {
  }

  public ResXResourceManager(Type resourceType)
    : base(resourceType)
  {
  }

  protected override void Initialize(string baseName, Assembly assembly)
  {
    this.Extension = "resx";
    base.Initialize(baseName, assembly);
  }
}
