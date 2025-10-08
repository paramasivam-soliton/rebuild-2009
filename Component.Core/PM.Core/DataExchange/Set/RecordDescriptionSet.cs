// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.Set.RecordDescriptionSet
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.DataExchange.Description;
using PathMedical.Exception;
using PathMedical.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Xml.Linq;

#nullable disable
namespace PathMedical.DataExchange.Set;

[DebuggerDisplay("Identifier: {Identifier}")]
public class RecordDescriptionSet
{
  private readonly Dictionary<string, RecordDescription> recordDescriptions;

  public string Identifier { get; protected set; }

  public IEnumerable<RecordDescription> RecordDescriptions
  {
    get => (IEnumerable<RecordDescription>) this.recordDescriptions.Values;
  }

  public RecordDescriptionSet()
  {
    this.recordDescriptions = new Dictionary<string, RecordDescription>();
  }

  public RecordDescription this[string recordDescriptionIdentifier]
  {
    get
    {
      RecordDescription recordDescription = (RecordDescription) null;
      if (!string.IsNullOrEmpty(recordDescriptionIdentifier))
        recordDescription = this.recordDescriptions.Values.FirstOrDefault<RecordDescription>((Func<RecordDescription, bool>) (rd => rd.Identifier.Equals(recordDescriptionIdentifier)));
      return recordDescription;
    }
  }

  public void AddRecordDescription(RecordDescription[] recordDescription)
  {
    foreach (RecordDescription recordDescription1 in recordDescription)
    {
      if (!this.recordDescriptions.ContainsKey(recordDescription1.Identifier))
        this.recordDescriptions.Add(recordDescription1.Identifier, recordDescription1);
    }
  }

  public RecordDescription GetRecordDescription(string recordDescriptionIdentifier)
  {
    return this[recordDescriptionIdentifier];
  }

  public IEnumerator GetEnumerator() => (IEnumerator) this.recordDescriptions.GetEnumerator();

  public static RecordDescriptionSet LoadXmlFile(string xmlFileName)
  {
    if (string.IsNullOrEmpty(xmlFileName))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (xmlFileName));
    RecordDescriptionSet recordDescriptionSet = new RecordDescriptionSet();
    FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, xmlFileName);
    try
    {
      fileIoPermission.Demand();
    }
    catch (SecurityException ex)
    {
      throw ExceptionFactory.Instance.CreateException<DataExchangeSetException>($"The access to file {xmlFileName} has been denied.", (System.Exception) ex);
    }
    XElement element = File.Exists(xmlFileName) ? XElement.Load(xmlFileName) : throw ExceptionFactory.Instance.CreateException<FileNotFoundException>(nameof (xmlFileName));
    recordDescriptionSet.Identifier = element.SafeAttribute("Identifier").Value;
    if (element.Element((XName) "RecordDescriptions") != null)
      recordDescriptionSet.LoadXmlRecordDescriptions((XContainer) element.Element((XName) "RecordDescriptions"));
    return recordDescriptionSet;
  }

  public static RecordDescriptionSet LoadXmlFile(XElement xmlElement)
  {
    if (xmlElement == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (xmlElement));
    RecordDescriptionSet recordDescriptionSet = new RecordDescriptionSet();
    recordDescriptionSet.Identifier = xmlElement.SafeAttribute("Identifier").Value;
    if (xmlElement.Element((XName) "RecordDescriptions") != null)
      recordDescriptionSet.LoadXmlRecordDescriptions((XContainer) xmlElement.Element((XName) "RecordDescriptions"));
    return recordDescriptionSet;
  }

  private void LoadXmlRecordDescriptions(XContainer xmlRecordDescriptions)
  {
    if (xmlRecordDescriptions == null)
      return;
    foreach (XElement element in xmlRecordDescriptions.Elements((XName) "RecordDescription"))
    {
      RecordDescription recordDescription = new RecordDescription(this, element);
      if (this.recordDescriptions.ContainsKey(recordDescription.Identifier))
        throw ExceptionFactory.Instance.CreateException<DataExchangeSetException>($"Can't add record description {recordDescription.Identifier} since it already exists.");
      this.recordDescriptions.Add(recordDescription.Identifier, recordDescription);
    }
  }

  public bool IsValid
  {
    get
    {
      bool isValid = true;
      if (this.recordDescriptions != null && this.recordDescriptions.Values != null)
      {
        foreach (ComplexColumnDescription columnDescription in this.recordDescriptions.Values.OfType<RecordDescription>().SelectMany<RecordDescription, ComplexColumnDescription>((Func<RecordDescription, IEnumerable<ComplexColumnDescription>>) (rd => rd.Columns.OfType<ComplexColumnDescription>())))
        {
          ComplexColumnDescription description = columnDescription;
          isValid = this.recordDescriptions.Any<KeyValuePair<string, RecordDescription>>((Func<KeyValuePair<string, RecordDescription>, bool>) (rd => string.Compare(rd.Key, description.RecordDescriptionIdentifier) == 0));
          if (!isValid)
            break;
        }
      }
      return isValid;
    }
  }
}
