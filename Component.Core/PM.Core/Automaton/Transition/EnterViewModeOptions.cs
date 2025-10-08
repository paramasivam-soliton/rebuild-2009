// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Transition.EnterViewModeOptions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Guard;
using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.Automaton.Transition;

public class EnterViewModeOptions
{
  public EnterViewModeOptions(ViewModeType viewMode, State state)
  {
    if (state == (State) null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (state));
    this.ViewMode = viewMode;
    this.State = state;
    this.NeededPermissions = (IEnumerable<Guid>) new List<Guid>();
    this.PreventingPermissions = (IEnumerable<Guid>) new List<Guid>();
    this.PreventedByAnyViewMode = new List<ViewModeType>();
  }

  public ViewModeType ViewMode { get; protected set; }

  public List<ViewModeType> PreventedByAnyViewMode { get; protected set; }

  public State State { get; protected set; }

  public IEnumerable<Guid> NeededPermissions { get; protected set; }

  public IEnumerable<Guid> PreventingPermissions { get; protected set; }

  public IGuard PermissionGuard
  {
    get
    {
      PermissionGuardComposition permissionGuard = new PermissionGuardComposition();
      foreach (Guid neededPermission in this.NeededPermissions)
        permissionGuard.Add(new PathMedical.Automaton.Guard.PermissionGuard(neededPermission, true));
      return (IGuard) permissionGuard;
    }
  }

  public EnterViewModeOptions PreventedByViewMode(params ViewModeType[] viewMode)
  {
    if (viewMode != null && viewMode.Length != 0)
    {
      foreach (ViewModeType viewModeType in viewMode)
        this.PreventedByAnyViewMode.Add(viewModeType);
    }
    return this;
  }

  public EnterViewModeOptions NeedsAnyPermission(params Guid[] neededPermissions)
  {
    if (neededPermissions != null)
      ((List<Guid>) this.NeededPermissions).AddRange((IEnumerable<Guid>) neededPermissions);
    return this;
  }

  public EnterViewModeOptions PreventedByAnyPermission(params Guid[] preventingPermissions)
  {
    if (preventingPermissions != null)
      ((List<Guid>) this.PreventingPermissions).AddRange((IEnumerable<Guid>) preventingPermissions);
    return this;
  }
}
