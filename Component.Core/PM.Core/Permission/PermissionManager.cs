// Decompiled with JetBrains decompiler
// Type: PathMedical.Permission.PermissionManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.Permission;

public class PermissionManager
{
  public static PermissionManager Instance => PathMedical.Singleton.Singleton<PermissionManager>.Instance;

  private PermissionManager()
  {
  }

  public IPermissionManager ConcretePermissionManager { get; set; }

  public bool HasPermission(Guid accessPermissionId)
  {
    return this.ConcretePermissionManager.HasPermission(accessPermissionId);
  }
}
