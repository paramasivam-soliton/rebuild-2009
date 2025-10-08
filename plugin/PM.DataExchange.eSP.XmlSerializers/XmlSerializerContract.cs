// Decompiled with JetBrains decompiler
// Type: Microsoft.Xml.Serialization.GeneratedAssembly.XmlSerializerContract
// Assembly: PM.DataExchange.eSP.XmlSerializers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BF431D5-59B5-474D-AC31-ED3AC503BC3E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.XmlSerializers.dll

using PathMedical.DataExchange.eSP.EspWebService;
using System;
using System.Collections;
using System.Xml.Serialization;

#nullable disable
namespace Microsoft.Xml.Serialization.GeneratedAssembly;

public class XmlSerializerContract : XmlSerializerImplementation
{
  private Hashtable readMethods;
  private Hashtable writeMethods;
  private Hashtable typedSerializers;

  public override XmlSerializationReader Reader
  {
    get => (XmlSerializationReader) new XmlSerializationReaderDataUpload();
  }

  public override XmlSerializationWriter Writer
  {
    get => (XmlSerializationWriter) new XmlSerializationWriterDataUpload();
  }

  public override Hashtable ReadMethods
  {
    get
    {
      if (this.readMethods == null)
      {
        Hashtable hashtable = new Hashtable();
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.downloadSyncDataResponseDownloadSyncDataResult downloadSyncData(System.String):Response"] = (object) "Read7_downloadSyncDataResponse";
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.downloadSyncDataResponseDownloadSyncDataResult downloadSyncData(System.String):OutHeaders"] = (object) "Read8_Item";
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.uploadDataResponseUploadDataResult uploadData(System.String):Response"] = (object) "Read9_uploadDataResponse";
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.uploadDataResponseUploadDataResult uploadData(System.String):OutHeaders"] = (object) "Read10_uploadDataResponseOutHeaders";
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.validateEquipmentResponseValidateEquipmentResult validateEquipment(System.String):Response"] = (object) "Read11_validateEquipmentResponse";
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.validateEquipmentResponseValidateEquipmentResult validateEquipment(System.String):OutHeaders"] = (object) "Read12_Item";
        if (this.readMethods == null)
          this.readMethods = hashtable;
      }
      return this.readMethods;
    }
  }

  public override Hashtable WriteMethods
  {
    get
    {
      if (this.writeMethods == null)
      {
        Hashtable hashtable = new Hashtable();
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.downloadSyncDataResponseDownloadSyncDataResult downloadSyncData(System.String)"] = (object) "Write7_downloadSyncData";
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.downloadSyncDataResponseDownloadSyncDataResult downloadSyncData(System.String):InHeaders"] = (object) "Write8_downloadSyncDataInHeaders";
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.uploadDataResponseUploadDataResult uploadData(System.String)"] = (object) "Write9_uploadData";
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.uploadDataResponseUploadDataResult uploadData(System.String):InHeaders"] = (object) "Write10_uploadDataInHeaders";
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.validateEquipmentResponseValidateEquipmentResult validateEquipment(System.String)"] = (object) "Write11_validateEquipment";
        hashtable[(object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.validateEquipmentResponseValidateEquipmentResult validateEquipment(System.String):InHeaders"] = (object) "Write12_validateEquipmentInHeaders";
        if (this.writeMethods == null)
          this.writeMethods = hashtable;
      }
      return this.writeMethods;
    }
  }

  public override Hashtable TypedSerializers
  {
    get
    {
      if (this.typedSerializers == null)
      {
        Hashtable hashtable = new Hashtable();
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.downloadSyncDataResponseDownloadSyncDataResult downloadSyncData(System.String)", (object) new ArrayOfObjectSerializer());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.validateEquipmentResponseValidateEquipmentResult validateEquipment(System.String):InHeaders", (object) new ArrayOfObjectSerializer10());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.validateEquipmentResponseValidateEquipmentResult validateEquipment(System.String):Response", (object) new ArrayOfObjectSerializer9());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.downloadSyncDataResponseDownloadSyncDataResult downloadSyncData(System.String):InHeaders", (object) new ArrayOfObjectSerializer2());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.validateEquipmentResponseValidateEquipmentResult validateEquipment(System.String):OutHeaders", (object) new ArrayOfObjectSerializer11());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.uploadDataResponseUploadDataResult uploadData(System.String):Response", (object) new ArrayOfObjectSerializer5());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.downloadSyncDataResponseDownloadSyncDataResult downloadSyncData(System.String):Response", (object) new ArrayOfObjectSerializer1());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.uploadDataResponseUploadDataResult uploadData(System.String)", (object) new ArrayOfObjectSerializer4());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.uploadDataResponseUploadDataResult uploadData(System.String):InHeaders", (object) new ArrayOfObjectSerializer6());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.uploadDataResponseUploadDataResult uploadData(System.String):OutHeaders", (object) new ArrayOfObjectSerializer7());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.validateEquipmentResponseValidateEquipmentResult validateEquipment(System.String)", (object) new ArrayOfObjectSerializer8());
        hashtable.Add((object) "PathMedical.DataExchange.eSP.EspWebService.DataUpload:PathMedical.DataExchange.eSP.EspWebService.downloadSyncDataResponseDownloadSyncDataResult downloadSyncData(System.String):OutHeaders", (object) new ArrayOfObjectSerializer3());
        if (this.typedSerializers == null)
          this.typedSerializers = hashtable;
      }
      return this.typedSerializers;
    }
  }

  public override bool CanSerialize(Type type) => type == typeof (DataUpload);

  public override XmlSerializer GetSerializer(Type type) => (XmlSerializer) null;
}
