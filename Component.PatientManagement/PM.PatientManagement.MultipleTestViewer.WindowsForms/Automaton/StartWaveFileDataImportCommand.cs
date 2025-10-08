// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Automaton.StartWaveFileDataImportCommand
// Assembly: PM.PatientManagement.MultipleTestViewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2267BB5-2AA5-4F30-972C-4D9F0F378D06
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.MultipleTestViewer.WindowsForms.dll

using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Data;
using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties;
using PathMedical.ResourceManager;
using System;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Automaton;

public class StartWaveFileDataImportCommand : CommandBase
{
  private readonly WaveFormDataManager waveFormDataManager;

  public StartWaveFileDataImportCommand(WaveFormDataManager model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (StartWaveFileDataImportCommand);
    this.waveFormDataManager = model;
  }

  public override void Execute()
  {
    if (this.waveFormDataManager == null || this.TriggerEventArgs == null)
      return;
    OpenFileDialog openFileDialog1 = new OpenFileDialog();
    openFileDialog1.Title = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("OpenFileDialogTitle");
    openFileDialog1.Multiselect = true;
    OpenFileDialog openFileDialog2 = openFileDialog1;
    if (openFileDialog2.ShowDialog() != DialogResult.OK)
      return;
    this.waveFormDataManager.StartDataDownloading(new FileInformationTriggerContext(openFileDialog2.FileNames).FilesToLoad);
  }
}
