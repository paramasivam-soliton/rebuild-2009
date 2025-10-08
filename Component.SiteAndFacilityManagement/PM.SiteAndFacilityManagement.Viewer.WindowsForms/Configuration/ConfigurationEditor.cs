// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Configuration.ConfigurationEditor
// Assembly: PM.SiteAndFacilityManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6D876CF9-7A09-41FB-BA50-FDFF116B5382
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Properties;
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
namespace PathMedical.SiteAndFacilityManagement.Viewer.WindowsForms.Configuration;

[ToolboxItem(false)]
public sealed class ConfigurationEditor : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private ModelMapper<SiteFacilityConfiguration> modelMapper;
  private IContainer components;
  private LayoutControl rootLayoutControl;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlGroup layoutControlGroup2;
  private EmptySpaceItem emptySpaceItem1;
  private DevExpressCheckedEdit activeCheckbox;
  private LayoutControlItem layoutControlItem2;
  private EmptySpaceItem emptySpaceItem2;

  public ConfigurationEditor()
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.CreateRibbonBarCommands();
    this.InitializeModelMapper();
  }

  public ConfigurationEditor(IModel model)
    : this()
  {
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateRibbonBarCommands()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.ConfigurationEditor_RibbonNavigationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.ConfigurationEditor_RibbonModificationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.ConfigurationEditor_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void InitializeModelMapper()
  {
    ModelMapper<SiteFacilityConfiguration> modelMapper = new ModelMapper<SiteFacilityConfiguration>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<SiteFacilityConfiguration, object>>) (s => (object) s.IsSiteFacilityManagementEnabled), (object) this.activeCheckbox);
    this.modelMapper = modelMapper;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.ChangeType != ChangeType.ItemEdited || !(e.Type == typeof (SiteFacilityConfiguration)))
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new ConfigurationEditor.UpdateConfigurationCallBack(this.FillFields), (object) (e.ChangedObject as SiteFacilityConfiguration));
    else
      this.FillFields(e.ChangedObject as SiteFacilityConfiguration);
  }

  private void FillFields(SiteFacilityConfiguration configuration)
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ConfigurationEditor));
    this.rootLayoutControl = new LayoutControl();
    this.activeCheckbox = new DevExpressCheckedEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutControlGroup2 = new LayoutControlGroup();
    this.layoutControlItem2 = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.rootLayoutControl.BeginInit();
    this.rootLayoutControl.SuspendLayout();
    this.activeCheckbox.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutControlGroup2.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.SuspendLayout();
    this.rootLayoutControl.Controls.Add((Control) this.activeCheckbox);
    componentResourceManager.ApplyResources((object) this.rootLayoutControl, "rootLayoutControl");
    this.rootLayoutControl.Name = "rootLayoutControl";
    this.rootLayoutControl.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.activeCheckbox, "activeCheckbox");
    this.activeCheckbox.FormatString = (string) null;
    this.activeCheckbox.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.activeCheckbox.IsReadOnly = false;
    this.activeCheckbox.IsUndoing = false;
    this.activeCheckbox.Name = "activeCheckbox";
    this.activeCheckbox.Properties.Appearance.BorderColor = Color.Yellow;
    this.activeCheckbox.Properties.Appearance.Options.UseBorderColor = true;
    this.activeCheckbox.Properties.BorderStyle = BorderStyles.Simple;
    this.activeCheckbox.Properties.Caption = componentResourceManager.GetString("activeCheckbox.Properties.Caption");
    this.activeCheckbox.StyleController = (IStyleController) this.rootLayoutControl;
    this.activeCheckbox.Validator = (ICustomValidator) null;
    this.activeCheckbox.Value = (object) false;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlGroup2,
      (BaseLayoutItem) this.emptySpaceItem1
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(778, 511 /*0x01FF*/);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup2, "layoutControlGroup2");
    this.layoutControlGroup2.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.emptySpaceItem2
    });
    this.layoutControlGroup2.Location = new Point(0, 0);
    this.layoutControlGroup2.Name = "layoutControlGroup2";
    this.layoutControlGroup2.Size = new Size(758, 69);
    this.layoutControlItem2.Control = (Control) this.activeCheckbox;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(0, 0);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(221, 25);
    this.layoutControlItem2.TextSize = new Size(0, 0);
    this.layoutControlItem2.TextToControlDistance = 0;
    this.layoutControlItem2.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(0, 69);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(758, 422);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(221, 0);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(513, 25);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.rootLayoutControl);
    this.Name = nameof (ConfigurationEditor);
    this.rootLayoutControl.EndInit();
    this.rootLayoutControl.ResumeLayout(false);
    this.activeCheckbox.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutControlGroup2.EndInit();
    this.layoutControlItem2.EndInit();
    this.emptySpaceItem1.EndInit();
    this.emptySpaceItem2.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateConfigurationCallBack(SiteFacilityConfiguration configuration);
}
