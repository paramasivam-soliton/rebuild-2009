// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ModelViewController.View
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.XtraLayout;
using PathMedical.Automaton;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.Controls;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.ModelViewController;

public class View : 
  UserControl,
  IView,
  IDisposable,
  ISupportControllerAction,
  ISupportUserInterfaceManager
{
  private bool isLayoutEditingDisabled;
  protected ViewModeType viewMode;

  public View()
    : this((IModel) null)
  {
  }

  public View(IModel model)
  {
    this.ToolbarElements = new List<object>();
    this.StatusBarElements = new List<object>();
    this.SubViews = (ICollection<IView>) new List<IView>();
  }

  public List<object> ToolbarElements { get; protected set; }

  public List<object> StatusBarElements { get; protected set; }

  public event EventHandler<TriggerEventArgs> ControllerActionRequested;

  public void RequestControllerAction(object sender, TriggerEventArgs e)
  {
    try
    {
      Cursor.Current = Cursors.WaitCursor;
      if (this.ControllerActionRequested == null)
        return;
      this.ControllerActionRequested(sender, e);
    }
    catch (ModelException ex)
    {
      this.DisplayError(ex.Message);
    }
    catch (Exception ex)
    {
      this.DisplayError(PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("FatalErrorOccured"));
      throw;
    }
    finally
    {
      Cursor.Current = Cursors.Default;
    }
  }

  public ICollection<IView> SubViews { get; private set; }

  public void RegisterSubView(IView subView)
  {
    if (this.SubViews == null)
      this.SubViews = (ICollection<IView>) new List<IView>();
    if (subView == null)
      return;
    this.SubViews.Add(subView);
    subView.ControllerActionRequested += new EventHandler<TriggerEventArgs>(this.RequestControllerAction);
  }

  public virtual bool IsFormDataValid => this.ValidateView();

  public virtual AnswerType DisplayQuestion(
    string questionText,
    AnswerOptionType possibleAnswerOptionTypes,
    QuestionIcon questionIcon)
  {
    MessageBoxButtons buttons = (MessageBoxButtons) possibleAnswerOptionTypes;
    MessageBoxIcon icon = (MessageBoxIcon) questionIcon;
    return (AnswerType) MessageBox.Show(questionText, string.Empty, buttons, icon);
  }

  public virtual void DisplayMessage(string messageText)
  {
    int num = (int) MessageBox.Show(messageText, PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("InformationMessageCaption"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  public virtual void DisplayError(string errorText)
  {
    int num = (int) MessageBox.Show(errorText, PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("ErrorMessageCaption"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  public virtual void CopyUIToModel()
  {
    foreach (IView subView in (IEnumerable<IView>) this.SubViews)
      subView.CopyUIToModel();
  }

  public virtual void CleanUpView()
  {
    foreach (IView subView in (IEnumerable<IView>) this.SubViews)
      subView.CleanUpView();
  }

  public ViewModeType ViewMode
  {
    get => this.viewMode;
    set
    {
      if (!this.isLayoutEditingDisabled)
      {
        foreach (LayoutControl layoutControl in ((IEnumerable<FieldInfo>) this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)).Where<FieldInfo>((Func<FieldInfo, bool>) (f => typeof (LayoutControl).IsAssignableFrom(f.FieldType))).Select<FieldInfo, LayoutControl>((Func<FieldInfo, LayoutControl>) (f => f.GetValue((object) this) as LayoutControl)))
          layoutControl.AllowCustomizationMenu = false;
        this.isLayoutEditingDisabled = true;
      }
      ((IEnumerable<FieldInfo>) this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)).Where<FieldInfo>((Func<FieldInfo, bool>) (f => typeof (LayoutControlItem).IsAssignableFrom(f.FieldType))).Select<FieldInfo, LayoutControlItem>((Func<FieldInfo, LayoutControlItem>) (f => f.GetValue((object) this) as LayoutControlItem)).Where<LayoutControlItem>((Func<LayoutControlItem, bool>) (c => c.Control is IValidatableControl)).FirstOrDefault<LayoutControlItem>()?.Control.Focus();
      this.viewMode = value;
      foreach (IView subView in (IEnumerable<IView>) this.SubViews)
        subView.ViewMode = this.viewMode;
      switch (this.ViewMode)
      {
        case ViewModeType.Viewing:
          SystemConfigurationManager.Instance.UserInterfaceManager.ToggleToolbarElement(Triggers.Add, false);
          SystemConfigurationManager.Instance.UserInterfaceManager.ToggleToolbarElement(Triggers.StartModule, false);
          break;
        case ViewModeType.Editing:
          SystemConfigurationManager.Instance.UserInterfaceManager.ToggleToolbarElement(Triggers.Add, false);
          SystemConfigurationManager.Instance.UserInterfaceManager.ToggleToolbarElement(Triggers.StartModule, true);
          break;
        case ViewModeType.Adding:
          SystemConfigurationManager.Instance.UserInterfaceManager.ToggleToolbarElement(Triggers.Add, true);
          SystemConfigurationManager.Instance.UserInterfaceManager.ToggleToolbarElement(Triggers.StartModule, false);
          break;
      }
      this.OnViewModeChanged(EventArgs.Empty);
    }
  }

  protected virtual void OnViewModeChanged(EventArgs e)
  {
  }

  public virtual bool ValidateView()
  {
    return ((IEnumerable<FieldInfo>) this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)).Where<FieldInfo>((Func<FieldInfo, bool>) (f => typeof (IValidatableControl).IsAssignableFrom(f.FieldType))).Select<FieldInfo, IValidatableControl>((Func<FieldInfo, IValidatableControl>) (f => f.GetValue((object) this) as IValidatableControl)).Where<IValidatableControl>((Func<IValidatableControl, bool>) (c => c.IsActive && !c.IsReadOnly)).All<IValidatableControl>((Func<IValidatableControl, bool>) (c => c.Validate())) && this.SubViews.OfType<View>().All<View>((Func<View, bool>) (subView => subView.ValidateView()));
  }

  protected string HelpMarker { get; set; }

  public string RequestContextHelp() => this.HelpMarker;
}
