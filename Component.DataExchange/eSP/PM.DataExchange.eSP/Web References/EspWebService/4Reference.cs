// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.EspWebService.validateEquipmentResponseValidateEquipmentResult
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace PathMedical.DataExchange.eSP.EspWebService;

[GeneratedCode("System.Xml", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[XmlType(AnonymousType = true, Namespace = "http://www.myapp.org")]
[Serializable]
public class validateEquipmentResponseValidateEquipmentResult
{
  private XmlElement[] itemsField;
  private string[] textField;

  [XmlAnyElement]
  public XmlElement[] Items
  {
    get => this.itemsField;
    set => this.itemsField = value;
  }

  [XmlText]
  public string[] Text
  {
    get => this.textField;
    set => this.textField = value;
  }
}
