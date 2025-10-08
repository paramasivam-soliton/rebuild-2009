// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.IsFuncTrueGuard
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class IsFuncTrueGuard : GuardBase
{
  private readonly Func<bool> func;

  public IsFuncTrueGuard(Func<bool> func)
  {
    this.func = func != null ? func : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (func));
  }

  public override bool Execute(TriggerEventArgs e) => this.func();
}
