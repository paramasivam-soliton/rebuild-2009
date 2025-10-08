// Decompiled with JetBrains decompiler
// Type: PathMedical.Logging.LoggerBase
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.Logging;

public abstract class LoggerBase : ILogger
{
  protected abstract void LogError(System.Exception exception, string message);

  protected abstract void LogDebug(System.Exception exception, string message);

  protected abstract void LogInfo(System.Exception exception, string message);

  protected abstract void LogWarning(System.Exception exception, string message);

  public abstract string RevisionId { get; set; }

  public virtual void Error(System.Exception exception)
  {
    if (!this.IsErrorEnabled)
      return;
    this.LogError(exception, string.Empty);
  }

  public virtual void Error(System.Exception exception, string message)
  {
    if (!this.IsErrorEnabled)
      return;
    this.LogError(exception, message);
  }

  public virtual void Error(System.Exception exception, string message, params object[] messageArgs)
  {
    if (!this.IsErrorEnabled)
      return;
    this.LogError(exception, string.Format(message, messageArgs));
  }

  public virtual void Error(string message)
  {
    if (!this.IsErrorEnabled)
      return;
    this.LogError((System.Exception) null, message);
  }

  public virtual void Error(string message, params object[] messageArgs)
  {
    if (!this.IsErrorEnabled)
      return;
    this.LogError((System.Exception) null, string.Format(message, messageArgs));
  }

  public abstract bool IsErrorEnabled { get; }

  public virtual void Warning(System.Exception exception)
  {
    if (!this.IsWarningEnabled)
      return;
    this.LogWarning(exception, string.Empty);
  }

  public virtual void Warning(System.Exception exception, string message)
  {
    if (!this.IsWarningEnabled)
      return;
    this.LogWarning(exception, message);
  }

  public virtual void Warning(System.Exception exception, string message, params object[] messageArgs)
  {
    if (!this.IsWarningEnabled)
      return;
    this.LogWarning(exception, string.Format(message, messageArgs));
  }

  public virtual void Warning(string message)
  {
    if (!this.IsWarningEnabled)
      return;
    this.LogWarning((System.Exception) null, message);
  }

  public virtual void Warning(string message, params object[] messageArgs)
  {
    if (!this.IsWarningEnabled)
      return;
    this.LogWarning((System.Exception) null, string.Format(message, messageArgs));
  }

  public abstract bool IsWarningEnabled { get; }

  public virtual void Info(System.Exception exception)
  {
    if (!this.IsInfoEnabled)
      return;
    this.LogInfo(exception, string.Empty);
  }

  public virtual void Info(System.Exception exception, string message)
  {
    if (!this.IsInfoEnabled)
      return;
    this.LogInfo(exception, message);
  }

  public virtual void Info(System.Exception exception, string message, params object[] messageArgs)
  {
    if (!this.IsInfoEnabled)
      return;
    this.LogInfo(exception, string.Format(message, messageArgs));
  }

  public virtual void Info(string message)
  {
    if (!this.IsInfoEnabled)
      return;
    this.LogInfo((System.Exception) null, message);
  }

  public virtual void Info(string message, params object[] messageArgs)
  {
    if (!this.IsInfoEnabled)
      return;
    this.LogInfo((System.Exception) null, string.Format(message, messageArgs));
  }

  public abstract bool IsInfoEnabled { get; }

  public virtual void Debug(System.Exception exception)
  {
    if (!this.IsDebugEnabled)
      return;
    this.LogDebug(exception, string.Empty);
  }

  public virtual void Debug(System.Exception exception, string message)
  {
    if (!this.IsDebugEnabled)
      return;
    this.LogDebug(exception, message);
  }

  public virtual void Debug(System.Exception exception, string message, params object[] messageArgs)
  {
    if (!this.IsDebugEnabled)
      return;
    this.LogDebug(exception, string.Format(message, messageArgs));
  }

  public virtual void Debug(string message)
  {
    if (!this.IsDebugEnabled)
      return;
    this.LogDebug((System.Exception) null, message);
  }

  public virtual void Debug(string message, params object[] messageArgs)
  {
    if (!this.IsDebugEnabled)
      return;
    this.LogDebug((System.Exception) null, string.Format(message, messageArgs));
  }

  public abstract bool IsDebugEnabled { get; }

  public abstract void LogTo(string fileName);
}
