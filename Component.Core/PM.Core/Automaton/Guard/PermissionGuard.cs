// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.PermissionGuard
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Permission;
using System;
using System.Diagnostics;

#nullable disable
namespace PathMedical.Automaton.Guard;

[DebuggerDisplay("AccessPermission: {accessPermissionId}")]
public class PermissionGuard : GuardBase
{
  private readonly bool accessPermissionFlag;
  private readonly Guid accessPermissionId;

  public PermissionGuard()
  {
    this.Name = nameof (PermissionGuard);
    this.accessPermissionId = Guid.Empty;
    this.accessPermissionFlag = false;
  }

  public PermissionGuard(Guid accessPermissionId, bool accessPermissionFlag)
  {
    if (accessPermissionId.Equals(Guid.Empty))
      throw ExceptionFactory.Instance.CreateException<ArgumentException>(nameof (accessPermissionId));
    this.Name = nameof (PermissionGuard);
    this.accessPermissionId = accessPermissionId;
    this.accessPermissionFlag = accessPermissionFlag;
  }

  public override bool Execute(TriggerEventArgs e)
  {
    return PermissionManager.Instance.HasPermission(this.accessPermissionId) == this.accessPermissionFlag;
  }
}
