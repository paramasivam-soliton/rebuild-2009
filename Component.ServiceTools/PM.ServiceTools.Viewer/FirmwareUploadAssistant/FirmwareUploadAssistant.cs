// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.FirmwareUploadAssistant.FirmwareUploadAssistant
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraWizard;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.ResourceManager;
using PathMedical.ServiceTools.Automaton;
using PathMedical.ServiceTools.WindowsForms.Automaton;
using PathMedical.ServiceTools.WindowsForms.Properties;
using PathMedical.Type1077;
using PathMedical.Type1077.Automaton;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.FirmwareUploadAssistant;

[ToolboxItem(false)]
public class FirmwareUploadAssistant : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private bool _pageChangedHandled;
  private IContainer components;
  private WizardControl assistantFirmwareUpload;
  private WelcomeWizardPage pageWelcome;
  private ListView instrumentListView;
  private CompletionWizardPage pageSynchronizationCompleted;
  private LabelControl labelOperationMessage;
  private DevExpress.XtraWizard.WizardPage pageSynchronizing;
  private LayoutControl layoutControl1;
  private PictureEdit progressDataDownload;
  private LayoutControlGroup layoutControlGroup1;
  private EmptySpaceItem emptySpaceItem2;
  private LayoutControlItem layoutControlItem1;

  public FirmwareUploadAssistant() => this.InitializeComponent();

  public FirmwareUploadAssistant(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType != ChangeType.SelectionChanged && e.ChangeType != ChangeType.ItemEdited && e.ChangeType != ChangeType.ItemAdded || !(e.ChangedObject is List<Type1077Instrument>))
      return;
    List<Type1077Instrument> changedObject = e.ChangedObject as List<Type1077Instrument>;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PathMedical.ServiceTools.WindowsForms.FirmwareUploadAssistant.FirmwareUploadAssistant.UpdateInstrumentListCallBack(this.UpdateInstrumentList), (object) changedObject);
    else
      this.UpdateInstrumentList(changedObject);
  }

  private void UpdateInstrumentList(List<Type1077Instrument> instruments)
  {
    if (instruments == null)
      return;
    this.instrumentListView.Clear();
    this.instrumentListView.LargeImageList = new ImageList()
    {
      ImageSize = new Size(32 /*0x20*/, 32 /*0x20*/)
    };
    foreach (IInstrument instrument in instruments)
    {
      this.instrumentListView.Items.Add(new ListViewItem()
      {
        Text = $"{instrument.Name} \n (Serial {instrument.SerialNumber})",
        Tag = (object) instrument,
        ImageKey = instrument.Name
      });
      if (!this.instrumentListView.LargeImageList.Images.ContainsKey(instrument.Name))
        this.instrumentListView.LargeImageList.Images.Add(instrument.Name, (Image) instrument.Image);
    }
  }

  private void OnPageChanging(object sender, WizardPageChangingEventArgs e)
  {
    if (this._pageChangedHandled)
      return;
    if (e.Page == this.pageWelcome)
    {
      this.instrumentListView.Clear();
      this.RequestControllerAction((object) this, new TriggerEventArgs(InstrumentManagementTriggers.StartInstrumentSearch, (TriggerContext) null, true, new EventHandler<TriggerExecutedEventArgs>(this.OnInstrumentSearchCompleted)));
    }
    else
    {
      if (e.Page != this.pageSynchronizing || !(this.instrumentListView.SelectedItems[0].Tag is Type1077Instrument tag))
        return;
      SelectFirmwareFileTriggerContext context = new SelectFirmwareFileTriggerContext(tag);
      this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.SelectFirmwareFileTrigger, (TriggerContext) context));
      this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.UploadFirmwareFileTrigger, (TriggerContext) new UploadFirmwareFileTriggerContext(tag), true, new EventHandler<TriggerExecutedEventArgs>(this.OnDataUploadStateChanged)));
    }
  }

  private void OnWizzardCompleteClick(object sender, CancelEventArgs e)
  {
    try
    {
      this._pageChangedHandled = true;
      this.assistantFirmwareUpload.SelectedPage = (BaseWizardPage) this.pageWelcome;
    }
    finally
    {
      this._pageChangedHandled = false;
    }
  }

  private void OnWizzardCancelClick(object sender, CancelEventArgs e)
  {
    try
    {
      this._pageChangedHandled = true;
      this.assistantFirmwareUpload.SelectedPage = (BaseWizardPage) this.pageWelcome;
    }
    finally
    {
      this._pageChangedHandled = false;
    }
  }

  private void OnDataDownloadStateChanged(object sender, TriggerExecutedEventArgs e)
  {
    if (e == null || e.TriggerEventArgs == null || !(e.TriggerEventArgs.TriggerContext is IntrumentSelectionTriggerContext))
      return;
    Type1077Instrument instrument = (e.TriggerEventArgs.TriggerContext as IntrumentSelectionTriggerContext).Instrument;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PathMedical.ServiceTools.WindowsForms.FirmwareUploadAssistant.FirmwareUploadAssistant.UpdateDataDownloadCallBack(this.UpdateDataDownloadProgress), (object) instrument, (object) e.State, (object) e.Text);
    else
      this.UpdateDataDownloadProgress(instrument, e.State, e.Text);
  }

  private void UpdateDataDownloadProgress(
    Type1077Instrument instrument,
    TriggerExecutionState state,
    string stateDescription)
  {
    this.labelOperationMessage.Text = string.Empty;
    this.progressDataDownload.Image = (Image) null;
    if (instrument == null)
      return;
    switch (state)
    {
      case TriggerExecutionState.Running:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationRunning") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        break;
      case TriggerExecutionState.Failed:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        this.labelOperationMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelOperationMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) "An error occurred while uploading firmware.", (object) stateDescription);
        this.pageSynchronizationCompleted.Text = "Firmware upload failed";
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantFirmwareUpload.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Executed:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.pageSynchronizationCompleted.Text = "Firmware upload successfully";
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantFirmwareUpload.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
    }
  }

  private void OnDataUploadStateChanged(object sender, TriggerExecutedEventArgs e)
  {
    if (e == null || e.TriggerEventArgs == null || !(e.TriggerEventArgs.TriggerContext is UploadFirmwareFileTriggerContext))
      return;
    Type1077Instrument instrument = (e.TriggerEventArgs.TriggerContext as UploadFirmwareFileTriggerContext).Instrument;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PathMedical.ServiceTools.WindowsForms.FirmwareUploadAssistant.FirmwareUploadAssistant.UpdateDataUploadCallBack(this.UpdateDataUploadProgress), (object) instrument, (object) e.State, (object) e.Text);
    else
      this.UpdateDataUploadProgress(instrument, e.State, e.Text);
  }

  private void UpdateDataUploadProgress(
    Type1077Instrument instrument,
    TriggerExecutionState state,
    string stateDescription)
  {
    if (instrument == null)
      return;
    switch (state)
    {
      case TriggerExecutionState.Aborted:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.labelOperationMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelOperationMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) "Upload aborted", (object) stateDescription);
        this.pageSynchronizationCompleted.Text = "Upload aborted";
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantFirmwareUpload.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Running:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationRunning") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        break;
      case TriggerExecutionState.Failed:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        this.labelOperationMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelOperationMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) "", (object) stateDescription);
        this.pageSynchronizationCompleted.Text = "Upload failed";
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantFirmwareUpload.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Executed:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        this.assistantFirmwareUpload.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
    }
  }

  private void OnInstrumentSearchCompleted(object sender, TriggerExecutedEventArgs e)
  {
  }

  private void InstrumentSelectionChanged(object sender, EventArgs e)
  {
    if (this.instrumentListView.SelectedItems != null && this.instrumentListView.SelectedItems.Count > 0)
    {
      Instrument tag = this.instrumentListView.SelectedItems[0].Tag as Instrument;
      this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.ChangeSelection, (TriggerContext) new ChangeSelectionTriggerContext<Instrument>((ICollection<Instrument>) null, (ICollection<Instrument>) new List<Instrument>()
      {
        tag
      })));
      this.pageWelcome.AllowNext = true;
    }
    else
    {
      this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.ChangeSelection, (TriggerContext) new ChangeSelectionTriggerContext<Instrument>((ICollection<Instrument>) null, (ICollection<Instrument>) new List<Instrument>())));
      this.pageWelcome.AllowNext = false;
    }
  }

  private void OnAssistantVisibilityChanged(object sender, EventArgs e)
  {
    if (!this.assistantFirmwareUpload.Visible || this.assistantFirmwareUpload.SelectedPage != this.pageWelcome)
      return;
    this.pageWelcome.AllowNext = false;
    this.instrumentListView.Clear();
    this.RequestControllerAction((object) this, new TriggerEventArgs(InstrumentManagementTriggers.StartInstrumentSearch, (TriggerContext) null, true, new EventHandler<TriggerExecutedEventArgs>(this.OnInstrumentSearchCompleted)));
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.assistantFirmwareUpload = new WizardControl();
    this.pageWelcome = new WelcomeWizardPage();
    this.instrumentListView = new ListView();
    this.pageSynchronizationCompleted = new CompletionWizardPage();
    this.labelOperationMessage = new LabelControl();
    this.pageSynchronizing = new DevExpress.XtraWizard.WizardPage();
    this.layoutControl1 = new LayoutControl();
    this.progressDataDownload = new PictureEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutControlItem1 = new LayoutControlItem();
    this.assistantFirmwareUpload.BeginInit();
    this.assistantFirmwareUpload.SuspendLayout();
    this.pageWelcome.SuspendLayout();
    this.pageSynchronizationCompleted.SuspendLayout();
    this.pageSynchronizing.SuspendLayout();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.progressDataDownload.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.SuspendLayout();
    this.assistantFirmwareUpload.Controls.Add((Control) this.pageWelcome);
    this.assistantFirmwareUpload.Controls.Add((Control) this.pageSynchronizationCompleted);
    this.assistantFirmwareUpload.Controls.Add((Control) this.pageSynchronizing);
    this.assistantFirmwareUpload.Name = "assistantFirmwareUpload";
    this.assistantFirmwareUpload.Pages.AddRange(new BaseWizardPage[3]
    {
      (BaseWizardPage) this.pageWelcome,
      (BaseWizardPage) this.pageSynchronizing,
      (BaseWizardPage) this.pageSynchronizationCompleted
    });
    this.assistantFirmwareUpload.Text = "Firmware Upload Assistant";
    this.assistantFirmwareUpload.WizardStyle = WizardStyle.WizardAero;
    this.assistantFirmwareUpload.SelectedPageChanging += new WizardPageChangingEventHandler(this.OnPageChanging);
    this.assistantFirmwareUpload.CancelClick += new CancelEventHandler(this.OnWizzardCancelClick);
    this.assistantFirmwareUpload.FinishClick += new CancelEventHandler(this.OnWizzardCompleteClick);
    this.assistantFirmwareUpload.VisibleChanged += new EventHandler(this.OnAssistantVisibilityChanged);
    this.pageWelcome.AllowBack = false;
    this.pageWelcome.AllowNext = false;
    this.pageWelcome.Controls.Add((Control) this.instrumentListView);
    this.pageWelcome.IntroductionText = "";
    this.pageWelcome.Name = "pageWelcome";
    this.pageWelcome.Size = new Size(548, 242);
    this.pageWelcome.Text = "Select an Instrument and click next to continue";
    this.instrumentListView.Dock = DockStyle.Fill;
    this.instrumentListView.Location = new Point(0, 0);
    this.instrumentListView.Name = "instrumentListView";
    this.instrumentListView.Size = new Size(548, 242);
    this.instrumentListView.TabIndex = 1;
    this.instrumentListView.UseCompatibleStateImageBehavior = false;
    this.instrumentListView.SelectedIndexChanged += new EventHandler(this.InstrumentSelectionChanged);
    this.pageSynchronizationCompleted.AllowBack = false;
    this.pageSynchronizationCompleted.AllowNext = false;
    this.pageSynchronizationCompleted.Controls.Add((Control) this.labelOperationMessage);
    this.pageSynchronizationCompleted.FinishText = "";
    this.pageSynchronizationCompleted.Name = "pageSynchronizationCompleted";
    this.pageSynchronizationCompleted.ProceedText = "";
    this.pageSynchronizationCompleted.Size = new Size(548, 242);
    this.pageSynchronizationCompleted.Text = "Firmware Upload has been completed.";
    this.labelOperationMessage.AllowHtmlString = true;
    this.labelOperationMessage.Appearance.ImageAlign = ContentAlignment.TopLeft;
    this.labelOperationMessage.Appearance.Options.UseTextOptions = true;
    this.labelOperationMessage.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
    this.labelOperationMessage.Dock = DockStyle.Fill;
    this.labelOperationMessage.ImageAlignToText = ImageAlignToText.LeftTop;
    this.labelOperationMessage.Location = new Point(0, 0);
    this.labelOperationMessage.Name = "labelOperationMessage";
    this.labelOperationMessage.Size = new Size(274, 14);
    this.labelOperationMessage.TabIndex = 0;
    this.labelOperationMessage.Text = "Click \"Finish\" button and wait until the instrument reboots";
    this.pageSynchronizing.Controls.Add((Control) this.layoutControl1);
    this.pageSynchronizing.Name = "pageSynchronizing";
    this.pageSynchronizing.Size = new Size(548, 242);
    this.pageSynchronizing.Text = "Exchange Data with AccuScreen";
    this.layoutControl1.Controls.Add((Control) this.progressDataDownload);
    this.layoutControl1.Dock = DockStyle.Fill;
    this.layoutControl1.Location = new Point(0, 0);
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    this.layoutControl1.Size = new Size(548, 242);
    this.layoutControl1.TabIndex = 0;
    this.layoutControl1.Text = "layoutControl1";
    this.progressDataDownload.Location = new Point(131, 12);
    this.progressDataDownload.Name = "progressDataDownload";
    this.progressDataDownload.Properties.BorderStyle = BorderStyles.NoBorder;
    this.progressDataDownload.Properties.NullText = " ";
    this.progressDataDownload.Properties.PictureAlignment = ContentAlignment.MiddleLeft;
    this.progressDataDownload.Size = new Size(405, 23);
    this.progressDataDownload.StyleController = (IStyleController) this.layoutControl1;
    this.progressDataDownload.TabIndex = 4;
    this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutControlItem1
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "Root";
    this.layoutControlGroup1.Size = new Size(548, 242);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.Text = "Root";
    this.layoutControlGroup1.TextVisible = false;
    this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
    this.emptySpaceItem2.Location = new Point(0, 27);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(528, 195);
    this.emptySpaceItem2.Text = "emptySpaceItem2";
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    this.layoutControlItem1.AppearanceItemCaption.Font = new Font("Tahoma", 10f);
    this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
    this.layoutControlItem1.Control = (Control) this.progressDataDownload;
    this.layoutControlItem1.CustomizationFormText = "Receiving Probe Information";
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(528, 27);
    this.layoutControlItem1.Text = "Uploading Firmware";
    this.layoutControlItem1.TextSize = new Size(115, 16 /*0x10*/);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.assistantFirmwareUpload);
    this.Name = nameof (FirmwareUploadAssistant);
    this.Size = new Size(608, 404);
    this.assistantFirmwareUpload.EndInit();
    this.assistantFirmwareUpload.ResumeLayout(false);
    this.pageWelcome.ResumeLayout(false);
    this.pageSynchronizationCompleted.ResumeLayout(false);
    this.pageSynchronizationCompleted.PerformLayout();
    this.pageSynchronizing.ResumeLayout(false);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.progressDataDownload.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutControlItem1.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateDataDownloadCallBack(
    Type1077Instrument instrument,
    TriggerExecutionState state,
    string stateDescription);

  private delegate void UpdateDataUploadCallBack(
    Type1077Instrument instrument,
    TriggerExecutionState state,
    string stateDescription);

  private delegate void UpdateInstrumentListCallBack(List<Type1077Instrument> instrument);
}
