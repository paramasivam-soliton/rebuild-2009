// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.DataExportLexer
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.Type1077.DataExchange.Column;
using PathMedical.Type1077.Properties;
using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PathMedical.Type1077.DataExchange;

[CLSCompliant(false)]
public class DataExportLexer
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (DataExportLexer), "$Rev: 1403 $");

  public static DataExportLexer Instance => PathMedical.Singleton.Singleton<DataExportLexer>.Instance;

  public RecordDescriptionSet RecordDescriptionSet { get; set; }

  private DataExportLexer()
  {
    this.RecordDescriptionSet = RecordDescriptionSet.LoadXmlFile(XmlHelper.GetResourceEmbeddedDocument(Assembly.GetExecutingAssembly(), "PathMedical.Type1077.DataExchange.Type1077DataExchangeSet.xml") ?? throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.DataExportLexer_StructureForLexerNotFound));
  }

  public List<InstrumentToken> Parse(List<DataExchangeTokenSet> dataExchangeTokenSets)
  {
    if (dataExchangeTokenSets == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (dataExchangeTokenSets));
    if (this.RecordDescriptionSet == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>(Resources.DataExportLexer_RecordDescriptionUndefined);
    this.CreateTokenSets(dataExchangeTokenSets.ToArray());
    return (List<InstrumentToken>) null;
  }

  public List<InstrumentToken> CreateTokenSets(DataExchangeTokenSet[] tokenSets)
  {
    List<InstrumentToken> tokenSets1 = new List<InstrumentToken>();
    foreach (DataExchangeTokenSet tokenSet in tokenSets)
    {
      foreach (ISupportDataExchangeToken dataExchangeToken in tokenSet)
      {
        if (!string.IsNullOrEmpty(dataExchangeToken.UniqueIdentifier))
          tokenSet.RecordDescription.GetColumnDescriptionByIdentifier(dataExchangeToken.UniqueIdentifier);
      }
    }
    return tokenSets1;
  }
}
