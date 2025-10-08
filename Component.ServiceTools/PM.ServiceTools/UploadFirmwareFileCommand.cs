// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.UploadFirmwareFileCommand
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton.Command;
using PathMedical.InstrumentManagement;
using PathMedical.Type1077;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

public class UploadFirmwareFileCommand : CommandBase
{
  private Type1077Manager Model { get; set; }

  public UploadFirmwareFileCommand(Type1077Manager model)
  {
    this.Name = nameof (UploadFirmwareFileCommand);
    this.Model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null)
      return;
    this.Model.uploadSelectedfirmware((IInstrument) (this.TriggerEventArgs.TriggerContext as UploadFirmwareFileTriggerContext).Instrument);
  }
}
