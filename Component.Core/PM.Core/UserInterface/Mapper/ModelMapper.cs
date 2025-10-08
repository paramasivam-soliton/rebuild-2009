// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Mapper.ModelMapper`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Extensions;
using PathMedical.UserInterface.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.UserInterface.Mapper;

public class ModelMapper<TEntity> : 
  IModelMapper<TEntity>,
  IEnumerable<ModelMapItem<TEntity>>,
  IEnumerable
  where TEntity : class
{
  private ICollection<TEntity> entities;
  private readonly List<ModelMapItem<TEntity>> modelMapItems = new List<ModelMapItem<TEntity>>();
  private bool isEditingEnabled;

  public ModelMapper(bool isEditingEnabled) => this.isEditingEnabled = isEditingEnabled;

  public void Add(
    Expression<Func<TEntity, object>> entityPropertyAccess,
    object ui,
    Expression<Func<object>> uiPropertyAccess)
  {
    if (!(ui is IControl))
      return;
    ModelMapItem<TEntity> modelMapItem = new ModelMapItem<TEntity>(entityPropertyAccess, ui as IControl, uiPropertyAccess);
    modelMapItem.EnableViewControl(this.isEditingEnabled);
    this.modelMapItems.Add(modelMapItem);
  }

  public void Add(
    Expression<Func<TEntity, object>> entityPropertyAccess,
    object ui)
  {
    if (!(ui is IControl))
      return;
    ModelMapItem<TEntity> modelMapItem = new ModelMapItem<TEntity>(entityPropertyAccess, ui as IControl, (Expression<Func<object>>) (() => (ui as IControl).Value));
    modelMapItem.EnableViewControl(this.isEditingEnabled);
    this.modelMapItems.Add(modelMapItem);
  }

  public void CopyModelToUI(TEntity entity)
  {
    if ((object) entity != null)
    {
      this.CopyModelToUI((ICollection<TEntity>) new TEntity[1]
      {
        entity
      });
    }
    else
    {
      this.entities = (ICollection<TEntity>) new List<TEntity>();
      this.CopyModelToUI(this.entities);
    }
  }

  public void CopyModelToUI(ICollection<TEntity> modelEntities)
  {
    this.entities = modelEntities;
    this.modelMapItems.Where<ModelMapItem<TEntity>>((Func<ModelMapItem<TEntity>, bool>) (modelMapItem => modelMapItem != null)).ForEach<ModelMapItem<TEntity>>((Action<ModelMapItem<TEntity>>) (modelMapItem => modelMapItem.CopyModelToViewControl(this.entities, this.isEditingEnabled)));
  }

  public void CopyUIToModel()
  {
    this.modelMapItems.ForEach((Action<ModelMapItem<TEntity>>) (modelMapItem => modelMapItem.CopyViewControlToModel(this.entities)));
  }

  public void SetUIEnabled(bool isUIEnabled)
  {
    this.isEditingEnabled = isUIEnabled;
    this.modelMapItems.Where<ModelMapItem<TEntity>>((Func<ModelMapItem<TEntity>, bool>) (modelMapItem => modelMapItem != null)).ForEach<ModelMapItem<TEntity>>((Action<ModelMapItem<TEntity>>) (modelMapItem => modelMapItem.EnableViewControl(this.isEditingEnabled)));
  }

  public void SetUIEnabledForced(bool isUIEnabledForced, params object[] uis)
  {
    foreach (object ui1 in uis)
    {
      object ui = ui1;
      this.modelMapItems.First<ModelMapItem<TEntity>>((Func<ModelMapItem<TEntity>, bool>) (mmi => mmi.ViewControl == ui)).IsEnabledForced = new bool?(isUIEnabledForced);
    }
  }

  public IEnumerator<ModelMapItem<TEntity>> GetEnumerator()
  {
    return (IEnumerator<ModelMapItem<TEntity>>) this.modelMapItems.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.modelMapItems.GetEnumerator();
}
