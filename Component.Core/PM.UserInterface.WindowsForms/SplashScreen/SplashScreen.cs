// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.SplashScreen;

public class SplashScreen : Form
{
  private static PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen msFrmSplash;
  private static Thread msOThread;
  private double mDblOpacityIncrement = 0.05;
  private double m_dblOpacityDecrement = 0.08;
  private const int TIMER_INTERVAL = 50;
  private string m_sStatus;
  private double m_dblCompletionFraction;
  private Rectangle m_rProgress;
  private double m_dblLastCompletionFraction;
  private double m_dblPBIncrementPerTimerInterval = 0.015;
  private bool m_bFirstLaunch;
  private DateTime m_dtStart;
  private bool m_bDTSet;
  private int m_iIndex = 1;
  private int m_iActualTicks;
  private ArrayList m_alPreviousCompletionFraction;
  private ArrayList m_alActualTimes = new ArrayList();
  private const string REG_KEY_INITIALIZATION = "Initialization";
  private const string REGVALUE_PB_MILISECOND_INCREMENT = "Increment";
  private const string REGVALUE_PB_PERCENTS = "Percents";
  private IContainer components;
  private Label lblStatus;
  private Label lblTimeRemaining;
  private System.Windows.Forms.Timer timer1;
  private Panel pnlStatus;

  public SplashScreen()
  {
    this.InitializeComponent();
    this.Opacity = 0.0;
    this.timer1.Interval = 50;
    this.timer1.Start();
  }

  public static void ShowSplashScreen()
  {
    if (PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash != null)
      return;
    PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msOThread = new Thread(new ThreadStart(PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.ShowForm));
    PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msOThread.IsBackground = true;
    PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msOThread.Start();
  }

  public static PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen SplashForm
  {
    get => PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash;
  }

  private static void ShowForm()
  {
    PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash = new PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen();
    Application.Run((Form) PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash);
  }

  public static void CloseForm()
  {
    if (PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash != null && !PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash.IsDisposed)
      PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash.mDblOpacityIncrement = -PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash.m_dblOpacityDecrement;
    PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msOThread = (Thread) null;
    PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash = (PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen) null;
  }

  public static void SetStatus(string newStatus) => PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.SetStatus(newStatus, true);

  public static void SetStatus(string newStatus, bool setReference)
  {
    if (PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash == null)
      return;
    PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash.m_sStatus = newStatus;
    if (!setReference)
      return;
    PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash.SetReferenceInternal();
  }

  public static void SetReferencePoint()
  {
    if (PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash == null)
      return;
    PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.msFrmSplash.SetReferenceInternal();
  }

  private void SetReferenceInternal()
  {
    if (!this.m_bDTSet)
    {
      this.m_bDTSet = true;
      this.m_dtStart = DateTime.Now;
      this.ReadIncrements();
    }
    this.m_alActualTimes.Add((object) this.ElapsedMilliSeconds());
    this.m_dblLastCompletionFraction = this.m_dblCompletionFraction;
    if (this.m_alPreviousCompletionFraction != null && this.m_iIndex < this.m_alPreviousCompletionFraction.Count)
      this.m_dblCompletionFraction = (double) this.m_alPreviousCompletionFraction[this.m_iIndex++];
    else
      this.m_dblCompletionFraction = (double) (this.m_iIndex > 0 ? 1 : 0);
  }

  private double ElapsedMilliSeconds() => (DateTime.Now - this.m_dtStart).TotalMilliseconds;

