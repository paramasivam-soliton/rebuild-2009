// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.DevExpressProgressBar
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraPrinting;
using DevExpress.XtraReports;
using DevExpress.XtraReports.UI;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

[ToolboxItem(true)]
[DefaultBindableProperty("Position")]
public class DevExpressProgressBar : XRControl
{
  private float pos;
  private float maxVal = 100f;

  public DevExpressProgressBar() => this.ForeColor = SystemColors.Highlight;

  [DefaultValue(100)]
  public float MaxValue
  {
    get => this.maxVal;
    set
    {
      if ((double) value <= 0.0)
        return;
      this.maxVal = value;
    }
  }

  [DefaultValue(0)]
  [Bindable(true)]
  public float Position
  {
    get => this.pos;
    set
    {
      if ((double) value < 0.0 || (double) value > (double) this.maxVal)
        return;
      this.pos = value;
    }
  }

  protected override VisualBrick CreateBrick(VisualBrick[] childrenBricks)
  {
    return (VisualBrick) new PanelBrick((IBrickOwner) this);
  }

  protected override void PutStateToBrick(VisualBrick brick, PrintingSystem ps)
  {
    base.PutStateToBrick(brick, ps);
    PanelBrick panelBrick = (PanelBrick) brick;
    VisualBrick visualBrick1 = new VisualBrick();
    visualBrick1.Sides = BorderSide.None;
    visualBrick1.BackColor = panelBrick.Style.ForeColor;
    VisualBrick visualBrick2 = visualBrick1;
    RectangleF rect = panelBrick.Rect;
    double width = (double) rect.Width * ((double) this.Position / (double) this.MaxValue);
    rect = panelBrick.Rect;
    double height = (double) rect.Height;
    RectangleF rectangleF = new RectangleF(0.0f, 0.0f, (float) width, (float) height);
    visualBrick2.Rect = rectangleF;
    panelBrick.Bricks.Add((Brick) visualBrick1);
  }

  public override string ToString() => $"DevExpressProgressBar {this.Name} [{this.Text}]";
}
