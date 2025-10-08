// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.WaveFormImport.WaveFileImportProgress
// Assembly: PM.PatientManagement.MultipleTestViewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2267BB5-2AA5-4F30-972C-4D9F0F378D06
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.MultipleTestViewer.WindowsForms.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.WaveFormImport;

[Obsolete]
internal class WaveFileImportProgress
{
  private readonly List<string> successfullyImported = new List<string>();

  public string Folder { get; set; }

  public string[] Files { get; set; }

  public int TotalFiles { get; set; }

  public int ProcessedFiles { get; set; }

  public string CurrentFile { get; set; }

  public string ActivityDescription { get; set; }

  public ProcessState ProcessState { get; set; }

  public string[] SuccessfullyImported => this.successfullyImported.ToArray();

  public void AddSuccessfullyImportedFile(string file) => this.successfullyImported.Add(file);
}
