// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Communicator.WindowsForms.FirmwareImageImport.FirmwareImportAssistant
// Assembly: PM.Type1077.Communicator.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4173E4B7-BF2C-4E03-A869-8E0498B48F2A
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.Communicator.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;
using DevExpress.XtraWizard;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.Type1077.Communicator.WindowsForms.Properties;
using PathMedical.Type1077.Firmware;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.Type1077.Communicator.WindowsForms.FirmwareImageImport;

[ToolboxItem(false)]
public class FirmwareImportAssistant : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private IContainer components;
  private WizardControl assistantFirmwareImport;
  private WelcomeWizardPage pageWelcome;
  private DevExpress.XtraWizard.WizardPage pageImportInstrumentImage;
  private CompletionWizardPage pageImageImportCompleted;
  private FolderBrowserDialog folderBrowserDialog;
  private LayoutControl layoutControl1;
  private LabelControl importProgressDetails;
  private ProgressBarControl importProgress;
  private LabelControl importProgressTitle;
  private LayoutControlGroup layoutControlGroup2;
  private LayoutControlItem layoutControlItem1;
  private LayoutControlItem layoutControlItem3;
  private LayoutControlItem layoutControlItem4;
  private EmptySpaceItem emptySpaceItem1;
  private EmptySpaceItem emptySpaceItem3;
  private LayoutControl layoutControl2;
  private LayoutControlGroup layoutControlGroup3;
  private ButtonEdit folderName;
  private EmptySpaceItem emptySpaceItem2;
  private LayoutControlItem layoutControlItem2;
  private LabelControl labelOperationMessage;

  public FirmwareImportAssistant()
  {
    this.InitializeComponent();
    this.folderName.Properties.NullValuePrompt = ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("FirmwareImportEnterFolderName");
    this.folderName.ButtonClick += new ButtonPressedEventHandler(this.FolderNameButtonClick);
  }

  public FirmwareImportAssistant(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType != ChangeType.SelectionChanged && e.ChangeType != ChangeType.ItemEdited && e.ChangeType != ChangeType.ItemAdded || !(e.ChangedObject is FirmwareImportProgress))
      return;
    FirmwareImportProgress changedObject = e.ChangedObject as FirmwareImportProgress;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new FirmwareImportAssistant.UpdateFirmwareImportProgressCallBack(this.UpdateFirmwareImportInformation), (object) changedObject);
    else
      this.UpdateFirmwareImportInformation(changedObject);
  }

  private void UpdateFirmwareImportInformation(FirmwareImportProgress progress)
  {
    if (progress == null)
      return;
    switch (progress.ProcessState)
    {
      case ProcessState.Processing:
        this.importProgressTitle.Text = progress.ActivityDescription;
        this.importProgress.Properties.Maximum = progress.TotalFiles;
        this.importProgress.Properties.Minimum = 0;
        this.importProgress.EditValue = (object) progress.ProcessedFiles;
        this.importProgressDetails.Text = progress.CurrentFile;
        break;
      case ProcessState.Cancelled:
        this.pageImportInstrumentImage.AllowNext = false;
        this.pageImportInstrumentImage.AllowBack = false;
        this.pageImportInstrumentImage.AllowCancel = false;
        if (this.pageImageImportCompleted.InvokeRequired)
          this.BeginInvoke((Delegate) new FirmwareImportAssistant.UpdateCompletionPageCalBack(this.UpdateCompletionPage), (object) progress, (object) true, (object) false, (object) false);
        else
          this.UpdateCompletionPage(progress, true, false, false);
        this.assistantFirmwareImport.SelectedPage = (BaseWizardPage) this.pageImageImportCompleted;
        break;
      case ProcessState.Failed:
        this.pageImportInstrumentImage.AllowNext = false;
        this.pageImportInstrumentImage.AllowBack = false;
        this.pageImportInstrumentImage.AllowCancel = false;
        if (this.pageImageImportCompleted.InvokeRequired)
          this.BeginInvoke((Delegate) new FirmwareImportAssistant.UpdateCompletionPageCalBack(this.UpdateCompletionPage), (object) progress, (object) true, (object) false, (object) false);
        else
          this.UpdateCompletionPage(progress, true, false, false);
        this.assistantFirmwareImport.SelectedPage = (BaseWizardPage) this.pageImageImportCompleted;
        break;
      case ProcessState.Completed:
        this.pageImportInstrumentImage.AllowNext = false;
        this.pageImportInstrumentImage.AllowBack = false;
        this.pageImportInstrumentImage.AllowCancel = false;
        if (this.pageImageImportCompleted.InvokeRequired)
          this.BeginInvoke((Delegate) new FirmwareImportAssistant.UpdateCompletionPageCalBack(this.UpdateCompletionPage), (object) progress, (object) true, (object) false, (object) false);
        else
          this.UpdateCompletionPage(progress, true, false, false);
        this.assistantFirmwareImport.SelectedPage = (BaseWizardPage) this.pageImageImportCompleted;
        break;
    }
  }

  private void UpdateCompletionPage(
    FirmwareImportProgress progress,
    bool allowNext,
    bool allowBack,
    bool allowCancel)
  {
    if (progress == null)
      return;
    switch (progress.ProcessState)
    {
      case ProcessState.Cancelled:
        this.labelOperationMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.pageImageImportCompleted.Text = Resources.FirmwareImportAssistant_ImportCanlled;
        this.labelOperationMessage.Text = Resources.FirmwareImportAssistant_ImportCancelledText;
        break;
      case ProcessState.Failed:
        this.pageImageImportCompleted.Text = Resources.FirmwareImportAssistant_ImportFailed;
        this.labelOperationMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationFailed") as Bitmap);
        this.labelOperationMessage.Text = progress.ActivityDescription;
        break;
      case ProcessState.Completed:
        this.labelOperationMessage.Appearance.Image = (Image) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_OperationSuccessfully") as Bitmap);
        this.pageImageImportCompleted.Text = Resources.FirmwareImportAssistant_ImportComplete;
        if (progress.SuccessfullyImported != null && progress.SuccessfullyImported.Length != 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          stringBuilder.AppendFormat(Resources.FirmwareImportAssistant_ImportCompleteText, (object) progress.SuccessfullyImported.Length);
          foreach (string str in progress.SuccessfullyImported)
            stringBuilder.AppendFormat("<br>{0}", (object) str);
          this.labelOperationMessage.Text = stringBuilder.ToString();
          break;
        }
        this.labelOperationMessage.Text = Resources.FirmwareImportAssistant_NoFirmwareFound;
        break;
    }
    this.pageImageImportCompleted.AllowNext = allowNext;
    this.pageImageImportCompleted.AllowBack = allowBack;
    this.pageImageImportCompleted.AllowCancel = allowCancel;
  }

  private void AssistantPageChanging(object sender, WizardPageChangingEventArgs e)
  {
    if (e.Page != this.pageImportInstrumentImage)
      return;
    if (string.IsNullOrEmpty(this.folderName.Text))
      e.Cancel = true;
    else
      this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Import, (TriggerContext) new FirmwareImportTriggerContext(this.folderName.Text, false)));
  }

  private void VisibilityChanged(object sender, EventArgs e)
  {
    if (this.assistantFirmwareImport == null)
      return;
    this.assistantFirmwareImport.SelectedPage = (BaseWizardPage) this.pageWelcome;
  }

  private void FolderNameButtonClick(object sender, ButtonPressedEventArgs e)
  {
    if (e == null || e.Button == null || e.Button.Kind != ButtonPredefines.Ellipsis)
      return;
    this.folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
    this.folderBrowserDialog.SelectedPath = this.folderName.Text;
    if (this.folderBrowserDialog.ShowDialog() != DialogResult.OK)
      return;
    this.folderName.Text = this.folderBrowserDialog.SelectedPath;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FirmwareImportAssistant));
    this.assistantFirmwareImport = new WizardControl();
    this.pageWelcome = new WelcomeWizardPage();
    this.layoutControl2 = new LayoutControl();
    this.folderName = new ButtonEdit();
    this.layoutControlGroup3 = new LayoutControlGroup();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.pageImportInstrumentImage = new DevExpress.XtraWizard.WizardPage();
    this.layoutControl1 = new LayoutControl();
    this.importProgressDetails = new LabelControl();
    this.importProgress = new ProgressBarControl();
    this.importProgressTitle = new LabelControl();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.emptySpaceItem3 = new EmptySpaceItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutControlItem4 = new LayoutControlItem();
    this.pageImageImportCompleted = new CompletionWizardPage();
    this.labelOperationMessage = new LabelControl();
    this.folderBrowserDialog = new FolderBrowserDialog();
    this.assistantFirmwareImport.BeginInit();
    this.assistantFirmwareImport.SuspendLayout();
    this.pageWelcome.SuspendLayout();
    this.layoutControl2.BeginInit();
    this.layoutControl2.SuspendLayout();
    this.folderName.Properties.BeginInit();
    this.layoutControlGroup3.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.pageImportInstrumentImage.SuspendLayout();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.importProgress.Properties.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.emptySpaceItem3.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.pageImageImportCompleted.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.assistantFirmwareImport, "assistantFirmwareImport");
    this.assistantFirmwareImport.Controls.Add((Control) this.pageWelcome);
    this.assistantFirmwareImport.Controls.Add((Control) this.pageImportInstrumentImage);
    this.assistantFirmwareImport.Controls.Add((Control) this.pageImageImportCompleted);
    this.assistantFirmwareImport.LookAndFeel.SkinName = "Seven";
    this.assistantFirmwareImport.Name = "assistantFirmwareImport";
    this.assistantFirmwareImport.Pages.AddRange(new BaseWizardPage[3]
    {
      (BaseWizardPage) this.pageWelcome,
      (BaseWizardPage) this.pageImportInstrumentImage,
      (BaseWizardPage) this.pageImageImportCompleted
    });
    this.assistantFirmwareImport.WizardStyle = WizardStyle.WizardAero;
    this.assistantFirmwareImport.SelectedPageChanging += new WizardPageChangingEventHandler(this.AssistantPageChanging);
    this.assistantFirmwareImport.CancelClick += new CancelEventHandler(this.CancelClick);
    this.assistantFirmwareImport.PrevClick += new WizardCommandButtonClickEventHandler(this.PreviousClick);
    this.assistantFirmwareImport.VisibleChanged += new EventHandler(this.VisibilityChanged);
    componentResourceManager.ApplyResources((object) this.pageWelcome, "pageWelcome");
    this.pageWelcome.Controls.Add((Control) this.layoutControl2);
    this.pageWelcome.Name = "pageWelcome";
    componentResourceManager.ApplyResources((object) this.layoutControl2, "layoutControl2");
    this.layoutControl2.Controls.Add((Control) this.folderName);
    this.layoutControl2.Name = "layoutControl2";
    this.layoutControl2.Root = this.layoutControlGroup3;
    componentResourceManager.ApplyResources((object) this.folderName, "folderName");
    this.folderName.Name = "folderName";
    this.folderName.Properties.AccessibleDescription = componentResourceManager.GetString("folderName.Properties.AccessibleDescription");
    this.folderName.Properties.AccessibleName = componentResourceManager.GetString("folderName.Properties.AccessibleName");
    this.folderName.Properties.AutoHeight = (bool) componentResourceManager.GetObject("folderName.Properties.AutoHeight");
    this.folderName.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.folderName.Properties.Mask.AutoComplete = (AutoCompleteType) componentResourceManager.GetObject("folderName.Properties.Mask.AutoComplete");
    this.folderName.Properties.Mask.BeepOnError = (bool) componentResourceManager.GetObject("folderName.Properties.Mask.BeepOnError");
    this.folderName.Properties.Mask.EditMask = componentResourceManager.GetString("folderName.Properties.Mask.EditMask");
    this.folderName.Properties.Mask.IgnoreMaskBlank = (bool) componentResourceManager.GetObject("folderName.Properties.Mask.IgnoreMaskBlank");
    this.folderName.Properties.Mask.MaskType = (MaskType) componentResourceManager.GetObject("folderName.Properties.Mask.MaskType");
    this.folderName.Properties.Mask.PlaceHolder = (char) componentResourceManager.GetObject("folderName.Properties.Mask.PlaceHolder");
    this.folderName.Properties.Mask.SaveLiteral = (bool) componentResourceManager.GetObject("folderName.Properties.Mask.SaveLiteral");
    this.folderName.Properties.Mask.ShowPlaceHolders = (bool) componentResourceManager.GetObject("folderName.Properties.Mask.ShowPlaceHolders");
    this.folderName.Properties.Mask.UseMaskAsDisplayFormat = (bool) componentResourceManager.GetObject("folderName.Properties.Mask.UseMaskAsDisplayFormat");
    this.folderName.Properties.NullValuePrompt = componentResourceManager.GetString("folderName.Properties.NullValuePrompt");
    this.folderName.Properties.NullValuePromptShowForEmptyValue = (bool) componentResourceManager.GetObject("folderName.Properties.NullValuePromptShowForEmptyValue");
    this.folderName.StyleController = (IStyleController) this.layoutControl2;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup3, "layoutControlGroup3");
    this.layoutControlGroup3.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup3.GroupBordersVisible = false;
    this.layoutControlGroup3.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutControlItem2
    });
    this.layoutControlGroup3.Location = new Point(0, 0);
    this.layoutControlGroup3.Name = "layoutControlGroup3";
    this.layoutControlGroup3.Size = new Size(415, 167);
    this.layoutControlGroup3.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup3.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(0, 24);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(395, 123);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    this.layoutControlItem2.Control = (Control) this.folderName;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 0);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(395, 24);
    this.layoutControlItem2.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this.pageImportInstrumentImage, "pageImportInstrumentImage");
    this.pageImportInstrumentImage.AllowBack = false;
    this.pageImportInstrumentImage.AllowNext = false;
    this.pageImportInstrumentImage.Controls.Add((Control) this.layoutControl1);
    this.pageImportInstrumentImage.Name = "pageImportInstrumentImage";
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Controls.Add((Control) this.importProgressDetails);
    this.layoutControl1.Controls.Add((Control) this.importProgress);
    this.layoutControl1.Controls.Add((Control) this.importProgressTitle);
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup2;
    componentResourceManager.ApplyResources((object) this.importProgressDetails, "importProgressDetails");
    this.importProgressDetails.AutoEllipsis = true;
    this.importProgressDetails.Name = "importProgressDetails";
    this.importProgressDetails.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.importProgress, "importProgress");
    this.importProgress.Name = "importProgress";
    this.importProgress.Properties.AccessibleDescription = componentResourceManager.GetString("importProgress.Properties.AccessibleDescription");
    this.importProgress.Properties.AccessibleName = componentResourceManager.GetString("importProgress.Properties.AccessibleName");
    this.importProgress.Properties.AutoHeight = (bool) componentResourceManager.GetObject("importProgress.Properties.AutoHeight");
    this.importProgress.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.importProgressTitle, "importProgressTitle");
    this.importProgressTitle.Name = "importProgressTitle";
    this.importProgressTitle.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup2.GroupBordersVisible = false;
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.emptySpaceItem3,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutControlItem4
    });
    this.layoutControlGroup2.Location = new Point(0, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(415, 167);
    this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup2.TextVisible = false;
    this.layoutControlItem1.Control = (Control) this.importProgressTitle;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 33);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(395, 17);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    this.layoutControlItem3.Control = (Control) this.importProgress;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(0, 50);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(395, 19);
    this.layoutControlItem3.TextSize = new Size(0, 0);
    this.layoutControlItem3.TextToControlDistance = 0;
    this.layoutControlItem3.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem3, "emptySpaceItem3");
    this.emptySpaceItem3.Location = new Point(0, 86);
    this.emptySpaceItem3.Name = "emptySpaceItem3";
    this.emptySpaceItem3.Size = new Size(395, 61);
    this.emptySpaceItem3.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(0, 0);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(395, 33);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.layoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlItem4.AppearanceItemCaption.TextOptions.WordWrap = WordWrap.Wrap;
    this.layoutControlItem4.Control = (Control) this.importProgressDetails;
    componentResourceManager.ApplyResources((object) this.layoutControlItem4, "layoutControlItem4");
    this.layoutControlItem4.Location = new Point(0, 69);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(395, 17);
    this.layoutControlItem4.TextSize = new Size(0, 0);
    this.layoutControlItem4.TextToControlDistance = 0;
    this.layoutControlItem4.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.pageImageImportCompleted, "pageImageImportCompleted");
    this.pageImageImportCompleted.AllowBack = false;
    this.pageImageImportCompleted.Controls.Add((Control) this.labelOperationMessage);
    this.pageImageImportCompleted.Name = "pageImageImportCompleted";
    componentResourceManager.ApplyResources((object) this.labelOperationMessage, "labelOperationMessage");
    this.labelOperationMessage.AllowHtmlString = true;
    this.labelOperationMessage.Appearance.ImageAlign = ContentAlignment.TopLeft;
    this.labelOperationMessage.Appearance.Options.UseTextOptions = true;
    this.labelOperationMessage.Appearance.TextOptions.VAlignment = VertAlignment.Top;
    this.labelOperationMessage.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
    this.labelOperationMessage.ImageAlignToText = ImageAlignToText.LeftTop;
    this.labelOperationMessage.Name = "labelOperationMessage";
    componentResourceManager.ApplyResources((object) this.folderBrowserDialog, "folderBrowserDialog");
    this.folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
    this.folderBrowserDialog.ShowNewFolderButton = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.assistantFirmwareImport);
    this.Name = nameof (FirmwareImportAssistant);
    this.assistantFirmwareImport.EndInit();
    this.assistantFirmwareImport.ResumeLayout(false);
    this.pageWelcome.ResumeLayout(false);
    this.layoutControl2.EndInit();
    this.layoutControl2.ResumeLayout(false);
    this.folderName.Properties.EndInit();
    this.layoutControlGroup3.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutControlItem2.EndInit();
    this.pageImportInstrumentImage.ResumeLayout(false);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.importProgress.Properties.EndInit();
    this.layoutControlGroup2.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlItem3.EndInit();
    this.emptySpaceItem3.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutControlItem4.EndInit();
    this.pageImageImportCompleted.ResumeLayout(false);
    this.pageImageImportCompleted.PerformLayout();
    this.ResumeLayout(false);
  }

  private delegate void UpdateFirmwareImportProgressCallBack(
    FirmwareImportProgress firmwareImportProgress);

  private delegate void UpdateCompletionPageCalBack(
    FirmwareImportProgress progress,
    bool allowNext,
    bool allowBack,
    bool allowCancel);
}
