// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Service.EspSyncAllResponse
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
[XmlType(AnonymousType = true)]
[XmlRoot(ElementName = "espSyncAllResponse", IsNullable = false)]
[Serializable]
public class EspSyncAllResponse
{
  public string MessageRef { get; set; }

  public DateTime MessageDateTime { get; set; }

  public RequestResult RequestResult { get; set; }

  public string FailureReason { get; set; }

  [XmlArrayItem("FacilityDetails", IsNullable = false)]
  public FacilityDetails[] SiteFacilityData { get; set; }

  [XmlArrayItem("DeviceDetails", IsNullable = false)]
  public DeviceDetails[] ScreeningDevices { get; set; }

  [XmlArrayItem("UserDetails", IsNullable = false)]
  public UserDetails[] DeviceUsers { get; set; }

  [XmlArrayItem("RiskFactor", IsNullable = false)]
  public RiskFactor[] RiskFactors { get; set; }
}
