// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress.RibbonHelper
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;

public class RibbonHelper
{
  private readonly IView viewToSupport;

  public RibbonHelper(IView view)
  {
    this.viewToSupport = view != null ? view : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
  }

  public RibbonPageGroup CreateRibbonPageGroup([Localizable(true)] string caption)
  {
    return new RibbonPageGroup()
    {
      Text = caption,
      ShowCaptionButton = false
    };
  }

  public RibbonPageGroup CreateRibbonPageGroup([Localizable(true)] string caption, Trigger trigger)
  {
    RibbonPageGroup resultToDeliver = this.CreateRibbonPageGroup(caption);
    resultToDeliver.ShowCaptionButton = true;
    resultToDeliver.CaptionButtonClick += (RibbonPageGroupEventHandler) ((_param1, _param2) => this.viewToSupport.RequestControllerAction((object) resultToDeliver, new TriggerEventArgs(trigger)));
    return resultToDeliver;
  }

  public RibbonPageGroup CreateModificationGroup([Localizable(true)] string modificationObjectName)
  {
    RibbonPageGroup ribbonPageGroup = this.CreateRibbonPageGroup(Resources.RibbonHelper_RibbonModificaitonGroup);
    BarButtonItem largeImageButton1 = this.CreateLargeImageButton(Resources.RibbonHelper_SaveButtonName, string.Empty, string.Empty, PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GN_Save") as Bitmap, Triggers.Save);
    if (Application.ProductName.Equals("Mira"))
    {
      largeImageButton1.Glyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("Save") as Bitmap);
      largeImageButton1.LargeGlyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("Save") as Bitmap);
    }
    BarButtonItem largeImageButton2 = this.CreateLargeImageButton(Resources.RibbonHelper_RevertButtonName, string.Empty, string.Empty, PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GN_TrashEmpty") as Bitmap, Triggers.RevertModifications);
    if (Application.ProductName.Equals("Mira"))
    {
      largeImageButton2.Glyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("TrashEmpty") as Bitmap);
      largeImageButton2.LargeGlyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("TrashEmpty") as Bitmap);
    }
    BarButtonItem largeImageButton3 = this.CreateLargeImageButton(Resources.RibbonHelper_UndoButtonName, string.Empty, string.Empty, PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GN_Undo") as Bitmap, Triggers.Undo);
    if (Application.ProductName.Equals("Mira"))
    {
      largeImageButton3.Glyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("Undo") as Bitmap);
      largeImageButton3.LargeGlyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("Undo") as Bitmap);
    }
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton1);
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton2);
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton3);
    return ribbonPageGroup;
  }

  public RibbonPageGroup CreateNavigationGroup([Localizable(true)] string modificationObjectName)
  {
    RibbonPageGroup ribbonPageGroup = this.CreateRibbonPageGroup(Resources.RibbonHelper_NavigationGroupName);
    BarButtonItem largeImageButton = this.CreateLargeImageButton(Resources.RibbonHelper_GoBackButtonName, string.Empty, $"Go back to {modificationObjectName}", PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GN_GoBack") as Bitmap, Triggers.GoBack);
    if (Application.ProductName.Equals("Mira"))
    {
      largeImageButton.Glyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GoBack") as Bitmap);
      largeImageButton.LargeGlyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GoBack") as Bitmap);
    }
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton);
    return ribbonPageGroup;
  }

  public RibbonPageGroup CreateHelpGroup([Localizable(true)] string modificationObjectName, PathMedical.UserInterface.WindowsForms.ModelViewController.View view)
  {
    RibbonPageGroup ribbonPageGroup = this.CreateRibbonPageGroup(Resources.RibbonHelper_HelpGroupName);
    BarButtonItem barButtonItem = new BarButtonItem();
    barButtonItem.Caption = Resources.RibbonHelper_HelpButtonName;
    barButtonItem.Description = string.Empty;
    barButtonItem.Hint = string.Format(Resources.RibbonHelper_HelpButtonModuleHint, (object) modificationObjectName);
    barButtonItem.Glyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GN_Help") as Bitmap);
    barButtonItem.LargeGlyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("GN_Help") as Bitmap);
    barButtonItem.RibbonStyle = RibbonItemStyles.Large;
    barButtonItem.Tag = (object) new TriggerExecutionInformation(Triggers.Help);
    BarButtonItem helpButton = barButtonItem;
    if (Application.ProductName.Equals("Mira"))
    {
      helpButton.Glyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("Help") as Bitmap);
      helpButton.LargeGlyph = (Image) (PathMedical.UserInterface.WindowsForms.ComponentResourceManagement.Instance.ResourceManager.GetObject("Help") as Bitmap);
    }
    helpButton.ItemClick += (ItemClickEventHandler) ((_param1, _param2) =>
    {
      HelpRequestTriggerContext context = new HelpRequestTriggerContext((IView) view, view.RequestContextHelp());
      this.viewToSupport.RequestControllerAction((object) helpButton, new TriggerEventArgs(Triggers.Help, (TriggerContext) context));
    });
    ribbonPageGroup.ItemLinks.Add((BarItem) helpButton);
    return ribbonPageGroup;
  }

  public RibbonPageGroup CreateMaintenanceGroup(
    [Localizable(true)] string modificationObjectName,
    Bitmap addImage,
    Bitmap editImage,
    Bitmap deleteImage)
  {
    RibbonPageGroup ribbonPageGroup = this.CreateRibbonPageGroup(modificationObjectName);
    BarButtonItem largeImageButton1 = this.CreateLargeImageButton(Resources.RibbonHelper_AddButtonName, string.Empty, string.Empty, addImage, Triggers.Add);
    BarButtonItem largeImageButton2 = this.CreateLargeImageButton(Resources.RibbonHelper_EditButtonName, string.Empty, string.Empty, editImage, Triggers.StartModule);
    BarButtonItem largeImageButton3 = this.CreateLargeImageButton(Resources.RibbonHelper_DeleteButtonName, string.Empty, string.Empty, deleteImage, Triggers.Delete);
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton1);
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton2);
    ribbonPageGroup.ItemLinks.Add((BarItem) largeImageButton3);
    return ribbonPageGroup;
  }

  public BarButtonItem CreateLargeImageButton(
    [Localizable(true)] string caption,
    [Localizable(true)] string description,
    [Localizable(true)] string tooltip,
    Bitmap image,
    Trigger trigger)
  {
    BarButtonItem barButtonItem = new BarButtonItem();
    barButtonItem.Caption = caption;
    barButtonItem.Description = description;
    barButtonItem.Hint = tooltip;
    barButtonItem.Glyph = (Image) image;
    barButtonItem.LargeGlyph = (Image) image;
    barButtonItem.RibbonStyle = RibbonItemStyles.Large;
    barButtonItem.Tag = (object) new TriggerExecutionInformation(trigger);
    BarButtonItem resultToDeliver = barButtonItem;
    resultToDeliver.ItemClick += (ItemClickEventHandler) ((_param1, _param2) => this.viewToSupport.RequestControllerAction((object) resultToDeliver, new TriggerEventArgs(trigger)));
    return resultToDeliver;
  }

  public BarCheckItem CreateLargeToggleButton(
    [Localizable(true)] string caption,
    [Localizable(true)] string description,
    [Localizable(true)] string tooltip,
    Bitmap image,
    Trigger trigger)
  {
    BarCheckItem barCheckItem = new BarCheckItem();
    barCheckItem.Caption = caption;
    barCheckItem.Description = description;
    barCheckItem.Hint = tooltip;
    barCheckItem.Glyph = (Image) image;
    barCheckItem.LargeGlyph = (Image) image;
    barCheckItem.RibbonStyle = RibbonItemStyles.Large;
    barCheckItem.Tag = (object) new TriggerExecutionInformation(trigger);
    BarCheckItem resultToDeliver = barCheckItem;
    resultToDeliver.ItemClick += (ItemClickEventHandler) ((_param1, _param2) => this.viewToSupport.RequestControllerAction((object) resultToDeliver, new TriggerEventArgs(trigger)));
    return resultToDeliver;
  }

  public BarButtonItem CreateLargeImageButton(
    Guid permissionId,
    [Localizable(true)] string caption,
    [Localizable(true)] string description,
    [Localizable(true)] string tooltip,
    Bitmap image,
    Trigger trigger)
  {
    BarButtonItem largeImageButton = this.CreateLargeImageButton(caption, description, tooltip, image, trigger);
    largeImageButton.Tag = (object) new TriggerExecutionInformation(trigger, permissionId);
    return largeImageButton;
  }

  public BarButtonItem CreateLargeImageButton(
    [Localizable(true)] string caption,
    [Localizable(true)] string description,
    [Localizable(true)] string tooltip,
    Bitmap image,
    Trigger trigger,
    ItemClickEventHandler eventHandler)
  {
    BarButtonItem barButtonItem = new BarButtonItem();
    barButtonItem.Caption = caption;
    barButtonItem.Description = description;
    barButtonItem.Hint = tooltip;
    barButtonItem.Glyph = (Image) image;
    barButtonItem.LargeGlyph = (Image) image;
    barButtonItem.RibbonStyle = RibbonItemStyles.Large;
    barButtonItem.Tag = (object) new TriggerExecutionInformation(trigger);
    BarButtonItem largeImageButton = barButtonItem;
    if (eventHandler != null)
      largeImageButton.ItemClick += eventHandler;
    return largeImageButton;
  }

  public BarButtonItem CreateLargeImageButton(
    [Localizable(true)] string caption,
    [Localizable(true)] string description,
    [Localizable(true)] string tooltip,
    Bitmap enabledImage,
    Bitmap disabledImage,
    Trigger trigger)
  {
    BarButtonItem largeImageButton = this.CreateLargeImageButton(caption, description, tooltip, enabledImage, trigger);
    largeImageButton.LargeGlyphDisabled = (Image) disabledImage;
    return largeImageButton;
  }

  public BarButtonItem CreateLargeImageButton(
    Guid permissionId,
    [Localizable(true)] string caption,
    [Localizable(true)] string description,
    [Localizable(true)] string tooltip,
    Bitmap enabledImage,
    Bitmap disabledImage,
    Trigger trigger)
  {
    BarButtonItem largeImageButton = this.CreateLargeImageButton(permissionId, caption, description, tooltip, enabledImage, trigger);
    largeImageButton.LargeGlyphDisabled = (Image) disabledImage;
    return largeImageButton;
  }

  public BarButtonItem CreateLargeImageButton(
    IApplicationComponentModule applicationComponentModule)
  {
    string caption = GlobalResourceEnquirer.Instance.GetResourceByName($"{applicationComponentModule.Id}.Name") as string;
    string description = GlobalResourceEnquirer.Instance.GetResourceByName($"{applicationComponentModule.Id}.Description") as string;
    if (string.IsNullOrEmpty(caption))
      caption = applicationComponentModule.ShortcutName ?? applicationComponentModule.Name;
    if (string.IsNullOrEmpty(description))
      description = applicationComponentModule.Description;
    return this.CreateLargeImageButton(caption, description, string.Empty, applicationComponentModule.Image as Bitmap, new Trigger(applicationComponentModule.Id.ToString()));
  }

  public BarButtonItem CreateLargeImageButton(
    [Localizable(true)] string caption,
    [Localizable(true)] string description,
    [Localizable(true)] string tooltip,
    Bitmap image,
    Trigger trigger,
    TriggerContext context)
  {
    BarButtonItem barButtonItem = new BarButtonItem();
    barButtonItem.Caption = caption;
    barButtonItem.Description = description;
    barButtonItem.Hint = tooltip;
    barButtonItem.Glyph = (Image) image;
    barButtonItem.LargeGlyph = (Image) image;
    barButtonItem.RibbonStyle = RibbonItemStyles.Large;
    barButtonItem.Tag = (object) new TriggerExecutionInformation(trigger);
    BarButtonItem resultToDeliver = barButtonItem;
    resultToDeliver.ItemClick += (ItemClickEventHandler) ((_param1, _param2) => this.viewToSupport.RequestControllerAction((object) resultToDeliver, new TriggerEventArgs(trigger, context)));
    return resultToDeliver;
  }

  public BarButtonItem CreateLargeDropDownButton(
    [Localizable(true)] string caption,
    [Localizable(true)] string description,
    [Localizable(true)] string tooltip,
    Bitmap image,
    GalleryDropDown dropDownControl)
  {
    BarButtonItem largeDropDownButton = new BarButtonItem();
    largeDropDownButton.ButtonStyle = BarButtonStyle.DropDown;
    largeDropDownButton.DropDownControl = (PopupControl) dropDownControl;
    largeDropDownButton.ActAsDropDown = true;
    largeDropDownButton.Caption = caption;
    largeDropDownButton.Description = description;
    largeDropDownButton.Hint = tooltip;
    largeDropDownButton.Glyph = (Image) image;
    largeDropDownButton.LargeGlyph = (Image) image;
    largeDropDownButton.RibbonStyle = RibbonItemStyles.Large;
    return largeDropDownButton;
  }

  public GalleryDropDown CreateGalleryDropDown()
  {
    GalleryDropDown galleryDropDown = new GalleryDropDown();
    galleryDropDown.ShowCaption = true;
    galleryDropDown.Gallery.ShowItemText = true;
    galleryDropDown.Gallery.ShowScrollBar = ShowScrollBar.Auto;
    galleryDropDown.Gallery.CheckDrawMode = CheckDrawMode.ImageAndText;
    galleryDropDown.Gallery.ImageSize = new Size(32 /*0x20*/, 32 /*0x20*/);
    galleryDropDown.Gallery.Appearance.ItemCaption.TextOptions.HAlignment = HorzAlignment.Near;
    galleryDropDown.Gallery.Appearance.ItemCaption.Options.UseTextOptions = true;
    galleryDropDown.Gallery.Appearance.ItemDescription.TextOptions.HAlignment = HorzAlignment.Near;
    galleryDropDown.Gallery.Appearance.ItemDescription.TextOptions.VAlignment = VertAlignment.Bottom;
    galleryDropDown.Gallery.Appearance.ItemDescription.Options.UseTextOptions = true;
    galleryDropDown.GalleryItemClick += (GalleryItemClickEventHandler) ((sender, e) =>
    {
      if (e == null || e.Item == null || !(e.Item.Tag is TriggerExecutionInformation))
        return;
      this.viewToSupport.RequestControllerAction((object) this, new TriggerEventArgs((e.Item.Tag as TriggerExecutionInformation).Trigger));
    });
    return galleryDropDown;
  }

  public GalleryItemGroup CreateGalleryGroup([Localizable(true)] string caption)
  {
    return new GalleryItemGroup() { Caption = caption };
  }

  public GalleryItem CreateGalleryItem(
    [Localizable(true)] string caption,
    [Localizable(true)] string description,
    [Localizable(true)] string tooltip,
    Bitmap image,
    Trigger trigger)
  {
    return new GalleryItem()
    {
      Caption = caption,
      Description = description,
      Hint = tooltip,
      Image = (Image) image,
      HoverImage = (Image) image,
      Tag = (object) new TriggerExecutionInformation(trigger)
    };
  }

  public GalleryItem CreateGalleryItem(IApplicationComponentModule module)
  {
    return new GalleryItem()
    {
      Caption = module.ShortcutName ?? module.Name,
      Description = module.Description,
      Image = (Image) (module.Image as Bitmap),
      HoverImage = (Image) (module.Image as Bitmap),
      Tag = (object) new TriggerExecutionInformation(new Trigger(module.Id.ToString()))
    };
  }
}
