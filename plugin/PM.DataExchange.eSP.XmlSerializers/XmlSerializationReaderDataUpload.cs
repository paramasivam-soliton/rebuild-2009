// Decompiled with JetBrains decompiler
// Type: Microsoft.Xml.Serialization.GeneratedAssembly.XmlSerializationReaderDataUpload
// Assembly: PM.DataExchange.eSP.XmlSerializers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BF431D5-59B5-474D-AC31-ED3AC503BC3E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.XmlSerializers.dll

using PathMedical.DataExchange.eSP.EspWebService;
using System;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Microsoft.Xml.Serialization.GeneratedAssembly;

public class XmlSerializationReaderDataUpload : XmlSerializationReader
{
  private string id2_httpwwwmyapporg;
  private string id12_Item;
  private string id5_Item;
  private string id17_Item;
  private string id6_uploadDataResponse;
  private string id4_CSPCHD;
  private string id3_downloadSyncDataResult;
  private string id1_downloadSyncDataResponse;
  private string id15_relay;
  private string id8_validateEquipmentResponse;
  private string id14_role;
  private string id7_uploadDataResult;
  private string id10_mustUnderstand;
  private string id9_validateEquipmentResult;
  private string id16_id;
  private string id13_actor;
  private string id11_Item;

  public object[] Read7_downloadSyncDataResponse()
  {
    int content1 = (int) this.Reader.MoveToContent();
    object[] o = new object[1];
    int content2 = (int) this.Reader.MoveToContent();
    int whileIterations1 = 0;
    int readerCount1 = this.ReaderCount;
    while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
    {
      if (this.Reader.IsStartElement(this.id1_downloadSyncDataResponse, this.id2_httpwwwmyapporg))
      {
        bool[] flagArray = new bool[1];
        if (this.Reader.IsEmptyElement)
        {
          this.Reader.Skip();
          int content3 = (int) this.Reader.MoveToContent();
          continue;
        }
        this.Reader.ReadStartElement();
        int content4 = (int) this.Reader.MoveToContent();
        int whileIterations2 = 0;
        int readerCount2 = this.ReaderCount;
        while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
        {
          if (this.Reader.NodeType == XmlNodeType.Element)
          {
            if (!flagArray[0] && (object) this.Reader.LocalName == (object) this.id3_downloadSyncDataResult && (object) this.Reader.NamespaceURI == (object) this.id2_httpwwwmyapporg)
            {
              o[0] = (object) this.Read2_Item(false, true);
              flagArray[0] = true;
            }
            else
              this.UnknownNode((object) o, "http://www.myapp.org:downloadSyncDataResult");
          }
          else
            this.UnknownNode((object) o, "http://www.myapp.org:downloadSyncDataResult");
          int content5 = (int) this.Reader.MoveToContent();
          this.CheckReaderCount(ref whileIterations2, ref readerCount2);
        }
        this.ReadEndElement();
      }
      else
        this.UnknownNode((object) null, "http://www.myapp.org:downloadSyncDataResponse");
      int content6 = (int) this.Reader.MoveToContent();
      this.CheckReaderCount(ref whileIterations1, ref readerCount1);
    }
    return o;
  }

  public object[] Read8_Item()
  {
    int content1 = (int) this.Reader.MoveToContent();
    object[] o = new object[1];
    bool[] flagArray = new bool[1];
    int content2 = (int) this.Reader.MoveToContent();
    int whileIterations = 0;
    int readerCount = this.ReaderCount;
    while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
    {
      if (this.Reader.NodeType == XmlNodeType.Element)
      {
        if (!flagArray[0] && (object) this.Reader.LocalName == (object) this.id4_CSPCHD && (object) this.Reader.NamespaceURI == (object) this.id5_Item)
        {
          o[0] = (object) this.Read4_CSPCHD(false, true);
          flagArray[0] = true;
        }
        else
          this.UnknownNode((object) o, "http://www.intersystems.com/SOAPheaders:CSPCHD");
      }
      else
        this.UnknownNode((object) o, "http://www.intersystems.com/SOAPheaders:CSPCHD");
      int content3 = (int) this.Reader.MoveToContent();
      this.CheckReaderCount(ref whileIterations, ref readerCount);
    }
    return o;
  }

