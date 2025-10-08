// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Tokens.DataExchangeTokenSet
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.Exception;
using PathMedical.Property;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PathMedical.DataExchange.Tokens;

[DebuggerDisplay("{RecordDescription.Identifier} [{RecordDescription.Description}]")]
public class DataExchangeTokenSet
{
  public RecordDescription RecordDescription { get; set; }

  public List<ISupportDataExchangeToken> Tokens { get; protected set; }

  public DataExchangeTokenSet(RecordDescription recordDescription)
  {
    this.RecordDescription = recordDescription != null ? recordDescription : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordDescription));
    this.Tokens = new List<ISupportDataExchangeToken>();
  }

  public static DataExchangeTokenSet Create(RecordDescription recordDescription, object data)
  {
    DataExchangeTokenSet exchangeTokenSet = recordDescription != null ? new DataExchangeTokenSet(recordDescription) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordDescription));
    foreach (ISupportDataExchangeColumnDescription column in recordDescription.Columns)
    {
      DataExchangeTokenBase token = new DataExchangeTokenBase()
      {
        UniqueIdentifier = column.UniqueFieldIdentifier
      };
      token.Data = PropertyHelper<DataExchangeColumnAttribute>.GetPropertyValue(token.UniqueIdentifier, data);
      exchangeTokenSet.Add((ISupportDataExchangeToken) token);
    }
    return exchangeTokenSet;
  }

  public void Add(ISupportDataExchangeToken token)
  {
    if (token == null)
      return;
    this.Tokens.Add(token);
  }

  public IEnumerator GetEnumerator()
  {
    IEnumerator enumerator = (IEnumerator) null;
    if (this.Tokens != null)
      enumerator = (IEnumerator) this.Tokens.GetEnumerator();
    return enumerator;
  }

  public ISupportDataExchangeToken this[string tokenName]
  {
    get
    {
      ISupportDataExchangeToken dataExchangeToken = (ISupportDataExchangeToken) null;
      if (this.Tokens != null)
        dataExchangeToken = this.Tokens.FirstOrDefault<ISupportDataExchangeToken>((Func<ISupportDataExchangeToken, bool>) (t => string.Compare(t.UniqueIdentifier, tokenName) == 0));
      return dataExchangeToken;
    }
  }

  public bool SupportsType<T>()
  {
    Type type = typeof (T);
    DataExchangeRecordAttribute exchangeRecordAttribute = type.GetCustomAttributes(typeof (DataExchangeRecordAttribute), true).Cast<DataExchangeRecordAttribute>().FirstOrDefault<DataExchangeRecordAttribute>();
    string strB = type.Name;
    if (exchangeRecordAttribute != null && exchangeRecordAttribute.Identifier is string)
      strB = exchangeRecordAttribute.Identifier as string;
    return string.Compare(this.RecordDescription.Identifier, strB) == 0;
  }
}
