// Decompiled with JetBrains decompiler
// Type: GN.Otometrics.NHS.NHS
// Assembly: AccuScreenServicetool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E723BD4-2FBA-4A66-910E-0878AA53AFFA
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\AccuScreenServicetool.exe

using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using GN.Otometrics.NHS.Properties;
using PathMedical.DatabaseManagement;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceManagement;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace GN.Otometrics.NHS;

public class NHS : RibbonForm
{
  private IContainer components;
  private RibbonStatusBar ribbonStatusBar;
  private PanelControl clientPanel;
  private RibbonPageGroup ribbonPageGroup1;
  private RibbonPageGroup ribbonPageGroup3;
  private RibbonPageGroup patientRibbonGroup;
  private ApplicationMenu skinningMenu;
  private RibbonControl ribbon;
  private RepositoryItemTextEdit patientRangeSelection;
  private RepositoryItemTextEdit repositoryItemTextEdit2;
  private RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;
  private RepositoryItemTextEdit patientSearchTextField;
  private RepositoryItemTextEdit repositoryItemTextEdit4;
  private BarEditItem findData;
  private RepositoryItemComboBox repositoryItemComboBox1;
  private RepositoryItemButtonEdit repositoryItemButtonEdit1;
  private RepositoryItemButtonEdit patientSearchText;
  private ApplicationMenu applicationMainMenu;
  private BarStaticItem barStaticItem1;
  private BarStaticItem barVersionInformation;

  public NHS()
  {
    UserLookAndFeel.Default.SkinName = "Seven";
    this.InitializeComponent();
    this.Ribbon.Pages.Clear();
    UserInterfaceManager.Instance.Initialize((Form) this, this.ribbon, this.clientPanel, this.StatusBar);
    this.barVersionInformation.Caption = $"Version {Application.ProductVersion} ({DatabaseProviderFactory.Instance.DatabaseProvider.Name})";
    this.barVersionInformation.ItemClick += new ItemClickEventHandler(this.OnAccuLinkInformationClick);
  }

