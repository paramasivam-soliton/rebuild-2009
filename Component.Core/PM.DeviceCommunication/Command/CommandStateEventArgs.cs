// Decompiled with JetBrains decompiler
// Type: PathMedical.DeviceCommunication.Command.CommandStateEventArgs
// Assembly: PM.DeviceCommunication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E130EB71-E7EE-44AC-9835-8527DD4D919F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.DeviceCommunication.dll

using System;
using System.Diagnostics;

#nullable disable
namespace PathMedical.DeviceCommunication.Command;

[DebuggerDisplay("State = {State}")]
public class CommandStateEventArgs : EventArgs
{
  public CommandState OldState { get; protected set; }

  public CommandState NewState { get; protected set; }

  public CommandStateEventArgs(CommandState oldState, CommandState newState)
  {
    this.OldState = oldState;
    this.NewState = newState;
  }
}
