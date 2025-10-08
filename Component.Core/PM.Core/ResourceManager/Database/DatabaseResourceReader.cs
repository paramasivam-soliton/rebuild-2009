// Decompiled with JetBrains decompiler
// Type: PathMedical.ResourceManager.Database.DatabaseResourceReader
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections;
using System.Globalization;
using System.Resources;

#nullable disable
namespace PathMedical.ResourceManager.Database;

public class DatabaseResourceReader : IResourceReader, IEnumerable, IDisposable
{
  private readonly string baseNameField;
  private readonly CultureInfo cultureInfo;
  private readonly ResourcesAdapter resourceAdapter;

  public DatabaseResourceReader(string baseNameField, CultureInfo cultureInfo)
  {
    this.baseNameField = baseNameField;
    this.cultureInfo = cultureInfo;
    this.resourceAdapter = new ResourcesAdapter();
  }

  public IDictionaryEnumerator GetEnumerator()
  {
    return this.resourceAdapter.Get(this.cultureInfo, this.baseNameField).GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public void Close()
  {
  }

  public void Dispose()
  {
  }
}
