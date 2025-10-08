// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PredefinedCommentManagement.PredefinedCommentMasterDetailBrowser
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.PatientManagement.Viewer.WindowsForms.Properties;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PredefinedCommentManagement;

[ToolboxItem(false)]
public sealed class PredefinedCommentMasterDetailBrowser : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private readonly DevExpressSingleSelectionGridViewHelper<PredefinedComment> commentsSingleSelectionGridHelper;
  private ModelMapper<PredefinedComment> modelMapper;
  private bool processingLangaugeChange;
  private IContainer components;
  private LayoutControl commentMaintenanceViewLayout;
  private GridControl predefinedGridControl;
  private GridView commentGridView;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlItem layoutControlItem1;
  private DevExpressTextEdit comment;
  private LayoutControlItem layoutComment;
  private LayoutControlGroup commentDetailGroup;
  private DevExpressComboBoxEdit languageSelection;
  private LayoutControlItem layoutLanguageSelection;
  private DevExpressTextEdit commentTranslation;
  private LayoutControlGroup layoutGroupInternationalization;
  private LayoutControlItem layoutCommentInternationalization;
  private EmptySpaceItem emptySpaceItem2;
  private GridColumn columnComment;
  private DevExpressComboBoxEdit isActive;
  private LayoutControlItem layoutIsActive;
  private EmptySpaceItem emptySpaceItem1;
  private EmptySpaceItem emptySpaceItem3;
  private LayoutControlGroup layoutGroupPredefinedComments;

  public PredefinedCommentMasterDetailBrowser()
  {
    this.InitializeComponent();
    this.CreateToolbarElements();
    this.Dock = DockStyle.Fill;
    this.InitializeSelectionValues();
    this.InitializeModelMapper();
  }

  public PredefinedCommentMasterDetailBrowser(IModel model)
    : this()
  {
    this.commentsSingleSelectionGridHelper = model != null ? new DevExpressSingleSelectionGridViewHelper<PredefinedComment>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.commentGridView, model) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void CreateToolbarElements()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    this.ToolbarElements.Add((object) ribbonHelper.CreateNavigationGroup(Resources.PredefinedCommentMasterDetailBrowser_RibbonNavigationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateMaintenanceGroup(Resources.PredefinedCommentMasterDetailBrowser_RibbonMaintenanceGroup, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_CommentAdd") as Bitmap, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_CommentEdit") as Bitmap, ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_CommentDelete") as Bitmap));
    this.ToolbarElements.Add((object) ribbonHelper.CreateModificationGroup(Resources.PredefinedCommentMasterDetailBrowser_RibbonModificationGroup));
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(Resources.PredefinedCommentMasterDetailBrowser_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if ((e.ChangeType == ChangeType.SelectionChanged || e.ChangeType == ChangeType.ItemEdited || e.ChangeType == ChangeType.ItemAdded) && e.Type == typeof (PredefinedComment) && !e.IsList)
      this.FillFields(e.ChangedObject as PredefinedComment);
    if (e.ChangeType != ChangeType.SelectionChanged && e.ChangeType != ChangeType.ItemEdited && e.ChangeType != ChangeType.ItemAdded || !(e.Type == typeof (PredefinedComment)) || !e.IsList || !(e.ChangedObject is IEnumerable<PredefinedComment> changedObject) || changedObject.Count<PredefinedComment>() <= 0)
      return;
    this.FillFields(changedObject.FirstOrDefault<PredefinedComment>());
  }

  private void FillFields(PredefinedComment predefinedComment)
  {
    this.modelMapper.SetUIEnabled(predefinedComment != null && this.ViewMode != 0);
    try
    {
      this.processingLangaugeChange = true;
      this.modelMapper.CopyModelToUI(predefinedComment);
    }
    finally
    {
      this.processingLangaugeChange = false;
    }
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void InitializeSelectionValues()
  {
    this.languageSelection.DataSource = (object) SystemConfigurationManager.Instance.OptionalLanguages.Select<CultureInfo, ComboBoxEditItemWrapper>((Func<CultureInfo, ComboBoxEditItemWrapper>) (sl => new ComboBoxEditItemWrapper(sl.DisplayName == sl.EnglishName ? sl.DisplayName : $"{sl.DisplayName} ({sl.EnglishName})", (object) sl))).Distinct<ComboBoxEditItemWrapper>().OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
    this.isActive.DataSource = (object) ((IEnumerable<bool>) new bool[2]
    {
      true,
      false
    }).Select<bool, ComboBoxEditItemWrapper>((Func<bool, ComboBoxEditItemWrapper>) (sl => new ComboBoxEditItemWrapper(sl))).ToList<ComboBoxEditItemWrapper>();
  }

  private void InitializeModelMapper()
  {
    ModelMapper<PredefinedComment> modelMapper = new ModelMapper<PredefinedComment>(this.ViewMode != 0);
    modelMapper.Add((Expression<Func<PredefinedComment, object>>) (c => c.BaseText), (object) this.comment);
    modelMapper.Add((Expression<Func<PredefinedComment, object>>) (c => (object) c.IsActive), (object) this.isActive);
    modelMapper.Add((Expression<Func<PredefinedComment, object>>) (c => c.TranslationCulture), (object) this.languageSelection);
    modelMapper.Add((Expression<Func<PredefinedComment, object>>) (c => c.TranslationName), (object) this.commentTranslation);
    this.modelMapper = modelMapper;
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != 0);
  }

  private void OnLanguageTranslationChanging(object sender, ChangingEventArgs e)
  {
    if (this.processingLangaugeChange || e.NewValue == null)
      return;
    if (e.NewValue.Equals(e.OldValue))
      return;
    try
    {
      this.processingLangaugeChange = true;
      if (!(e.NewValue is ComboBoxEditItemWrapper newValue))
        return;
      ChangeCultureTriggerContext context = new ChangeCultureTriggerContext(newValue.Value as CultureInfo);
      this.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.ChangeLanguage, (TriggerContext) context));
    }
    finally
    {
      this.processingLangaugeChange = false;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PredefinedCommentMasterDetailBrowser));
    this.commentMaintenanceViewLayout = new LayoutControl();
    this.isActive = new DevExpressComboBoxEdit();
    this.commentTranslation = new DevExpressTextEdit();
    this.languageSelection = new DevExpressComboBoxEdit();
    this.comment = new DevExpressTextEdit();
    this.predefinedGridControl = new GridControl();
    this.commentGridView = new GridView();
    this.columnComment = new GridColumn();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.commentDetailGroup = new LayoutControlGroup();
    this.layoutComment = new LayoutControlItem();
    this.layoutIsActive = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutGroupInternationalization = new LayoutControlGroup();
    this.layoutLanguageSelection = new LayoutControlItem();
    this.layoutCommentInternationalization = new LayoutControlItem();
    this.emptySpaceItem3 = new EmptySpaceItem();
    this.layoutGroupPredefinedComments = new LayoutControlGroup();
    this.layoutControlItem1 = new LayoutControlItem();
    this.commentMaintenanceViewLayout.BeginInit();
    this.commentMaintenanceViewLayout.SuspendLayout();
    this.isActive.Properties.BeginInit();
    this.commentTranslation.Properties.BeginInit();
    this.languageSelection.Properties.BeginInit();
    this.comment.Properties.BeginInit();
    this.predefinedGridControl.BeginInit();
    this.commentGridView.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.commentDetailGroup.BeginInit();
    this.layoutComment.BeginInit();
    this.layoutIsActive.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutGroupInternationalization.BeginInit();
    this.layoutLanguageSelection.BeginInit();
    this.layoutCommentInternationalization.BeginInit();
    this.emptySpaceItem3.BeginInit();
    this.layoutGroupPredefinedComments.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.SuspendLayout();
    this.commentMaintenanceViewLayout.Controls.Add((Control) this.isActive);
    this.commentMaintenanceViewLayout.Controls.Add((Control) this.commentTranslation);
    this.commentMaintenanceViewLayout.Controls.Add((Control) this.languageSelection);
    this.commentMaintenanceViewLayout.Controls.Add((Control) this.comment);
    this.commentMaintenanceViewLayout.Controls.Add((Control) this.predefinedGridControl);
    componentResourceManager.ApplyResources((object) this.commentMaintenanceViewLayout, "commentMaintenanceViewLayout");
    this.commentMaintenanceViewLayout.Name = "commentMaintenanceViewLayout";
    this.commentMaintenanceViewLayout.Root = this.layoutControlGroup1;
    this.isActive.EnterMoveNextControl = true;
    this.isActive.FormatString = (string) null;
    this.isActive.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.isActive.IsReadOnly = false;
    this.isActive.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.isActive, "isActive");
    this.isActive.Name = "isActive";
    this.isActive.Properties.Appearance.BorderColor = Color.LightGray;
    this.isActive.Properties.Appearance.Options.UseBorderColor = true;
    this.isActive.Properties.BorderStyle = BorderStyles.Simple;
    this.isActive.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("isActive.Properties.Buttons"))
    });
    this.isActive.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.isActive.ShowEmptyElement = false;
    this.isActive.StyleController = (IStyleController) this.commentMaintenanceViewLayout;
    this.isActive.Validator = (ICustomValidator) null;
    this.isActive.Value = (object) null;
    componentResourceManager.ApplyResources((object) this.commentTranslation, "commentTranslation");
    this.commentTranslation.EnterMoveNextControl = true;
    this.commentTranslation.FormatString = (string) null;
    this.commentTranslation.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.commentTranslation.IsReadOnly = false;
    this.commentTranslation.IsUndoing = false;
    this.commentTranslation.Name = "commentTranslation";
    this.commentTranslation.Properties.Appearance.BorderColor = Color.Yellow;
    this.commentTranslation.Properties.Appearance.Options.UseBorderColor = true;
    this.commentTranslation.Properties.BorderStyle = BorderStyles.Simple;
    this.commentTranslation.StyleController = (IStyleController) this.commentMaintenanceViewLayout;
    this.commentTranslation.Validator = (ICustomValidator) null;
    this.commentTranslation.Value = (object) "";
    this.languageSelection.EnterMoveNextControl = true;
    this.languageSelection.FormatString = (string) null;
    this.languageSelection.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.languageSelection.IsNavigationOnly = true;
    this.languageSelection.IsReadOnly = false;
    this.languageSelection.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.languageSelection, "languageSelection");
    this.languageSelection.Name = "languageSelection";
    this.languageSelection.Properties.Appearance.BorderColor = Color.LightGray;
    this.languageSelection.Properties.Appearance.Options.UseBorderColor = true;
    this.languageSelection.Properties.BorderStyle = BorderStyles.Simple;
    this.languageSelection.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("languageSelection.Properties.Buttons"))
    });
    this.languageSelection.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.languageSelection.ShowEmptyElement = false;
    this.languageSelection.ShowModified = false;
    this.languageSelection.StyleController = (IStyleController) this.commentMaintenanceViewLayout;
    this.languageSelection.Validator = (ICustomValidator) null;
    this.languageSelection.Value = (object) null;
    this.languageSelection.EditValueChanging += new ChangingEventHandler(this.OnLanguageTranslationChanging);
    componentResourceManager.ApplyResources((object) this.comment, "comment");
    this.comment.EnterMoveNextControl = true;
    this.comment.FormatString = (string) null;
    this.comment.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.comment.IsMandatory = true;
    this.comment.IsReadOnly = false;
    this.comment.IsUndoing = false;
    this.comment.Name = "comment";
    this.comment.Properties.Appearance.BackColor = Color.LightYellow;
    this.comment.Properties.Appearance.BorderColor = Color.Yellow;
    this.comment.Properties.Appearance.Options.UseBackColor = true;
    this.comment.Properties.Appearance.Options.UseBorderColor = true;
    this.comment.Properties.BorderStyle = BorderStyles.Simple;
    this.comment.StyleController = (IStyleController) this.commentMaintenanceViewLayout;
    this.comment.Validator = (ICustomValidator) null;
    this.comment.Value = (object) "";
    componentResourceManager.ApplyResources((object) this.predefinedGridControl, "predefinedGridControl");
    this.predefinedGridControl.MainView = (BaseView) this.commentGridView;
    this.predefinedGridControl.Name = "predefinedGridControl";
    this.predefinedGridControl.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.commentGridView
    });
    this.commentGridView.Columns.AddRange(new GridColumn[1]
    {
      this.columnComment
    });
    this.commentGridView.GridControl = this.predefinedGridControl;
    this.commentGridView.Name = "commentGridView";
    this.commentGridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;
    this.commentGridView.OptionsBehavior.AllowDeleteRows = DefaultBoolean.False;
    this.commentGridView.OptionsBehavior.Editable = false;
    this.commentGridView.OptionsCustomization.AllowFilter = false;
    this.commentGridView.OptionsFilter.AllowFilterEditor = false;
    this.commentGridView.OptionsMenu.EnableColumnMenu = false;
    this.commentGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.commentGridView.OptionsView.EnableAppearanceEvenRow = true;
    this.commentGridView.OptionsView.EnableAppearanceOddRow = true;
    this.commentGridView.OptionsView.ShowDetailButtons = false;
    this.commentGridView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.columnComment, "columnComment");
    this.columnComment.FieldName = "BaseText";
    this.columnComment.Name = "columnComment";
    this.layoutControlGroup1.AppearanceItemCaption.Options.UseTextOptions = true;
    this.layoutControlGroup1.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Far;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.commentDetailGroup,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutGroupInternationalization,
      (BaseLayoutItem) this.layoutGroupPredefinedComments
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(960, 750);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.commentDetailGroup, "commentDetailGroup");
    this.commentDetailGroup.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutComment,
      (BaseLayoutItem) this.layoutIsActive,
      (BaseLayoutItem) this.emptySpaceItem1
    });
    this.commentDetailGroup.Location = new Point(467, 0);
    this.commentDetailGroup.Name = "commentDetailGroup";
    this.commentDetailGroup.Size = new Size(473, 92);
    this.layoutComment.Control = (Control) this.comment;
    componentResourceManager.ApplyResources((object) this.layoutComment, "layoutComment");
    this.layoutComment.Location = new Point(0, 0);
    this.layoutComment.Name = "layoutComment";
    this.layoutComment.Size = new Size(449, 24);
    this.layoutComment.TextSize = new Size(53, 13);
    this.layoutIsActive.Control = (Control) this.isActive;
    componentResourceManager.ApplyResources((object) this.layoutIsActive, "layoutIsActive");
    this.layoutIsActive.Location = new Point(0, 24);
    this.layoutIsActive.Name = "layoutIsActive";
    this.layoutIsActive.Size = new Size(188, 24);
    this.layoutIsActive.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(188, 24);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(261, 24);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem2, "emptySpaceItem2");
    this.emptySpaceItem2.Location = new Point(467, 209);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(473, 521);
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutGroupInternationalization, "layoutGroupInternationalization");
    this.layoutGroupInternationalization.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutLanguageSelection,
      (BaseLayoutItem) this.layoutCommentInternationalization,
      (BaseLayoutItem) this.emptySpaceItem3
    });
    this.layoutGroupInternationalization.Location = new Point(467, 92);
    this.layoutGroupInternationalization.Name = "layoutGroupInternationalization";
    this.layoutGroupInternationalization.Size = new Size(473, 117);
    this.layoutLanguageSelection.Control = (Control) this.languageSelection;
    componentResourceManager.ApplyResources((object) this.layoutLanguageSelection, "layoutLanguageSelection");
    this.layoutLanguageSelection.Location = new Point(0, 0);
    this.layoutLanguageSelection.Name = "layoutLanguageSelection";
    this.layoutLanguageSelection.Size = new Size(449, 24);
    this.layoutLanguageSelection.TextSize = new Size(53, 13);
    this.layoutCommentInternationalization.Control = (Control) this.commentTranslation;
    componentResourceManager.ApplyResources((object) this.layoutCommentInternationalization, "layoutCommentInternationalization");
    this.layoutCommentInternationalization.Location = new Point(0, 49);
    this.layoutCommentInternationalization.Name = "layoutCommentInternationalization";
    this.layoutCommentInternationalization.Size = new Size(449, 24);
    this.layoutCommentInternationalization.TextSize = new Size(53, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem3, "emptySpaceItem3");
    this.emptySpaceItem3.Location = new Point(0, 24);
    this.emptySpaceItem3.Name = "emptySpaceItem3";
    this.emptySpaceItem3.Size = new Size(449, 25);
    this.emptySpaceItem3.TextSize = new Size(0, 0);
    componentResourceManager.ApplyResources((object) this.layoutGroupPredefinedComments, "layoutGroupPredefinedComments");
    this.layoutGroupPredefinedComments.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem1
    });
    this.layoutGroupPredefinedComments.Location = new Point(0, 0);
    this.layoutGroupPredefinedComments.Name = "layoutGroupPredefinedComments";
    this.layoutGroupPredefinedComments.Size = new Size(467, 730);
    this.layoutControlItem1.Control = (Control) this.predefinedGridControl;
    componentResourceManager.ApplyResources((object) this.layoutControlItem1, "layoutControlItem1");
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutControlItem1";
    this.layoutControlItem1.Size = new Size(443, 686);
    this.layoutControlItem1.TextSize = new Size(0, 0);
    this.layoutControlItem1.TextToControlDistance = 0;
    this.layoutControlItem1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.commentMaintenanceViewLayout);
    this.Name = nameof (PredefinedCommentMasterDetailBrowser);
    this.commentMaintenanceViewLayout.EndInit();
    this.commentMaintenanceViewLayout.ResumeLayout(false);
    this.isActive.Properties.EndInit();
    this.commentTranslation.Properties.EndInit();
    this.languageSelection.Properties.EndInit();
    this.comment.Properties.EndInit();
    this.predefinedGridControl.EndInit();
    this.commentGridView.EndInit();
    this.layoutControlGroup1.EndInit();
    this.commentDetailGroup.EndInit();
    this.layoutComment.EndInit();
    this.layoutIsActive.EndInit();
    this.emptySpaceItem1.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutGroupInternationalization.EndInit();
    this.layoutLanguageSelection.EndInit();
    this.layoutCommentInternationalization.EndInit();
    this.emptySpaceItem3.EndInit();
    this.layoutGroupPredefinedComments.EndInit();
    this.layoutControlItem1.EndInit();
    this.ResumeLayout(false);
  }
}
