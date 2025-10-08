// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.InstrumentManager
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.Exception;
using PathMedical.InstrumentManagement.DataAccessLayer;
using PathMedical.InstrumentManagement.Properties;
using PathMedical.Logging;
using PathMedical.ResourceManager;
using PathMedical.SiteAndFacilityManagement;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement;
using PathMedical.UserProfileManagement.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace PathMedical.InstrumentManagement;

public class InstrumentManager : 
  ISingleSelectionModel<Instrument>,
  IModel,
  ISingleEditingModel,
  ISingleSelectionModel<User>
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (InstrumentManager));
  private readonly ModelHelper<Instrument, InstrumentAdapter> modelHelper;
  private readonly ModelHelper<User, UserAdapter> userModelHelper;
  private User selectedUser;

  public static InstrumentManager Instance => PathMedical.Singleton.Singleton<InstrumentManager>.Instance;

  private InstrumentManager()
  {
    this.modelHelper = new ModelHelper<Instrument, InstrumentAdapter>(new EventHandler<ModelChangedEventArgs>(this.ModelChanged), new string[2]
    {
      "Site.Facilities",
      "UsersOnInstrument.Profile.ProfileAccessPermissions"
    });
    this.userModelHelper = new ModelHelper<User, UserAdapter>(new EventHandler<ModelChangedEventArgs>(this.ModelChanged), Array.Empty<string>());
    SiteManager.Instance.Changed += new EventHandler<ModelChangedEventArgs>(this.ModelChanged);
    UserManager.Instance.Changed += new EventHandler<ModelChangedEventArgs>(this.OnUserChanged);
    this.LoadDataExchangeDescriptions();
  }

  private void OnUserChanged(object sender, ModelChangedEventArgs e)
  {
    if (e == null || !e.Type.Equals(typeof (User)) || e.ChangeType != ChangeType.ItemEdited && e.ChangeType != ChangeType.ItemDeleted)
      return;
    this.RefreshData();
    if (!this.IsOneItemSelected<Instrument>())
      return;
    InstrumentManager.BuildUserAssociated(this.modelHelper.SelectedItem);
    this.ModelChanged((object) this, ModelChangedEventArgs.Create<List<User>>(this.modelHelper.SelectedItem.UsersOnInstrument, ChangeType.ListLoaded));
  }

  public Instrument SelectedItem
  {
    get
    {
      return this.modelHelper.SelectedItems != null ? this.modelHelper.SelectedItems.FirstOrDefault<Instrument>() : (Instrument) null;
    }
    set
    {
      this.modelHelper.SelectedItems = (ICollection<Instrument>) new Instrument[1]
      {
        value
      };
      if (this.Changed == null)
        return;
      this.ModelChanged((object) this, ModelChangedEventArgs.Create<ICollection<Instrument>>(this.modelHelper.SelectedItems, ChangeType.ListLoaded));
    }
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  private void ModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangedObject is Instrument)
    {
      int changeType = (int) e.ChangeType;
    }
    if (this.Changed == null)
      return;
    this.Changed(sender, e);
  }

  public Instrument SelectedInstrument
  {
    get => this.modelHelper.SelectedItems.FirstOrDefault<Instrument>();
    set
    {
      this.modelHelper.SelectedItems = (ICollection<Instrument>) new Instrument[1]
      {
        value
      };
      InstrumentManager.BuildUserAssociated(value);
    }
  }

  public List<Instrument> Instruments
  {
    get
    {
      if (this.modelHelper.Items == null)
        this.LoadInstruments();
      return this.modelHelper.Items;
    }
  }

  public User SelectedUser
  {
    get => this.selectedUser;
    set
    {
      if (this.selectedUser == value)
        return;
      this.selectedUser = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<User>(this.selectedUser, ChangeType.SelectionChanged));
    }
  }

  private static void BuildUserAssociated(Instrument instrument)
  {
    if (instrument == null)
      return;
    if (UserManager.Instance.Users == null)
      UserManager.Instance.RefreshData();
    if (instrument.UsersOnInstrument == null)
      instrument.UsersOnInstrument = new List<User>();
    foreach (User user in UserManager.Instance.Users.Where<User>((Func<User, bool>) (u => u.IsActive)).Where<User>((Func<User, bool>) (u => instrument.UsersOnInstrument == null || !instrument.UsersOnInstrument.Contains(u))).Select<User, User>((Func<User, User>) (u => u.Clone() as User)).ToList<User>())
    {
      user.UserOnInstrumentValue = false;
      instrument.UsersOnInstrument.Add(user);
    }
    foreach (User user in instrument.UsersOnInstrument.Except<User>(UserManager.Instance.Users.Where<User>((Func<User, bool>) (u => u.IsActive))).ToList<User>())
      instrument.UsersOnInstrument.Remove(user);
  }

  private void LoadInstruments()
  {
    this.modelHelper.LoadItems(new Func<InstrumentAdapter, ICollection<Instrument>>(this.DoLoadInstruments));
  }

  private ICollection<Instrument> DoLoadInstruments(InstrumentAdapter adapter)
  {
    List<Instrument> list = adapter.All.ToList<Instrument>();
    foreach (Instrument instrument in list)
      InstrumentManager.BuildUserAssociated(instrument);
    return (ICollection<Instrument>) list;
  }

  public void Store()
  {
    Instrument selectedInstrument = this.modelHelper.SelectedItems.OfType<Instrument>().SingleOrDefault<Instrument>();
    if (selectedInstrument != null)
    {
      if (this.Instruments.Any<Instrument>((Func<Instrument, bool>) (i => i.Id != selectedInstrument.Id && i.SerialNumber.Equals(selectedInstrument.SerialNumber, StringComparison.InvariantCultureIgnoreCase))))
        throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("InstrumentWithSameSerialExists"));
      if (!string.IsNullOrEmpty(selectedInstrument.Code) && this.Instruments.Where<Instrument>((Func<Instrument, bool>) (i => !string.IsNullOrEmpty(i.Code))).Any<Instrument>((Func<Instrument, bool>) (i => i.Id != selectedInstrument.Id && i.Code.Equals(selectedInstrument.Code, StringComparison.InvariantCultureIgnoreCase))))
        throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("InstrumentWithSameCodeExists"));
      Guid? instrumentTypeSignature = selectedInstrument.InstrumentTypeSignature;
      if (instrumentTypeSignature.HasValue)
      {
        instrumentTypeSignature = selectedInstrument.InstrumentTypeSignature;
        Guid empty = Guid.Empty;
        if ((instrumentTypeSignature.HasValue ? (instrumentTypeSignature.GetValueOrDefault() == empty ? 1 : 0) : 0) == 0)
          goto label_8;
      }
      selectedInstrument.InstrumentTypeSignature = new Guid?(new Guid("BAC087B8-331E-4cd3-8F6E-12A9C13E7E61"));
    }
