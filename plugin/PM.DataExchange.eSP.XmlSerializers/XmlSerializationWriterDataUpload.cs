// Decompiled with JetBrains decompiler
// Type: Microsoft.Xml.Serialization.GeneratedAssembly.XmlSerializationWriterDataUpload
// Assembly: PM.DataExchange.eSP.XmlSerializers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BF431D5-59B5-474D-AC31-ED3AC503BC3E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.XmlSerializers.dll

using PathMedical.DataExchange.eSP.EspWebService;
using System.Xml.Serialization;

#nullable disable
namespace Microsoft.Xml.Serialization.GeneratedAssembly;

public class XmlSerializationWriterDataUpload : XmlSerializationWriter
{
  public void Write7_downloadSyncData(object[] p)
  {
    this.WriteStartDocument();
    this.TopLevelElement();
    int length = p.Length;
    this.WriteStartElement("downloadSyncData", "http://www.myapp.org", (object) null, false);
    if (length > 0)
      this.WriteElementString("SyncAllXML", "http://www.myapp.org", (string) p[0]);
    this.WriteEndElement();
  }

  public void Write8_downloadSyncDataInHeaders(object[] p)
  {
    this.WriteStartDocument();
    this.TopLevelElement();
    if (p.Length <= 0)
      return;
    this.Write4_CSPCHD("CSPCHD", "http://www.intersystems.com/SOAPheaders", (CSPCHD) p[0], false, false);
  }

  public void Write9_uploadData(object[] p)
  {
    this.WriteStartDocument();
    this.TopLevelElement();
    int length = p.Length;
    this.WriteStartElement("uploadData", "http://www.myapp.org", (object) null, false);
    if (length > 0)
      this.WriteElementString("thisMessageXML", "http://www.myapp.org", (string) p[0]);
    this.WriteEndElement();
  }

  public void Write10_uploadDataInHeaders(object[] p)
  {
    this.WriteStartDocument();
    this.TopLevelElement();
    if (p.Length <= 0)
      return;
    this.Write4_CSPCHD("CSPCHD", "http://www.intersystems.com/SOAPheaders", (CSPCHD) p[0], false, false);
  }

  public void Write11_validateEquipment(object[] p)
  {
    this.WriteStartDocument();
    this.TopLevelElement();
    int length = p.Length;
    this.WriteStartElement("validateEquipment", "http://www.myapp.org", (object) null, false);
    if (length > 0)
      this.WriteElementString("equipXML", "http://www.myapp.org", (string) p[0]);
    this.WriteEndElement();
  }

  public void Write12_validateEquipmentInHeaders(object[] p)
  {
    this.WriteStartDocument();
    this.TopLevelElement();
    if (p.Length <= 0)
      return;
    this.Write4_CSPCHD("CSPCHD", "http://www.intersystems.com/SOAPheaders", (CSPCHD) p[0], false, false);
  }

  private void Write4_CSPCHD(string n, string ns, CSPCHD o, bool isNullable, bool needType)
  {
    if (o == null)
    {
      if (!isNullable)
        return;
      this.WriteNullTagLiteral(n, ns);
    }
    else
    {
      if (!needType && !(o.GetType() == typeof (CSPCHD)))
        throw this.CreateUnknownTypeException((object) o);
      this.WriteStartElement(n, ns, (object) o, false, (XmlSerializerNamespaces) null);
      if (needType)
        this.WriteXsiType("CSPCHD", "http://www.intersystems.com/SOAPheaders");
      if (o.EncodedMustUnderstand != "0")
        this.WriteAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", o.EncodedMustUnderstand);
      if (o.EncodedMustUnderstand12 != "0")
        this.WriteAttribute("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope", o.EncodedMustUnderstand12);
      if (o.Actor != null && o.Actor.Length != 0)
        this.WriteAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/", o.Actor);
      if (o.Role != null && o.Role.Length != 0)
        this.WriteAttribute("role", "http://www.w3.org/2003/05/soap-envelope", o.Role);
      if (o.EncodedRelay != "0")
        this.WriteAttribute("relay", "http://www.w3.org/2003/05/soap-envelope", o.EncodedRelay);
      this.WriteElementString("id", "http://www.intersystems.com/SOAPheaders", o.id);
      this.WriteEndElement((object) o);
    }
  }

  protected override void InitCallbacks()
  {
  }
}
