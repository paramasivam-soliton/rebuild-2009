// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ModelViewController.IController`2
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton;
using System;

#nullable disable
namespace PathMedical.UserInterface.ModelViewController;

public interface IController<TModel, TView> : IDisposable
  where TModel : class, IModel
  where TView : class, IView
{
  TModel Model { get; }

  TView View { get; }

  void OnViewRequestsControllerAction(object sender, TriggerEventArgs triggerEventArgs);

  bool IsAccessGranted(Trigger trigger);
}
