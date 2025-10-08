// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ResourceManager.ResourcesResourceSet
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.Exception;
using PathMedical.ResourceManager;
using System;
using System.Globalization;
using System.Resources;
using System.Text;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.ResourceManager;

public class ResourcesResourceSet : CustomResourceSet
{
  private string baseNameField;
  private CultureInfo cultureInfo;
  private string extension;

  public ResourcesResourceSet(string baseNameField, CultureInfo cultureInfo, string extension)
    : base((IResourceReader) new ResourcesResourceReader(baseNameField, cultureInfo, extension))
  {
    this.baseNameField = baseNameField;
    this.cultureInfo = cultureInfo;
    this.extension = extension;
  }

  public override Type GetDefaultReader() => typeof (ResourcesResourceReader);

  public override Type GetDefaultWriter()
  {
    if (this.extension == "resx")
      return typeof (ResXResourceWriter);
    if (this.extension == "resources")
      return typeof (ResourceWriter);
    throw ExceptionFactory.Instance.CreateException<ArgumentException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "The file has an unsupported suffix {0}.", (object) this.extension));
  }

  public override IResourceReader CreateDefaultReader()
  {
    return (IResourceReader) new ResourcesResourceReader(this.baseNameField, this.cultureInfo, this.extension);
  }

  protected virtual string GetResourceFileName()
  {
    StringBuilder stringBuilder = new StringBuilder(this.baseNameField);
    stringBuilder.Append(".");
    if (!this.cultureInfo.Equals((object) CultureInfo.InvariantCulture))
    {
      stringBuilder.Append(this.cultureInfo.Name);
      stringBuilder.Append(".");
    }
    stringBuilder.Append(this.extension);
    return stringBuilder.ToString();
  }

  public override IResourceWriter CreateDefaultWriter()
  {
    if (this.extension.Equals("resx", StringComparison.InvariantCultureIgnoreCase))
      return (IResourceWriter) new ResXResourceWriter(this.GetResourceFileName());
    if (this.extension.Equals("resources", StringComparison.InvariantCultureIgnoreCase))
      return (IResourceWriter) new ResourceWriter(this.GetResourceFileName());
    throw ExceptionFactory.Instance.CreateException<ArgumentException>(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "The file has an unsupported suffix {0}.", (object) this.extension));
  }
}
