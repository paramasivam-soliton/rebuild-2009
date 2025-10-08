// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.UserManager
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DataExchange;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Login;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserProfileManagement.DataAccessLayer;
using PathMedical.UserProfileManagement.Properties;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

#nullable disable
namespace PathMedical.UserProfileManagement;

public class UserManager : ISingleEditingModel, IModel, ISingleSelectionModel<User>
{
  private readonly ModelHelper<User, UserAdapter> modelHelper;
  private Dictionary<User, string> userPasswords;
  public static readonly int MaximumFailedLogins = 3;
  private RecordDescriptionSet recordDescriptionSet;

  private UserManager()
  {
    this.modelHelper = new ModelHelper<User, UserAdapter>(new EventHandler<ModelChangedEventArgs>(this.ModelChanged), new string[1]
    {
      "Profile.ProfileAccessPermissions"
    });
    UserProfileManager.Instance.Changed += new EventHandler<ModelChangedEventArgs>(this.ModelChanged);
  }

  public static UserManager Instance => PathMedical.Singleton.Singleton<UserManager>.Instance;

  public Guid ViewUsersAccessPermissionId => new Guid("1A594128-5655-4d8b-98FC-036E614C5145");

  public Guid MaintainUsersAccessPermissionId => new Guid("51AD22BD-2512-49e7-A9C0-6EE58E6EBFB9");

  public Guid DeleteUserAccountPermissionId => new Guid("BBB96902-EE62-44c7-ADD9-045B2DA35C73");

  public Guid ConfigureUserManagementPermissionId
  {
    get => new Guid("1EEB95DC-6A1D-471d-A3F4-CF21B3214584");
  }

  public Guid UnlockUserAccountPermissionId => new Guid("3EE48430-8256-41D7-B54C-7B4B50D1355B");

  public Guid AdministratorAccountId => new Guid("5CEA63CB-F36F-4325-BC39-FC96B3FBC38B");

  public Guid ScreenerAccountId => new Guid("24E0BF29-6DDF-49fa-B8D0-F222A280A947");

  public List<User> Users
  {
    get
    {
      if (this.modelHelper.Items == null)
        this.LoadUsers();
      return this.modelHelper.Items;
    }
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void Store()
  {
    User selectedUser = this.modelHelper.SelectedItems.OfType<User>().SingleOrDefault<User>();
    if (selectedUser != null && this.Users.Any<User>((Func<User, bool>) (u => u.Id != selectedUser.Id && u.LoginName.Equals(selectedUser.LoginName, StringComparison.InvariantCultureIgnoreCase))))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("UserWithSameNameExists"));
    if (this.userPasswords != null && this.modelHelper.SelectedItem != null && !string.Equals(this.modelHelper.SelectedItem.Password, this.userPasswords.GetValueOrDefault<User, string>(this.modelHelper.SelectedItem)))
    {
      Guid guid = Guid.NewGuid();
      this.modelHelper.SelectedItem.Password = LoginManager.Instance.EncryptPassword(this.modelHelper.SelectedItem.Password, new Guid?(guid));
      this.modelHelper.SelectedItem.PasswordSalt = new Guid?(guid);
    }
    this.modelHelper.Store();
    this.RememberPasswords();
  }

