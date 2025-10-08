// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.IsNotGuard
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class IsNotGuard : GuardBase
{
  private readonly IGuard guard;

  public IsNotGuard(IGuard guard)
  {
    if (guard == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (guard));
    this.Name = $"IsNotGuard ({guard.Name})";
    this.guard = guard;
  }

  public override bool Execute(TriggerEventArgs e) => !this.guard.Execute(e);
}
