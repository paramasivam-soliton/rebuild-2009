// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.SiteManager
// Assembly: PM.SiteAndFacilityManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F9CADFD-E9C9-4783-BA9E-8D20FAD3C075
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement.DataAccessLayer;
using PathMedical.SiteAndFacilityManagement.Properties;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement;

public class SiteManager : ISingleEditingModel, IModel, ISingleSelectionModel<Site>
{
  private static readonly ILogger logger = LogFactory.Instance.Create(typeof (SiteManager), "$Rev: 1588 $");
  private readonly ModelHelper<Site, SiteAdapter> siteModel;

  public static SiteManager Instance => PathMedical.Singleton.Singleton<SiteManager>.Instance;

  public Site SelectedItem
  {
    get => this.siteModel.SelectedItem;
    set => this.siteModel.SelectedItem = value;
  }

  public List<Site> Sites
  {
    get
    {
      if (this.siteModel.Items == null)
        this.RefreshData();
      return this.siteModel.Items;
    }
  }

  private SiteManager()
  {
    this.siteModel = new ModelHelper<Site, SiteAdapter>(new EventHandler<ModelChangedEventArgs>(this.OnModelHelperChanged), new string[1]
    {
      "Facilities"
    });
    this.LoadDataExchangeDescriptions();
  }

  private void OnModelHelperChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed(sender, e);
  }

  private void LoadSites()
  {
    this.siteModel.LoadItems((Func<SiteAdapter, ICollection<Site>>) (adapter => adapter.FetchEntities((Expression<Func<Site, bool>>) (i => i.Inactive == new DateTime?()))));
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void Store()
  {
    if (this.SelectedItem == null)
      return;
    if (this.Sites.Any<Site>((Func<Site, bool>) (f => f.Id != this.SelectedItem.Id && f.Code.Equals(this.SelectedItem.Code, StringComparison.InvariantCultureIgnoreCase))))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("SiteWithSameCodeExists"));
    this.siteModel.Store();
  }

  public void Delete()
  {
    if (this.SelectedItem == null)
      return;
    SiteManager.logger.Info("Deleting site {0} with Id {1}.", (object) this.SelectedItem.Name, (object) this.SelectedItem.Id);
    this.SelectedItem.Inactive = new DateTime?(DateTime.Now);
    this.siteModel.MarkItemAsDeleted();
    this.RefreshData();
  }

  public void CancelNewItem() => this.siteModel.CancelAddItem();

  public void PrepareAddItem() => this.siteModel.PrepareAddItem(new Site());

  public void RefreshData() => this.LoadSites();

  public void RevertModifications()
  {
    if (this.SelectedItem != null && this.SelectedItem.Id == Guid.Empty)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<Site>(this.SelectedItem, ChangeType.SelectionChanged));
    }
    else
      this.siteModel.RefreshSelectedItems();
  }

  public void ChangeSingleSelection(Site selection) => this.SelectedItem = selection;

  bool ISingleSelectionModel<Site>.IsOneItemSelected<T>() => this.SelectedItem != null;

  public bool IsOneItemAvailable<T>() where T : Site => this.Sites != null && this.Sites.Count > 0;

  public List<DataExchangeSetMap> RecordSetMaps { get; protected set; }

  public List<RecordDescriptionSet> RecordDescriptionSets { get; protected set; }

  private void LoadDataExchangeDescriptions()
  {
    this.RecordDescriptionSets = new List<RecordDescriptionSet>();
    this.RecordSetMaps = new List<DataExchangeSetMap>();
    this.RecordDescriptionSets.Add(RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.SiteAndFacilityManagement.DataExchange.PluginDataDescription.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.SiteManager_Exception_PluginDataDescriptionMissing)));
  }

  public void Import(List<Site> sites)
  {
    if (sites == null || sites.Count == 0)
      return;
    using (DBScope scope = new DBScope())
    {
      new AdapterBase<Site>(scope).Store((ICollection<Site>) sites);
      scope.Complete();
    }
  }

  public void Delete(Site site)
  {
    if (site == null)
      return;
    using (DBScope scope = new DBScope())
    {
      AdapterBase<Site> adapterBase = new AdapterBase<Site>(scope);
      site.Inactive = new DateTime?(DateTime.Now);
      this.siteModel.MarkItemAsDeleted(site);
      scope.Complete();
      this.RefreshData();
    }
  }

  public void Store(Site site)
  {
    if (site == null)
      return;
    using (DBScope scope = new DBScope())
    {
      new AdapterBase<Site>(scope).Store(site);
      scope.Complete();
    }
  }
}
