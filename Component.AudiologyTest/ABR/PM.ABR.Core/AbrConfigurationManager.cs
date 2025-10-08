// Decompiled with JetBrains decompiler
// Type: PathMedical.ABR.AbrConfigurationManager
// Assembly: PM.ABR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 09E7728F-8618-4147-9D4A-E38CA516B245
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ABR.dll

using PathMedical.ABR.Properties;
using PathMedical.DataExchange;
using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Tokens;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using PathMedical.Type1077.DataExchange;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PathMedical.ABR;

public class AbrConfigurationManager : ISingleEditingModel, IModel, ISingleSelectionModel<AbrPreset>
{
  private readonly ModelHelper<AbrPreset, AbrPresetAdapter> abrPresetModelHelper;

  public static AbrConfigurationManager Instance => PathMedical.Singleton.Singleton<AbrConfigurationManager>.Instance;

  public Guid MaintainAbrConfigurationAccessPermissionId => Guid.Empty;

  public AbrPreset SelectedItem
  {
    get => this.abrPresetModelHelper.SelectedItem;
    set => this.abrPresetModelHelper.SelectedItem = value;
  }

  public List<AbrPreset> AbrPresets
  {
    get
    {
      if (this.abrPresetModelHelper.Items == null)
        this.LoadAbrPresets();
      return this.abrPresetModelHelper.Items;
    }
  }

  private void LoadAbrPresets()
  {
    this.abrPresetModelHelper.LoadItems((Func<AbrPresetAdapter, ICollection<AbrPreset>>) (adapter => adapter.All));
  }

  private AbrConfigurationManager()
  {
    this.abrPresetModelHelper = new ModelHelper<AbrPreset, AbrPresetAdapter>(new EventHandler<ModelChangedEventArgs>(this.ModelChanged), Array.Empty<string>());
  }

  private void ModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed(sender, e);
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void Store()
  {
    if (this.SelectedItem == null)
      return;
    AbrPreset selectedPreset = this.abrPresetModelHelper.SelectedItems.OfType<AbrPreset>().SingleOrDefault<AbrPreset>();
    if (selectedPreset != null && this.AbrPresets.Any<AbrPreset>((Func<AbrPreset, bool>) (i => i.Id != selectedPreset.Id && i.Name.Equals(selectedPreset.Name, StringComparison.InvariantCultureIgnoreCase))))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("PresetWithSameNameExists"));
    this.abrPresetModelHelper.Store();
  }

  public void Delete() => this.abrPresetModelHelper.Delete();

  public void CancelNewItem() => this.abrPresetModelHelper.CancelAddItem();

  public void PrepareAddItem() => this.abrPresetModelHelper.PrepareAddItem(new AbrPreset());

  public void RefreshData()
  {
    this.LoadAbrPresets();
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<ICollection<AbrPreset>>(this.abrPresetModelHelper.SelectedItems, ChangeType.SelectionChanged));
  }

  public void RevertModifications()
  {
    if (this.SelectedItem != null && this.SelectedItem.Id == Guid.Empty)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<AbrPreset>(this.SelectedItem, ChangeType.SelectionChanged));
    }
    else
      this.abrPresetModelHelper.RefreshSelectedItems();
  }

  public void ChangeSingleSelection(AbrPreset selection) => this.SelectedItem = selection;

  bool ISingleSelectionModel<AbrPreset>.IsOneItemSelected<T>() => this.SelectedItem != null;

  bool ISingleSelectionModel<AbrPreset>.IsOneItemAvailable<T>()
  {
    return this.abrPresetModelHelper != null && this.abrPresetModelHelper.Items != null && this.abrPresetModelHelper.Items.Count > 0;
  }

  public void WritePresets(Stream stream)
  {
    IEnumerable<object> array = (IEnumerable<object>) this.AbrPresets.ToArray();
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "AbrPreset", "32975");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, array);
    RecordDescription recordDescription = recordMap.ToRecordDescription;
    Type1077StreamWriter type1077StreamWriter = new Type1077StreamWriter(stream, new RecordDescription[1]
    {
      recordDescription
    });
    try
    {
      type1077StreamWriter.Open();
      type1077StreamWriter.Write(recordDescription, exchangeTokenSetList.ToArray());
    }
    finally
    {
      type1077StreamWriter.Close();
    }
  }
}
