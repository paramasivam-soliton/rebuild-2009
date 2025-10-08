// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ApplicationComponentModuleBase`2
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.ComponentModel;

#nullable disable
namespace PathMedical.UserInterface;

public abstract class ApplicationComponentModuleBase<TModel, TView> : IApplicationComponentModule
  where TModel : class, IModel
  where TView : class, IView
{
  public Guid Id { get; protected set; }

  [Localizable(true)]
  public string ShortcutName { get; protected set; }

  [Localizable(true)]
  public string Name { get; protected set; }

  [Localizable(true)]
  public string Description { get; protected set; }

  public IView ContentControl
  {
    get => this.Controller != null ? (IView) this.Controller.View : (IView) null;
  }

  public IController<TModel, TView> Controller { get; protected set; }

  public object Image { get; protected set; }

  public virtual void Start(Trigger additionalTrigger)
  {
    if (this.Controller == null)
      return;
    this.Controller.OnViewRequestsControllerAction((object) this, new TriggerEventArgs(Triggers.StartModule));
    if (!(additionalTrigger != (Trigger) null))
      return;
    this.Controller.OnViewRequestsControllerAction((object) this, new TriggerEventArgs(additionalTrigger));
  }

  public virtual bool Suspend()
  {
    bool flag = true;
    if (this.Controller != null)
    {
      TriggerEventArgs triggerEventArgs = new TriggerEventArgs(Triggers.CloseModule);
      this.Controller.OnViewRequestsControllerAction((object) this, triggerEventArgs);
      if (triggerEventArgs.Cancel)
        flag = false;
    }
    return flag;
  }

  public virtual bool IsAccessGranted(Trigger trigger)
  {
    bool flag = false;
    if (this.Controller != null && trigger != (Trigger) null)
      flag = this.Controller.IsAccessGranted(trigger);
    return flag;
  }
}
