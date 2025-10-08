// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Connector.WindowsForms.Export.EspExportAssistant
// Assembly: PM.DataExchange.eSP.Connector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1934FEB7-E9C0-480F-B9F2-DEDB47DB36DA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.Connector.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraWizard;
using PathMedical.Automaton;
using PathMedical.DataExchange.eSP.Connector.WindowsForms.Properties;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.DataExchange.eSP.Connector.WindowsForms.Export;

[ToolboxItem(false)]
public class EspExportAssistant : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private IContainer components;
  private WizardControl assistantEspExport;
  private WelcomeWizardPage pageWelcome;
  private CompletionWizardPage pageSynchronizationCompleted;
  private LabelControl welcomeMessage;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup1;
  private DevExpress.XtraWizard.WizardPage pageSynchronizing;
  private LayoutControl layoutControl2;
  private PictureEdit progressConfigurationUpdate;
  private PictureEdit progressDataDownload;
  private LayoutControlGroup layoutControlGroup2;
  private EmptySpaceItem emptySpaceItem2;
  private LayoutControlItem layoutControlItem3;
  private LayoutControlItem layoutControlItem4;
  private LabelControl labelCompletionMessage;
  private LayoutControlItem layoutControlItem1;

  public EspExportAssistant() => this.InitializeComponent();

  public EspExportAssistant(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
  }

  private void AssistantPageChanging(object sender, WizardPageChangingEventArgs e)
  {
    if (e.Page != this.pageSynchronizing || e.PrevPage != this.pageWelcome)
      return;
    this.progressDataDownload.Image = (Image) null;
    this.progressConfigurationUpdate.Image = (Image) null;
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Export, (TriggerContext) null, true, new EventHandler<TriggerExecutedEventArgs>(this.OnExportStateChanged)));
  }

  private void OnExportStateChanged(object sender, TriggerExecutedEventArgs e)
  {
    if (e == null || e.TriggerEventArgs == null)
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new EspExportAssistant.UpdateProgressSynchronizationCallBack(this.UpdateExportProgressSynchronization), (object) e.State, (object) e.Text);
    else
      this.UpdateExportProgressSynchronization(e.State, e.Text);
  }

  private void UpdateExportProgressSynchronization(TriggerExecutionState state, string description)
  {
    switch (state)
    {
      case TriggerExecutionState.Aborted:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Text = $"{"The export of test results has been successfully aborted."}";
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.pageSynchronizationCompleted.Text = "Data Sychronization has been failed";
        this.assistantEspExport.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Running:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationRunning") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        break;
      case TriggerExecutionState.Failed:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) "Test results couldn't be send to eSP.", (object) description);
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.pageSynchronizationCompleted.Text = "Data Sychronization has been failed";
        this.assistantEspExport.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Executed:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Import, (TriggerContext) null, true, new EventHandler<TriggerExecutedEventArgs>(this.OnImportStateChanged)));
        break;
    }
  }

  private void OnImportStateChanged(object sender, TriggerExecutedEventArgs e)
  {
    if (e == null || e.TriggerEventArgs == null)
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new EspExportAssistant.UpdateProgressSynchronizationCallBack(this.UpdateImportProgressSynchronization), (object) e.State, (object) e.Text);
    else
      this.UpdateImportProgressSynchronization(e.State, e.Text);
  }

  private void UpdateImportProgressSynchronization(TriggerExecutionState state, string description)
  {
    switch (state)
    {
      case TriggerExecutionState.Aborted:
        this.progressConfigurationUpdate.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Text = $"{"The export of test results has been successfully aborted."}";
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.pageSynchronizationCompleted.Text = "Data Sychronization has been failed";
        this.assistantEspExport.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Running:
        this.progressConfigurationUpdate.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationRunning") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        break;
      case TriggerExecutionState.Failed:
        this.progressConfigurationUpdate.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) "Test results couldn't be send to eSP.", (object) description);
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.pageSynchronizationCompleted.Text = "Data Sychronization has been failed";
        this.assistantEspExport.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Executed:
        this.progressConfigurationUpdate.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.labelCompletionMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.labelCompletionMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) "Test results and configuration have been successfully exchanged with eSP.", (object) description);
        this.pageSynchronizationCompleted.Text = "Data Sychronization has been completed successfully";
        this.assistantEspExport.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
    }
  }

  private void VisibilityChanged(object sender, EventArgs e)
  {
    if (this.assistantEspExport == null)
      return;
    this.assistantEspExport.SelectedPage = (BaseWizardPage) this.pageWelcome;
  }

  private void ChangePage(BaseWizardPage page)
  {
    if (this.assistantEspExport == null || page == null)
      return;
    this.assistantEspExport.SelectedPage = page;
  }

  private void CancelClick(object sender, CancelEventArgs e)
  {
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.AbortAndClose, (TriggerContext) null));
  }

  private void PreviousClick(object sender, WizardCommandButtonClickEventArgs e)
  {
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EspExportAssistant));
    this.assistantEspExport = new WizardControl();
    this.pageWelcome = new WelcomeWizardPage();
    this.welcomeMessage = new LabelControl();
    this.pageSynchronizationCompleted = new CompletionWizardPage();
    this.layoutControl1 = new LayoutControl();
    this.labelCompletionMessage = new LabelControl();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.pageSynchronizing = new DevExpress.XtraWizard.WizardPage();
    this.layoutControl2 = new LayoutControl();
    this.progressConfigurationUpdate = new PictureEdit();
    this.progressDataDownload = new PictureEdit();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutControlItem4 = new LayoutControlItem();
    this.assistantEspExport.BeginInit();
    this.assistantEspExport.SuspendLayout();
    this.pageWelcome.SuspendLayout();
    this.pageSynchronizationCompleted.SuspendLayout();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.layoutControlGroup1.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.pageSynchronizing.SuspendLayout();
    this.layoutControl2.BeginInit();
    this.layoutControl2.SuspendLayout();
    this.progressConfigurationUpdate.Properties.BeginInit();
    this.progressDataDownload.Properties.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.SuspendLayout();
    this.assistantEspExport.Controls.Add((Control) this.pageWelcome);
    this.assistantEspExport.Controls.Add((Control) this.pageSynchronizationCompleted);
    this.assistantEspExport.Controls.Add((Control) this.pageSynchronizing);
    this.assistantEspExport.LookAndFeel.SkinName = "Seven";
    this.assistantEspExport.Name = "assistantEspExport";
    this.assistantEspExport.Pages.AddRange(new BaseWizardPage[3]
    {
      (BaseWizardPage) this.pageWelcome,
      (BaseWizardPage) this.pageSynchronizing,
      (BaseWizardPage) this.pageSynchronizationCompleted
    });
    this.assistantEspExport.Text = "eSP Synchronization";
    this.assistantEspExport.WizardStyle = WizardStyle.WizardAero;
    this.assistantEspExport.VisibleChanged += new EventHandler(this.VisibilityChanged);
    this.assistantEspExport.PrevClick += new WizardCommandButtonClickEventHandler(this.PreviousClick);
    this.assistantEspExport.SelectedPageChanging += new WizardPageChangingEventHandler(this.AssistantPageChanging);
    this.assistantEspExport.CancelClick += new CancelEventHandler(this.CancelClick);
    this.pageWelcome.Controls.Add((Control) this.welcomeMessage);
    this.pageWelcome.IntroductionText = "This wizzard guides you through the synchronization process with the eSP system.";
    this.pageWelcome.Name = "pageWelcome";
    this.pageWelcome.Size = new Size(415, 165);
    this.pageWelcome.Text = "Welcome to eSP data synchronization";
    this.welcomeMessage.AllowHtmlString = true;
    this.welcomeMessage.Dock = DockStyle.Fill;
    this.welcomeMessage.Location = new Point(0, 0);
    this.welcomeMessage.Name = "welcomeMessage";
    this.welcomeMessage.Size = new Size(408, 84);
    this.welcomeMessage.TabIndex = 0;
    this.welcomeMessage.Text = componentResourceManager.GetString("welcomeMessage.Text");
    this.pageSynchronizationCompleted.AllowBack = false;
    this.pageSynchronizationCompleted.Controls.Add((Control) this.layoutControl1);
    this.pageSynchronizationCompleted.FinishText = "";
    this.pageSynchronizationCompleted.Name = "pageSynchronizationCompleted";
    this.pageSynchronizationCompleted.Size = new Size(415, 165);
    this.pageSynchronizationCompleted.Text = "Synchronization has been completed";
    this.layoutControl1.Controls.Add((Control) this.labelCompletionMessage);
    this.layoutControl1.Dock = DockStyle.Fill;
    this.layoutControl1.Location = new Point(0, 0);
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    this.layoutControl1.Size = new Size(415, 165);
    this.layoutControl1.TabIndex = 0;
    this.layoutControl1.Text = "layoutControl1";
    this.labelCompletionMessage.AllowHtmlString = true;
    this.labelCompletionMessage.Appearance.ImageAlign = ContentAlignment.TopLeft;
    this.labelCompletionMessage.Dock = DockStyle.Fill;
    this.labelCompletionMessage.ImageAlignToText = ImageAlignToText.LeftTop;
    this.labelCompletionMessage.Location = new Point(12, 12);
    this.labelCompletionMessage.Name = "labelCompletionMessage";
    this.labelCompletionMessage.Size = new Size(51, 14);
    this.labelCompletionMessage.StyleController = (IStyleController) this.layoutControl1;
    this.labelCompletionMessage.TabIndex = 4;
    this.labelCompletionMessage.Text = "Completed";
    this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem1
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "Root";
    this.layoutControlGroup1.Size = new Size(415, 165);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.Text = "Root";
    this.layoutControlGroup1.TextVisible = false;
    this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlItem1.AppearanceItemCaption.TextOptions.WordWrap = WordWrap.Wrap;
    this.layoutControlItem1.Control = (Control) this.labelCompletionMessage;
    this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(395, 145);
    this.layoutControlItem1.Text = "layoutControlItem1";
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    this.pageSynchronizing.Controls.Add((Control) this.layoutControl2);
    this.pageSynchronizing.Name = "pageSynchronizing";
    this.pageSynchronizing.Size = new Size(415, 165);
    this.pageSynchronizing.Text = "Exchanging Data with eSP";
    this.layoutControl2.Controls.Add((Control) this.progressConfigurationUpdate);
    this.layoutControl2.Controls.Add((Control) this.progressDataDownload);
    this.layoutControl2.Dock = DockStyle.Fill;
    this.layoutControl2.Location = new Point(0, 0);
    this.layoutControl2.Name = "layoutControl2";
    this.layoutControl2.Root = this.layoutControlGroup2;
    this.layoutControl2.Size = new Size(415, 165);
    this.layoutControl2.TabIndex = 0;
    this.layoutControl2.Text = "layoutControl2";
    this.progressConfigurationUpdate.Location = new Point(150, 36);
    this.progressConfigurationUpdate.Name = "progressConfigurationUpdate";
    this.progressConfigurationUpdate.Properties.BorderStyle = BorderStyles.NoBorder;
    this.progressConfigurationUpdate.Properties.NullText = " ";
    this.progressConfigurationUpdate.Properties.PictureAlignment = ContentAlignment.MiddleLeft;
    this.progressConfigurationUpdate.Size = new Size(253, 20);
    this.progressConfigurationUpdate.StyleController = (IStyleController) this.layoutControl2;
    this.progressConfigurationUpdate.TabIndex = 5;
    this.progressDataDownload.Location = new Point(150, 12);
    this.progressDataDownload.Name = "progressDataDownload";
    this.progressDataDownload.Properties.BorderStyle = BorderStyles.NoBorder;
    this.progressDataDownload.Properties.NullText = " ";
    this.progressDataDownload.Properties.PictureAlignment = ContentAlignment.MiddleLeft;
    this.progressDataDownload.Size = new Size(253, 20);
    this.progressDataDownload.StyleController = (IStyleController) this.layoutControl2;
    this.progressDataDownload.TabIndex = 4;
    this.layoutControlGroup2.CustomizationFormText = "Root";
    this.layoutControlGroup2.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup2.GroupBordersVisible = false;
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.layoutControlItem4
    });
    this.layoutControlGroup2.Location = new Point(0, 0);
    this.layoutControlGroup2.Name = "Root";
    this.layoutControlGroup2.Size = new Size(415, 165);
    this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup2.Text = "Root";
    this.layoutControlGroup2.TextVisible = false;
    this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
    this.emptySpaceItem2.Location = new Point(0, 48 /*0x30*/);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(395, 97);
    this.emptySpaceItem2.Text = "emptySpaceItem2";
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    this.layoutControlItem3.AppearanceItemCaption.Font = new Font("Tahoma", 10f);
    this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
    this.layoutControlItem3.Control = (Control) this.progressDataDownload;
    this.layoutControlItem3.CustomizationFormText = "Sending Test Results";
    this.layoutControlItem3.Location = new Point(0, 0);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(395, 24);
    this.layoutControlItem3.Text = "Sending Test Results";
    this.layoutControlItem3.TextSize = new Size(134, 16 /*0x10*/);
    this.layoutControlItem4.AppearanceItemCaption.Font = new Font("Tahoma", 10f);
    this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
    this.layoutControlItem4.Control = (Control) this.progressConfigurationUpdate;
    this.layoutControlItem4.CustomizationFormText = "Receiving Configuration";
    this.layoutControlItem4.Location = new Point(0, 24);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(395, 24);
    this.layoutControlItem4.Text = "Receiving Configuration";
    this.layoutControlItem4.TextSize = new Size(134, 16 /*0x10*/);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.assistantEspExport);
    this.Name = nameof (EspExportAssistant);
    this.Size = new Size(475, 327);
    this.assistantEspExport.EndInit();
    this.assistantEspExport.ResumeLayout(false);
    this.pageWelcome.ResumeLayout(false);
    this.pageWelcome.PerformLayout();
    this.pageSynchronizationCompleted.ResumeLayout(false);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.layoutControlGroup1.EndInit();
    this.layoutControlItem1.EndInit();
    this.pageSynchronizing.ResumeLayout(false);
    this.layoutControl2.EndInit();
    this.layoutControl2.ResumeLayout(false);
    this.progressConfigurationUpdate.Properties.EndInit();
    this.progressDataDownload.Properties.EndInit();
    this.layoutControlGroup2.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutControlItem4.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void ChangePageProgressCallBack(BaseWizardPage page);

  private delegate void UpdateProgressSynchronizationCallBack(
    TriggerExecutionState state,
    string description);
}