  public object[] Read9_uploadDataResponse()
  {
    int content1 = (int) this.Reader.MoveToContent();
    object[] o = new object[1];
    int content2 = (int) this.Reader.MoveToContent();
    int whileIterations1 = 0;
    int readerCount1 = this.ReaderCount;
    while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
    {
      if (this.Reader.IsStartElement(this.id6_uploadDataResponse, this.id2_httpwwwmyapporg))
      {
        bool[] flagArray = new bool[1];
        if (this.Reader.IsEmptyElement)
        {
          this.Reader.Skip();
          int content3 = (int) this.Reader.MoveToContent();
          continue;
        }
        this.Reader.ReadStartElement();
        int content4 = (int) this.Reader.MoveToContent();
        int whileIterations2 = 0;
        int readerCount2 = this.ReaderCount;
        while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
        {
          if (this.Reader.NodeType == XmlNodeType.Element)
          {
            if (!flagArray[0] && (object) this.Reader.LocalName == (object) this.id7_uploadDataResult && (object) this.Reader.NamespaceURI == (object) this.id2_httpwwwmyapporg)
            {
              o[0] = (object) this.Read5_Item(false, true);
              flagArray[0] = true;
            }
            else
              this.UnknownNode((object) o, "http://www.myapp.org:uploadDataResult");
          }
          else
            this.UnknownNode((object) o, "http://www.myapp.org:uploadDataResult");
          int content5 = (int) this.Reader.MoveToContent();
          this.CheckReaderCount(ref whileIterations2, ref readerCount2);
        }
        this.ReadEndElement();
      }
      else
        this.UnknownNode((object) null, "http://www.myapp.org:uploadDataResponse");
      int content6 = (int) this.Reader.MoveToContent();
      this.CheckReaderCount(ref whileIterations1, ref readerCount1);
    }
    return o;
  }

  public object[] Read10_uploadDataResponseOutHeaders()
  {
    int content1 = (int) this.Reader.MoveToContent();
    object[] o = new object[1];
    bool[] flagArray = new bool[1];
    int content2 = (int) this.Reader.MoveToContent();
    int whileIterations = 0;
    int readerCount = this.ReaderCount;
    while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
    {
      if (this.Reader.NodeType == XmlNodeType.Element)
      {
        if (!flagArray[0] && (object) this.Reader.LocalName == (object) this.id4_CSPCHD && (object) this.Reader.NamespaceURI == (object) this.id5_Item)
        {
          o[0] = (object) this.Read4_CSPCHD(false, true);
          flagArray[0] = true;
        }
        else
          this.UnknownNode((object) o, "http://www.intersystems.com/SOAPheaders:CSPCHD");
      }
      else
        this.UnknownNode((object) o, "http://www.intersystems.com/SOAPheaders:CSPCHD");
      int content3 = (int) this.Reader.MoveToContent();
      this.CheckReaderCount(ref whileIterations, ref readerCount);
    }
    return o;
  }

