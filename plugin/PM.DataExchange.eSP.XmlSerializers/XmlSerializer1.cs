// Decompiled with JetBrains decompiler
// Type: Microsoft.Xml.Serialization.GeneratedAssembly.XmlSerializer1
// Assembly: PM.DataExchange.eSP.XmlSerializers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3BF431D5-59B5-474D-AC31-ED3AC503BC3E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.XmlSerializers.dll

using System.Xml.Serialization;

#nullable disable
namespace Microsoft.Xml.Serialization.GeneratedAssembly;

public abstract class XmlSerializer1 : XmlSerializer
{
  protected override XmlSerializationReader CreateReader()
  {
    return (XmlSerializationReader) new XmlSerializationReaderDataUpload();
  }

  protected override XmlSerializationWriter CreateWriter()
  {
    return (XmlSerializationWriter) new XmlSerializationWriterDataUpload();
  }
}
