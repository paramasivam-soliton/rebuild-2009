// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.CommandBase
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.ComponentModel;

#nullable disable
namespace PathMedical.Automaton.Command;

public abstract class CommandBase : ICommand
{
  [Localizable(false)]
  public string Description { get; set; }

  public bool IsAsynchronousCommand { get; protected set; }

  [Localizable(false)]
  public string Name { get; protected set; }

  public object Owner { get; protected set; }

  public virtual TriggerEventArgs TriggerEventArgs { get; set; }

  public abstract void Execute();
}
