// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ResourceManager.ResourcesResourceReader
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.Exception;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.ResourceManager;

public class ResourcesResourceReader : IResourceReader, IEnumerable, IDisposable
{
  private string baseNameField;
  private CultureInfo cultureInfo;
  private string extension;

  public ResourcesResourceReader(string baseNameField, CultureInfo cultureInfo, string extension)
  {
    this.baseNameField = baseNameField;
    this.cultureInfo = cultureInfo;
    this.extension = extension;
  }

  protected virtual string GetResourceFileName()
  {
    StringBuilder stringBuilder = new StringBuilder(this.baseNameField);
    stringBuilder.Append(".");
    if (!this.cultureInfo.Equals((object) CultureInfo.InvariantCulture))
    {
      stringBuilder.Append(this.cultureInfo.Name);
      stringBuilder.Append(".");
    }
    stringBuilder.Append(this.extension);
    return stringBuilder.ToString();
  }

  protected virtual IResourceReader GetResourceReader(string fileName)
  {
    if (this.extension.Equals("resx", StringComparison.InvariantCultureIgnoreCase))
      return !string.IsNullOrEmpty(fileName) ? (IResourceReader) new ResXResourceReader(fileName) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, nameof (fileName)));
    if (this.extension == "resources")
      return (IResourceReader) new ResourceReader(this.GetResourceFileName());
    throw ExceptionFactory.Instance.CreateException<ArgumentException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Unknown resource extension ({0})", (object) this.extension));
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public IDictionaryEnumerator GetEnumerator()
  {
    Hashtable hashtable = new Hashtable();
    string resourceFileName = this.GetResourceFileName();
    if (File.Exists(resourceFileName))
    {
      IResourceReader resourceReader = this.GetResourceReader(resourceFileName);
      try
      {
        IDictionaryEnumerator enumerator = resourceReader.GetEnumerator();
        while (enumerator.MoveNext())
          hashtable.Add(enumerator.Key, enumerator.Value);
      }
      finally
      {
        resourceReader.Close();
      }
    }
    return hashtable.GetEnumerator();
  }

  public void Close()
  {
  }

  public void Dispose()
  {
  }
}