  public object[] Read11_validateEquipmentResponse()
  {
    int content1 = (int) this.Reader.MoveToContent();
    object[] o = new object[1];
    int content2 = (int) this.Reader.MoveToContent();
    int whileIterations1 = 0;
    int readerCount1 = this.ReaderCount;
    while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
    {
      if (this.Reader.IsStartElement(this.id8_validateEquipmentResponse, this.id2_httpwwwmyapporg))
      {
        bool[] flagArray = new bool[1];
        if (this.Reader.IsEmptyElement)
        {
          this.Reader.Skip();
          int content3 = (int) this.Reader.MoveToContent();
          continue;
        }
        this.Reader.ReadStartElement();
        int content4 = (int) this.Reader.MoveToContent();
        int whileIterations2 = 0;
        int readerCount2 = this.ReaderCount;
        while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
        {
          if (this.Reader.NodeType == XmlNodeType.Element)
          {
            if (!flagArray[0] && (object) this.Reader.LocalName == (object) this.id9_validateEquipmentResult && (object) this.Reader.NamespaceURI == (object) this.id2_httpwwwmyapporg)
            {
              o[0] = (object) this.Read6_Item(false, true);
              flagArray[0] = true;
            }
            else
              this.UnknownNode((object) o, "http://www.myapp.org:validateEquipmentResult");
          }
          else
            this.UnknownNode((object) o, "http://www.myapp.org:validateEquipmentResult");
          int content5 = (int) this.Reader.MoveToContent();
          this.CheckReaderCount(ref whileIterations2, ref readerCount2);
        }
        this.ReadEndElement();
      }
      else
        this.UnknownNode((object) null, "http://www.myapp.org:validateEquipmentResponse");
      int content6 = (int) this.Reader.MoveToContent();
      this.CheckReaderCount(ref whileIterations1, ref readerCount1);
    }
    return o;
  }

  public object[] Read12_Item()
  {
    int content1 = (int) this.Reader.MoveToContent();
    object[] o = new object[1];
    bool[] flagArray = new bool[1];
    int content2 = (int) this.Reader.MoveToContent();
    int whileIterations = 0;
    int readerCount = this.ReaderCount;
    while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
    {
      if (this.Reader.NodeType == XmlNodeType.Element)
      {
        if (!flagArray[0] && (object) this.Reader.LocalName == (object) this.id4_CSPCHD && (object) this.Reader.NamespaceURI == (object) this.id5_Item)
        {
          o[0] = (object) this.Read4_CSPCHD(false, true);
          flagArray[0] = true;
        }
        else
          this.UnknownNode((object) o, "http://www.intersystems.com/SOAPheaders:CSPCHD");
      }
      else
        this.UnknownNode((object) o, "http://www.intersystems.com/SOAPheaders:CSPCHD");
      int content3 = (int) this.Reader.MoveToContent();
      this.CheckReaderCount(ref whileIterations, ref readerCount);
    }
    return o;
  }

