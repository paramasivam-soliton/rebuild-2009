// Decompiled with JetBrains decompiler
// Type: PathMedical.ResourceManager.CustomResourceSet
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections;
using System.Resources;

#nullable disable
namespace PathMedical.ResourceManager;

public class CustomResourceSet(IResourceReader resourceReader) : ResourceSet(resourceReader)
{
  public Hashtable Table => this.Table;

  public virtual IResourceReader CreateDefaultReader()
  {
    return (IResourceReader) Activator.CreateInstance(this.GetDefaultReader());
  }

  public virtual IResourceWriter CreateDefaultWriter()
  {
    return (IResourceWriter) Activator.CreateInstance(this.GetDefaultWriter());
  }

  public virtual void Add(string key, object value) => this.Table.Add((object) key, value);
}
