// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Automaton.ImportConfigurationCommand
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using PathMedical.Automaton.Command;
using PathMedical.Exception;
using System;

#nullable disable
namespace PathMedical.DataExchange.eSP.Automaton;

public class ImportConfigurationCommand : CommandBase
{
  private readonly EspManager model;

  public ImportConfigurationCommand(EspManager model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    this.Name = nameof (ImportConfigurationCommand);
    this.model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null)
      return;
    this.model.ImportConfiguration();
  }
}
