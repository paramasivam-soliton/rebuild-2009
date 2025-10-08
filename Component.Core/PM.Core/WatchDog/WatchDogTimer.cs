// Decompiled with JetBrains decompiler
// Type: PathMedical.WatchDog.WatchDogTimer
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Logging;
using System;
using System.Timers;

#nullable disable
namespace PathMedical.WatchDog;

public class WatchDogTimer
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (WatchDogTimer), "$Rev: 1285 $");
  private readonly Timer watchDogTimer;
  private readonly Action watchDogAction;

  public Guid Id { get; protected set; }

  public WatchDogTimer(int interval, Action watchDogAction)
  {
    if (interval <= 0)
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (interval));
    if (watchDogAction == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (watchDogAction));
    this.Id = Guid.NewGuid();
    WatchDogTimer.Logger.Debug("Creating watch dog {1} to check every {0} ms.", (object) interval, (object) this.Id);
    this.watchDogTimer = new Timer((double) interval);
    this.watchDogAction = watchDogAction;
    this.watchDogTimer.Elapsed += new ElapsedEventHandler(this.WatchDogTimerElapsed);
  }

  private void WatchDogTimerElapsed(object sender, ElapsedEventArgs e)
  {
    if (sender == null || e == null)
      return;
    WatchDogTimer.Logger.Debug("Watch dog {1} is executing defined timeout action at {0}", (object) e.SignalTime, (object) this.Id);
    try
    {
      this.watchDogAction();
    }
    catch (System.Exception ex)
    {
      WatchDogTimer.Logger.Error(ex, "Failure while executing action for watch dog {0}.", (object) this.Id);
      throw;
    }
    WatchDogTimer.Logger.Debug("Watch dog {1} has executed defined timeout action at {0:}", (object) DateTime.Now, (object) this.Id);
  }

  public void ChangeInterval(int interval)
  {
    if (interval <= 0)
      throw ExceptionFactory.Instance.CreateException<ArgumentOutOfRangeException>(nameof (interval));
    WatchDogTimer.Logger.Debug("Changing watch dog intervall from {0} to {1}", (object) this.watchDogTimer.Interval, (object) interval);
    int num = this.watchDogTimer.Enabled ? 1 : 0;
    this.DeActivate();
    this.watchDogTimer.Interval = (double) interval;
    if (num == 0)
      return;
    this.Activate();
  }

  public void Activate()
  {
    if (this.watchDogTimer.Enabled)
      return;
    WatchDogTimer.Logger.Debug("Activating watch dog.");
    this.watchDogTimer.Enabled = true;
    this.watchDogTimer.Start();
  }

  public void DeActivate()
  {
    if (!this.watchDogTimer.Enabled)
      return;
    WatchDogTimer.Logger.Debug("Deactivating watch dog.");
    this.watchDogTimer.Enabled = false;
    this.watchDogTimer.Stop();
  }

  public void Restart(int interval)
  {
    if (!this.watchDogTimer.Enabled)
      return;
    this.watchDogTimer.Stop();
    if (interval > 0)
      this.watchDogTimer.Interval = (double) interval;
    this.watchDogTimer.Start();
  }
}
