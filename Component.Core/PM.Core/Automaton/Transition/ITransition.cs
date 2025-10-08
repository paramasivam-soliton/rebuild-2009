// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Transition.ITransition
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton.Guard;

#nullable disable
namespace PathMedical.Automaton.Transition;

public interface ITransition
{
  GuardComposition Guards { get; }

  State State { get; }
}
