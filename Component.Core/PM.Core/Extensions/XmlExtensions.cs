// Decompiled with JetBrains decompiler
// Type: PathMedical.Extensions.XmlExtensions
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Xml.Linq;

#nullable disable
namespace PathMedical.Extensions;

public static class XmlExtensions
{
  public static XElement SafeElement(this XContainer container, string name)
  {
    return container.Element((XName) name) ?? new XElement((XName) name);
  }

  public static XAttribute SafeAttribute(this XElement element, string name)
  {
    return element.Attribute((XName) name) ?? new XAttribute((XName) name, (object) "");
  }
}
