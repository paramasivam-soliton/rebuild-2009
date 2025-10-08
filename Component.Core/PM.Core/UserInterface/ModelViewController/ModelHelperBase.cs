// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ModelViewController.ModelHelperBase`2
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.UserInterface.ModelViewController;

public abstract class ModelHelperBase<TEntity, TAdapter>
  where TEntity : class, new()
  where TAdapter : AdapterBase<TEntity>
{
  private List<TEntity> filterableItems;
  private List<TEntity> unfilteredItems;
  private TEntity oldCurrentItem;

  protected abstract void RaiseChangeEvent(object sender, ModelChangedEventArgs e);

  protected string[] RelationsToInclude { get; set; }

  public List<TEntity> Items
  {
    get => this.filterableItems;
    private set
    {
      this.filterableItems = value;
      this.SynchronizeAfterReload();
      this.RaiseChangeEvent((object) this, ModelChangedEventArgs.Create<List<TEntity>>(this.filterableItems, ChangeType.ListLoaded));
    }
  }

  public void LoadItems(Func<TAdapter, ICollection<TEntity>> loadFunction)
  {
    using (DBScope dbScope = new DBScope())
    {
      try
      {
        if (Activator.CreateInstance(typeof (TAdapter), (object) dbScope) is TAdapter instance)
        {
          instance.LoadWithRelation(this.RelationsToInclude);
          this.Items = new List<TEntity>((IEnumerable<TEntity>) loadFunction(instance).ToList<TEntity>());
          this.unfilteredItems = this.Items;
        }
        dbScope.Complete();
      }
      catch (ConnectionException ex)
      {
        throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagement.Instance.ResourceManager.GetString("DatabaseConnectionFailed"), (System.Exception) ex);
      }
      catch (ExecutionException ex)
      {
        throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagement.Instance.ResourceManager.GetString("CantLoadRecords"), (System.Exception) ex);
      }
    }
  }

  public void LoadHistoryItems(
    Func<TAdapter, ICollection<TEntity>> loadFunction,
    params string[] relationsToLoad)
  {
    using (DBScope dbScope = new DBScope())
    {
      try
      {
        if (Activator.CreateInstance(typeof (TAdapter), (object) dbScope) is TAdapter instance)
        {
          instance.LoadWithRelation(relationsToLoad);
          this.Items = new List<TEntity>((IEnumerable<TEntity>) new List<TEntity>((IEnumerable<TEntity>) loadFunction(instance)));
        }
        dbScope.Complete();
      }
      catch (ConnectionException ex)
      {
        throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagement.Instance.ResourceManager.GetString("DatabaseConnectionFailed"), (System.Exception) ex);
      }
      catch (ExecutionException ex)
      {
        throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagement.Instance.ResourceManager.GetString("CantLoadRecords"), (System.Exception) ex);
      }
    }
  }

  public abstract void SynchronizeAfterReload();

  protected TEntity CurrentItem { get; set; }

  public void PrepareAddItem(TEntity newItem)
  {
    this.oldCurrentItem = this.CurrentItem;
    this.Items.Add(newItem);
    this.CurrentItem = newItem;
    this.RaiseChangeEvent((object) this, ModelChangedEventArgs.Create<TEntity>(this.CurrentItem, ChangeType.ItemAdded));
  }

  public void CancelAddItem()
  {
    if ((object) this.CurrentItem != null && this.Items.Contains(this.CurrentItem))
      this.Items.Remove(this.CurrentItem);
    this.RestoreItemSelection();
  }

  public void RestoreItemSelection()
  {
    this.CurrentItem = this.oldCurrentItem;
    this.RaiseChangeEvent((object) this, ModelChangedEventArgs.Create<TEntity>(this.CurrentItem, ChangeType.SelectionChanged));
  }

  public void Store()
  {
    if ((object) this.CurrentItem == null)
      return;
    this.Store(this.CurrentItem);
  }

  public void Store(TEntity item)
  {
    try
    {
      this.DoInAdapter((Action<TAdapter, TEntity>) ((adapter, it) => adapter.Store(it)), item);
      if (this.Items == null)
        this.Items = new List<TEntity>();
      int num = !this.Items.Contains(item) ? 1 : 0;
      if (num != 0)
        this.Items.Add(item);
      ChangeType changeType = num != 0 ? ChangeType.ItemAdded : ChangeType.ItemEdited;
      this.RaiseChangeEvent((object) this, ModelChangedEventArgs.Create<TEntity>(item, changeType));
    }
    catch (ExecutionException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagement.Instance.ResourceManager.GetString("CantStoreRecord"), (System.Exception) ex);
    }
  }

  public void Delete()
  {
    if ((object) this.CurrentItem == null)
      return;
    try
    {
      this.DoInAdapter((Action<TAdapter, TEntity>) ((adapter, item) => adapter.Delete(item)), this.CurrentItem);
      if (this.Items.Contains(this.CurrentItem))
        this.Items.Remove(this.CurrentItem);
      this.CurrentItem = default (TEntity);
      this.oldCurrentItem = default (TEntity);
      this.RaiseChangeEvent((object) this, ModelChangedEventArgs.Create<TEntity>(default (TEntity), ChangeType.ItemDeleted));
    }
    catch (ExecutionException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagement.Instance.ResourceManager.GetString("CantDeleteRecord"), (System.Exception) ex);
    }
  }

  public void DoInAdapter(Action<TAdapter, TEntity> action, TEntity item)
  {
    using (DBScope dbScope = new DBScope())
    {
      if (!(Activator.CreateInstance(typeof (TAdapter), (object) dbScope) is TAdapter instance))
        return;
      instance.LoadWithRelation(this.RelationsToInclude);
      action(instance, item);
      dbScope.Complete();
    }
  }
}
