// Decompiled with JetBrains decompiler
// Type: PathMedical.InstrumentManagement.DpoaeConfigurationManager
// Assembly: PM.DPOAE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 38B92F02-B758-4EF7-9103-415B55783CFC
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DPOAE.dll

using PathMedical.DataExchange;
using PathMedical.DataExchange.Description;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Tokens;
using PathMedical.DPOAE;
using PathMedical.Exception;
using PathMedical.InstrumentManagement.Properties;
using PathMedical.ResourceManager;
using PathMedical.Type1077.DataExchange;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace PathMedical.InstrumentManagement;

public class DpoaeConfigurationManager : 
  ISingleEditingModel,
  IModel,
  ISingleSelectionModel<DpoaePreset>
{
  private readonly ModelHelper<DpoaePreset, DpoaePresetAdapter> dpoaeConfigurationModel;

  public static DpoaeConfigurationManager Instance => PathMedical.Singleton.Singleton<DpoaeConfigurationManager>.Instance;

  private DpoaeConfigurationManager()
  {
    this.dpoaeConfigurationModel = new ModelHelper<DpoaePreset, DpoaePresetAdapter>(new EventHandler<ModelChangedEventArgs>(this.OnModelHelperChanged), Array.Empty<string>());
  }

  public Guid MaintainDpoaeConfigurationAccessPermissionId => Guid.Empty;

  private void OnModelHelperChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed(sender, e);
  }

  public DpoaePreset SelectedItem
  {
    get => this.dpoaeConfigurationModel.SelectedItem;
    set => this.dpoaeConfigurationModel.SelectedItem = value;
  }

  public List<DpoaePreset> DpoaePresets
  {
    get
    {
      if (this.dpoaeConfigurationModel.Items == null)
        this.LoadDpoaePresets();
      return this.dpoaeConfigurationModel.Items;
    }
  }

  private void LoadDpoaePresets()
  {
    this.dpoaeConfigurationModel.LoadItems((Func<DpoaePresetAdapter, ICollection<DpoaePreset>>) (adapter => adapter.All));
  }

  public event EventHandler<ModelChangedEventArgs> Changed;

  public void Store()
  {
    if (this.SelectedItem == null)
      return;
    DpoaePreset selectedPreset = this.dpoaeConfigurationModel.SelectedItems.OfType<DpoaePreset>().SingleOrDefault<DpoaePreset>();
    if (selectedPreset != null && this.DpoaePresets.Any<DpoaePreset>((Func<DpoaePreset, bool>) (i => i.Id != selectedPreset.Id && i.Name.Equals(selectedPreset.Name, StringComparison.InvariantCultureIgnoreCase))))
      throw ExceptionFactory.Instance.CreateException<ModelException>(ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetString("PresetWithSameNameExists"));
    this.dpoaeConfigurationModel.Store();
  }

  public void Delete() => this.dpoaeConfigurationModel.Delete();

  public void CancelNewItem() => this.dpoaeConfigurationModel.CancelAddItem();

  public void PrepareAddItem() => this.dpoaeConfigurationModel.PrepareAddItem(new DpoaePreset());

  public void RefreshData()
  {
    this.LoadDpoaePresets();
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<ICollection<DpoaePreset>>(this.dpoaeConfigurationModel.SelectedItems, ChangeType.SelectionChanged));
  }

  public void RevertModifications()
  {
    if (this.SelectedItem != null && this.SelectedItem.Id == Guid.Empty)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, ModelChangedEventArgs.Create<DpoaePreset>(this.SelectedItem, ChangeType.SelectionChanged));
    }
    else
      this.dpoaeConfigurationModel.RefreshSelectedItems();
  }

  public void ChangeSingleSelection(DpoaePreset selection) => this.SelectedItem = selection;

  bool ISingleSelectionModel<DpoaePreset>.IsOneItemSelected<T>() => this.SelectedItem != null;

  bool ISingleSelectionModel<DpoaePreset>.IsOneItemAvailable<T>()
  {
    return this.dpoaeConfigurationModel != null && this.dpoaeConfigurationModel.Items != null && this.dpoaeConfigurationModel.Items.Count > 0;
  }

  public void WriteConfiguration(Stream stream)
  {
    if (this.DpoaePresets == null || this.DpoaePresets.Count == 0)
      return;
    List<object> dataRecords = new List<object>();
    foreach (DpoaePreset dpoaePreset in this.DpoaePresets.Where<DpoaePreset>((Func<DpoaePreset, bool>) (p => p != null)).ToArray<DpoaePreset>())
    {
      DpoaeInstrumentPreset instrumentPreset = (DpoaeInstrumentPreset) null;
      switch (dpoaePreset.PresetNumber)
      {
        case DpoaeProtocolType.Protocol1:
          instrumentPreset = DpoaeInstrumentPresets.Protocol1;
          break;
        case DpoaeProtocolType.Protocol2:
          instrumentPreset = DpoaeInstrumentPresets.Protocol2;
          break;
        case DpoaeProtocolType.Protocol3:
          instrumentPreset = DpoaeInstrumentPresets.Protocol3;
          break;
        case DpoaeProtocolType.Protocol4:
          instrumentPreset = DpoaeInstrumentPresets.Protocol4;
          break;
      }
      if (instrumentPreset != null)
      {
        instrumentPreset.Name = dpoaePreset.Name;
        instrumentPreset.Category = dpoaePreset.Category;
        dataRecords.Add((object) instrumentPreset);
      }
    }
    RecordMap recordMap = DataExchangeManager.Instance.GetRecordMap("PM", "Type1077", "DpoaePreset", "32991");
    List<DataExchangeTokenSet> exchangeTokenSetList = DataExchangeManager.Instance.MoveDataExchangeTokenSets(recordMap, (IEnumerable<object>) dataRecords);
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
