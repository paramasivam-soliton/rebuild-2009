// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Command.ChangeTranslationCommand
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.PatientManagement.Command;

public class ChangeTranslationCommand : CommandBase
{
  private readonly ISupportInternationalization model;
  private IView view;

  public ChangeTranslationCommand(ISupportInternationalization model, IView view)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    if (view == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("View");
    this.model = model;
    this.view = view;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || !(this.TriggerEventArgs.TriggerContext is ChangeCultureTriggerContext))
      return;
    this.model.ChangeLanguage((this.TriggerEventArgs.TriggerContext as ChangeCultureTriggerContext).CultureInfo);
  }
}
