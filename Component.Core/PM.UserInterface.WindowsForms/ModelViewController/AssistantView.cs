// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ModelViewController.AssistantView
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.UserInterface.ModelViewController;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.ModelViewController;

public class AssistantView : View, IAssistantView
{
  private Panel contentPanel;
  private Panel informationPanel;

  public AssistantView(params IView[] views)
  {
    this.InitializeComponent();
    foreach (IView view in views)
      this.RegisterSubView(view);
    if (!(this.SubViews.FirstOrDefault<IView>() is View contentView))
      return;
    this.SwitchContentView((IView) contentView);
  }

  private void InitializeComponent()
  {
    this.contentPanel = new Panel();
    this.informationPanel = new Panel();
    this.SuspendLayout();
    this.contentPanel.Dock = DockStyle.Bottom;
    this.contentPanel.Location = new Point(0, 80 /*0x50*/);
    this.contentPanel.Name = "contentPanel";
    this.contentPanel.Size = new Size(520, 370);
    this.contentPanel.TabIndex = 1;
    this.informationPanel.Dock = DockStyle.Top;
    this.informationPanel.Location = new Point(0, 0);
    this.informationPanel.Name = "informationPanel";
    this.informationPanel.Size = new Size(520, 53);
    this.informationPanel.TabIndex = 2;
    this.Controls.Add((Control) this.informationPanel);
    this.Controls.Add((Control) this.contentPanel);
    this.Name = nameof (AssistantView);
    this.Size = new Size(520, 450);
    this.ResumeLayout(false);
  }

  public void SwitchContentView(IView contentView)
  {
    if (contentView == null || !(contentView is UserControl))
      return;
    if (this.contentPanel.InvokeRequired)
      this.contentPanel.BeginInvoke((Delegate) new AssistantView.SwitchContectViewCallBack(this.SwitchContentViewNow), (object) contentView);
    else
      this.SwitchContentViewNow(contentView);
  }

  private void SwitchContentViewNow(IView view)
  {
    this.contentPanel.Controls.Clear();
    View view1 = view as View;
    view1.Dock = DockStyle.Fill;
    this.contentPanel.Controls.Add((Control) view1);
  }

  private delegate void SwitchContectViewCallBack(IView view);
}
