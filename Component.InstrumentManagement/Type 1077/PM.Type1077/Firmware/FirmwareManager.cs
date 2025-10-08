// Decompiled with JetBrains decompiler
// Type: PathMedical.Type1077.Firmware.FirmwareManager
// Assembly: PM.Type1077, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 144A5CD5-1259-4BA4-983B-135CF20CDF2F
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.Type1077.dll

using PathMedical.DatabaseManagement;
using PathMedical.DataExchange;
using PathMedical.Encryption;
using PathMedical.Exception;
using PathMedical.Extensions;
using PathMedical.FileSystem;
using PathMedical.Logging;
using PathMedical.ResourceManager;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;

#nullable disable
namespace PathMedical.Type1077.Firmware;

public class FirmwareManager : ISingleSelectionModel<FirmwareImage>, IModel
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (FirmwareManager), "$Rev: 1472 $");
  private readonly ModelHelper<FirmwareImage, FirmwareImageAdapter> modelHelper;
  private FirmwareImportProgress firmwareImportProgress;
  private bool cancelFirmwareImport;
  private BackgroundWorker importWorker;

  public static FirmwareManager Instance => PathMedical.Singleton.Singleton<FirmwareManager>.Instance;

  private FirmwareManager()
  {
    this.modelHelper = new ModelHelper<FirmwareImage, FirmwareImageAdapter>(new EventHandler<ModelChangedEventArgs>(this.ModelChanged), Array.Empty<string>());
  }

  public FirmwareImage SelectedItem
  {
    get
    {
      return this.modelHelper.SelectedItems == null ? (FirmwareImage) null : this.modelHelper.SelectedItems.FirstOrDefault<FirmwareImage>();
    }
    set
    {
      this.modelHelper.SelectedItems = (ICollection<FirmwareImage>) new FirmwareImage[1]
      {
        value
      };
      if (this.Changed == null)
        return;
      this.ModelChanged((object) this, ModelChangedEventArgs.Create<ICollection<FirmwareImage>>(this.modelHelper.SelectedItems, ChangeType.ListLoaded));
    }
  }

  public ICollection SelectedItems
  {
    set
    {
      this.modelHelper.SelectedItems = (ICollection<FirmwareImage>) value.OfType<FirmwareImage>().ToList<FirmwareImage>();
    }
  }

  [CLSCompliant(false)]
  public event EventHandler<ModelChangedEventArgs> Changed;

  private void ModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed(sender, e);
  }

  public List<FirmwareImage> Firmwares
  {
    get
    {
      if (this.modelHelper.Items == null)
        this.LoadFirmware();
      return this.modelHelper.Items;
    }
  }

  private void LoadFirmware()
  {
    this.modelHelper.LoadItems((Func<FirmwareImageAdapter, ICollection<FirmwareImage>>) (adapter => adapter.All));
  }

  public void Store()
  {
    FirmwareImage selectedFirmware = this.modelHelper.SelectedItems.OfType<FirmwareImage>().SingleOrDefault<FirmwareImage>();
    if (selectedFirmware != null && this.Firmwares.Any<FirmwareImage>((Func<FirmwareImage, bool>) (f => f.InstrumentTypeSignature != selectedFirmware.InstrumentTypeSignature && f.Languages.ToString().Equals(selectedFirmware.Languages.ToString()) && DateTime.Compare(f.DateTime, selectedFirmware.DateTime) > 0)))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<PathMedical.InstrumentManagement.Properties.Resources>.Instance.ResourceManager.GetString("BetterFirmwareExists"));
    this.modelHelper.Store();
  }

  private static void Store(FirmwareImage firmware)
  {
    using (DBScope scope = new DBScope())
    {
      new FirmwareImageAdapter(scope).Store(firmware);
      scope.Complete();
    }
  }

  public void Delete()
  {
    this.modelHelper.Delete();
    this.RefreshData();
  }

  public void CancelNewItem() => this.modelHelper.CancelAddItem();

  public void PrepareAddItem() => this.modelHelper.PrepareAddItem(new FirmwareImage());

  public void RefreshData()
  {
    this.LoadFirmware();
    if (this.Changed == null)
      return;
    this.ModelChanged((object) this, ModelChangedEventArgs.Create<List<FirmwareImage>>(this.modelHelper.Items, ChangeType.ListLoaded));
    this.Changed((object) this, ModelChangedEventArgs.Create<ICollection<FirmwareImage>>(this.modelHelper.SelectedItems, ChangeType.SelectionChanged));
  }

  public void RevertModifications()
  {
    if (this.SelectedItem != null && this.SelectedItem.Id == Guid.Empty)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<FirmwareImage>(this.SelectedItem, ChangeType.SelectionChanged));
    }
    else
      this.modelHelper.RefreshSelectedItems();
  }

  public string ImportFromFolder { get; set; }

  public bool SearchImportFolderRecursive { get; set; }

  public void StartImport()
  {
    this.cancelFirmwareImport = false;
    this.importWorker = new BackgroundWorker();
    this.importWorker.DoWork += new DoWorkEventHandler(this.Import);
    this.importWorker.RunWorkerAsync();
  }

  public void CancelImport() => this.cancelFirmwareImport = true;

  private void Import(object sender, DoWorkEventArgs e)
  {
    try
    {
      this.firmwareImportProgress = new FirmwareImportProgress()
      {
        Folder = this.ImportFromFolder,
        ProcessedFiles = 0,
        TotalFiles = 0,
        ProcessState = ProcessState.Processing
      };
      this.ReportFirmwareImportActivity(PathMedical.Type1077.Properties.Resources.FirmwareManager_Import_Searching, ProcessState.Processing, 1, 0);
      string[] firmwareImageCandidates = this.GetFirmwareImageCandidates(this.ImportFromFolder);
      string[] source = (string[]) null;
      if (!this.cancelFirmwareImport)
      {
        this.ReportFirmwareImportActivity(PathMedical.Type1077.Properties.Resources.FirmwareManager_Import_Validating, ProcessState.Processing, firmwareImageCandidates != null ? ((IEnumerable<string>) firmwareImageCandidates).Count<string>() : 0, 0);
        source = this.GetValidCandidates(firmwareImageCandidates);
      }
      if (source != null && !this.cancelFirmwareImport)
      {
        this.ReportFirmwareImportActivity(PathMedical.Type1077.Properties.Resources.FirmwareManager_Import_CheckingUpdate, ProcessState.Processing, ((IEnumerable<string>) source).Count<string>(), 0);
        int current = 0;
        try
        {
          foreach (string str in source)
          {
            ++current;
            this.ReportFirmwareImportProgress(str, current);
            this.Load(str);
            if (this.cancelFirmwareImport)
              break;
          }
        }
        catch
        {
          this.ReportFirmwareImportActivity(PathMedical.Type1077.Properties.Resources.FirmwareManager_Import_Failed, ProcessState.Cancelled, 0, 0);
        }
      }
      if (!this.cancelFirmwareImport)
        this.ReportFirmwareImportActivity(PathMedical.Type1077.Properties.Resources.FirmwareManager_Import_completed, ProcessState.Completed, 0, 0);
      else
        this.ReportFirmwareImportActivity(PathMedical.Type1077.Properties.Resources.FirmwareManager_Import_canceled, ProcessState.Cancelled, 0, 0);
    }
    catch (System.Exception ex)
    {
      this.ReportFirmwareImportActivity(string.Format(PathMedical.Type1077.Properties.Resources.FirmwareManager_FirmwareImageImportFailed, (object) ex.Message), ProcessState.Failed, 0, 0);
    }
  }

  public string[] GetFirmwareImageCandidates(string folder)
  {
    if (string.IsNullOrEmpty(folder))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (folder));
    if (!FileSystemHelper.DoesFolderExists(folder))
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format(PathMedical.Type1077.Properties.Resources.FirmwareManager_FolderNotFound, (object) folder));
    string[] firmwareImageCandidates = (string[]) null;
    try
    {
      firmwareImageCandidates = Directory.GetFiles(folder, "*.xml", SearchOption.TopDirectoryOnly);
    }
    catch (UnauthorizedAccessException ex)
    {
      FirmwareManager.Logger.Info("UnauthorizedAccessException while searching candidates for firmware update.", (object) ex);
    }
    return firmwareImageCandidates;
  }

  public string[] GetValidCandidates(string[] candidates)
  {
    List<string> stringList = new List<string>();
    int current = 0;
    if (candidates != null)
    {
      XmlHelper xmlHelper = new XmlHelper();
      foreach (string candidate in candidates)
      {
        ++current;
        this.ReportFirmwareImportProgress(string.Format(PathMedical.Type1077.Properties.Resources.FirmwareManager_InspectingFiles, (object) candidate), current);
        XmlSchema resourceEmbeddedSchema = XmlHelper.GetResourceEmbeddedSchema(Assembly.GetExecutingAssembly(), "PathMedical.Type1077.Firmware.Firmware.xsd");
        XDocument document = XmlHelper.GetDocument(candidate);
        if (document != null && xmlHelper.IsXmlDocumentValid(document, resourceEmbeddedSchema))
          stringList.Add(candidate);
        if (this.cancelFirmwareImport)
          break;
      }
    }
    return stringList.ToArray();
  }

  public void Load(string file)
  {
    this.firmwareImportProgress.CurrentFile = !string.IsNullOrEmpty(file) ? file : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (file));
    XElement container = FileSystemHelper.DoesFileExists(file) ? XElement.Load(file) : throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format(PathMedical.Type1077.Properties.Resources.FirmwareManager_FileAccessError, (object) file));
    FirmwareImage firmware = new FirmwareImage()
    {
      InstrumentTypeSignature = new Guid(container.SafeElement("InstrumentType").Value),
      DateTime = DateTime.Parse(container.SafeElement("Date").Value),
      CheckSum = container.SafeElement("CheckSum").Value,
      Version = container.SafeElement("Version").Value,
      LanguagePackName = container.SafeElement("LanguagePack").SafeAttribute("ID").Value,
      Languages = FirmwareManager.GetLanguages((XContainer) container.SafeElement("LanguagePack")),
      BuildNumber = long.Parse(container.SafeElement("BuildNumber").Value)
    };
    firmware.Image = FirmwareManager.GetImage(firmware.CheckSum, container.SafeElement("Image"));
    this.LoadFirmware();
    FirmwareImage firmware1 = this.Firmwares.FirstOrDefault<FirmwareImage>((Func<FirmwareImage, bool>) (f => f.InstrumentTypeSignature == firmware.InstrumentTypeSignature && string.Compare(f.LanguagePackName, firmware.LanguagePackName, true) == 0));
    if (firmware1 != null)
    {
      if (firmware.BuildNumber <= firmware1.BuildNumber)
        return;
      firmware1.DateTime = firmware.DateTime;
      firmware1.CheckSum = firmware.CheckSum;
      firmware1.Version = firmware.Version;
      firmware1.Languages = firmware.Languages;
      firmware1.Image = firmware.Image;
      firmware1.BuildNumber = firmware.BuildNumber;
      FirmwareManager.Store(firmware1);
      this.firmwareImportProgress.AddSuccessfullyImportedFile(file);
    }
    else
    {
      FirmwareManager.Store(firmware);
      this.firmwareImportProgress.AddSuccessfullyImportedFile(file);
    }
  }

  public FirmwareImage LoadFirmwareFromFile(string file)
  {
    if (string.IsNullOrEmpty(file))
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (file));
    XElement container = FileSystemHelper.DoesFileExists(file) ? XElement.Load(file) : throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format(PathMedical.Type1077.Properties.Resources.FirmwareManager_FileAccessError, (object) file));
    FirmwareImage firmwareImage = new FirmwareImage()
    {
      InstrumentTypeSignature = new Guid(container.SafeElement("InstrumentType").Value),
      DateTime = DateTime.Parse(container.SafeElement("Date").Value),
      CheckSum = container.SafeElement("CheckSum").Value,
      Version = container.SafeElement("Version").Value,
      LanguagePackName = container.SafeElement("LanguagePack").SafeAttribute("ID").Value,
      Languages = FirmwareManager.GetLanguages((XContainer) container.SafeElement("LanguagePack")),
      BuildNumber = long.Parse(container.SafeElement("BuildNumber").Value)
    };
    firmwareImage.Image = FirmwareManager.GetImage(firmwareImage.CheckSum, container.SafeElement("Image"));
    return firmwareImage;
  }

  private static string GetLanguages(XContainer element)
  {
    StringBuilder stringBuilder = new StringBuilder();
    string str = "";
    foreach (XElement element1 in element.Elements())
    {
      if (!string.IsNullOrEmpty(element1.Value))
      {
        stringBuilder.AppendFormat("{1}{0}", (object) element1.Value, (object) str);
        str = ",";
      }
    }
    return stringBuilder.ToString();
  }

  private static byte[] GetImage(string checkSum, XElement xmlImageElement)
  {
    byte[] image = new byte[0];
    if (xmlImageElement == null)
      return image;
    string str = xmlImageElement.Value;
    string md5Hash = MD5Engine.GetMD5Hash(str);
    if (str != null && string.Compare(md5Hash, checkSum, true) == 0)
      image = Convert.FromBase64String(str);
    return image;
  }

  private void ReportFirmwareImportActivity(
    string activity,
    ProcessState processState,
    int maximum,
    int current)
  {
    if (this.firmwareImportProgress == null)
      this.firmwareImportProgress = new FirmwareImportProgress();
    this.firmwareImportProgress.ActivityDescription = activity;
    this.firmwareImportProgress.ProcessState = processState;
    this.firmwareImportProgress.TotalFiles = maximum;
    this.firmwareImportProgress.ProcessedFiles = current;
    this.Changed((object) this, ModelChangedEventArgs.Create<FirmwareImportProgress>(this.firmwareImportProgress, ChangeType.ItemEdited));
  }

  private void ReportFirmwareImportProgress(string subTitle, int current)
  {
    if (this.firmwareImportProgress == null)
      this.firmwareImportProgress = new FirmwareImportProgress();
    this.firmwareImportProgress.CurrentFile = subTitle;
    this.firmwareImportProgress.ProcessedFiles = current;
    this.Changed((object) this, ModelChangedEventArgs.Create<FirmwareImportProgress>(this.firmwareImportProgress, ChangeType.ItemEdited));
  }

  public void CreateFirmwareFile(string binaryImageFileName)
  {
  }

  public string CreateBase64EncodedImage(string file)
  {
    FileIOPermission fileIoPermission = !string.IsNullOrEmpty(file) ? new FileIOPermission(FileIOPermissionAccess.AllAccess, file) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (file));
    try
    {
      fileIoPermission.Demand();
    }
    catch (SecurityException ex)
    {
      throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format(PathMedical.Type1077.Properties.Resources.FirmwareManager_CreateBase64EncodedImage_FileAccessDenied, (object) file), (System.Exception) ex);
    }
    string base64EncodedImage = string.Empty;
    byte[] numArray = new byte[0];
    if (File.Exists(file))
      numArray = File.ReadAllBytes(file);
    if (((IEnumerable<byte>) numArray).Count<byte>() > 0)
      base64EncodedImage = Convert.ToBase64String(numArray);
    return base64EncodedImage;
  }

  public byte[] CreateBinaryImage(string base64EncodedImage)
  {
    return !string.IsNullOrEmpty(base64EncodedImage) ? Convert.FromBase64String(base64EncodedImage) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (base64EncodedImage));
  }

  public void ChangeSingleSelection(FirmwareImage selection) => throw new NotImplementedException();

  public bool IsOneItemSelected<T>() where T : FirmwareImage => throw new NotImplementedException();

  bool ISingleSelectionModel<FirmwareImage>.IsOneItemAvailable<T>()
  {
    throw new NotImplementedException();
  }
}
