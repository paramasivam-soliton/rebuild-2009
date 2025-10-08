// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.Panels.PatientCommentEditor
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.PatientManagement.Command;
using PathMedical.PatientManagement.CommentManagement;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.Presentation.DevExpress;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientEditor.Panels;

[ToolboxItem(false)]
public class PatientCommentEditor : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private ModelMapper<Comment> modelMapper;
  private DevExpressSingleSelectionGridViewHelper<Comment> commentGridViewHelper;
  private IContainer components;
  private DevExpressComboBoxEdit commentAssignment;
  private SimpleButton addCommentButton;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup1;
  private LayoutControlItem layoutControlItem2;
  private LayoutControlItem layoutCommentAssignment;
  private GridControl commentGridControl;
  private GridView commentGridView;
  private GridColumn columnCommentText;
  private GridColumn columnDate;
  private GridColumn columnExaminer;
  private LayoutControlGroup layoutGroupComments;
  private LayoutControlItem layoutControlItem3;
  private EmptySpaceItem emptySpaceItem1;
  private GridColumn gridColumn1;
  private RepositoryItemButtonEdit repositoryItemDeleteButton;

  public PatientCommentEditor()
  {
    this.InitializeComponent();
    this.addCommentButton.Click += new EventHandler(this.AddCommentButtonToList);
    this.InitializeSelectionValues();
    this.InitializeModelMapper();
  }

  public PatientCommentEditor(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    this.commentGridViewHelper = new DevExpressSingleSelectionGridViewHelper<Comment>((PathMedical.UserInterface.WindowsForms.ModelViewController.View) this, this.commentGridView, model, PatientManagementTriggers.SelectComment, "Comments");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
    CommentManager.Instance.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void InitializeSelectionValues()
  {
    this.commentAssignment.DataSource = (object) CommentManager.Instance.CommentList.Select<PredefinedComment, ComboBoxEditItemWrapper>((Func<PredefinedComment, ComboBoxEditItemWrapper>) (i => new ComboBoxEditItemWrapper(i.Text, (object) i))).OrderBy<ComboBoxEditItemWrapper, string>((Func<ComboBoxEditItemWrapper, string>) (ciw => ciw.Name)).ToList<ComboBoxEditItemWrapper>();
  }

  private void InitializeModelMapper()
  {
    this.modelMapper = new ModelMapper<Comment>(this.ViewMode != 0);
  }

  protected override void OnViewModeChanged(EventArgs e)
  {
    bool isUIEnabled = this.ViewMode != 0;
    this.modelMapper.SetUIEnabled(isUIEnabled);
    this.commentAssignment.Enabled = isUIEnabled;
    this.addCommentButton.Enabled = isUIEnabled;
    this.commentGridView.OptionsBehavior.Editable = isUIEnabled;
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if ((e.ChangeType == ChangeType.SelectionChanged || e.ChangeType == ChangeType.ItemEdited) && e.Type == typeof (Comment) && !e.IsList)
    {
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new PatientCommentEditor.UpdateCommentDataCallBack(this.FillFields), (object) (e.ChangedObject as Comment));
      else
        this.FillFields(e.ChangedObject as Comment);
    }
    else
    {
      if (!(e.Type == typeof (PredefinedComment)) || e.ChangeType == ChangeType.SelectionChanged)
        return;
      this.InitializeSelectionValues();
    }
  }

  public override void CopyUIToModel() => this.modelMapper.CopyUIToModel();

  private void FillFields(Comment comment)
  {
    this.modelMapper.SetUIEnabled(comment != null && this.ViewMode != 0);
    this.modelMapper.CopyModelToUI(comment);
    this.InitializeSelectionValues();
  }

  private void AddCommentButtonToList(object sender, EventArgs e)
  {
    object comment = this.commentAssignment.Value;
    if (comment == null)
      return;
    CommentTriggerContext context = new CommentTriggerContext(comment);
    this.RequestControllerAction((object) this, new TriggerEventArgs(PatientManagementTriggers.AddComment, (TriggerContext) context));
    this.commentAssignment.Value = (object) null;
  }

  private void OnDeletePatientComment(object sender, ButtonPressedEventArgs e)
  {
    if (e == null)
      return;
    Comment selectedEntity = this.commentGridViewHelper.GetSelectedEntity();
    if (selectedEntity == null)
      return;
    CommentTriggerContext context = new CommentTriggerContext((object) selectedEntity);
    this.RequestControllerAction((object) this, new TriggerEventArgs(PatientManagementTriggers.DeleteComment, (TriggerContext) context));
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PatientCommentEditor));
    SerializableAppearanceObject appearance = new SerializableAppearanceObject();
    this.commentAssignment = new DevExpressComboBoxEdit();
    this.layoutControl1 = new LayoutControl();
    this.commentGridControl = new GridControl();
    this.commentGridView = new GridView();
    this.columnCommentText = new GridColumn();
    this.columnDate = new GridColumn();
    this.columnExaminer = new GridColumn();
    this.gridColumn1 = new GridColumn();
    this.repositoryItemDeleteButton = new RepositoryItemButtonEdit();
    this.addCommentButton = new SimpleButton();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.layoutGroupComments = new LayoutControlGroup();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutCommentAssignment = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.commentAssignment.Properties.BeginInit();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.commentGridControl.BeginInit();
    this.commentGridView.BeginInit();
    this.repositoryItemDeleteButton.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.layoutGroupComments.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutCommentAssignment.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.SuspendLayout();
    this.commentAssignment.EnterMoveNextControl = true;
    this.commentAssignment.FormatString = (string) null;
    this.commentAssignment.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.commentAssignment.IsReadOnly = false;
    this.commentAssignment.IsUndoing = false;
    componentResourceManager.ApplyResources((object) this.commentAssignment, "commentAssignment");
    this.commentAssignment.Name = "commentAssignment";
    this.commentAssignment.Properties.Appearance.BorderColor = Color.LightGray;
    this.commentAssignment.Properties.Appearance.Options.UseBorderColor = true;
    this.commentAssignment.Properties.BorderStyle = BorderStyles.Simple;
    this.commentAssignment.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("commentAssignment.Properties.Buttons"))
    });
    this.commentAssignment.ShowEmptyElement = true;
    this.commentAssignment.StyleController = (IStyleController) this.layoutControl1;
    this.commentAssignment.Validator = (ICustomValidator) null;
    this.commentAssignment.Value = (object) null;
    this.layoutControl1.Controls.Add((Control) this.commentAssignment);
    this.layoutControl1.Controls.Add((Control) this.commentGridControl);
    this.layoutControl1.Controls.Add((Control) this.addCommentButton);
    componentResourceManager.ApplyResources((object) this.layoutControl1, "layoutControl1");
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    componentResourceManager.ApplyResources((object) this.commentGridControl, "commentGridControl");
    this.commentGridControl.MainView = (BaseView) this.commentGridView;
    this.commentGridControl.Name = "commentGridControl";
    this.commentGridControl.RepositoryItems.AddRange(new RepositoryItem[1]
    {
      (RepositoryItem) this.repositoryItemDeleteButton
    });
    this.commentGridControl.ViewCollection.AddRange(new BaseView[1]
    {
      (BaseView) this.commentGridView
    });
    this.commentGridView.Columns.AddRange(new GridColumn[4]
    {
      this.columnCommentText,
      this.columnDate,
      this.columnExaminer,
      this.gridColumn1
    });
    this.commentGridView.GridControl = this.commentGridControl;
    this.commentGridView.Name = "commentGridView";
    this.commentGridView.OptionsCustomization.AllowFilter = false;
    this.commentGridView.OptionsFilter.AllowFilterEditor = false;
    this.commentGridView.OptionsMenu.EnableColumnMenu = false;
    this.commentGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
    this.commentGridView.OptionsView.ShowDetailButtons = false;
    this.commentGridView.OptionsView.ShowGroupPanel = false;
    componentResourceManager.ApplyResources((object) this.columnCommentText, "columnCommentText");
    this.columnCommentText.FieldName = "Text";
    this.columnCommentText.Name = "columnCommentText";
    this.columnCommentText.OptionsColumn.AllowEdit = false;
    componentResourceManager.ApplyResources((object) this.columnDate, "columnDate");
    this.columnDate.DisplayFormat.FormatString = "G";
    this.columnDate.DisplayFormat.FormatType = FormatType.DateTime;
    this.columnDate.FieldName = "CreationDate";
    this.columnDate.Name = "columnDate";
    this.columnDate.OptionsColumn.AllowEdit = false;
    componentResourceManager.ApplyResources((object) this.columnExaminer, "columnExaminer");
    this.columnExaminer.FieldName = "Examiner";
    this.columnExaminer.Name = "columnExaminer";
    this.columnExaminer.OptionsColumn.AllowEdit = false;
    this.gridColumn1.ColumnEdit = (RepositoryItem) this.repositoryItemDeleteButton;
    this.gridColumn1.Name = "gridColumn1";
    componentResourceManager.ApplyResources((object) this.gridColumn1, "gridColumn1");
    componentResourceManager.ApplyResources((object) this.repositoryItemDeleteButton, "repositoryItemDeleteButton");
    this.repositoryItemDeleteButton.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton((ButtonPredefines) componentResourceManager.GetObject("repositoryItemDeleteButton.Buttons"), componentResourceManager.GetString("repositoryItemDeleteButton.Buttons1"), (int) componentResourceManager.GetObject("repositoryItemDeleteButton.Buttons2"), (bool) componentResourceManager.GetObject("repositoryItemDeleteButton.Buttons3"), (bool) componentResourceManager.GetObject("repositoryItemDeleteButton.Buttons4"), (bool) componentResourceManager.GetObject("repositoryItemDeleteButton.Buttons5"), (ImageLocation) componentResourceManager.GetObject("repositoryItemDeleteButton.Buttons6"), (Image) componentResourceManager.GetObject("repositoryItemDeleteButton.Buttons7"), new KeyShortcut(Keys.None), (AppearanceObject) appearance, componentResourceManager.GetString("repositoryItemDeleteButton.Buttons8"), componentResourceManager.GetObject("repositoryItemDeleteButton.Buttons9"), (SuperToolTip) componentResourceManager.GetObject("repositoryItemDeleteButton.Buttons10"), (bool) componentResourceManager.GetObject("repositoryItemDeleteButton.Buttons11"))
    });
    this.repositoryItemDeleteButton.Name = "repositoryItemDeleteButton";
    this.repositoryItemDeleteButton.TextEditStyle = TextEditStyles.HideTextEditor;
    this.repositoryItemDeleteButton.ButtonClick += new ButtonPressedEventHandler(this.OnDeletePatientComment);
    componentResourceManager.ApplyResources((object) this.addCommentButton, "addCommentButton");
    this.addCommentButton.Name = "addCommentButton";
    this.addCommentButton.StyleController = (IStyleController) this.layoutControl1;
    componentResourceManager.ApplyResources((object) this.layoutControlGroup1, "layoutControlGroup1");
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutGroupComments
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(816, 576);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.TextVisible = false;
    componentResourceManager.ApplyResources((object) this.layoutGroupComments, "layoutGroupComments");
    this.layoutGroupComments.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.layoutCommentAssignment,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutControlItem2
    });
    this.layoutGroupComments.Location = new Point(0, 0);
    this.layoutGroupComments.Name = "layoutGroupComments";
    this.layoutGroupComments.Size = new Size(796, 556);
    this.layoutControlItem3.Control = (Control) this.commentGridControl;
    componentResourceManager.ApplyResources((object) this.layoutControlItem3, "layoutControlItem3");
    this.layoutControlItem3.Location = new Point(0, 0);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(772, 486);
    this.layoutControlItem3.TextSize = new Size(0, 0);
    this.layoutControlItem3.TextToControlDistance = 0;
    this.layoutControlItem3.TextVisible = false;
    this.layoutCommentAssignment.Control = (Control) this.commentAssignment;
    componentResourceManager.ApplyResources((object) this.layoutCommentAssignment, "layoutCommentAssignment");
    this.layoutCommentAssignment.Location = new Point(0, 486);
    this.layoutCommentAssignment.Name = "layoutCommentAssignment";
    this.layoutCommentAssignment.Size = new Size(563, 26);
    this.layoutCommentAssignment.TextSize = new Size(45, 13);
    componentResourceManager.ApplyResources((object) this.emptySpaceItem1, "emptySpaceItem1");
    this.emptySpaceItem1.Location = new Point(678, 486);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(94, 26);
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.layoutControlItem2.Control = (Control) this.addCommentButton;
    componentResourceManager.ApplyResources((object) this.layoutControlItem2, "layoutControlItem2");
    this.layoutControlItem2.Location = new Point(563, 486);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(115, 26);
    this.layoutControlItem2.TextSize = new Size(0, 0);
    this.layoutControlItem2.TextToControlDistance = 0;
    this.layoutControlItem2.TextVisible = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl1);
    this.Name = nameof (PatientCommentEditor);
    this.commentAssignment.Properties.EndInit();
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.commentGridControl.EndInit();
    this.commentGridView.EndInit();
    this.repositoryItemDeleteButton.EndInit();
    this.layoutControlGroup1.EndInit();
    this.layoutGroupComments.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutCommentAssignment.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutControlItem2.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateCommentDataCallBack(Comment comment);
}
