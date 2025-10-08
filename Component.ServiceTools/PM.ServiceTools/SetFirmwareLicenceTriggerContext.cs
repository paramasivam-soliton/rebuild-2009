// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.SetFirmwareLicenceTriggerContext
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton;
using PathMedical.InstrumentManagement;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

public class SetFirmwareLicenceTriggerContext : TriggerContext
{
  public Instrument Instrument { get; protected set; }

  public string LicenceKey { get; protected set; }

  public SetFirmwareLicenceTriggerContext(Instrument instrument, string licenceKey)
  {
    this.Instrument = instrument;
    this.LicenceKey = licenceKey;
  }
}
