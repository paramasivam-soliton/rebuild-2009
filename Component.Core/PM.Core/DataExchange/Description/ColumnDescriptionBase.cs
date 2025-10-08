// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Description.ColumnDescriptionBase
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Extensions;
using System;
using System.Xml.Linq;

#nullable disable
namespace PathMedical.DataExchange.Description;

public abstract class ColumnDescriptionBase : ISupportDataExchangeColumnDescription
{
  protected ColumnDescriptionBase(RecordDescription recordDescription)
  {
    if (recordDescription == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordDescription));
  }

  protected ColumnDescriptionBase(RecordDescription recordDescription, XElement xmColumnDescription)
    : this(recordDescription)
  {
    if (xmColumnDescription == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (xmColumnDescription));
    if (string.Compare(xmColumnDescription.Name.LocalName, "ColumnDescription", true) != 0)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>("XML element is not a ColumnDescription.");
    if (xmColumnDescription.SafeAttribute("Identifier").Value == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>("XML element doesn't contain a column identifier.");
    if (xmColumnDescription.SafeAttribute("Datatype").Value == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>($"XML element of record {recordDescription.Identifier} for column {xmColumnDescription.SafeAttribute("Identifier").Value} doesn't define a data type.");
    try
    {
      this.UniqueFieldIdentifier = xmColumnDescription.SafeAttribute("Identifier").Value;
      this.Description = xmColumnDescription.SafeAttribute(nameof (Description)).Value;
      this.Format = xmColumnDescription.SafeAttribute(nameof (Format)).Value;
      this.IsArray = !string.IsNullOrEmpty(xmColumnDescription.SafeAttribute(nameof (IsArray)).Value) && Convert.ToBoolean(xmColumnDescription.SafeAttribute(nameof (IsArray)).Value);
      if (this.IsArray)
        this.DataTypes = (DataTypes) Enum.Parse(typeof (DataTypes), $"{xmColumnDescription.SafeAttribute("Datatype").Value}{"Array"}");
      else
        this.DataTypes = (DataTypes) Enum.Parse(typeof (DataTypes), xmColumnDescription.SafeAttribute("Datatype").Value);
    }
    catch (ArgumentException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ColumnDescriptionException>($"Failure while loading data exchange description for column {this.UniqueFieldIdentifier} of record {recordDescription.Identifier}.", (System.Exception) ex);
    }
  }

  public DataTypes DataTypes { get; set; }

  public bool IsReadOnly { get; set; }

  public bool IsArray { get; set; }

  public string Format { get; set; }

  public string UniqueFieldIdentifier { get; set; }

  public string Description { get; set; }
}
