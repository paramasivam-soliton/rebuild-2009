// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Command.ChangeCreationTimeStampFilterCommand
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.PatientManagement.Command;

public class ChangeCreationTimeStampFilterCommand : CommandBase
{
  private readonly PatientManager model;

  public ChangeCreationTimeStampFilterCommand(PatientManager model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    this.Name = nameof (ChangeCreationTimeStampFilterCommand);
    this.model = model;
  }

  public override void Execute()
  {
    try
    {
      if (this.TriggerEventArgs == null || !(this.TriggerEventArgs.TriggerContext is SearchTriggerContext))
        return;
      SearchTriggerContext triggerContext = this.TriggerEventArgs.TriggerContext as SearchTriggerContext;
      if (!(triggerContext.SearchValue is CreationTimeStampFilterType))
        return;
      this.model.CreationTimeStampFilterType = (CreationTimeStampFilterType) triggerContext.SearchValue;
    }
    catch (ModelException ex)
    {
      throw;
    }
  }
}
