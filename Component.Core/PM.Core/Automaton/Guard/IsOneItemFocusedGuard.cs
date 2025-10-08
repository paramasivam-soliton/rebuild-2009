// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.IsOneItemFocusedGuard`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class IsOneItemFocusedGuard<TEntity> : GuardBase where TEntity : class, new()
{
  private readonly IMultiSelectionModel<TEntity> model;

  public IsOneItemFocusedGuard(IMultiSelectionModel<TEntity> model)
  {
    this.model = model != null ? model : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (IsOneItemFocusedGuard<TEntity>);
  }

  public override bool Execute(TriggerEventArgs e) => this.model.IsOneItemFocused<TEntity>();
}
