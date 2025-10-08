// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.Connector.WindowsForms.ConfigurationImport.EspImportConfigurationAssistant
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
namespace PathMedical.DataExchange.eSP.Connector.WindowsForms.ConfigurationImport;

[ToolboxItem(false)]
public class EspImportConfigurationAssistant : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private IContainer components;
  private WizardControl assistantEspExport;
  private WelcomeWizardPage pageWelcome;
  private DevExpress.XtraWizard.WizardPage pageSynchronization;
  private CompletionWizardPage pageSynchronizationCompleted;
  private LabelControl welcomeMessage;
  private LayoutControlItem layoutDataDownload;
  private LayoutControl layoutExchangeDataPage;
  private PictureEdit progressReceiveConfiguration;
  private LayoutControlGroup layoutControlGroup1;
  private EmptySpaceItem emptySpaceItem1;
  private LayoutControlItem layoutControlItem1;
  private LabelControl labelCompletionMessage;

  public EspImportConfigurationAssistant() => this.InitializeComponent();

  public EspImportConfigurationAssistant(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
  }

  private void AssistantPageChanging(object sender, WizardPageChangingEventArgs e)
  {
    if (e.Page != this.pageSynchronization || e.PrevPage != this.pageWelcome)
      return;
    this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Import, (TriggerContext) null, true, new EventHandler<TriggerExecutedEventArgs>(this.OnImportStateChanged)));
  }

  private void OnImportStateChanged(object sender, TriggerExecutedEventArgs e)
  {
    if (e == null || e.TriggerEventArgs == null)
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new EspImportConfigurationAssistant.UpdateProgressSynchronizationCallBack(this.UpdateProgressSynchronization), (object) e.State, (object) e.Text);
    else
      this.UpdateProgressSynchronization(e.State, e.Text);
  }

  private void UpdateProgressSynchronization(TriggerExecutionState state, string description)
  {
    switch (state)
    {
      case TriggerExecutionState.Aborted:
        this.progressReceiveConfiguration.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Text = $"{"The configuration synchronization has been successfully aborted."}";
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantEspExport.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Running:
        this.progressReceiveConfiguration.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationRunning") as Bitmap);
        this.pageSynchronization.AllowNext = false;
        this.pageSynchronization.AllowBack = false;
        this.pageSynchronization.AllowCancel = false;
        break;
      case TriggerExecutionState.Failed:
        this.progressReceiveConfiguration.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelCompletionMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) "The configuration couldn' be received from eSP.", (object) description);
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantEspExport.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Executed:
        this.progressReceiveConfiguration.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.labelCompletionMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.labelCompletionMessage.Text = $"{"The configuration has been successfully received from eSP."}";
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
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
    if (e == null || this.assistantEspExport.SelectedPage != this.pageSynchronizationCompleted)
      return;
    e.Handled = true;
    this.ChangePage((BaseWizardPage) this.pageSynchronization);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EspImportConfigurationAssistant));
    this.assistantEspExport = new WizardControl();
    this.pageWelcome = new WelcomeWizardPage();
    this.welcomeMessage = new LabelControl();
    this.pageSynchronizationCompleted = new CompletionWizardPage();
    this.labelCompletionMessage = new LabelControl();
    this.pageSynchronization = new DevExpress.XtraWizard.WizardPage();
    this.layoutExchangeDataPage = new LayoutControl();
    this.progressReceiveConfiguration = new PictureEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutDataDownload = new LayoutControlItem();
    this.assistantEspExport.BeginInit();
    this.assistantEspExport.SuspendLayout();
    this.pageWelcome.SuspendLayout();
    this.pageSynchronizationCompleted.SuspendLayout();
    this.pageSynchronization.SuspendLayout();
    this.layoutExchangeDataPage.BeginInit();
    this.layoutExchangeDataPage.SuspendLayout();
    this.progressReceiveConfiguration.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutDataDownload.BeginInit();
    this.SuspendLayout();
    this.assistantEspExport.Controls.Add((Control) this.pageWelcome);
    this.assistantEspExport.Controls.Add((Control) this.pageSynchronizationCompleted);
    this.assistantEspExport.Controls.Add((Control) this.pageSynchronization);
    this.assistantEspExport.LookAndFeel.SkinName = "Seven";
    this.assistantEspExport.Name = "assistantEspExport";
    this.assistantEspExport.Pages.AddRange(new BaseWizardPage[3]
    {
      (BaseWizardPage) this.pageWelcome,
      (BaseWizardPage) this.pageSynchronization,
      (BaseWizardPage) this.pageSynchronizationCompleted
    });
    this.assistantEspExport.Text = "eSP Connector";
    this.assistantEspExport.WizardStyle = WizardStyle.WizardAero;
    this.assistantEspExport.VisibleChanged += new EventHandler(this.VisibilityChanged);
    this.assistantEspExport.PrevClick += new WizardCommandButtonClickEventHandler(this.PreviousClick);
    this.assistantEspExport.SelectedPageChanging += new WizardPageChangingEventHandler(this.AssistantPageChanging);
    this.assistantEspExport.CancelClick += new CancelEventHandler(this.CancelClick);
    this.pageWelcome.Controls.Add((Control) this.welcomeMessage);
    this.pageWelcome.IntroductionText = "This wizzard guides you through the synchronization process with the eSP system.";
    this.pageWelcome.Name = "pageWelcome";
    this.pageWelcome.Size = new Size(415, 165);
    this.pageWelcome.Text = "Welcome to eSP configuration synchronization";
    this.welcomeMessage.AllowHtmlString = true;
    this.welcomeMessage.Dock = DockStyle.Fill;
    this.welcomeMessage.Location = new Point(0, 0);
    this.welcomeMessage.Name = "welcomeMessage";
    this.welcomeMessage.Size = new Size(407, 70);
    this.welcomeMessage.TabIndex = 0;
    this.welcomeMessage.Text = componentResourceManager.GetString("welcomeMessage.Text");
    this.pageSynchronizationCompleted.AllowBack = false;
    this.pageSynchronizationCompleted.Controls.Add((Control) this.labelCompletionMessage);
    this.pageSynchronizationCompleted.FinishText = "";
    this.pageSynchronizationCompleted.Name = "pageSynchronizationCompleted";
    this.pageSynchronizationCompleted.Size = new Size(415, 165);
    this.pageSynchronizationCompleted.Text = "eSP configuration synchronization has been completed";
    this.labelCompletionMessage.AllowHtmlString = true;
    this.labelCompletionMessage.Appearance.ImageAlign = ContentAlignment.TopLeft;
    this.labelCompletionMessage.Appearance.Options.UseTextOptions = true;
    this.labelCompletionMessage.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
    this.labelCompletionMessage.Dock = DockStyle.Fill;
    this.labelCompletionMessage.ImageAlignToText = ImageAlignToText.LeftTop;
    this.labelCompletionMessage.Location = new Point(0, 0);
    this.labelCompletionMessage.Name = "labelCompletionMessage";
    this.labelCompletionMessage.Size = new Size(0, 0);
    this.labelCompletionMessage.TabIndex = 0;
    this.pageSynchronization.Controls.Add((Control) this.layoutExchangeDataPage);
    this.pageSynchronization.DescriptionText = "Select a folder that contains new software versions for your instruments.";
    this.pageSynchronization.Name = "pageSynchronization";
    this.pageSynchronization.Size = new Size(415, 165);
    this.pageSynchronization.Text = "Exchanging data with eSP";
    this.layoutExchangeDataPage.Controls.Add((Control) this.progressReceiveConfiguration);
    this.layoutExchangeDataPage.Dock = DockStyle.Fill;
    this.layoutExchangeDataPage.Location = new Point(0, 0);
    this.layoutExchangeDataPage.Name = "layoutExchangeDataPage";
    this.layoutExchangeDataPage.Root = this.layoutControlGroup1;
    this.layoutExchangeDataPage.Size = new Size(415, 165);
    this.layoutExchangeDataPage.TabIndex = 0;
    this.layoutExchangeDataPage.Text = "layoutExchangeDataPage";
    this.progressReceiveConfiguration.Location = new Point(180, 12);
    this.progressReceiveConfiguration.Name = "progressReceiveConfiguration";
    this.progressReceiveConfiguration.Properties.Appearance.Font = new Font("Tahoma", 10f);
    this.progressReceiveConfiguration.Properties.Appearance.Options.UseFont = true;
    this.progressReceiveConfiguration.Properties.BorderStyle = BorderStyles.NoBorder;
    this.progressReceiveConfiguration.Properties.NullText = " ";
    this.progressReceiveConfiguration.Properties.PictureAlignment = ContentAlignment.MiddleLeft;
    this.progressReceiveConfiguration.Size = new Size(223, 20);
    this.progressReceiveConfiguration.StyleController = (IStyleController) this.layoutExchangeDataPage;
    this.progressReceiveConfiguration.TabIndex = 4;
    this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutControlItem1
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "Root";
    this.layoutControlGroup1.Size = new Size(415, 165);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.Text = "Root";
    this.layoutControlGroup1.TextVisible = false;
    this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
    this.emptySpaceItem1.Location = new Point(0, 24);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(395, 121);
    this.emptySpaceItem1.Text = "emptySpaceItem1";
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.AppearanceItemCaption.Font = new Font("Tahoma", 10f);
    this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
    this.layoutControlItem1.Control = (Control) this.progressReceiveConfiguration;
    this.layoutControlItem1.CustomizationFormText = "Receiving Configuration Data";
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(395, 24);
    this.layoutControlItem1.Text = "Receiving Configuration Data";
    this.layoutControlItem1.TextSize = new Size(164, 16 /*0x10*/);
    this.layoutDataDownload.AppearanceItemCaption.Font = new Font("Tahoma", 10f);
    this.layoutDataDownload.AppearanceItemCaption.Options.UseFont = true;
    this.layoutDataDownload.CustomizationFormText = "Receiving Patients and Tests";
    this.layoutDataDownload.Location = new Point(0, 0);
    this.layoutDataDownload.Name = "layoutDataDownload";
    this.layoutDataDownload.Size = new Size(460, 31 /*0x1F*/);
    this.layoutDataDownload.Text = "Receiving Patients and Tests";
    this.layoutDataDownload.TextSize = new Size(163, 16 /*0x10*/);
    this.layoutDataDownload.TextToControlDistance = 5;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.assistantEspExport);
    this.Name = nameof (EspImportConfigurationAssistant);
    this.Size = new Size(475, 327);
    this.assistantEspExport.EndInit();
    this.assistantEspExport.ResumeLayout(false);
    this.pageWelcome.ResumeLayout(false);
    this.pageWelcome.PerformLayout();
    this.pageSynchronizationCompleted.ResumeLayout(false);
    this.pageSynchronizationCompleted.PerformLayout();
    this.pageSynchronization.ResumeLayout(false);
    this.layoutExchangeDataPage.EndInit();
    this.layoutExchangeDataPage.ResumeLayout(false);
    this.progressReceiveConfiguration.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutDataDownload.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateProgressSynchronizationCallBack(
    TriggerExecutionState state,
    string description);
}
