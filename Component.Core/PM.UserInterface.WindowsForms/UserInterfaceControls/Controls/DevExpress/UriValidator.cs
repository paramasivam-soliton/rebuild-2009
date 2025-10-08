// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.UriValidator
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.UserInterface.Fields;
using System;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class UriValidator : ICustomValidator
{
  public bool Validate(object value)
  {
    if (!(value is string))
      return false;
    string uriString = value as string;
    if (!Uri.IsWellFormedUriString(uriString, UriKind.Absolute))
      return false;
    Uri uri = new Uri(uriString);
    return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
  }

  public string ErrorText { get; set; }
}
