// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.DocumentPrintingManager
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using PathMedical.Exception;
using System;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceManagement;

public class DocumentPrintingManager : IDocumentPrintManager
{
  public static DocumentPrintingManager Instance => PathMedical.Singleton.Singleton<DocumentPrintingManager>.Instance;

  private DocumentPrintingManager() => this.PrintParameters = new List<PrintParameter>();

  public object DataSource { get; set; }

  public List<PrintParameter> PrintParameters { get; set; }

  public void ShowPreviewDialog(Type reportType)
  {
    XtraReport xtraReport = !(reportType == (Type) null) ? this.CreateReport(reportType) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (reportType));
    if (xtraReport == null)
      return;
    xtraReport.DataSource = this.DataSource;
    xtraReport.Parameters.AddRange(DocumentPrintingManager.ToDevExpressParameters(this.PrintParameters));
    xtraReport.ShowPreviewDialog();
  }

  public void ShowPrintDialog(Type reportType)
  {
    XtraReport xtraReport = !(reportType == (Type) null) ? this.CreateReport(reportType) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (reportType));
    if (xtraReport == null)
      return;
    xtraReport.DataSource = this.DataSource;
    xtraReport.Parameters.AddRange(DocumentPrintingManager.ToDevExpressParameters(this.PrintParameters));
    int num = (int) xtraReport.PrintDialog();
  }

  public void ShowRibbonDesigner(Type reportType)
  {
    XtraReport xtraReport = !(reportType == (Type) null) ? this.CreateReport(reportType) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (reportType));
    if (xtraReport == null)
      return;
    xtraReport.DataSource = this.DataSource;
    xtraReport.ShowRibbonDesigner();
  }

  public void ShowDesigner(Type reportType)
  {
    XtraReport xtraReport = !(reportType == (Type) null) ? this.CreateReport(reportType) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (reportType));
    if (xtraReport == null)
      return;
    xtraReport.DataSource = this.DataSource;
    xtraReport.ShowDesigner();
  }

  public void Print(Type reportType)
  {
    XtraReport xtraReport = !(reportType == (Type) null) ? this.CreateReport(reportType) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (reportType));
    if (xtraReport == null)
      return;
    xtraReport.DataSource = this.DataSource;
    xtraReport.Print();
  }

  private static Parameter[] ToDevExpressParameters(List<PrintParameter> printParameters)
  {
    List<Parameter> parameterList = new List<Parameter>();
    foreach (PrintParameter printParameter in printParameters)
    {
      Parameter parameter = new Parameter()
      {
        Name = printParameter.Name,
        Description = printParameter.Description,
        Value = printParameter.Value
      };
      switch (printParameter.Type)
      {
        case TypeCode.Boolean:
          parameter.ParameterType = ParameterType.Boolean;
          break;
        case TypeCode.Int32:
          parameter.ParameterType = ParameterType.Int32;
          break;
        case TypeCode.Single:
          parameter.ParameterType = ParameterType.Float;
          break;
        case TypeCode.Double:
          parameter.ParameterType = ParameterType.Double;
          break;
        case TypeCode.Decimal:
          parameter.ParameterType = ParameterType.Decimal;
          break;
        case TypeCode.DateTime:
          parameter.ParameterType = ParameterType.DateTime;
          break;
        case TypeCode.String:
          parameter.ParameterType = ParameterType.String;
          break;
      }
      parameterList.Add(parameter);
    }
    return parameterList.ToArray();
  }

  private XtraReport CreateReport(Type reportType)
  {
    return reportType != (Type) null ? Activator.CreateInstance(reportType) as XtraReport : (XtraReport) null;
  }
}
