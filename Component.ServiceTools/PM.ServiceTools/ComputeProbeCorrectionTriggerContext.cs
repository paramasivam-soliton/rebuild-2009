// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.Automaton.ComputeProbeCorrectionTriggerContext
// Assembly: PM.ServiceTools, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6893656-9FB8-40DB-89B4-7E4443C86B76
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.dll

using PathMedical.Automaton;
using PathMedical.Type1077;

#nullable disable
namespace PathMedical.ServiceTools.Automaton;

public class ComputeProbeCorrectionTriggerContext : TriggerContext
{
  public int Frequency { get; protected set; }

  public int Speaker { get; protected set; }

  public double FrequencySpeakerCorrectionValue { get; protected set; }

  public Type1077ProbeInformation Probe { get; protected set; }

  public ComputeProbeCorrectionTriggerContext(
    Type1077ProbeInformation probe,
    int speaker,
    int frequency,
    double frequencySpeakerCorrectionValue)
  {
    this.Probe = probe;
    this.FrequencySpeakerCorrectionValue = frequencySpeakerCorrectionValue;
    this.Frequency = frequency;
    this.Speaker = speaker;
  }
}
