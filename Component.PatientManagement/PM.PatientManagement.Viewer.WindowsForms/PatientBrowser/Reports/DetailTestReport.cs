// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.Reports.DetailTestReport
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.XtraPrinting;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using PathMedical.AudiologyTest;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.Plugin;
using PathMedical.SiteAndFacilityManagement;
using PathMedical.SystemConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.Reports;

public class DetailTestReport : XtraReport
{
  private XRSummary dateFormat;
  private IContainer components;
  private DetailBand patientDetails;
  private PageHeaderBand PageHeader;
  private PageFooterBand PageFooter;
  private XRPanel xrPanel1;
  private XRLabel xrLabelPatientPatientRecordNumber;
  private XRLabel xrLabelGender;
  private XRLabel xrLabelPatientContactFullName;
  private XRLabel xrLabelPatientContactGender;
  private XRLabel xrLabelCaregiverContactPrimaryAddress1;
  private XRLabel xrLabelCaregiverContactPrimaryAddressCity;
  private Parameter TestType;
  private XRControlStyle xrControlStyle1;
  private XRPanel xrPanel2;
  private XRLabel xrLabelCaregiverContactFullName;
  private XRLabel xrLabelPatientID;
  private XRLabel xrLabelPatientName;
  private XRLabel xrLabelPatientDOB;
  private XRLabel xrLabelPatientContactDateOfBirth;
  private DetailReportBand testResultOverview;
  private DetailBand testResultOverviewDetails;
  private XRPageInfo xrPageInfo1;
  private XRLabel page;
  private XRLabel xrLabelPrinted;
  private XRLabel xrLabelReportTitle;
  private XRPictureBox xrPictureBox1;
  private XRTable bestTestOverviewTable;
  private XRTableRow bestTestOverviewDetailRow;
  private XRTableCell bestTestOverviewTestType;
  private XRTableCell bestTestOverviewTestEar;
  private XRTableCell bestTestOverviewTestDateAndTime;
  private XRTableCell bestTestOverviewTestResult;
  private XRTableCell bestTestOverviewDuration;
  private XRTableCell bestTestOverviewInstrument;
  private XRTableCell bestTestOverviewProbe;
  private XRTableCell bestTestOverviewExaminer;
  private FormattingRule formattingRule1;
  private GroupHeaderBand testResultOverviewHeader;
  private XRTable xrTable1;
  private XRTableRow xrTableRow1;
  private XRTableCell xrTableCell1;
  private XRTableCell xrTableCell2;
  private XRTableCell xrTableCell3;
  private XRTableCell xrTableCell4;
  private XRTableCell xrTableCell5;
  private XRTableCell xrTableCell6;
  private XRTableCell xrTableCell7;
  private XRTableCell xrTableCell8;
  private GroupFooterBand testResultOverviewFooter;
  private XRTable xrTable2;
  private XRTableRow bestTestOverviewHeaderRow;
  private XRTableCell bestTestOverviewTestTypeTitle;
  private XRTableCell bestTestOverviewTestEarTitle;
  private XRTableCell bestTestOverviewTestDateTitle;
  private XRTableCell bestTestOverviewTestResultTitle;
  private XRTableCell bestTestOverviewDurationTitle;
  private XRTableCell bestTestOverviewInstrumentTitle;
  private XRTableCell bestTestOverviewProbeTitle;
  private XRTableCell bestTestOverviewExaminerTitle;
  private TopMarginBand topMarginBand1;
  private BottomMarginBand bottomMarginBand1;
  private XRLabel xrLabelScreeningFacilityContent;
  private XRLabel xrLabelScreeningFacility;
  private XRLabel xrLabelPhysician;
  private XRLabel xrLabelPhysicianContent;
  private XRLabel xrLabelNicu;
  private XRLabel xrLabelNicuStatus;
  private DetailReportBand patientCommmentOverview;
  private DetailBand patientCommentDetails;
  private GroupHeaderBand patientCommentHeader;
  private XRTable xrTableCommentHeader;
  private XRTableRow xrTableRow2;
  private XRTableCell xrTableCellCommentCreatedHeader;
  private XRTableCell xrTableCellCommentHeader;
  private XRTableCell xrTableCellCreatedByHeader;
  private XRLabel xrLabelComments;
  private XRTable xrTable3;
  private XRTableRow xrTableRow3;
  private XRTableCell xrTableCellCommentCreated;
  private XRTableCell xrTableCellComment;
  private XRTableCell xrTableCellCommenCreatedBy;
  private XRLabel locationContent;
  private XRLabel location;
  private XRTableCell xrTableCell10;
  private XRTableCell xrTableCell9;

