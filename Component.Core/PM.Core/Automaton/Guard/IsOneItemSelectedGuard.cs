// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.IsOneItemSelectedGuard`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class IsOneItemSelectedGuard<TEntity> : GuardBase where TEntity : class, new()
{
  private readonly ISingleSelectionModel<TEntity> model;

  public IsOneItemSelectedGuard(ISingleSelectionModel<TEntity> model)
  {
    this.model = model != null ? model : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (IsOneItemSelectedGuard<TEntity>);
  }

  public override bool Execute(TriggerEventArgs e) => this.model.IsOneItemSelected<TEntity>();
}
