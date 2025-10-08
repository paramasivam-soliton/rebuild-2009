// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.Panel.FastTableLayoutPanel
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.Panel;

public class FastTableLayoutPanel : TableLayoutPanel
{
  public FastTableLayoutPanel() => this.DoubleBuffered = true;

  public FastTableLayoutPanel(IContainer container) => this.DoubleBuffered = true;
}
