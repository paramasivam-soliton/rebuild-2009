// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.XmlHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

#nullable disable
namespace PathMedical.DataExchange;

public class XmlHelper
{
  public string LoadString(XAttribute attribute)
  {
    string empty = string.Empty;
    if (attribute != null)
      empty = attribute.Value;
    return empty;
  }

  public T LoadEnum<T>(XAttribute attribute)
  {
    if (attribute != null)
    {
      if (attribute.Value != null)
      {
        try
        {
          return (T) Enum.Parse(typeof (T), attribute.Value);
        }
        catch (ArgumentException ex)
        {
          throw ExceptionFactory.Instance.CreateException<ArgumentException>($"Failure while loading xml attribute {attribute}", (System.Exception) ex);
        }
      }
    }
    throw ExceptionFactory.Instance.CreateException<ArgumentException>($"Failure while loading xml attribute {attribute}");
  }

  public bool IsXmlDocumentValid(string xmlFilename, string schemaFilename)
  {
    bool validationSuccessfully = true;
    XmlReaderSettings settings = new XmlReaderSettings()
    {
      ValidationType = ValidationType.Schema,
      Schemas = new XmlSchemaSet()
    };
    settings.Schemas.Add((string) null, schemaFilename);
    settings.ValidationEventHandler += (ValidationEventHandler) ((o, u) => validationSuccessfully = true);
    XmlReader xmlReader = (XmlReader) null;
    try
    {
      xmlReader = XmlReader.Create(xmlFilename, settings);
      while (xmlReader.Read())
        ;
    }
    catch (XmlException ex)
    {
      validationSuccessfully = false;
    }
    catch (IOException ex)
    {
      validationSuccessfully = false;
    }
    catch (UriFormatException ex)
    {
      validationSuccessfully = false;
    }
    catch (ArgumentNullException ex)
    {
      validationSuccessfully = false;
    }
    catch (UnauthorizedAccessException ex)
    {
      validationSuccessfully = false;
    }
    finally
    {
      xmlReader?.Close();
    }
    return validationSuccessfully;
  }

  public string ValidationErrorMessage { get; protected set; }

  public bool IsXmlDocumentValid(XDocument source, XmlSchema validationSchema)
  {
    bool validationSuccessfully = true;
    this.ValidationErrorMessage = string.Empty;
    XmlSchemaSet schemas = new XmlSchemaSet();
    schemas.Add(validationSchema);
    try
    {
      source.Validate(schemas, (ValidationEventHandler) ((o, u) =>
      {
        validationSuccessfully = false;
        this.ValidationErrorMessage = $"{u.Message}{this.ValidationErrorMessage}";
      }));
    }
    catch (XmlException ex)
    {
      validationSuccessfully = false;
    }
    catch (IOException ex)
    {
      validationSuccessfully = false;
    }
    catch (UriFormatException ex)
    {
      validationSuccessfully = false;
    }
    catch (ArgumentNullException ex)
    {
      validationSuccessfully = false;
    }
    catch (UnauthorizedAccessException ex)
    {
      validationSuccessfully = false;
    }
    return validationSuccessfully;
  }

  public static XElement GetResourceEmbeddedDocument(Assembly assembly, [Localizable(false)] string name)
  {
    if (assembly == (Assembly) null || string.IsNullOrEmpty(name))
      return (XElement) null;
    Stream manifestResourceStream = assembly.GetManifestResourceStream(name);
    XElement embeddedDocument = (XElement) null;
    if (manifestResourceStream != null)
      embeddedDocument = XElement.Load(XmlReader.Create(manifestResourceStream));
    return embeddedDocument;
  }

  public static XmlSchema GetResourceEmbeddedSchema(Assembly assembly, [Localizable(false)] string name)
  {
    if (assembly == (Assembly) null || string.IsNullOrEmpty(name))
      return (XmlSchema) null;
    Stream manifestResourceStream = assembly.GetManifestResourceStream(name);
    XmlSchema resourceEmbeddedSchema = (XmlSchema) null;
    if (manifestResourceStream != null)
      resourceEmbeddedSchema = XmlSchema.Read(XmlReader.Create(manifestResourceStream), (ValidationEventHandler) null);
    return resourceEmbeddedSchema;
  }

  public static XDocument GetDocument(string xmlFileName)
  {
    XDocument document = (XDocument) null;
    XmlReaderSettings settings = new XmlReaderSettings();
    using (XmlReader reader = XmlReader.Create(xmlFileName, settings))
    {
      try
      {
        document = XDocument.Load(reader);
      }
      catch (XmlException ex)
      {
      }
      catch (IOException ex)
      {
      }
      catch (UriFormatException ex)
      {
      }
      catch (ArgumentNullException ex)
      {
      }
      catch (UnauthorizedAccessException ex)
      {
      }
    }
    return document;
  }
}
