// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceManagement.UserInterfaceManager
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using PathMedical.Automaton;
using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.Login;
using PathMedical.Plugin;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.Assistant;
using PathMedical.UserInterface.WindowsForms.Login;
using PathMedical.UserProfileManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceManagement;

public class UserInterfaceManager : IUserInterfaceManager
{
  private static readonly ILogger Logger = LogFactory.Instance.Create("UserInterface");
  private readonly BarItem logoff = (BarItem) new BarStaticItem();
  private readonly BarItem userInformation = (BarItem) new BarStaticItem();
  private LoginPanel loginPanel;
  private readonly List<IApplicationComponent> registeredApplicationComponents;
  private readonly Stack<IApplicationComponentModule> activeApplicationComponentModuleHistory = new Stack<IApplicationComponentModule>();
  private BarButtonItem saveModificationsButton;
  private BarButtonItem undoAllModificationsButton;
  private BarButtonItem undoLastModificationButton;
  private IApplicationComponentModule activeAssistant;
  private AssistantForm assistentForm;
  private readonly DefaultLookAndFeel lookAndFeel = new DefaultLookAndFeel();

  public Form RootForm { get; protected set; }

  public PanelControl ContentPanel { get; private set; }

  public RibbonControl Ribbon { get; private set; }

  public static UserInterfaceManager Instance => PathMedical.Singleton.Singleton<UserInterfaceManager>.Instance;

  private UserInterfaceManager()
  {
    this.registeredApplicationComponents = new List<IApplicationComponent>();
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<PathMedical.Properties.Resources>.Instance.ResourceManager);
    GlobalResourceEnquirer.Instance.RegisterResourceManager(ComponentResourceManagementBase<PathMedical.UserInterface.WindowsForms.Properties.Resources>.Instance.ResourceManager);
    BonusSkins.Register();
    DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider.GetErrorIcon += (GetErrorIconEventHandler) (e => e.ErrorIcon = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GN_MandatoryField") as Bitmap));
  }