  private void OnAccuLinkInformationClick(object sender, ItemClickEventArgs e)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("AccuScreen Service Tool {0}{1}", (object) Application.ProductVersion, (object) Environment.NewLine);
    stringBuilder.AppendFormat("Database: {0}{1}", (object) DatabaseProviderFactory.Instance.DatabaseProvider.Name, (object) Environment.NewLine);
    stringBuilder.AppendFormat("Database Connection: {0}{1}", (object) DatabaseProviderFactory.Instance.ConnectionString, (object) Environment.NewLine);
    stringBuilder.AppendFormat("Instrument Dump Folder: {0}{1}", (object) SystemConfigurationManager.Instance.TemporaryInstrumentDataDirectory, (object) Environment.NewLine);
    int num = (int) UserInterfaceManager.Instance.ShowMessageBox(stringBuilder.ToString(), "AccuScreen Service Tool System Information", AnswerOptionType.OK, QuestionIcon.Information);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    SerializableAppearanceObject appearance1 = new SerializableAppearanceObject();
    SerializableAppearanceObject appearance2 = new SerializableAppearanceObject();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (GN.Otometrics.NHS.NHS));
    this.ribbonStatusBar = new RibbonStatusBar();
    this.barVersionInformation = new BarStaticItem();
    this.ribbon = new RibbonControl();
    this.applicationMainMenu = new ApplicationMenu(this.components);
    this.barStaticItem1 = new BarStaticItem();
    this.patientRangeSelection = new RepositoryItemTextEdit();
    this.repositoryItemTextEdit2 = new RepositoryItemTextEdit();
    this.repositoryItemZoomTrackBar1 = new RepositoryItemZoomTrackBar();
    this.patientSearchTextField = new RepositoryItemTextEdit();
    this.repositoryItemTextEdit4 = new RepositoryItemTextEdit();
    this.repositoryItemComboBox1 = new RepositoryItemComboBox();
    this.repositoryItemButtonEdit1 = new RepositoryItemButtonEdit();
    this.patientSearchText = new RepositoryItemButtonEdit();
    this.clientPanel = new PanelControl();
    this.ribbonPageGroup1 = new RibbonPageGroup();
    this.ribbonPageGroup3 = new RibbonPageGroup();
    this.patientRibbonGroup = new RibbonPageGroup();
    this.skinningMenu = new ApplicationMenu(this.components);
    this.findData = new BarEditItem();
    this.ribbon.BeginInit();
    this.applicationMainMenu.BeginInit();
    this.patientRangeSelection.BeginInit();
    this.repositoryItemTextEdit2.BeginInit();
    this.repositoryItemZoomTrackBar1.BeginInit();
    this.patientSearchTextField.BeginInit();
    this.repositoryItemTextEdit4.BeginInit();
    this.repositoryItemComboBox1.BeginInit();
    this.repositoryItemButtonEdit1.BeginInit();
    this.patientSearchText.BeginInit();
    this.clientPanel.BeginInit();
    this.skinningMenu.BeginInit();
    this.SuspendLayout();
    this.ribbonStatusBar.ItemLinks.Add((BarItem) this.barVersionInformation);
    this.ribbonStatusBar.Location = new Point(0, 724);
    this.ribbonStatusBar.Name = "ribbonStatusBar";
    this.ribbonStatusBar.Ribbon = this.ribbon;
    this.ribbonStatusBar.Size = new Size(992, 25);
    this.barVersionInformation.Alignment = BarItemLinkAlignment.Right;
    this.barVersionInformation.Id = 42;
    this.barVersionInformation.Name = "barVersionInformation";
    this.barVersionInformation.TextAlignment = StringAlignment.Near;
    this.ribbon.ApplicationButtonDropDownControl = (PopupControl) this.applicationMainMenu;
    this.ribbon.ApplicationIcon = Resources.GNO;
    this.ribbon.AutoSizeItems = true;
    this.ribbon.Items.AddRange(new BarItem[2]
    {
      (BarItem) this.barStaticItem1,
      (BarItem) this.barVersionInformation
    });
    this.ribbon.Location = new Point(0, 0);
    this.ribbon.MaxItemId = 43;
    this.ribbon.Name = "ribbon";
    this.ribbon.RepositoryItems.AddRange(new RepositoryItem[8]
    {
      (RepositoryItem) this.patientRangeSelection,
      (RepositoryItem) this.repositoryItemTextEdit2,
      (RepositoryItem) this.repositoryItemZoomTrackBar1,
      (RepositoryItem) this.patientSearchTextField,
      (RepositoryItem) this.repositoryItemTextEdit4,
      (RepositoryItem) this.repositoryItemComboBox1,
      (RepositoryItem) this.repositoryItemButtonEdit1,
      (RepositoryItem) this.patientSearchText
    });
    this.ribbon.Size = new Size(992, 48 /*0x30*/);
    this.ribbon.StatusBar = this.ribbonStatusBar;
    this.applicationMainMenu.BottomPaneControlContainer = (PopupControlContainer) null;
    this.applicationMainMenu.Name = "applicationMainMenu";
    this.applicationMainMenu.Ribbon = this.ribbon;
    this.applicationMainMenu.RightPaneControlContainer = (PopupControlContainer) null;
    this.barStaticItem1.Caption = "EmptySkinSelectionItem";
    this.barStaticItem1.Id = 41;
    this.barStaticItem1.Name = "barStaticItem1";
    this.barStaticItem1.TextAlignment = StringAlignment.Near;
    this.patientRangeSelection.AutoHeight = false;
    this.patientRangeSelection.Name = "patientRangeSelection";
    this.repositoryItemTextEdit2.AutoHeight = false;
    this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
    this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
    this.repositoryItemZoomTrackBar1.ScrollThumbStyle = ScrollThumbStyle.ArrowDownRight;
    this.patientSearchTextField.AutoHeight = false;
    this.patientSearchTextField.Name = "patientSearchTextField";
    this.repositoryItemTextEdit4.AutoHeight = false;
    this.repositoryItemTextEdit4.Name = "repositoryItemTextEdit4";
    this.repositoryItemComboBox1.AutoHeight = false;
    this.repositoryItemComboBox1.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton(ButtonPredefines.Combo)
    });
    this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
    this.repositoryItemButtonEdit1.AutoHeight = false;
    this.repositoryItemButtonEdit1.Buttons.AddRange(new EditorButton[2]
    {
      new EditorButton(ButtonPredefines.Left, "", -1, true, true, true, ImageLocation.MiddleCenter, (Image) null, new KeyShortcut(Keys.None), (AppearanceObject) appearance1, "", (object) null, (SuperToolTip) null, false),
      new EditorButton(ButtonPredefines.Right)
    });
    this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
    this.repositoryItemButtonEdit1.NullText = "Last 6 month";
    this.repositoryItemButtonEdit1.TextEditStyle = TextEditStyles.DisableTextEditor;
    this.patientSearchText.AutoHeight = false;
    this.patientSearchText.Buttons.AddRange(new EditorButton[2]
    {
      new EditorButton(ButtonPredefines.Delete, "", -1, false, true, false, ImageLocation.MiddleCenter, (Image) null, new KeyShortcut(Keys.None), (AppearanceObject) appearance2, "", (object) null, (SuperToolTip) null, false),
      new EditorButton(ButtonPredefines.Glyph)
    });
    this.patientSearchText.Name = "patientSearchText";
    this.clientPanel.BorderStyle = BorderStyles.NoBorder;
    this.clientPanel.Dock = DockStyle.Fill;
    this.clientPanel.Location = new Point(0, 48 /*0x30*/);
    this.clientPanel.Name = "clientPanel";
    this.clientPanel.Size = new Size(992, 676);
    this.clientPanel.TabIndex = 2;
    this.ribbonPageGroup1.Glyph = (Image) Resources.GNO;
    this.ribbonPageGroup1.Name = "ribbonPageGroup1";
    this.ribbonPageGroup1.ShowCaptionButton = false;
    this.ribbonPageGroup1.Text = "Patients";
    this.ribbonPageGroup3.Name = "ribbonPageGroup3";
    this.ribbonPageGroup3.Text = "ribbonPageGroup2";
    this.patientRibbonGroup.Name = "patientRibbonGroup";
    this.patientRibbonGroup.Text = "Patients";
    this.skinningMenu.BottomPaneControlContainer = (PopupControlContainer) null;
    this.skinningMenu.Name = "skinningMenu";
    this.skinningMenu.Ribbon = this.ribbon;
    this.skinningMenu.RightPaneControlContainer = (PopupControlContainer) null;
    this.findData.Caption = "Find";
    this.findData.Edit = (RepositoryItem) this.patientSearchTextField;
    this.findData.Id = 28;
    this.findData.Name = "findData";
    this.AllowFormGlass = DefaultBoolean.False;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(992, 749);
    this.Controls.Add((Control) this.clientPanel);
    this.Controls.Add((Control) this.ribbonStatusBar);
    this.Controls.Add((Control) this.ribbon);
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.Name = nameof (NHS);
    this.Ribbon = this.ribbon;
    this.StatusBar = this.ribbonStatusBar;
    this.Text = "AccuLink";
    this.ribbon.EndInit();
    this.applicationMainMenu.EndInit();
    this.patientRangeSelection.EndInit();
    this.repositoryItemTextEdit2.EndInit();
    this.repositoryItemZoomTrackBar1.EndInit();
    this.patientSearchTextField.EndInit();
    this.repositoryItemTextEdit4.EndInit();
    this.repositoryItemComboBox1.EndInit();
    this.repositoryItemButtonEdit1.EndInit();
    this.patientSearchText.EndInit();
    this.clientPanel.EndInit();
    this.skinningMenu.EndInit();
    this.ResumeLayout(false);
  }
}
