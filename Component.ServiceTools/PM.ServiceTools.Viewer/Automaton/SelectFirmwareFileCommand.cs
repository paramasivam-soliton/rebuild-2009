// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.Automaton.SelectFirmwareFileCommand
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using PathMedical.Automaton.Command;
using PathMedical.InstrumentManagement;
using PathMedical.Type1077;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.Automaton;

internal class SelectFirmwareFileCommand : CommandBase
{
  private Type1077Manager Model { get; set; }

  public SelectFirmwareFileCommand(Type1077Manager model)
  {
    this.Name = nameof (SelectFirmwareFileCommand);
    this.Model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null)
      return;
    OpenFileDialog openFileDialog1 = new OpenFileDialog();
    openFileDialog1.Title = "Select Firmware File";
    openFileDialog1.Multiselect = false;
    openFileDialog1.DefaultExt = "xml";
    openFileDialog1.Filter = "xml files (*.xml)|*.xml";
    openFileDialog1.RestoreDirectory = true;
    OpenFileDialog openFileDialog2 = openFileDialog1;
    if (openFileDialog2.ShowDialog() != DialogResult.OK)
      return;
    this.Model.selectedFirmwareFile((IInstrument) (this.TriggerEventArgs.TriggerContext as SelectFirmwareFileTriggerContext).Instrument, openFileDialog2.FileName);
  }
}
