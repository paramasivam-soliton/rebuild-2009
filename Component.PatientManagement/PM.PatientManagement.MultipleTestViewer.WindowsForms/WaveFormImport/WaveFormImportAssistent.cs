// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.WaveFormImport.WaveFormImportAssistent
// Assembly: PM.PatientManagement.MultipleTestViewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2267BB5-2AA5-4F30-972C-4D9F0F378D06
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.MultipleTestViewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraWizard;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.WaveFormImport;

[ToolboxItem(false)]
[Obsolete]
public class WaveFormImportAssistent : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private bool fileerror;
  private bool filesok;
  private IContainer components;
  private WizardControl assistentWaveFileImporter;
  private WelcomeWizardPage pageWelcome;
  private DevExpress.XtraWizard.WizardPage pageSelectWaveDateFolder;
  private CompletionWizardPage pageWaveImportCompleted;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup1;
  private LabelControl errorLabel;
  private EmptySpaceItem emptySpaceItem3;
  private EmptySpaceItem emptySpaceItem4;
  private LayoutControlItem layoutControlItem5;
  private LayoutControlGroup ImportlayoutControlGroup;
  private LayoutControlGroup layoutControlGroup2;
  private OpenFileDialog openFileDialog;

  public WaveFormImportAssistent()
  {
    this.InitializeComponent();
    this.assistentWaveFileImporter.SelectedPage = (BaseWizardPage) this.pageWelcome;
    this.errorLabel.Text = "";
    this.openFileDialog.Title = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("OpenFileDialogTitle");
  }

  public WaveFormImportAssistent(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    this.fileerror = false;
    if (e.ChangeType != ChangeType.ListLoaded || !(e.ChangedObject is bool) || !(bool) e.ChangedObject)
      return;
    this.fileerror = true;
    BaseWizardPage selectWaveDateFolder = (BaseWizardPage) this.pageSelectWaveDateFolder;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new WaveFormImportAssistent.ChangePageProgressCallBack(this.ChangePage), (object) selectWaveDateFolder);
    else
      this.ChangePage(selectWaveDateFolder);
  }

  private void VisibilityChanged(object sender, EventArgs e)
  {
    if (!this.assistentWaveFileImporter.Visible)
      return;
    BaseWizardPage selectedPage = this.assistentWaveFileImporter.SelectedPage;
    WelcomeWizardPage pageWelcome = this.pageWelcome;
  }

  private void ChangePage(BaseWizardPage page)
  {
    if (this.assistentWaveFileImporter == null || page == null)
      return;
    this.assistentWaveFileImporter.SelectedPage = page;
    if (!this.fileerror)
    {
      this.errorLabel.Text = "";
    }
    else
    {
      this.errorLabel.Text = Resources.WaveFormImportAssistent_NoFilesFound;
      this.pageSelectWaveDateFolder.AllowNext = false;
    }
  }

  private void CancelClick(object sender, CancelEventArgs e)
  {
    this.assistentWaveFileImporter.SelectedPage = (BaseWizardPage) this.pageWelcome;
  }

  private void OnWizzardCompleteClick(object sender, CancelEventArgs e)
  {
    this.assistentWaveFileImporter.SuspendLayout();
    this.assistentWaveFileImporter.SelectedPage = (BaseWizardPage) this.pageWelcome;
    this.assistentWaveFileImporter.ResumeLayout();
  }

  private void OnPageChanging(object sender, WizardPageChangingEventArgs e)
  {
    if (e.Page != this.pageSelectWaveDateFolder)
      return;
    this.errorLabel.Text = "";
    if (this.openFileDialog.ShowDialog() == DialogResult.OK)
      this.RequestControllerAction((object) this, new TriggerEventArgs(WaveFileImportTriggers.StartDataImport, (TriggerContext) new FileInformationTriggerContext(this.openFileDialog.FileNames), true, new EventHandler<TriggerExecutedEventArgs>(this.OnImportComplete)));
    else
      e.Cancel = true;
  }

  private void OnImportComplete(object sender, TriggerExecutedEventArgs e)
  {
    if (e.State == TriggerExecutionState.Executed)
    {
      this.filesok = true;
      this.ChangePage((BaseWizardPage) this.pageWaveImportCompleted);
    }
    if (e.State != TriggerExecutionState.Failed || this.filesok)
      return;
    this.ChangePage((BaseWizardPage) this.pageWelcome);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (WaveFormImportAssistent));
    this.assistentWaveFileImporter = new WizardControl();
    this.pageWelcome = new WelcomeWizardPage();
    this.pageSelectWaveDateFolder = new DevExpress.XtraWizard.WizardPage();
    this.layoutControl1 = new LayoutControl();
    this.errorLabel = new LabelControl();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.emptySpaceItem3 = new EmptySpaceItem();
    this.emptySpaceItem4 = new EmptySpaceItem();
    this.layoutControlItem5 = new LayoutControlItem();
    this.pageWaveImportCompleted = new CompletionWizardPage();
    this.ImportlayoutControlGroup = new LayoutControlGroup();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.openFileDialog = new OpenFileDialog();
    this.assistentWaveFileImporter.BeginInit();
    this.assistentWaveFileImporter.SuspendLayout();
    this.pageSelectWaveDateFolder.SuspendLayout();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.layoutControlGroup1.BeginInit();
    this.emptySpaceItem3.BeginInit();
    this.emptySpaceItem4.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.ImportlayoutControlGroup.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.SuspendLayout();
    this.assistentWaveFileImporter.Appearance.Page.BackColor = Color.White;
    this.assistentWaveFileImporter.Appearance.Page.Options.UseBackColor = true;
    this.assistentWaveFileImporter.Controls.Add((Control) this.pageWelcome);
    this.assistentWaveFileImporter.Controls.Add((Control) this.pageSelectWaveDateFolder);
    this.assistentWaveFileImporter.Controls.Add((Control) this.pageWaveImportCompleted);
    this.assistentWaveFileImporter.Name = "assistentWaveFileImporter";
    this.assistentWaveFileImporter.Pages.AddRange(new BaseWizardPage[3]
    {
      (BaseWizardPage) this.pageWelcome,
      (BaseWizardPage) this.pageSelectWaveDateFolder,
      (BaseWizardPage) this.pageWaveImportCompleted
    });
    componentResourceManager.ApplyResources((object) this.assistentWaveFileImporter, "assistentWaveFileImporter");
    this.assistentWaveFileImporter.WizardStyle = WizardStyle.WizardAero;
    this.assistentWaveFileImporter.SelectedPageChanging += new WizardPageChangingEventHandler(this.OnPageChanging);
    this.assistentWaveFileImporter.CancelClick += new CancelEventHandler(this.CancelClick);
    this.assistentWaveFileImporter.FinishClick += new CancelEventHandler(this.OnWizzardCompleteClick);
    this.assistentWaveFileImporter.VisibleChanged += new EventHandler(this.VisibilityChanged);
    componentResourceManager.ApplyResources((object) this.pageWelcome, "pageWelcome");
    this.pageWelcome.Name = "pageWelcome";
    this.pageSelectWaveDateFolder.Controls.Add((Control) this.layoutControl1);
    componentResourceManager.ApplyResources((object) this.pageSelectWaveDateFolder, "pageSelectWaveDateFolder");
    this.pageSelectWaveDateFolder.Name = "pageSelectWaveDateFolder";
    this.layoutControl1.Controls.Add((Control) this.errorLabel);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.errorLabel, "errorLabel");
    this.errorLabel.Name = "errorLabel";
    this.errorLabel.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.emptySpaceItem3,
      (BaseLayoutItem) this.emptySpaceItem4,
      (BaseLayoutItem) this.layoutControlItem5
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(415, 167);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem3, "emptySpaceItem3");
    this.emptySpaceItem3.Location = new Point(0, 0);
    this.emptySpaceItem3.Name = "emptySpaceItem3";
    this.emptySpaceItem3.Size = new Size(395, 106);
    this.emptySpaceItem3.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem4, "emptySpaceItem4");
    this.emptySpaceItem4.Location = new Point(0, 123);
    this.emptySpaceItem4.MinSize = new Size(104, 24);
    this.emptySpaceItem4.Name = "emptySpaceItem4";
    this.emptySpaceItem4.Size = new Size(395, 24);
    this.emptySpaceItem4.SizeConstraintsType = SizeConstraintsType.Custom;
    this.emptySpaceItem4.TextSize = new Size(0, 0);
    this.layoutControlItem5.Control = (Control) this.errorLabel;
    componentResourceManager.ApplyResources((object) this.layoutControlItem5, "layoutControlItem5");
    this.layoutControlItem5.Location = new Point(0, 106);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(395, 17);
    this.layoutControlItem5.TextSize = new Size(0, 0);
    this.layoutControlItem5.TextToControlDistance = 0;
    this.layoutControlItem5.TextVisible = false;
    this.pageWaveImportCompleted.Name = "pageWaveImportCompleted";
    componentResourceManager.ApplyResources((object) this.pageWaveImportCompleted, "pageWaveImportCompleted");
    componentResourceManager.ApplyResources((object) this.ImportlayoutControlGroup, "ImportlayoutControlGroup");
    this.ImportlayoutControlGroup.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.ImportlayoutControlGroup.GroupBordersVisible = false;
    this.ImportlayoutControlGroup.Location = new Point(0, 0);
    this.ImportlayoutControlGroup.Name = "ImportlayoutControlGroup";
    this.ImportlayoutControlGroup.OptionsItemText.TextToControlDistance = 5;
    this.ImportlayoutControlGroup.Size = new Size(180, 120);
    this.ImportlayoutControlGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.ImportlayoutControlGroup.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup2.GroupBordersVisible = false;
    this.layoutControlGroup2.Location = new Point(0, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 5;
    this.layoutControlGroup2.Size = new Size(443, 182);
    this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup2.TextVisible = false;
    this.openFileDialog.Multiselect = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.assistentWaveFileImporter);
    this.Name = nameof (WaveFormImportAssistent);
    this.assistentWaveFileImporter.EndInit();
    this.assistentWaveFileImporter.ResumeLayout(false);
    this.pageSelectWaveDateFolder.ResumeLayout(false);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.layoutControlGroup1.EndInit();
    this.emptySpaceItem3.EndInit();
    this.emptySpaceItem4.EndInit();
    this.layoutControlItem5.EndInit();
    this.ImportlayoutControlGroup.EndInit();
    this.layoutControlGroup2.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void ChangePageProgressCallBack(BaseWizardPage page);
}
