// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.UserProfileManager
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.ResourceManager;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement.DataAccessLayer;
using PathMedical.UserProfileManagement.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PathMedical.UserProfileManagement;

public class UserProfileManager : 
  ISingleEditingModel,
  IModel,
  ISingleSelectionModel<UserProfile>,
  ISingleSelectionModel<AccessPermission>
{
  private readonly ModelHelper<UserProfile, ProfileAdapter> modelHelper;
  private List<AccessPermission> allAccessPermissions;
  private BindingList<AccessPermission> profileAccessPermissions;
  private AccessPermission selectedAccessPermission;

  public static UserProfileManager Instance => PathMedical.Singleton.Singleton<UserProfileManager>.Instance;

  private UserProfileManager()
  {
    this.modelHelper = new ModelHelper<UserProfile, ProfileAdapter>(new EventHandler<ModelChangedEventArgs>(this.ModelChanged), new string[1]
    {
      nameof (ProfileAccessPermissions)
    });
    this.LoadDataExchangeDescriptions();
  }

  public Guid ViewUsersAccessPermissionId => new Guid("1A594128-5655-4d8b-98FC-036E614C5145");

  public Guid MaintainUsersAccessPermissionId => new Guid("51AD22BD-2512-49e7-A9C0-6EE58E6EBFB9");

  public Guid AdministratorProfileId => new Guid("29FEC285-F8BD-4eae-937A-888B8B8DC92B");

  public Guid ScreenerProfileId => new Guid("D9E01228-B83D-477d-BF7C-7006982DA8E4");

  public UserProfile SelectedProfile
  {
    get => this.modelHelper.SelectedItems.FirstOrDefault<UserProfile>();
    set
    {
      this.modelHelper.SelectedItems = (ICollection<UserProfile>) new UserProfile[1]
      {
        value
      };
      this.BuildAccessPermissions();
    }
  }

  public List<UserProfile> Profiles
  {
    get
    {
      if (this.modelHelper.Items == null)
        this.LoadProfiles();
      return this.modelHelper.Items;
    }
  }

  public AccessPermission SelectedAccessPermission
  {
    get => this.selectedAccessPermission;
    set
    {
      if (this.selectedAccessPermission == value)
        return;
      this.selectedAccessPermission = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<AccessPermission>(this.selectedAccessPermission, ChangeType.SelectionChanged));
    }
  }

  public BindingList<AccessPermission> ProfileAccessPermissions
  {
    get
    {
      if (this.profileAccessPermissions == null && this.SelectedProfile != null)
        this.BuildAccessPermissions();
      return this.profileAccessPermissions;
    }
    set
    {
      this.profileAccessPermissions = value;
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<BindingList<AccessPermission>>(this.profileAccessPermissions, ChangeType.ListLoaded));
    }
  }

  public List<AccessPermission> AllAccessPermissions
  {
    get
    {
      if (this.allAccessPermissions == null)
        this.allAccessPermissions = CurrentDataHelper<AccessPermission>.Instance.Items.ToList<AccessPermission>();
      return this.allAccessPermissions;
    }
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void Store()
  {
    if (this.SelectedProfile == null)
      return;
    if (this.Profiles.Any<UserProfile>((Func<UserProfile, bool>) (p => p.Id != this.SelectedProfile.Id && p.Name.Equals(this.SelectedProfile.Name, StringComparison.InvariantCultureIgnoreCase))))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("ProfileWithSameNameExists"));
    this.SelectedProfile.ProfileAccessPermissions = this.ProfileAccessPermissions.Select<AccessPermission, AccessPermission>((Func<AccessPermission, AccessPermission>) (pap => pap.CreateShallowPropertyCopy<AccessPermission>())).ToList<AccessPermission>();
    this.modelHelper.Store();
  }

  public void Delete()
  {
    if (this.SelectedProfile == null)
      return;
    if (UserManager.Instance.Users.Any<User>((Func<User, bool>) (u => u.Profile != null && u.Profile.Id == this.SelectedProfile.Id)))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CannotDeleteProfileInUse"));
    this.SelectedProfile.ProfileAccessPermissions = (List<AccessPermission>) null;
    this.modelHelper.Store();
    this.modelHelper.Delete();
  }

  public void CancelNewItem() => this.modelHelper.CancelAddItem();

  public void PrepareAddItem()
  {
    this.modelHelper.PrepareAddItem(new UserProfile());
    this.BuildAccessPermissions();
  }

  public void RefreshData() => this.LoadProfiles();

  public void RevertModifications()
  {
    if (this.modelHelper.SelectedItem != null && this.modelHelper.SelectedItem.Id == Guid.Empty)
    {
      if (this.Changed == null)
        return;
      this.modelHelper.SelectedItem.ProfileAccessPermissions = (List<AccessPermission>) null;
      this.BuildAccessPermissions();
      this.Changed((object) this, ModelChangedEventArgs.Create<UserProfile>(this.modelHelper.SelectedItem, ChangeType.SelectionChanged));
    }
    else
    {
      this.modelHelper.RefreshSelectedItems();
      this.BuildAccessPermissions();
    }
  }

  private void LoadProfiles()
  {
    this.modelHelper.LoadItems((Func<ProfileAdapter, ICollection<UserProfile>>) (adapter => adapter.All));
  }

  private void ModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed(sender, e);
  }

  private void BuildAccessPermissions()
  {
    List<AccessPermission> list = this.AllAccessPermissions.Select<AccessPermission, AccessPermission>((Func<AccessPermission, AccessPermission>) (ap => ap.CreateShallowPropertyCopy<AccessPermission>())).ToList<AccessPermission>();
    list.ForEach((Action<AccessPermission>) (pa => pa.AccessPermissionFlag = false));
    if (this.SelectedProfile != null && this.SelectedProfile.ProfileAccessPermissions != null)
    {
      foreach (AccessPermission accessPermission1 in this.SelectedProfile.ProfileAccessPermissions)
      {
        if (accessPermission1 != null)
        {
          AccessPermission permission = accessPermission1;
          AccessPermission accessPermission2 = list.FirstOrDefault<AccessPermission>((Func<AccessPermission, bool>) (pap => pap.Id == permission.Id));
          if (accessPermission2 != null)
            accessPermission2.AccessPermissionFlag = accessPermission1.AccessPermissionFlag;
        }
      }
    }
    this.ProfileAccessPermissions = new BindingList<AccessPermission>((IList<AccessPermission>) list);
    if (this.SelectedProfile == null)
      return;
    this.SelectedProfile.ProfileAccessPermissions = list;
  }

  public void ReportAccessPermissionModification()
  {
    if (this.Changed != null)
      this.Changed((object) this, ModelChangedEventArgs.Create<AccessPermission>(this.SelectedAccessPermission, ChangeType.ItemEdited));
    this.ProfileAccessPermissions.ResetBindings();
  }

  public void ChangeSingleSelection(UserProfile selection) => this.SelectedProfile = selection;

  bool ISingleSelectionModel<UserProfile>.IsOneItemSelected<T>()
  {
    return this.selectedAccessPermission != null;
  }

  bool ISingleSelectionModel<UserProfile>.IsOneItemAvailable<T>()
  {
    return this.Profiles != null && this.Profiles.Count > 0;
  }

  public void ChangeSingleSelection(AccessPermission selection)
  {
    this.SelectedAccessPermission = selection;
  }

  bool ISingleSelectionModel<AccessPermission>.IsOneItemSelected<T>()
  {
    return this.SelectedAccessPermission != null;
  }

  bool ISingleSelectionModel<AccessPermission>.IsOneItemAvailable<T>()
  {
    return this.AllAccessPermissions != null && this.AllAccessPermissions.Count > 0;
  }

  public List<DataExchangeSetMap> RecordSetMaps { get; protected set; }

  public List<RecordDescriptionSet> RecordDescriptionSets { get; protected set; }

  private void LoadDataExchangeDescriptions()
  {
    this.RecordDescriptionSets = new List<RecordDescriptionSet>();
    this.RecordSetMaps = new List<DataExchangeSetMap>();
    this.RecordDescriptionSets.Add(RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.UserProfileManagement.DataExchange.PluginDataDescription.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.UserProfileManager_Exception_PluginDataDescriptionMissing)));
  }
}
