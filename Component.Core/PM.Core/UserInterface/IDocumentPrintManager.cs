// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.IDocumentPrintManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.UserInterface;

public interface IDocumentPrintManager
{
  object DataSource { get; set; }

  List<PrintParameter> PrintParameters { get; set; }

  void ShowPreviewDialog(Type reportType);

  void ShowRibbonDesigner(Type reportType);

  void ShowDesigner(Type reportType);

  void ShowPrintDialog(Type reportType);

  void Print(Type reportType);
}
