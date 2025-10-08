// Decompiled with JetBrains decompiler
// Type: PathMedical.Logging.NetTraceLogger
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.FileSystem;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

#nullable disable
namespace PathMedical.Logging;

public class NetTraceLogger : LoggerBase
{
  private static TraceSource globalInstanceTracer;
  private static int loggerInstance;
  private readonly TraceSource localInstanceTracer;

  public override string RevisionId { get; set; }

  public NetTraceLogger()
  {
    if (NetTraceLogger.globalInstanceTracer == null)
      NetTraceLogger.globalInstanceTracer = new TraceSource("GlobalTracer", SourceLevels.Verbose);
    ++NetTraceLogger.loggerInstance;
  }

  public NetTraceLogger(string classToTrace)
    : this()
  {
    this.localInstanceTracer = new TraceSource(classToTrace, SourceLevels.Off);
  }

  public override void LogTo(string fileName)
  {
    if (FileSystemHelper.DoesFileExists(fileName))
    {
      try
      {
        FileSystemHelper.DeleteFile(fileName);
      }
      catch (Exception ex)
      {
      }
    }
    NetTraceLogger.globalInstanceTracer.Listeners.Add((TraceListener) new TextWriterTraceListener(fileName));
  }

  ~NetTraceLogger()
  {
    if (this.localInstanceTracer != null)
    {
      try
      {
        this.localInstanceTracer.Flush();
        this.localInstanceTracer.Close();
      }
      catch (ObjectDisposedException ex)
      {
      }
    }
    if (NetTraceLogger.loggerInstance > 0)
      --NetTraceLogger.loggerInstance;
    if (NetTraceLogger.loggerInstance != 0)
      return;
    if (NetTraceLogger.globalInstanceTracer == null)
      return;
    try
    {
      NetTraceLogger.globalInstanceTracer.Flush();
      NetTraceLogger.globalInstanceTracer.Close();
      Trace.Flush();
    }
    catch (ObjectDisposedException ex)
    {
    }
  }

  private void Write(
    TraceEventType eventTypeToTrace,
    int id,
    string message,
    Exception exceptionToLog)
  {
    StringBuilder stringBuilder = new StringBuilder();
    int managedThreadId = Thread.CurrentThread.ManagedThreadId;
    int num = -1;
    try
    {
      num = Process.GetCurrentProcess().Id;
    }
    catch (InvalidOperationException ex)
    {
    }
    catch (PlatformNotSupportedException ex)
    {
    }
    DateTime now = DateTime.Now;
    stringBuilder.Append(string.Format("[{5}] [{0:G} {1:000}] [Process {2}] [Thread {3}] {4}", (object) now, (object) now.Millisecond, (object) num, (object) managedThreadId, (object) message, string.IsNullOrEmpty(this.RevisionId) ? (object) "$Rev: Unknown$" : (object) this.RevisionId));
    if (exceptionToLog != null)
    {
      NetTraceLogger.globalInstanceTracer.TraceData(eventTypeToTrace, id, (object) stringBuilder.ToString(), (object) exceptionToLog);
      this.localInstanceTracer.TraceData(eventTypeToTrace, id, (object) message, (object) exceptionToLog);
    }
    else
    {
      NetTraceLogger.globalInstanceTracer.TraceData(eventTypeToTrace, id, (object) stringBuilder.ToString());
      this.localInstanceTracer.TraceData(eventTypeToTrace, id, (object) stringBuilder.ToString());
    }
  }

  protected override void LogError(Exception exception, string message)
  {
    this.Write(TraceEventType.Error, 0, message, exception);
  }

  public override bool IsErrorEnabled
  {
    get
    {
      return this.localInstanceTracer.Switch.Level <= SourceLevels.Error || NetTraceLogger.globalInstanceTracer.Switch.Level <= SourceLevels.Error;
    }
  }

  protected override void LogWarning(Exception exception, string message)
  {
    this.Write(TraceEventType.Warning, 0, message, exception);
  }

  public override bool IsWarningEnabled
  {
    get
    {
      return this.localInstanceTracer.Switch.Level <= SourceLevels.Warning || NetTraceLogger.globalInstanceTracer.Switch.Level <= SourceLevels.Warning;
    }
  }

  protected override void LogInfo(Exception exception, string message)
  {
    this.Write(TraceEventType.Information, 0, message, exception);
  }

  public override bool IsInfoEnabled
  {
    get
    {
      return this.localInstanceTracer.Switch.Level <= SourceLevels.Information || NetTraceLogger.globalInstanceTracer.Switch.Level <= SourceLevels.Information;
    }
  }

  protected override void LogDebug(Exception exception, string message)
  {
    this.Write(TraceEventType.Verbose, 0, message, exception);
  }

  public override bool IsDebugEnabled
  {
    get
    {
      return this.localInstanceTracer.Switch.Level <= SourceLevels.Verbose || NetTraceLogger.globalInstanceTracer.Switch.Level <= SourceLevels.Verbose;
    }
  }
}
