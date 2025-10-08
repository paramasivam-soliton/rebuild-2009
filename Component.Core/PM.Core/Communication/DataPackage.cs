// Decompiled with JetBrains decompiler
// Type: PathMedical.Communication.DataPackage`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Collections.Generic;

#nullable disable
namespace PathMedical.Communication;

public class DataPackage<T>
{
  protected List<T> Items;

  public virtual List<T> Elements
  {
    get
    {
      List<T> elements = new List<T>();
      lock (this.Items)
      {
        foreach (T obj in this.Items)
          elements.Add(obj);
      }
      return elements;
    }
  }

  public virtual void Add(T item)
  {
    if (this.Items == null)
      this.Items = new List<T>();
    this.Items.Add(item);
  }

  public virtual void Add(T[] items)
  {
    if (this.Items == null)
      this.Items = new List<T>();
    this.Items.AddRange((IEnumerable<T>) items);
  }

  public virtual void Clear()
  {
    if (this.Items == null)
      return;
    this.Items.Clear();
  }
}
