// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Communicator.WindowsForms.ConfigurationSynchronization.ConfigurationSynchronizationAssistant
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
using PathMedical.InstrumentManagement;
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
namespace PathMedical.Type1077.Communicator.WindowsForms.ConfigurationSynchronization;

[ToolboxItem(false)]
public class ConfigurationSynchronizationAssistant : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private bool pageChangedHandled;
  private IContainer components;
  private WizardControl assistantDownloadData;
  private WelcomeWizardPage pageWelcome;
  private CompletionWizardPage pageSynchronizationCompleted;
  private ListView instrumentListView;
  private EmptySpaceItem emptySpaceItem1;
  private DevExpress.XtraWizard.WizardPage pageSynchronizing;
  private LabelControl labelOperationMessage;
  private LayoutControl layoutControl1;
  private PictureEdit progressConfigurationUpdate;
  private LayoutControlGroup layoutControlGroup1;
  private EmptySpaceItem emptySpaceItem2;
  private LayoutControlItem layoutControlItem2;

  public ConfigurationSynchronizationAssistant() => this.InitializeComponent();

  public ConfigurationSynchronizationAssistant(IModel model)
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
      this.BeginInvoke((Delegate) new ConfigurationSynchronizationAssistant.UpdateFirmwareImportProgressCallBack(this.UpdateInstrumentList), (object) changedObject);
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
    if (this.pageChangedHandled)
      return;
    if (e.Page == this.pageWelcome)
    {
      this.instrumentListView.Clear();
      this.RequestControllerAction((object) this, new TriggerEventArgs(InstrumentManagementTriggers.StartInstrumentSearch, (TriggerContext) null, true, (EventHandler<TriggerExecutedEventArgs>) null));
    }
    else
    {
      if (e.Page != this.pageSynchronizing || !(this.instrumentListView.SelectedItems[0].Tag is Type1077Instrument tag))
        return;
      this.pageSynchronizationCompleted.Text = string.Format(Resources.Synchronizing_Complete, (object) tag.Name);
      this.RequestControllerAction((object) this, new TriggerEventArgs(InstrumentManagementTriggers.StartConfigurationSynchronization, (TriggerContext) new IntrumentSelectionTriggerContext(tag), true, new EventHandler<TriggerExecutedEventArgs>(this.OnDataConfigurationSynchronizationCompleted)));
    }
  }

  private void OnWizzardCompleteClick(object sender, CancelEventArgs e)
  {
    try
    {
      this.pageChangedHandled = true;
      this.assistantDownloadData.SelectedPage = (BaseWizardPage) this.pageWelcome;
    }
    finally
    {
      this.pageChangedHandled = false;
    }
  }

  private void OnWizzardCancelClick(object sender, CancelEventArgs e)
  {
    try
    {
      this.pageChangedHandled = true;
      this.assistantDownloadData.SelectedPage = (BaseWizardPage) this.pageWelcome;
    }
    finally
    {
      this.pageChangedHandled = false;
    }
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

  private void OnDataConfigurationSynchronizationCompleted(
    object sender,
    TriggerExecutedEventArgs e)
  {
    if (e == null || e.TriggerEventArgs == null || !(e.TriggerEventArgs.TriggerContext is IntrumentSelectionTriggerContext))
      return;
    Type1077Instrument instrument = (e.TriggerEventArgs.TriggerContext as IntrumentSelectionTriggerContext).Instrument;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new ConfigurationSynchronizationAssistant.UpdateDataSynchronizationProgressCallBack(this.UpdateDataSynchronizationProgress), (object) instrument, (object) e.State, (object) e.Text);
    else
      this.UpdateDataSynchronizationProgress((Instrument) instrument, e.State, e.Text);
  }

  private void UpdateDataSynchronizationProgress(
    Instrument instrument,
    TriggerExecutionState state,
    string stateDescription)
  {
    this.labelOperationMessage.Text = string.Empty;
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
        this.labelOperationMessage.Text = string.Format("{1}{0}{2}", (object) Environment.NewLine, (object) "Aborted configuration synchronizatin on user request.", (object) stateDescription);
        this.pageSynchronizationCompleted.Text = Resources.ConfigurationSynchronizationAssistant_ConfigurationAbort;
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantDownloadData.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
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
        this.pageSynchronizationCompleted.Text = Resources.ConfigurationSynchronizationAssistant_ConfigurationFailed;
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantDownloadData.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
      case TriggerExecutionState.Executed:
        this.pageSynchronizing.AllowNext = false;
        this.pageSynchronizing.AllowBack = false;
        this.pageSynchronizing.AllowCancel = false;
        this.pageSynchronizationCompleted.Text = Resources.ConfigurationSynchronizationAssistant_ConfigurationComplete;
        this.labelOperationMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.labelOperationMessage.Text = Resources.ConfigurationSynchronizationAssistant_ConfigurationSuccessfull;
        this.pageSynchronizationCompleted.AllowNext = true;
        this.pageSynchronizationCompleted.AllowBack = false;
        this.pageSynchronizationCompleted.AllowCancel = false;
        this.assistantDownloadData.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
        break;
    }
  }

  private void OnAssistantVisibilityChanged(object sender, EventArgs e)
  {
    if (!this.assistantDownloadData.Visible || this.assistantDownloadData.SelectedPage != this.pageWelcome)
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConfigurationSynchronizationAssistant));
    this.assistantDownloadData = new WizardControl();
    this.pageWelcome = new WelcomeWizardPage();
    this.instrumentListView = new ListView();
    this.pageSynchronizationCompleted = new CompletionWizardPage();
    this.labelOperationMessage = new LabelControl();
    this.pageSynchronizing = new DevExpress.XtraWizard.WizardPage();
    this.layoutControl1 = new LayoutControl();
    this.progressConfigurationUpdate = new PictureEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.assistantDownloadData.BeginInit();
    this.assistantDownloadData.SuspendLayout();
    this.pageWelcome.SuspendLayout();
    this.pageSynchronizationCompleted.SuspendLayout();
    this.pageSynchronizing.SuspendLayout();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.progressConfigurationUpdate.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.SuspendLayout();
    this.assistantDownloadData.Controls.Add((Control) this.pageWelcome);
    this.assistantDownloadData.Controls.Add((Control) this.pageSynchronizationCompleted);
    this.assistantDownloadData.Controls.Add((Control) this.pageSynchronizing);
    this.assistantDownloadData.Name = "assistantDownloadData";
    this.assistantDownloadData.Pages.AddRange(new BaseWizardPage[3]
    {
      (BaseWizardPage) this.pageWelcome,
      (BaseWizardPage) this.pageSynchronizing,
      (BaseWizardPage) this.pageSynchronizationCompleted
    });
    componentResourceManager.ApplyResources((object) this.assistantDownloadData, "assistantDownloadData");
    this.assistantDownloadData.WizardStyle = WizardStyle.WizardAero;
    this.assistantDownloadData.SelectedPageChanging += new WizardPageChangingEventHandler(this.OnPageChanging);
    this.assistantDownloadData.CancelClick += new CancelEventHandler(this.OnWizzardCancelClick);
    this.assistantDownloadData.FinishClick += new CancelEventHandler(this.OnWizzardCompleteClick);
    this.assistantDownloadData.VisibleChanged += new EventHandler(this.OnAssistantVisibilityChanged);
    this.pageWelcome.AllowBack = false;
    this.pageWelcome.AllowNext = false;
    this.pageWelcome.Controls.Add((Control) this.instrumentListView);
    componentResourceManager.ApplyResources((object) this.pageWelcome, "pageWelcome");
    this.pageWelcome.Name = "pageWelcome";
    componentResourceManager.ApplyResources((object) this.instrumentListView, "instrumentListView");
    this.instrumentListView.Name = "instrumentListView";
    this.instrumentListView.UseCompatibleStateImageBehavior = false;
    this.instrumentListView.SelectedIndexChanged += new EventHandler(this.InstrumentSelectionChanged);
    this.pageSynchronizationCompleted.AllowBack = false;
    this.pageSynchronizationCompleted.AllowNext = false;
    this.pageSynchronizationCompleted.Controls.Add((Control) this.labelOperationMessage);
    componentResourceManager.ApplyResources((object) this.pageSynchronizationCompleted, "pageSynchronizationCompleted");
    this.pageSynchronizationCompleted.Name = "pageSynchronizationCompleted";
    this.labelOperationMessage.AllowHtmlString = true;
    this.labelOperationMessage.Appearance.ImageAlign = ContentAlignment.TopLeft;
    this.labelOperationMessage.Appearance.Options.UseTextOptions = true;
    this.labelOperationMessage.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
    componentResourceManager.ApplyResources((object) this.labelOperationMessage, "labelOperationMessage");
    this.labelOperationMessage.ImageAlignToText = ImageAlignToText.LeftTop;
    this.labelOperationMessage.Name = "labelOperationMessage";
    this.pageSynchronizing.Controls.Add((Control) this.layoutControl1);
    this.pageSynchronizing.Name = "pageSynchronizing";
    componentResourceManager.ApplyResources((object) this.pageSynchronizing, "pageSynchronizing");
    this.layoutControl1.Controls.Add((Control) this.progressConfigurationUpdate);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.progressConfigurationUpdate, "progressConfigurationUpdate");
    this.progressConfigurationUpdate.Name = "progressConfigurationUpdate";
    this.progressConfigurationUpdate.Properties.Appearance.Font = new Font("Tahoma", 10f);
    this.progressConfigurationUpdate.Properties.Appearance.Options.UseFont = true;
    this.progressConfigurationUpdate.Properties.BorderStyle = BorderStyles.NoBorder;
    this.progressConfigurationUpdate.Properties.NullText = componentResourceManager.GetString("progressConfigurationUpdate.Properties.NullText");
    this.progressConfigurationUpdate.Properties.PictureAlignment = ContentAlignment.MiddleLeft;
    this.progressConfigurationUpdate.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutControlItem2
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "Root";
    this.layoutControlGroup1.Size = new Size(480, 221);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(0, 24);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(460, 177);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    this.layoutControlItem2.AppearanceItemCaption.Font = new Font("Tahoma", 10f);
    this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
    this.layoutControlItem2.Control = (Control) this.progressConfigurationUpdate;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 0);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(460, 24);
    this.layoutControlItem2.TextSize = new Size(126, 16 /*0x10*/);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(0, 58);
    this.emptySpaceItem1.MinSize = new Size(104, 24);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(488, 101);
    this.emptySpaceItem1.SizeConstraintsType = SizeConstraintsType.Custom;
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.assistantDownloadData);
    this.Name = nameof (ConfigurationSynchronizationAssistant);
    this.assistantDownloadData.EndInit();
    this.assistantDownloadData.ResumeLayout(false);
    this.pageWelcome.ResumeLayout(false);
    this.pageSynchronizationCompleted.ResumeLayout(false);
    this.pageSynchronizationCompleted.PerformLayout();
    this.pageSynchronizing.ResumeLayout(false);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.progressConfigurationUpdate.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutControlItem2.EndInit();
    this.emptySpaceItem1.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateFirmwareImportProgressCallBack(List<Type1077Instrument> instrument);

  private delegate void UpdateDataSynchronizationProgressCallBack(
    Instrument instrument,
    TriggerExecutionState state,
    string stateDescription);
}
