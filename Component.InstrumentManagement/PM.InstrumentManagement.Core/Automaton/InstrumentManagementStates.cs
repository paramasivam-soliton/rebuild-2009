// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.Automaton.InstrumentManagementStates
// Assembly: PM.InstrumentManagement.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FE647C98-A102-42B0-8F3B-3BC3217F0325
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.InstrumentManagement.Core.dll

using PathMedical.Automaton;

#nullable disable
namespace PathMedical.InstrumentManagement.Automaton;

public abstract class InstrumentManagementStates
{
  public static readonly State SearchingInstruments = new State(nameof (SearchingInstruments));
  public static readonly State DownloadingInstrumentData = new State(nameof (DownloadingInstrumentData));
}
