// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.TestServiceRegistry
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

public class TestServiceRegistry : IModel
{
  private Dictionary<Guid, ITestService> registeredServices;

  public event EventHandler<ModelChangedEventArgs> Changed;

  public TestServiceRegistry() => this.registeredServices = new Dictionary<Guid, ITestService>();

  public void RegisterService(ITestService serviceToRegister)
  {
    if (serviceToRegister == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (serviceToRegister));
    if (this.registeredServices.Keys.Contains<Guid>(serviceToRegister.Signature))
      return;
    this.registeredServices.Add(serviceToRegister.Signature, serviceToRegister);
    if (this.Changed == null)
      return;
    this.Changed((object) this, ModelChangedEventArgs.Create<Dictionary<Guid, ITestService>>(this.registeredServices, ChangeType.ItemAdded));
  }

  public IEnumerable<ITestService> Get()
  {
    List<ITestService> testServiceList = new List<ITestService>();
    foreach (ITestService testService in this.registeredServices.Values)
      testServiceList.Add(testService);
    return (IEnumerable<ITestService>) testServiceList;
  }

  public ITestService Get(Guid testServiceSignature)
  {
    ITestService testService = (ITestService) null;
    if (this.registeredServices.ContainsKey(testServiceSignature))
      testService = this.registeredServices[testServiceSignature];
    return testService;
  }

  public IEnumerable<ITestServicePreset> GetDefaultPresetList()
  {
    List<ITestServicePreset> defaultPresetList = new List<ITestServicePreset>();
    Guid empty = Guid.Empty;
    foreach (ITestService testService in this.registeredServices.Values)
    {
      if (testService.PresetRegistry != null)
      {
        ITestServicePreset testServicePreset = testService.PresetRegistry.Get(testService.DefaultPresetId);
        if (testServicePreset != null)
          defaultPresetList.Add(testServicePreset);
      }
    }
    return (IEnumerable<ITestServicePreset>) defaultPresetList;
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
