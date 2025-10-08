// Decompiled with JetBrains decompiler
// Type: PathMedical.Logging.LogFactory
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

#nullable disable
namespace PathMedical.Logging;

public class LogFactory
{
  private readonly Dictionary<string, ILogger> loggerInventory;
  private Type logApplication;

  public static LogFactory Instance { get; private set; }

  static LogFactory() => LogFactory.Instance = new LogFactory();

  public Type LogApplication
  {
    get => this.logApplication;
    set => this.logApplication = value;
  }

  private LogFactory() => this.loggerInventory = new Dictionary<string, ILogger>();

  public ILogger Create(string loggerName) => this.Create(loggerName, string.Empty);

  public ILogger Create(string loggerName, [Localizable(false)] string revisionId)
  {
    int length = loggerName.IndexOf("`1");
    if (length > 0)
      loggerName = loggerName.Substring(0, length);
    ILogger logger = (ILogger) null;
    if (this.logApplication == (Type) null)
      this.logApplication = typeof (NetTraceLogger);
    if (this.loggerInventory.ContainsKey(loggerName))
      this.loggerInventory.TryGetValue(loggerName, out logger);
    if (logger == null)
    {
      Type[] types = new Type[1]{ typeof (string) };
      object[] parameters = new object[1]
      {
        (object) loggerName
      };
      try
      {
        ConstructorInfo constructor = this.logApplication.GetConstructor(types);
        object obj = !(constructor != (ConstructorInfo) null) ? Activator.CreateInstance(this.logApplication) : constructor.Invoke(parameters);
        switch (obj)
        {
          case null:
          case ILogger _:
            logger = obj != null ? obj as ILogger : throw ExceptionFactory.Instance.CreateException<LoggerCreateException>(Resources.LogFactory_LoggerCreationFailure);
            break;
          default:
            throw ExceptionFactory.Instance.CreateException<LoggerCreateException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, ComponentResourceManagement.Instance.ResourceManager.GetString("ImplementILoggerException"), (object) this.logApplication));
        }
      }
      catch (ArgumentException ex)
      {
        ExceptionFactory.Instance.CreateException<LoggerCreateException>(string.Empty, (System.Exception) ex);
      }
      catch (NotSupportedException ex)
      {
        ExceptionFactory.Instance.CreateException<LoggerCreateException>(string.Empty, (System.Exception) ex);
      }
      catch (TargetInvocationException ex)
      {
        ExceptionFactory.Instance.CreateException<LoggerCreateException>(string.Empty, (System.Exception) ex);
      }
      catch (MethodAccessException ex)
      {
        ExceptionFactory.Instance.CreateException<LoggerCreateException>(string.Empty, (System.Exception) ex);
      }
      catch (MissingMethodException ex)
      {
        ExceptionFactory.Instance.CreateException<LoggerCreateException>(string.Empty, (System.Exception) ex);
      }
      catch (MemberAccessException ex)
      {
        ExceptionFactory.Instance.CreateException<LoggerCreateException>(string.Empty, (System.Exception) ex);
      }
      catch (InvalidComObjectException ex)
      {
        ExceptionFactory.Instance.CreateException<LoggerCreateException>(string.Empty, (System.Exception) ex);
      }
      catch (COMException ex)
      {
        ExceptionFactory.Instance.CreateException<LoggerCreateException>(string.Empty, (System.Exception) ex);
      }
      catch (TypeLoadException ex)
      {
        ExceptionFactory.Instance.CreateException<LoggerCreateException>(string.Empty, (System.Exception) ex);
      }
      if (!string.IsNullOrEmpty(loggerName) && logger != null)
      {
        logger.RevisionId = revisionId;
        this.loggerInventory.Add(loggerName, logger);
      }
    }
    return logger;
  }

  public ILogger Create(Type requestedClass)
  {
    return this.Create(string.IsNullOrEmpty(requestedClass.FullName) ? requestedClass.Name : requestedClass.FullName, string.Empty);
  }

  public ILogger Create(Type requestedClass, [Localizable(false)] string revisionId)
  {
    return this.Create(string.IsNullOrEmpty(requestedClass.FullName) ? requestedClass.Name : requestedClass.FullName, revisionId);
  }
}
