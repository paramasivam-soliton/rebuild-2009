// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Description.DataExchangeColumnAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.DataExchange.Description;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class DataExchangeColumnAttribute : Attribute
{
  public object Identifier { get; set; }

  public bool IsReadOnly { get; set; }

  public bool ValueLoaded { get; set; }

  public DataExchangeColumnAttribute()
  {
    this.Identifier = (object) null;
    this.IsReadOnly = false;
    this.ValueLoaded = false;
  }

  public DataExchangeColumnAttribute(object identifier)
    : this()
  {
    this.Identifier = identifier;
  }
}
