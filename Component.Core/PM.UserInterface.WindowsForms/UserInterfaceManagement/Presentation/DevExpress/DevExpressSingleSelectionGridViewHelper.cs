// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress.DevExpressSingleSelectionGridViewHelper`1
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Property;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;

[DebuggerDisplay("{this.gridView.Name} {this.dataMember}")]
public class DevExpressSingleSelectionGridViewHelper<T> where T : class, new()
{
  private static readonly ILogger Logger = LogFactory.Instance.Create("UserInterface");
  private readonly PathMedical.UserInterface.WindowsForms.ModelViewController.View view;
  private readonly GridView gridView;
  private readonly string dataMember;
  private bool refreshingDataSource;
  private T lastFocusedObject;
  private int lastFocusedHandle = int.MinValue;
  private int[] lastSelectedHandles = new int[0];
  private bool isEventHandled;
  private readonly Trigger changeSelectionTrigger = Triggers.ChangeSelection;
  private readonly Trigger changeFocusTrigger = Triggers.ChangeCurrent;

  private ICollection<T> DataSource
  {
    set => this.gridView.GridControl.DataSource = (object) value;
    get
    {
      return this.gridView.GridControl.DataSource == null ? (ICollection<T>) null : this.gridView.GridControl.DataSource as ICollection<T>;
    }
  }

