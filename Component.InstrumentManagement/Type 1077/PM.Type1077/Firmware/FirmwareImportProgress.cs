// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Firmware.FirmwareImportProgress
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using System.Collections.Generic;

#nullable disable
namespace PathMedical.Type1077.Firmware;

public class FirmwareImportProgress
{
  private readonly List<string> successfullyImported = new List<string>();

  public string Folder { get; set; }

  public string IsSearchRecursive { get; set; }

  public string[] Files { get; set; }

  public int TotalFiles { get; set; }

  public int ProcessedFiles { get; set; }

  public string CurrentFile { get; set; }

  public string ActivityDescription { get; set; }

  public ProcessState ProcessState { get; set; }

  public string[] SuccessfullyImported => this.successfullyImported.ToArray();

  public void AddSuccessfullyImportedFile(string file) => this.successfullyImported.Add(file);
}
