// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Stream.FlatFileStreamWriter
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Properties;
using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;

#nullable disable
namespace PathMedical.DataExchange.Stream;

public class FlatFileStreamWriter : IDataExchangeStreamWriter, IDisposable
{
  private readonly bool fileCreated;
  private bool instanceDisposed;

  public RecordDescription RecordDescription { get; protected set; }

  public Encoding Encoding { get; set; }

  public string DecimalPoint { get; set; }

  public bool UnquoteQuotationMark { get; set; }

  public string QuotationMark { get; set; }

  public string UnquoteMark { get; set; }

  public string Separator { get; set; }

  public string EndOfFileMarker { get; set; }

  public bool IsFixedLength { get; set; }

  public string LineFeed { get; set; }

  public string FileName { get; protected set; }

  public bool Append { get; protected set; }

  public bool CreateColumnHeaderRow { get; set; }

  protected StreamWriter Writer { get; set; }

  private FlatFileStreamWriter()
  {
    this.fileCreated = false;
    this.FileName = string.Empty;
    this.Append = false;
    this.CreateColumnHeaderRow = true;
    this.DecimalPoint = ".";
    this.Separator = ",";
    this.QuotationMark = "\"";
    this.UnquoteMark = "\\";
    this.UnquoteQuotationMark = true;
    this.Encoding = Encoding.GetEncoding(850);
    this.LineFeed = Environment.NewLine;
  }

