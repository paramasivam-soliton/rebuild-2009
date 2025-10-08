// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.IsViewValidGuard
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class IsViewValidGuard : GuardBase
{
  private readonly IView view;
  private int handledTriggerRequestId;
  private bool isFormDataValid;

  public IsViewValidGuard(IView view)
  {
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.Name = nameof (IsViewValidGuard);
    this.view = view;
  }

  public override bool Execute(TriggerEventArgs e)
  {
    if (this.handledTriggerRequestId != TriggerRequestScope.CurrentId)
    {
      this.isFormDataValid = this.view.IsFormDataValid;
      this.handledTriggerRequestId = TriggerRequestScope.CurrentId;
    }
    return this.isFormDataValid;
  }
}
