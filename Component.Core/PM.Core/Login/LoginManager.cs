// Decompiled with JetBrains decompiler
// Type: PathMedical.Login.LoginManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Properties;
using System;

#nullable disable
namespace PathMedical.Login;

public class LoginManager
{
  public static LoginManager Instance => PathMedical.Singleton.Singleton<LoginManager>.Instance;

  private LoginManager()
  {
  }

  public bool IsUserLoggedIn => this.LoggedInUserData != null;

  public LoggedInUserData LoggedInUserData { get; set; }

  public ILoginVerifier LoginVerifier { get; private set; }

  public LoginResult TryLoginUser(string loginName, string password)
  {
    if (this.LoginVerifier == null)
      throw ExceptionFactory.Instance.CreateException<ApplicationException>(Resources.LoginManager_NoLoginVerifierAttached);
    LoginResult loginResult = this.LoginVerifier.LoginUser(loginName, password);
    if (loginResult.Kind == LoginResultKind.Successful)
      this.LoggedInUserData = loginResult.LoggedInUserData;
    return loginResult;
  }

  public string EncryptPassword(string password, Guid? salt)
  {
    if (this.LoginVerifier == null)
      throw ExceptionFactory.Instance.CreateException<ApplicationException>(Resources.LoginManager_NoLoginVerifierAttached);
    return this.LoginVerifier.EncryptPassword(password, salt);
  }

  public void LogoutUser() => this.LoggedInUserData = (LoggedInUserData) null;

  public void InitializeLoginVerifier(ILoginVerifier loginVerifier)
  {
    this.LoginVerifier = loginVerifier;
  }
}
