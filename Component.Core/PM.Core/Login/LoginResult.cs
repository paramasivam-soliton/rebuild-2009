// Decompiled with JetBrains decompiler
// Type: PathMedical.Login.LoginResult
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.Login;

public class LoginResult
{
  public LoginResultKind Kind { get; protected set; }

  public int? RemainingTries { get; protected set; }

  public int? LockTimeInMinutes { get; protected set; }

  public LoggedInUserData LoggedInUserData { get; protected set; }

  protected LoginResult()
  {
  }

  public static LoginResult CreateSuccessResult(LoggedInUserData loggedInUserData)
  {
    return new LoginResult()
    {
      Kind = LoginResultKind.Successful,
      LoggedInUserData = loggedInUserData
    };
  }

  public static LoginResult CreateWrongPasswordResult(int remainingTries)
  {
    return new LoginResult()
    {
      Kind = LoginResultKind.WrongPassword,
      RemainingTries = new int?(remainingTries)
    };
  }

  public static LoginResult CreateLockResult(int lockTimeInMinutes)
  {
    return new LoginResult()
    {
      Kind = LoginResultKind.UserLocked,
      LockTimeInMinutes = new int?(lockTimeInMinutes),
      RemainingTries = new int?(0)
    };
  }

  public static LoginResult CreateNotFoundResult()
  {
    return new LoginResult()
    {
      Kind = LoginResultKind.UnknownUser
    };
  }
}
