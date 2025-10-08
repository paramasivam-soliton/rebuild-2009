// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.IApplicationComponentModule
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.ComponentModel;

#nullable disable
namespace PathMedical.UserInterface;

public interface IApplicationComponentModule
{
  Guid Id { get; }

  [Localizable(true)]
  string Name { get; }

  [Localizable(true)]
  string ShortcutName { get; }

  [Localizable(true)]
  string Description { get; }

  IView ContentControl { get; }

  object Image { get; }

  void Start(Trigger trigger);

  bool Suspend();

  bool IsAccessGranted(Trigger trigger);
}
