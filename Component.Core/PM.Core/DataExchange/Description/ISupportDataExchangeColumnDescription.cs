// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Description.ISupportDataExchangeColumnDescription
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

#nullable disable
namespace PathMedical.DataExchange.Description;

public interface ISupportDataExchangeColumnDescription
{
  string UniqueFieldIdentifier { get; }

  string Description { get; set; }
}
