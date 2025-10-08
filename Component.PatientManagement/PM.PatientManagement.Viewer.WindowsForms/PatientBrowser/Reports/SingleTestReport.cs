// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.SingleTestReport
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using PathMedical.AudiologyTest;
using PathMedical.SystemConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser;

public class SingleTestReport : XtraReport
{
  private IContainer components;
  private DetailBand patientDetails;
  private PageHeaderBand PageHeader;
  private PageFooterBand PageFooter;
  private BindingSource bindingSource1;
  private XRPanel xrPanel1;
  private XRLabel xrLabelPatientPatientRecordNumber;
  private XRLabel xrLabelGender;
  private XRLabel xrLabelPatientContactFullName;
  private XRLabel xrLabelPatientContactGender;
  private XRLabel xrLabelCaregiverContactPrimaryAddress1;
  private XRLabel xrLabelCaregiverContactPrimaryAddressCity;
  private XRLabel xrLabelCaregiverContactPrimaryAddressZip;
  private XRLabel xrLabelTestType;
  private XRLabel xrLabelEarSide;
  private XRLabel xrLabelTestDate;
  private XRLabel xrLabelTestResult;
  private Parameter TestType;
  private XRLabel xrLabelTEOAELeft;
  private XRLabel xrLabelTEOAEEarSideLeft;
  private XRLabel xrLabelTEOAETestDateLeft;
  private XRLabel xrLabelTEOAETestResultLeft;
  private XRLabel xrLabelTEOAETestResultRight;
  private XRLabel xrLabelTEOAETestDateRight;
  private XRLabel xrLabelTEOAERight;
  private XRLabel xrLabelTEOAEEarSideRight;
  private XRControlStyle xrControlStyle1;
  private XRLine xrLine1;
  private XRPictureBox xrHospitalLogo;
  private XRLabel xrLabelCareGiver;
  private XRLabel xrLabelPatient;
  private XRPanel xrPanel2;
  private XRLabel xrLabelCaregiverContactFullName;
  private XRLabel xrLabelPatientID;
  private XRLabel xrLabelPatientName;
  private XRLabel xrLabelPatientDOB;
  private XRLabel xrLabelPatientContactDateOfBirth;
  private DetailReportBand testOverview;
  private DetailBand testResultsDateils;
  private XRLabel xrLabelTestduration;
  private XRLabel xrLabelTEOAETestDurationLeft;
  private XRLabel xrLabelTEOAETestDurationRight;
  private XRLabel xrLabelExaminer;
  private XRLabel xrLabelTEOAETestExaminerLeft;
  private XRLabel xrLabelTEOAETestExaminerRight;
  private XRLabel xrLabelInsturmentID;
  private XRLabel xrLabelTEOAETestInstrumentSerialLeft;
  private XRLabel xrLabelTEOAETestInstrumentSerialRight;
  private XRLabel xrLabelProbeID;
  private XRLabel xrLabelTEOAETestProbeSerialLeft;
  private XRLabel xrLabelTEOAETestProbeSerialRight;
  private XRLabel xrLabelDPOAETestProbeSerialRight;
  private XRLabel xrLabelDPOAETestProbeSerialLeft;
  private XRLabel xrLabelDPOAETestInstrumentSerialRight;
  private XRLabel xrLabelDPOAETestInstrumentSerialLeft;
  private XRLabel xrLabelDPOAETestExaminerRight;
  private XRLabel xrLabelDPOAETestExaminerLeft;
  private XRLabel xrLabelDPOAETestDurationRight;
  private XRLabel xrLabelDPOAETestDurationLeft;
  private XRLabel xrLabelDPOAELeft;
  private XRLabel xrLabelDPOAEEarSideLeft;
  private XRLabel xrLabelDPOAETestDateLeft;
  private XRLabel xrLabelDPOAETestResultLeft;
  private XRLabel xrLabelDPOAETestResultRight;
  private XRLabel xrLabelDPOAETestDateRight;
  private XRLabel xrLabelDPOAERight;
  private XRLabel xrLabelDPOAEEarSideRight;
  private XRLabel xrLabelABRTestProbeSerialRight;
  private XRLabel xrLabelABRTestProbeSerialLeft;
  private XRLabel xrLabelABRTestInstrumentSerialRight;
  private XRLabel xrLabelABRTestInstrumentSerialLeft;
  private XRLabel xrLabelABRTestExaminerRight;
  private XRLabel xrLabelABRTestExaminerLeft;
  private XRLabel xrLabelABRTestDurationRight;
  private XRLabel xrLabelABRTestDurationLeft;
  private XRLabel xrLabelABRLeft;
  private XRLabel xrLabelABREarSideLeft;
  private XRLabel xrLabelABRTestDateLeft;
  private XRLabel xrLabelABRTestResultLeft;
  private XRLabel xrLabelABRTestResultRight;
  private XRLabel xrLabelABRTestDateRight;
  private XRLabel xrLabelABRRight;
  private XRLabel xrLabelABREarSideRight;
  private XRPageInfo xrPageInfo1;
  private XRLabel xrLabel1;
  private XRLabel xrLabelPrinted;
  private XRLabel xrLabelReportTitle;
  private XRLine xrLine2;
  private XRPictureBox xrPictureBox1;
  private DetailReportBand commentOverview;
  private DetailBand Detail;
  private XRLabel xrCommentHeader;
  private XRRichText xrPatientComments;

  public SingleTestReport()
  {
    this.InitializeComponent();
    this.RequestParameters = false;
  }

  public void LoadTestData()
  {
    if (!(this.DataSource is IEnumerable<object>))
      return;
    Patient patient = (this.DataSource as IEnumerable<object>).OfType<Patient>().FirstOrDefault<Patient>();
    if (!this.IsEmptyValue((object) patient.AudiologyTests))
    {
      switch (this.Parameters["TestType"].Value.ToString())
      {
        case "0":
          this.LoadTEOAE(patient);
          this.LoadDPOAE(patient);
          this.LoadABR(patient);
          break;
        case "2":
          this.RemoveLeftEarDPOAE();
          this.RemoveLeftEarABR();
          this.RemoveRightEarABR();
          this.RemoveRightEarDPOAE();
          this.LoadTEOAE(patient);
          break;
        case "4":
          this.RemoveLeftEarDPOAE();
          this.RemoveLeftEarTEOAE();
          this.RemoveRightEarTEOAE();
          this.RemoveRightEarDPOAE();
          this.LoadABR(patient);
          break;
        case "8":
          this.RemoveLeftEarTEOAE();
          this.RemoveLeftEarABR();
          this.RemoveRightEarABR();
          this.RemoveRightEarTEOAE();
          this.LoadDPOAE(patient);
          break;
        default:
          this.RemoveEmptyTest();
          break;
      }
    }
    else
      this.RemoveEmptyTest();
    this.LoadPatienComments(patient);
  }

