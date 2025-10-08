// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.FacilityManager
// Assembly: PM.SiteAndFacilityManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F9CADFD-E9C9-4783-BA9E-8D20FAD3C075
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement.DataAccessLayer;
using PathMedical.SiteAndFacilityManagement.Properties;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement;

public class FacilityManager : ISingleEditingModel, IModel, ISingleSelectionModel<Facility>
{
  private readonly ModelHelper<Facility, FacilityAdapter> facilityModelHelper;

  private FacilityManager()
  {
    this.facilityModelHelper = new ModelHelper<Facility, FacilityAdapter>(new EventHandler<ModelChangedEventArgs>(this.OnModelHelperChanged), new string[2]
    {
      "Site",
      "LocationTypes"
    });
  }

  public static FacilityManager Instance => PathMedical.Singleton.Singleton<FacilityManager>.Instance;

  public Guid MaintainFacilitiesAccessPermissionId
  {
    get => new Guid("DD30B85E-4877-4293-8D1D-59F00EF03256");
  }

  public Facility SelectedFacility
  {
    get => this.facilityModelHelper.SelectedItem;
    set => this.facilityModelHelper.SelectedItem = value;
  }

  public List<Facility> Facilities
  {
    get
    {
      if (this.facilityModelHelper.Items == null)
        this.RefreshData();
      return this.facilityModelHelper.Items;
    }
  }

  public List<Facility> ActiveFacilities
  {
    get
    {
      if (this.facilityModelHelper.Items == null)
        this.RefreshData();
      return this.facilityModelHelper.Items != null ? this.facilityModelHelper.Items.Where<Facility>((Func<Facility, bool>) (f => !f.Inactive.HasValue)).ToList<Facility>() : (List<Facility>) null;
    }
  }

  private void LoadFacilities()
  {
    this.facilityModelHelper.LoadItems((Func<FacilityAdapter, ICollection<Facility>>) (adapter => (ICollection<Facility>) adapter.FetchEntities((Expression<Func<Facility, bool>>) (f => f.Inactive == new DateTime?())).ToList<Facility>()));
  }

  private void OnModelHelperChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.Changed != null)
    {
      this.Changed((object) this, ModelChangedEventArgs.Create<IEnumerable<Site>>(CurrentDataHelper<Site>.Instance.Items.Where<Site>((Func<Site, bool>) (s => !s.Inactive.HasValue)), ChangeType.ListLoaded));
      this.Changed((object) this, ModelChangedEventArgs.Create<IEnumerable<LocationType>>(CurrentDataHelper<LocationType>.Instance.Items.Where<LocationType>((Func<LocationType, bool>) (s => !s.Inactive.HasValue)), ChangeType.ListLoaded));
    }
    if (this.Changed == null)
      return;
    this.Changed(sender, e);
  }

  public void Store()
  {
    if (this.SelectedFacility == null)
      return;
    if (this.Facilities.Any<Facility>((Func<Facility, bool>) (f => f.Id != this.SelectedFacility.Id && f.Code.Equals(this.SelectedFacility.Code, StringComparison.InvariantCultureIgnoreCase))))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("FacilityWithSameCodeExists"));
    this.facilityModelHelper.Store();
  }

  public void Delete()
  {
    this.SelectedFacility.Inactive = new DateTime?(DateTime.Now);
    this.facilityModelHelper.MarkItemAsDeleted();
    this.RefreshData();
  }

  public void CancelNewItem() => this.facilityModelHelper.CancelAddItem();

  public void PrepareAddItem() => this.facilityModelHelper.PrepareAddItem(new Facility());

  public void RefreshData()
  {
    if (this.Changed != null)
    {
      this.Changed((object) this, ModelChangedEventArgs.Create<IEnumerable<Site>>(CurrentDataHelper<Site>.Instance.Items.Where<Site>((Func<Site, bool>) (s => !s.Inactive.HasValue)), ChangeType.ListLoaded));
      this.Changed((object) this, ModelChangedEventArgs.Create<IEnumerable<LocationType>>(CurrentDataHelper<LocationType>.Instance.Items.Where<LocationType>((Func<LocationType, bool>) (s => !s.Inactive.HasValue)), ChangeType.ListLoaded));
    }
    this.LoadFacilities();
  }

  public void RevertModifications()
  {
    if (this.SelectedFacility != null && this.SelectedFacility.Id == Guid.Empty)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<Facility>(this.SelectedFacility, ChangeType.SelectionChanged));
    }
    else
      this.facilityModelHelper.RefreshSelectedItems();
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void ChangeSingleSelection(Facility selection) => this.SelectedFacility = selection;

  public bool IsOneItemSelected<T>() where T : Facility => this.SelectedFacility != null;

  public bool IsOneItemAvailable<T>() where T : Facility
  {
    return this.Facilities != null && this.Facilities.Count > 0;
  }

  public void Import(List<Facility> facilities)
  {
    if (facilities == null || facilities.Count == 0)
      return;
    using (DBScope scope = new DBScope())
    {
      AdapterBase<Facility> adapterBase = new AdapterBase<Facility>(scope);
      adapterBase.LoadWithRelation("Site", "LocationTypes");
      adapterBase.Store((ICollection<Facility>) facilities);
      scope.Complete();
    }
  }

  public void Delete(Facility facility)
  {
    if (facility == null)
      return;
    using (DBScope scope = new DBScope())
    {
      new AdapterBase<Facility>(scope).Delete(facility);
      scope.Complete();
    }
  }

  public void Store(Facility facility)
  {
    if (facility == null)
      return;
    using (DBScope scope = new DBScope())
    {
      new AdapterBase<Facility>(scope).Store(facility);
      scope.Complete();
    }
  }

  public void AssignDefaultLocationTypes(LocationType locationType)
  {
    this.RefreshData();
    List<Facility> activeFacilities = FacilityManager.Instance.ActiveFacilities;
    using (DBScope dbScope = new DBScope())
    {
      foreach (Facility facility in activeFacilities)
      {
        if (facility.LocationTypes != null)
          facility.LocationTypes.Add(locationType);
        else
          facility.LocationTypes = new List<LocationType>()
          {
            locationType
          };
        this.Store(facility);
      }
      dbScope.Complete();
    }
  }
}
