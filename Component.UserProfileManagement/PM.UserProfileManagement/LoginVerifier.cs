// Decompiled with JetBrains decompiler
// Type: PathMedical.UserProfileManagement.LoginVerifier
// Assembly: PM.UserProfileManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 210CE7C9-6056-4099-A07B-EB2D78618349
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserProfileManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.Encryption;
using PathMedical.Exception;
using PathMedical.Login;
using PathMedical.Plugin;
using PathMedical.SystemConfiguration;
using PathMedical.UserProfileManagement.DataAccessLayer;
using PathMedical.UserProfileManagement.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

#nullable disable
namespace PathMedical.UserProfileManagement;

public class LoginVerifier : ILoginVerifier, IPlugin
{
  public static LoginVerifier Instance => PathMedical.Singleton.Singleton<LoginVerifier>.Instance;

  private LoginVerifier()
  {
    this.Fingerprint = new Guid("26663135-FB4D-4162-B490-7AD2DB8864FB");
    this.Name = Resources.LoginVerifier_ModuleName;
    this.Description = Resources.LoginVerifier_ModuleDescription;
  }

  [Localizable(true)]
  public string Name { get; private set; }

  [Localizable(true)]
  public string Description { get; private set; }

  public Guid Fingerprint { get; private set; }

  public int LoadPriority { get; protected set; }

  public LoginResult LoginUser(string loginName, string password)
  {
    LoginResult loginResult;
    using (DBScope scope = new DBScope())
    {
      UserAdapter adapter = new UserAdapter(scope);
      adapter.LoadWithRelation("Profile.ProfileAccessPermissions");
      ICollection<User> source = adapter.FetchEntities((Expression<Func<User, bool>>) (u => u.LoginName == loginName && u.IsActive == true));
      if (source.Count == 1)
      {
        User user = source.First<User>();
        if (this.IsPasswordCorrect(user, password) && LoginVerifier.CheckUserLock(user, adapter))
        {
          loginResult = LoginResult.CreateSuccessResult(new LoggedInUserData(user.Id, $"{user.Forename} {user.Surname}", (object) user));
        }
        else
        {
          UserManager.Instance.MarkUserWithBadLogin(user, adapter, scope.TransactionTimestamp);
          DateTime? lockTimestamp = user.LockTimestamp;
          if (lockTimestamp.HasValue)
          {
            lockTimestamp = user.LockTimestamp;
            TimeSpan timeSpan = lockTimestamp.Value.AddMinutes((double) SystemConfigurationManager.Instance.UserAccountLockingTime) - DateTime.Now;
            loginResult = LoginResult.CreateLockResult(timeSpan.Minutes + (timeSpan.Seconds != 0 ? 1 : 0));
          }
          else
            loginResult = LoginResult.CreateWrongPasswordResult(UserManager.MaximumFailedLogins - user.FailedLogins.Value);
        }
      }
      else
        loginResult = LoginResult.CreateNotFoundResult();
      scope.Complete();
    }
    return loginResult;
  }

  public string EncryptPassword(string password, Guid? salt)
  {
    if (salt.HasValue)
    {
      Guid? nullable = salt;
      Guid empty = Guid.Empty;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 0) == 0)
      {
        List<byte> byteList = new List<byte>();
        byteList.AddRange((IEnumerable<byte>) Encoding.Unicode.GetBytes(password ?? string.Empty));
        byteList.AddRange((IEnumerable<byte>) salt.Value.ToByteArray());
        return MD5Engine.GetMD5Hash(byteList.ToArray());
      }
    }
    throw ExceptionFactory.Instance.CreateException<ArgumentException>(nameof (salt));
  }

  private bool IsPasswordCorrect(User user, string password)
  {
    return this.EncryptPassword(password, user.PasswordSalt) == user.Password;
  }

  private static bool CheckUserLock(User user, UserAdapter adapter)
  {
    if (!user.LockTimestamp.HasValue)
      return true;
    if (SystemConfigurationManager.Instance.Plugins.Any<IPlugin>((Func<IPlugin, bool>) (p => p.Fingerprint.Equals(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554")))) || user.LockTimestamp.Value.AddMinutes((double) SystemConfigurationManager.Instance.UserAccountLockingTime) > DateTime.Now)
      return false;
    user.LockTimestamp = new DateTime?();
    adapter.Store(user);
    return true;
  }
}
