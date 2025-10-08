// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Map.IndexerRecord
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;

#nullable disable
namespace PathMedical.DataExchange.Map;

public class IndexerRecord
{
  public object Parent { get; set; }

  public object Child { get; set; }

  public ISupportDataExchangeColumnDescription ChildDescription { get; set; }
}
