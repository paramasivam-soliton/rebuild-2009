// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewer
// Assembly: PM.PatientManagement.MultipleTestViewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2267BB5-2AA5-4F30-972C-4D9F0F378D06
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.MultipleTestViewer.WindowsForms.dll

using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraBars.Ribbon;
using PathMedical.ABR;
using PathMedical.DPOAE;
using PathMedical.Exception;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.TEOAE;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer;

[ToolboxItem(false)]
public sealed class TestViewer : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private DockManager testViewDock;
  private int files;
  private bool controlsAdded;
  private IContainer components;

  public TestViewer()
  {
    this.InitializeComponent();
    this.BuildUi();
    this.Dock = DockStyle.Fill;
  }

  public TestViewer(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.InvokeRequired && e.Type == typeof (int))
    {
      this.files = (int) e.ChangedObject;
      this.BeginInvoke((Delegate) new PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewer.ClearViewCallBack(this.ClearViewCall));
    }
    else if (e.Type == typeof (int))
    {
      this.files = (int) e.ChangedObject;
      this.ClearViewCall();
    }
    if (e.Type == typeof (TeoaeTestInformation))
    {
      Dictionary<TeoaeTestInformation, string> changedObject = e.ChangedObject as Dictionary<TeoaeTestInformation, string>;
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewer.UpdateTestViewerCallBackTeoae(this.UpdateTestViewerTeoae), (object) changedObject);
      else
        this.UpdateTestViewerTeoae(changedObject);
    }
    if (e.Type == typeof (DpoaeTestInformation))
    {
      Dictionary<DpoaeTestInformation, string> changedObject = e.ChangedObject as Dictionary<DpoaeTestInformation, string>;
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewer.UpdateTestViewerCallBackDpoae(this.UpdateTestViewerDpoae), (object) changedObject);
      else
        this.UpdateTestViewerDpoae(changedObject);
    }
    if (e.Type == typeof (AbrTestInformation))
    {
      Dictionary<AbrTestInformation, string> changedObject = e.ChangedObject as Dictionary<AbrTestInformation, string>;
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.TestViewer.TestViewer.UpdateTestViewerCallBackAbr(this.UpdateTestViewerAbr), (object) changedObject);
      else
        this.UpdateTestViewerAbr(changedObject);
    }
    if (!(e.Type == typeof (string)))
      return;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string str in (List<string>) e.ChangedObject)
      stringBuilder.AppendLine(str);
    this.DisplayError(string.Format(PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties.Resources.TestViewer_FilesNotLoadedMessage, (object) stringBuilder));
  }

  private void BuildUi()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    RibbonPageGroup ribbonPageGroup = ribbonHelper.CreateRibbonPageGroup(PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties.Resources.TestViewer_RibbonPageGroup);
    BarButtonItem largeImageButton = ribbonHelper.CreateLargeImageButton(PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties.Resources.TestViewer_RibbonImageButtonCaption, PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties.Resources.TestViewer_RibbonImageButtonDescription, string.Empty, ComponentResourceManagementBase<PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties.Resources>.Instance.ResourceManager.GetObject("Data_Import") as Bitmap, WaveFileImportTriggers.LoadTestDataFromFile);
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton);
    this.ToolbarElements.Add((object) ribbonPageGroup);
    this.ToolbarElements.Add((object) ribbonHelper.CreateHelpGroup(PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties.Resources.TestViewer_RibbonHelpGroup, (PathMedical.UserInterface.WindowsForms.ModelViewController.View) this));
  }

  private void ClearViewCall()
  {
    this.SuspendLayout();
    if (this.testViewDock != null)
    {
      this.testViewDock.RootPanels.Clear();
      this.testViewDock.Dispose();
    }
    this.testViewDock = new DockManager((ContainerControl) this);
    this.ResumeLayout();
  }

  private void UpdateTestViewerTeoae(
    Dictionary<TeoaeTestInformation, string> teoaeTests)
  {
    if (teoaeTests == null)
      return;
    this.SuspendLayout();
    this.testViewDock.BeginUpdate();
    foreach (TeoaeTestInformation key in teoaeTests.Keys)
    {
      TeoaeTestInformation information = key;
      ITestPlugin testPlugin = SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().FirstOrDefault<ITestPlugin>((Func<ITestPlugin, bool>) (p => p.TestTypeSignature == information.NativeTestTypeSignature));
      if (testPlugin != null)
      {
        ITestDetailView view = testPlugin.CreateView((object) key);
        DockPanel dockPanel = this.testViewDock.AddPanel(DockingStyle.Left);
        if (dockPanel != null && view != null && view is Control)
        {
          dockPanel.SuspendLayout();
          dockPanel.Name = key.Id.ToString();
          dockPanel.Text = string.Format(PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties.Resources.TestViewer_TeoaePanelText, (object) teoaeTests[information]);
          dockPanel.Width = this.Width / this.files;
          dockPanel.ControlContainer.Controls.Add(view as Control);
          dockPanel.ControlContainer.AutoScroll = true;
          dockPanel.ControlContainer.HorizontalScroll.Enabled = false;
          dockPanel.ControlContainer.HorizontalScroll.Visible = false;
          dockPanel.ResumeLayout(false);
          this.controlsAdded = true;
        }
      }
    }
    this.testViewDock.EndUpdate();
    this.ResumeLayout(false);
  }

  private void UpdateTestViewerDpoae(
    Dictionary<DpoaeTestInformation, string> dpoaeTests)
  {
    if (dpoaeTests == null)
      return;
    this.SuspendLayout();
    foreach (DpoaeTestInformation key in dpoaeTests.Keys)
    {
      DpoaeTestInformation information = key;
      ITestPlugin testPlugin = SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().FirstOrDefault<ITestPlugin>((Func<ITestPlugin, bool>) (p => p.TestTypeSignature == information.NativeTestTypeSignature));
      if (testPlugin != null)
      {
        ITestDetailView view = testPlugin.CreateView((object) key);
        DockPanel dockPanel = this.testViewDock.AddPanel(DockingStyle.Left);
        if (dockPanel != null && view != null && view is Control)
        {
          dockPanel.Name = key.Id.ToString();
          dockPanel.Text = string.Format(PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties.Resources.TestViewer_DpoaePanelText, (object) dpoaeTests[information]);
          dockPanel.ControlContainer.Controls.Add(view as Control);
          dockPanel.Width = this.Width / this.files;
          this.controlsAdded = true;
        }
      }
    }
    this.ResumeLayout(false);
  }

  private void UpdateTestViewerAbr(Dictionary<AbrTestInformation, string> abrTests)
  {
    if (abrTests == null)
      return;
    this.SuspendLayout();
    foreach (AbrTestInformation key in abrTests.Keys)
    {
      AbrTestInformation information = key;
      ITestPlugin testPlugin = SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().FirstOrDefault<ITestPlugin>((Func<ITestPlugin, bool>) (p => p.TestTypeSignature == information.NativeTestTypeSignature));
      if (testPlugin != null)
      {
        ITestDetailView view = testPlugin.CreateView((object) key);
        DockPanel dockPanel = this.testViewDock.AddPanel(DockingStyle.Left);
        if (dockPanel != null && view != null && view is Control)
        {
          dockPanel.Name = key.Id.ToString();
          dockPanel.Text = string.Format(PathMedical.PatientManagement.MultipleTestViewer.WindowsForms.Properties.Resources.TestViewer_AbrPanelText, (object) abrTests[information]);
          dockPanel.ControlContainer.Controls.Add(view as Control);
          dockPanel.Width = this.Width / this.files;
          this.controlsAdded = true;
        }
      }
    }
    this.ResumeLayout(false);
  }

  private void TestViewer_Resize(object sender, EventArgs e)
  {
    if (this.testViewDock == null)
      return;
    this.SuspendLayout();
    this.testViewDock.BeginUpdate();
    int num = this.Width / this.files;
    for (int index = this.testViewDock.RootPanels.Count - 1; index >= 0; --index)
      this.testViewDock.RootPanels[index].Width = num;
    this.testViewDock.EndUpdate();
    this.ResumeLayout();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (TestViewer);
    this.Size = new Size(890, 513);
    this.Resize += new EventHandler(this.TestViewer_Resize);
    this.ResumeLayout(false);
  }

  private delegate void UpdateTestViewerCallBackTeoae(
    Dictionary<TeoaeTestInformation, string> teoaeTests);

  private delegate void UpdateTestViewerCallBackDpoae(
    Dictionary<DpoaeTestInformation, string> dpoaeTests);

  private delegate void UpdateTestViewerCallBackAbr(Dictionary<AbrTestInformation, string> abrTest);

  private delegate void ClearViewCallBack();
}
