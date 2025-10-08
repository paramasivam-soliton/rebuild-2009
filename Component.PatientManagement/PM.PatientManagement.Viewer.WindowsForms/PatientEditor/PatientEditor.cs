// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.PatientEditor
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using PathMedical.Exception;
using PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.Panels;
using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor;

[ToolboxItem(false)]
public class PatientEditor : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly PatientDataEditor patientDataEditor;
  private readonly PatientRiskIndicatorEditor patientRiskIndicatorEditor;
  private readonly PatientCommentEditor patientCommentEditor;
  private IContainer components;
  private XtraTabControl patientDataTabControl;
  private XtraTabPage patientDetailTab;
  private XtraTabPage patientRiskIndicatorTab;
  private GroupControl patientEditorInformation;
  private XtraTabPage patientCommentEditorTab;

  public PatientEditor()
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.patientDataTabControl.SelectedTabPage = this.patientDetailTab;
    this.HelpMarker = "patients_details_01.html";
  }

  public PatientEditor(
    IModel model,
    PatientDataEditor patientDataEditor,
    PatientRiskIndicatorEditor riskIndicatorEditor,
    PatientCommentEditor patientCommentEditor)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    if (patientDataEditor == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (patientDataEditor));
    if (riskIndicatorEditor == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (patientRiskIndicatorEditor));
    this.RegisterSubView((IView) patientDataEditor);
    this.RegisterSubView((IView) riskIndicatorEditor);
    this.RegisterSubView((IView) patientCommentEditor);
    patientDataEditor.Dock = DockStyle.Fill;
    this.patientDataEditor = patientDataEditor;
    this.patientDetailTab.Controls.Add((Control) this.patientDataEditor);
    riskIndicatorEditor.Dock = DockStyle.Fill;
    this.patientRiskIndicatorEditor = riskIndicatorEditor;
    this.patientRiskIndicatorTab.Controls.Add((Control) this.patientRiskIndicatorEditor);
    patientCommentEditor.Dock = DockStyle.Fill;
    this.patientCommentEditor = patientCommentEditor;
    this.patientCommentEditorTab.Controls.Add((Control) this.patientCommentEditor);
    this.InitializeToolbar();
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void InitializeToolbar()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.PatientEditor_InitializeToolbar_RibbonNavigationGroup));
    this.ToolbarElements.Add((object) this.patientRiskIndicatorEditor.QuickChangeRiskIndicatorGroup);
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.PatientEditor_InitializeToolbar_RibbonModificationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.PatientEditor_InitializeToolbar_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void FillHeader(Patient patient)
  {
    if (patient != null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (patient.PatientContact != null)
        stringBuilder.AppendFormat("{0}", (object) patient.PatientContact.FullName);
      if (patient.PatientContact != null && patient.PatientContact.DateOfBirth.HasValue)
      {
        DateTime dateTime = patient.PatientContact.DateOfBirth.Value;
        CultureInfo currentLanguage = SystemConfigurationManager.Instance.CurrentLanguage;
        stringBuilder.AppendFormat(" / {0}", currentLanguage.IsNeutralCulture ? (object) dateTime.ToString("D") : (object) dateTime.ToString("D", (IFormatProvider) currentLanguage));
      }
      if (!string.IsNullOrEmpty(patient.PatientRecordNumber))
        stringBuilder.AppendFormat(" / {0}", (object) patient.PatientRecordNumber);
      if (patient.ActiveRiskIndicatorCount > 0)
      {
        this.patientRiskIndicatorTab.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_RiskIndicatorAssigned") as Bitmap);
        this.patientRiskIndicatorTab.Text = Resources.PatientEditor_RiskIndicatorTabText;
      }
      else
      {
        this.patientRiskIndicatorTab.Image = (Image) null;
        this.patientRiskIndicatorTab.Text = Resources.PatientEditor_RiskIndicatorTabText;
      }
      this.patientEditorInformation.Text = stringBuilder.ToString();
    }
    else
      this.patientEditorInformation.Text = Resources.PatientEditor_InformationText;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (!(e.ChangedObject is Patient) || e.ChangeType != ChangeType.SelectionChanged && e.ChangeType != ChangeType.ItemEdited && e.ChangeType != ChangeType.ItemAdded)
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.PatientEditor.UpdatePatientDataCallBack(this.FillHeader), (object) (e.ChangedObject as Patient));
    else
      this.FillHeader(e.ChangedObject as Patient);
  }

  private void OnVisibilityChanged(object sender, EventArgs e)
  {
    if (!this.Visible)
      return;
    this.patientDataTabControl.SelectedTabPage = this.patientDetailTab;
  }

  private void OnTabChanged(object sender, TabPageChangedEventArgs e)
  {
    if (this.patientRiskIndicatorEditor == null || this.patientRiskIndicatorEditor.AssignUnknownRiskToNoRiskElement == null)
      return;
    this.patientRiskIndicatorEditor.AssignUnknownRiskToNoRiskElement.Enabled = this.patientDataTabControl.SelectedTabPage == this.patientRiskIndicatorTab;
    if (this.patientRiskIndicatorEditor.QuickChangeRiskIndicatorGroup.Ribbon == null)
      return;
    this.patientRiskIndicatorEditor.QuickChangeRiskIndicatorGroup.Ribbon.Refresh();
  }

  public bool ValidateEditorMandatoryGroups() => this.patientDataEditor.ValidateMandatoryGroups();

  public string EditorMandatoryGroupsValidationMessage
  {
    get => this.patientDataEditor.MandatoryGroupsValidationMessage;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.PatientEditor));
    this.patientDataTabControl = new XtraTabControl();
    this.patientRiskIndicatorTab = new XtraTabPage();
    this.patientDetailTab = new XtraTabPage();
    this.patientCommentEditorTab = new XtraTabPage();
    this.patientEditorInformation = new GroupControl();
    this.patientDataTabControl.BeginInit();
    this.patientDataTabControl.SuspendLayout();
    this.patientEditorInformation.BeginInit();
    this.patientEditorInformation.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.patientDataTabControl, "patientDataTabControl");
    this.patientDataTabControl.Name = "patientDataTabControl";
    this.patientDataTabControl.SelectedTabPage = this.patientRiskIndicatorTab;
    this.patientDataTabControl.TabPages.AddRange(new XtraTabPage[3]
    {
      this.patientDetailTab,
      this.patientRiskIndicatorTab,
      this.patientCommentEditorTab
    });
    this.patientDataTabControl.SelectedPageChanged += new TabPageChangedEventHandler(this.OnTabChanged);
    this.patientRiskIndicatorTab.Name = "patientRiskIndicatorTab";
    componentResourceManager.ApplyResources((object) this.patientRiskIndicatorTab, "patientRiskIndicatorTab");
    this.patientDetailTab.Name = "patientDetailTab";
    componentResourceManager.ApplyResources((object) this.patientDetailTab, "patientDetailTab");
    this.patientCommentEditorTab.Name = "patientCommentEditorTab";
    componentResourceManager.ApplyResources((object) this.patientCommentEditorTab, "patientCommentEditorTab");
    this.patientEditorInformation.Appearance.GradientMode = LinearGradientMode.BackwardDiagonal;
    this.patientEditorInformation.AppearanceCaption.Font = new Font("Tahoma", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.patientEditorInformation.AppearanceCaption.GradientMode = LinearGradientMode.BackwardDiagonal;
    this.patientEditorInformation.AppearanceCaption.Options.UseFont = true;
    this.patientEditorInformation.Controls.Add((Control) this.patientDataTabControl);
    componentResourceManager.ApplyResources((object) this.patientEditorInformation, "patientEditorInformation");
    this.patientEditorInformation.Name = "patientEditorInformation";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = SystemColors.Control;
    this.Controls.Add((Control) this.patientEditorInformation);
    this.DoubleBuffered = true;
    this.Name = nameof (PatientEditor);
    this.VisibleChanged += new EventHandler(this.OnVisibilityChanged);
    this.patientDataTabControl.EndInit();
    this.patientDataTabControl.ResumeLayout(false);
    this.patientEditorInformation.EndInit();
    this.patientEditorInformation.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private delegate void UpdatePatientDataCallBack(Patient patient);
}