  private void ReadIncrements()
  {
    double result1;
    this.m_dblPBIncrementPerTimerInterval = !double.TryParse(RegistryAccess.GetStringRegistryValue("Increment", "0.0015"), NumberStyles.Float, (IFormatProvider) NumberFormatInfo.InvariantInfo, out result1) ? 0.0015 : result1;
    string stringRegistryValue = RegistryAccess.GetStringRegistryValue("Percents", "");
    if (stringRegistryValue != "")
    {
      string[] strArray = stringRegistryValue.Split((char[]) null);
      this.m_alPreviousCompletionFraction = new ArrayList();
      for (int index = 0; index < strArray.Length; ++index)
      {
        double result2;
        if (double.TryParse(strArray[index], NumberStyles.Float, (IFormatProvider) NumberFormatInfo.InvariantInfo, out result2))
          this.m_alPreviousCompletionFraction.Add((object) result2);
        else
          this.m_alPreviousCompletionFraction.Add((object) 1.0);
      }
    }
    else
    {
      this.m_bFirstLaunch = true;
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.ReadIncrementsCallBack(this.UpdateLabelTimeRemaining), (object) "");
      else
        this.UpdateLabelTimeRemaining("");
    }
  }

  private void UpdateLabelTimeRemaining(string text) => this.lblTimeRemaining.Text = text;

  private void StoreIncrements()
  {
    string stringValue = "";
    double num = this.ElapsedMilliSeconds();
    for (int index = 0; index < this.m_alActualTimes.Count; ++index)
      stringValue = $"{stringValue}{((double) this.m_alActualTimes[index] / num).ToString("0.####", (IFormatProvider) NumberFormatInfo.InvariantInfo)} ";
    RegistryAccess.SetStringRegistryValue("Percents", stringValue);
    this.m_dblPBIncrementPerTimerInterval = 1.0 / (double) this.m_iActualTicks;
    RegistryAccess.SetStringRegistryValue("Increment", this.m_dblPBIncrementPerTimerInterval.ToString("#.000000", (IFormatProvider) NumberFormatInfo.InvariantInfo));
  }

  private void timer1_Tick(object sender, EventArgs e)
  {
    this.lblStatus.Text = this.m_sStatus;
    if (this.mDblOpacityIncrement > 0.0)
    {
      ++this.m_iActualTicks;
      if (this.Opacity < 1.0)
        this.Opacity += this.mDblOpacityIncrement;
    }
    else if (this.Opacity > 0.0)
    {
      this.Opacity += this.mDblOpacityIncrement;
    }
    else
    {
      this.StoreIncrements();
      this.Close();
    }
    if (this.m_bFirstLaunch || this.m_dblLastCompletionFraction >= this.m_dblCompletionFraction)
      return;
    this.m_dblLastCompletionFraction += this.m_dblPBIncrementPerTimerInterval;
    int width = (int) Math.Floor((double) this.pnlStatus.ClientRectangle.Width * this.m_dblLastCompletionFraction);
    int height = this.pnlStatus.ClientRectangle.Height;
    Rectangle clientRectangle = this.pnlStatus.ClientRectangle;
    int x = clientRectangle.X;
    clientRectangle = this.pnlStatus.ClientRectangle;
    int y = clientRectangle.Y;
    if (width <= 0 || height <= 0)
      return;
    this.m_rProgress = new Rectangle(x, y, width, height);
    this.pnlStatus.Invalidate(this.m_rProgress);
    int num = 1 + (int) (50.0 * ((1.0 - this.m_dblLastCompletionFraction) / this.m_dblPBIncrementPerTimerInterval)) / 1000;
    if (num == 1)
    {
      if (this.InvokeRequired)
        this.BeginInvoke((Delegate) new PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.ReadIncrementsCallBack(this.UpdateLabelTimeRemaining), (object) string.Format("1 second remaining"));
      else
        this.UpdateLabelTimeRemaining($"{num} seconds remaining");
    }
    else if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.ReadIncrementsCallBack(this.UpdateLabelTimeRemaining), (object) $"{num} seconds remaining");
    else
      this.UpdateLabelTimeRemaining($"{num} seconds remaining");
  }

  private void pnlStatus_Paint(object sender, PaintEventArgs e)
  {
    if (this.m_bFirstLaunch || e.ClipRectangle.Width <= 0 || this.m_iActualTicks <= 1)
      return;
    LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.m_rProgress, Color.FromArgb(100, 100, 100), Color.FromArgb(150, 150, (int) byte.MaxValue), LinearGradientMode.Horizontal);
    e.Graphics.FillRectangle((Brush) linearGradientBrush, this.m_rProgress);
  }

  private void SplashScreen_DoubleClick(object sender, EventArgs e) => PathMedical.UserInterface.WindowsForms.SplashScreen.SplashScreen.CloseForm();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.lblStatus = new Label();
    this.pnlStatus = new Panel();
    this.lblTimeRemaining = new Label();
    this.timer1 = new System.Windows.Forms.Timer(this.components);
    this.SuspendLayout();
    this.lblStatus.BackColor = Color.Transparent;
    this.lblStatus.Location = new Point(152, 116);
    this.lblStatus.Name = "lblStatus";
    this.lblStatus.Size = new Size(237, 14);
    this.lblStatus.TabIndex = 0;
    this.pnlStatus.BackColor = Color.Transparent;
    this.pnlStatus.Location = new Point(152, 138);
    this.pnlStatus.Name = "pnlStatus";
    this.pnlStatus.Size = new Size(237, 24);
    this.pnlStatus.TabIndex = 1;
    this.pnlStatus.Paint += new PaintEventHandler(this.pnlStatus_Paint);
    this.lblTimeRemaining.BackColor = Color.Transparent;
    this.lblTimeRemaining.Location = new Point(152, 169);
    this.lblTimeRemaining.Name = "lblTimeRemaining";
    this.lblTimeRemaining.Size = new Size(237, 16 /*0x10*/);
    this.lblTimeRemaining.TabIndex = 2;
    this.lblTimeRemaining.Text = "Time remaining";
    this.timer1.Tick += new EventHandler(this.timer1_Tick);
    this.AutoScaleBaseSize = new Size(5, 13);
    this.BackColor = SystemColors.Window;
    this.BackgroundImageLayout = ImageLayout.Zoom;
    this.ClientSize = new Size(419, 231);
    this.Controls.Add((Control) this.lblTimeRemaining);
    this.Controls.Add((Control) this.pnlStatus);
    this.Controls.Add((Control) this.lblStatus);
    this.DoubleBuffered = true;
    this.FormBorderStyle = FormBorderStyle.None;
    this.Name = nameof (SplashScreen);
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = nameof (SplashScreen);
    this.DoubleClick += new EventHandler(this.SplashScreen_DoubleClick);
    this.ResumeLayout(false);
  }

  private delegate void ReadIncrementsCallBack(string text);
}
