// Decompiled with JetBrains decompiler
// Type: PathMedical.Exception.ExceptionFactory
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Culture;
using PathMedical.Localization;
using PathMedical.Logging;
using PathMedical.Properties;
using PathMedical.Reflection;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace PathMedical.Exception;

public class ExceptionFactory : IExceptionFactory
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (ExceptionFactory));
  private readonly CultureInfo currentExceptionCulture;
  private CultureInfo configuredSystemCulture;
  private ICultureManager cultureManager;

  public static ExceptionFactory Instance => PathMedical.Singleton.Singleton<ExceptionFactory>.Instance;

  public CultureInfo CultureOfUserException { get; set; }

  public CultureInfo CultureOfSystemException { get; set; }

  public ICultureManager CultureManager
  {
    get => this.cultureManager;
    set
    {
      this.cultureManager = value;
      if (this.cultureManager == null || this.CultureOfUserException != null)
        return;
      this.CultureOfUserException = this.cultureManager.GetCurrentUICulture();
    }
  }

  private ExceptionFactory()
  {
    this.CultureOfSystemException = new CultureInfo("en-US");
    this.currentExceptionCulture = this.CultureOfSystemException;
  }

  public T CreateException<T>() where T : System.Exception
  {
    T exception;
    try
    {
      this.SetExceptionCulture();
      exception = ReflectionHelper.GetClass<T>(new object[0]);
    }
    catch (ReflectionHelperException ex)
    {
      ExceptionFactory.Logger.Error((System.Exception) ex, "Failure while creating exception of type {0}", (object) typeof (T));
      throw new System.Exception(string.Format(Resources.ExceptionFactory_ExceptionCreationFailure, (object) typeof (T)), (System.Exception) ex);
    }
    finally
    {
      this.RestoreCulture();
    }
    ExceptionFactory.Logger.Error((System.Exception) exception);
    return exception;
  }

  public T CreateException<T>([Localizable(true), LocalizationRequired(true)] string message) where T : System.Exception
  {
    if (string.IsNullOrEmpty(message))
      return ExceptionFactory.Instance.CreateException<T>();
    T exception;
    try
    {
      this.SetExceptionCulture();
      exception = ReflectionHelper.GetClass<T>(new object[1]
      {
        (object) message
      });
    }
    catch (ReflectionHelperException ex)
    {
      ExceptionFactory.Logger.Error((System.Exception) ex, "Failure while creating exception of type {0}", (object) typeof (T));
      throw new System.Exception(string.Format(Resources.ExceptionFactory_ExceptionCreationFailure, (object) typeof (T)), (System.Exception) ex);
    }
    finally
    {
      this.RestoreCulture();
    }
    ExceptionFactory.Logger.Error((System.Exception) exception);
    return exception;
  }

  public T CreateException<T>([Localizable(true)] string message, System.Exception innerException) where T : System.Exception
  {
    if (string.IsNullOrEmpty(message))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (message));
    if (innerException == null)
      return ExceptionFactory.Instance.CreateException<T>(message);
    try
    {
      this.SetExceptionCulture();
      T exception = ReflectionHelper.GetClass<T>(new object[2]
      {
        (object) message,
        (object) innerException
      });
      ExceptionFactory.Logger.Error((System.Exception) exception);
      return exception;
    }
    catch (ReflectionHelperException ex)
    {
      string message1 = string.Format(Resources.ExceptionFactory_ExceptionCreationFailure, (object) typeof (T));
      ExceptionFactory.Logger.Error((System.Exception) ex, "Failure while creating exception of type {0}", (object) typeof (T));
      throw new System.Exception(message1, (System.Exception) ex);
    }
    finally
    {
      this.RestoreCulture();
    }
  }

  protected void SetExceptionCulture()
  {
    if (this.cultureManager == null)
      return;
    if (this.configuredSystemCulture == null)
      this.configuredSystemCulture = CultureInfo.CurrentUICulture;
    this.cultureManager.SetCulture(this.currentExceptionCulture);
  }

  protected void RestoreCulture()
  {
    if (this.cultureManager == null || this.configuredSystemCulture == null)
      return;
    this.cultureManager.SetCulture(this.configuredSystemCulture);
    this.configuredSystemCulture = (CultureInfo) null;
  }
}
