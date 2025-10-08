// Decompiled with JetBrains decompiler
// Type: PathMedical.ResourceManager.Database.DatabaseResourceWriter
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;
using System.Collections;
using System.Globalization;
using System.Resources;

#nullable disable
namespace PathMedical.ResourceManager.Database;

public class DatabaseResourceWriter : IResourceWriter, IDisposable
{
  private readonly SortedList resourceList;

  public DatabaseResourceWriter() => this.resourceList = new SortedList();

  public void Close() => this.Dispose(true);

  public void Dispose() => this.Dispose(true);

  private void Dispose(bool disposing)
  {
    if (!disposing)
      return;
    SortedList resourceList = this.resourceList;
  }

  public void AddResource(string name, object value)
  {
    if (name == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, nameof (name)));
    if (this.resourceList == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "No resource list available."));
    this.resourceList.Add((object) name, value);
  }

  public void AddResource(string name, string value) => this.AddResource(name, (object) value);

  public void AddResource(string name, byte[] value) => this.AddResource(name, (object) value);

  public void Generate()
  {
  }
}
