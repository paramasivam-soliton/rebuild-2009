// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Description.DataExchangeRecordAttribute
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;

#nullable disable
namespace PathMedical.DataExchange.Description;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DataExchangeRecordAttribute : Attribute
{
  public object Identifier { get; set; }

  public DataExchangeRecordAttribute()
  {
  }

  public DataExchangeRecordAttribute(object identifier) => this.Identifier = identifier;
}
