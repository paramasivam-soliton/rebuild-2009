// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ModelViewController.ModelHelper`2
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.Exception;
using PathMedical.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PathMedical.UserInterface.ModelViewController;

public class ModelHelper<TEntity, TAdapter>
  where TEntity : class, new()
  where TAdapter : AdapterBase<TEntity>
{
  private readonly string[] relationsToInclude;
  private ICollection<TEntity> oldSelectedItems;
  private ICollection<TEntity> selectedItems;
  private TEntity currentItem;
  private TEntity oldCurrentItem;
  private List<TEntity> filterableItems;
  private List<TEntity> unfilteredItems;

  private event EventHandler<ModelChangedEventArgs> Changed;

  public string[] AdapterRelationNames => this.relationsToInclude;

  public SelectionModelType SelectionModel { get; set; }

  public ModelHelper(
    EventHandler<ModelChangedEventArgs> modelChangedEventHandler,
    [Localizable(false)] params string[] relationsToInclude)
  {
    this.SelectionModel = SelectionModelType.SingleSelectionModel;
    this.Changed = modelChangedEventHandler;
    this.relationsToInclude = relationsToInclude;
  }

  public ICollection<TEntity> SelectedItems
  {
    get => this.selectedItems;
    set
    {
      this.selectedItems = value;
      this.RaiseSelectionChangeEvent();
    }
  }

  public TEntity SelectedItem
  {
    get
    {
      if (this.SelectionModel != SelectionModelType.SingleSelectionModel)
        return this.currentItem;
      return this.SelectedItems == null ? default (TEntity) : this.SelectedItems.SingleOrDefault<TEntity>();
    }
    set
    {
      if (this.SelectionModel == SelectionModelType.SingleSelectionModel)
      {
        this.SelectedItems = (ICollection<TEntity>) new TEntity[1]
        {
          value
        };
      }
      else
      {
        this.currentItem = value;
        this.RaiseFocusChangedEvent();
      }
    }
  }

  public void RaiseSelectionChangeEvent()
  {
    if (this.Changed == null)
      return;
    if (this.SelectionModel == SelectionModelType.SingleSelectionModel)
    {
      TEntity changedObject = default (TEntity);
      if (this.SelectedItems != null)
        changedObject = this.SelectedItems.FirstOrDefault<TEntity>();
      this.Changed((object) this, ModelChangedEventArgs.Create<TEntity>(changedObject, ChangeType.SelectionChanged));
    }
    else
      this.Changed((object) this, ModelChangedEventArgs.Create<ICollection<TEntity>>(this.SelectedItems, ChangeType.SelectionChanged));
  }

  private void RaiseFocusChangedEvent()
  {
    if (this.Changed == null || this.SelectionModel != SelectionModelType.MultiSelectionModel)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<TEntity>(this.SelectedItem, ChangeType.SelectionChanged));
  }

  private void RaiseItemAddedEvent()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<TEntity>(this.SelectedItems.FirstOrDefault<TEntity>(), ChangeType.ItemAdded));
  }

  private void RaiseItemDeletedEvent()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<TEntity>(default (TEntity), ChangeType.ItemDeleted));
  }

  public List<TEntity> UnfilteredItems => this.unfilteredItems;

  public List<TEntity> Items
  {
    get => this.filterableItems;
    private set
    {
      this.filterableItems = value;
      this.SynchronizeAfterReload();
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<List<TEntity>>(this.filterableItems, ChangeType.ListLoaded));
      this.Changed((object) this, ModelChangedEventArgs.Create<ICollection<TEntity>>(this.SelectedItems, ChangeType.SelectionChanged));
      if (this.SelectionModel != SelectionModelType.MultiSelectionModel)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<TEntity>(this.SelectedItem, ChangeType.SelectionChanged));
    }
  }

  private void SynchronizeAfterReload()
  {
    if (this.SelectionModel == SelectionModelType.SingleSelectionModel && this.SelectedItems == null || this.SelectionModel == SelectionModelType.MultiSelectionModel && this.SelectedItems == null && (object) this.SelectedItem == null)
      return;
    Dictionary<object, TEntity> dictionary = this.Items.ToDictionary<TEntity, object>((Func<TEntity, object>) (ri => EntityHelper.GetPrimaryKey((object) ri)));
    if (this.SelectedItems != null)
    {
      TEntity[] array = this.SelectedItems.Where<TEntity>((Func<TEntity, bool>) (si => (object) si != null)).ToArray<TEntity>();
      for (int index = 0; index < array.Length; ++index)
      {
        object primaryKey = EntityHelper.GetPrimaryKey((object) array[index]);
        TEntity valueOrDefault = dictionary.GetValueOrDefault<object, TEntity>(primaryKey);
        if ((object) valueOrDefault != null)
          array[index] = valueOrDefault;
      }
      this.SelectedItems = (ICollection<TEntity>) array;
    }
    if (this.SelectionModel != SelectionModelType.MultiSelectionModel || (object) this.SelectedItem == null)
      return;
    object primaryKey1 = EntityHelper.GetPrimaryKey((object) this.currentItem);
    TEntity valueOrDefault1 = dictionary.GetValueOrDefault<object, TEntity>(primaryKey1);
    if ((object) valueOrDefault1 == null)
      return;
    this.SelectedItem = valueOrDefault1;
  }

  public void LoadItems(Func<TAdapter, ICollection<TEntity>> loadFunction)
  {
    using (DBScope dbScope = new DBScope())
    {
      try
      {
        if (Activator.CreateInstance(typeof (TAdapter), (object) dbScope) is TAdapter instance)
        {
          instance.LoadWithRelation(this.relationsToInclude);
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

  public void Filter(List<TEntity> filteredItems)
  {
    this.Items = new List<TEntity>((IEnumerable<TEntity>) filteredItems);
    if (this.SelectionModel == SelectionModelType.SingleSelectionModel)
    {
      this.SelectedItems = (ICollection<TEntity>) null;
    }
    else
    {
      this.SelectedItems = (ICollection<TEntity>) null;
      this.SelectedItem = default (TEntity);
    }
  }

  public void ResetFilter()
  {
    this.Items = this.unfilteredItems;
    if (this.SelectionModel == SelectionModelType.SingleSelectionModel)
    {
      this.SelectedItems = (ICollection<TEntity>) null;
    }
    else
    {
      this.SelectedItems = (ICollection<TEntity>) null;
      this.SelectedItem = default (TEntity);
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
          instance.LoadWithRelation(this.relationsToInclude);
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

  public void PrepareAddItem(TEntity newItem)
  {
    this.oldSelectedItems = this.SelectedItems;
    this.oldCurrentItem = this.currentItem;
    this.Items.Add(newItem);
    this.selectedItems = (ICollection<TEntity>) new TEntity[1]
    {
      newItem
    };
    if (this.SelectionModel == SelectionModelType.MultiSelectionModel)
      this.currentItem = newItem;
    this.RaiseItemAddedEvent();
  }

  public void CancelAddItem()
  {
    TEntity entity = this.SelectedItems.SingleOrDefault<TEntity>();
    if ((object) entity != null && this.Items.Contains(entity))
      this.Items.Remove(entity);
    this.RestoreItemSelection();
  }

  public void RestoreItemSelection()
  {
    if (this.oldSelectedItems == null || !this.oldSelectedItems.IsContentEqual<TEntity>(this.SelectedItems))
      this.SelectedItems = this.oldSelectedItems;
    if (this.SelectionModel != SelectionModelType.MultiSelectionModel || (object) this.oldCurrentItem != null && (object) this.oldCurrentItem == (object) this.currentItem)
      return;
    this.SelectedItem = this.oldCurrentItem;
  }

  public void Store()
  {
    if (this.SelectionModel == SelectionModelType.SingleSelectionModel)
    {
      if (this.SelectedItems == null)
        return;
      using (DBScope dbScope = new DBScope())
      {
        foreach (TEntity selectedItem in (IEnumerable<TEntity>) this.SelectedItems)
          this.Store(selectedItem);
        dbScope.Complete();
      }
    }
    else
    {
      if (this.SelectionModel != SelectionModelType.MultiSelectionModel || (object) this.SelectedItem == null)
        return;
      using (DBScope dbScope = new DBScope())
      {
        this.Store(this.SelectedItem);
        dbScope.Complete();
      }
    }
  }

  public void Store(TEntity item)
  {
    try
    {
      this.DoInAdapter((Action<TAdapter, TEntity>) ((adapter, it) => adapter.Store(it)), item);
      if (this.Items == null)
        this.Items = new List<TEntity>();
      bool flag = !this.Items.Contains(item);
      if (flag)
        this.Items.Add(item);
      if (this.Changed == null)
        return;
      ChangeType changeType = flag ? ChangeType.ItemAdded : ChangeType.ItemEdited;
      this.Changed((object) this, ModelChangedEventArgs.Create<TEntity>(item, changeType));
    }
    catch (ExecutionException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagement.Instance.ResourceManager.GetString("CantStoreRecord"), (System.Exception) ex);
    }
  }

  public void Delete()
  {
    if (this.SelectedItems == null)
      return;
    try
    {
      using (DBScope dbScope = new DBScope())
      {
        foreach (TEntity selectedItem in (IEnumerable<TEntity>) this.SelectedItems)
          this.DoInAdapter((Action<TAdapter, TEntity>) ((adapter, item) => adapter.Delete(item)), selectedItem);
        dbScope.Complete();
      }
      foreach (TEntity selectedItem in (IEnumerable<TEntity>) this.SelectedItems)
        this.Items.Remove(selectedItem);
      this.selectedItems = (ICollection<TEntity>) null;
      this.currentItem = default (TEntity);
      this.RaiseItemDeletedEvent();
    }
    catch (ExecutionException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagement.Instance.ResourceManager.GetString("CantDeleteRecord"), (System.Exception) ex);
    }
  }

  public void Delete(TEntity entity)
  {
    if ((object) entity == null)
      return;
    try
    {
      using (DBScope dbScope = new DBScope())
      {
        this.DoInAdapter((Action<TAdapter, TEntity>) ((adapter, item) => adapter.Delete(item)), entity);
        dbScope.Complete();
      }
      if (this.Items != null && this.Items.Contains(entity))
        this.Items.Remove(entity);
      this.selectedItems = (ICollection<TEntity>) null;
      this.currentItem = default (TEntity);
      this.RaiseItemDeletedEvent();
    }
    catch (ExecutionException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagement.Instance.ResourceManager.GetString("CantDeleteRecord"), (System.Exception) ex);
    }
  }

  public void MarkItemAsDeleted()
  {
    this.Store();
    foreach (TEntity selectedItem in (IEnumerable<TEntity>) this.SelectedItems)
      this.Items.Remove(selectedItem);
    this.selectedItems = (ICollection<TEntity>) null;
    this.currentItem = default (TEntity);
    this.RaiseItemDeletedEvent();
  }

  public void MarkItemAsDeleted(TEntity entity)
  {
    this.Store(entity);
    this.Items.Remove(entity);
    this.selectedItems = (ICollection<TEntity>) null;
    this.currentItem = default (TEntity);
    this.RaiseItemDeletedEvent();
  }

  public void DoInAdapter(Action<TAdapter, TEntity> action, TEntity item)
  {
    using (DBScope dbScope = new DBScope())
    {
      if (!(Activator.CreateInstance(typeof (TAdapter), (object) dbScope) is TAdapter instance))
        return;
      instance.LoadWithRelation(this.relationsToInclude);
      action(instance, item);
      dbScope.Complete();
    }
  }

  public void RefreshSelectedItems()
  {
    if (this.SelectedItems == null)
      return;
    using (new DBScope())
    {
      foreach (TEntity selectedItem in (IEnumerable<TEntity>) this.SelectedItems)
        this.DoInAdapter((Action<TAdapter, TEntity>) ((adapter, item) => adapter.RefreshEntity(item)), selectedItem);
    }
    this.RaiseSelectionChangeEvent();
  }
}
