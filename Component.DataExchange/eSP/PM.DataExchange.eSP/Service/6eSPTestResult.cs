// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Service.PatientType
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace PathMedical.DataExchange.eSP.Service;

[GeneratedCode("xsd", "2.0.50727.3038")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType]
[Serializable]
public class PatientType
{
  public PatientIdType PatientId { get; set; }

  [XmlElement(IsNullable = true)]
  public string Title { get; set; }

  [XmlElement(IsNullable = true)]
  public string Forename { get; set; }

  [XmlElement(IsNullable = true)]
  public string Surname { get; set; }

  [XmlElement(IsNullable = true)]
  public string FullName { get; set; }

  [XmlElement(ElementName = "DOB", IsNullable = true)]
  public DateTime? Dob { get; set; }

  [XmlElement(IsNullable = true)]
  public ConsentType? Consent { get; set; }

  [XmlElement(ElementName = "NICU", IsNullable = true)]
  public YesNoType? Nicu { get; set; }

  [XmlArrayItem("RiskFactor", IsNullable = false)]
  public RiskFactorType[] RiskFactors { get; set; }

  [XmlArrayItem("Test", IsNullable = false)]
  public TestType[] Tests { get; set; }
}
