// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.AccuLink.WindowsForms.Configuration.AccuLinkDataExchangeConfigurationEditor
// Assembly: PM.DataExchange.AccuLink.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7B9FC5AD-6EE7-4FA1-8083-412FCFB9EB4F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.AccuLink.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout;
using PathMedical.Exception;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.DataExchange.AccuLink.WindowsForms.Configuration;

public sealed class AccuLinkDataExchangeConfigurationEditor : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly DXValidationProvider validationProvider;
  private ModelMapper<AccuLinkDataExchangeConfiguration> modelMapper;
  private IContainer components;
  private LayoutControl rootLayoutControl;
  private LayoutControlGroup layoutRootGroup;
  private DevExpressButtonEdit exportFolder;
  private DevExpressButtonEdit importFolder;
  private LayoutControlGroup layoutImportGroup;
  private LayoutControlItem layoutImportFolder;
  private EmptySpaceItem emptySpaceItem1;
  private LayoutControlGroup layoutExportOptions;
  private LayoutControlItem layoutExportFolder;
  private DevExpressCheckedEdit importRiskIndicators;
  private LayoutControlItem layoutControlItem1;
  private DevExpressCheckedEdit importUsers;
  private LayoutControlItem layoutControlItem2;
  private DevExpressCheckedEdit exportUsers;
  private DevExpressCheckedEdit exportRiskIndicators;
  private LayoutControlItem layoutControlItem3;
  private LayoutControlItem layoutControlItem4;
  private EmptySpaceItem emptySpaceItem2;
  private EmptySpaceItem emptySpaceItem3;
  private DevExpressCheckedEdit exportPredefinedComments;
  private DevExpressCheckedEdit importPredefinedComments;
  private LayoutControlItem layoutControlItem5;
  private LayoutControlItem layoutControlItem6;

  public AccuLinkDataExchangeConfigurationEditor()
  {
    this.InitializeComponent();
    this.CreateToolbarElements();
    this.Dock = DockStyle.Fill;
    this.InitializeModelMapper();
    this.validationProvider = new DXValidationProvider();
  }

  public AccuLinkDataExchangeConfigurationEditor(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateToolbarElements()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup("System Configuration"));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup("Configuration"));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup("Help", (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void InitializeModelMapper()
  {
    ModelMapper<AccuLinkDataExchangeConfiguration> modelMapper = new ModelMapper<AccuLinkDataExchangeConfiguration>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<AccuLinkDataExchangeConfiguration, object>>) (c => c.ImportFolder), (object) this.importFolder);
    modelMapper.Add((Expression<Func<AccuLinkDataExchangeConfiguration, object>>) (c => (object) c.ImportUsers), (object) this.importUsers);
    modelMapper.Add((Expression<Func<AccuLinkDataExchangeConfiguration, object>>) (c => (object) c.ImportRiskIndicators), (object) this.importRiskIndicators);
    modelMapper.Add((Expression<Func<AccuLinkDataExchangeConfiguration, object>>) (c => (object) c.ImportPredefinedComments), (object) this.importPredefinedComments);
    modelMapper.Add((Expression<Func<AccuLinkDataExchangeConfiguration, object>>) (c => c.ExportFolder), (object) this.exportFolder);
    modelMapper.Add((Expression<Func<AccuLinkDataExchangeConfiguration, object>>) (c => (object) c.ExportUsers), (object) this.exportUsers);
    modelMapper.Add((Expression<Func<AccuLinkDataExchangeConfiguration, object>>) (c => (object) c.ExportRiskIndicators), (object) this.exportRiskIndicators);
    modelMapper.Add((Expression<Func<AccuLinkDataExchangeConfiguration, object>>) (c => (object) c.ExportPredefinedComments), (object) this.exportPredefinedComments);
    this.modelMapper = modelMapper;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType != ChangeType.ItemEdited || !(e.Type == typeof (AccuLinkDataExchangeConfiguration)))
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new AccuLinkDataExchangeConfigurationEditor.UpdateEspConfigurationCallBack(this.FillFields), (object) (e.ChangedObject as AccuLinkDataExchangeConfiguration));
    else
      this.FillFields(e.ChangedObject as AccuLinkDataExchangeConfiguration);
  }

  private void FillFields(AccuLinkDataExchangeConfiguration configuration)
  {
    this.modelMapper.CopyModelToUI(configuration);
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  protected override void OnViewModeChanged(EventArgs e)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != 0);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.rootLayoutControl = new LayoutControl();
    this.exportPredefinedComments = new DevExpressCheckedEdit();
    this.importPredefinedComments = new DevExpressCheckedEdit();
    this.exportUsers = new DevExpressCheckedEdit();
    this.exportRiskIndicators = new DevExpressCheckedEdit();
    this.importUsers = new DevExpressCheckedEdit();
    this.importRiskIndicators = new DevExpressCheckedEdit();
    this.exportFolder = new DevExpressButtonEdit();
    this.importFolder = new DevExpressButtonEdit();
    this.layoutRootGroup = new LayoutControlGroup();
    this.layoutImportGroup = new LayoutControlGroup();
    this.layoutImportFolder = new LayoutControlItem();
    this.layoutControlItem1 = new LayoutControlItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutControlItem5 = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutExportOptions = new LayoutControlGroup();
    this.layoutExportFolder = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutControlItem4 = new LayoutControlItem();
    this.emptySpaceItem3 = new EmptySpaceItem();
    this.layoutControlItem6 = new LayoutControlItem();
    this.rootLayoutControl.BeginInit();
    this.rootLayoutControl.SuspendLayout();
    this.exportPredefinedComments.Properties.BeginInit();
    this.importPredefinedComments.Properties.BeginInit();
    this.exportUsers.Properties.BeginInit();
    this.exportRiskIndicators.Properties.BeginInit();
    this.importUsers.Properties.BeginInit();
    this.importRiskIndicators.Properties.BeginInit();
    this.exportFolder.Properties.BeginInit();
    this.importFolder.Properties.BeginInit();
    this.layoutRootGroup.BeginInit();
    this.layoutImportGroup.BeginInit();
    this.layoutImportFolder.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutExportOptions.BeginInit();
    this.layoutExportFolder.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.emptySpaceItem3.BeginInit();
    this.layoutControlItem6.BeginInit();
    this.SuspendLayout();
    this.rootLayoutControl.Controls.Add((Control) this.exportPredefinedComments);
    this.rootLayoutControl.Controls.Add((Control) this.importPredefinedComments);
    this.rootLayoutControl.Controls.Add((Control) this.exportUsers);
    this.rootLayoutControl.Controls.Add((Control) this.exportRiskIndicators);
    this.rootLayoutControl.Controls.Add((Control) this.importUsers);
    this.rootLayoutControl.Controls.Add((Control) this.importRiskIndicators);
    this.rootLayoutControl.Controls.Add((Control) this.exportFolder);
    this.rootLayoutControl.Controls.Add((Control) this.importFolder);
    this.rootLayoutControl.Dock = DockStyle.Fill;
    this.rootLayoutControl.Location = new Point(0, 0);
    this.rootLayoutControl.Name = "rootLayoutControl";
    this.rootLayoutControl.Root = this.layoutRootGroup;
    this.rootLayoutControl.Size = new Size(777, 571);
    this.rootLayoutControl.TabIndex = 0;
    this.rootLayoutControl.Text = "layoutControl1";
    this.exportPredefinedComments.Caption = (string) null;
    this.exportPredefinedComments.FormatString = (string) null;
    this.exportPredefinedComments.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.exportPredefinedComments.IsReadOnly = false;
    this.exportPredefinedComments.IsUndoing = false;
    this.exportPredefinedComments.Location = new Point(24, 257);
    this.exportPredefinedComments.Name = "exportPredefinedComments";
    this.exportPredefinedComments.Properties.Caption = "Export Predefined Comments";
    this.exportPredefinedComments.Size = new Size(221, 19);
    this.exportPredefinedComments.StyleController = (IStyleController) this.rootLayoutControl;
    this.exportPredefinedComments.TabIndex = 11;
    this.exportPredefinedComments.Validator = (ICustomValidator) null;
    this.exportPredefinedComments.Value = (object) false;
    this.importPredefinedComments.Caption = (string) null;
    this.importPredefinedComments.FormatString = (string) null;
    this.importPredefinedComments.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.importPredefinedComments.IsReadOnly = false;
    this.importPredefinedComments.IsUndoing = false;
    this.importPredefinedComments.Location = new Point(24, 118);
    this.importPredefinedComments.Name = "importPredefinedComments";
    this.importPredefinedComments.Properties.BorderStyle = BorderStyles.Simple;
    this.importPredefinedComments.Properties.Caption = "Import Predefined Comments";
    this.importPredefinedComments.Size = new Size(165, 21);
    this.importPredefinedComments.StyleController = (IStyleController) this.rootLayoutControl;
    this.importPredefinedComments.TabIndex = 10;
    this.importPredefinedComments.Validator = (ICustomValidator) null;
    this.importPredefinedComments.Value = (object) false;
    this.exportUsers.Caption = (string) null;
    this.exportUsers.FormatString = (string) null;
    this.exportUsers.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.exportUsers.IsReadOnly = false;
    this.exportUsers.IsUndoing = false;
    this.exportUsers.Location = new Point(24, 234);
    this.exportUsers.Name = "exportUsers";
    this.exportUsers.Properties.Caption = "Export Users";
    this.exportUsers.Size = new Size(221, 19);
    this.exportUsers.StyleController = (IStyleController) this.rootLayoutControl;
    this.exportUsers.TabIndex = 9;
    this.exportUsers.Validator = (ICustomValidator) null;
    this.exportUsers.Value = (object) false;
    this.exportRiskIndicators.Caption = (string) null;
    this.exportRiskIndicators.FormatString = (string) null;
    this.exportRiskIndicators.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.exportRiskIndicators.IsReadOnly = false;
    this.exportRiskIndicators.IsUndoing = false;
    this.exportRiskIndicators.Location = new Point(24, 211);
    this.exportRiskIndicators.Name = "exportRiskIndicators";
    this.exportRiskIndicators.Properties.Caption = "Export Risk Factors";
    this.exportRiskIndicators.Size = new Size(221, 19);
    this.exportRiskIndicators.StyleController = (IStyleController) this.rootLayoutControl;
    this.exportRiskIndicators.TabIndex = 8;
    this.exportRiskIndicators.Validator = (ICustomValidator) null;
    this.exportRiskIndicators.Value = (object) false;
    this.importUsers.Caption = (string) null;
    this.importUsers.FormatString = (string) null;
    this.importUsers.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.importUsers.IsReadOnly = false;
    this.importUsers.IsUndoing = false;
    this.importUsers.Location = new Point(24, 93);
    this.importUsers.Name = "importUsers";
    this.importUsers.Properties.BorderStyle = BorderStyles.Simple;
    this.importUsers.Properties.Caption = "Import Users";
    this.importUsers.Size = new Size(165, 21);
    this.importUsers.StyleController = (IStyleController) this.rootLayoutControl;
    this.importUsers.TabIndex = 7;
    this.importUsers.Validator = (ICustomValidator) null;
    this.importUsers.Value = (object) false;
    this.importRiskIndicators.Caption = (string) null;
    this.importRiskIndicators.FormatString = (string) null;
    this.importRiskIndicators.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.importRiskIndicators.IsReadOnly = false;
    this.importRiskIndicators.IsUndoing = false;
    this.importRiskIndicators.Location = new Point(24, 68);
    this.importRiskIndicators.Name = "importRiskIndicators";
    this.importRiskIndicators.Properties.BorderStyle = BorderStyles.Simple;
    this.importRiskIndicators.Properties.Caption = "Import Risk Factors";
    this.importRiskIndicators.Size = new Size(165, 21);
    this.importRiskIndicators.StyleController = (IStyleController) this.rootLayoutControl;
    this.importRiskIndicators.TabIndex = 6;
    this.importRiskIndicators.Validator = (ICustomValidator) null;
    this.importRiskIndicators.Value = (object) false;
    this.exportFolder.Caption = (string) null;
    this.exportFolder.EditValue = (object) "";
    this.exportFolder.EnterMoveNextControl = true;
    this.exportFolder.FormatString = (string) null;
    this.exportFolder.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.exportFolder.IsReadOnly = false;
    this.exportFolder.IsUndoing = false;
    this.exportFolder.Location = new Point(93, 187);
    this.exportFolder.Name = "exportFolder";
    this.exportFolder.Properties.Appearance.BorderColor = Color.Yellow;
    this.exportFolder.Properties.Appearance.Options.UseBorderColor = true;
    this.exportFolder.Properties.BorderStyle = BorderStyles.Simple;
    this.exportFolder.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.exportFolder.Size = new Size(660, 20);
    this.exportFolder.StyleController = (IStyleController) this.rootLayoutControl;
    this.exportFolder.TabIndex = 5;
    this.exportFolder.Validator = (ICustomValidator) null;
    this.exportFolder.Value = (object) "";
    this.importFolder.Caption = (string) null;
    this.importFolder.EditValue = (object) "";
    this.importFolder.EnterMoveNextControl = true;
    this.importFolder.FormatString = (string) null;
    this.importFolder.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.importFolder.IsReadOnly = false;
    this.importFolder.IsUndoing = false;
    this.importFolder.Location = new Point(93, 44);
    this.importFolder.Name = "importFolder";
    this.importFolder.Properties.Appearance.BorderColor = Color.Yellow;
    this.importFolder.Properties.Appearance.Options.UseBorderColor = true;
    this.importFolder.Properties.BorderStyle = BorderStyles.Simple;
    this.importFolder.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.importFolder.Size = new Size(660, 20);
    this.importFolder.StyleController = (IStyleController) this.rootLayoutControl;
    this.importFolder.TabIndex = 4;
    this.importFolder.Validator = (ICustomValidator) null;
    this.importFolder.Value = (object) "";
    this.layoutRootGroup.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutRootGroup.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    this.layoutRootGroup.CustomizationFormText = "layoutRootGroup";
    this.layoutRootGroup.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutRootGroup.GroupBordersVisible = false;
    this.layoutRootGroup.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutImportGroup,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutExportOptions
    });
    this.layoutRootGroup.Location = new Point(0, 0);
    this.layoutRootGroup.Name = "layoutRootGroup";
    this.layoutRootGroup.Size = new Size(777, 571);
    this.layoutRootGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutRootGroup.Text = "layoutRootGroup";
    this.layoutRootGroup.TextVisible = false;
    this.layoutImportGroup.CustomizationFormText = "Import Options";
    this.layoutImportGroup.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.layoutImportFolder,
      (BaseLayoutItem) this.layoutControlItem1,
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutControlItem5
    });
    this.layoutImportGroup.Location = new Point(0, 0);
    this.layoutImportGroup.Name = "layoutImportGroup";
    this.layoutImportGroup.Size = new Size(757, 143);
    this.layoutImportGroup.Text = "Import Options";
    this.layoutImportFolder.Control = (Control) this.importFolder;
    this.layoutImportFolder.CustomizationFormText = "Import Folder";
    this.layoutImportFolder.Location = new Point(0, 0);
    this.layoutImportFolder.Name = "layoutImportFolder";
    this.layoutImportFolder.Size = new Size(733, 24);
    this.layoutImportFolder.Text = "Import Folder";
    this.layoutImportFolder.TextSize = new Size(65, 13);
    this.layoutControlItem1.Control = (Control) this.importRiskIndicators;
    this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
    this.layoutControlItem1.Location = new Point(0, 24);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(169, 25);
    this.layoutControlItem1.Text = "layoutControlItem1";
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    this.layoutControlItem2.Control = (Control) this.importUsers;
    this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
    this.layoutControlItem2.Location = new Point(0, 49);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(169, 25);
    this.layoutControlItem2.Text = "layoutControlItem2";
    this.layoutControlItem2.TextSize = new Size(0, 0);
    this.layoutControlItem2.TextToControlDistance = 0;
    this.layoutControlItem2.TextVisible = false;
    this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
    this.emptySpaceItem2.Location = new Point(169, 24);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(564, 75);
    this.emptySpaceItem2.Text = "emptySpaceItem2";
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    this.layoutControlItem5.Control = (Control) this.importPredefinedComments;
    this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
    this.layoutControlItem5.Location = new Point(0, 74);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(169, 25);
    this.layoutControlItem5.Text = "layoutControlItem5";
    this.layoutControlItem5.TextSize = new Size(0, 0);
    this.layoutControlItem5.TextToControlDistance = 0;
    this.layoutControlItem5.TextVisible = false;
    this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
    this.emptySpaceItem1.Location = new Point(0, 280);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(757, 271);
    this.emptySpaceItem1.Text = "emptySpaceItem1";
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.layoutExportOptions.CustomizationFormText = "Export Options";
    this.layoutExportOptions.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.layoutExportFolder,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.layoutControlItem4,
      (BaseLayoutItem) this.emptySpaceItem3,
      (BaseLayoutItem) this.layoutControlItem6
    });
    this.layoutExportOptions.Location = new Point(0, 143);
    this.layoutExportOptions.Name = "layoutExportOptions";
    this.layoutExportOptions.Size = new Size(757, 137);
    this.layoutExportOptions.Text = "Export Options";
    this.layoutExportFolder.Control = (Control) this.exportFolder;
    this.layoutExportFolder.CustomizationFormText = "Export Folder";
    this.layoutExportFolder.Location = new Point(0, 0);
    this.layoutExportFolder.Name = "layoutExportFolder";
    this.layoutExportFolder.Size = new Size(733, 24);
    this.layoutExportFolder.Text = "Export Folder";
    this.layoutExportFolder.TextSize = new Size(65, 13);
    this.layoutControlItem3.Control = (Control) this.exportRiskIndicators;
    this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
    this.layoutControlItem3.Location = new Point(0, 24);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(225, 23);
    this.layoutControlItem3.Text = "layoutControlItem3";
    this.layoutControlItem3.TextSize = new Size(0, 0);
    this.layoutControlItem3.TextToControlDistance = 0;
    this.layoutControlItem3.TextVisible = false;
    this.layoutControlItem4.Control = (Control) this.exportUsers;
    this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
    this.layoutControlItem4.Location = new Point(0, 47);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(225, 23);
    this.layoutControlItem4.Text = "layoutControlItem4";
    this.layoutControlItem4.TextSize = new Size(0, 0);
    this.layoutControlItem4.TextToControlDistance = 0;
    this.layoutControlItem4.TextVisible = false;
    this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
    this.emptySpaceItem3.Location = new Point(225, 24);
    this.emptySpaceItem3.Name = "emptySpaceItem3";
    this.emptySpaceItem3.Size = new Size(508, 69);
    this.emptySpaceItem3.Text = "emptySpaceItem3";
    this.emptySpaceItem3.TextSize = new Size(0, 0);
    this.layoutControlItem6.Control = (Control) this.exportPredefinedComments;
    this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
    this.layoutControlItem6.Location = new Point(0, 70);
    this.layoutControlItem6.Name = "layoutControlItem6";
    this.layoutControlItem6.Size = new Size(225, 23);
    this.layoutControlItem6.Text = "layoutControlItem6";
    this.layoutControlItem6.TextSize = new Size(0, 0);
    this.layoutControlItem6.TextToControlDistance = 0;
    this.layoutControlItem6.TextVisible = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.rootLayoutControl);
    this.Name = nameof (AccuLinkDataExchangeConfigurationEditor);
    this.Size = new Size(777, 571);
    this.rootLayoutControl.EndInit();
    this.rootLayoutControl.ResumeLayout(false);
    this.exportPredefinedComments.Properties.EndInit();
    this.importPredefinedComments.Properties.EndInit();
    this.exportUsers.Properties.EndInit();
    this.exportRiskIndicators.Properties.EndInit();
    this.importUsers.Properties.EndInit();
    this.importRiskIndicators.Properties.EndInit();
    this.exportFolder.Properties.EndInit();
    this.importFolder.Properties.EndInit();
    this.layoutRootGroup.EndInit();
    this.layoutImportGroup.EndInit();
    this.layoutImportFolder.EndInit();
    this.layoutControlItem1.EndInit();
    this.layoutControlItem2.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutControlItem5.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutExportOptions.EndInit();
    this.layoutExportFolder.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutControlItem4.EndInit();
    this.emptySpaceItem3.EndInit();
    this.layoutControlItem6.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateEspConfigurationCallBack(
    AccuLinkDataExchangeConfiguration configuration);
}