  private CSPCHD Read4_CSPCHD(bool isNullable, bool checkType)
  {
    XmlQualifiedName xsiType = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
    bool flag = false;
    if (isNullable)
      flag = this.ReadNull();
    if (checkType && !(xsiType == (XmlQualifiedName) null) && ((object) xsiType.Name != (object) this.id4_CSPCHD || (object) xsiType.Namespace != (object) this.id5_Item))
      throw this.CreateUnknownTypeException(xsiType);
    if (flag)
      return (CSPCHD) null;
    CSPCHD o = new CSPCHD();
    bool[] flagArray = new bool[6];
    while (this.Reader.MoveToNextAttribute())
    {
      if (!flagArray[0] && (object) this.Reader.LocalName == (object) this.id10_mustUnderstand && (object) this.Reader.NamespaceURI == (object) this.id11_Item)
      {
        o.EncodedMustUnderstand = this.Reader.Value;
        flagArray[0] = true;
      }
      else if (!flagArray[1] && (object) this.Reader.LocalName == (object) this.id10_mustUnderstand && (object) this.Reader.NamespaceURI == (object) this.id12_Item)
      {
        o.EncodedMustUnderstand12 = this.Reader.Value;
        flagArray[1] = true;
      }
      else if (!flagArray[2] && (object) this.Reader.LocalName == (object) this.id13_actor && (object) this.Reader.NamespaceURI == (object) this.id11_Item)
      {
        o.Actor = this.Reader.Value;
        flagArray[2] = true;
      }
      else if (!flagArray[3] && (object) this.Reader.LocalName == (object) this.id14_role && (object) this.Reader.NamespaceURI == (object) this.id12_Item)
      {
        o.Role = this.Reader.Value;
        flagArray[3] = true;
      }
      else if (!flagArray[4] && (object) this.Reader.LocalName == (object) this.id15_relay && (object) this.Reader.NamespaceURI == (object) this.id12_Item)
      {
        o.EncodedRelay = this.Reader.Value;
        flagArray[4] = true;
      }
      else if (!this.IsXmlnsAttribute(this.Reader.Name))
        this.UnknownNode((object) o, "http://schemas.xmlsoap.org/soap/envelope/:mustUnderstand, http://www.w3.org/2003/05/soap-envelope:mustUnderstand, http://schemas.xmlsoap.org/soap/envelope/:actor, http://www.w3.org/2003/05/soap-envelope:role, http://www.w3.org/2003/05/soap-envelope:relay");
    }
    this.Reader.MoveToElement();
    if (this.Reader.IsEmptyElement)
    {
      this.Reader.Skip();
      return o;
    }
    this.Reader.ReadStartElement();
    int content1 = (int) this.Reader.MoveToContent();
    int whileIterations = 0;
    int readerCount = this.ReaderCount;
    while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
    {
      if (this.Reader.NodeType == XmlNodeType.Element)
      {
        if (!flagArray[5] && (object) this.Reader.LocalName == (object) this.id16_id && (object) this.Reader.NamespaceURI == (object) this.id5_Item)
        {
          o.id = this.Reader.ReadElementString();
          flagArray[5] = true;
        }
        else
          this.UnknownNode((object) o, "http://www.intersystems.com/SOAPheaders:id");
      }
      else
        this.UnknownNode((object) o, "http://www.intersystems.com/SOAPheaders:id");
      int content2 = (int) this.Reader.MoveToContent();
      this.CheckReaderCount(ref whileIterations, ref readerCount);
    }
    this.ReadEndElement();
    return o;
  }

