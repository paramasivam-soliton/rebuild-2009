// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.DeviceSelectorAsistant.DeviceSelectorAssistant
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraWizard;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.Type1077;
using PathMedical.Type1077.Automaton;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.DeviceSelectorAsistant;

public class DeviceSelectorAssistant : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private bool _pageChangedHandled;
  private IContainer components;
  private WizardControl assistantDeviceSelection;
  private WelcomeWizardPage pageWelcome;
  private ListView instrumentListView;
  private CompletionWizardPage pageSynchronizationCompleted;
  private LabelControl labelDownLoadOperationMessage;

  public DeviceSelectorAssistant() => this.InitializeComponent();

  public DeviceSelectorAssistant(IModel model)
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
      this.BeginInvoke((Delegate) new DeviceSelectorAssistant.UpdateInstrumentListCallBack(this.UpdateInstrumentList), (object) changedObject);
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
    if (this._pageChangedHandled || e.Page != this.pageWelcome)
      return;
    this.instrumentListView.Clear();
    this.RequestControllerAction((object) this, new TriggerEventArgs(InstrumentManagementTriggers.StartInstrumentSearch, (TriggerContext) null, true, new EventHandler<TriggerExecutedEventArgs>(this.OnInstrumentSearchCompleted)));
  }

  private void OnWizzardCompleteClick(object sender, CancelEventArgs e)
  {
    try
    {
      this._pageChangedHandled = true;
      this.assistantDeviceSelection.SelectedPage = (BaseWizardPage) this.pageWelcome;
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
      this.assistantDeviceSelection.SelectedPage = (BaseWizardPage) this.pageWelcome;
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
      this.BeginInvoke((Delegate) new DeviceSelectorAssistant.UpdateDataDownloadCallBack(this.UpdateDataDownloadProgress), (object) instrument, (object) e.State, (object) e.Text);
    else
      this.UpdateDataDownloadProgress(instrument, e.State, e.Text);
  }

  private void UpdateDataDownloadProgress(
    Type1077Instrument instrument,
    TriggerExecutionState state,
    string stateDescription)
  {
    this.labelDownLoadOperationMessage.Text = string.Empty;
    if (instrument == null || state != TriggerExecutionState.Executed)
      return;
    this.pageSynchronizationCompleted.Text = "AccuScreen selection successfully";
    this.pageSynchronizationCompleted.AllowNext = true;
    this.pageSynchronizationCompleted.AllowBack = false;
    this.pageSynchronizationCompleted.AllowCancel = false;
    this.assistantDeviceSelection.SelectedPage = (BaseWizardPage) this.pageSynchronizationCompleted;
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
    if (!this.assistantDeviceSelection.Visible || this.assistantDeviceSelection.SelectedPage != this.pageWelcome)
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
    this.assistantDeviceSelection = new WizardControl();
    this.pageWelcome = new WelcomeWizardPage();
    this.instrumentListView = new ListView();
    this.pageSynchronizationCompleted = new CompletionWizardPage();
    this.labelDownLoadOperationMessage = new LabelControl();
    this.assistantDeviceSelection.BeginInit();
    this.assistantDeviceSelection.SuspendLayout();
    this.pageWelcome.SuspendLayout();
    this.pageSynchronizationCompleted.SuspendLayout();
    this.SuspendLayout();
    this.assistantDeviceSelection.Controls.Add((Control) this.pageWelcome);
    this.assistantDeviceSelection.Controls.Add((Control) this.pageSynchronizationCompleted);
    this.assistantDeviceSelection.Name = "assistantDeviceSelection";
    this.assistantDeviceSelection.Pages.AddRange(new BaseWizardPage[2]
    {
      (BaseWizardPage) this.pageWelcome,
      (BaseWizardPage) this.pageSynchronizationCompleted
    });
    this.assistantDeviceSelection.Text = "Instrument Selection Assistant";
    this.assistantDeviceSelection.WizardStyle = WizardStyle.WizardAero;
    this.assistantDeviceSelection.SelectedPageChanging += new WizardPageChangingEventHandler(this.OnPageChanging);
    this.assistantDeviceSelection.CancelClick += new CancelEventHandler(this.OnWizzardCancelClick);
    this.assistantDeviceSelection.FinishClick += new CancelEventHandler(this.OnWizzardCompleteClick);
    this.assistantDeviceSelection.VisibleChanged += new EventHandler(this.OnAssistantVisibilityChanged);
    this.pageWelcome.AllowBack = false;
    this.pageWelcome.AllowNext = false;
    this.pageWelcome.Controls.Add((Control) this.instrumentListView);
    this.pageWelcome.IntroductionText = "";
    this.pageWelcome.Name = "pageWelcome";
    this.pageWelcome.Size = new Size(557, 247);
    this.pageWelcome.Text = "Select an Instrument and click next to continue";
    this.instrumentListView.Dock = DockStyle.Fill;
    this.instrumentListView.Location = new Point(0, 0);
    this.instrumentListView.Name = "instrumentListView";
    this.instrumentListView.Size = new Size(557, 247);
    this.instrumentListView.TabIndex = 1;
    this.instrumentListView.UseCompatibleStateImageBehavior = false;
    this.instrumentListView.SelectedIndexChanged += new EventHandler(this.InstrumentSelectionChanged);
    this.pageSynchronizationCompleted.AllowBack = false;
    this.pageSynchronizationCompleted.AllowNext = false;
    this.pageSynchronizationCompleted.Controls.Add((Control) this.labelDownLoadOperationMessage);
    this.pageSynchronizationCompleted.FinishText = "";
    this.pageSynchronizationCompleted.Name = "pageSynchronizationCompleted";
    this.pageSynchronizationCompleted.ProceedText = "";
    this.pageSynchronizationCompleted.Size = new Size(557, 247);
    this.pageSynchronizationCompleted.Text = "Instrument selection has been completed.";
    this.labelDownLoadOperationMessage.AllowHtmlString = true;
    this.labelDownLoadOperationMessage.Appearance.ImageAlign = ContentAlignment.TopLeft;
    this.labelDownLoadOperationMessage.Appearance.Options.UseTextOptions = true;
    this.labelDownLoadOperationMessage.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
    this.labelDownLoadOperationMessage.Dock = DockStyle.Fill;
    this.labelDownLoadOperationMessage.ImageAlignToText = ImageAlignToText.LeftTop;
    this.labelDownLoadOperationMessage.Location = new Point(0, 0);
    this.labelDownLoadOperationMessage.Name = "labelDownLoadOperationMessage";
    this.labelDownLoadOperationMessage.Size = new Size(51, 14);
    this.labelDownLoadOperationMessage.TabIndex = 0;
    this.labelDownLoadOperationMessage.Text = "Completed";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.assistantDeviceSelection);
    this.Name = nameof (DeviceSelectorAssistant);
    this.Size = new Size(617, 409);
    this.assistantDeviceSelection.EndInit();
    this.assistantDeviceSelection.ResumeLayout(false);
    this.pageWelcome.ResumeLayout(false);
    this.pageSynchronizationCompleted.ResumeLayout(false);
    this.pageSynchronizationCompleted.PerformLayout();
    this.ResumeLayout(false);
  }

  private delegate void UpdateDataDownloadCallBack(
    Type1077Instrument instrument,
    TriggerExecutionState state,
    string stateDescription);

  private delegate void UpdateInstrumentListCallBack(List<Type1077Instrument> instrument);
}
