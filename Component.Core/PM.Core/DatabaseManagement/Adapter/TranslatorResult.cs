// Decompiled with JetBrains decompiler
// Type: PathMedical.DatabaseManagement.Adapter.TranslatorResult
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Collections.Generic;

#nullable disable
namespace PathMedical.DatabaseManagement.Adapter;

public class TranslatorResult
{
  internal TranslatorResult(string whereClause, Dictionary<string, object> parameters)
  {
    this.WhereClause = whereClause;
    this.Parameters = parameters;
  }

  public string WhereClause { get; private set; }

  public Dictionary<string, object> Parameters { get; private set; }
}
