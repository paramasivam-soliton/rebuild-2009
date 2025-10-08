// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Data.WaveFormDataManager
// Assembly: PM.PatientManagement.MultipleTestViewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2267BB5-2AA5-4F30-972C-4D9F0F378D06
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.MultipleTestViewer.WindowsForms.dll

using PathMedical.ABR;
using PathMedical.DPOAE;
using PathMedical.TEOAE;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Data;

public class WaveFormDataManager : IModel
{
  public Dictionary<TeoaeTestInformation, string> TeoaeTests;
  public Dictionary<DpoaeTestInformation, string> DpoaeTests;
  public Dictionary<AbrTestInformation, string> AbrTests;
  private bool exception;

  public static WaveFormDataManager Instance => PathMedical.Singleton.Singleton<WaveFormDataManager>.Instance;

  private WaveFormDataManager() => this.exception = false;

  public void StartDataImport()
  {
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void RefreshData()
  {
  }

  internal void StartDataDownloading(string[] files)
  {
    List<string> stringList = new List<string>();
    int changedObject = 0;
    if (files == null)
      return;
    Cursor.Current = Cursors.WaitCursor;
    this.TeoaeTests = new Dictionary<TeoaeTestInformation, string>();
    this.AbrTests = new Dictionary<AbrTestInformation, string>();
    this.DpoaeTests = new Dictionary<DpoaeTestInformation, string>();
    foreach (string file in files)
    {
      FileInfo fileInfo = new FileInfo(file);
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      string s = File.ReadAllText(fileInfo.FullName);
      try
      {
        MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(s));
        object key = new object();
        try
        {
          key = binaryFormatter.Deserialize((Stream) serializationStream);
        }
        catch (SerializationException ex)
        {
          this.exception = true;
          stringList.Add(fileInfo.Name);
        }
        string name = fileInfo.Name;
        serializationStream.Close();
        if (key.GetType() == typeof (TeoaeTestInformation))
        {
          this.TeoaeTests.Add((TeoaeTestInformation) key, name);
          ++changedObject;
        }
        else if (key.GetType() == typeof (DpoaeTestInformation))
        {
          this.DpoaeTests.Add((DpoaeTestInformation) key, name);
          ++changedObject;
        }
        else if (key.GetType() == typeof (AbrTestInformation))
        {
          this.AbrTests.Add((AbrTestInformation) key, name);
          ++changedObject;
        }
      }
      catch (FormatException ex)
      {
        this.exception = true;
        stringList.Add(fileInfo.Name);
      }
    }
    if (this.Changed != null)
    {
      this.Changed((object) this, ModelChangedEventArgs.Create<int>(changedObject, ChangeType.ListLoaded));
      if (this.TeoaeTests.Count<KeyValuePair<TeoaeTestInformation, string>>() > 0)
        this.Changed((object) this, ModelChangedEventArgs.Create<Dictionary<TeoaeTestInformation, string>>(this.TeoaeTests, ChangeType.ListLoaded));
      if (this.AbrTests.Count<KeyValuePair<AbrTestInformation, string>>() > 0)
        this.Changed((object) this, ModelChangedEventArgs.Create<Dictionary<AbrTestInformation, string>>(this.AbrTests, ChangeType.ListLoaded));
      if (this.DpoaeTests.Count<KeyValuePair<DpoaeTestInformation, string>>() > 0)
        this.Changed((object) this, ModelChangedEventArgs.Create<Dictionary<DpoaeTestInformation, string>>(this.DpoaeTests, ChangeType.ListLoaded));
      if (stringList.Count<string>() > 0)
        this.Changed((object) this, ModelChangedEventArgs.Create<List<string>>(stringList, ChangeType.ListLoaded));
    }
    else
      this.Changed((object) this, ModelChangedEventArgs.Create<int>(((IEnumerable<string>) files).Count<string>(), ChangeType.ListLoaded));
    Cursor.Current = Cursors.Default;
  }
}