  public void Initialize(
    Form rootForm,
    RibbonControl ribbon,
    PanelControl contentPanel,
    RibbonStatusBar statusBar)
  {
    if (rootForm == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (rootForm));
    if (ribbon == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (ribbon));
    if (contentPanel == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (contentPanel));
    if (statusBar == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (statusBar));
    this.RootForm = rootForm;
    this.Ribbon = ribbon;
    this.ContentPanel = contentPanel;
    this.RootForm.FormClosing += new FormClosingEventHandler(this.ApplicationClosing);
    this.RootForm.KeyPreview = true;
    this.RootForm.KeyUp += new KeyEventHandler(this.OnRootFormKeyUp);
    this.Ribbon.SelectedPageChanging += new RibbonPageChangingEventHandler(this.RibbonPageChanging);
    CommandManager.Instance.OperationPerformed += new EventHandler(this.UpdateModificationManagementButtons);
    this.Ribbon.ApplicationCaption = Application.ProductName;
    if (Application.ProductName.Equals("Mira"))
    {
      this.userInformation.Glyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("LoggedInUser") as Bitmap);
      this.logoff.Glyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("LoginLock") as Bitmap);
    }
    else
    {
      this.userInformation.Glyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GN_LoggedInUser") as Bitmap);
      this.logoff.Glyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GN_LoginLock") as Bitmap);
    }
    this.logoff.Caption = string.Empty;
    this.logoff.Hint = PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetString("Logoff");
    this.logoff.ItemClick += new ItemClickEventHandler(this.OnLogoffItemClick);
    if (!LoginManager.Instance.IsUserLoggedIn)
      this.ShowLogin();
    else
      this.InitializeComponents();
  }

  private void InitializeComponents()
  {
    this.Ribbon.PageHeaderItemLinks.Clear();
    this.Ribbon.PageHeaderItemLinks.Add(this.userInformation);
    this.Ribbon.PageHeaderItemLinks.Add(this.logoff);
    this.ClearApplicationComponentRibbon();
    foreach (IPlugin applicationComponent in this.registeredApplicationComponents)
      this.AddApplicationComponentToRibbon(applicationComponent);
    this.Ribbon.SelectedPage = this.Ribbon.Pages.Count > 0 ? this.Ribbon.Pages[0] : (RibbonPage) null;
    if (!SystemConfigurationManager.Instance.IsUserManagementActive || LoginManager.Instance.LoggedInUserData == null || LoginManager.Instance.LoggedInUserData.Entity == null || !(LoginManager.Instance.LoggedInUserData.Entity is User entity))
      return;
    this.userInformation.Caption = entity.LoginName;
    this.userInformation.Hint = string.IsNullOrEmpty(entity.FullName) ? entity.LoginName : entity.FullName;
    this.logoff.Hint = string.Format(PathMedical.UserInterface.WindowsForms.Properties.Resources.UserInterfaceManager_LogoutUserHint, (object) entity.LoginName);
  }

  private void ShowLogin()
  {
    if (this.Ribbon == null)
      return;
    SystemConfigurationManager.Instance.SetLanguage(SystemConfigurationManager.Instance.BaseLanguage);
    LoginPanel loginPanel = new LoginPanel();
    loginPanel.Dock = DockStyle.Fill;
    this.loginPanel = loginPanel;
    this.Ribbon.SuspendLayout();
    this.Ribbon.PageHeaderItemLinks.Clear();
    this.Ribbon.Pages.Clear();
    this.Ribbon.ResumeLayout();
    this.ContentPanel.SuspendLayout();
    this.ContentPanel.Controls.Clear();
    this.ContentPanel.Controls.Add((Control) this.loginPanel);
    this.ContentPanel.ResumeLayout();
  }

  public void ContinueAfterLogin()
  {
    if (LoginManager.Instance.LoggedInUserData != null && LoginManager.Instance.LoggedInUserData.Entity is User)
    {
      User entity = LoginManager.Instance.LoggedInUserData.Entity as User;
      if (!string.IsNullOrEmpty(entity.Language))
      {
        try
        {
          SystemConfigurationManager.Instance.SetLanguage(new CultureInfo(entity.Language));
        }
        catch (System.Exception ex)
        {
          UserInterfaceManager.Logger.Error(ex, "Failure while setting user language to {0}", (object) entity.LanguageName);
        }
      }
    }
    this.InitializeComponents();
  }

  private void OnLogoffItemClick(object sender, ItemClickEventArgs e)
  {
    CancelEventArgs e1 = new CancelEventArgs();
    this.RequestShutdownActiveModule(e1);
    if (e1.Cancel)
      return;
    this.ShowLogin();
    LoginManager.Instance.LogoutUser();
  }

  public void StartApplication(Form mainForm)
  {
    if (mainForm == null)
      return;
    Application.Run(mainForm);
  }

  public IEnumerable<IApplicationComponent> RegisteredApplicationComponents
  {
    get => (IEnumerable<IApplicationComponent>) this.registeredApplicationComponents;
  }

  public event EventHandler<ApplicationComponentChangingEventArgs> ApplicationComponentChanging;

  public event EventHandler<EventArgs> ApplicationComponentChanged;

  public void AddApplicationComponent(IApplicationComponent applicationComponent)
  {
    if (applicationComponent == null)
      return;
    if (this.registeredApplicationComponents.Contains(applicationComponent))
      return;
    try
    {
      this.AddApplicationComponentToRibbon((IPlugin) applicationComponent);
      this.registeredApplicationComponents.Add(applicationComponent);
    }
    catch (UserInterfaceManagerException ex)
    {
      UserInterfaceManager.Logger.Info((System.Exception) ex, "Failure while adding {0} to presentation layer", (object) applicationComponent);
    }
  }

  public void RemoveApplicationComponent(IApplicationComponent applicationComponent)
  {
    if (applicationComponent == null)
      return;
    if (!this.registeredApplicationComponents.Contains(applicationComponent))
      return;
    try
    {
      this.RemoveApplicationComponentFromRibbon((IPlugin) applicationComponent);
      this.registeredApplicationComponents.Remove(applicationComponent);
    }
    catch (UserInterfaceManagerException ex)
    {
      UserInterfaceManager.Logger.Info((System.Exception) ex, "Failure while removing {0} from presentation layer", (object) applicationComponent);
    }
  }

  private void RibbonPageChanging(object sender, RibbonPageChangingEventArgs e)
  {
    if (e == null || e.Page == null || e.Page.Tag == null)
      return;
    IApplicationComponent applicationComponent = this.GetApplicationComponent((Guid) e.Page.Tag);
    if (applicationComponent == null)
      return;
    Type activeModuleType = applicationComponent.ActiveModuleType;
    if (!(activeModuleType != (Type) null))
      return;
    Guid tag = (Guid) e.Page.Tag;
    if (!this.ChangeApplicationComponentModule(activeModuleType, (Trigger) null, tag))
    {
      e.Cancel = true;
    }
    else
    {
      if (this.activeApplicationComponentModuleHistory.Count <= 0)
        return;
      IApplicationComponentModule applicationComponentModule = this.activeApplicationComponentModuleHistory.Peek();
      this.activeApplicationComponentModuleHistory.Clear();
      this.activeApplicationComponentModuleHistory.Push(applicationComponentModule);
    }
  }

  private void AddApplicationComponentToRibbon(IPlugin plugin)
  {
    if (plugin == null || this.Ribbon == null || this.Ribbon.Pages == null || this.GetRibbonPage(plugin) != null)
      return;
    if (SystemConfigurationManager.Instance.IsUserManagementActive)
    {
      bool flag = false;
      if (plugin is IApplicationComponent)
      {
        IApplicationComponentModule applicationComponentModule = ApplicationComponentModuleManager.Instance.Get((plugin as IApplicationComponent).ActiveModuleType);
        if (applicationComponentModule != null && applicationComponentModule.IsAccessGranted(Triggers.StartModule))
          flag = true;
      }
      if (flag)
      {
        string text = GlobalResourceEnquirer.Instance.GetResourceByName($"{plugin.Fingerprint}.Name") as string;
        if (string.IsNullOrEmpty(text))
          text = plugin.Name;
        this.Ribbon.Pages.Add(new RibbonPage(text)
        {
          Tag = (object) plugin.Fingerprint
        });
      }
    }
    if (SystemConfigurationManager.Instance.IsUserManagementActive)
      return;
    this.Ribbon.Pages.Add(new RibbonPage(plugin.Name)
    {
      Tag = (object) plugin.Fingerprint
    });
  }

  private void RemoveApplicationComponentFromRibbon(IPlugin plugin)
  {
    if (plugin == null || this.Ribbon == null || this.Ribbon.Pages == null)
      return;
    RibbonPage ribbonPage = this.GetRibbonPage(plugin);
    if (ribbonPage == null)
      return;
    this.Ribbon.Pages.Remove(ribbonPage);
  }

  private void ClearApplicationComponentRibbon()
  {
    if (this.Ribbon == null || this.Ribbon.Pages == null)
      return;
    this.Ribbon.Pages.Clear();
  }

  private IApplicationComponent GetApplicationComponent(Guid fingerprint)
  {
    return this.registeredApplicationComponents.FirstOrDefault<IApplicationComponent>((Func<IApplicationComponent, bool>) (ac => ac.Fingerprint == fingerprint));
  }

  private RibbonPage GetRibbonPage(IPlugin plugin)
  {
    RibbonPage ribbonPage = (RibbonPage) null;
    if (this.Ribbon != null && this.Ribbon.Pages != null)
      ribbonPage = this.Ribbon.Pages.OfType<RibbonPage>().Where<RibbonPage>((Func<RibbonPage, bool>) (rc => rc.Tag.Equals((object) plugin.Fingerprint))).FirstOrDefault<RibbonPage>();
    return ribbonPage;
  }

  public IApplicationComponentModule ActiveApplicationComponentModule { get; private set; }

  public event EventHandler<ApplicationComponentModuleChangingEventArgs> ApplicationComponentModuleChanging;

  public event EventHandler<EventArgs> ApplicationComponentModuleChanged;

  public bool GoBackToLastApplicationComponentModule()
  {
    this.activeApplicationComponentModuleHistory.Pop();
    IApplicationComponentModule module = this.activeApplicationComponentModuleHistory.Pop();
    if (module == null)
      return false;
    this.ChangeApplicationComponentModule(module, Triggers.StartModule);
    return true;
  }

  public bool ChangeApplicationComponentModule(IApplicationComponentModule module, Trigger trigger)
  {
    if (this.Ribbon == null || this.Ribbon.SelectedPage == null || !(this.Ribbon.SelectedPage.Tag is Guid))
      return false;
    Guid tag = (Guid) this.Ribbon.SelectedPage.Tag;
    return this.ChangeApplicationComponentModule(module, trigger, tag);
  }

  public bool ChangeApplicationComponentModule(
    Type moduleType,
    Trigger additionalModuleStartTrigger,
    Guid applicationComponentId)
  {
    return this.ChangeApplicationComponentModule(ApplicationComponentModuleManager.Instance.Get(moduleType), additionalModuleStartTrigger, applicationComponentId);
  }

  public bool ChangeApplicationComponentModule(
    IApplicationComponentModule module,
    Trigger additionalModuleStartTrigger,
    Guid applicationComponentId)
  {
    if (module == null || applicationComponentId.Equals(Guid.Empty))
      return false;
    ApplicationComponentModuleChangingEventArgs e = new ApplicationComponentModuleChangingEventArgs(this.ActiveApplicationComponentModule, module);
    if (this.ApplicationComponentModuleChanging != null)
      this.ApplicationComponentModuleChanging((object) this, e);
    this.RequestShutdownActiveModule((CancelEventArgs) e);
    if (e.Cancel)
    {
      UserInterfaceManager.Logger.Debug("Aborted to change application component module from {0} to {1}.", (object) this.ActiveApplicationComponentModule, (object) module);
      return false;
    }
    this.ShowApplicationComponentModuleToolbar(module, applicationComponentId);
    this.ShowApplicationComponentModuleView(module);
    this.ResetUndoManagement();
    module.Start(additionalModuleStartTrigger);
    this.activeApplicationComponentModuleHistory.Push(module);
    if (this.ApplicationComponentModuleChanged != null)
      this.ApplicationComponentModuleChanged((object) this, (EventArgs) new ApplicationComponentModuleChangedEventArgs(module));
    return true;
  }

  private void ShowApplicationComponentModuleView(IApplicationComponentModule module)
  {
    this.ContentPanel.SuspendLayout();
    this.ContentPanel.Controls.Clear();
    this.ContentPanel.Tag = (object) module;
    this.Ribbon.ApplicationDocumentCaption = module.Name ?? string.Empty;
    if (module.ContentControl is PathMedical.UserInterface.WindowsForms.ModelViewController.View)
    {
      this.ContentPanel.Focus();
      this.ContentPanel.Controls.Add((Control) (module.ContentControl as PathMedical.UserInterface.WindowsForms.ModelViewController.View));
    }
    this.ContentPanel.ResumeLayout();
  }

  private void ShowApplicationComponentModuleToolbar(
    IApplicationComponentModule module,
    Guid applicationComponentId)
  {
    if (UserInterfaceManager.Logger.IsDebugEnabled)
    {
      if (this.ActiveApplicationComponentModule != null)
        UserInterfaceManager.Logger.Debug("Changing application component module from {0} to {1}.", (object) this.ActiveApplicationComponentModule, (object) module);
      else
        UserInterfaceManager.Logger.Debug("Starting up application component module {0}.", (object) module);
    }
    RibbonPage ribbonPage = this.Ribbon.Pages.OfType<RibbonPage>().Where<RibbonPage>((Func<RibbonPage, bool>) (rp => rp != null && rp.Tag != null)).FirstOrDefault<RibbonPage>((Func<RibbonPage, bool>) (rp => rp.Tag.Equals((object) applicationComponentId)));
    ribbonPage.Groups.Clear();
    List<RibbonPageGroup> list = module.ContentControl.ToolbarElements.OfType<RibbonPageGroup>().ToList<RibbonPageGroup>();
    foreach (BarButtonItem barButtonItem in list.SelectMany<RibbonPageGroup, BarButtonItemLink>((Func<RibbonPageGroup, IEnumerable<BarButtonItemLink>>) (bg => bg.ItemLinks.OfType<BarButtonItemLink>())).Select<BarButtonItemLink, BarButtonItem>((Func<BarButtonItemLink, BarButtonItem>) (bb => bb.Item)).ToList<BarButtonItem>())
    {
      if (!this.Ribbon.Items.Contains((BarItem) barButtonItem))
        this.Ribbon.Items.Add((BarItem) barButtonItem);
    }
    foreach (BarEditItem barEditItem in list.SelectMany<RibbonPageGroup, BarEditItemLink>((Func<RibbonPageGroup, IEnumerable<BarEditItemLink>>) (bg => bg.ItemLinks.OfType<BarEditItemLink>())).Select<BarEditItemLink, BarEditItem>((Func<BarEditItemLink, BarEditItem>) (bb => bb.Item)).ToList<BarEditItem>())
    {
      if (!this.Ribbon.Items.Contains((BarItem) barEditItem))
        this.Ribbon.Items.Add((BarItem) barEditItem);
    }
    ribbonPage.Groups.AddRange(list.ToArray());
    this.ActiveApplicationComponentModule = module;
  }

  private void RequestShutdownActiveModule(CancelEventArgs e)
  {
    if (this.ActiveApplicationComponentModule == null || this.ActiveApplicationComponentModule.Suspend())
      return;
    e.Cancel = true;
  }

  public void SetAllowedTriggers(List<Trigger> allowedTriggers, Trigger currentTrigger)
  {
    Trigger[] modificationGroupTriggers = new Trigger[3]
    {
      Triggers.Save,
      Triggers.RevertModifications,
      Triggers.Undo
    };
    try
    {
      if (!SystemConfigurationManager.Instance.IsUserManagementActive)
        return;
      List<\u003C\u003Ef__AnonymousType0<BarButtonItem, TriggerExecutionInformation>> list = this.ActiveApplicationComponentModule.ContentControl.ToolbarElements.OfType<RibbonPageGroup>().SelectMany<RibbonPageGroup, BarButtonItemLink>((Func<RibbonPageGroup, IEnumerable<BarButtonItemLink>>) (group => group.ItemLinks.OfType<BarButtonItemLink>())).Select(button => new
      {
        Item = button.Item,
        Tei = button.Item.Tag as TriggerExecutionInformation
      }).Where(i => i.Tei != null && !((IEnumerable<Trigger>) modificationGroupTriggers).Contains<Trigger>(i.Tei.Trigger)).ToList();
      foreach (var data in list)
      {
        bool flag = allowedTriggers.Contains(data.Tei.Trigger);
        data.Item.Visibility = BarItemVisibility.Always;
        data.Item.Enabled = flag;
      }
      if (currentTrigger != (Trigger) null)
      {
        var data = list.FirstOrDefault(t => t.Tei.Trigger == currentTrigger);
        if (data != null)
        {
          data.Item.Enabled = false;
          data.Item.Visibility = BarItemVisibility.Always;
        }
      }
      foreach (RibbonPageGroup ribbonPageGroup in this.ActiveApplicationComponentModule.ContentControl.ToolbarElements.OfType<RibbonPageGroup>())
        ribbonPageGroup.Visible = ribbonPageGroup.ItemLinks.PageGroup.ItemLinks.OfType<BarItemLink>().Where<BarItemLink>((Func<BarItemLink, bool>) (bil => bil.Item.Visibility != BarItemVisibility.Never)).Count<BarItemLink>() != 0;
    }
    catch (ArgumentNullException ex)
    {
      UserInterfaceManager.Logger.Error((System.Exception) ex, "Failure while setting allowed triggers.");
    }
  }

  public void ToggleToolbarElement(Trigger trigger, bool active)
  {
    if (trigger == (Trigger) null)
      return;
    try
    {
      foreach (BarButtonItem barButtonItem in this.ActiveApplicationComponentModule.ContentControl.ToolbarElements.OfType<RibbonPageGroup>().SelectMany<RibbonPageGroup, BarButtonItemLink>((Func<RibbonPageGroup, IEnumerable<BarButtonItemLink>>) (bg => bg.ItemLinks.OfType<BarButtonItemLink>())).Select<BarButtonItemLink, BarButtonItem>((Func<BarButtonItemLink, BarButtonItem>) (bb => bb.Item)).Where<BarButtonItem>((Func<BarButtonItem, bool>) (i => i.Tag is TriggerExecutionInformation && ((TriggerExecutionInformation) i.Tag).Trigger == trigger)).ToList<BarButtonItem>())
      {
        barButtonItem.ButtonStyle = active ? BarButtonStyle.Check : BarButtonStyle.Default;
        barButtonItem.Down = active;
      }
    }
    catch (ArgumentNullException ex)
    {
      UserInterfaceManager.Logger.Error("Failure while toggling trigger {0}.", (object) trigger);
    }
  }

  public RibbonPageGroup ModificationManagementGroup { get; protected set; }

  private void ResetUndoManagement()
  {
    if (this.ActiveApplicationComponentModule != null)
    {
      List<object> toolbarElements = this.ActiveApplicationComponentModule.ContentControl.ToolbarElements;
      this.saveModificationsButton = toolbarElements.OfType<RibbonPageGroup>().SelectMany<RibbonPageGroup, BarButtonItemLink>((Func<RibbonPageGroup, IEnumerable<BarButtonItemLink>>) (bg => bg.ItemLinks.OfType<BarButtonItemLink>())).Select<BarButtonItemLink, BarButtonItem>((Func<BarButtonItemLink, BarButtonItem>) (bb => bb.Item)).FirstOrDefault<BarButtonItem>((Func<BarButtonItem, bool>) (bi => bi.Tag is TriggerExecutionInformation && ((TriggerExecutionInformation) bi.Tag).Trigger == Triggers.Save));
      this.undoAllModificationsButton = toolbarElements.OfType<RibbonPageGroup>().SelectMany<RibbonPageGroup, BarButtonItemLink>((Func<RibbonPageGroup, IEnumerable<BarButtonItemLink>>) (bg => bg.ItemLinks.OfType<BarButtonItemLink>())).Select<BarButtonItemLink, BarButtonItem>((Func<BarButtonItemLink, BarButtonItem>) (bb => bb.Item)).FirstOrDefault<BarButtonItem>((Func<BarButtonItem, bool>) (bi => bi.Tag is TriggerExecutionInformation && ((TriggerExecutionInformation) bi.Tag).Trigger == Triggers.RevertModifications));
      this.undoLastModificationButton = toolbarElements.OfType<RibbonPageGroup>().SelectMany<RibbonPageGroup, BarButtonItemLink>((Func<RibbonPageGroup, IEnumerable<BarButtonItemLink>>) (bg => bg.ItemLinks.OfType<BarButtonItemLink>())).Select<BarButtonItemLink, BarButtonItem>((Func<BarButtonItemLink, BarButtonItem>) (bb => bb.Item)).FirstOrDefault<BarButtonItem>((Func<BarButtonItem, bool>) (bi => bi.Tag is TriggerExecutionInformation && ((TriggerExecutionInformation) bi.Tag).Trigger == Triggers.Undo));
    }
    else
    {
      this.saveModificationsButton = (BarButtonItem) null;
      this.undoAllModificationsButton = (BarButtonItem) null;
      this.undoLastModificationButton = (BarButtonItem) null;
    }
    CommandManager.Instance.Reset();
    this.UpdateModificationManagementButtons((object) this, EventArgs.Empty);
  }

  private void UpdateModificationManagementButtons(object sender, EventArgs e)
  {
    if (this.undoAllModificationsButton != null)
      this.undoAllModificationsButton.Enabled = CommandManager.Instance.IsUnsaved && CommandManager.Instance.IsRevertEnabled;
    if (this.saveModificationsButton != null)
      this.saveModificationsButton.Enabled = CommandManager.Instance.IsUnsaved;
    if (this.undoLastModificationButton != null)
      this.undoLastModificationButton.Enabled = CommandManager.Instance.CanUndo;
    this.Ribbon.Refresh();
  }

  public void StartAssistant(IApplicationComponentModule module, Trigger trigger)
  {
    UserInterfaceManager.Logger.Debug("Starting assistant {0}", (object) module);
    if (this.assistentForm == null)
      this.assistentForm = new AssistantForm();
    if (this.activeAssistant != null)
      this.CloseAssistant();
    this.activeAssistant = module;
    Control contentControl = (Control) this.activeAssistant.ContentControl;
    contentControl.Dock = DockStyle.Fill;
    this.assistentForm.SuspendLayout();
    this.assistentForm.StartPosition = FormStartPosition.CenterParent;
    this.assistentForm.Controls.Add(contentControl);
    this.assistentForm.ResumeLayout();
    this.activeAssistant.Start(trigger);
    int num = (int) this.assistentForm.ShowDialog((IWin32Window) this.RootForm);
    this.CloseAssistant();
  }

  public void CloseAssistant()
  {
    UserInterfaceManager.Logger.Debug("Closing assistant {0}", (object) this.activeAssistant);
    if (this.activeAssistant != null)
    {
      this.activeAssistant.Suspend();
      this.activeAssistant = (IApplicationComponentModule) null;
    }
    if (this.assistentForm == null)
      return;
    this.assistentForm.SuspendLayout();
    this.assistentForm.Controls.Clear();
    this.assistentForm.ResumeLayout();
  }

  private void ApplicationClosing(object sender, FormClosingEventArgs e)
  {
    switch (e.CloseReason)
    {
      case CloseReason.UserClosing:
      case CloseReason.TaskManagerClosing:
      case CloseReason.ApplicationExitCall:
        this.RequestShutdownActiveModule((CancelEventArgs) e);
        break;
    }
  }

  public string HelpUrl { get; set; }

  public void GetHelp(IView view, string helpContext)
  {
    if (string.IsNullOrEmpty(this.HelpUrl))
      return;
    string url = string.Format(this.HelpUrl, (object) SystemConfigurationManager.Instance.CurrentLanguage.IetfLanguageTag);
    if (string.IsNullOrEmpty(helpContext) && view is PathMedical.UserInterface.WindowsForms.ModelViewController.View)
      Help.ShowHelp((Control) (view as PathMedical.UserInterface.WindowsForms.ModelViewController.View), url, HelpNavigator.TableOfContents);
    else
      Help.ShowHelp((Control) (view as PathMedical.UserInterface.WindowsForms.ModelViewController.View), url, HelpNavigator.Topic, (object) helpContext);
  }

  private void RequestHelpForCurrentModule()
  {
    if (this.ActiveApplicationComponentModule == null || this.ActiveApplicationComponentModule.ContentControl == null)
      return;
    string fieldId = string.Empty;
    if (this.ActiveApplicationComponentModule.ContentControl is PathMedical.UserInterface.WindowsForms.ModelViewController.View)
      fieldId = (this.ActiveApplicationComponentModule.ContentControl as PathMedical.UserInterface.WindowsForms.ModelViewController.View).RequestContextHelp();
    HelpRequestTriggerContext context = new HelpRequestTriggerContext(this.ActiveApplicationComponentModule.ContentControl, fieldId);
    this.ActiveApplicationComponentModule.ContentControl.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Help, (TriggerContext) context));
  }

  private void OnRootFormKeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.F1)
      this.RequestHelpForCurrentModule();
    if (e.Control && e.KeyCode == Keys.S)
      this.RequestSaveForCurrentModule();
    if (e.Control && e.KeyCode == Keys.Z)
      this.RequestUndoForCurrentModule();
    if (e.KeyCode == Keys.Delete)
      this.RequestDeleteTest();
    if (e.Control && e.KeyCode == Keys.X)
      this.RequestCutTest();
    if (!e.Control || e.KeyCode != Keys.V)
      return;
    this.RequestPasteTest();
  }

  private void RequestSaveForCurrentModule()
  {
    if (this.ActiveApplicationComponentModule == null || this.ActiveApplicationComponentModule.ContentControl == null)
      return;
    this.ActiveApplicationComponentModule.ContentControl.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Save, (TriggerContext) null));
  }

  private void RequestUndoForCurrentModule()
  {
    if (this.ActiveApplicationComponentModule == null || this.ActiveApplicationComponentModule.ContentControl == null)
      return;
    this.ActiveApplicationComponentModule.ContentControl.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Undo, (TriggerContext) null));
  }

  private void RequestDeleteTest()
  {
    if (this.ActiveApplicationComponentModule == null || this.ActiveApplicationComponentModule.ContentControl == null)
      return;
    this.ActiveApplicationComponentModule.ContentControl.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.DeleteSubItem, (TriggerContext) null));
  }

  private void RequestCutTest()
  {
    if (this.ActiveApplicationComponentModule == null || this.ActiveApplicationComponentModule.ContentControl == null)
      return;
    this.ActiveApplicationComponentModule.ContentControl.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Cut, (TriggerContext) null));
  }

  private void RequestPasteTest()
  {
    if (this.ActiveApplicationComponentModule == null || this.ActiveApplicationComponentModule.ContentControl == null)
      return;
    this.ActiveApplicationComponentModule.ContentControl.RequestControllerAction((object) this, new TriggerEventArgs(Triggers.Paste, (TriggerContext) null));
  }

  public string Skin
  {
    get => this.lookAndFeel.LookAndFeel.ActiveSkinName;
    set
    {
      this.lookAndFeel.LookAndFeel.UseWindowsXPTheme = false;
      this.lookAndFeel.LookAndFeel.SkinName = value;
    }
  }

  public IEnumerable<string> Skins
  {
    get
    {
      List<string> skins = new List<string>();
      foreach (SkinContainer skin in (CollectionBase) SkinManager.Default.Skins)
        skins.Add(skin.SkinName);
      return (IEnumerable<string>) skins;
    }
  }

  public void SetFormSubmissionControl(object control)
  {
    if (this.RootForm == null)
      return;
    if (control is IButtonControl)
      this.RootForm.AcceptButton = control as IButtonControl;
    else
      this.RootForm.AcceptButton = (IButtonControl) null;
  }

  public AnswerType ShowMessageBox(
    string text,
    string caption,
    AnswerOptionType possibleAnswerOptionTypes,
    QuestionIcon questionIcon)
  {
    MessageBoxButtons buttons = (MessageBoxButtons) possibleAnswerOptionTypes;
    MessageBoxIcon icon = (MessageBoxIcon) questionIcon;
    return (AnswerType) MessageBox.Show(text, caption, buttons, icon);
  }
}
