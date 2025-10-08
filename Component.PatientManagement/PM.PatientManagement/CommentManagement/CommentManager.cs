// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.CommentManagement.CommentManager
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.DatabaseManagement;
using PathMedical.Exception;
using PathMedical.PatientManagement.DataAccessLayer;
using PathMedical.ResourceManager;
using PathMedical.SystemConfiguration;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;

#nullable disable
namespace PathMedical.PatientManagement.CommentManagement;

public class CommentManager : 
  ISingleEditingModel,
  IModel,
  ISingleSelectionModel<PredefinedComment>,
  ISupportInternationalization
{
  private readonly Guid commentMaintenanceAccessPermissionId = new Guid("C7C2F750-58B4-486A-B23D-4AB8485E75C9");
  private readonly ModelHelper<PredefinedComment, PredefinedCommentAdapter> commentModelHelper;

  public static CommentManager Instance => PathMedical.Singleton.Singleton<CommentManager>.Instance;

  public Guid MaintenanceAccessPermissionId => this.commentMaintenanceAccessPermissionId;

  public PredefinedComment SelectedItem
  {
    get => this.commentModelHelper.SelectedItem;
    set => this.commentModelHelper.SelectedItem = value;
  }

  public List<PredefinedComment> CommentList
  {
    get
    {
      if (this.commentModelHelper.Items == null)
        this.LoadComments();
      return this.commentModelHelper.Items;
    }
  }

  public void LoadComments()
  {
    this.commentModelHelper.LoadItems((Func<PredefinedCommentAdapter, ICollection<PredefinedComment>>) (adapter => adapter.All));
  }

  private CommentManager()
  {
    this.SetDefaultTranslationCulture();
    this.commentModelHelper = new ModelHelper<PredefinedComment, PredefinedCommentAdapter>(new EventHandler<ModelChangedEventArgs>(this.OnModelHelperChanged), new string[1]
    {
      "CommentTranslationList"
    });
  }

  private void OnModelHelperChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, e);
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void Store()
  {
    if (this.SelectedItem == null)
      return;
    this.commentModelHelper.Store();
  }

  public void Delete()
  {
    using (DBScope dbScope = new DBScope())
    {
      using (DbCommand dbCommand = dbScope.CreateDbCommand())
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("SELECT COUNT(*) FROM PredefinedCommentAssociation");
        stringBuilder.AppendFormat(" WHERE PredefinedCommentId = @PredefinedCommentId");
        dbCommand.CommandText = stringBuilder.ToString();
        dbScope.AddDbParameter(dbCommand, "PredefinedCommentId", (object) this.SelectedItem.PredefinedCommentId);
        object obj = dbCommand.ExecuteScalar();
        num = 0;
        if (!(obj is int num))
          ;
        dbScope.Complete();
        if (num > 0)
          throw ExceptionFactory.Instance.CreateException<ModelException>($"Comment can't be deleted since it has been assigned to {num} patient(s) or test(s).");
      }
    }
    this.commentModelHelper.Delete();
  }

  public void CancelNewItem() => this.commentModelHelper.CancelAddItem();

  public void PrepareAddItem() => this.commentModelHelper.PrepareAddItem(new PredefinedComment());

  public void RefreshData()
  {
    this.LoadComments();
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<ICollection<PredefinedComment>>(this.commentModelHelper.SelectedItems, ChangeType.SelectionChanged));
  }

  public void RevertModifications()
  {
    if (this.SelectedItem != null && this.SelectedItem.PredefinedCommentId == Guid.Empty)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<PredefinedComment>(this.SelectedItem, ChangeType.SelectionChanged));
    }
    else
      this.commentModelHelper.RefreshSelectedItems();
  }

  public void ChangeSingleSelection(PredefinedComment selection) => this.SelectedItem = selection;

  public bool IsOneItemSelected<T>() where T : PredefinedComment => this.SelectedItem != null;

  bool ISingleSelectionModel<PredefinedComment>.IsOneItemAvailable<T>()
  {
    return this.CommentList != null && this.CommentList.Count > 0;
  }

  public CultureInfo TranslationCulture { get; private set; }

  private void SetDefaultTranslationCulture()
  {
    if (this.TranslationCulture != null)
      return;
    this.TranslationCulture = SystemConfigurationManager.Instance.CurrentLanguage.Equals((object) SystemConfigurationManager.Instance.BaseLanguage) ? SystemConfigurationManager.Instance.SupportedLanguages.Where<CultureInfo>((Func<CultureInfo, bool>) (ci => !ci.Equals((object) SystemConfigurationManager.Instance.BaseLanguage))).FirstOrDefault<CultureInfo>() : SystemConfigurationManager.Instance.CurrentLanguage;
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<PredefinedComment>(this.SelectedItem, ChangeType.ItemEdited));
  }

  public void ChangeLanguage(CultureInfo cultureInfo)
  {
    if (cultureInfo == null)
      cultureInfo = SystemConfigurationManager.Instance.BaseLanguage;
    if (this.TranslationCulture == null || cultureInfo.Equals((object) SystemConfigurationManager.Instance.BaseLanguage))
    {
      this.SetDefaultTranslationCulture();
    }
    else
    {
      this.TranslationCulture = cultureInfo;
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<PredefinedComment>(this.SelectedItem, ChangeType.ItemEdited));
    }
  }
}
