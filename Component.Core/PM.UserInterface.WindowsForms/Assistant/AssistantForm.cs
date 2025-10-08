// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.Assistant.AssistantForm
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.Assistant;

public class AssistantForm : Form
{
  private IContainer components;

  public AssistantForm() => this.InitializeComponent();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AssistantForm));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AssistantForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
  }
}
