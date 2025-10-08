// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.PermissionGuardComposition
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Collections.Generic;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class PermissionGuardComposition : PermissionGuard
{
  private readonly List<PermissionGuard> permissionGuards = new List<PermissionGuard>();

  public int Count
  {
    get
    {
      int count = 0;
      if (this.permissionGuards != null)
        count = this.permissionGuards.Count;
      return count;
    }
  }

  public void Add(PermissionGuard permissionGuard)
  {
    if (permissionGuard == null)
      return;
    this.permissionGuards.Add(permissionGuard);
  }

  public override bool Execute(TriggerEventArgs e)
  {
    if (this.permissionGuards == null || this.permissionGuards.Count == 0)
      return true;
    foreach (GuardBase permissionGuard in this.permissionGuards)
    {
      if (permissionGuard.Execute(TriggerEventArgs.Empty))
        return true;
    }
    return false;
  }
}
