// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Automaton.StartFirmwareImportCommand
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.Automaton.Command;
using PathMedical.Type1077.Firmware;
using System;

#nullable disable
namespace PathMedical.Type1077.Automaton;

[CLSCompliant(false)]
public class StartFirmwareImportCommand : CommandBase
{
  private readonly FirmwareManager firmwareManager;

  public StartFirmwareImportCommand(FirmwareManager firmwareManager)
  {
    this.firmwareManager = firmwareManager;
  }

  public override void Execute()
  {
    if (this.firmwareManager == null || this.TriggerEventArgs == null || !(this.TriggerEventArgs.TriggerContext is FirmwareImportTriggerContext triggerContext))
      return;
    this.firmwareManager.ImportFromFolder = triggerContext.Folder;
    this.firmwareManager.SearchImportFolderRecursive = triggerContext.Recursive;
    this.firmwareManager.StartImport();
  }
}
