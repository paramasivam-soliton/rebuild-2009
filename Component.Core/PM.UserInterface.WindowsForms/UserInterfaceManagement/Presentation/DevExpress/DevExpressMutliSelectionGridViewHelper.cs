// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress.DevExpressMutliSelectionGridViewHelper`1
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.Data;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.Property;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;

public class DevExpressMutliSelectionGridViewHelper<T> where T : class, new()
{
  private static readonly ILogger Logger = LogFactory.Instance.Create("UserInterface");
  private readonly Trigger changeFocusTrigger = Triggers.ChangeCurrent;
  private readonly Trigger changeSelectionTrigger = Triggers.ChangeSelection;
  private readonly string dataMember;
  private readonly GridView gridView;
  private readonly PathMedical.UserInterface.WindowsForms.ModelViewController.View view;
  private bool isChangeFocusEventHandled;
  private bool isChangeSelectionEventHandled;
  private int lastFocusedHandle = int.MinValue;
  private T lastFocusedObject;
  private int[] lastSelectedHandles = new int[0];
  private bool refreshingDataSource;

  private DevExpressMutliSelectionGridViewHelper(PathMedical.UserInterface.WindowsForms.ModelViewController.View view, GridView gridView)
  {
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    if (gridView == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (gridView));
    this.view = view;
    this.gridView = gridView;
    this.gridView.SelectionChanged += new SelectionChangedEventHandler(this.OnSelectionChanged);
    this.gridView.FocusedRowChanged += new FocusedRowChangedEventHandler(this.OnFocusedRowChanged);
    this.gridView.CustomDrawEmptyForeground += new CustomDrawEventHandler(DevExpressMutliSelectionGridViewHelper<T>.DisplayEmptyGridMessage);
    this.gridView.OptionsCustomization.AllowFilter = false;
    DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} has created its grid view helper.", (object) this);
  }

  public DevExpressMutliSelectionGridViewHelper(PathMedical.UserInterface.WindowsForms.ModelViewController.View view, GridView gridView, IModel model)
    : this(view, gridView)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    if (!(model is IMultiSelectionModel<T>))
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("Model doesn't support multi selection.");
    DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} listens on change events of {1}", (object) this, (object) model);
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  public DevExpressMutliSelectionGridViewHelper(
    PathMedical.UserInterface.WindowsForms.ModelViewController.View view,
    GridView grid,
    Trigger changeSelectionTrigger)
    : this(view, grid)
  {
    DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} has been requested to use {1} for selection change requests.", (object) this, (object) changeSelectionTrigger);
    this.changeSelectionTrigger = changeSelectionTrigger;
  }

  public DevExpressMutliSelectionGridViewHelper(
    PathMedical.UserInterface.WindowsForms.ModelViewController.View view,
    GridView grid,
    IModel model,
    Trigger changeSelectionTrigger)
    : this(view, grid, model)
  {
    DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} has been requested to use {1} for selection change requests.", (object) this, (object) changeSelectionTrigger);
    this.changeSelectionTrigger = changeSelectionTrigger;
  }

  public DevExpressMutliSelectionGridViewHelper(
    PathMedical.UserInterface.WindowsForms.ModelViewController.View view,
    GridView grid,
    Trigger changeSelectionTrigger,
    string dataMember)
    : this(view, grid, changeSelectionTrigger)
  {
    DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} has been instructed to get its data from sub property {1}. ", (object) this, (object) dataMember);
    this.dataMember = dataMember;
  }

  public DevExpressMutliSelectionGridViewHelper(
    PathMedical.UserInterface.WindowsForms.ModelViewController.View view,
    GridView grid,
    IModel model,
    Trigger changeSelectionTrigger,
    string dataMember)
    : this(view, grid, model, changeSelectionTrigger)
  {
    DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} has been instructed to get its data from sub property {1}. ", (object) this, (object) dataMember);
    this.dataMember = dataMember;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.gridView == null || this.gridView.GridControl == null)
      return;
    if (string.IsNullOrEmpty(this.dataMember) && e.Type != typeof (T))
    {
      DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} ignores model update since the changed object of type {1} which has no affect to this grid view.", (object) this, (object) e.Type);
    }
    else
    {
      DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} receives a model udpate {1} from {2}. {3}", (object) this, (object) e.ToShortString(), sender, (object) e, (object) this);
      if (this.gridView.GridControl.InvokeRequired)
        this.gridView.GridControl.BeginInvoke((Delegate) new DevExpressMutliSelectionGridViewHelper<T>.UpdateModelDataCallBack(this.UpdateGridData), sender, (object) e);
      else
        this.UpdateGridData(sender, e);
    }
  }

  private void UpdateGridData(object sender, ModelChangedEventArgs e)
  {
    try
    {
      this.refreshingDataSource = true;
      switch (e.ChangeType)
      {
        case ChangeType.ItemAdded:
          if (!(e.Type == typeof (T)))
            break;
          T changedObject = e.ChangedObject as T;
          DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is selecting new item: [{1}]", (object) this, (object) PropertyHelper.GetPropertyTypeName((object) changedObject));
          this.FocusItem(((IEnumerable<int>) this.FindRowHandles(changedObject)).First<int>());
          break;
        case ChangeType.ItemEdited:
          if (!(e.Type == typeof (T)))
            break;
          DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is refreshing the item {1} since it has been edited.", (object) this, (object) PropertyHelper.GetPropertyTypeName(e.ChangedObject));
          this.RefreshGridItem(e.ChangedObject as T);
          break;
        case ChangeType.ItemDeleted:
          if (!(e.Type == typeof (T)))
            break;
          DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is consolidating its data source due to an deleted item", (object) this);
          this.ConsolidateDataSource();
          DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is requests to change the current because an item of its data source has been deleted.");
          this.RequestChangeCurrent();
          break;
        case ChangeType.SelectionChanged:
          DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is consolidating its data source due to a selection change", (object) this);
          this.ConsolidateDataSource();
          if (e.Type == typeof (T) && !e.IsList && e.ChangedObject != null)
          {
            DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is changing the selected item.", (object) this);
            this.FocusItem(e.ChangedObject as T);
            break;
          }
          if (e.Type == typeof (T) && !e.IsList && e.ChangedObject == null && this.gridView.FocusedRowHandle >= 0)
          {
            DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} got a request that selection has changed to an empty object. Requesting to change the current item that the grid has been selected automatically.", (object) this);
            this.RequestChangeCurrent();
            break;
          }
          if (string.IsNullOrEmpty(this.dataMember))
            break;
          DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} got a selection changed request and starts to refresh the nested data source.", (object) this);
          this.RefreshNestedDataSource(e.ChangedObject);
          break;
        case ChangeType.ListLoaded:
          if (!(e.Type == typeof (T)) || !e.IsList)
            break;
          this.DataSource = (ICollection<T>) e.ChangedObject;
          DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} has updated its data source. {1}", (object) this, (object) this.GetFullInformation());
          break;
      }
    }
    finally
    {
      this.refreshingDataSource = false;
    }
  }

  private void OnFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
  {
    if (this.isChangeFocusEventHandled)
      DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} already handles the 'focus changed' event. This might be a nested call.", (object) this);
    else if (this.refreshingDataSource)
    {
      DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is now updating its data source and ignore 'focus changed' events.", (object) this);
    }
    else
    {
      if (e == null)
        return;
      DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} starts handling 'focus changed' event emmitted by [{1}]. Focused row handle is [{2}]. Old row handle is [{3}]", (object) this, sender, (object) e.FocusedRowHandle, (object) e.PrevFocusedRowHandle);
      this.RequestChangeCurrent();
    }
  }

  private void RequestChangeCurrent()
  {
    bool focusEventHandled = this.isChangeFocusEventHandled;
    try
    {
      this.isChangeFocusEventHandled = true;
      T row = this.gridView.GetRow(this.gridView.FocusedRowHandle) as T;
      if ((object) this.lastFocusedObject != (object) row)
      {
        ChangeCurrentItemTriggerContext<T> context = new ChangeCurrentItemTriggerContext<T>(default (T), row);
        DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} starts request to change current. Focused object is [{1}].", (object) this, (object) PropertyHelper.GetPropertyTypeName((object) row));
        TriggerEventArgs e = new TriggerEventArgs(this.changeFocusTrigger, (TriggerContext) context);
        this.view.RequestControllerAction((object) this, e);
        if (e.Cancel)
        {
          DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is canceling change current.", (object) this);
          this.RestoreLastCurrentFocusedAsFocused();
        }
        else
          this.StoreCurrentFocusedAsLastFocused();
      }
      else
        DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is ignoring request to change current since focused object hasn't changed.", (object) this);
    }
    finally
    {
      this.isChangeFocusEventHandled = focusEventHandled;
    }
  }

  private void FocusItem(int handleToFocus)
  {
    bool focusEventHandled = this.isChangeFocusEventHandled;
    try
    {
      this.isChangeFocusEventHandled = true;
      this.gridView.FocusedRowHandle = handleToFocus;
      this.StoreCurrentFocusedAsLastFocused();
    }
    finally
    {
      this.isChangeFocusEventHandled = focusEventHandled;
    }
  }

  private void FocusItem(T item)
  {
    bool focusEventHandled = this.isChangeFocusEventHandled;
    try
    {
      this.isChangeFocusEventHandled = true;
      int[] rowHandles = this.FindRowHandles(item);
      if (((IEnumerable<int>) rowHandles).Count<int>() <= 0)
        return;
      this.FocusItem(rowHandles[0]);
    }
    finally
    {
      this.isChangeFocusEventHandled = focusEventHandled;
    }
  }

  private void StoreCurrentFocusedAsLastFocused()
  {
    this.lastFocusedHandle = this.gridView.FocusedRowHandle;
    this.lastFocusedObject = this.gridView.GetRow(this.lastFocusedHandle) as T;
  }

  private void RestoreLastCurrentFocusedAsFocused() => this.FocusItem(this.lastFocusedHandle);

  private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
  {
    if (this.isChangeSelectionEventHandled)
      return;
    this.RequestChangeSelection();
  }

  private void RequestChangeSelection()
  {
    int[] selectedRows = this.gridView.GetSelectedRows();
    if (DevExpressMutliSelectionGridViewHelper<T>.Logger.IsDebugEnabled)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (int num in selectedRows)
        stringBuilder.AppendFormat("{0} ", (object) num);
      DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is handling request to change selected item to {1}.", (object) this, (object) stringBuilder.ToString());
    }
    try
    {
      this.isChangeSelectionEventHandled = true;
      List<T> list = ((IEnumerable<int>) selectedRows).Select<int, object>((Func<int, object>) (h => this.gridView.GetRow(h))).OfType<T>().ToList<T>();
      ChangeSelectionTriggerContext<T> context = new ChangeSelectionTriggerContext<T>((ICollection<T>) null, (ICollection<T>) list);
      DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is requesting to change the selection to {1}.", (object) this, (object) PropertyHelper.GetPropertyTypeName((object) list));
      TriggerEventArgs e = new TriggerEventArgs(this.changeSelectionTrigger, (TriggerContext) context);
      this.view.RequestControllerAction((object) this, e);
      if (e.Cancel)
      {
        DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is canceling request to change selection to {1}.", (object) this, (object) PropertyHelper.GetPropertyTypeName((object) list));
        this.RestoreLastSelectionAsSelected();
      }
      else
        this.StoreCurrentSelectedAsLastSelected();
    }
    finally
    {
      this.isChangeSelectionEventHandled = false;
    }
  }

  private void SelectItems(params int[] handlesToSelect)
  {
    bool selectionEventHandled = this.isChangeSelectionEventHandled;
    try
    {
      this.isChangeSelectionEventHandled = true;
      this.gridView.BeginSelection();
      this.gridView.ClearSelection();
      if (handlesToSelect != null)
      {
        foreach (int rowHandle in handlesToSelect)
        {
          DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is selected row with handle {0}.", (object) this, (object) rowHandle);
          this.gridView.SelectRow(rowHandle);
        }
      }
      this.gridView.EndSelection();
      this.StoreCurrentFocusedAsLastFocused();
    }
    finally
    {
      this.isChangeSelectionEventHandled = selectionEventHandled;
    }
  }

  private void SelectItems(IEnumerable<T> items)
  {
    bool selectionEventHandled = this.isChangeSelectionEventHandled;
    try
    {
      this.isChangeSelectionEventHandled = true;
      this.SelectItems(this.FindRowHandles(items.ToArray<T>()));
    }
    finally
    {
      this.isChangeSelectionEventHandled = selectionEventHandled;
    }
  }

  private void StoreCurrentSelectedAsLastSelected()
  {
    this.lastSelectedHandles = this.gridView.GetSelectedRows();
  }

  private void RestoreLastSelectionAsSelected() => this.SelectItems(this.lastSelectedHandles);

  private void RefreshNestedDataSource(object dataSource)
  {
    DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} is refreshing its nested data source {1}.", (object) this, (object) this.dataMember);
    PropertyInfo property = dataSource.GetType().GetProperty(this.dataMember);
    if (property == (PropertyInfo) null)
      return;
    IList<T> source = property.GetValue(dataSource, (object[]) null) as IList<T>;
    DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} assigned new data source {1} after selection of the root element has changed.", (object) this, (object) this.dataMember);
    this.DataSource = (ICollection<T>) source;
    if (source == null)
      return;
    this.FocusItem(source.FirstOrDefault<T>());
  }

  private void RefreshGridItem(T item)
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
      DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} couldn't search a row handle in the grid since the items are null. Delivering {1} as row handle.", (object) this, (object) int.MinValue);
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
    DevExpressMutliSelectionGridViewHelper<T>.Logger.Debug("{0} hasn't found requested row handles. Delivering {1} as row handle.", (object) this, (object) int.MinValue);
    return new int[1]{ int.MinValue };
  }

  private static void DisplayEmptyGridMessage(object sender, CustomDrawEventArgs e)
  {
    if (sender == null || !(sender is ColumnView))
      return;
    BindingSource dataSource = (sender as ColumnView).DataSource as BindingSource;
    string emptyGridMessage = Resources.DevExpressMutliSelectionGridViewHelper_EmptyGridMessage;
    if (dataSource != null && dataSource.Count != 0)
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
    e.Graphics.DrawString(emptyGridMessage, font, Brushes.Black, (RectangleF) layoutRectangle);
  }

  public int GetClickedRow(GridView clickedView, Point pt)
  {
    if (clickedView == null)
      return int.MinValue;
    GridHitInfo gridHitInfo = clickedView.CalcHitInfo(pt);
    return gridHitInfo != null ? gridHitInfo.RowHandle : int.MinValue;
  }

  private ICollection<T> DataSource
  {
    set => this.gridView.GridControl.DataSource = (object) value;
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