  public void Delete()
  {
    if (this.modelHelper.SelectedItems == null || this.modelHelper.SelectedItems.Count == 0)
      return;
    if (this.modelHelper.SelectedItems.Any<User>((Func<User, bool>) (u => u.Id == this.AdministratorAccountId || u.Id == this.ScreenerAccountId)))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("CannotDeleteUser"));
    try
    {
      using (DBScope dbScope = new DBScope())
      {
        using (DbCommand dbCommand = dbScope.CreateDbCommand())
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.Append("DELETE FROM InstrumentUserAssociation");
          stringBuilder.AppendFormat(" WHERE UserId = @UserId");
          dbCommand.CommandText = stringBuilder.ToString();
          dbScope.AddDbParameter(dbCommand, "UserId", (object) this.modelHelper.SelectedItem.Id);
          dbCommand.ExecuteNonQuery();
        }
        this.modelHelper.Delete();
        dbScope.Complete();
      }
    }
    catch (System.Exception ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("Instrument can't be deleted."), ex);
    }
  }

  public void CancelNewItem() => this.modelHelper.CancelAddItem();

  public void PrepareAddItem()
  {
    this.modelHelper.PrepareAddItem(new User());
    this.RememberPasswords();
  }

  public void RefreshData()
  {
    if (this.Changed != null)
      this.Changed((object) this, ModelChangedEventArgs.Create<List<UserProfile>>(UserProfileManager.Instance.Profiles, ChangeType.ListLoaded));
    this.LoadUsers();
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<ICollection<User>>(this.modelHelper.SelectedItems, ChangeType.SelectionChanged));
  }

  public void RevertModifications()
  {
    if (this.modelHelper.SelectedItem != null && this.modelHelper.SelectedItem.Id == Guid.Empty)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<ICollection<User>>(this.modelHelper.SelectedItems, ChangeType.SelectionChanged));
    }
    else
      this.modelHelper.RefreshSelectedItems();
  }

  public void MarkUserWithBadLogin(User user, UserAdapter adapter, DateTime timestamp)
  {
    if (user.LockTimestamp.HasValue)
      return;
    int valueOrDefault = user.FailedLogins.GetValueOrDefault();
    user.FailedLogins = new int?(valueOrDefault + 1);
    int? failedLogins = user.FailedLogins;
    int maximumFailedLogins = UserManager.MaximumFailedLogins;
    if (failedLogins.GetValueOrDefault() == maximumFailedLogins & failedLogins.HasValue)
    {
      user.FailedLogins = new int?(0);
      user.LockTimestamp = new DateTime?(timestamp);
    }
    adapter.Store(user);
  }

  public DataExchangeTokenSet GetDataExchangeTokenSet(Guid id) => (DataExchangeTokenSet) null;

  private void LoadUsers()
  {
    this.modelHelper.LoadItems((Func<UserAdapter, ICollection<User>>) (adapter => adapter.All));
  }

  public void ChangeSingleSelection(User selection)
  {
    if (this.modelHelper == null)
      return;
    this.modelHelper.SelectedItem = selection;
    this.RememberPasswords();
  }

  bool ISingleSelectionModel<User>.IsOneItemSelected<T>()
  {
    bool flag = false;
    if (this.modelHelper != null)
      flag = this.modelHelper.SelectedItem != null;
    return flag;
  }

  bool ISingleSelectionModel<User>.IsOneItemAvailable<T>()
  {
    return this.Users != null && this.Users.Count > 0;
  }

  public RecordDescriptionSet RecordDescriptionSet
  {
    get
    {
      if (this.recordDescriptionSet == null)
        this.recordDescriptionSet = RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.UserProfileManagement.DataExchange.UserProfileDataExchangeSet.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.UserManager_Exception_PluginDataDescriptionMissing));
      return this.recordDescriptionSet;
    }
  }

  private void ModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangedObject is UserProfile)
    {
      int changeType = (int) e.ChangeType;
    }
    if (this.Changed == null)
      return;
    this.Changed(sender, e);
  }

  private void RememberPasswords()
  {
    this.userPasswords = this.modelHelper.SelectedItems.ToDictionary<User, User, string>((Func<User, User>) (u => u), (Func<User, string>) (u => u.Password));
  }

  public void ChangeSelection(Type selectionType, object selection)
  {
    if (selection == null)
      this.modelHelper.SelectedItem = new User();
    this.modelHelper.SelectedItem = selection as User;
  }

  public void Import(List<User> users)
  {
    if (users == null || users.Count == 0)
      return;
    using (DBScope scope = new DBScope())
    {
      AdapterBase<User> adapterBase = new AdapterBase<User>(scope);
      adapterBase.LoadWithRelation("Profile.ProfileAccessPermissions");
      adapterBase.Store((ICollection<User>) users);
      scope.Complete();
    }
  }

  public void Store(User user)
  {
    if (user == null)
      return;
    using (DBScope scope = new DBScope())
    {
      AdapterBase<User> adapterBase = new AdapterBase<User>(scope);
      adapterBase.LoadWithRelation("Profile.ProfileAccessPermissions");
      adapterBase.Store(user);
      scope.Complete();
    }
  }

  public void ChangePrivateUserSettings()
  {
    if (string.Equals(this.modelHelper.SelectedItem.Password, this.userPasswords.GetValueOrDefault<User, string>(this.modelHelper.SelectedItem)))
      return;
    Guid guid = Guid.NewGuid();
    this.modelHelper.SelectedItem.Password = LoginManager.Instance.EncryptPassword(this.modelHelper.SelectedItem.Password, new Guid?(guid));
    this.modelHelper.SelectedItem.PasswordSalt = new Guid?(guid);
    using (DBScope scope = new DBScope())
    {
      AdapterBase<User> adapterBase = new AdapterBase<User>(scope);
      adapterBase.LoadWithRelation("Profile.ProfileAccessPermissions");
      adapterBase.Store(this.modelHelper.SelectedItem);
      scope.Complete();
    }
  }

  public bool UnlockUserAccount()
  {
    bool flag = false;
    if (this.modelHelper.SelectedItems != null && this.modelHelper.SelectedItems.Count > 0)
    {
      User user = this.modelHelper.SelectedItems.FirstOrDefault<User>();
      if (user != null && user.LockTimestamp.HasValue)
      {
        user.LockTimestamp = new DateTime?();
        this.modelHelper.Store(user);
        flag = true;
      }
    }
    return flag;
  }
}
