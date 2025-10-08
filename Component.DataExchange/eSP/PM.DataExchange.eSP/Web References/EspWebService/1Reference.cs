// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.EspWebService.CSPCHD
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

#nullable disable
namespace PathMedical.DataExchange.eSP.EspWebService;

[GeneratedCode("System.Xml", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(Namespace = "http://www.intersystems.com/SOAPheaders")]
[XmlRoot(Namespace = "http://www.intersystems.com/SOAPheaders", IsNullable = false)]
[Serializable]
public class CSPCHD : SoapHeader
{
  private string idField;

  public string id
  {
    get => this.idField;
    set => this.idField = value;
  }
}
