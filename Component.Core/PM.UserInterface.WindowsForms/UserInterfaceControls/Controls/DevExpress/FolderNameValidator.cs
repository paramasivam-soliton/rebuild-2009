// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.FolderNameValidator
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.UserInterface.Fields;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class FolderNameValidator : ICustomValidator
{
  private readonly Regex folderNameExpression;

  public FolderNameValidator()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("^((");
    stringBuilder.Append("([a-zA-Z]:)");
    stringBuilder.Append("(\\\\([\\w ]*))");
    stringBuilder.Append("|(\\\\{2}[a-zA-Z0-9\\-\\._]*)");
    stringBuilder.Append("(\\\\(\\w[\\w ]*))");
    stringBuilder.Append("))");
    this.folderNameExpression = new Regex(stringBuilder.ToString());
  }

  public bool Validate(object value)
  {
    if (!(value is string))
      return true;
    string input = value as string;
    return string.IsNullOrEmpty(input) || this.folderNameExpression.IsMatch(input);
  }

  public string ErrorText { get; set; }
}
