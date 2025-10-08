// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.WaveFileImportTriggers
// Assembly: PM.PatientManagement.MultipleTestViewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2267BB5-2AA5-4F30-972C-4D9F0F378D06
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.MultipleTestViewer.WindowsForms.dll

using PathMedical.Automaton;

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms;

public abstract class WaveFileImportTriggers
{
  public static readonly Trigger StartDataImport = new Trigger(nameof (StartDataImport));
  public static readonly Trigger LoadTestDataFromFile = new Trigger(nameof (LoadTestDataFromFile));
}
