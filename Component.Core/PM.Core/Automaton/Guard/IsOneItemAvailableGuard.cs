// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.IsOneItemAvailableGuard`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class IsOneItemAvailableGuard<TEntity> : GuardBase where TEntity : class, new()
{
  private readonly IModel model;

  public IsOneItemAvailableGuard(ISingleSelectionModel<TEntity> model)
  {
    this.model = model != null ? (IModel) model : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
  }

  public IsOneItemAvailableGuard(IMultiSelectionModel<TEntity> model)
  {
    this.model = model != null ? (IModel) model : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
  }

  public override bool Execute(TriggerEventArgs e)
  {
    bool flag = false;
    if (this.model != null)
    {
      flag = true;
      if (this.model is ISingleSelectionModel<TEntity>)
        flag = (this.model as ISingleSelectionModel<TEntity>).IsOneItemAvailable<TEntity>();
      if (this.model is IMultiSelectionModel<TEntity>)
        flag = (this.model as IMultiSelectionModel<TEntity>).IsOneItemSelected<TEntity>();
    }
    return flag;
  }
}
