// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.CheckPermissionCommand
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Permission;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class CheckPermissionCommand : CommandBase
{
  private readonly Guid accessPermissionId;

  public CheckPermissionCommand(Guid accessPermissionId)
  {
    if (accessPermissionId.Equals(Guid.Empty))
      throw ExceptionFactory.Instance.CreateException<ArgumentException>(nameof (accessPermissionId));
    this.Name = nameof (CheckPermissionCommand);
    this.accessPermissionId = accessPermissionId;
  }

  public override void Execute()
  {
    PermissionManager.Instance.HasPermission(this.accessPermissionId);
  }
}
