// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Stream.IDataExchangeStreamReader
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Tokens;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.DataExchange.Stream;

public interface IDataExchangeStreamReader
{
  void Open();

  void Close();

  List<DataExchangeTokenSet> Read();
}
