// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Map.LiteralMapItem
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;
using System.Diagnostics;

#nullable disable
namespace PathMedical.DataExchange.Map;

[DebuggerDisplay("Literal: {Literal} ToColumn: {ToColumnName}")]
public class LiteralMapItem : IColumnMapItem
{
  public object Literal { get; protected set; }

  public string ToColumnName { get; protected set; }

  public bool IsMandatory { get; set; }

  public LiteralMapItem(object literal, string columnName)
  {
    this.ToColumnName = !string.IsNullOrEmpty(columnName) ? columnName : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (columnName));
    this.Literal = literal;
  }
}
