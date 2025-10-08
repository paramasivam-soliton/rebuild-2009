// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Firmware.FirmwareImportTriggerContext
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Automaton;
using System;

#nullable disable
namespace PathMedical.Type1077.Firmware;

[CLSCompliant(false)]
public class FirmwareImportTriggerContext : TriggerContext
{
  public string Folder { get; protected set; }

  public bool Recursive { get; protected set; }

  public FirmwareImportTriggerContext(string folder, bool recursive)
  {
    this.Folder = folder;
    this.Recursive = recursive;
  }
}
