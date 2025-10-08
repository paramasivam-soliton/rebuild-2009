// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Service.EspSyncAllRequest
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

#nullable disable
namespace PathMedical.DataExchange.eSP.Service;

[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlRoot(ElementName = "espSyncAllRequest", IsNullable = false)]
[XmlType(AnonymousType = true)]
[Serializable]
public class EspSyncAllRequest
{
  public string MessageRef { get; set; }

  public string SiteCode { get; set; }

  [XmlElement(ElementName = "ManufacturerID", DataType = "integer")]
  public string ManufacturerId { get; set; }
}
