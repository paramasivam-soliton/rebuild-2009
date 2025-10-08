// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Description.TextColumnDescription
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Extensions;
using System;
using System.Diagnostics;
using System.Xml.Linq;

#nullable disable
namespace PathMedical.DataExchange.Description;

[DebuggerDisplay("Identifier: {UniqueFieldIdentifier} DataTypes: {DataTypes}")]
public class TextColumnDescription : ColumnDescriptionBase
{
  public int? MinimumLength { get; set; }

  public int? MaximumLength { get; set; }

  public TextColumnDescription(RecordDescription recordDescription)
    : base(recordDescription)
  {
  }

  public TextColumnDescription(RecordDescription recordDescription, XElement xmColumnDescription)
    : base(recordDescription, xmColumnDescription)
  {
    try
    {
      if (!string.IsNullOrEmpty(xmColumnDescription.SafeAttribute("MinLength").Value))
        this.MinimumLength = new int?(Convert.ToInt32(xmColumnDescription.SafeAttribute("MinLength").Value));
      if (string.IsNullOrEmpty(xmColumnDescription.SafeAttribute("MaxLength").Value))
        return;
      this.MaximumLength = new int?(Convert.ToInt32(xmColumnDescription.SafeAttribute("MaxLength").Value));
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ColumnDescriptionException>($"Failure while loading data exchange description for column {this.UniqueFieldIdentifier} of record {recordDescription.Identifier}.", (System.Exception) ex);
    }
    catch (FormatException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ColumnDescriptionException>($"Failure while loading data exchange description for column {this.UniqueFieldIdentifier} of record {recordDescription.Identifier}.", (System.Exception) ex);
    }
    catch (OverflowException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ColumnDescriptionException>($"Failure while loading data exchange description for column {this.UniqueFieldIdentifier} of record {recordDescription.Identifier}.", (System.Exception) ex);
    }
  }
}
