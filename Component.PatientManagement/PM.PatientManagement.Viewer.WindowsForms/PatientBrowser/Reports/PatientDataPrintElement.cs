// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.Reports.PatientDataPrintElement
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.AudiologyTest;
using System.Collections.Generic;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.Reports;

internal class PatientDataPrintElement
{
  public Patient Patient { get; set; }

  public List<AudiologyTestInformation> BestAudiologyTests { get; set; }

  public List<ITestDetailSubReport> DetailSubReports { get; set; }

  public List<PathMedical.PatientManagement.CommentManagement.Comment> Comment { get; set; }

  public string Facility { get; set; }

  public string Location { get; set; }
}