  private validateEquipmentResponseValidateEquipmentResult Read6_Item(
    bool isNullable,
    bool checkType)
  {
    XmlQualifiedName xsiType = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
    bool flag = false;
    if (isNullable)
      flag = this.ReadNull();
    if (checkType && !(xsiType == (XmlQualifiedName) null) && ((object) xsiType.Name != (object) this.id17_Item || (object) xsiType.Namespace != (object) this.id2_httpwwwmyapporg))
      throw this.CreateUnknownTypeException(xsiType);
    if (flag)
      return (validateEquipmentResponseValidateEquipmentResult) null;
    validateEquipmentResponseValidateEquipmentResult o = new validateEquipmentResponseValidateEquipmentResult();
    XmlElement[] a1 = (XmlElement[]) null;
    int num1 = 0;
    string[] a2 = (string[]) null;
    int num2 = 0;
    while (this.Reader.MoveToNextAttribute())
    {
      if (!this.IsXmlnsAttribute(this.Reader.Name))
        this.UnknownNode((object) o);
    }
    this.Reader.MoveToElement();
    if (this.Reader.IsEmptyElement)
    {
      this.Reader.Skip();
      o.Items = (XmlElement[]) this.ShrinkArray((Array) a1, num1, typeof (XmlElement), true);
      o.Text = (string[]) this.ShrinkArray((Array) a2, num2, typeof (string), true);
      return o;
    }
    this.Reader.ReadStartElement();
    int content1 = (int) this.Reader.MoveToContent();
    int whileIterations = 0;
    int readerCount = this.ReaderCount;
    while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
    {
      if (this.Reader.NodeType == XmlNodeType.Element)
      {
        a1 = (XmlElement[]) this.EnsureArrayIndex((Array) a1, num1, typeof (XmlElement));
        a1[num1++] = (XmlElement) this.ReadXmlNode(false);
      }
      else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA || this.Reader.NodeType == XmlNodeType.Whitespace || this.Reader.NodeType == XmlNodeType.SignificantWhitespace)
      {
        a2 = (string[]) this.EnsureArrayIndex((Array) a2, num2, typeof (string));
        a2[num2++] = this.Reader.ReadString();
      }
      else
        this.UnknownNode((object) o, "");
      int content2 = (int) this.Reader.MoveToContent();
      this.CheckReaderCount(ref whileIterations, ref readerCount);
    }
    o.Items = (XmlElement[]) this.ShrinkArray((Array) a1, num1, typeof (XmlElement), true);
    o.Text = (string[]) this.ShrinkArray((Array) a2, num2, typeof (string), true);
    this.ReadEndElement();
    return o;
  }

  private uploadDataResponseUploadDataResult Read5_Item(bool isNullable, bool checkType)
  {
    XmlQualifiedName xsiType = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
    bool flag = false;
    if (isNullable)
      flag = this.ReadNull();
    if (checkType && !(xsiType == (XmlQualifiedName) null) && ((object) xsiType.Name != (object) this.id17_Item || (object) xsiType.Namespace != (object) this.id2_httpwwwmyapporg))
      throw this.CreateUnknownTypeException(xsiType);
    if (flag)
      return (uploadDataResponseUploadDataResult) null;
    uploadDataResponseUploadDataResult o = new uploadDataResponseUploadDataResult();
    XmlElement[] a1 = (XmlElement[]) null;
    int num1 = 0;
    string[] a2 = (string[]) null;
    int num2 = 0;
    while (this.Reader.MoveToNextAttribute())
    {
      if (!this.IsXmlnsAttribute(this.Reader.Name))
        this.UnknownNode((object) o);
    }
    this.Reader.MoveToElement();
    if (this.Reader.IsEmptyElement)
    {
      this.Reader.Skip();
      o.Items = (XmlElement[]) this.ShrinkArray((Array) a1, num1, typeof (XmlElement), true);
      o.Text = (string[]) this.ShrinkArray((Array) a2, num2, typeof (string), true);
      return o;
    }
    this.Reader.ReadStartElement();
    int content1 = (int) this.Reader.MoveToContent();
    int whileIterations = 0;
    int readerCount = this.ReaderCount;
    while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
    {
      if (this.Reader.NodeType == XmlNodeType.Element)
      {
        a1 = (XmlElement[]) this.EnsureArrayIndex((Array) a1, num1, typeof (XmlElement));
        a1[num1++] = (XmlElement) this.ReadXmlNode(false);
      }
      else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA || this.Reader.NodeType == XmlNodeType.Whitespace || this.Reader.NodeType == XmlNodeType.SignificantWhitespace)
      {
        a2 = (string[]) this.EnsureArrayIndex((Array) a2, num2, typeof (string));
        a2[num2++] = this.Reader.ReadString();
      }
      else
        this.UnknownNode((object) o, "");
      int content2 = (int) this.Reader.MoveToContent();
      this.CheckReaderCount(ref whileIterations, ref readerCount);
    }
    o.Items = (XmlElement[]) this.ShrinkArray((Array) a1, num1, typeof (XmlElement), true);
    o.Text = (string[]) this.ShrinkArray((Array) a2, num2, typeof (string), true);
    this.ReadEndElement();
    return o;
  }

  private downloadSyncDataResponseDownloadSyncDataResult Read2_Item(bool isNullable, bool checkType)
  {
    XmlQualifiedName xsiType = checkType ? this.GetXsiType() : (XmlQualifiedName) null;
    bool flag = false;
    if (isNullable)
      flag = this.ReadNull();
    if (checkType && !(xsiType == (XmlQualifiedName) null) && ((object) xsiType.Name != (object) this.id17_Item || (object) xsiType.Namespace != (object) this.id2_httpwwwmyapporg))
      throw this.CreateUnknownTypeException(xsiType);
    if (flag)
      return (downloadSyncDataResponseDownloadSyncDataResult) null;
    downloadSyncDataResponseDownloadSyncDataResult o = new downloadSyncDataResponseDownloadSyncDataResult();
    XmlElement[] a1 = (XmlElement[]) null;
    int num1 = 0;
    string[] a2 = (string[]) null;
    int num2 = 0;
    while (this.Reader.MoveToNextAttribute())
    {
      if (!this.IsXmlnsAttribute(this.Reader.Name))
        this.UnknownNode((object) o);
    }
    this.Reader.MoveToElement();
    if (this.Reader.IsEmptyElement)
    {
      this.Reader.Skip();
      o.Items = (XmlElement[]) this.ShrinkArray((Array) a1, num1, typeof (XmlElement), true);
      o.Text = (string[]) this.ShrinkArray((Array) a2, num2, typeof (string), true);
      return o;
    }
    this.Reader.ReadStartElement();
    int content1 = (int) this.Reader.MoveToContent();
    int whileIterations = 0;
    int readerCount = this.ReaderCount;
    while (this.Reader.NodeType != XmlNodeType.EndElement && this.Reader.NodeType != XmlNodeType.None)
    {
      if (this.Reader.NodeType == XmlNodeType.Element)
      {
        a1 = (XmlElement[]) this.EnsureArrayIndex((Array) a1, num1, typeof (XmlElement));
        a1[num1++] = (XmlElement) this.ReadXmlNode(false);
      }
      else if (this.Reader.NodeType == XmlNodeType.Text || this.Reader.NodeType == XmlNodeType.CDATA || this.Reader.NodeType == XmlNodeType.Whitespace || this.Reader.NodeType == XmlNodeType.SignificantWhitespace)
      {
        a2 = (string[]) this.EnsureArrayIndex((Array) a2, num2, typeof (string));
        a2[num2++] = this.Reader.ReadString();
      }
      else
        this.UnknownNode((object) o, "");
      int content2 = (int) this.Reader.MoveToContent();
      this.CheckReaderCount(ref whileIterations, ref readerCount);
    }
    o.Items = (XmlElement[]) this.ShrinkArray((Array) a1, num1, typeof (XmlElement), true);
    o.Text = (string[]) this.ShrinkArray((Array) a2, num2, typeof (string), true);
    this.ReadEndElement();
    return o;
  }

  protected override void InitCallbacks()
  {
  }

  protected override void InitIDs()
  {
    this.id2_httpwwwmyapporg = this.Reader.NameTable.Add("http://www.myapp.org");
    this.id12_Item = this.Reader.NameTable.Add("http://www.w3.org/2003/05/soap-envelope");
    this.id5_Item = this.Reader.NameTable.Add("http://www.intersystems.com/SOAPheaders");
    this.id17_Item = this.Reader.NameTable.Add("");
    this.id6_uploadDataResponse = this.Reader.NameTable.Add("uploadDataResponse");
    this.id4_CSPCHD = this.Reader.NameTable.Add("CSPCHD");
    this.id3_downloadSyncDataResult = this.Reader.NameTable.Add("downloadSyncDataResult");
    this.id1_downloadSyncDataResponse = this.Reader.NameTable.Add("downloadSyncDataResponse");
    this.id15_relay = this.Reader.NameTable.Add("relay");
    this.id8_validateEquipmentResponse = this.Reader.NameTable.Add("validateEquipmentResponse");
    this.id14_role = this.Reader.NameTable.Add("role");
    this.id7_uploadDataResult = this.Reader.NameTable.Add("uploadDataResult");
    this.id10_mustUnderstand = this.Reader.NameTable.Add("mustUnderstand");
    this.id9_validateEquipmentResult = this.Reader.NameTable.Add("validateEquipmentResult");
    this.id16_id = this.Reader.NameTable.Add("id");
    this.id13_actor = this.Reader.NameTable.Add("actor");
    this.id11_Item = this.Reader.NameTable.Add("http://schemas.xmlsoap.org/soap/envelope/");
  }
}
