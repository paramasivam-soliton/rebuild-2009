// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Description.ComplexColumnDescription
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

[DebuggerDisplay("Identifier: {UniqueFieldIdentifier} RecordDescription: {RecordDescriptionIdentifier} Array: {IsArray}")]
public class ComplexColumnDescription : ISupportDataExchangeColumnDescription
{
  public string UniqueFieldIdentifier { get; protected set; }

  public string Description { get; set; }

  public int Length { get; protected set; }

  public bool IsArray { get; protected set; }

  public string RecordDescriptionIdentifier { get; protected set; }

  public ComplexColumnDescription(XElement xmlColumnDescription)
  {
    if (xmlColumnDescription == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (xmlColumnDescription));
    if (!xmlColumnDescription.Name.LocalName.Equals(nameof (ComplexColumnDescription)))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("The column description is not a ComplexColumnDescription.");
    if (string.IsNullOrEmpty(xmlColumnDescription.SafeAttribute("Identifier").Value))
      throw ExceptionFactory.Instance.CreateException<ColumnDescriptionException>("The complex column description doesn't contain an identfier.");
    this.UniqueFieldIdentifier = !string.IsNullOrEmpty(xmlColumnDescription.SafeAttribute("RecordDescription").Value) ? xmlColumnDescription.SafeAttribute("Identifier").Value : throw ExceptionFactory.Instance.CreateException<ColumnDescriptionException>("The complex column description doesn't contain a record description.");
    this.RecordDescriptionIdentifier = xmlColumnDescription.SafeAttribute("RecordDescription").Value;
    this.IsArray = !string.IsNullOrEmpty(xmlColumnDescription.SafeAttribute(nameof (IsArray)).Value) && Convert.ToBoolean(xmlColumnDescription.SafeAttribute(nameof (IsArray)).Value);
  }
}