label_8:
    InstrumentManager.BuildUserAssociated(this.modelHelper.SelectedItem);
    GlobalInstrumentConfiguration.Instance.Store();
    this.modelHelper.Store();
    this.RefreshData();
  }

  public void Delete()
  {
    if (this.modelHelper.SelectedItem == null)
      return;
    try
    {
      using (DBScope dbScope = new DBScope())
      {
        using (DbCommand dbCommand = dbScope.CreateDbCommand())
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("DELETE FROM InstrumentUserAssociation");
          stringBuilder.AppendFormat(" WHERE InstrumentId = @InstrumentId");
          dbCommand.CommandText = stringBuilder.ToString();
          dbScope.AddDbParameter(dbCommand, "InstrumentId", (object) this.modelHelper.SelectedItem.Id);
          dbCommand.ExecuteNonQuery();
        }
        this.modelHelper.Delete();
        dbScope.Complete();
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(Resources.InstrumentManager_DeletionError, ex);
    }
  }

  public void CancelNewItem() => this.modelHelper.CancelAddItem();

  public void PrepareAddItem()
  {
    Instrument instrument = new Instrument();
    InstrumentManager.BuildUserAssociated(instrument);
    this.modelHelper.PrepareAddItem(instrument);
  }

  public void RefreshData()
  {
    SiteManager.Instance.RefreshData();
    this.LoadInstruments();
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<ICollection<Instrument>>(this.modelHelper.SelectedItems, ChangeType.SelectionChanged));
  }

  public void RevertModifications()
  {
    GlobalInstrumentConfiguration.Instance.ResetChanges();
    if (this.SelectedItem != null && this.SelectedItem.Id == Guid.Empty)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<Instrument>(this.SelectedItem, ChangeType.SelectionChanged));
    }
    else
      this.modelHelper.RefreshSelectedItems();
  }

  public void ChangeSingleSelection(Instrument selection) => this.SelectedInstrument = selection;

  public bool IsOneItemSelected<T>() where T : Instrument => this.modelHelper.SelectedItem != null;

  bool ISingleSelectionModel<Instrument>.IsOneItemAvailable<T>()
  {
    return this.Instruments != null && this.Instruments.Count > 0;
  }

  public void ChangeSingleSelection(User selection) => this.SelectedUser = selection;

  bool ISingleSelectionModel<User>.IsOneItemSelected<T>() => this.SelectedUser != null;

  bool ISingleSelectionModel<User>.IsOneItemAvailable<T>()
  {
    return this.userModelHelper != null && this.userModelHelper.Items != null && this.userModelHelper.Items.Count > 0;
  }

  public void ReportAssignmentModification()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<User>(this.SelectedUser, ChangeType.ItemEdited));
  }

  public List<DataExchangeSetMap> RecordSetMaps { get; protected set; }

  public List<RecordDescriptionSet> RecordDescriptionSets { get; protected set; }

  private void LoadDataExchangeDescriptions()
  {
    try
    {
      this.RecordDescriptionSets = new List<RecordDescriptionSet>();
      this.RecordSetMaps = new List<DataExchangeSetMap>();
      this.RecordDescriptionSets.Add(RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.InstrumentManagement.DataExchange.PluginDataDescription.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("Can't find the XML document that describes the structure for data exchange.")));
    }
    catch (System.Exception ex)
    {
      throw;
    }
  }

  public void Import(List<Instrument> instruments)
  {
    if (instruments == null || instruments.Count == 0)
      return;
    using (DBScope scope = new DBScope())
    {
      AdapterBase<Instrument> adapterBase = new AdapterBase<Instrument>(scope);
      adapterBase.LoadWithRelation("Site");
      adapterBase.Store((ICollection<Instrument>) instruments);
      scope.Complete();
    }
  }

  public void Delete(Instrument instrument)
  {
    if (instrument == null)
      return;
    using (DBScope scope = new DBScope())
    {
      AdapterBase<Instrument> adapterBase = new AdapterBase<Instrument>(scope);
      adapterBase.LoadWithRelation("Site");
      using (DbCommand dbCommand = scope.CreateDbCommand())
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("DELETE FROM InstrumentUserAssociation");
        stringBuilder.AppendFormat(" WHERE InstrumentId = @InstrumentId");
        dbCommand.CommandText = stringBuilder.ToString();
        scope.AddDbParameter(dbCommand, "InstrumentId", (object) instrument.Id);
        dbCommand.ExecuteNonQuery();
      }
      adapterBase.Delete(instrument);
      scope.Complete();
    }
  }

  public void RegisterInstrumentConnection(Instrument instrument)
  {
    if (instrument == null)
      return;
    Instrument instrument1 = (Instrument) null;
    this.RefreshData();
    if (this.modelHelper.Items != null)
      instrument1 = this.modelHelper.Items.OfType<Instrument>().FirstOrDefault<Instrument>((Func<Instrument, bool>) (i =>
      {
        Guid? instrumentTypeSignature1 = i.InstrumentTypeSignature;
        Guid? instrumentTypeSignature2 = instrument.InstrumentTypeSignature;
        return (instrumentTypeSignature1.HasValue == instrumentTypeSignature2.HasValue ? (instrumentTypeSignature1.HasValue ? (instrumentTypeSignature1.GetValueOrDefault() == instrumentTypeSignature2.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0 && i.SerialNumber == instrument.SerialNumber;
      }));
    if (instrument1 != null)
    {
      instrument1.LastConnected = new DateTime?(DateTime.Now);
      instrument1.SoftwareVersion = instrument.SoftwareVersion;
      this.modelHelper.Store(instrument1);
    }
    else
    {
      Instrument instrument2 = new Instrument()
      {
        Id = instrument.Id,
        Name = instrument.Name,
        Code = instrument.Code,
        SerialNumber = instrument.SerialNumber,
        HardwareVersion = instrument.HardwareVersion,
        LastUpdated = instrument.LastUpdated,
        LastConnected = new DateTime?(DateTime.Now),
        InstrumentType = instrument.InstrumentType,
        InstrumentTypeSignature = instrument.InstrumentTypeSignature,
        Language = instrument.Language,
        FirmwareBuildNumber = instrument.FirmwareBuildNumber,
        LanguagePackName = instrument.LanguagePackName,
        SoftwareVersion = instrument.SoftwareVersion
      };
      instrument.LastConnected = new DateTime?(DateTime.Now);
      this.modelHelper.Store(instrument2);
    }
    this.RefreshData();
  }
}
