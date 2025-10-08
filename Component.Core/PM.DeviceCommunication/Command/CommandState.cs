// Decompiled with JetBrains decompiler
// Type: PathMedical.DeviceCommunication.Command.CommandState
// Assembly: PM.DeviceCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E130EB71-E7EE-44AC-9835-8527DD4D919F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DeviceCommunication.dll

#nullable disable
namespace PathMedical.DeviceCommunication.Command;

public enum CommandState
{
  Unknown,
  SendingOut,
  SentOut,
  Acknowledged,
  NotAcknowledged,
  Performing,
  Performed,
  Failed,
  InstrumentAbort,
  TransmissionError,
  TimedOut,
}
