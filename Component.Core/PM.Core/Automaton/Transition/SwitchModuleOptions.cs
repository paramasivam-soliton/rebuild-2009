// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Transition.SwitchModuleOptions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Guard;
using PathMedical.Exception;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.Automaton.Transition;

public class SwitchModuleOptions
{
  public SwitchModuleOptions(Trigger trigger, Type moduleType)
  {
    if (trigger == (Trigger) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (trigger));
    if (moduleType == (Type) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (moduleType));
    this.Trigger = trigger;
    this.ModuleType = moduleType;
    this.NeededPermissions = (IEnumerable<Guid>) new List<Guid>();
  }

  public Trigger Trigger { get; protected set; }

  public Type ModuleType { get; protected set; }

  public IEnumerable<Guid> NeededPermissions { get; protected set; }

  public SwitchModuleOptions NeedsAnyPermission(params Guid[] neededPermissions)
  {
    if (neededPermissions != null)
      ((List<Guid>) this.NeededPermissions).AddRange((IEnumerable<Guid>) neededPermissions);
    return this;
  }

  public IGuard CreateGuard()
  {
    PermissionGuardComposition guard = new PermissionGuardComposition();
    foreach (Guid neededPermission in this.NeededPermissions)
      guard.Add(new PermissionGuard(neededPermission, true));
    return (IGuard) guard;
  }
}
