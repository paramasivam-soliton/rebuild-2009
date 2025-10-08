// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Description.RecordDescription
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.Property;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace PathMedical.DataExchange.Description;

[DebuggerDisplay("RecordDescription {Identifier} [{Description}]")]
public class RecordDescription
{
  private readonly List<ISupportDataExchangeColumnDescription> columns;

  public RecordDescriptionSet RecordDescriptionSet { get; protected set; }

  public string Identifier { get; set; }

  public string Description { get; set; }

  public List<ISupportDataExchangeColumnDescription> Columns
  {
    get => this.columns.ToList<ISupportDataExchangeColumnDescription>();
  }

  public RecordDescription(RecordDescriptionSet recordDescriptionSet)
  {
    this.RecordDescriptionSet = recordDescriptionSet != null ? recordDescriptionSet : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordDescriptionSet));
  }

  public RecordDescription(RecordDescriptionSet recordDescriptionSet, XElement xmlRecordDescription)
  {
    if (recordDescriptionSet == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (recordDescriptionSet));
    if (xmlRecordDescription == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>("The XML defintion is empty so no record description will be created.");
    if (string.Compare(xmlRecordDescription.Name.LocalName, nameof (RecordDescription), true) != 0)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>("XML element is not a record description.");
    if (xmlRecordDescription.SafeAttribute(nameof (Identifier)).Value == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentException>("XML element doesn't contain a record identifier.");
    this.RecordDescriptionSet = recordDescriptionSet;
    this.Identifier = xmlRecordDescription.SafeAttribute(nameof (Identifier)).Value;
    this.Description = xmlRecordDescription.SafeAttribute(nameof (Description)).Value;
    this.columns = new List<ISupportDataExchangeColumnDescription>();
    this.LoadColumns(xmlRecordDescription.Elements());
  }

  private void LoadColumns(IEnumerable<XElement> xmlColumnDescriptions)
  {
    if (xmlColumnDescriptions == null || xmlColumnDescriptions.Count<XElement>() == 0)
      return;
    foreach (XElement columnDescription in xmlColumnDescriptions)
    {
      if (columnDescription.Name.LocalName.Equals("ColumnDescription"))
        this.AddColumn((DataTypes) Enum.Parse(typeof (DataTypes), columnDescription.SafeAttribute("Datatype").Value) != DataTypes.String ? (ISupportDataExchangeColumnDescription) new NumericColumnDescription(this, columnDescription) : (ISupportDataExchangeColumnDescription) new TextColumnDescription(this, columnDescription));
      if (columnDescription.Name.LocalName.Equals("IncludeRecordDescription") && !string.IsNullOrEmpty(columnDescription.SafeAttribute("Identifier").Value))
      {
        foreach (ISupportDataExchangeColumnDescription column in this.RecordDescriptionSet[columnDescription.SafeAttribute("Identifier").Value].Columns)
          this.AddColumn(column);
      }
      if (columnDescription.Name.LocalName.Equals("ComplexColumnDescription"))
        this.AddColumn((ISupportDataExchangeColumnDescription) new ComplexColumnDescription(columnDescription));
    }
  }

  private void AddColumn(ISupportDataExchangeColumnDescription column)
  {
    if (column == null)
      return;
    this.columns.Add(column);
  }

  public ISupportDataExchangeColumnDescription GetColumnDescriptionByIdentifier(
    string columnIdentifier)
  {
    if (string.IsNullOrEmpty(columnIdentifier))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (columnIdentifier));
    ISupportDataExchangeColumnDescription descriptionByIdentifier = (ISupportDataExchangeColumnDescription) null;
    string[] source = columnIdentifier.Split('.');
    string columnIdentifierToFind = source[0];
    if (((IEnumerable<string>) source).Count<string>() > 1)
    {
      ComplexColumnDescription columnDescription = this.columns.OfType<ComplexColumnDescription>().FirstOrDefault<ComplexColumnDescription>((Func<ComplexColumnDescription, bool>) (c => string.Compare(c.UniqueFieldIdentifier, columnIdentifierToFind) == 0));
      if (columnDescription != null)
        descriptionByIdentifier = this.RecordDescriptionSet[columnDescription.RecordDescriptionIdentifier].GetColumnDescriptionByIdentifier(string.Join(".", source, 1, source.Length - 1));
    }
    else
      descriptionByIdentifier = this.columns.FirstOrDefault<ISupportDataExchangeColumnDescription>((Func<ISupportDataExchangeColumnDescription, bool>) (c => string.Compare(c.UniqueFieldIdentifier, columnIdentifier) == 0));
    return descriptionByIdentifier;
  }

  internal T GetObject<T>(DataExchangeTokenSet tokenSet) where T : class
  {
    object target = (object) null;
    if (tokenSet.SupportsType<T>() && tokenSet.RecordDescription.Identifier.Equals(this.Identifier))
    {
      target = Activator.CreateInstance(typeof (T));
      if (target != null)
      {
        foreach (ISupportDataExchangeToken token in tokenSet)
        {
          if (this.GetColumnDescriptionByIdentifier(token.UniqueIdentifier) != null)
            PropertyHelper<DataExchangeColumnAttribute>.SetValue(token.UniqueIdentifier, token.Data, ref target);
        }
      }
    }
    return target as T;
  }
}
