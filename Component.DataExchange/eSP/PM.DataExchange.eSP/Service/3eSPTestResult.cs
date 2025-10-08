// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Service.TestType
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
public class TestType
{
  [XmlElement(IsNullable = true)]
  public string TestId { get; set; }

  [XmlElement(IsNullable = true)]
  public EarType? Ear { get; set; }

  [XmlElement("TestType", IsNullable = true)]
  public TestTypeType? TestType1 { get; set; }

  [XmlElement(IsNullable = true)]
  public DateTime? StartDate { get; set; }

  [XmlElement(IsNullable = true)]
  public DateTime? EndDate { get; set; }

  [XmlElement(DataType = "integer", IsNullable = true)]
  public string Duration { get; set; }

  [XmlElement(IsNullable = true)]
  public TestTypeTestOutcome? TestOutcome { get; set; }

  [XmlElement(DataType = "base64Binary", IsNullable = true)]
  public byte[] TestImageArray { get; set; }
}