  public DevExpressSingleSelectionGridViewHelper(PathMedical.UserInterface.WindowsForms.ModelViewController.View view, GridView gridView)
  {
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    if (gridView == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (gridView));
    if (gridView.IsMultiSelect)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("gridView is multi selection");
    this.view = view;
    this.gridView = gridView;
    this.gridView.FocusedRowChanged += new FocusedRowChangedEventHandler(this.FocusedRowChanged);
    this.gridView.CustomDrawEmptyForeground += new CustomDrawEventHandler(DevExpressSingleSelectionGridViewHelper<T>.DisplayEmptyGridMessage);
    this.gridView.OptionsCustomization.AllowFilter = false;
    this.gridView.OptionsView.ShowDetailButtons = false;
    this.gridView.OptionsDetail.EnableMasterViewMode = false;
    this.gridView.OptionsDetail.AllowZoomDetail = false;
    this.gridView.OptionsDetail.AutoZoomDetail = false;
    this.gridView.OptionsDetail.ShowDetailTabs = false;
    this.gridView.OptionsFilter.AllowFilterEditor = false;
    this.gridView.OptionsView.ShowGroupPanel = false;
    DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} has created its grid view helper.", (object) this);
  }

  public DevExpressSingleSelectionGridViewHelper(PathMedical.UserInterface.WindowsForms.ModelViewController.View view, GridView gridView, IModel model)
    : this(view, gridView)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
    DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} listens on change events of {1}", (object) this, (object) model);
  }

  public DevExpressSingleSelectionGridViewHelper(
    PathMedical.UserInterface.WindowsForms.ModelViewController.View view,
    GridView grid,
    Trigger changeFocusTrigger)
    : this(view, grid)
  {
    DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} has been requested to use {1} for selection change requests.", (object) this, (object) this.changeSelectionTrigger);
    this.changeSelectionTrigger = changeFocusTrigger;
  }

  public DevExpressSingleSelectionGridViewHelper(
    PathMedical.UserInterface.WindowsForms.ModelViewController.View view,
    GridView grid,
    IModel model,
    Trigger changeFocusTrigger)
    : this(view, grid, model)
  {
    DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} has been requested to use {1} for selection change requests.", (object) this, (object) this.changeSelectionTrigger);
    this.changeSelectionTrigger = changeFocusTrigger;
  }

  public DevExpressSingleSelectionGridViewHelper(
    PathMedical.UserInterface.WindowsForms.ModelViewController.View view,
    GridView grid,
    Trigger changeFocusTrigger,
    string dataMember)
    : this(view, grid, changeFocusTrigger)
  {
    DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} has been instructed to get its data from sub property {1}. ", (object) this, (object) dataMember);
    this.dataMember = dataMember;
  }

  public DevExpressSingleSelectionGridViewHelper(
    PathMedical.UserInterface.WindowsForms.ModelViewController.View view,
    GridView grid,
    IModel model,
    Trigger changeFocusTrigger,
    string dataMember)
    : this(view, grid, model, changeFocusTrigger)
  {
    DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} has been instructed to get its data from sub property {1}. ", (object) this, (object) dataMember);
    this.dataMember = dataMember;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.refreshingDataSource || this.gridView == null || this.gridView.GridControl == null)
      return;
    if (string.IsNullOrEmpty(this.dataMember) && e.Type != typeof (T))
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} ignores model update since the changed object of type {1} which has no affect to this grid view.", (object) this, (object) e.Type);
    }
    else
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} receives a model udpate {1} from {2}. {3}", (object) this, (object) e.ToShortString(), sender, (object) e, (object) this);
      if (this.gridView.GridControl.InvokeRequired)
        this.gridView.GridControl.BeginInvoke((Delegate) new DevExpressSingleSelectionGridViewHelper<T>.UpdateModelDataCallBack(this.UpdateGridData), sender, (object) e);
      else
        this.UpdateGridData(sender, e);
    }
  }

  private void UpdateGridData(object sender, ModelChangedEventArgs e)
  {
    if (e == null)
      return;
    try
    {
      this.isEventHandled = true;
      switch (e.ChangeType)
      {
        case ChangeType.ItemAdded:
          if (!(e.Type == typeof (T)))
            break;
          this.ConsolidateDataSource();
          T changedObject = e.ChangedObject as T;
          DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} is selecting new item: [{1}]", (object) this, (object) PropertyHelper.GetPropertyTypeName((object) changedObject));
          int rowHandle = this.FindRowHandles(changedObject)[0];
          this.FocusAndSelectItem(rowHandle, rowHandle);
          break;
        case ChangeType.ItemEdited:
          if (!(e.Type == typeof (T)))
            break;
          DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} is refreshing the item {1} since it has been edited.", (object) this, (object) PropertyHelper.GetPropertyTypeName(e.ChangedObject));
          this.RefreshGridItem(e.ChangedObject as T);
          break;
        case ChangeType.ItemDeleted:
          if (!(e.Type == typeof (T)))
            break;
          DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} is consolidating its data source due to an deleted item", (object) this);
          this.ConsolidateDataSource();
          DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} is requests to change the current because an item of its data source has been deleted.");
          this.RequestChangeSelection();
          break;
        case ChangeType.SelectionChanged:
          DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} is consolidating its data source due to a selection change", (object) this);
          this.ConsolidateDataSource();
          if (e.Type == typeof (T))
          {
            if (e.ChangedObject != null)
            {
              object obj = (object) (e.ChangedObject as ICollection<T>);
              if (obj == null)
                obj = (object) new T[1]
                {
                  e.ChangedObject as T
                };
              ICollection<T> source = (ICollection<T>) obj;
              if ((object) source.FirstOrDefault<T>() != null)
              {
                DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} is changing the selected item.", (object) this);
                this.FocusItem(source.FirstOrDefault<T>());
                break;
              }
              if (this.gridView.FocusedRowHandle < 0)
                break;
              DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} got a request that selection has changed to an empty object. Requesting to change the current item that the grid has been selected automatically.", (object) this);
              this.RequestChangeSelection();
              break;
            }
            if (this.gridView.FocusedRowHandle < 0)
              break;
            DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} got a request that selection has changed to an empty object. Requesting to change the current item that the grid has been selected automatically.", (object) this);
            this.RequestChangeSelection();
            break;
          }
          if (e.IsList || string.IsNullOrEmpty(this.dataMember))
            break;
          DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} got a selection changed request and starts to refresh the nested data source.", (object) this);
          this.RefreshNestedDataSource(e.ChangedObject);
          break;
        case ChangeType.ListLoaded:
          if (e.ChangedObject is IList<T> && string.IsNullOrEmpty(this.dataMember))
          {
            this.DataSource = (ICollection<T>) e.ChangedObject;
            DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} has updated its data source. {1}", (object) this, (object) this.GetFullInformation());
            break;
          }
          if (!(e.ChangedObject is IList<T>))
            break;
          this.DataSource = (ICollection<T>) e.ChangedObject;
          DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} has updated its data source. {1}", (object) this, (object) this.GetFullInformation());
          break;
      }
    }
    finally
    {
      this.isEventHandled = false;
    }
  }

  private void FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
  {
    if (this.isEventHandled)
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} already handles the 'focus changed' event. This might be a nested call.", (object) this);
    else if (this.refreshingDataSource)
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} is now updating its data source and ignore 'focus changed' events.", (object) this);
    }
    else
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} starts handling 'focus changed' event emmitted by [{1}]. Focused row handle is [{2}]. Old row handle is [{3}]", (object) this, sender, (object) e.FocusedRowHandle, (object) e.PrevFocusedRowHandle);
      this.RequestChangeSelection();
    }
  }

  public T GetSelectedEntity()
  {
    T selectedEntity = default (T);
    if (this.gridView != null)
    {
      int[] selectedRows = this.gridView.GetSelectedRows();
      if (selectedRows != null && selectedRows.Length != 0)
        selectedEntity = this.gridView.GetRow(selectedRows[0]) as T;
    }
    return selectedEntity;
  }

  private void RequestChangeSelection()
  {
    try
    {
      this.isEventHandled = true;
      int focusedRowHandle = this.gridView.FocusedRowHandle;
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} requests to change selected. Selected row handle is [{1}].", (object) this, (object) PropertyHelper.GetPropertyTypeName((object) focusedRowHandle));
      List<T> list = ((IEnumerable<int>) new int[1]
      {
        focusedRowHandle
      }).Select<int, object>((Func<int, object>) (h => this.gridView.GetRow(h))).OfType<T>().ToList<T>();
      ChangeSelectionTriggerContext<T> context = new ChangeSelectionTriggerContext<T>((ICollection<T>) null, (ICollection<T>) list);
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} requests to change selected. Selected object [{1}].", (object) this, (object) PropertyHelper.GetPropertyTypeName((object) list));
      TriggerEventArgs e = new TriggerEventArgs(this.changeSelectionTrigger, (TriggerContext) context);
      this.view.RequestControllerAction((object) this, e);
      if (e.Cancel)
      {
        DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("{0} is canceling change current.", (object) this);
        this.FocusAndSelectItem(this.lastFocusedHandle);
      }
      else
        this.StoreCurrentFocusedAsLastFocused();
    }
    finally
    {
      this.isEventHandled = false;
    }
  }

  private void StoreCurrentFocusedAsLastFocused()
  {
    this.lastFocusedHandle = this.gridView.FocusedRowHandle;
    this.lastFocusedObject = this.gridView.GetRow(this.lastFocusedHandle) as T;
  }

  private void FocusAndSelectItem(int handleToFocus, params int[] handlesToSelect)
  {
    bool isEventHandled = this.isEventHandled;
    try
    {
      this.isEventHandled = true;
      this.gridView.BeginSelection();
      this.gridView.ClearSelection();
      this.gridView.FocusedRowHandle = handleToFocus;
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("Focused handle [{0}] in grid {1}", (object) this.gridView.FocusedRowHandle, (object) this);
      if (handlesToSelect != null)
      {
        foreach (int rowHandle in handlesToSelect)
        {
          DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("Selected row with handle [{0}] in grid {1}", (object) rowHandle, (object) this);
          this.gridView.SelectRow(rowHandle);
        }
      }
      this.gridView.EndSelection();
      this.StoreCurrentFocusedAsLastFocused();
    }
    catch (System.Exception ex)
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Error(ex, "Failure while focusing and selecting a new item in grid {0}.", (object) this);
      this.RestoreLastFocused();
    }
    finally
    {
      this.isEventHandled = isEventHandled;
    }
  }

  private void FocusItem(T item)
  {
    try
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("Handling a selection change for {0} Item: [{1}]", (object) this, (object) PropertyHelper.GetPropertyTypeName((object) item));
      if ((object) item != null)
      {
        int handleToFocus = ((IEnumerable<int>) this.FindRowHandles(item)).FirstOrDefault<int>();
        this.FocusAndSelectItem(handleToFocus, handleToFocus);
      }
      else
        this.gridView.FocusedRowHandle = int.MinValue;
    }
    catch (System.Exception ex)
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Error(ex, "Failure while focusing item {1} in grid {0}.", (object) this, (object) item);
    }
  }

  private void RestoreLastFocused()
  {
    try
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("Restoring focused handle [{0}] in grid {1}", (object) this.gridView.FocusedRowHandle, (object) this);
      this.gridView.BeginSelection();
      this.gridView.ClearSelection();
      this.gridView.FocusedRowHandle = this.lastFocusedHandle;
      if (this.lastFocusedHandle >= 0)
        this.gridView.SelectRow(this.lastFocusedHandle);
      this.gridView.EndSelection();
    }
    catch (System.Exception ex)
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Error(ex, "Failure while restoring last focused and selected item [{0}]in grid {1}.", (object) this.lastFocusedHandle, (object) this);
    }
  }

  private void RefreshGridItem(T item)
  {
    try
    {
      if (this.gridView.FocusedRowHandle >= 0)
        this.gridView.RefreshRow(this.gridView.FocusedRowHandle);
      if ((object) item == null)
        return;
      int rowHandle = ((IEnumerable<int>) this.FindRowHandles(item)).FirstOrDefault<int>();
      if (rowHandle < 0 || rowHandle == this.gridView.FocusedRowHandle)
        return;
      this.gridView.RefreshRow(rowHandle);
    }
    catch (System.Exception ex)
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Error(ex, "Failure while refreshing row in grid {0} with item {1}.", (object) this, (object) item);
    }
  }

  private void RefreshNestedDataSource(object dataSource)
  {
    try
    {
      this.refreshingDataSource = true;
      if (dataSource == null)
        return;
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("Handling a selection change for {0} DataMember [{1}] Items: [{2}]", (object) this, (object) this.dataMember, (object) PropertyHelper.GetPropertyTypeName(dataSource));
      PropertyInfo property = dataSource.GetType().GetProperty(this.dataMember);
      if (property == (PropertyInfo) null)
        return;
      IList<T> objList = property.GetValue(dataSource, (object[]) null) as IList<T>;
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("Assigned new data source for {0} after selection has changed. DataMember [{1}] Data Source: [{2}]", (object) this, (object) this.dataMember, (object) PropertyHelper.GetPropertyTypeName((object) objList));
      this.DataSource = (ICollection<T>) objList;
      if (objList != null)
      {
        this.FocusItem(objList.FirstOrDefault<T>());
        this.RequestChangeSelection();
      }
      else
        this.RequestChangeSelection();
    }
    finally
    {
      this.refreshingDataSource = false;
    }
  }

  private void ConsolidateDataSource()
  {
    try
    {
      this.refreshingDataSource = true;
      this.gridView.RefreshData();
    }
    finally
    {
      this.refreshingDataSource = false;
    }
  }

  private int[] FindRowHandles(params T[] items)
  {
    if (items == null)
    {
      DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("Couldn't search a row handle in the grid since the items are null. Delivering {0} as row handle.", (object) int.MinValue);
      return new int[1]{ int.MinValue };
    }
    Dictionary<T, int> itemsAndIndices = new Dictionary<T, int>();
    this.ConsolidateDataSource();
    for (int rowHandle = 0; rowHandle < this.gridView.RowCount; ++rowHandle)
    {
      if (this.gridView.GetRow(rowHandle) is T row)
        itemsAndIndices.AddOrOverwriteEntry<T, int>(row, rowHandle);
    }
    int[] array = ((IEnumerable<T>) items).Where<T>((Func<T, bool>) (i => (object) i != null)).Select<T, int>((Func<T, int>) (i => itemsAndIndices.GetValueOrDefault<T, int>(i, int.MinValue))).ToArray<int>();
    if (((IEnumerable<int>) array).Count<int>() != 0)
      return array;
    DevExpressSingleSelectionGridViewHelper<T>.Logger.Debug("Haven't found any items in the grid. Delivering {0} as row handle.", (object) int.MinValue);
    return new int[1]{ int.MinValue };
  }

  private static void DisplayEmptyGridMessage(object sender, CustomDrawEventArgs e)
  {
    if (sender == null || !(sender is ColumnView) || (sender as ColumnView).DataSource is BindingSource dataSource && dataSource.Count != 0)
      return;
    Font font = new Font("Tahoma", 8f, FontStyle.Italic);
    Rectangle layoutRectangle;
    ref Rectangle local = ref layoutRectangle;
    Rectangle bounds = e.Bounds;
    int x = bounds.Left + 5;
    bounds = e.Bounds;
    int y = bounds.Top + 5;
    bounds = e.Bounds;
    int width = bounds.Width + 5;
    bounds = e.Bounds;
    int height = bounds.Top + 5;
    local = new Rectangle(x, y, width, height);
    e.Graphics.DrawString("No data available", font, Brushes.Black, (RectangleF) layoutRectangle);
  }

  public override string ToString() => this.gridView.Name;

  private string GetFullInformation()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("DevExpressMultiSelectionGridViewHelper, ");
    stringBuilder.AppendFormat("View [{0}], Grid [{1}], ", (object) this.view.Name, (object) this.gridView.Name);
    stringBuilder.AppendFormat("Change Selection Trigger [{0}], ", (object) this.changeSelectionTrigger);
    stringBuilder.AppendFormat("Managed Data Type [{0}], ", (object) typeof (T));
    stringBuilder.AppendFormat("Data Member [{0}], ", string.IsNullOrEmpty(this.dataMember) ? (object) "n/a" : (object) this.dataMember);
    stringBuilder.AppendFormat("Data Source [{0}]", (object) PropertyHelper.GetPropertyTypeName(this.gridView.GridControl.DataSource));
    return stringBuilder.ToString();
  }

  private delegate void UpdateModelDataCallBack(object sender, ModelChangedEventArgs e) where T : class, new();
}
