// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.TestDetailPluginBase`2
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

using PathMedical.AudiologyTest.DataAccessLayer;
using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.DataExchange;
using PathMedical.DataExchange.Map;
using PathMedical.DataExchange.Set;
using PathMedical.DataExchange.Tokens;
using PathMedical.Logging;
using PathMedical.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace PathMedical.AudiologyTest;

public abstract class TestDetailPluginBase<TEntity, TAdapter> : 
  ITestPlugin,
  IPlugin,
  ISupportPluginDataExchange
  where TEntity : class, ITestInformation, new()
  where TAdapter : AdapterBase<TEntity>
{
  private static readonly ILogger Logger = LogFactory.Instance.Create($"TestDetailPluginBase<{typeof (TEntity)}>", "$Rev: 1599 $");

  public string Name { get; protected set; }

  public string Description { get; protected set; }

  public Guid Fingerprint { get; protected set; }

  public int LoadPriority { get; protected set; }

  public List<DataExchangeSetMap> RecordSetMaps { get; protected set; }

  public List<RecordDescriptionSet> RecordDescriptionSets { get; protected set; }

  public List<Type> Types
  {
    get => new List<Type>() { typeof (TEntity) };
  }

  public void Import(
    IEnumerable<DataExchangeTokenSet> dataExchangeTokenSets)
  {
    if (dataExchangeTokenSets == null)
      return;
    using (DBScope scope = new DBScope())
    {
      try
      {
        List<TEntity> source = DataExchangeManager.Instance.FetchEntities<TEntity>(dataExchangeTokenSets);
        if (source != null && source.Count<TEntity>() > 0)
        {
          TAdapter instance = Activator.CreateInstance(typeof (TAdapter), (object) scope) as TAdapter;
          this.ConfigureAdapterRelations(instance);
          TestInformationAdapter informationAdapter = new TestInformationAdapter(scope);
          if ((object) instance != null)
          {
            foreach (TEntity entity in source)
            {
              if (!TestDetailPluginBase<TEntity, TAdapter>.IsExistingEntity(entity, instance))
              {
                AudiologyTestInformation audiologyTestInfomation = this.CreateAudiologyTestInfomation(entity);
                if (!audiologyTestInfomation.PatientId.Equals((object) Guid.Empty))
                {
                  informationAdapter.Store(audiologyTestInfomation);
                  instance.Store(entity);
                }
              }
            }
          }
        }
        scope.Complete();
      }
      catch (System.Exception ex)
      {
        TestDetailPluginBase<TEntity, TAdapter>.Logger.Error(ex, "An error occurred while import test data of type {0}.", (object) typeof (TEntity));
        throw;
      }
    }
  }

  public TEntity Get(Guid testDetailId)
  {
    if (testDetailId.Equals(Guid.Empty))
      return default (TEntity);
    using (DBScope scope = new DBScope())
    {
      TEntity entity = new AdapterBase<TEntity>(scope).All.Where<TEntity>((Func<TEntity, bool>) (t => t.AudiologyTestId == testDetailId)).FirstOrDefault<TEntity>();
      scope.Complete();
      return entity;
    }
  }

  public object Export(ExportType exportType, Guid testDetailId)
  {
    if (testDetailId.Equals(Guid.Empty))
      return (object) null;
    if (exportType != ExportType.BinaryImage)
      throw new NotImplementedException();
    TEntity entity = this.Get(testDetailId);
    if ((object) entity == null || !((object) entity is ISerializable))
      return (object) null;
    ISerializable serializable = (object) entity as ISerializable;
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    MemoryStream memoryStream = new MemoryStream();
    MemoryStream serializationStream = memoryStream;
    ISerializable graph = serializable;
    binaryFormatter.Serialize((Stream) serializationStream, (object) graph);
    return (object) memoryStream.ToArray();
  }

  private static bool IsExistingEntity(TEntity testDetail, TAdapter adapter)
  {
    return (object) adapter.All.FirstOrDefault<TEntity>((Func<TEntity, bool>) (ep => ep.Id == testDetail.Id)) != null;
  }

  public int StorePriority => 100;

  public DataExchangeTokenSet GetDataExchangeTokenSet(object id) => (DataExchangeTokenSet) null;

  public List<DataExchangeTokenSet> GetDataExchangeTokenSets(
    object id,
    params object[] additionalIds)
  {
    return (List<DataExchangeTokenSet>) null;
  }

  public Guid TestTypeSignature { get; protected set; }

  public Guid InstrumentSignature { get; protected set; }

  public Type DetailViewerComponentModuleType { get; protected set; }

  public Type DetailReportType { get; protected set; }

  public void Delete(Guid testDetailId)
  {
    if (testDetailId == Guid.Empty)
      return;
    using (DBScope scope = new DBScope())
    {
      AdapterBase<TEntity> adapterBase = new AdapterBase<TEntity>(scope);
      TEntity entity = this.Get(testDetailId);
      if ((object) entity != null)
        adapterBase.Delete(entity);
      scope.Complete();
    }
  }

  public Type ConfigurationModuleType { get; protected set; }

  public abstract void WriteConfiguration(Stream stream);

  public void AssignTestToNewPatient(Guid newPatient, Guid audiologyTestInformation)
  {
    if (audiologyTestInformation == Guid.Empty || newPatient == Guid.Empty)
      return;
    using (DBScope scope = new DBScope())
    {
      AdapterBase<TEntity> adapterBase = new AdapterBase<TEntity>(scope);
      TEntity entity = this.Get(audiologyTestInformation);
      if ((object) entity != null)
      {
        entity.PatientId = new Guid?(newPatient);
        adapterBase.Store(entity);
      }
      scope.Complete();
    }
  }

  public object GetTestInformation(Guid testId) => (object) this.Get(testId);

  public ITestDetailView CreateView(object entity)
  {
    ITestDetailView view = (ITestDetailView) null;
    if (!(entity is TEntity) || !(this.DetailViewerComponentModuleType != (Type) null))
      return view;
    List<Type> typeList = new List<Type>();
    typeList.Add(typeof (TEntity));
    object[] source = new object[1]{ entity };
    ConstructorInfo constructor = this.DetailViewerComponentModuleType.GetType().GetConstructor(typeList.ToArray());
    object obj = (object) null;
    if (constructor != (ConstructorInfo) null)
      obj = constructor.Invoke(((IEnumerable<object>) source).ToArray<object>());
    view = obj as ITestDetailView;
    return view;
  }

  protected abstract void ConfigureAdapterRelations(TAdapter adapter);

  protected abstract AudiologyTestInformation CreateAudiologyTestInfomation(TEntity testInformation);
}
