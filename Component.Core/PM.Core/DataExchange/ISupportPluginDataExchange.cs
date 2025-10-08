// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.ISupportPluginDataExchange
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Plugin;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.DataExchange;

public interface ISupportPluginDataExchange : IPlugin
{
  List<RecordDescriptionSet> RecordDescriptionSets { get; }

  List<DataExchangeSetMap> RecordSetMaps { get; }

  List<Type> Types { get; }

  void Import(
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets);

  object Export(ExportType exportType, Guid testDetailId);

  int StorePriority { get; }
}
