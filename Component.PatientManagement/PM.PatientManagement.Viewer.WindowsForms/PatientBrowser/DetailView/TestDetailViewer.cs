// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.DetailView.TestDetailViewer
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using PathMedical.AudiologyTest;
using PathMedical.Plugin;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.PatientBrowser.DetailView;

[ToolboxItem(false)]
public class TestDetailViewer : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private IContainer components;

  public TestDetailViewer() => this.InitializeComponent();

  public TestDetailViewer(IModel model)
    : this()
  {
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (e.Type != typeof (AudiologyTestInformation))
      return;
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new TestDetailViewer.UpdateModelDataCallBack(this.UpdateModelChanges), (object) e);
    else
      this.UpdateModelChanges(e);
  }

  private void UpdateModelChanges(ModelChangedEventArgs e)
  {
    AudiologyTestInformation audiologyTestInformation = e.ChangedObject as AudiologyTestInformation;
    if (audiologyTestInformation == null)
    {
      this.Controls.Clear();
    }
    else
    {
      ITestPlugin testPlugin = SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().FirstOrDefault<ITestPlugin>((Func<ITestPlugin, bool>) (p =>
      {
        Guid testTypeSignature1 = p.TestTypeSignature;
        Guid? testTypeSignature2 = audiologyTestInformation.TestTypeSignature;
        return testTypeSignature2.HasValue && testTypeSignature1 == testTypeSignature2.GetValueOrDefault();
      }));
      if (testPlugin != null)
      {
        if (!(testPlugin.DetailViewerComponentModuleType == (Type) null))
        {
          try
          {
            this.SuspendLayout();
            this.Controls.Clear();
            if (!(ApplicationComponentModuleManager.Instance.Get(testPlugin.DetailViewerComponentModuleType).ContentControl is Control contentControl))
              return;
            contentControl.Dock = DockStyle.Fill;
            if (!Application.ProductName.Equals("AccuLink"))
              return;
            this.Controls.Add(contentControl);
            return;
          }
          finally
          {
            this.ResumeLayout();
          }
        }
      }
      this.Controls.Clear();
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
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (TestDetailViewer);
    this.ResumeLayout(false);
  }

  private delegate void UpdateModelDataCallBack(ModelChangedEventArgs e);
}
