// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.LocationTypeManager
// Assembly: PM.SiteAndFacilityManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F9CADFD-E9C9-4783-BA9E-8D20FAD3C075
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement.DataAccessLayer;
using PathMedical.SiteAndFacilityManagement.Properties;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement;

public class LocationTypeManager : ISingleEditingModel, IModel, ISingleSelectionModel<LocationType>
{
  private readonly ModelHelper<LocationType, LocationTypeAdapter> locationModelHelper;

  private LocationTypeManager()
  {
    this.locationModelHelper = new ModelHelper<LocationType, LocationTypeAdapter>(new EventHandler<ModelChangedEventArgs>(this.OnModelHelperChanged), Array.Empty<string>());
  }

  public static LocationTypeManager Instance => PathMedical.Singleton.Singleton<LocationTypeManager>.Instance;

  public LocationType SelectedLocationType
  {
    get
    {
      LocationType selectedLocationType = (LocationType) null;
      if (this.locationModelHelper != null && this.locationModelHelper.SelectedItems != null)
        selectedLocationType = this.locationModelHelper.SelectedItems.FirstOrDefault<LocationType>();
      return selectedLocationType;
    }
    set
    {
      if (this.locationModelHelper == null)
        return;
      this.locationModelHelper.SelectedItems = (ICollection<LocationType>) new LocationType[1]
      {
        value
      };
    }
  }

  public List<LocationType> LocationTypes
  {
    get
    {
      if (this.locationModelHelper.Items == null)
        this.RefreshData();
      return this.locationModelHelper.Items;
    }
  }

  private void OnModelHelperChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed(sender, e);
  }

  private void LoadLocationTypes()
  {
    this.locationModelHelper.LoadItems((Func<LocationTypeAdapter, ICollection<LocationType>>) (adapter => adapter.FetchEntities((Expression<Func<LocationType, bool>>) (i => i.Inactive == new DateTime?()))));
  }

  public void Store()
  {
    if (this.SelectedLocationType == null)
      return;
    if (this.LocationTypes.Any<LocationType>((Func<LocationType, bool>) (lt => lt.Id != this.SelectedLocationType.Id && lt.Code.Equals(this.SelectedLocationType.Code, StringComparison.InvariantCultureIgnoreCase))))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("RecordWithSameCodeExists"));
    this.locationModelHelper.Store();
  }

  public void Delete()
  {
    this.SelectedLocationType.Inactive = new DateTime?(DateTime.Now);
    this.locationModelHelper.MarkItemAsDeleted();
    this.RefreshData();
  }

  public void CancelNewItem() => this.locationModelHelper.CancelAddItem();

  public void PrepareAddItem() => this.locationModelHelper.PrepareAddItem(new LocationType());

  public void RefreshData() => this.LoadLocationTypes();

  public void RevertModifications()
  {
    if (this.SelectedLocationType != null && this.SelectedLocationType.Id == Guid.Empty)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<LocationType>(this.SelectedLocationType, ChangeType.SelectionChanged));
    }
    else
      this.locationModelHelper.RefreshSelectedItems();
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void ChangeSingleSelection(LocationType selection)
  {
    this.SelectedLocationType = selection;
  }

  public bool IsOneItemSelected<T>() where T : LocationType => this.SelectedLocationType != null;

  public bool IsOneItemAvailable<T>() where T : LocationType
  {
    return this.LocationTypes != null && this.LocationTypes.Count > 0;
  }

  public void Import(List<LocationType> import)
  {
    if (import == null || import.Count == 0)
      return;
    using (DBScope scope = new DBScope())
    {
      new AdapterBase<LocationType>(scope).Store((ICollection<LocationType>) import);
      scope.Complete();
    }
  }

  public void Delete(LocationType location)
  {
    if (location == null)
      return;
    using (DBScope scope = new DBScope())
    {
      new AdapterBase<LocationType>(scope).Delete(location);
      scope.Complete();
    }
  }
}
