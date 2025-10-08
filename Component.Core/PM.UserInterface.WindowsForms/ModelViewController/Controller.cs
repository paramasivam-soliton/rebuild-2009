// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ModelViewController.Controller`2
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Logging;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement;
using System;
using System.Threading;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.ModelViewController;

public abstract class Controller<TModel, TView> : IController<TModel, TView>, IDisposable
  where TModel : class, IModel
  where TView : class, IView
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(nameof (Controller<TModel, TView>), "$Rev: 1378 $");
  private TView view;
  private TriggerEventArgs executingTriggerEventArgs;
  private Thread asynchronusTriggerThread;
  protected bool InstanceDisposed;

  public TModel Model { get; protected set; }

  public TView View
  {
    get => this.view;
    protected set
    {
      this.view = value;
      this.view.ControllerActionRequested += new EventHandler<TriggerEventArgs>(this.OnViewRequestsControllerAction);
    }
  }

  public IApplicationComponentModule ApplicationComponentModule { get; private set; }

  protected Controller()
  {
  }

  protected Controller(
    IApplicationComponentModule applicationComponentModule)
  {
    this.ApplicationComponentModule = applicationComponentModule;
  }

  protected StateMachine StateMachine { get; set; }

  private void ExecuteTriggerAsynchronously()
  {
    try
    {
      this.asynchronusTriggerThread = new Thread(new ThreadStart(this.ExecuteTrigger))
      {
        Name = $"Asynchronous Trigger Execution {this.executingTriggerEventArgs.RequestedTrigger}"
      };
      this.asynchronusTriggerThread.Start();
    }
    catch (OutOfMemoryException ex)
    {
    }
    catch (ThreadStateException ex)
    {
    }
  }

  private void ExecuteTrigger()
  {
    try
    {
      if (this.executingTriggerEventArgs.TriggerCallBack != null)
        this.executingTriggerEventArgs.TriggerCallBack((object) this, new TriggerExecutedEventArgs(TriggerExecutionState.Running, this.executingTriggerEventArgs));
      this.StateMachine.Execute(this.executingTriggerEventArgs);
      if (this.executingTriggerEventArgs.TriggerCallBack != null)
        this.executingTriggerEventArgs.TriggerCallBack((object) this, new TriggerExecutedEventArgs(TriggerExecutionState.Executed, this.executingTriggerEventArgs));
      if (this.ApplicationComponentModule != UserInterfaceManager.Instance.ActiveApplicationComponentModule)
        return;
      UserInterfaceManager.Instance.SetAllowedTriggers(this.StateMachine.GetAllowedTriggers(), this.StateMachine.GeCurrentTrigger());
    }
    catch (Exception ex)
    {
      if (this.executingTriggerEventArgs.TriggerCallBack != null)
        this.executingTriggerEventArgs.TriggerCallBack((object) this, new TriggerExecutedEventArgs(TriggerExecutionState.Failed, this.executingTriggerEventArgs)
        {
          Text = ex.Message
        });
      else
        throw;
    }
  }

  public void OnViewRequestsControllerAction(object sender, TriggerEventArgs triggerEventArgs)
  {
    if (this.StateMachine != null && triggerEventArgs != null)
    {
      this.executingTriggerEventArgs = triggerEventArgs;
      if (this.executingTriggerEventArgs.IsAsynchronousExecution)
        this.ExecuteTriggerAsynchronously();
      else
        this.ExecuteTrigger();
    }
    else
    {
      if (triggerEventArgs == null)
        return;
      Controller<TModel, TView>.Logger.Error("State machine is undefined and can't execute trigger {0}", (object) triggerEventArgs.RequestedTrigger);
    }
  }

  public void CleanUpView()
  {
    if ((object) this.View == null)
      return;
    this.View.CleanUpView();
  }

  public bool IsAccessGranted(Trigger trigger)
  {
    bool flag = false;
    if (trigger != (Trigger) null && this.StateMachine != null)
      flag = this.StateMachine.IsAccessGranted(trigger);
    return flag;
  }

  public virtual void Dispose()
  {
    if (this.InstanceDisposed)
      return;
    this.InstanceDisposed = true;
    this.View.Dispose();
    this.View = default (TView);
    this.Model = default (TModel);
    GC.SuppressFinalize((object) this);
  }
}
