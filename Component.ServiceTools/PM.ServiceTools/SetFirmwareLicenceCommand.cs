// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.SetFirmwareLicenceCommand
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton.Command;
using PathMedical.InstrumentManagement;
using PathMedical.Type1077;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

public class SetFirmwareLicenceCommand : CommandBase
{
  private Type1077Manager Model { get; set; }

  public SetFirmwareLicenceCommand(Type1077Manager model)
  {
    this.Name = "SetFirmwareLicence";
    this.Model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || this.TriggerEventArgs.TriggerContext == null || !(this.TriggerEventArgs.TriggerContext is SetFirmwareLicenceTriggerContext))
      return;
    SetFirmwareLicenceTriggerContext triggerContext = this.TriggerEventArgs.TriggerContext as SetFirmwareLicenceTriggerContext;
    this.Model.setFirmwareLicence((IInstrument) triggerContext.Instrument, triggerContext.LicenceKey);
  }
}
