// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Service.UserDetails
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using PathMedical.DataExchange.Description;
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
[XmlRoot(IsNullable = false)]
[DataExchangeRecord]
[Serializable]
public class UserDetails
{
  public UserDetails()
  {
    if (EspConfigurationManager.Instance.EspConfiguration == null)
      return;
    this.Password = EspConfigurationManager.Instance.EspConfiguration.DefaultUserPassword;
    this.UserProfileId = EspConfigurationManager.Instance.EspConfiguration.DefaultUserProfileId;
  }

  [DataExchangeColumn]
  public string ScreenerId { get; set; }

  [XmlElement(ElementName = "espUserID", DataType = "integer")]
  [DataExchangeColumn]
  public string EspUserID { get; set; }

  [DataExchangeColumn]
  public string Password { get; set; }

  [DataExchangeColumn]
  public Guid UserProfileId { get; set; }
}