  public DetailTestReport()
  {
    this.InitializeComponent();
    this.RequestParameters = false;
    DateTimeFormatInfo currentDateTimeFormat = SystemConfigurationManager.Instance.CurrentDateTimeFormat;
    if (currentDateTimeFormat != null)
      this.dateFormat = new XRSummary()
      {
        FormatString = $"{{0:{currentDateTimeFormat.FullDateTimePattern}}}"
      };
    ImageConverter imageConverter = new ImageConverter();
    Image image = (Image) null;
    string configurationValue1 = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("ABA643CC-82CB-42be-A0A5-A3966B927554"), "ReportPicture");
    if (!string.IsNullOrEmpty(configurationValue1))
    {
      image = (Image) imageConverter.ConvertFrom((object) Convert.FromBase64String(configurationValue1));
    }
    else
    {
      string configurationValue2 = SystemConfigurationManager.Instance.GetSystemConfigurationValue(new Guid("8B5B064B-EC5F-4524-96DD-0E88BD3FA952"), "DefaultReportPicture");
      if (!string.IsNullOrEmpty(configurationValue2))
        image = (Image) imageConverter.ConvertFrom((object) Convert.FromBase64String(configurationValue2));
    }
    if (image != null)
    {
      this.xrPictureBox1.Size = new Size(image.Width + 15, image.Height + 10);
      this.xrPictureBox1.Location = new Point(this.PageHeader.Right - this.xrPictureBox1.Size.Width - 5, 5);
      this.xrPictureBox1.Image = image;
      this.xrPictureBox1.Sizing = ImageSizeMode.Normal;
    }
    else
      this.xrPictureBox1.Image = (Image) null;
  }

  private void DetailTestReport_BeforePrint(object sender, PrintEventArgs e)
  {
    if (this.DataSource is Patient)
    {
      Patient dataSource1 = this.DataSource as Patient;
      this.DataSource = (object) new List<object>()
      {
        (object) dataSource1
      };
      PathMedical.AudiologyTest.TestType[] array1 = this.Parameters.Where<Parameter>((Func<Parameter, bool>) (p => p.Name.Equals("TestType"))).Select<Parameter, PathMedical.AudiologyTest.TestType>((Func<Parameter, PathMedical.AudiologyTest.TestType>) (p => (PathMedical.AudiologyTest.TestType) p.Value)).ToArray<PathMedical.AudiologyTest.TestType>();
      ReportFormat[] array2 = this.Parameters.Where<Parameter>((Func<Parameter, bool>) (p => p.Name.Equals("ReportFormat"))).Select<Parameter, ReportFormat>((Func<Parameter, ReportFormat>) (p => (ReportFormat) p.Value)).ToArray<ReportFormat>();
      List<PatientDataPrintElement> dataSource2 = new List<PatientDataPrintElement>();
      PatientDataPrintElement dataPrintElement = new PatientDataPrintElement()
      {
        Patient = dataSource1,
        BestAudiologyTests = this.LoadTestData(dataSource1, array1),
        Comment = DetailTestReport.LoadComments(dataSource1)
      };
      if (dataPrintElement.BestAudiologyTests != null)
      {
        AudiologyTestInformation bestAudiologyTest = dataPrintElement.BestAudiologyTests.FirstOrDefault<AudiologyTestInformation>();
        if (bestAudiologyTest != null)
        {
          if (dataSource1.FreeText1 == null)
            dataPrintElement.Patient.FreeText1 = Resources.DetailTestReport_PhysicianUnknown;
          FacilityManager.Instance.RefreshData();
          LocationTypeManager.Instance.RefreshData();
          dataPrintElement.Facility = FacilityManager.Instance.Facilities.Where<Facility>((Func<Facility, bool>) (f =>
          {
            Guid id = f.Id;
            Guid? facilityId = bestAudiologyTest.FacilityId;
            return facilityId.HasValue && id == facilityId.GetValueOrDefault();
          })).Select<Facility, string>((Func<Facility, string>) (f => f.Name)).FirstOrDefault<string>();
          dataPrintElement.Location = LocationTypeManager.Instance.LocationTypes.Where<LocationType>((Func<LocationType, bool>) (l =>
          {
            Guid id = l.Id;
            Guid? facilityLocationId = bestAudiologyTest.FacilityLocationId;
            return facilityLocationId.HasValue && id == facilityLocationId.GetValueOrDefault();
          })).Select<LocationType, string>((Func<LocationType, string>) (l => l.Name)).FirstOrDefault<string>();
          dataPrintElement.DetailSubReports = this.LoadDetailSubReports((IEnumerable<AudiologyTestInformation>) dataPrintElement.BestAudiologyTests);
          dataSource2.Add(dataPrintElement);
          bool bindMotherContact = dataSource1.CaregiverContact != null;
          bool bindCaregiverContact = dataSource1.CaregiverContact != null && dataSource1.CaregiverContact.PrimaryAddress != null && !string.IsNullOrEmpty(dataSource1.CaregiverContact.PrimaryAddress.Address1);
          this.BindPatientData(dataSource2, bindMotherContact, bindCaregiverContact);
          if (dataPrintElement.Patient.Comments.Count<Comment>() > 0)
          {
            this.BindPatientComments(dataSource2);
          }
          else
          {
            this.xrLabelComments.Visible = false;
            this.xrTableCommentHeader.Visible = false;
          }
          this.BindBestTestResults(dataSource2);
          if (array2[0] == ReportFormat.Detail)
            this.BindDetailSubReports(dataSource2);
        }
      }
    }
    this.xrLabelPrinted.Text += DateTime.Now.ToString();
    this.xrLabelPrinted.Font = new Font("Arial", 11f);
    this.BeforeReportPrint();
  }

  private void BindPatientData(
    List<PatientDataPrintElement> dataSource,
    bool bindMotherContact,
    bool bindCaregiverContact)
  {
    XRBinding binding1 = new XRBinding("Text", (object) dataSource, "Patient.PatientRecordNumber");
    XRBinding binding2 = new XRBinding("Text", (object) dataSource, "Patient.PatientContact.FullName");
    XRBinding binding3 = new XRBinding("Text", (object) dataSource, "Patient.PatientContact.DateOfBirth", "{0:d}");
    XRBinding binding4 = new XRBinding("Text", (object) dataSource, "Patient.PatientContact.Gender");
    XRBinding binding5 = new XRBinding("Text", (object) dataSource, "Patient.NicuStatus");
    XRBinding binding6 = new XRBinding("Text", (object) dataSource, "Patient.FreeText1");
    XRBinding binding7 = new XRBinding("Text", (object) dataSource, "facility");
    XRBinding binding8 = new XRBinding("Text", (object) dataSource, "location");
    this.xrLabelPatientPatientRecordNumber.DataBindings.Add(binding1);
    this.xrLabelPatientContactFullName.DataBindings.Add(binding2);
    this.xrLabelPatientContactDateOfBirth.DataBindings.Add(binding3);
    this.xrLabelPatientContactGender.DataBindings.Add(binding4);
    this.xrLabelNicuStatus.DataBindings.Add(binding5);
    this.xrLabelPhysicianContent.DataBindings.Add(binding6);
    this.xrLabelScreeningFacilityContent.DataBindings.Add(binding7);
    this.locationContent.DataBindings.Add(binding8);
    if (!bindCaregiverContact & bindMotherContact)
    {
      XRBinding binding9 = new XRBinding("Text", (object) dataSource, "Patient.MotherContact.FullName");
      XRBinding binding10 = new XRBinding("Text", (object) dataSource, "Patient.MotherContact.PrimaryAddress.Address1");
      XRBinding binding11 = new XRBinding("Text", (object) dataSource, "Patient.MotherContact.PrimaryAddress.ZipCity");
      this.xrLabelCaregiverContactFullName.DataBindings.Add(binding9);
      this.xrLabelCaregiverContactPrimaryAddress1.DataBindings.Add(binding10);
      this.xrLabelCaregiverContactPrimaryAddressCity.DataBindings.Add(binding11);
    }
    else
    {
      if (!bindCaregiverContact)
        return;
      XRBinding binding12 = new XRBinding("Text", (object) dataSource, "Patient.CaregiverContact.FullName");
      XRBinding binding13 = new XRBinding("Text", (object) dataSource, "Patient.CaregiverContact.PrimaryAddress.Address1");
      XRBinding binding14 = new XRBinding("Text", (object) dataSource, "Patient.CaregiverContact.PrimaryAddress.ZipCity");
      this.xrLabelCaregiverContactFullName.DataBindings.Add(binding12);
      this.xrLabelCaregiverContactPrimaryAddress1.DataBindings.Add(binding13);
      this.xrLabelCaregiverContactPrimaryAddressCity.DataBindings.Add(binding14);
    }
  }

  private void BindBestTestResults(List<PatientDataPrintElement> dataSource)
  {
    XRBinding binding1 = new XRBinding("Text", (object) dataSource, "BestAudiologyTests.TestType");
    XRBinding binding2 = new XRBinding("Text", (object) dataSource, "BestAudiologyTests.TestObjectName");
    XRBinding binding3 = new XRBinding("Text", (object) dataSource, "BestAudiologyTests.TestDate");
    XRBinding binding4 = new XRBinding("Text", (object) dataSource, "BestAudiologyTests.TestResultName");
    XRBinding binding5 = new XRBinding("Text", (object) dataSource, "BestAudiologyTests.DurationFormatted");
    XRBinding binding6 = new XRBinding("Text", (object) dataSource, "BestAudiologyTests.InstrumentSerial");
    XRBinding binding7 = new XRBinding("Text", (object) dataSource, "BestAudiologyTests.DetailInformation.ProbeSerialNumber");
    XRBinding binding8 = new XRBinding("Text", (object) dataSource, "BestAudiologyTests.Examiner");
    this.testResultOverview.DataSource = (object) dataSource;
    this.testResultOverview.DataMember = "BestAudiologyTests";
    this.bestTestOverviewTestType.DataBindings.Add(binding1);
    this.bestTestOverviewTestEar.DataBindings.Add(binding2);
    this.bestTestOverviewTestDateAndTime.DataBindings.Add(binding3);
    this.bestTestOverviewTestResult.DataBindings.Add(binding4);
    this.bestTestOverviewDuration.DataBindings.Add(binding5);
    this.bestTestOverviewInstrument.DataBindings.Add(binding6);
    this.bestTestOverviewProbe.DataBindings.Add(binding7);
    this.bestTestOverviewExaminer.DataBindings.Add(binding8);
  }

  private void BindPatientComments(List<PatientDataPrintElement> dataSource)
  {
    XRBinding binding1 = new XRBinding("Text", (object) dataSource, "Comment.CreationDate");
    XRBinding binding2 = new XRBinding("Text", (object) dataSource, "Comment.Text");
    XRBinding binding3 = new XRBinding("Text", (object) dataSource, "Comment.Examiner");
    this.patientCommmentOverview.DataSource = (object) dataSource;
    this.patientCommmentOverview.DataMember = "Comment";
    this.xrTableCellCommentCreated.DataBindings.Add(binding1);
    this.xrTableCellComment.DataBindings.Add(binding2);
    this.xrTableCellCommenCreatedBy.DataBindings.Add(binding3);
  }

  private static List<Comment> LoadComments(Patient patient) => patient.Comments.ToList<Comment>();

  private void BindDetailSubReports(List<PatientDataPrintElement> dataSource)
  {
    foreach (ITestDetailSubReport detailSubReport in dataSource[0].DetailSubReports)
    {
      DetailBand child1 = new DetailBand();
      DetailReportBand detailReportBand = new DetailReportBand();
      XRSubreport xrSubreport = new XRSubreport();
      xrSubreport.ReportSource = detailSubReport as XtraReport;
      XRSubreport child2 = xrSubreport;
      child1.Controls.Add((XRControl) child2);
      child1.Controls.Add((XRControl) new XRPageBreak());
      detailReportBand.Controls.Add((XRControl) child1);
      this.Bands.Add((Band) detailReportBand);
    }
  }

  private List<AudiologyTestInformation> LoadTestData(Patient patient, params PathMedical.AudiologyTest.TestType[] testTypes)
  {
    List<AudiologyTestInformation> audiologyTestInformationList = new List<AudiologyTestInformation>();
    foreach (PathMedical.AudiologyTest.TestType testType in testTypes)
    {
      AudiologyTestInformation audiologyTestResult1 = PatientManager.Instance.GetBestAudiologyTestResult(patient, testType, TestObject.LeftEar);
      if (audiologyTestResult1 != null)
        audiologyTestInformationList.Add(audiologyTestResult1);
      AudiologyTestInformation audiologyTestResult2 = PatientManager.Instance.GetBestAudiologyTestResult(patient, testType, TestObject.RightEar);
      if (audiologyTestResult2 != null)
        audiologyTestInformationList.Add(audiologyTestResult2);
    }
    return audiologyTestInformationList;
  }

  private List<ITestDetailSubReport> LoadDetailSubReports(
    IEnumerable<AudiologyTestInformation> audiologyTestInformations)
  {
    List<ITestDetailSubReport> testDetailSubReportList = new List<ITestDetailSubReport>();
    ITestPlugin[] array = SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().ToArray<ITestPlugin>();
    foreach (AudiologyTestInformation audiologyTestInformation in audiologyTestInformations)
    {
      AudiologyTestInformation information = audiologyTestInformation;
      ITestPlugin testPlugin = ((IEnumerable<ITestPlugin>) array).FirstOrDefault<ITestPlugin>((Func<ITestPlugin, bool>) (p =>
      {
        Guid testTypeSignature1 = p.TestTypeSignature;
        Guid? testTypeSignature2 = information.TestTypeSignature;
        return testTypeSignature2.HasValue && testTypeSignature1 == testTypeSignature2.GetValueOrDefault();
      }));
      if (testPlugin != null)
      {
        Type detailReportType = testPlugin.DetailReportType;
        if (detailReportType != (Type) null)
        {
          object instance = Activator.CreateInstance(detailReportType);
          if (instance is ITestDetailSubReport)
          {
            ITestDetailSubReport testDetailSubReport = instance as ITestDetailSubReport;
            testDetailSubReport.TestId = new Guid?(audiologyTestInformation.TestDetailId);
            testDetailSubReportList.Add(testDetailSubReport);
          }
        }
      }
    }
    return testDetailSubReportList;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DetailTestReport));
    this.patientDetails = new DetailBand();
    this.locationContent = new XRLabel();
    this.location = new XRLabel();
    this.xrLabelPhysician = new XRLabel();
    this.xrLabelScreeningFacilityContent = new XRLabel();
    this.xrLabelScreeningFacility = new XRLabel();
    this.xrPanel1 = new XRPanel();
    this.xrLabelNicuStatus = new XRLabel();
    this.xrLabelNicu = new XRLabel();
    this.xrLabelPatientPatientRecordNumber = new XRLabel();
    this.xrLabelGender = new XRLabel();
    this.xrLabelPatientContactFullName = new XRLabel();
    this.xrLabelPatientContactGender = new XRLabel();
    this.xrLabelPatientID = new XRLabel();
    this.xrLabelPatientName = new XRLabel();
    this.xrLabelPatientDOB = new XRLabel();
    this.xrLabelPatientContactDateOfBirth = new XRLabel();
    this.xrPanel2 = new XRPanel();
    this.xrLabelCaregiverContactPrimaryAddressCity = new XRLabel();
    this.xrLabelCaregiverContactPrimaryAddress1 = new XRLabel();
    this.xrLabelCaregiverContactFullName = new XRLabel();
    this.xrLabelReportTitle = new XRLabel();
    this.xrLabelPhysicianContent = new XRLabel();
    this.PageHeader = new PageHeaderBand();
    this.xrPictureBox1 = new XRPictureBox();
    this.PageFooter = new PageFooterBand();
    this.xrPageInfo1 = new XRPageInfo();
    this.page = new XRLabel();
    this.xrLabelPrinted = new XRLabel();
    this.xrControlStyle1 = new XRControlStyle();
    this.testResultOverview = new DetailReportBand();
    this.testResultOverviewDetails = new DetailBand();
    this.bestTestOverviewTable = new XRTable();
    this.bestTestOverviewDetailRow = new XRTableRow();
    this.bestTestOverviewTestType = new XRTableCell();
    this.bestTestOverviewTestEar = new XRTableCell();
    this.bestTestOverviewTestDateAndTime = new XRTableCell();
    this.xrTableCell10 = new XRTableCell();
    this.bestTestOverviewTestResult = new XRTableCell();
    this.bestTestOverviewDuration = new XRTableCell();
    this.bestTestOverviewInstrument = new XRTableCell();
    this.bestTestOverviewProbe = new XRTableCell();
    this.bestTestOverviewExaminer = new XRTableCell();
    this.testResultOverviewHeader = new GroupHeaderBand();
    this.xrTable1 = new XRTable();
    this.xrTableRow1 = new XRTableRow();
    this.xrTableCell1 = new XRTableCell();
    this.xrTableCell2 = new XRTableCell();
    this.xrTableCell3 = new XRTableCell();
    this.xrTableCell9 = new XRTableCell();
    this.xrTableCell4 = new XRTableCell();
    this.xrTableCell5 = new XRTableCell();
    this.xrTableCell6 = new XRTableCell();
    this.xrTableCell7 = new XRTableCell();
    this.xrTableCell8 = new XRTableCell();
    this.testResultOverviewFooter = new GroupFooterBand();
    this.xrTable2 = new XRTable();
    this.bestTestOverviewHeaderRow = new XRTableRow();
    this.bestTestOverviewTestTypeTitle = new XRTableCell();
    this.bestTestOverviewTestEarTitle = new XRTableCell();
    this.bestTestOverviewTestDateTitle = new XRTableCell();
    this.bestTestOverviewTestResultTitle = new XRTableCell();
    this.bestTestOverviewDurationTitle = new XRTableCell();
    this.bestTestOverviewInstrumentTitle = new XRTableCell();
    this.bestTestOverviewProbeTitle = new XRTableCell();
    this.bestTestOverviewExaminerTitle = new XRTableCell();
    this.formattingRule1 = new FormattingRule();
    this.topMarginBand1 = new TopMarginBand();
    this.bottomMarginBand1 = new BottomMarginBand();
    this.patientCommmentOverview = new DetailReportBand();
    this.patientCommentDetails = new DetailBand();
    this.xrTable3 = new XRTable();
    this.xrTableRow3 = new XRTableRow();
    this.xrTableCellCommentCreated = new XRTableCell();
    this.xrTableCellComment = new XRTableCell();
    this.xrTableCellCommenCreatedBy = new XRTableCell();
    this.patientCommentHeader = new GroupHeaderBand();
    this.xrTableCommentHeader = new XRTable();
    this.xrTableRow2 = new XRTableRow();
    this.xrTableCellCommentCreatedHeader = new XRTableCell();
    this.xrTableCellCommentHeader = new XRTableCell();
    this.xrTableCellCreatedByHeader = new XRTableCell();
    this.xrLabelComments = new XRLabel();
    this.bestTestOverviewTable.BeginInit();
    this.xrTable1.BeginInit();
    this.xrTable2.BeginInit();
    this.xrTable3.BeginInit();
    this.xrTableCommentHeader.BeginInit();
    this.BeginInit();
    this.patientDetails.Controls.AddRange(new XRControl[9]
    {
      (XRControl) this.locationContent,
      (XRControl) this.location,
      (XRControl) this.xrLabelPhysician,
      (XRControl) this.xrLabelScreeningFacilityContent,
      (XRControl) this.xrLabelScreeningFacility,
      (XRControl) this.xrPanel1,
      (XRControl) this.xrPanel2,
      (XRControl) this.xrLabelReportTitle,
      (XRControl) this.xrLabelPhysicianContent
    });
    this.patientDetails.HeightF = 352f;
    this.patientDetails.Name = "patientDetails";
    this.patientDetails.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.patientDetails, "patientDetails");
    componentResourceManager.ApplyResources((object) this.locationContent, "locationContent");
    this.locationContent.Name = "locationContent";
    this.locationContent.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.locationContent.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.location, "location");
    this.location.Name = "location";
    this.location.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.xrLabelPhysician, "xrLabelPhysician");
    this.xrLabelPhysician.Name = "xrLabelPhysician";
    this.xrLabelPhysician.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.xrLabelScreeningFacilityContent, "xrLabelScreeningFacilityContent");
    this.xrLabelScreeningFacilityContent.Name = "xrLabelScreeningFacilityContent";
    this.xrLabelScreeningFacilityContent.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelScreeningFacilityContent.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrLabelScreeningFacility, "xrLabelScreeningFacility");
    this.xrLabelScreeningFacility.Name = "xrLabelScreeningFacility";
    this.xrLabelScreeningFacility.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrPanel1.Controls.AddRange(new XRControl[10]
    {
      (XRControl) this.xrLabelNicuStatus,
      (XRControl) this.xrLabelNicu,
      (XRControl) this.xrLabelPatientPatientRecordNumber,
      (XRControl) this.xrLabelGender,
      (XRControl) this.xrLabelPatientContactFullName,
      (XRControl) this.xrLabelPatientContactGender,
      (XRControl) this.xrLabelPatientID,
      (XRControl) this.xrLabelPatientName,
      (XRControl) this.xrLabelPatientDOB,
      (XRControl) this.xrLabelPatientContactDateOfBirth
    });
    componentResourceManager.ApplyResources((object) this.xrPanel1, "xrPanel1");
    this.xrPanel1.Name = "xrPanel1";
    componentResourceManager.ApplyResources((object) this.xrLabelNicuStatus, "xrLabelNicuStatus");
    this.xrLabelNicuStatus.Name = "xrLabelNicuStatus";
    this.xrLabelNicuStatus.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.xrLabelNicu, "xrLabelNicu");
    this.xrLabelNicu.Name = "xrLabelNicu";
    this.xrLabelNicu.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.xrLabelPatientPatientRecordNumber, "xrLabelPatientPatientRecordNumber");
    this.xrLabelPatientPatientRecordNumber.Name = "xrLabelPatientPatientRecordNumber";
    this.xrLabelPatientPatientRecordNumber.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPatientPatientRecordNumber.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.xrLabelGender, "xrLabelGender");
    this.xrLabelGender.Name = "xrLabelGender";
    this.xrLabelGender.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.xrLabelPatientContactFullName, "xrLabelPatientContactFullName");
    this.xrLabelPatientContactFullName.Name = "xrLabelPatientContactFullName";
    this.xrLabelPatientContactFullName.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPatientContactFullName.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.xrLabelPatientContactGender, "xrLabelPatientContactGender");
    this.xrLabelPatientContactGender.Name = "xrLabelPatientContactGender";
    this.xrLabelPatientContactGender.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.xrLabelPatientID, "xrLabelPatientID");
    this.xrLabelPatientID.Name = "xrLabelPatientID";
    this.xrLabelPatientID.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.xrLabelPatientName, "xrLabelPatientName");
    this.xrLabelPatientName.Name = "xrLabelPatientName";
    this.xrLabelPatientName.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.xrLabelPatientDOB, "xrLabelPatientDOB");
    this.xrLabelPatientDOB.Name = "xrLabelPatientDOB";
    this.xrLabelPatientDOB.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.xrLabelPatientContactDateOfBirth, "xrLabelPatientContactDateOfBirth");
    this.xrLabelPatientContactDateOfBirth.Name = "xrLabelPatientContactDateOfBirth";
    this.xrLabelPatientContactDateOfBirth.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrPanel2.Controls.AddRange(new XRControl[3]
    {
      (XRControl) this.xrLabelCaregiverContactPrimaryAddressCity,
      (XRControl) this.xrLabelCaregiverContactPrimaryAddress1,
      (XRControl) this.xrLabelCaregiverContactFullName
    });
    componentResourceManager.ApplyResources((object) this.xrPanel2, "xrPanel2");
    this.xrPanel2.Name = "xrPanel2";
    componentResourceManager.ApplyResources((object) this.xrLabelCaregiverContactPrimaryAddressCity, "xrLabelCaregiverContactPrimaryAddressCity");
    this.xrLabelCaregiverContactPrimaryAddressCity.Name = "xrLabelCaregiverContactPrimaryAddressCity";
    this.xrLabelCaregiverContactPrimaryAddressCity.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelCaregiverContactPrimaryAddressCity.ProcessNullValues = ValueSuppressType.SuppressAndShrink;
    componentResourceManager.ApplyResources((object) this.xrLabelCaregiverContactPrimaryAddress1, "xrLabelCaregiverContactPrimaryAddress1");
    this.xrLabelCaregiverContactPrimaryAddress1.Name = "xrLabelCaregiverContactPrimaryAddress1";
    this.xrLabelCaregiverContactPrimaryAddress1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.xrLabelCaregiverContactFullName, "xrLabelCaregiverContactFullName");
    this.xrLabelCaregiverContactFullName.Name = "xrLabelCaregiverContactFullName";
    this.xrLabelCaregiverContactFullName.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelCaregiverContactFullName.ProcessNullValues = ValueSuppressType.Suppress;
    componentResourceManager.ApplyResources((object) this.xrLabelReportTitle, "xrLabelReportTitle");
    this.xrLabelReportTitle.Name = "xrLabelReportTitle";
    this.xrLabelReportTitle.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelReportTitle.StylePriority.UseFont = false;
    componentResourceManager.ApplyResources((object) this.xrLabelPhysicianContent, "xrLabelPhysicianContent");
    this.xrLabelPhysicianContent.Name = "xrLabelPhysicianContent";
    this.xrLabelPhysicianContent.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.PageHeader.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrPictureBox1
    });
    this.PageHeader.HeightF = 51f;
    this.PageHeader.Name = "PageHeader";
    this.PageHeader.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.PageHeader, "PageHeader");
    this.xrPictureBox1.Image = (Image) componentResourceManager.GetObject("xrPictureBox1.Image");
    componentResourceManager.ApplyResources((object) this.xrPictureBox1, "xrPictureBox1");
    this.xrPictureBox1.Name = "xrPictureBox1";
    this.PageFooter.Controls.AddRange(new XRControl[3]
    {
      (XRControl) this.xrPageInfo1,
      (XRControl) this.page,
      (XRControl) this.xrLabelPrinted
    });
    this.PageFooter.HeightF = 25f;
    this.PageFooter.Name = "PageFooter";
    this.PageFooter.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.PageFooter, "PageFooter");
    componentResourceManager.ApplyResources((object) this.xrPageInfo1, "xrPageInfo1");
    this.xrPageInfo1.Name = "xrPageInfo1";
    this.xrPageInfo1.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    componentResourceManager.ApplyResources((object) this.page, "page");
    this.page.Name = "page";
    this.page.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPrinted.AutoWidth = true;
    componentResourceManager.ApplyResources((object) this.xrLabelPrinted, "xrLabelPrinted");
    this.xrLabelPrinted.Name = "xrLabelPrinted";
    this.xrLabelPrinted.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelPrinted.WordWrap = false;
    this.xrControlStyle1.Name = "xrControlStyle1";
    this.xrControlStyle1.Padding = new PaddingInfo(0, 0, 0, 0, 100f);
    this.testResultOverview.Bands.AddRange(new Band[3]
    {
      (Band) this.testResultOverviewDetails,
      (Band) this.testResultOverviewHeader,
      (Band) this.testResultOverviewFooter
    });
    this.testResultOverview.Level = 0;
    this.testResultOverview.Name = "testResultOverview";
    componentResourceManager.ApplyResources((object) this.testResultOverview, "testResultOverview");
    this.testResultOverviewDetails.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.bestTestOverviewTable
    });
    this.testResultOverviewDetails.HeightF = 28f;
    this.testResultOverviewDetails.Name = "testResultOverviewDetails";
    componentResourceManager.ApplyResources((object) this.testResultOverviewDetails, "testResultOverviewDetails");
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTable, "bestTestOverviewTable");
    this.bestTestOverviewTable.Name = "bestTestOverviewTable";
    this.bestTestOverviewTable.Rows.AddRange(new XRTableRow[1]
    {
      this.bestTestOverviewDetailRow
    });
    this.bestTestOverviewTable.StylePriority.UseFont = false;
    this.bestTestOverviewDetailRow.Cells.AddRange(new XRTableCell[9]
    {
      this.bestTestOverviewTestType,
      this.bestTestOverviewTestEar,
      this.bestTestOverviewTestDateAndTime,
      this.xrTableCell10,
      this.bestTestOverviewTestResult,
      this.bestTestOverviewDuration,
      this.bestTestOverviewInstrument,
      this.bestTestOverviewProbe,
      this.bestTestOverviewExaminer
    });
    this.bestTestOverviewDetailRow.Name = "bestTestOverviewDetailRow";
    componentResourceManager.ApplyResources((object) this.bestTestOverviewDetailRow, "bestTestOverviewDetailRow");
    this.bestTestOverviewDetailRow.Weight = 1.0;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestType, "bestTestOverviewTestType");
    this.bestTestOverviewTestType.Name = "bestTestOverviewTestType";
    this.bestTestOverviewTestType.StylePriority.UseFont = false;
    this.bestTestOverviewTestType.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewTestType.Weight = 101.0 / 329.0;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestEar, "bestTestOverviewTestEar");
    this.bestTestOverviewTestEar.Name = "bestTestOverviewTestEar";
    this.bestTestOverviewTestEar.StylePriority.UseFont = false;
    this.bestTestOverviewTestEar.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewTestEar.Weight = 0.21300905881480406;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestDateAndTime, "bestTestOverviewTestDateAndTime");
    this.bestTestOverviewTestDateAndTime.Name = "bestTestOverviewTestDateAndTime";
    this.bestTestOverviewTestDateAndTime.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.bestTestOverviewTestDateAndTime.StylePriority.UseFont = false;
    this.bestTestOverviewTestDateAndTime.StylePriority.UsePadding = false;
    this.bestTestOverviewTestDateAndTime.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewTestDateAndTime.Weight = 0.58835511689087872;
    this.xrTableCell10.Name = "xrTableCell10";
    componentResourceManager.ApplyResources((object) this.xrTableCell10, "xrTableCell10");
    this.xrTableCell10.Weight = 0.064814738744177258;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestResult, "bestTestOverviewTestResult");
    this.bestTestOverviewTestResult.Name = "bestTestOverviewTestResult";
    this.bestTestOverviewTestResult.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.bestTestOverviewTestResult.StylePriority.UseFont = false;
    this.bestTestOverviewTestResult.StylePriority.UsePadding = false;
    this.bestTestOverviewTestResult.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewTestResult.Weight = 0.36819967386489855;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewDuration, "bestTestOverviewDuration");
    this.bestTestOverviewDuration.Name = "bestTestOverviewDuration";
    this.bestTestOverviewDuration.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.bestTestOverviewDuration.StylePriority.UseFont = false;
    this.bestTestOverviewDuration.StylePriority.UsePadding = false;
    this.bestTestOverviewDuration.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewDuration.Weight = 0.299507025061971;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewInstrument, "bestTestOverviewInstrument");
    this.bestTestOverviewInstrument.Name = "bestTestOverviewInstrument";
    this.bestTestOverviewInstrument.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.bestTestOverviewInstrument.StylePriority.UseFont = false;
    this.bestTestOverviewInstrument.StylePriority.UsePadding = false;
    this.bestTestOverviewInstrument.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewInstrument.Weight = 0.3836929073932806;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewProbe, "bestTestOverviewProbe");
    this.bestTestOverviewProbe.Name = "bestTestOverviewProbe";
    this.bestTestOverviewProbe.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.bestTestOverviewProbe.StylePriority.UseFont = false;
    this.bestTestOverviewProbe.StylePriority.UsePadding = false;
    this.bestTestOverviewProbe.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewProbe.Weight = 0.28197456944757771;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewExaminer, "bestTestOverviewExaminer");
    this.bestTestOverviewExaminer.Name = "bestTestOverviewExaminer";
    this.bestTestOverviewExaminer.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.bestTestOverviewExaminer.StylePriority.UseFont = false;
    this.bestTestOverviewExaminer.StylePriority.UsePadding = false;
    this.bestTestOverviewExaminer.StylePriority.UseTextAlignment = false;
    this.bestTestOverviewExaminer.Weight = 0.45345602832344561;
    this.testResultOverviewHeader.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrTable1
    });
    this.testResultOverviewHeader.HeightF = 28f;
    this.testResultOverviewHeader.Name = "testResultOverviewHeader";
    this.testResultOverviewHeader.RepeatEveryPage = true;
    componentResourceManager.ApplyResources((object) this.testResultOverviewHeader, "testResultOverviewHeader");
    componentResourceManager.ApplyResources((object) this.xrTable1, "xrTable1");
    this.xrTable1.Name = "xrTable1";
    this.xrTable1.Rows.AddRange(new XRTableRow[1]
    {
      this.xrTableRow1
    });
    this.xrTableRow1.Borders = BorderSide.Bottom;
    this.xrTableRow1.Cells.AddRange(new XRTableCell[9]
    {
      this.xrTableCell1,
      this.xrTableCell2,
      this.xrTableCell3,
      this.xrTableCell9,
      this.xrTableCell4,
      this.xrTableCell5,
      this.xrTableCell6,
      this.xrTableCell7,
      this.xrTableCell8
    });
    this.xrTableRow1.Name = "xrTableRow1";
    this.xrTableRow1.StylePriority.UseBorders = false;
    componentResourceManager.ApplyResources((object) this.xrTableRow1, "xrTableRow1");
    this.xrTableRow1.Weight = 1.0;
    this.xrTableCell1.Name = "xrTableCell1";
    this.xrTableCell1.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrTableCell1, "xrTableCell1");
    this.xrTableCell1.Weight = 101.0 / 329.0;
    this.xrTableCell2.Name = "xrTableCell2";
    this.xrTableCell2.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrTableCell2, "xrTableCell2");
    this.xrTableCell2.Weight = 0.21300899099796378;
    this.xrTableCell3.Name = "xrTableCell3";
    this.xrTableCell3.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.xrTableCell3.StylePriority.UsePadding = false;
    this.xrTableCell3.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrTableCell3, "xrTableCell3");
    this.xrTableCell3.Weight = 0.5883554559750801;
    this.xrTableCell9.Name = "xrTableCell9";
    componentResourceManager.ApplyResources((object) this.xrTableCell9, "xrTableCell9");
    this.xrTableCell9.Weight = 0.064814518339446389;
    this.xrTableCell4.Name = "xrTableCell4";
    this.xrTableCell4.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.xrTableCell4.StylePriority.UsePadding = false;
    this.xrTableCell4.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrTableCell4, "xrTableCell4");
    this.xrTableCell4.Weight = 0.36819962300226833;
    this.xrTableCell5.Name = "xrTableCell5";
    this.xrTableCell5.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.xrTableCell5.StylePriority.UsePadding = false;
    this.xrTableCell5.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrTableCell5, "xrTableCell5");
    this.xrTableCell5.Weight = 0.29950702506197097;
    this.xrTableCell6.Name = "xrTableCell6";
    this.xrTableCell6.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.xrTableCell6.StylePriority.UsePadding = false;
    this.xrTableCell6.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrTableCell6, "xrTableCell6");
    this.xrTableCell6.Weight = 0.3836929073932806;
    this.xrTableCell7.Name = "xrTableCell7";
    this.xrTableCell7.Padding = new PaddingInfo(0, 5, 0, 0, 100f);
    this.xrTableCell7.StylePriority.UsePadding = false;
    this.xrTableCell7.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrTableCell7, "xrTableCell7");
    this.xrTableCell7.Weight = 0.28197441685968705;
    this.xrTableCell8.Name = "xrTableCell8";
    this.xrTableCell8.Padding = new PaddingInfo(5, 0, 0, 0, 100f);
    this.xrTableCell8.StylePriority.UsePadding = false;
    this.xrTableCell8.StylePriority.UseTextAlignment = false;
    componentResourceManager.ApplyResources((object) this.xrTableCell8, "xrTableCell8");
    this.xrTableCell8.Weight = 0.45345618091133633;
    this.testResultOverviewFooter.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrTable2
    });
    this.testResultOverviewFooter.HeightF = 50f;
    this.testResultOverviewFooter.Name = "testResultOverviewFooter";
    componentResourceManager.ApplyResources((object) this.testResultOverviewFooter, "testResultOverviewFooter");
    componentResourceManager.ApplyResources((object) this.xrTable2, "xrTable2");
    this.xrTable2.Name = "xrTable2";
    this.xrTable2.Rows.AddRange(new XRTableRow[1]
    {
      this.bestTestOverviewHeaderRow
    });
    this.bestTestOverviewHeaderRow.Borders = BorderSide.Top;
    this.bestTestOverviewHeaderRow.Cells.AddRange(new XRTableCell[8]
    {
      this.bestTestOverviewTestTypeTitle,
      this.bestTestOverviewTestEarTitle,
      this.bestTestOverviewTestDateTitle,
      this.bestTestOverviewTestResultTitle,
      this.bestTestOverviewDurationTitle,
      this.bestTestOverviewInstrumentTitle,
      this.bestTestOverviewProbeTitle,
      this.bestTestOverviewExaminerTitle
    });
    this.bestTestOverviewHeaderRow.Name = "bestTestOverviewHeaderRow";
    this.bestTestOverviewHeaderRow.StylePriority.UseBorders = false;
    componentResourceManager.ApplyResources((object) this.bestTestOverviewHeaderRow, "bestTestOverviewHeaderRow");
    this.bestTestOverviewHeaderRow.Weight = 1.0;
    this.bestTestOverviewTestTypeTitle.Name = "bestTestOverviewTestTypeTitle";
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestTypeTitle, "bestTestOverviewTestTypeTitle");
    this.bestTestOverviewTestTypeTitle.Weight = 101.0 / 329.0;
    this.bestTestOverviewTestEarTitle.Name = "bestTestOverviewTestEarTitle";
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestEarTitle, "bestTestOverviewTestEarTitle");
    this.bestTestOverviewTestEarTitle.Weight = 0.21300895708954365;
    this.bestTestOverviewTestDateTitle.Name = "bestTestOverviewTestDateTitle";
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestDateTitle, "bestTestOverviewTestDateTitle");
    this.bestTestOverviewTestDateTitle.Weight = 0.49291174474095872;
    this.bestTestOverviewTestResultTitle.Name = "bestTestOverviewTestResultTitle";
    componentResourceManager.ApplyResources((object) this.bestTestOverviewTestResultTitle, "bestTestOverviewTestResultTitle");
    this.bestTestOverviewTestResultTitle.Weight = 0.431235664262034;
    this.bestTestOverviewDurationTitle.Name = "bestTestOverviewDurationTitle";
    componentResourceManager.ApplyResources((object) this.bestTestOverviewDurationTitle, "bestTestOverviewDurationTitle");
    this.bestTestOverviewDurationTitle.Weight = 0.35043279274860295;
    this.bestTestOverviewInstrumentTitle.Name = "bestTestOverviewInstrumentTitle";
    componentResourceManager.ApplyResources((object) this.bestTestOverviewInstrumentTitle, "bestTestOverviewInstrumentTitle");
    this.bestTestOverviewInstrumentTitle.Weight = 0.42998936192887088;
    this.bestTestOverviewProbeTitle.Name = "bestTestOverviewProbeTitle";
    componentResourceManager.ApplyResources((object) this.bestTestOverviewProbeTitle, "bestTestOverviewProbeTitle");
    this.bestTestOverviewProbeTitle.Weight = 0.28197456944757765;
    this.bestTestOverviewExaminerTitle.Name = "bestTestOverviewExaminerTitle";
    componentResourceManager.ApplyResources((object) this.bestTestOverviewExaminerTitle, "bestTestOverviewExaminerTitle");
    this.bestTestOverviewExaminerTitle.Weight = 0.45345602832344573;
    this.formattingRule1.Name = "formattingRule1";
    this.topMarginBand1.HeightF = 24f;
    this.topMarginBand1.Name = "topMarginBand1";
    componentResourceManager.ApplyResources((object) this.topMarginBand1, "topMarginBand1");
    this.bottomMarginBand1.HeightF = 16f;
    this.bottomMarginBand1.Name = "bottomMarginBand1";
    componentResourceManager.ApplyResources((object) this.bottomMarginBand1, "bottomMarginBand1");
    this.patientCommmentOverview.Bands.AddRange(new Band[2]
    {
      (Band) this.patientCommentDetails,
      (Band) this.patientCommentHeader
    });
    this.patientCommmentOverview.Level = 1;
    this.patientCommmentOverview.Name = "patientCommmentOverview";
    componentResourceManager.ApplyResources((object) this.patientCommmentOverview, "patientCommmentOverview");
    this.patientCommentDetails.Controls.AddRange(new XRControl[1]
    {
      (XRControl) this.xrTable3
    });
    this.patientCommentDetails.HeightF = 28f;
    this.patientCommentDetails.Name = "patientCommentDetails";
    componentResourceManager.ApplyResources((object) this.patientCommentDetails, "patientCommentDetails");
    componentResourceManager.ApplyResources((object) this.xrTable3, "xrTable3");
    this.xrTable3.Name = "xrTable3";
    this.xrTable3.Rows.AddRange(new XRTableRow[1]
    {
      this.xrTableRow3
    });
    this.xrTableRow3.Cells.AddRange(new XRTableCell[3]
    {
      this.xrTableCellCommentCreated,
      this.xrTableCellComment,
      this.xrTableCellCommenCreatedBy
    });
    this.xrTableRow3.Name = "xrTableRow3";
    componentResourceManager.ApplyResources((object) this.xrTableRow3, "xrTableRow3");
    this.xrTableRow3.Weight = 1.0;
    componentResourceManager.ApplyResources((object) this.xrTableCellCommentCreated, "xrTableCellCommentCreated");
    this.xrTableCellCommentCreated.Name = "xrTableCellCommentCreated";
    this.xrTableCellCommentCreated.StylePriority.UseFont = false;
    this.xrTableCellCommentCreated.Weight = 0.45045051918373447;
    componentResourceManager.ApplyResources((object) this.xrTableCellComment, "xrTableCellComment");
    this.xrTableCellComment.Name = "xrTableCellComment";
    this.xrTableCellComment.StylePriority.UseFont = false;
    this.xrTableCellComment.Weight = 2.089965751579216;
    componentResourceManager.ApplyResources((object) this.xrTableCellCommenCreatedBy, "xrTableCellCommenCreatedBy");
    this.xrTableCellCommenCreatedBy.Name = "xrTableCellCommenCreatedBy";
    this.xrTableCellCommenCreatedBy.StylePriority.UseFont = false;
    this.xrTableCellCommenCreatedBy.Weight = 0.45958372923704949;
    this.patientCommentHeader.Controls.AddRange(new XRControl[2]
    {
      (XRControl) this.xrTableCommentHeader,
      (XRControl) this.xrLabelComments
    });
    this.patientCommentHeader.HeightF = 52f;
    this.patientCommentHeader.Name = "patientCommentHeader";
    componentResourceManager.ApplyResources((object) this.patientCommentHeader, "patientCommentHeader");
    componentResourceManager.ApplyResources((object) this.xrTableCommentHeader, "xrTableCommentHeader");
    this.xrTableCommentHeader.Name = "xrTableCommentHeader";
    this.xrTableCommentHeader.Rows.AddRange(new XRTableRow[1]
    {
      this.xrTableRow2
    });
    this.xrTableRow2.Cells.AddRange(new XRTableCell[3]
    {
      this.xrTableCellCommentCreatedHeader,
      this.xrTableCellCommentHeader,
      this.xrTableCellCreatedByHeader
    });
    this.xrTableRow2.Name = "xrTableRow2";
    componentResourceManager.ApplyResources((object) this.xrTableRow2, "xrTableRow2");
    this.xrTableRow2.Weight = 1.0;
    componentResourceManager.ApplyResources((object) this.xrTableCellCommentCreatedHeader, "xrTableCellCommentCreatedHeader");
    this.xrTableCellCommentCreatedHeader.Name = "xrTableCellCommentCreatedHeader";
    this.xrTableCellCommentCreatedHeader.StylePriority.UseFont = false;
    this.xrTableCellCommentCreatedHeader.Weight = 0.45045051918373447;
    componentResourceManager.ApplyResources((object) this.xrTableCellCommentHeader, "xrTableCellCommentHeader");
    this.xrTableCellCommentHeader.Name = "xrTableCellCommentHeader";
    this.xrTableCellCommentHeader.StylePriority.UseFont = false;
    this.xrTableCellCommentHeader.Weight = 2.089965751579216;
    componentResourceManager.ApplyResources((object) this.xrTableCellCreatedByHeader, "xrTableCellCreatedByHeader");
    this.xrTableCellCreatedByHeader.Name = "xrTableCellCreatedByHeader";
    this.xrTableCellCreatedByHeader.StylePriority.UseFont = false;
    this.xrTableCellCreatedByHeader.Weight = 0.45958372923704949;
    componentResourceManager.ApplyResources((object) this.xrLabelComments, "xrLabelComments");
    this.xrLabelComments.Name = "xrLabelComments";
    this.xrLabelComments.Padding = new PaddingInfo(2, 2, 0, 0, 100f);
    this.xrLabelComments.StylePriority.UseFont = false;
    this.Bands.AddRange(new Band[7]
    {
      (Band) this.patientDetails,
      (Band) this.PageHeader,
      (Band) this.PageFooter,
      (Band) this.testResultOverview,
      (Band) this.topMarginBand1,
      (Band) this.bottomMarginBand1,
      (Band) this.patientCommmentOverview
    });
    componentResourceManager.ApplyResources((object) this, "$this");
    this.FormattingRuleSheet.AddRange(new FormattingRule[1]
    {
      this.formattingRule1
    });
    this.Margins = new Margins(100, 76, 24, 16 /*0x10*/);
    this.StyleSheet.AddRange(new XRControlStyle[1]
    {
      this.xrControlStyle1
    });
    this.Version = "9.3";
    this.BeforePrint += new PrintEventHandler(this.DetailTestReport_BeforePrint);
    this.bestTestOverviewTable.EndInit();
    this.xrTable1.EndInit();
    this.xrTable2.EndInit();
    this.xrTable3.EndInit();
    this.xrTableCommentHeader.EndInit();
    this.EndInit();
  }
}
