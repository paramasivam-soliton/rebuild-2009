// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.TriggerRequestScope
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Threading;

#nullable disable
namespace PathMedical.Automaton;

public class TriggerRequestScope : IDisposable
{
  private static int runningId;
  private static int nestedScopeCount;

  public TriggerRequestScope()
  {
    if (TriggerRequestScope.nestedScopeCount == 0)
      this.Id = Interlocked.Increment(ref TriggerRequestScope.runningId);
    Interlocked.Increment(ref TriggerRequestScope.nestedScopeCount);
  }

  public int Id { get; private set; }

  public static int CurrentId => TriggerRequestScope.runningId;

  public void Dispose() => Interlocked.Decrement(ref TriggerRequestScope.nestedScopeCount);
}
