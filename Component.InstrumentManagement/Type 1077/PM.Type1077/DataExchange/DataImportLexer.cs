// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.DataExchange.DataImportLexer
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Logging;
using PathMedical.SystemConfiguration;
using PathMedical.Type1077.DataExchange.Column;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;

#nullable disable
namespace PathMedical.Type1077.DataExchange;

internal class DataImportLexer
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (DataImportLexer), "$Rev: 1091 $");
  private const int MaximumRawDataElementSize = 512 /*0x0200*/;

  public static DataImportLexer Instance => PathMedical.Singleton.Singleton<DataImportLexer>.Instance;

  public RecordDescriptionSet RecordDescriptionSet { get; set; }

  private DataImportLexer()
  {
    this.RecordDescriptionSet = DataExchangeManager.Instance.GetRecordDescriptionSet("Type1077");
  }

  public List<DataExchangeTokenSet> Parse(Stream stream)
  {
    DateTime now = DateTime.Now;
    DataImportLexer.Logger.Debug("Starting to parse raw data.");
    if (stream == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (stream));
    if (this.RecordDescriptionSet == null)
      throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("RecordDescriptionSet is undefined");
    List<InstrumentToken> instrumentTokenList = DataImportLexer.Tokenize(stream);
    IEnumerable<IEnumerable<InstrumentToken>> tokenSets = DataImportLexer.CreateTokenSets((IEnumerable<InstrumentToken>) instrumentTokenList.ToArray());
    List<DataExchangeTokenSet> exchangeTokenSets = this.CreateDataExchangeTokenSets(DataImportLexer.ConsolidateTokenSets(tokenSets));
    TimeSpan timeSpan = DateTime.Now.Subtract(now);
    DataImportLexer.Logger.Info("Raw data has been parsed. [{0}] tokens / [{1}] sets / [{2}] consolidated sets / processing time [{3}] seconds.", (object) instrumentTokenList.Count, (object) tokenSets.Count<IEnumerable<InstrumentToken>>(), (object) exchangeTokenSets.Count<DataExchangeTokenSet>(), (object) timeSpan.TotalSeconds);
    return exchangeTokenSets;
  }

  private static List<InstrumentToken> Tokenize(Stream stream)
  {
    if (stream == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (stream));
    List<byte> byteList1 = new List<byte>();
    using (BinaryReader binaryReader = new BinaryReader(stream))
    {
      long length = binaryReader.BaseStream.Length;
      for (int index = 0; (long) index < length; ++index)
        byteList1.Add(binaryReader.ReadByte());
    }
    string path = string.Empty;
    try
    {
      string instrumentDataDirectory = SystemConfigurationManager.Instance.TemporaryInstrumentDataDirectory;
      path = Path.Combine(instrumentDataDirectory, string.Format("Received-{0:yyyy}{0:MM}{0:dd}-{0:HH}{0:mm}{0:ss}-{0:fff}.{1}", (object) DateTime.Now, (object) "dump"));
      if (!Directory.Exists(instrumentDataDirectory))
        Directory.CreateDirectory(instrumentDataDirectory);
      new FileIOPermission(FileIOPermissionAccess.AllAccess, path).Demand();
      using (BinaryWriter binaryWriter = new BinaryWriter((Stream) new FileStream(path, FileMode.CreateNew)))
        binaryWriter.Write(byteList1.ToArray());
    }
    catch (SecurityException ex)
    {
      DataImportLexer.Logger.Error((System.Exception) ex, "The access to file {0} has been denied.", (object) path);
    }
    catch (DirectoryNotFoundException ex)
    {
      DataImportLexer.Logger.Error((System.Exception) ex, "The direcotry for file file {0} doesn't exist.", (object) path);
    }
    catch (System.Exception ex)
    {
      DataImportLexer.Logger.Error(ex, "The dump for instrument data can't be created.");
    }
    MemoryStream input = new MemoryStream(byteList1.ToArray());
    List<InstrumentToken> instrumentTokenList = new List<InstrumentToken>();
    using (BinaryReader binaryReader = new BinaryReader((Stream) input))
    {
      long length = binaryReader.BaseStream.Length;
      List<byte> byteList2 = new List<byte>();
      List<byte> byteList3 = new List<byte>();
      long num1 = 0;
      while (num1 + 4L < length)
      {
        byte[] collection1 = binaryReader.ReadBytes(4);
        byteList3.AddRange((IEnumerable<byte>) collection1);
        long num2 = num1 + 4L;
        ushort uint16_1 = DataConverter.ToUInt16(byteList3.GetRange(0, 2).ToArray(), 0);
        ushort uint16_2 = DataConverter.ToUInt16(byteList3.GetRange(2, 2).ToArray(), 0);
        byteList2.AddRange((IEnumerable<byte>) byteList3.ToArray());
        if (num2 + (long) uint16_2 <= length && uint16_1 > (ushort) 0 && uint16_2 <= (ushort) 512 /*0x0200*/)
        {
          byte[] collection2 = binaryReader.ReadBytes((int) uint16_2);
          byteList2.AddRange((IEnumerable<byte>) collection2);
          byteList3.AddRange((IEnumerable<byte>) collection2);
          num1 = num2 + (long) uint16_2;
          InstrumentToken instrumentToken = new InstrumentToken()
          {
            UniqueFieldIdentifier = Convert.ToString(uint16_1),
            Length = (int) uint16_2,
            Data = collection2,
            RawData = byteList3.ToArray()
          };
          instrumentTokenList.Add(instrumentToken);
          DataImportLexer.Logger.Debug("Created token with identifier {0} [{1}] Length {2} Data {3}", (object) instrumentToken.UniqueFieldIdentifier, (object) Convert.ToUInt16(instrumentToken.UniqueFieldIdentifier).ToHex(), (object) instrumentToken.Length, (object) instrumentToken.Data.ToHex());
        }
        else
        {
          DataImportLexer.Logger.Info("Invalid flash data. Stopped reading at position {0} of {1} while processing token {2} containing {3} bytes.", (object) num2, (object) length, (object) uint16_1, (object) uint16_2);
          long count = length - num2;
          byteList2.AddRange((IEnumerable<byte>) binaryReader.ReadBytes((int) count));
          num1 = length;
          instrumentTokenList.Clear();
        }
        byteList3.Clear();
      }
    }
    return instrumentTokenList;
  }

  private static IEnumerable<IEnumerable<InstrumentToken>> CreateTokenSets(
    IEnumerable<InstrumentToken> tokens)
  {
    IEnumerable<InstrumentToken> instrumentTokens = tokens.SkipWhile<InstrumentToken>((Func<InstrumentToken, bool>) (ic => string.Compare(ic.UniqueFieldIdentifier, UniqueFieldIdentifiers.Sync.Identifier) != 0));
    List<IEnumerable<InstrumentToken>> tokenSets = new List<IEnumerable<InstrumentToken>>();
    List<InstrumentToken> instrumentTokenList = new List<InstrumentToken>();
    foreach (InstrumentToken instrumentToken in instrumentTokens)
    {
      if (string.Compare(instrumentToken.UniqueFieldIdentifier, UniqueFieldIdentifiers.Sync.Identifier) == 0)
      {
        instrumentTokenList = new List<InstrumentToken>();
        tokenSets.Add((IEnumerable<InstrumentToken>) instrumentTokenList);
      }
      if (DataImportLexer.Logger.IsDebugEnabled && string.Compare(instrumentToken.UniqueFieldIdentifier, UniqueFieldIdentifiers.RecordIdentifier.Identifier) == 0 && instrumentTokenList.Count == 1)
        DataImportLexer.Logger.Debug("Detected token set with identifier {0} [{1}]", (object) instrumentToken.UniqueFieldIdentifier, (object) Convert.ToUInt16(instrumentToken.UniqueFieldIdentifier).ToHex());
      instrumentTokenList.Add(instrumentToken);
    }
    DataImportLexer.Logger.Debug("Detected {0} token set(s)", (object) tokenSets.Count);
    return (IEnumerable<IEnumerable<InstrumentToken>>) tokenSets;
  }

  private static IEnumerable<IEnumerable<InstrumentToken>> ConsolidateTokenSets(
    IEnumerable<IEnumerable<InstrumentToken>> tokenSets)
  {
    List<IEnumerable<InstrumentToken>> source = new List<IEnumerable<InstrumentToken>>();
    foreach (IEnumerable<InstrumentToken> tokenSet in tokenSets)
    {
      InstrumentToken instrumentToken = tokenSet.FirstOrDefault<InstrumentToken>((Func<InstrumentToken, bool>) (token => string.Compare(token.UniqueFieldIdentifier, UniqueFieldIdentifiers.IsObsolete.Identifier) == 0));
      if (instrumentToken != null && DataConverter.ToUInt16(instrumentToken.Data, 0) == ushort.MaxValue)
        source.Add(tokenSet);
      else if (instrumentToken == null)
        source.Add(tokenSet);
    }
    int num = tokenSets.Count<IEnumerable<InstrumentToken>>() - source.Count<IEnumerable<InstrumentToken>>();
    DataImportLexer.Logger.Debug("{0} token sets out of {1} token sets are obsolete and will not be processed.", (object) num, (object) tokenSets.Count<IEnumerable<InstrumentToken>>());
    return (IEnumerable<IEnumerable<InstrumentToken>>) source;
  }

  private List<DataExchangeTokenSet> CreateDataExchangeTokenSets(
    IEnumerable<IEnumerable<InstrumentToken>> tokenSets)
  {
    List<DataExchangeTokenSet> exchangeTokenSets = new List<DataExchangeTokenSet>();
    foreach (IEnumerable<InstrumentToken> tokenSet in tokenSets)
    {
      ushort identifier = tokenSet.Where<InstrumentToken>((Func<InstrumentToken, bool>) (token => string.Compare(token.UniqueFieldIdentifier, UniqueFieldIdentifiers.RecordIdentifier.Identifier) == 0)).Select(token => new
      {
        Identifier = DataConverter.ToUInt16(token.Data, 0)
      }).FirstOrDefault().Identifier;
      try
      {
        RecordDescription recordDescription = this.RecordDescriptionSet[Convert.ToString(identifier)];
        if (recordDescription != null)
        {
          DataExchangeTokenSet exchangeTokenSet = new DataExchangeTokenSet(recordDescription);
          foreach (InstrumentToken instrumentToken in tokenSet)
          {
            if (!string.IsNullOrEmpty(instrumentToken.UniqueFieldIdentifier))
            {
              ISupportDataExchangeColumnDescription descriptionByIdentifier = exchangeTokenSet.RecordDescription.GetColumnDescriptionByIdentifier(instrumentToken.UniqueFieldIdentifier);
              if (descriptionByIdentifier != null)
              {
                DataExchangeTokenBase token = new DataExchangeTokenBase()
                {
                  UniqueIdentifier = instrumentToken.UniqueFieldIdentifier,
                  ColumnDescription = descriptionByIdentifier
                };
                if (descriptionByIdentifier is ColumnDescriptionBase)
                {
                  ColumnDescriptionBase columnDescriptionBase = descriptionByIdentifier as ColumnDescriptionBase;
                  try
                  {
                    object obj = DataConverter.GetValue(columnDescriptionBase.DataTypes, instrumentToken.Data);
                    token.Data = DataExchangeDataType.CreateInstance(columnDescriptionBase.DataTypes, obj);
                    DataImportLexer.Logger.Debug("Created data exchange token [{0}] [{1}] of type {2} containing [{3}] [LSB:{4}]", (object) token.UniqueIdentifier, (object) Convert.ToUInt16(token.UniqueIdentifier).ToHex(), (object) columnDescriptionBase.DataTypes, token.Data != null ? (object) Convert.ToString(token.Data) : (object) "<No Data>", token.Data != null ? (object) instrumentToken.Data.ToHex() : (object) "<No Data>");
                  }
                  catch (DataConverterException ex)
                  {
                    DataImportLexer.Logger.Error((System.Exception) ex, "Failure while converting data exchange token [{0}]. Data [{1}] Type [{2}]", (object) instrumentToken.UniqueFieldIdentifier, (object) instrumentToken.Data.ToHex(), (object) columnDescriptionBase.DataTypes);
                  }
                }
                else
                  DataImportLexer.Logger.Debug("Created data exchange token [{0}] [{1}]", (object) token.UniqueIdentifier, (object) token.UniqueIdentifier.ToHex(true));
                exchangeTokenSet.Add((ISupportDataExchangeToken) token);
              }
              else
                DataImportLexer.Logger.Error("Token [{0}] [{1}] can't be analyzed since it is not included in record description [{2}] [{3}]. Unprocessed data: [{4}]", (object) instrumentToken.UniqueFieldIdentifier, (object) Convert.ToUInt16(instrumentToken.UniqueFieldIdentifier).ToHex(), (object) exchangeTokenSet.RecordDescription.Identifier, (object) Convert.ToUInt16(exchangeTokenSet.RecordDescription.Identifier).ToHex(), instrumentToken.Data.Length != 0 ? (object) instrumentToken.Data.ToHex() : (object) "<No Data>");
            }
          }
          exchangeTokenSets.Add(exchangeTokenSet);
        }
        else
          DataImportLexer.Logger.Error("No record description for [{0}] [{1}] available. Skipping record.", (object) identifier, (object) Convert.ToUInt16(identifier).ToHex());
      }
      catch (ArgumentNullException ex)
      {
        DataImportLexer.Logger.Info("Couldn't find a record description for record identifier [{0}] [{1}].", (object) identifier, (object) Convert.ToUInt16(identifier).ToHex());
      }
    }
    return exchangeTokenSets;
  }
}
