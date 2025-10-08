// Decompiled with JetBrains decompiler
// Type: PathMedical.Logging.ILogger
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.ComponentModel;

#nullable disable
namespace PathMedical.Logging;

public interface ILogger
{
  string RevisionId { get; set; }

  void Error(System.Exception exception);

  void Error(System.Exception exception, string message);

  void Error(System.Exception exception, [Localizable(false)] string message, params object[] messageArgs);

  void Error(string message);

  void Error(string message, params object[] messageArgs);

  bool IsErrorEnabled { get; }

  void Warning(System.Exception exception);

  void Warning(System.Exception exception, string message);

  void Warning(System.Exception exception, string message, params object[] messageArgs);

  void Warning(string message);

  void Warning(string message, params object[] messageArgs);

  bool IsWarningEnabled { get; }

  void Info(System.Exception exception);

  void Info(System.Exception exception, string message);

  void Info(System.Exception exception, string message, params object[] messageArgs);

  void Info(string message);

  void Info(string message, params object[] messageArgs);

  bool IsInfoEnabled { get; }

  void Debug(Exception exception);

  void Debug(Exception exception, string message);

  void Debug(Exception exception, string message, params object[] messageArgs);

  void Debug(string message);

  void Debug([Localizable(false)] string message, params object[] messageArgs);

  bool IsDebugEnabled { get; }

  void LogTo(string fileName);
}
