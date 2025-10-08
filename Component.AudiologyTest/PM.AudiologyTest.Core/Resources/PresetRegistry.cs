// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.PresetRegistry
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.AudiologyTest;

public class PresetRegistry : IModel
{
  private readonly Dictionary<Guid, ITestServicePreset> registeredPresets;

  public event EventHandler<ModelChangedEventArgs> Changed;

  public PresetRegistry() => this.registeredPresets = new Dictionary<Guid, ITestServicePreset>();

  public void Register(ITestServicePreset presetToRegister)
  {
    if (presetToRegister == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (presetToRegister));
    if (this.registeredPresets.Keys.Contains<Guid>(presetToRegister.Id))
      return;
    lock (this.registeredPresets)
      this.registeredPresets.Add(presetToRegister.Id, presetToRegister);
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<Dictionary<Guid, ITestServicePreset>>(this.registeredPresets, ChangeType.ItemAdded));
  }

  public ITestServicePreset Get(Guid presetId)
  {
    ITestServicePreset testServicePreset = (ITestServicePreset) null;
    if (this.registeredPresets.ContainsKey(presetId))
      this.registeredPresets.TryGetValue(presetId, out testServicePreset);
    return testServicePreset;
  }

  public IEnumerable<ITestServicePreset> Get()
  {
    List<ITestServicePreset> testServicePresetList = new List<ITestServicePreset>();
    foreach (ITestServicePreset testServicePreset in this.registeredPresets.Values)
      testServicePresetList.Add(testServicePreset);
    return (IEnumerable<ITestServicePreset>) testServicePresetList;
  }

  public void Store() => throw new NotImplementedException();

  public void Delete() => throw new NotImplementedException();

  public void ChangeSelection(Type selectionType, object selectedItem)
  {
    throw new NotImplementedException();
  }

  public void CancelNewItem() => throw new NotImplementedException();

  public void PrepareAddItem() => throw new NotImplementedException();

  public void RefreshData()
  {
  }

  public void RevertModifications() => throw new NotImplementedException();
}
