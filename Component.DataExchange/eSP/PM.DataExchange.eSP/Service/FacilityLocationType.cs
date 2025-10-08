// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Service.FacilityLocationType
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

#nullable disable
namespace PathMedical.DataExchange.eSP.Service;

[GeneratedCode("xsd", "2.0.50727.3038")]
[XmlType]
[Serializable]
public enum FacilityLocationType
{
  Inpatient,
  Outpatient,
  [XmlEnum("Home Visit")] HomeVisit,
}