  private void LoadTEOAE(Patient patient)
  {
    XRSummary xrSummary = new XRSummary();
    CultureInfo currentLanguage = SystemConfigurationManager.Instance.CurrentLanguage;
    xrSummary.FormatString = $"{{0:{currentLanguage.DateTimeFormat.FullDateTimePattern}}}";
    if (patient.OverallResultLeftEarTeoae != null)
    {
      Guid? testIdToPrintLeft = patient.OverallResultLeftEarTeoae.ReferenceToTestId;
      this.xrLabelTEOAETestDateLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintLeft;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).TestDate.ToString();
      this.xrLabelTEOAETestResultLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintLeft;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).LeftEarTestResult.ToString();
      this.xrLabelTEOAETestDurationLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintLeft;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).DurationFormatted;
      this.xrLabelTEOAETestExaminerLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintLeft;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).UserAccountId.ToString();
      this.xrLabelTEOAETestInstrumentSerialLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintLeft;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).InstrumentId.ToString();
      this.xrLabelTEOAETestProbeSerialLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintLeft;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).ProbeId.ToString();
      this.xrLabelTEOAETestDateLeft.AutoWidth = true;
      this.xrLabelTEOAEEarSideLeft.Text = "left";
      this.xrLabelTEOAELeft.Text = "TEOAE";
      this.xrLabelTEOAETestDateLeft.Summary = xrSummary;
      this.xrLine2.Visible = true;
    }
    else
      this.RemoveLeftEarTEOAE();
    if (patient.OverallResultRightEarTeoae != null)
    {
      Guid? testIdToPrintRight = patient.OverallResultRightEarTeoae.ReferenceToTestId;
      this.xrLabelTEOAETestDateRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).TestDate.ToString();
      this.xrLabelTEOAETestDateRight.AutoWidth = true;
      this.xrLabelTEOAETestResultRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).RightEarTestResult.ToString();
      this.xrLabelTEOAETestDurationRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).DurationFormatted;
      XRLabel testExaminerRight = this.xrLabelTEOAETestExaminerRight;
      Guid? nullable1 = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable2 = testIdToPrintRight;
        return nullable2.HasValue && testDetailId == nullable2.GetValueOrDefault();
      })).UserAccountId;
      string str1 = nullable1.ToString();
      testExaminerRight.Text = str1;
      XRLabel instrumentSerialRight = this.xrLabelTEOAETestInstrumentSerialRight;
      nullable1 = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable3 = testIdToPrintRight;
        return nullable3.HasValue && testDetailId == nullable3.GetValueOrDefault();
      })).InstrumentId;
      string str2 = nullable1.ToString();
      instrumentSerialRight.Text = str2;
      XRLabel probeSerialRight = this.xrLabelTEOAETestProbeSerialRight;
      nullable1 = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable4 = testIdToPrintRight;
        return nullable4.HasValue && testDetailId == nullable4.GetValueOrDefault();
      })).ProbeId;
      string str3 = nullable1.ToString();
      probeSerialRight.Text = str3;
      this.xrLabelTEOAETestDateRight.Summary = xrSummary;
      this.xrLabelTEOAEEarSideRight.Text = "right";
      this.xrLabelTEOAERight.Text = "TEOAE";
      this.xrLine2.Visible = true;
    }
    else
      this.RemoveRightEarTEOAE();
  }

  private void LoadDPOAE(Patient patient)
  {
    XRSummary xrSummary = new XRSummary();
    CultureInfo currentLanguage = SystemConfigurationManager.Instance.CurrentLanguage;
    xrSummary.FormatString = $"{{0:{currentLanguage.DateTimeFormat.FullDateTimePattern}}}";
    if (patient.OverallResultLeftEarDpoae != null)
    {
      Guid? testIdToPrintLeft = patient.OverallResultLeftEarDpoae.ReferenceToTestId;
      if (testIdToPrintLeft.HasValue)
      {
        this.xrLabelDPOAETestDateLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
        {
          Guid testDetailId = t.TestDetailId;
          Guid? nullable = testIdToPrintLeft;
          return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
        })).TestDate.ToString();
        this.xrLabelDPOAETestResultLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
        {
          Guid testDetailId = t.TestDetailId;
          Guid? nullable = testIdToPrintLeft;
          return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
        })).LeftEarTestResult.ToString();
        this.xrLabelDPOAETestDurationLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
        {
          Guid testDetailId = t.TestDetailId;
          Guid? nullable = testIdToPrintLeft;
          return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
        })).DurationFormatted;
        this.xrLabelDPOAETestExaminerLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
        {
          Guid testDetailId = t.TestDetailId;
          Guid? nullable = testIdToPrintLeft;
          return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
        })).UserAccountId.ToString();
        this.xrLabelDPOAETestDateLeft.AutoWidth = true;
        this.xrLabelDPOAEEarSideLeft.Text = "left";
        this.xrLabelDPOAELeft.Text = "DPOAE";
        this.xrLabelDPOAETestDateLeft.Summary = xrSummary;
        this.xrLine2.Visible = true;
      }
    }
    else
      this.RemoveLeftEarDPOAE();
    if (patient.OverallResultRightEarDpoae != null)
    {
      Guid? testIdToPrintRight = patient.OverallResultRightEarDpoae.ReferenceToTestId;
      this.xrLabelDPOAETestDateRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).TestDate.ToString();
      this.xrLabelDPOAETestDateRight.AutoWidth = true;
      this.xrLabelDPOAETestResultRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).RightEarTestResult.ToString();
      this.xrLabelDPOAETestDurationRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).DurationFormatted;
      this.xrLabelDPOAETestExaminerRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).UserAccountId.ToString();
      this.xrLabelDPOAETestDateRight.Summary = xrSummary;
      this.xrLabelDPOAEEarSideRight.Text = "right";
      this.xrLabelDPOAERight.Text = "DPOAE";
      this.xrLine2.Visible = true;
    }
    else
      this.RemoveRightEarDPOAE();
  }

  private void LoadABR(Patient patient)
  {
    XRSummary xrSummary = new XRSummary();
    CultureInfo currentLanguage = SystemConfigurationManager.Instance.CurrentLanguage;
    xrSummary.FormatString = $"{{0:{currentLanguage.DateTimeFormat.FullDateTimePattern}}}";
    if (patient.OverallResultLeftEarAbr != null)
    {
      Guid? testIdToPrintLeft = patient.OverallResultLeftEarAbr.ReferenceToTestId;
      this.xrLabelABRTestDateLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintLeft;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).TestDate.ToString();
      this.xrLabelABRTestResultLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintLeft;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).LeftEarTestResult.ToString();
      this.xrLabelABRTestDurationLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintLeft;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).DurationFormatted;
      this.xrLabelABRTestExaminerLeft.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintLeft;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).UserAccountId.ToString();
      this.xrLabelABRTestDateLeft.AutoWidth = true;
      this.xrLabelABREarSideLeft.Text = "left";
      this.xrLabelABRLeft.Text = "ABR";
      this.xrLabelABRTestDateLeft.Summary = xrSummary;
      this.xrLine2.Visible = true;
    }
    else
      this.RemoveLeftEarABR();
    if (patient.OverallResultRightEarAbr != null)
    {
      Guid? testIdToPrintRight = patient.OverallResultRightEarAbr.ReferenceToTestId;
      this.xrLabelABRTestDateRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).TestDate.ToString();
      this.xrLabelABRTestDateRight.AutoWidth = true;
      this.xrLabelABRTestResultRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).RightEarTestResult.ToString();
      this.xrLabelABRTestDurationRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).DurationFormatted;
      this.xrLabelABRTestExaminerRight.Text = patient.AudiologyTests.FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
      {
        Guid testDetailId = t.TestDetailId;
        Guid? nullable = testIdToPrintRight;
        return nullable.HasValue && testDetailId == nullable.GetValueOrDefault();
      })).UserAccountId.ToString();
      this.xrLabelABRTestDateRight.Summary = xrSummary;
      this.xrLabelABREarSideRight.Text = "right";
      this.xrLabelABRRight.Text = "ABR";
      this.xrLine2.Visible = true;
    }
    else
      this.RemoveRightEarABR();
  }

  private void RemoveEmptyTest()
  {
    this.RemoveLeftEarTest();
    this.RemoveRightEarTest();
  }

  private void RemoveLeftEarTest()
  {
    this.RemoveLeftEarTEOAE();
    this.RemoveLeftEarDPOAE();
    this.RemoveLeftEarABR();
  }

  private void RemoveRightEarTest()
  {
    this.RemoveRightEarTEOAE();
    this.RemoveRightEarDPOAE();
    this.RemoveRightEarABR();
  }

  private void RemoveLeftEarTEOAE()
  {
    this.xrLabelTEOAEEarSideLeft = (XRLabel) null;
    this.xrLabelTEOAETestDateLeft = (XRLabel) null;
    this.xrLabelTEOAETestResultLeft = (XRLabel) null;
    this.xrLabelTEOAELeft = (XRLabel) null;
    this.xrLabelTEOAETestDurationLeft = (XRLabel) null;
    this.xrLabelTEOAETestExaminerLeft = (XRLabel) null;
    this.xrLabelTEOAETestInstrumentSerialLeft = (XRLabel) null;
    this.xrLabelTEOAETestProbeSerialLeft = (XRLabel) null;
  }

  private void RemoveRightEarTEOAE()
  {
    this.xrLabelTEOAEEarSideRight = (XRLabel) null;
    this.xrLabelTEOAETestDateRight = (XRLabel) null;
    this.xrLabelTEOAETestResultRight = (XRLabel) null;
    this.xrLabelTEOAERight = (XRLabel) null;
    this.xrLabelTEOAETestDurationRight = (XRLabel) null;
    this.xrLabelTEOAETestExaminerRight = (XRLabel) null;
    this.xrLabelTEOAETestInstrumentSerialRight = (XRLabel) null;
    this.xrLabelTEOAETestProbeSerialRight = (XRLabel) null;
  }

  private void RemoveLeftEarDPOAE()
  {
    this.xrLabelDPOAEEarSideLeft = (XRLabel) null;
    this.xrLabelDPOAETestDateLeft = (XRLabel) null;
    this.xrLabelDPOAETestResultLeft = (XRLabel) null;
    this.xrLabelDPOAELeft = (XRLabel) null;
    this.xrLabelDPOAETestDurationLeft = (XRLabel) null;
    this.xrLabelDPOAETestExaminerLeft = (XRLabel) null;
    this.xrLabelDPOAETestInstrumentSerialLeft = (XRLabel) null;
    this.xrLabelDPOAETestProbeSerialLeft = (XRLabel) null;
  }

  private void RemoveRightEarDPOAE()
  {
    this.xrLabelDPOAEEarSideRight = (XRLabel) null;
    this.xrLabelDPOAETestDateRight = (XRLabel) null;
    this.xrLabelDPOAETestResultRight = (XRLabel) null;
    this.xrLabelDPOAERight = (XRLabel) null;
    this.xrLabelDPOAETestDurationRight = (XRLabel) null;
    this.xrLabelDPOAETestExaminerRight = (XRLabel) null;
    this.xrLabelDPOAETestInstrumentSerialRight = (XRLabel) null;
    this.xrLabelDPOAETestProbeSerialRight = (XRLabel) null;
  }

  private void RemoveLeftEarABR()
  {
    this.xrLabelABREarSideLeft = (XRLabel) null;
    this.xrLabelABRTestDateLeft = (XRLabel) null;
    this.xrLabelABRTestResultLeft = (XRLabel) null;
    this.xrLabelABRLeft = (XRLabel) null;
    this.xrLabelABRTestDurationLeft = (XRLabel) null;
    this.xrLabelABRTestExaminerLeft = (XRLabel) null;
    this.xrLabelABRTestInstrumentSerialLeft = (XRLabel) null;
    this.xrLabelABRTestProbeSerialLeft = (XRLabel) null;
  }

  private void RemoveRightEarABR()
  {
    this.xrLabelABREarSideRight = (XRLabel) null;
    this.xrLabelABRTestDateRight = (XRLabel) null;
    this.xrLabelABRTestResultRight = (XRLabel) null;
    this.xrLabelABRRight = (XRLabel) null;
    this.xrLabelABRTestDurationRight = (XRLabel) null;
    this.xrLabelABRTestExaminerRight = (XRLabel) null;
    this.xrLabelABRTestInstrumentSerialRight = (XRLabel) null;
    this.xrLabelABRTestProbeSerialRight = (XRLabel) null;
  }

  private void LoadPatienComments(Patient patient)
  {
    string[] strArray = new string[5];
    for (int index = 0; index < strArray.Length; ++index)
      strArray[index] = $"comment {index}";
    this.xrPatientComments.Lines = strArray;
  }

  private void SingleTestReport_BeforePrint(object sender, PrintEventArgs e)
  {
    this.LoadTestData();
    this.xrLabelPrinted.Text += DateTime.Now.ToString();
    this.xrLabelPrinted.Font = new Font("Arial", 11f);
    this.BeforeReportPrint();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SingleTestReport));
    this.patientDetails = new DetailBand();
    this.xrPanel1 = new XRPanel();
    this.xrLabelPatientPatientRecordNumber = new XRLabel();
    this.xrLabelGender = new XRLabel();
    this.xrLabelPatientContactFullName = new XRLabel();
    this.xrLabelPatientContactGender = new XRLabel();
    this.xrLabelPatientID = new XRLabel();
    this.xrLabelPatientName = new XRLabel();
    this.xrLabelPatientDOB = new XRLabel();
    this.xrLabelPatientContactDateOfBirth = new XRLabel();
    this.xrPanel2 = new XRPanel();
    this.xrLabelCaregiverContactPrimaryAddressZip = new XRLabel();
    this.xrLabelCaregiverContactPrimaryAddressCity = new XRLabel();
    this.xrLabelCaregiverContactPrimaryAddress1 = new XRLabel();
    this.xrLabelCaregiverContactFullName = new XRLabel();
    this.xrLabelCareGiver = new XRLabel();
    this.xrLabelPatient = new XRLabel();
    this.xrLabelReportTitle = new XRLabel();
    this.xrLabelTestType = new XRLabel();
    this.xrLabelEarSide = new XRLabel();
    this.xrLabelTestDate = new XRLabel();
    this.xrLabelTestResult = new XRLabel();
    this.xrLabelTEOAELeft = new XRLabel();
    this.xrLabelTEOAEEarSideLeft = new XRLabel();
    this.xrLabelTEOAETestDateLeft = new XRLabel();
    this.xrLabelTEOAETestResultLeft = new XRLabel();
    this.xrLabelTEOAETestResultRight = new XRLabel();
    this.xrLabelTEOAETestDateRight = new XRLabel();
    this.xrLabelTEOAERight = new XRLabel();
    this.xrLabelTEOAEEarSideRight = new XRLabel();
    this.xrLine1 = new XRLine();
    this.PageHeader = new PageHeaderBand();
    this.xrHospitalLogo = new XRPictureBox();
    this.xrPictureBox1 = new XRPictureBox();
    this.PageFooter = new PageFooterBand();
    this.xrPageInfo1 = new XRPageInfo();
    this.xrLabel1 = new XRLabel();
    this.xrLabelPrinted = new XRLabel();
    this.TestType = new Parameter();
    this.xrControlStyle1 = new XRControlStyle();
    this.testOverview = new DetailReportBand();
    this.testResultsDateils = new DetailBand();
    this.xrLabelTestduration = new XRLabel();
    this.xrLabelTEOAETestDurationLeft = new XRLabel();
    this.xrLabelTEOAETestDurationRight = new XRLabel();
    this.xrLabelExaminer = new XRLabel();
    this.xrLabelTEOAETestExaminerLeft = new XRLabel();
    this.xrLabelTEOAETestExaminerRight = new XRLabel();
    this.xrLabelInsturmentID = new XRLabel();
    this.xrLabelTEOAETestInstrumentSerialLeft = new XRLabel();
    this.xrLabelTEOAETestInstrumentSerialRight = new XRLabel();
    this.xrLabelProbeID = new XRLabel();
    this.xrLabelTEOAETestProbeSerialLeft = new XRLabel();
    this.xrLabelTEOAETestProbeSerialRight = new XRLabel();
    this.xrLabelDPOAETestProbeSerialRight = new XRLabel();
    this.xrLabelDPOAETestProbeSerialLeft = new XRLabel();
    this.xrLabelDPOAETestInstrumentSerialRight = new XRLabel();
    this.xrLabelDPOAETestInstrumentSerialLeft = new XRLabel();
    this.xrLabelDPOAETestExaminerRight = new XRLabel();
    this.xrLabelDPOAETestExaminerLeft = new XRLabel();
    this.xrLabelDPOAETestDurationRight = new XRLabel();
    this.xrLabelDPOAETestDurationLeft = new XRLabel();
    this.xrLabelDPOAELeft = new XRLabel();
    this.xrLabelDPOAEEarSideLeft = new XRLabel();
    this.xrLabelDPOAETestDateLeft = new XRLabel();
    this.xrLabelDPOAETestResultLeft = new XRLabel();
    this.xrLabelDPOAETestResultRight = new XRLabel();
    this.xrLabelDPOAETestDateRight = new XRLabel();
    this.xrLabelDPOAERight = new XRLabel();
    this.xrLabelDPOAEEarSideRight = new XRLabel();
    this.xrLabelABRTestProbeSerialRight = new XRLabel();
    this.xrLabelABRTestProbeSerialLeft = new XRLabel();
    this.xrLabelABRTestInstrumentSerialRight = new XRLabel();
    this.xrLabelABRTestInstrumentSerialLeft = new XRLabel();
    this.xrLabelABRTestExaminerRight = new XRLabel();
    this.xrLabelABRTestExaminerLeft = new XRLabel();
    this.xrLabelABRTestDurationRight = new XRLabel();
    this.xrLabelABRTestDurationLeft = new XRLabel();
    this.xrLabelABRLeft = new XRLabel();
    this.xrLabelABREarSideLeft = new XRLabel();
    this.xrLabelABRTestDateLeft = new XRLabel();
    this.xrLabelABRTestResultLeft = new XRLabel();
    this.xrLabelABRTestResultRight = new XRLabel();
    this.xrLabelABRTestDateRight = new XRLabel();
    this.xrLabelABRRight = new XRLabel();
    this.xrLabelABREarSideRight = new XRLabel();
    this.xrLine2 = new XRLine();
    this.bindingSource1 = new BindingSource(this.components);
    this.commentOverview = new DetailReportBand();
    this.Detail = new DetailBand();
    this.xrCommentHeader = new XRLabel();
    this.xrPatientComments = new XRRichText();
    ((ISupportInitialize) this.bindingSource1).BeginInit();
    this.xrPatientComments.BeginInit();
    this.BeginInit();
    this.patientDetails.Controls.AddRange(new XRControl[5]
    {
      (XRControl) this.xrPanel1,
      (XRControl) this.xrPanel2,
      (XRControl) this.xrLabelCareGiver,
      (XRControl) this.xrLabelPatient,
      (XRControl) this.xrLabelReportTitle
    });
    this.patientDetails.Height = 231;
    this.patientDetails.Name = "patientDetails";
    this.patientDetails.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
    this.patientDetails.TextAlignment = TextAlignment.TopLeft;
    this.xrPanel1.Controls.AddRange(new XRControl[8]
    {
      (XRControl) this.xrLabelPatientPatientRecordNumber,
      (XRControl) this.xrLabelGender,
      (XRControl) this.xrLabelPatientContactFullName,
      (XRControl) this.xrLabelPatientContactGender,
      (XRControl) this.xrLabelPatientID,
      (XRControl) this.xrLabelPatientName,
      (XRControl) this.xrLabelPatientDOB,
      (XRControl) this.xrLabelPatientContactDateOfBirth
    });
    this.xrPanel1.Location = new Point(8, 75);
    this.xrPanel1.Name = "xrPanel1";
    this.xrPanel1.Size = new Size(325, 150);
    this.xrLabelPatientPatientRecordNumber.DataBindings.AddRange(new XRBinding[1]
    {
      new XRBinding("Text", (object) null, "PatientRecordNumber", "")
    });
    this.xrLabelPatientPatientRecordNumber.Font = new Font("Arial", 11f, FontStyle.Bold);
    this.xrLabelPatientPatientRecordNumber.Location = new Point(117, 8);
    this.xrLabelPatientPatientRecordNumber.Name = "xrLabelPatientPatientRecordNumber";
    this.xrLabelPatientPatientRecordNumber.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPatientPatientRecordNumber.Size = new Size(199, 25);
    this.xrLabelPatientPatientRecordNumber.StylePriority.UseFont = false;
    this.xrLabelPatientPatientRecordNumber.Text = "xrLabelPatientPatientRecordNumber";
    this.xrLabelGender.Location = new Point(8, 108);
    this.xrLabelGender.Name = "xrLabelGender";
    this.xrLabelGender.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelGender.Size = new Size(101, 17);
    this.xrLabelGender.Text = "Gender:";
    this.xrLabelPatientContactFullName.DataBindings.AddRange(new XRBinding[1]
    {
      new XRBinding("Text", (object) null, "PatientContact.FullName", "")
    });
    this.xrLabelPatientContactFullName.Font = new Font("Arial", 11f, FontStyle.Bold);
    this.xrLabelPatientContactFullName.Location = new Point(117, 42);
    this.xrLabelPatientContactFullName.Name = "xrLabelPatientContactFullName";
    this.xrLabelPatientContactFullName.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPatientContactFullName.Size = new Size(199, 25);
    this.xrLabelPatientContactFullName.StylePriority.UseFont = false;
    this.xrLabelPatientContactFullName.Text = "xrLabelPatientContactFullName";
    this.xrLabelPatientContactGender.DataBindings.AddRange(new XRBinding[1]
    {
      new XRBinding("Text", (object) null, "PatientContact.Gender", "")
    });
    this.xrLabelPatientContactGender.Location = new Point(117, 108);
    this.xrLabelPatientContactGender.Name = "xrLabelPatientContactGender";
    this.xrLabelPatientContactGender.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPatientContactGender.Size = new Size(199, 25);
    this.xrLabelPatientContactGender.Text = "xrLabelPatientContactGender";
    this.xrLabelPatientID.Location = new Point(8, 8);
    this.xrLabelPatientID.Name = "xrLabelPatientID";
    this.xrLabelPatientID.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPatientID.Size = new Size(101, 25);
    this.xrLabelPatientID.Text = "Patient ID:";
    this.xrLabelPatientName.Location = new Point(8, 42);
    this.xrLabelPatientName.Name = "xrLabelPatientName";
    this.xrLabelPatientName.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPatientName.Size = new Size(101, 25);
    this.xrLabelPatientName.Text = "Patientname:";
    this.xrLabelPatientDOB.Location = new Point(8, 75);
    this.xrLabelPatientDOB.Name = "xrLabelPatientDOB";
    this.xrLabelPatientDOB.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPatientDOB.Size = new Size(101, 25);
    this.xrLabelPatientDOB.Text = "Date of Birth:";
    this.xrLabelPatientContactDateOfBirth.DataBindings.AddRange(new XRBinding[1]
    {
      new XRBinding("Text", (object) null, "PatientContact.DateOfBirth", "")
    });
    this.xrLabelPatientContactDateOfBirth.Location = new Point(117, 75);
    this.xrLabelPatientContactDateOfBirth.Name = "xrLabelPatientContactDateOfBirth";
    this.xrLabelPatientContactDateOfBirth.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPatientContactDateOfBirth.Size = new Size(199, 25);
    this.xrLabelPatientContactDateOfBirth.Text = "xrLabelPatientContactDateOfBirth";
    this.xrPanel2.Controls.AddRange(new XRControl[4]
    {
      (XRControl) this.xrLabelCaregiverContactPrimaryAddressZip,
      (XRControl) this.xrLabelCaregiverContactPrimaryAddressCity,
      (XRControl) this.xrLabelCaregiverContactPrimaryAddress1,
      (XRControl) this.xrLabelCaregiverContactFullName
    });
    this.xrPanel2.Location = new Point(342, 75);
    this.xrPanel2.Name = "xrPanel2";
    this.xrPanel2.Size = new Size(300, 117);
    this.xrLabelCaregiverContactPrimaryAddressZip.AutoWidth = true;
    this.xrLabelCaregiverContactPrimaryAddressZip.DataBindings.AddRange(new XRBinding[1]
    {
      new XRBinding("Text", (object) null, "CaregiverContact.PrimaryAddress.Zip", "")
    });
    this.xrLabelCaregiverContactPrimaryAddressZip.Location = new Point(8, 83);
    this.xrLabelCaregiverContactPrimaryAddressZip.Name = "xrLabelCaregiverContactPrimaryAddressZip";
    this.xrLabelCaregiverContactPrimaryAddressZip.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelCaregiverContactPrimaryAddressZip.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelCaregiverContactPrimaryAddressZip.Size = new Size(100, 25);
    this.xrLabelCaregiverContactPrimaryAddressZip.StylePriority.UseTextAlignment = false;
    this.xrLabelCaregiverContactPrimaryAddressZip.Text = "xrLabelCaregiverContactPrimaryAddressZip";
    this.xrLabelCaregiverContactPrimaryAddressZip.TextAlignment = TextAlignment.MiddleRight;
    this.xrLabelCaregiverContactPrimaryAddressCity.DataBindings.AddRange(new XRBinding[1]
    {
      new XRBinding("Text", (object) null, "CaregiverContact.PrimaryAddress.City", "")
    });
    this.xrLabelCaregiverContactPrimaryAddressCity.Location = new Point(125, 83);
    this.xrLabelCaregiverContactPrimaryAddressCity.Name = "xrLabelCaregiverContactPrimaryAddressCity";
    this.xrLabelCaregiverContactPrimaryAddressCity.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelCaregiverContactPrimaryAddressCity.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelCaregiverContactPrimaryAddressCity.Size = new Size(158, 25);
    this.xrLabelCaregiverContactPrimaryAddressCity.Text = "xrLabelCaregiverContactPrimaryAddressCity";
    this.xrLabelCaregiverContactPrimaryAddress1.DataBindings.AddRange(new XRBinding[1]
    {
      new XRBinding("Text", (object) null, "CaregiverContact.PrimaryAddress.Address1", "")
    });
    this.xrLabelCaregiverContactPrimaryAddress1.Location = new Point(8, 50);
    this.xrLabelCaregiverContactPrimaryAddress1.Name = "xrLabelCaregiverContactPrimaryAddress1";
    this.xrLabelCaregiverContactPrimaryAddress1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelCaregiverContactPrimaryAddress1.Size = new Size(275, 25);
    this.xrLabelCaregiverContactPrimaryAddress1.Text = "xrLabelCaregiverContactPrimaryAddress1";
    this.xrLabelCaregiverContactFullName.DataBindings.AddRange(new XRBinding[1]
    {
      new XRBinding("Text", (object) null, "CaregiverContact.FullName", "")
    });
    this.xrLabelCaregiverContactFullName.Location = new Point(8, 17);
    this.xrLabelCaregiverContactFullName.Name = "xrLabelCaregiverContactFullName";
    this.xrLabelCaregiverContactFullName.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelCaregiverContactFullName.ProcessNullValues = ValueSuppressType.Suppress;
    this.xrLabelCaregiverContactFullName.Size = new Size(275, 25);
    this.xrLabelCaregiverContactFullName.Text = "xrLabelCaregiverContactFullName";
    this.xrLabelCareGiver.Location = new Point(342, 42);
    this.xrLabelCareGiver.Name = "xrLabelCareGiver";
    this.xrLabelCareGiver.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelCareGiver.Size = new Size(100, 25);
    this.xrLabelCareGiver.Text = "Caregiver:";
    this.xrLabelPatient.Location = new Point(8, 42);
    this.xrLabelPatient.Name = "xrLabelPatient";
    this.xrLabelPatient.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPatient.Size = new Size(100, 25);
    this.xrLabelPatient.Text = "Patient:";
    this.xrLabelReportTitle.Font = new Font("Arial", 11f, FontStyle.Bold);
    this.xrLabelReportTitle.Location = new Point(8, 8);
    this.xrLabelReportTitle.Name = "xrLabelReportTitle";
    this.xrLabelReportTitle.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelReportTitle.Size = new Size(658, 25);
    this.xrLabelReportTitle.StylePriority.UseFont = false;
    this.xrLabelReportTitle.Text = "Newborn hearscreening results";
    this.xrLabelTestType.Location = new Point(8, 8);
    this.xrLabelTestType.Name = "xrLabelTestType";
    this.xrLabelTestType.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTestType.Size = new Size(42, 17);
    this.xrLabelTestType.Text = "Type";
    this.xrLabelEarSide.Location = new Point(67, 8);
    this.xrLabelEarSide.Name = "xrLabelEarSide";
    this.xrLabelEarSide.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelEarSide.Size = new Size(33, 17);
    this.xrLabelEarSide.Text = "TestObject";
    this.xrLabelTestDate.Location = new Point(133, 8);
    this.xrLabelTestDate.Name = "xrLabelTestDate";
    this.xrLabelTestDate.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTestDate.Size = new Size(75, 17);
    this.xrLabelTestDate.Text = "Date";
    this.xrLabelTestResult.Location = new Point(233, 8);
    this.xrLabelTestResult.Name = "xrLabelTestResult";
    this.xrLabelTestResult.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTestResult.Size = new Size(50, 17);
    this.xrLabelTestResult.Text = "Result";
    this.xrLabelTEOAELeft.Font = new Font("Arial", 10f);
    this.xrLabelTEOAELeft.Location = new Point(8, 33);
    this.xrLabelTEOAELeft.Name = "xrLabelTEOAELeft";
    this.xrLabelTEOAELeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAELeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAELeft.Size = new Size(55, 25);
    this.xrLabelTEOAELeft.StylePriority.UseFont = false;
    this.xrLabelTEOAELeft.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAELeft.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelTEOAEEarSideLeft.Font = new Font("Arial", 10f);
    this.xrLabelTEOAEEarSideLeft.Location = new Point(67, 33);
    this.xrLabelTEOAEEarSideLeft.Name = "xrLabelTEOAEEarSideLeft";
    this.xrLabelTEOAEEarSideLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAEEarSideLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAEEarSideLeft.Size = new Size(33, 25);
    this.xrLabelTEOAEEarSideLeft.StylePriority.UseFont = false;
    this.xrLabelTEOAEEarSideLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAEEarSideLeft.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelTEOAETestDateLeft.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestDateLeft.Location = new Point(100, 33);
    this.xrLabelTEOAETestDateLeft.Name = "xrLabelTEOAETestDateLeft";
    this.xrLabelTEOAETestDateLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestDateLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestDateLeft.Size = new Size(92, 25);
    this.xrLabelTEOAETestDateLeft.StylePriority.UseFont = false;
    this.xrLabelTEOAETestDateLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestDateLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelTEOAETestDateLeft.WordWrap = false;
    this.xrLabelTEOAETestResultLeft.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestResultLeft.Location = new Point(233, 33);
    this.xrLabelTEOAETestResultLeft.Name = "xrLabelTEOAETestResultLeft";
    this.xrLabelTEOAETestResultLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestResultLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestResultLeft.Size = new Size(72, 25);
    this.xrLabelTEOAETestResultLeft.StylePriority.UseFont = false;
    this.xrLabelTEOAETestResultLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestResultLeft.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelTEOAETestResultRight.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestResultRight.Location = new Point(233, 58);
    this.xrLabelTEOAETestResultRight.Name = "xrLabelTEOAETestResultRight";
    this.xrLabelTEOAETestResultRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestResultRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestResultRight.Size = new Size(72, 25);
    this.xrLabelTEOAETestResultRight.StylePriority.UseFont = false;
    this.xrLabelTEOAETestResultRight.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestResultRight.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelTEOAETestDateRight.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestDateRight.Location = new Point(100, 58);
    this.xrLabelTEOAETestDateRight.Name = "xrLabelTEOAETestDateRight";
    this.xrLabelTEOAETestDateRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestDateRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestDateRight.Size = new Size(92, 25);
    this.xrLabelTEOAETestDateRight.StylePriority.UseFont = false;
    this.xrLabelTEOAETestDateRight.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestDateRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelTEOAETestDateRight.WordWrap = false;
    this.xrLabelTEOAERight.Font = new Font("Arial", 10f);
    this.xrLabelTEOAERight.Location = new Point(8, 58);
    this.xrLabelTEOAERight.Name = "xrLabelTEOAERight";
    this.xrLabelTEOAERight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAERight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAERight.Size = new Size(55, 25);
    this.xrLabelTEOAERight.StylePriority.UseFont = false;
    this.xrLabelTEOAERight.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAERight.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelTEOAEEarSideRight.Font = new Font("Arial", 10f);
    this.xrLabelTEOAEEarSideRight.Location = new Point(67, 58);
    this.xrLabelTEOAEEarSideRight.Name = "xrLabelTEOAEEarSideRight";
    this.xrLabelTEOAEEarSideRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAEEarSideRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAEEarSideRight.Size = new Size(33, 25);
    this.xrLabelTEOAEEarSideRight.StylePriority.UseFont = false;
    this.xrLabelTEOAEEarSideRight.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAEEarSideRight.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLine1.Location = new Point(8, 25);
    this.xrLine1.Name = "xrLine1";
    this.xrLine1.Size = new Size(658, 2);
    this.PageHeader.Controls.AddRange(new XRControl[2]
    {
      (XRControl) this.xrHospitalLogo,
      (XRControl) this.xrPictureBox1
    });
    this.PageHeader.Height = 172;
    this.PageHeader.Name = "PageHeader";
    this.PageHeader.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
    this.PageHeader.TextAlignment = TextAlignment.TopLeft;
    this.xrHospitalLogo.Image = (Image) componentResourceManager.GetObject("xrHospitalLogo.Image");
    this.xrHospitalLogo.Location = new Point(542, 0);
    this.xrHospitalLogo.Name = "xrHospitalLogo";
    this.xrHospitalLogo.Padding = new PaddingInfo(0, 0, 2, 0, 100f);
    this.xrHospitalLogo.Size = new Size(125, 117);
    this.xrHospitalLogo.StylePriority.UsePadding = false;
    this.xrPictureBox1.Image = (Image) componentResourceManager.GetObject("xrPictureBox1.Image");
    this.xrPictureBox1.Location = new Point(458, 117);
    this.xrPictureBox1.Name = "xrPictureBox1";
    this.xrPictureBox1.Size = new Size(208 /*0xD0*/, 42);
    this.PageFooter.Controls.AddRange(new XRControl[3]
    {
      (XRControl) this.xrPageInfo1,
      (XRControl) this.xrLabel1,
      (XRControl) this.xrLabelPrinted
    });
    this.PageFooter.Height = 42;
    this.PageFooter.Name = "PageFooter";
    this.PageFooter.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
    this.PageFooter.TextAlignment = TextAlignment.TopLeft;
    this.xrPageInfo1.Location = new Point(633, 0);
    this.xrPageInfo1.Name = "xrPageInfo1";
    this.xrPageInfo1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrPageInfo1.Size = new Size(33, 17);
    this.xrLabel1.Location = new Point(583, 0);
    this.xrLabel1.Name = "xrLabel1";
    this.xrLabel1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabel1.Size = new Size(42, 17);
    this.xrLabel1.Text = "Page";
    this.xrLabelPrinted.AutoWidth = true;
    this.xrLabelPrinted.Location = new Point(8, 0);
    this.xrLabelPrinted.Name = "xrLabelPrinted";
    this.xrLabelPrinted.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPrinted.Size = new Size(242, 25);
    this.xrLabelPrinted.Text = "Printed on: ";
    this.xrLabelPrinted.WordWrap = false;
    this.TestType.Name = "TestType";
    this.TestType.Value = (object) "";
    this.xrControlStyle1.Name = "xrControlStyle1";
    this.xrControlStyle1.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
    this.testOverview.Bands.AddRange(new Band[1]
    {
      (Band) this.testResultsDateils
    });
    this.testOverview.Level = 0;
    this.testOverview.Name = "testOverview";
    this.testResultsDateils.Controls.AddRange(new XRControl[58]
    {
      (XRControl) this.xrLabelTestDate,
      (XRControl) this.xrLabelTEOAEEarSideRight,
      (XRControl) this.xrLabelTEOAERight,
      (XRControl) this.xrLabelTEOAETestDateRight,
      (XRControl) this.xrLabelTEOAETestResultRight,
      (XRControl) this.xrLabelTEOAETestResultLeft,
      (XRControl) this.xrLabelTEOAETestDateLeft,
      (XRControl) this.xrLabelTEOAEEarSideLeft,
      (XRControl) this.xrLabelTEOAELeft,
      (XRControl) this.xrLabelTestResult,
      (XRControl) this.xrLine1,
      (XRControl) this.xrLabelEarSide,
      (XRControl) this.xrLabelTestType,
      (XRControl) this.xrLabelTestduration,
      (XRControl) this.xrLabelTEOAETestDurationLeft,
      (XRControl) this.xrLabelTEOAETestDurationRight,
      (XRControl) this.xrLabelExaminer,
      (XRControl) this.xrLabelTEOAETestExaminerLeft,
      (XRControl) this.xrLabelTEOAETestExaminerRight,
      (XRControl) this.xrLabelInsturmentID,
      (XRControl) this.xrLabelTEOAETestInstrumentSerialLeft,
      (XRControl) this.xrLabelTEOAETestInstrumentSerialRight,
      (XRControl) this.xrLabelProbeID,
      (XRControl) this.xrLabelTEOAETestProbeSerialLeft,
      (XRControl) this.xrLabelTEOAETestProbeSerialRight,
      (XRControl) this.xrLabelDPOAETestProbeSerialRight,
      (XRControl) this.xrLabelDPOAETestProbeSerialLeft,
      (XRControl) this.xrLabelDPOAETestInstrumentSerialRight,
      (XRControl) this.xrLabelDPOAETestInstrumentSerialLeft,
      (XRControl) this.xrLabelDPOAETestExaminerRight,
      (XRControl) this.xrLabelDPOAETestExaminerLeft,
      (XRControl) this.xrLabelDPOAETestDurationRight,
      (XRControl) this.xrLabelDPOAETestDurationLeft,
      (XRControl) this.xrLabelDPOAELeft,
      (XRControl) this.xrLabelDPOAEEarSideLeft,
      (XRControl) this.xrLabelDPOAETestDateLeft,
      (XRControl) this.xrLabelDPOAETestResultLeft,
      (XRControl) this.xrLabelDPOAETestResultRight,
      (XRControl) this.xrLabelDPOAETestDateRight,
      (XRControl) this.xrLabelDPOAERight,
      (XRControl) this.xrLabelDPOAEEarSideRight,
      (XRControl) this.xrLabelABRTestProbeSerialRight,
      (XRControl) this.xrLabelABRTestProbeSerialLeft,
      (XRControl) this.xrLabelABRTestInstrumentSerialRight,
      (XRControl) this.xrLabelABRTestInstrumentSerialLeft,
      (XRControl) this.xrLabelABRTestExaminerRight,
      (XRControl) this.xrLabelABRTestExaminerLeft,
      (XRControl) this.xrLabelABRTestDurationRight,
      (XRControl) this.xrLabelABRTestDurationLeft,
      (XRControl) this.xrLabelABRLeft,
      (XRControl) this.xrLabelABREarSideLeft,
      (XRControl) this.xrLabelABRTestDateLeft,
      (XRControl) this.xrLabelABRTestResultLeft,
      (XRControl) this.xrLabelABRTestResultRight,
      (XRControl) this.xrLabelABRTestDateRight,
      (XRControl) this.xrLabelABRRight,
      (XRControl) this.xrLabelABREarSideRight,
      (XRControl) this.xrLine2
    });
    this.testResultsDateils.Height = 192 /*0xC0*/;
    this.testResultsDateils.Name = "testResultsDateils";
    this.xrLabelTestduration.Location = new Point(317, 8);
    this.xrLabelTestduration.Name = "xrLabelTestduration";
    this.xrLabelTestduration.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTestduration.Size = new Size(67, 17);
    this.xrLabelTestduration.Text = "Duration";
    this.xrLabelTEOAETestDurationLeft.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestDurationLeft.Location = new Point(308, 33);
    this.xrLabelTEOAETestDurationLeft.Name = "xrLabelTEOAETestDurationLeft";
    this.xrLabelTEOAETestDurationLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestDurationLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestDurationLeft.Size = new Size(67, 25);
    this.xrLabelTEOAETestDurationLeft.StylePriority.UseFont = false;
    this.xrLabelTEOAETestDurationLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestDurationLeft.TextAlignment = TextAlignment.MiddleRight;
    this.xrLabelTEOAETestDurationRight.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestDurationRight.Location = new Point(308, 58);
    this.xrLabelTEOAETestDurationRight.Name = "xrLabelTEOAETestDurationRight";
    this.xrLabelTEOAETestDurationRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestDurationRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestDurationRight.Size = new Size(67, 25);
    this.xrLabelTEOAETestDurationRight.StylePriority.UseFont = false;
    this.xrLabelTEOAETestDurationRight.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestDurationRight.TextAlignment = TextAlignment.MiddleRight;
    this.xrLabelExaminer.Location = new Point(592, 8);
    this.xrLabelExaminer.Name = "xrLabelExaminer";
    this.xrLabelExaminer.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelExaminer.Size = new Size(75, 17);
    this.xrLabelExaminer.Text = "Examiner";
    this.xrLabelTEOAETestExaminerLeft.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestExaminerLeft.Location = new Point(592, 33);
    this.xrLabelTEOAETestExaminerLeft.Name = "xrLabelTEOAETestExaminerLeft";
    this.xrLabelTEOAETestExaminerLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestExaminerLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestExaminerLeft.Size = new Size(75, 25);
    this.xrLabelTEOAETestExaminerLeft.StylePriority.UseFont = false;
    this.xrLabelTEOAETestExaminerLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestExaminerLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelTEOAETestExaminerRight.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestExaminerRight.Location = new Point(592, 58);
    this.xrLabelTEOAETestExaminerRight.Name = "xrLabelTEOAETestExaminerRight";
    this.xrLabelTEOAETestExaminerRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestExaminerRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestExaminerRight.Size = new Size(75, 25);
    this.xrLabelTEOAETestExaminerRight.StylePriority.UseFont = false;
    this.xrLabelTEOAETestExaminerRight.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestExaminerRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelInsturmentID.Location = new Point(392, 8);
    this.xrLabelInsturmentID.Name = "xrLabelInsturmentID";
    this.xrLabelInsturmentID.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelInsturmentID.Size = new Size(100, 17);
    this.xrLabelInsturmentID.Text = "Instrument";
    this.xrLabelTEOAETestInstrumentSerialLeft.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestInstrumentSerialLeft.Location = new Point(392, 33);
    this.xrLabelTEOAETestInstrumentSerialLeft.Name = "xrLabelTEOAETestInstrumentSerialLeft";
    this.xrLabelTEOAETestInstrumentSerialLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestInstrumentSerialLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestInstrumentSerialLeft.Size = new Size(100, 25);
    this.xrLabelTEOAETestInstrumentSerialLeft.StylePriority.UseFont = false;
    this.xrLabelTEOAETestInstrumentSerialLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestInstrumentSerialLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelTEOAETestInstrumentSerialRight.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestInstrumentSerialRight.Location = new Point(392, 58);
    this.xrLabelTEOAETestInstrumentSerialRight.Name = "xrLabelTEOAETestInstrumentSerialRight";
    this.xrLabelTEOAETestInstrumentSerialRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestInstrumentSerialRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestInstrumentSerialRight.Size = new Size(100, 25);
    this.xrLabelTEOAETestInstrumentSerialRight.StylePriority.UseFont = false;
    this.xrLabelTEOAETestInstrumentSerialRight.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestInstrumentSerialRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelProbeID.Location = new Point(508, 8);
    this.xrLabelProbeID.Name = "xrLabelProbeID";
    this.xrLabelProbeID.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelProbeID.Size = new Size(75, 17);
    this.xrLabelProbeID.Text = "Probe";
    this.xrLabelTEOAETestProbeSerialLeft.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestProbeSerialLeft.Location = new Point(508, 33);
    this.xrLabelTEOAETestProbeSerialLeft.Name = "xrLabelTEOAETestProbeSerialLeft";
    this.xrLabelTEOAETestProbeSerialLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestProbeSerialLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestProbeSerialLeft.Size = new Size(75, 25);
    this.xrLabelTEOAETestProbeSerialLeft.StylePriority.UseFont = false;
    this.xrLabelTEOAETestProbeSerialLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestProbeSerialLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelTEOAETestProbeSerialRight.Font = new Font("Arial", 10f);
    this.xrLabelTEOAETestProbeSerialRight.Location = new Point(508, 58);
    this.xrLabelTEOAETestProbeSerialRight.Name = "xrLabelTEOAETestProbeSerialRight";
    this.xrLabelTEOAETestProbeSerialRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelTEOAETestProbeSerialRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelTEOAETestProbeSerialRight.Size = new Size(75, 25);
    this.xrLabelTEOAETestProbeSerialRight.StylePriority.UseFont = false;
    this.xrLabelTEOAETestProbeSerialRight.StylePriority.UseTextAlignment = false;
    this.xrLabelTEOAETestProbeSerialRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelDPOAETestProbeSerialRight.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestProbeSerialRight.Location = new Point(508, 108);
    this.xrLabelDPOAETestProbeSerialRight.Name = "xrLabelDPOAETestProbeSerialRight";
    this.xrLabelDPOAETestProbeSerialRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestProbeSerialRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestProbeSerialRight.Size = new Size(75, 25);
    this.xrLabelDPOAETestProbeSerialRight.StylePriority.UseFont = false;
    this.xrLabelDPOAETestProbeSerialRight.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestProbeSerialRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelDPOAETestProbeSerialLeft.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestProbeSerialLeft.Location = new Point(508, 83);
    this.xrLabelDPOAETestProbeSerialLeft.Name = "xrLabelDPOAETestProbeSerialLeft";
    this.xrLabelDPOAETestProbeSerialLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestProbeSerialLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestProbeSerialLeft.Size = new Size(75, 25);
    this.xrLabelDPOAETestProbeSerialLeft.StylePriority.UseFont = false;
    this.xrLabelDPOAETestProbeSerialLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestProbeSerialLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelDPOAETestInstrumentSerialRight.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestInstrumentSerialRight.Location = new Point(392, 108);
    this.xrLabelDPOAETestInstrumentSerialRight.Name = "xrLabelDPOAETestInstrumentSerialRight";
    this.xrLabelDPOAETestInstrumentSerialRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestInstrumentSerialRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestInstrumentSerialRight.Size = new Size(100, 25);
    this.xrLabelDPOAETestInstrumentSerialRight.StylePriority.UseFont = false;
    this.xrLabelDPOAETestInstrumentSerialRight.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestInstrumentSerialRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelDPOAETestInstrumentSerialLeft.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestInstrumentSerialLeft.Location = new Point(392, 83);
    this.xrLabelDPOAETestInstrumentSerialLeft.Name = "xrLabelDPOAETestInstrumentSerialLeft";
    this.xrLabelDPOAETestInstrumentSerialLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestInstrumentSerialLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestInstrumentSerialLeft.Size = new Size(100, 25);
    this.xrLabelDPOAETestInstrumentSerialLeft.StylePriority.UseFont = false;
    this.xrLabelDPOAETestInstrumentSerialLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestInstrumentSerialLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelDPOAETestExaminerRight.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestExaminerRight.Location = new Point(592, 108);
    this.xrLabelDPOAETestExaminerRight.Name = "xrLabelDPOAETestExaminerRight";
    this.xrLabelDPOAETestExaminerRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestExaminerRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestExaminerRight.Size = new Size(75, 25);
    this.xrLabelDPOAETestExaminerRight.StylePriority.UseFont = false;
    this.xrLabelDPOAETestExaminerRight.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestExaminerRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelDPOAETestExaminerLeft.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestExaminerLeft.Location = new Point(592, 83);
    this.xrLabelDPOAETestExaminerLeft.Name = "xrLabelDPOAETestExaminerLeft";
    this.xrLabelDPOAETestExaminerLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestExaminerLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestExaminerLeft.Size = new Size(75, 25);
    this.xrLabelDPOAETestExaminerLeft.StylePriority.UseFont = false;
    this.xrLabelDPOAETestExaminerLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestExaminerLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelDPOAETestDurationRight.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestDurationRight.Location = new Point(308, 108);
    this.xrLabelDPOAETestDurationRight.Name = "xrLabelDPOAETestDurationRight";
    this.xrLabelDPOAETestDurationRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestDurationRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestDurationRight.Size = new Size(67, 25);
    this.xrLabelDPOAETestDurationRight.StylePriority.UseFont = false;
    this.xrLabelDPOAETestDurationRight.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestDurationRight.TextAlignment = TextAlignment.MiddleRight;
    this.xrLabelDPOAETestDurationLeft.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestDurationLeft.Location = new Point(308, 83);
    this.xrLabelDPOAETestDurationLeft.Name = "xrLabelDPOAETestDurationLeft";
    this.xrLabelDPOAETestDurationLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestDurationLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestDurationLeft.Size = new Size(67, 25);
    this.xrLabelDPOAETestDurationLeft.StylePriority.UseFont = false;
    this.xrLabelDPOAETestDurationLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestDurationLeft.TextAlignment = TextAlignment.MiddleRight;
    this.xrLabelDPOAELeft.Font = new Font("Arial", 10f);
    this.xrLabelDPOAELeft.Location = new Point(8, 83);
    this.xrLabelDPOAELeft.Name = "xrLabelDPOAELeft";
    this.xrLabelDPOAELeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAELeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAELeft.Size = new Size(55, 25);
    this.xrLabelDPOAELeft.StylePriority.UseFont = false;
    this.xrLabelDPOAELeft.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAELeft.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelDPOAEEarSideLeft.Font = new Font("Arial", 10f);
    this.xrLabelDPOAEEarSideLeft.Location = new Point(67, 83);
    this.xrLabelDPOAEEarSideLeft.Name = "xrLabelDPOAEEarSideLeft";
    this.xrLabelDPOAEEarSideLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAEEarSideLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAEEarSideLeft.Size = new Size(33, 25);
    this.xrLabelDPOAEEarSideLeft.StylePriority.UseFont = false;
    this.xrLabelDPOAEEarSideLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAEEarSideLeft.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelDPOAETestDateLeft.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestDateLeft.Location = new Point(100, 83);
    this.xrLabelDPOAETestDateLeft.Name = "xrLabelDPOAETestDateLeft";
    this.xrLabelDPOAETestDateLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestDateLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestDateLeft.Size = new Size(92, 25);
    this.xrLabelDPOAETestDateLeft.StylePriority.UseFont = false;
    this.xrLabelDPOAETestDateLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestDateLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelDPOAETestDateLeft.WordWrap = false;
    this.xrLabelDPOAETestResultLeft.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestResultLeft.Location = new Point(233, 83);
    this.xrLabelDPOAETestResultLeft.Name = "xrLabelDPOAETestResultLeft";
    this.xrLabelDPOAETestResultLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestResultLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestResultLeft.Size = new Size(72, 25);
    this.xrLabelDPOAETestResultLeft.StylePriority.UseFont = false;
    this.xrLabelDPOAETestResultLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestResultLeft.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelDPOAETestResultRight.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestResultRight.Location = new Point(233, 108);
    this.xrLabelDPOAETestResultRight.Name = "xrLabelDPOAETestResultRight";
    this.xrLabelDPOAETestResultRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestResultRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestResultRight.Size = new Size(72, 25);
    this.xrLabelDPOAETestResultRight.StylePriority.UseFont = false;
    this.xrLabelDPOAETestResultRight.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestResultRight.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelDPOAETestDateRight.Font = new Font("Arial", 10f);
    this.xrLabelDPOAETestDateRight.Location = new Point(100, 108);
    this.xrLabelDPOAETestDateRight.Name = "xrLabelDPOAETestDateRight";
    this.xrLabelDPOAETestDateRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAETestDateRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAETestDateRight.Size = new Size(92, 25);
    this.xrLabelDPOAETestDateRight.StylePriority.UseFont = false;
    this.xrLabelDPOAETestDateRight.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAETestDateRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelDPOAETestDateRight.WordWrap = false;
    this.xrLabelDPOAERight.Font = new Font("Arial", 10f);
    this.xrLabelDPOAERight.Location = new Point(8, 108);
    this.xrLabelDPOAERight.Name = "xrLabelDPOAERight";
    this.xrLabelDPOAERight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAERight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAERight.Size = new Size(55, 25);
    this.xrLabelDPOAERight.StylePriority.UseFont = false;
    this.xrLabelDPOAERight.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAERight.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelDPOAEEarSideRight.Font = new Font("Arial", 10f);
    this.xrLabelDPOAEEarSideRight.Location = new Point(67, 108);
    this.xrLabelDPOAEEarSideRight.Name = "xrLabelDPOAEEarSideRight";
    this.xrLabelDPOAEEarSideRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelDPOAEEarSideRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelDPOAEEarSideRight.Size = new Size(33, 25);
    this.xrLabelDPOAEEarSideRight.StylePriority.UseFont = false;
    this.xrLabelDPOAEEarSideRight.StylePriority.UseTextAlignment = false;
    this.xrLabelDPOAEEarSideRight.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelABRTestProbeSerialRight.Font = new Font("Arial", 10f);
    this.xrLabelABRTestProbeSerialRight.Location = new Point(508, 158);
    this.xrLabelABRTestProbeSerialRight.Name = "xrLabelABRTestProbeSerialRight";
    this.xrLabelABRTestProbeSerialRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestProbeSerialRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestProbeSerialRight.Size = new Size(75, 25);
    this.xrLabelABRTestProbeSerialRight.StylePriority.UseFont = false;
    this.xrLabelABRTestProbeSerialRight.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestProbeSerialRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelABRTestProbeSerialLeft.Font = new Font("Arial", 10f);
    this.xrLabelABRTestProbeSerialLeft.Location = new Point(508, 133);
    this.xrLabelABRTestProbeSerialLeft.Name = "xrLabelABRTestProbeSerialLeft";
    this.xrLabelABRTestProbeSerialLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestProbeSerialLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestProbeSerialLeft.Size = new Size(75, 25);
    this.xrLabelABRTestProbeSerialLeft.StylePriority.UseFont = false;
    this.xrLabelABRTestProbeSerialLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestProbeSerialLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelABRTestInstrumentSerialRight.Font = new Font("Arial", 10f);
    this.xrLabelABRTestInstrumentSerialRight.Location = new Point(392, 158);
    this.xrLabelABRTestInstrumentSerialRight.Name = "xrLabelABRTestInstrumentSerialRight";
    this.xrLabelABRTestInstrumentSerialRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestInstrumentSerialRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestInstrumentSerialRight.Size = new Size(100, 25);
    this.xrLabelABRTestInstrumentSerialRight.StylePriority.UseFont = false;
    this.xrLabelABRTestInstrumentSerialRight.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestInstrumentSerialRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelABRTestInstrumentSerialLeft.Font = new Font("Arial", 10f);
    this.xrLabelABRTestInstrumentSerialLeft.Location = new Point(392, 133);
    this.xrLabelABRTestInstrumentSerialLeft.Name = "xrLabelABRTestInstrumentSerialLeft";
    this.xrLabelABRTestInstrumentSerialLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestInstrumentSerialLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestInstrumentSerialLeft.Size = new Size(100, 25);
    this.xrLabelABRTestInstrumentSerialLeft.StylePriority.UseFont = false;
    this.xrLabelABRTestInstrumentSerialLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestInstrumentSerialLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelABRTestExaminerRight.Font = new Font("Arial", 10f);
    this.xrLabelABRTestExaminerRight.Location = new Point(592, 158);
    this.xrLabelABRTestExaminerRight.Name = "xrLabelABRTestExaminerRight";
    this.xrLabelABRTestExaminerRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestExaminerRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestExaminerRight.Size = new Size(75, 25);
    this.xrLabelABRTestExaminerRight.StylePriority.UseFont = false;
    this.xrLabelABRTestExaminerRight.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestExaminerRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelABRTestExaminerLeft.Font = new Font("Arial", 10f);
    this.xrLabelABRTestExaminerLeft.Location = new Point(592, 133);
    this.xrLabelABRTestExaminerLeft.Name = "xrLabelABRTestExaminerLeft";
    this.xrLabelABRTestExaminerLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestExaminerLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestExaminerLeft.Size = new Size(75, 25);
    this.xrLabelABRTestExaminerLeft.StylePriority.UseFont = false;
    this.xrLabelABRTestExaminerLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestExaminerLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelABRTestDurationRight.Font = new Font("Arial", 10f);
    this.xrLabelABRTestDurationRight.Location = new Point(308, 158);
    this.xrLabelABRTestDurationRight.Name = "xrLabelABRTestDurationRight";
    this.xrLabelABRTestDurationRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestDurationRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestDurationRight.Size = new Size(67, 25);
    this.xrLabelABRTestDurationRight.StylePriority.UseFont = false;
    this.xrLabelABRTestDurationRight.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestDurationRight.TextAlignment = TextAlignment.MiddleRight;
    this.xrLabelABRTestDurationLeft.Font = new Font("Arial", 10f);
    this.xrLabelABRTestDurationLeft.Location = new Point(308, 133);
    this.xrLabelABRTestDurationLeft.Name = "xrLabelABRTestDurationLeft";
    this.xrLabelABRTestDurationLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestDurationLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestDurationLeft.Size = new Size(67, 25);
    this.xrLabelABRTestDurationLeft.StylePriority.UseFont = false;
    this.xrLabelABRTestDurationLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestDurationLeft.TextAlignment = TextAlignment.MiddleRight;
    this.xrLabelABRLeft.Font = new Font("Arial", 10f);
    this.xrLabelABRLeft.Location = new Point(8, 133);
    this.xrLabelABRLeft.Name = "xrLabelABRLeft";
    this.xrLabelABRLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRLeft.Size = new Size(55, 25);
    this.xrLabelABRLeft.StylePriority.UseFont = false;
    this.xrLabelABRLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelABRLeft.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelABREarSideLeft.Font = new Font("Arial", 10f);
    this.xrLabelABREarSideLeft.Location = new Point(67, 133);
    this.xrLabelABREarSideLeft.Name = "xrLabelABREarSideLeft";
    this.xrLabelABREarSideLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABREarSideLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABREarSideLeft.Size = new Size(33, 25);
    this.xrLabelABREarSideLeft.StylePriority.UseFont = false;
    this.xrLabelABREarSideLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelABREarSideLeft.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelABRTestDateLeft.Font = new Font("Arial", 10f);
    this.xrLabelABRTestDateLeft.Location = new Point(100, 133);
    this.xrLabelABRTestDateLeft.Name = "xrLabelABRTestDateLeft";
    this.xrLabelABRTestDateLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestDateLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestDateLeft.Size = new Size(92, 25);
    this.xrLabelABRTestDateLeft.StylePriority.UseFont = false;
    this.xrLabelABRTestDateLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestDateLeft.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelABRTestDateLeft.WordWrap = false;
    this.xrLabelABRTestResultLeft.Font = new Font("Arial", 10f);
    this.xrLabelABRTestResultLeft.Location = new Point(233, 133);
    this.xrLabelABRTestResultLeft.Name = "xrLabelABRTestResultLeft";
    this.xrLabelABRTestResultLeft.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestResultLeft.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestResultLeft.Size = new Size(72, 25);
    this.xrLabelABRTestResultLeft.StylePriority.UseFont = false;
    this.xrLabelABRTestResultLeft.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestResultLeft.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelABRTestResultRight.Font = new Font("Arial", 10f);
    this.xrLabelABRTestResultRight.Location = new Point(233, 158);
    this.xrLabelABRTestResultRight.Name = "xrLabelABRTestResultRight";
    this.xrLabelABRTestResultRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestResultRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestResultRight.Size = new Size(72, 25);
    this.xrLabelABRTestResultRight.StylePriority.UseFont = false;
    this.xrLabelABRTestResultRight.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestResultRight.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelABRTestDateRight.Font = new Font("Arial", 10f);
    this.xrLabelABRTestDateRight.Location = new Point(100, 158);
    this.xrLabelABRTestDateRight.Name = "xrLabelABRTestDateRight";
    this.xrLabelABRTestDateRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRTestDateRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRTestDateRight.Size = new Size(92, 25);
    this.xrLabelABRTestDateRight.StylePriority.UseFont = false;
    this.xrLabelABRTestDateRight.StylePriority.UseTextAlignment = false;
    this.xrLabelABRTestDateRight.TextAlignment = TextAlignment.MiddleCenter;
    this.xrLabelABRTestDateRight.WordWrap = false;
    this.xrLabelABRRight.Font = new Font("Arial", 10f);
    this.xrLabelABRRight.Location = new Point(8, 158);
    this.xrLabelABRRight.Name = "xrLabelABRRight";
    this.xrLabelABRRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABRRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABRRight.Size = new Size(55, 25);
    this.xrLabelABRRight.StylePriority.UseFont = false;
    this.xrLabelABRRight.StylePriority.UseTextAlignment = false;
    this.xrLabelABRRight.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLabelABREarSideRight.Font = new Font("Arial", 10f);
    this.xrLabelABREarSideRight.Location = new Point(67, 158);
    this.xrLabelABREarSideRight.Name = "xrLabelABREarSideRight";
    this.xrLabelABREarSideRight.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelABREarSideRight.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    this.xrLabelABREarSideRight.Size = new Size(33, 25);
    this.xrLabelABREarSideRight.StylePriority.UseFont = false;
    this.xrLabelABREarSideRight.StylePriority.UseTextAlignment = false;
    this.xrLabelABREarSideRight.TextAlignment = TextAlignment.MiddleLeft;
    this.xrLine2.Location = new Point(8, 183);
    this.xrLine2.Name = "xrLine2";
    this.xrLine2.Size = new Size(658, 2);
    this.xrLine2.Visible = false;
    this.bindingSource1.DataSource = (object) typeof (Patient);
    this.commentOverview.Bands.AddRange(new Band[1]
    {
      (Band) this.Detail
    });
    this.commentOverview.Level = 1;
    this.commentOverview.Name = "commentOverview";
    this.Detail.Controls.AddRange(new XRControl[2]
    {
      (XRControl) this.xrCommentHeader,
      (XRControl) this.xrPatientComments
    });
    this.Detail.Name = "Detail";
    this.xrCommentHeader.Location = new Point(8, 8);
    this.xrCommentHeader.Name = "xrCommentHeader";
    this.xrCommentHeader.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrCommentHeader.Size = new Size(133, 17);
    this.xrCommentHeader.Text = "Patient comments:";
    this.xrPatientComments.Location = new Point(8, 33);
    this.xrPatientComments.Name = "xrPatientComments";
    this.xrPatientComments.SerializableRtfString = componentResourceManager.GetString("xrPatientComments.SerializableRtfString");
    this.xrPatientComments.Size = new Size(658, 58);
    this.Bands.AddRange(new Band[5]
    {
      (Band) this.patientDetails,
      (Band) this.PageHeader,
      (Band) this.PageFooter,
      (Band) this.testOverview,
      (Band) this.commentOverview
    });
    this.DataSource = (object) this.bindingSource1;
    this.Font = new Font("Arial", 11f);
    this.Margins = new Margins(100, 76, 60, 28);
    this.Parameters.AddRange(new Parameter[1]
    {
      this.TestType
    });
    this.StyleSheet.AddRange(new XRControlStyle[1]
    {
      this.xrControlStyle1
    });
    this.Version = "9.2";
    this.BeforePrint += new PrintEventHandler(this.SingleTestReport_BeforePrint);
    ((ISupportInitialize) this.bindingSource1).EndInit();
    this.xrPatientComments.EndInit();
    this.EndInit();
  }
}
