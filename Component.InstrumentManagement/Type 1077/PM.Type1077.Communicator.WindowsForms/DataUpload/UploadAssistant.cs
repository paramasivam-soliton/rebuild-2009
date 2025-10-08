// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Communicator.WindowsForms.DataUpload.UploadAssistant
// Assembly: PM.Type1077.Communicator.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4173E4B7-BF2C-4E03-A869-8E0498B48F2A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.Communicator.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using DevExpress.XtraWizard;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.Type1077.Automaton;
using PathMedical.Type1077.Communicator.WindowsForms.Properties;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.Type1077.Communicator.WindowsForms.DataUpload;

[ToolboxItem(false)]
public class UploadAssistant : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private IContainer components;
  private WizardControl assistantUploadData;
  private WelcomeWizardPage pageWelcome;
  private CompletionWizardPage pageSynchronizationCompleted;
  private ListView instrumentListView;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlItem layoutDataDownload;
  private DevExpress.XtraWizard.WizardPage pageSynchronizing;
  private LayoutControl layoutDataSynchronization;
  private PictureEdit progressDataDownload;
  private LayoutControlGroup layoutControlGroup2;
  private EmptySpaceItem emptySpaceItem2;
  private LayoutControlItem layoutControlItem3;
  private PictureEdit progressConfigurationUpdate;
  private LayoutControlItem layoutControlItem4;
  private LabelControl labelOperationMessage;
  private LayoutControlItem layoutControlItem1;

  public UploadAssistant() => this.InitializeComponent();

  public UploadAssistant(IModel model)
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
      this.BeginInvoke((Delegate) new UploadAssistant.UpdateInstrumentDetectionCallBack(this.UpdateInstrumentList), (object) changedObject);
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
    foreach (Type1077Instrument instrument in instruments)
    {
      this.instrumentListView.Items.Add(new ListViewItem()
      {
        Text = string.Format(Resources.UpdateInstrumentList_ItemText, (object) instrument.Name, (object) instrument.SerialNumber),
        Tag = (object) instrument,
        ImageKey = instrument.Name
      });
      if (!this.instrumentListView.LargeImageList.Images.ContainsKey(instrument.Name))
        this.instrumentListView.LargeImageList.Images.Add(instrument.Name, (Image) instrument.Image);
    }
  }

  private void OnPageChanging(object sender, WizardPageChangingEventArgs e)
  {
    if (e.Page == this.pageWelcome)
    {
      this.instrumentListView.Clear();
      this.labelOperationMessage.Text = string.Empty;
      this.pageSynchronizationCompleted.Text = string.Empty;
      this.RequestControllerAction((object) this, new TriggerEventArgs(InstrumentManagementTriggers.StartInstrumentSearch, (TriggerContext) null, true, (EventHandler<TriggerExecutedEventArgs>) null));
    }
    else
    {
      if (e.Page != this.pageSynchronizing || e.PrevPage != this.pageWelcome)
        return;
      this.progressDataDownload.Image = (Image) null;
      this.progressConfigurationUpdate.Image = (Image) null;
      if (!(this.instrumentListView.SelectedItems[0].Tag is Type1077Instrument tag))
        return;
      this.pageSynchronizationCompleted.Text = string.Format(Resources.Synchronizing_Complete, (object) tag.Name);
      this.RequestControllerAction((object) this, new TriggerEventArgs(InstrumentManagementTriggers.StartDataUpload, (TriggerContext) new IntrumentSelectionTriggerContext(tag), true, new EventHandler<TriggerExecutedEventArgs>(this.OnDataUploadStateChanged)));
    }
  }

  private void OnWizzardCompleteClick(object sender, CancelEventArgs e)
  {
    this.assistantUploadData.SelectedPage = (BaseWizardPage) this.pageWelcome;
  }

  private void OnWizzardCancelClick(object sender, CancelEventArgs e)
  {
    this.assistantUploadData.SelectedPage = (BaseWizardPage) this.pageWelcome;
  }

  private void OnDataUploadStateChanged(object sender, TriggerExecutedEventArgs e)
  {
    if (e == null || e.TriggerEventArgs == null || !(e.TriggerEventArgs.TriggerContext is IntrumentSelectionTriggerContext))
      return;
    Type1077Instrument instrument = (e.TriggerEventArgs.TriggerContext as IntrumentSelectionTriggerContext).Instrument;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new UploadAssistant.UpdateDataUploadCallBack(this.UpdateDataUploadProgress), (object) instrument, (object) e.State, (object) e.Text);
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
        this.labelOperationMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) Resources.UploadAssistant_PatientUploadAbort, (object) stateDescription);
        this.pageSynchronizationCompleted.Text = Resources.UploadAssistant_UploadAbort;
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantUploadData.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
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
        this.labelOperationMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) Resources.UploadAssistant_ErrorUploadingPatients, (object) stateDescription);
        this.pageSynchronizationCompleted.Text = Resources.UploadAssistant_UploadFailed;
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantUploadData.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Executed:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        this.RequestControllerAction((object) this, new TriggerEventArgs(InstrumentManagementTriggers.StartConfigurationSynchronization, (TriggerContext) new IntrumentSelectionTriggerContext(instrument), true, new EventHandler<TriggerExecutedEventArgs>(this.OnDataConfigurationSynchronizationCompleted)));
        break;
    }
  }

  private void InstrumentSelectionChanged(object sender, EventArgs e)
  {
    if (this.instrumentListView.SelectedItems != null && this.instrumentListView.SelectedItems.Count > 0)
    {
      Type1077Instrument tag = this.instrumentListView.SelectedItems[0].Tag as Type1077Instrument;
      this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.ChangeSelection, (TriggerContext) new ChangeSelectionTriggerContext<Type1077Instrument>((ICollection<Type1077Instrument>) null, (ICollection<Type1077Instrument>) new List<Type1077Instrument>()
      {
        tag
      })));
      this.pageWelcome.AllowNext = true;
    }
    else
    {
      this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.ChangeSelection, (TriggerContext) new ChangeSelectionTriggerContext<Type1077Instrument>((ICollection<Type1077Instrument>) null, (ICollection<Type1077Instrument>) new List<Type1077Instrument>())));
      this.pageWelcome.AllowNext = false;
    }
  }

  private void OnDataConfigurationSynchronizationCompleted(
    object sender,
    TriggerExecutedEventArgs e)
  {
    if (e == null || e.TriggerEventArgs == null || !(e.TriggerEventArgs.TriggerContext is IntrumentSelectionTriggerContext))
      return;
    Type1077Instrument instrument = (e.TriggerEventArgs.TriggerContext as IntrumentSelectionTriggerContext).Instrument;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new UploadAssistant.UpdateDataSynchronizationProgressCallBack(this.UpdateConfigurationSynchronizationProgress), (object) instrument, (object) e.State, (object) e.Text);
    else
      this.UpdateConfigurationSynchronizationProgress(instrument, e.State, e.Text);
  }

  private void UpdateConfigurationSynchronizationProgress(
    Type1077Instrument instrument,
    TriggerExecutionState state,
    string stateDescription)
  {
    this.progressConfigurationUpdate.Image = (Image) null;
    if (instrument == null)
      return;
    switch (state)
    {
      case TriggerExecutionState.Aborted:
        this.progressConfigurationUpdate.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.labelOperationMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelOperationMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) "Aborted patient data upload to AccuScreen on user request.", (object) stateDescription);
        this.pageSynchronizationCompleted.Text = Resources.UploadAssistant_UploadAbort;
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantUploadData.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Running:
        this.progressConfigurationUpdate.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationRunning") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        break;
      case TriggerExecutionState.Failed:
        this.progressConfigurationUpdate.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.labelOperationMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelOperationMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) Resources.Configuration_ErrorUploading, (object) stateDescription);
        this.pageSynchronizationCompleted.Text = Resources.UploadAssistant_UploadFailed;
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantUploadData.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Executed:
        this.progressDataDownload.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        this.pageSynchronizationCompleted.Text = Resources.UploadAssistant_UploadComplete;
        this.labelOperationMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.labelOperationMessage.Text = Resources.UploadAssistant_UploadCompleteMessage;
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantUploadData.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
    }
  }

  private void OnAssistantVisibilityChanged(object sender, EventArgs e)
  {
    if (!this.assistantUploadData.Visible || this.assistantUploadData.SelectedPage != this.pageWelcome)
      return;
    this.pageWelcome.AllowNext = false;
    this.instrumentListView.Clear();
    this.RequestControllerAction((object) this, new TriggerEventArgs(InstrumentManagementTriggers.StartInstrumentSearch, (TriggerContext) null, true, (EventHandler<TriggerExecutedEventArgs>) null));
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UploadAssistant));
    this.assistantUploadData = new WizardControl();
    this.pageWelcome = new WelcomeWizardPage();
    this.instrumentListView = new ListView();
    this.pageSynchronizationCompleted = new CompletionWizardPage();
    this.layoutControl1 = new LayoutControl();
    this.labelOperationMessage = new LabelControl();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.pageSynchronizing = new DevExpress.XtraWizard.WizardPage();
    this.layoutDataSynchronization = new LayoutControl();
    this.progressConfigurationUpdate = new PictureEdit();
    this.progressDataDownload = new PictureEdit();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutControlItem4 = new LayoutControlItem();
    this.layoutDataDownload = new LayoutControlItem();
    this.assistantUploadData.BeginInit();
    this.assistantUploadData.SuspendLayout();
    this.pageWelcome.SuspendLayout();
    this.pageSynchronizationCompleted.SuspendLayout();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.layoutControlGroup1.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.pageSynchronizing.SuspendLayout();
    this.layoutDataSynchronization.BeginInit();
    this.layoutDataSynchronization.SuspendLayout();
    this.progressConfigurationUpdate.Properties.BeginInit();
    this.progressDataDownload.Properties.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.layoutDataDownload.BeginInit();
    this.SuspendLayout();
    this.assistantUploadData.Controls.Add((Control) this.pageWelcome);
    this.assistantUploadData.Controls.Add((Control) this.pageSynchronizationCompleted);
    this.assistantUploadData.Controls.Add((Control) this.pageSynchronizing);
    this.assistantUploadData.Name = "assistantUploadData";
    this.assistantUploadData.Pages.AddRange(new BaseWizardPage[3]
    {
      (BaseWizardPage) this.pageWelcome,
      (BaseWizardPage) this.pageSynchronizing,
      (BaseWizardPage) this.pageSynchronizationCompleted
    });
    componentResourceManager.ApplyResources((object) this.assistantUploadData, "assistantUploadData");
    this.assistantUploadData.WizardStyle = WizardStyle.WizardAero;
    this.assistantUploadData.SelectedPageChanging += new WizardPageChangingEventHandler(this.OnPageChanging);
    this.assistantUploadData.CancelClick += new CancelEventHandler(this.OnWizzardCancelClick);
    this.assistantUploadData.FinishClick += new CancelEventHandler(this.OnWizzardCompleteClick);
    this.assistantUploadData.VisibleChanged += new EventHandler(this.OnAssistantVisibilityChanged);
    this.pageWelcome.Controls.Add((Control) this.instrumentListView);
    componentResourceManager.ApplyResources((object) this.pageWelcome, "pageWelcome");
    this.pageWelcome.Name = "pageWelcome";
    componentResourceManager.ApplyResources((object) this.instrumentListView, "instrumentListView");
    this.instrumentListView.Name = "instrumentListView";
    this.instrumentListView.UseCompatibleStateImageBehavior = false;
    this.instrumentListView.SelectedIndexChanged += new EventHandler(this.InstrumentSelectionChanged);
    this.pageSynchronizationCompleted.Controls.Add((Control) this.layoutControl1);
    componentResourceManager.ApplyResources((object) this.pageSynchronizationCompleted, "pageSynchronizationCompleted");
    this.pageSynchronizationCompleted.Name = "pageSynchronizationCompleted";
    this.layoutControl1.Controls.Add((Control) this.labelOperationMessage);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    this.labelOperationMessage.AllowHtmlString = true;
    this.labelOperationMessage.Appearance.ImageAlign = ContentAlignment.TopLeft;
    this.labelOperationMessage.Appearance.Options.UseTextOptions = true;
    this.labelOperationMessage.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
    this.labelOperationMessage.ImageAlignToText = ImageAlignToText.LeftTop;
    componentResourceManager.ApplyResources((object) this.labelOperationMessage, "labelOperationMessage");
    this.labelOperationMessage.Name = "labelOperationMessage";
    this.labelOperationMessage.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem1
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(480, 221);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.TextVisible = false;
    this.layoutControlItem1.Control = (Control) this.labelOperationMessage;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(460, 201);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    this.pageSynchronizing.Controls.Add((Control) this.layoutDataSynchronization);
    this.pageSynchronizing.Name = "pageSynchronizing";
    componentResourceManager.ApplyResources((object) this.pageSynchronizing, "pageSynchronizing");
    this.layoutDataSynchronization.Controls.Add((Control) this.progressConfigurationUpdate);
    this.layoutDataSynchronization.Controls.Add((Control) this.progressDataDownload);
    componentResourceManager.ApplyResources((object) this.layoutDataSynchronization, "layoutDataSynchronization");
    this.layoutDataSynchronization.Name = "layoutDataSynchronization";
    this.layoutDataSynchronization.Root = this.layoutControlGroup2;
    componentResourceManager.ApplyResources((object) this.progressConfigurationUpdate, "progressConfigurationUpdate");
    this.progressConfigurationUpdate.Name = "progressConfigurationUpdate";
    this.progressConfigurationUpdate.Properties.BorderStyle = BorderStyles.NoBorder;
    this.progressConfigurationUpdate.Properties.NullText = componentResourceManager.GetString("progressConfigurationUpdate.Properties.NullText");
    this.progressConfigurationUpdate.Properties.PictureAlignment = ContentAlignment.MiddleLeft;
    this.progressConfigurationUpdate.StyleController = (IStyleController) this.layoutDataSynchronization;
    componentResourceManager.ApplyResources((object) this.progressDataDownload, "progressDataDownload");
    this.progressDataDownload.Name = "progressDataDownload";
    this.progressDataDownload.Properties.BorderStyle = BorderStyles.NoBorder;
    this.progressDataDownload.Properties.NullText = componentResourceManager.GetString("progressDataDownload.Properties.NullText");
    this.progressDataDownload.Properties.PictureAlignment = ContentAlignment.MiddleLeft;
    this.progressDataDownload.StyleController = (IStyleController) this.layoutDataSynchronization;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
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
    this.layoutControlGroup2.Size = new Size(480, 221);
    this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup2.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(0, 48 /*0x30*/);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(460, 153);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    this.layoutControlItem3.AppearanceItemCaption.Font = new Font("Tahoma", 10f);
    this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
    this.layoutControlItem3.Control = (Control) this.progressDataDownload;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(0, 0);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(460, 24);
    this.layoutControlItem3.TextSize = new Size(130, 16 /*0x10*/);
    this.layoutControlItem4.AppearanceItemCaption.Font = new Font("Tahoma", 10f);
    this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
    this.layoutControlItem4.Control = (Control) this.progressConfigurationUpdate;
    componentResourceManager.ApplyResources((object) this.layoutControlItem4, "layoutControlItem4");
    this.layoutControlItem4.Location = new Point(0, 24);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(460, 24);
    this.layoutControlItem4.TextSize = new Size(130, 16 /*0x10*/);
    this.layoutDataDownload.AppearanceItemCaption.Font = new Font("Tahoma", 10f);
    this.layoutDataDownload.AppearanceItemCaption.Options.UseFont = true;
    componentResourceManager.ApplyResources((object) this.layoutDataDownload, "layoutDataDownload");
    this.layoutDataDownload.Location = new Point(0, 0);
    this.layoutDataDownload.Name = "layoutDataDownload";
    this.layoutDataDownload.Size = new Size(460, 31 /*0x1F*/);
    this.layoutDataDownload.TextSize = new Size(163, 16 /*0x10*/);
    this.layoutDataDownload.TextToControlDistance = 5;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.assistantUploadData);
    this.Name = nameof (UploadAssistant);
    this.assistantUploadData.EndInit();
    this.assistantUploadData.ResumeLayout(false);
    this.pageWelcome.ResumeLayout(false);
    this.pageSynchronizationCompleted.ResumeLayout(false);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.layoutControlGroup1.EndInit();
    this.layoutControlItem1.EndInit();
    this.pageSynchronizing.ResumeLayout(false);
    this.layoutDataSynchronization.EndInit();
    this.layoutDataSynchronization.ResumeLayout(false);
    this.progressConfigurationUpdate.Properties.EndInit();
    this.progressDataDownload.Properties.EndInit();
    this.layoutControlGroup2.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutControlItem4.EndInit();
    this.layoutDataDownload.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateDataUploadCallBack(
    Type1077Instrument instrument,
    TriggerExecutionState state,
    string stateDescription);

  private delegate void UpdateInstrumentDetectionCallBack(List<Type1077Instrument> instrument);

  private delegate void UpdateDataSynchronizationProgressCallBack(
    Type1077Instrument instrument,
    TriggerExecutionState state,
    string stateDescription);
}