  public FlatFileStreamWriter(RecordDescription recordDescription, System.IO.Stream stream)
    : this()
  {
    if (recordDescription == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordDescription));
    if (stream == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (stream));
    if (stream is FileStream && this.Append && !File.Exists((stream as FileStream).Name))
      this.fileCreated = true;
    try
    {
      this.Writer = new StreamWriter(stream);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(Resources.FlatFileStreamWriter_FileIsWriteProtected, (System.Exception) ex);
    }
  }

  public FlatFileStreamWriter(RecordDescription recordDescription, string fileName, bool append)
    : this()
  {
    if (string.IsNullOrEmpty(fileName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (fileName));
    if (recordDescription == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordDescription));
    this.FileName = fileName;
    this.RecordDescription = recordDescription;
    this.Append = append;
    if (!File.Exists(fileName) && this.Append)
      this.fileCreated = true;
    try
    {
      if (this.Encoding == null)
        this.Encoding = Encoding.Default;
      this.Writer = new StreamWriter(this.FileName, this.Append, this.Encoding)
      {
        AutoFlush = true
      };
    }
    catch (UnauthorizedAccessException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_FileAccessDenied, (object) this.FileName), (System.Exception) ex);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_FileOpenExceptionSystemDeviceName, (object) this.FileName), (System.Exception) ex);
    }
    catch (DirectoryNotFoundException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_FileOpenException_InvalidPath, (object) this.FileName), (System.Exception) ex);
    }
    catch (PathTooLongException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>($"Failure while opening the file {this.FileName}. The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.", (System.Exception) ex);
    }
    catch (IOException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_FileOpenException_FilenameHasInvalidSyntax, (object) this.FileName), (System.Exception) ex);
    }
    catch (SecurityException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_FileOpenException_AccessDenied, (object) this.FileName), (System.Exception) ex);
    }
  }

  public void Open()
  {
    if (this.Writer != null && this.Writer.BaseStream is FileStream)
    {
      FileStream baseStream = this.Writer.BaseStream as FileStream;
      FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, baseStream.Name);
      try
      {
        fileIoPermission.Demand();
      }
      catch (SecurityException ex)
      {
        throw ExceptionFactory.Instance.CreateException<DataExchangeSetException>(string.Format(Resources.FlatFileStreamWriter_FileOpenException_AccessDenied, (object) baseStream.Name), (System.Exception) ex);
      }
    }
    if (!this.fileCreated || !this.CreateColumnHeaderRow)
      return;
    string str = string.Empty;
    foreach (ISupportDataExchangeColumnDescription column in this.RecordDescription.Columns)
    {
      this.Write(string.Format("{1}{0}", (object) column.UniqueFieldIdentifier, (object) str));
      str = this.Separator;
    }
    this.Write($"{this.LineFeed}");
  }

  public void Close()
  {
    if (this.Writer == null)
      return;
    if (!string.IsNullOrEmpty(this.EndOfFileMarker))
      this.Write($"{this.EndOfFileMarker}");
    try
    {
      this.Writer.Flush();
      this.Writer.Close();
    }
    catch (ObjectDisposedException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_FileCloseException_WriterAlreadyClosed, (object) this.FileName), (System.Exception) ex);
    }
    catch (IOException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_FileCloseException_IoFailure, (object) this.FileName), (System.Exception) ex);
    }
    catch (EncoderFallbackException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_FileCloseException_UnicodeSurrogate, (object) this.FileName), (System.Exception) ex);
    }
  }

  public void Write(RecordDescription recordDescription, DataExchangeTokenSet dataExchangeTokenSet)
  {
    if (!this.ContainsRecordDescription(recordDescription))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordDescription));
    StringBuilder stringBuilder = new StringBuilder();
    string empty = string.Empty;
    foreach (ISupportDataExchangeColumnDescription column in recordDescription.Columns)
      ;
    this.Write(stringBuilder.ToString());
    this.Write($"{this.LineFeed}");
  }

  public bool ContainsRecordDescription(RecordDescription recordDescritpion)
  {
    bool flag = false;
    if (recordDescritpion != null && this.RecordDescription != null && this.RecordDescription.Identifier.Equals(recordDescritpion.Identifier))
      flag = true;
    return flag;
  }

  private void Write(string data)
  {
    try
    {
      if (string.IsNullOrEmpty(data))
        return;
      this.Writer.Write(data);
    }
    catch (ObjectDisposedException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_WriteException_FlushOrBufferFull, (object) this.FileName), (System.Exception) ex);
    }
    catch (NotSupportedException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_WriteException_EndOfStream, (object) this.FileName), (System.Exception) ex);
    }
    catch (IOException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeWriterException>(string.Format(Resources.FlatFileStreamWriter_WriteException_IoFailure, (object) this.FileName), (System.Exception) ex);
    }
  }

  private object FormatColumn(
    ISupportDataExchangeColumnDescription supportDataExchangeColumnDescription,
    object columnValue)
  {
    StringBuilder stringBuilder = new StringBuilder();
    switch (supportDataExchangeColumnDescription)
    {
      case TextColumnDescription _:
        string str1 = columnValue as string;
        if (!string.IsNullOrEmpty(str1))
        {
          if (this.UnquoteQuotationMark && !string.IsNullOrEmpty(this.UnquoteMark) && !string.IsNullOrEmpty(this.QuotationMark))
            str1.Replace(this.QuotationMark, this.UnquoteMark + this.QuotationMark);
          stringBuilder.AppendFormat("{0}{1}{0}", (object) this.QuotationMark, (object) str1);
          break;
        }
        stringBuilder.AppendFormat("{0}", (object) string.Empty);
        break;
      case NumericColumnDescription _:
        if ((supportDataExchangeColumnDescription as NumericColumnDescription).DataTypes == DataTypes.Float)
        {
          string str2 = columnValue as string;
          if (!string.IsNullOrEmpty(str2))
          {
            columnValue = (object) str2.Replace(",", this.DecimalPoint);
            goto default;
          }
          goto default;
        }
        goto default;
      default:
        stringBuilder.AppendFormat("{0}", columnValue);
        break;
    }
    return (object) stringBuilder.ToString();
  }

  public void Dispose()
  {
    if (this.instanceDisposed)
      return;
    this.Close();
    this.instanceDisposed = true;
    GC.SuppressFinalize((object) this);
  }
}
