// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ModelViewController.ModelBase`2
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement.Adapter;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.UserInterface.ModelViewController;

public abstract class ModelBase<TEntity, TAdapter> : IModel
  where TEntity : class, new()
  where TAdapter : AdapterBase<TEntity>
{
  public static ModelBase<TEntity, TAdapter> Instance
  {
    get => PathMedical.Singleton.Singleton<ModelBase<TEntity, TAdapter>>.Instance;
  }

  protected PathMedical.UserInterface.ModelViewController.ModelHelper<TEntity, TAdapter> ModelHelper { get; set; }

  protected ModelBase(params string[] propertyRelations)
  {
    this.ModelHelper = new PathMedical.UserInterface.ModelViewController.ModelHelper<TEntity, TAdapter>(new EventHandler<ModelChangedEventArgs>(this.ModelHelperChanged), propertyRelations);
  }

  public event EventHandler<ModelChangedEventArgs> Changed
  {
    add => this.ModelChangedListeners += value;
    remove => this.ModelChangedListeners -= value;
  }

  private event EventHandler<ModelChangedEventArgs> ModelChangedListeners;

  protected void ModelHelperChanged(object sender, ModelChangedEventArgs modelChangedEventArgs)
  {
    if (this.ModelChangedListeners == null)
      return;
    this.ModelChangedListeners((object) this, modelChangedEventArgs);
  }

  public void RefreshData() => this.LoadData();

  protected abstract ICollection<TEntity> DoLoadData(TAdapter adapter);

  protected void LoadData()
  {
    this.ModelHelper.LoadItems(new Func<TAdapter, ICollection<TEntity>>(this.DoLoadData));
  }
}
