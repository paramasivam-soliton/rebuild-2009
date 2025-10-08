// Decompiled with JetBrains decompiler
// Type: Microsoft.Xml.Serialization.GeneratedAssembly.ArrayOfObjectSerializer11
// Assembly: PM.DataExchange.eSP.XmlSerializers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BF431D5-59B5-474D-AC31-ED3AC503BC3E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.XmlSerializers.dll

using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace Microsoft.Xml.Serialization.GeneratedAssembly;

public sealed class ArrayOfObjectSerializer11 : XmlSerializer1
{
  public override bool CanDeserialize(XmlReader xmlReader)
  {
    return xmlReader.IsStartElement("validateEquipmentResponseOutHeaders", "http://www.myapp.org");
  }

  protected override object Deserialize(XmlSerializationReader reader)
  {
    return (object) ((XmlSerializationReaderDataUpload) reader).Read12_Item();
  }
}
