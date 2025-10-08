// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.AudiologyTestManager
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

using PathMedical.AudiologyTest.DataAccessLayer;
using PathMedical.DatabaseManagement;
using PathMedical.Exception;
using PathMedical.Plugin;
using PathMedical.SystemConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.AudiologyTest;

public class AudiologyTestManager
{
  private OverallTestInformationAdapter overallInformationAdapter;

  public static AudiologyTestManager Instance => PathMedical.Singleton.Singleton<AudiologyTestManager>.Instance;

  private AudiologyTestManager()
  {
  }

  public void UpdateOverallTestInformation(
    DBScope dbScope,
    List<AudiologyTestInformation> audiologyTests)
  {
    this.overallInformationAdapter = dbScope != null ? new OverallTestInformationAdapter(dbScope) : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (dbScope));
    foreach (IGrouping<TestType, AudiologyTestInformation> source1 in audiologyTests.GroupBy<AudiologyTestInformation, TestType>((Func<AudiologyTestInformation, TestType>) (t => t.TestType)))
    {
      foreach (IGrouping<TestObject, AudiologyTestInformation> source2 in source1.GroupBy<AudiologyTestInformation, TestObject>((Func<AudiologyTestInformation, TestObject>) (t => t.TestObject)))
      {
        AudiologyTestInformation bestTestResult = AudiologyTestManager.GetBestTestResult(source2.Key, source1.Key, (ICollection<AudiologyTestInformation>) source2.ToArray<AudiologyTestInformation>());
        if (bestTestResult != null)
          this.StoreOverallTestInformation(bestTestResult);
      }
    }
  }

  private static AudiologyTestInformation GetBestTestResult(
    TestObject testObject,
    TestType testType,
    ICollection<AudiologyTestInformation> testInformations)
  {
    return testObject != TestObject.RightEar ? ((testInformations.OrderBy<AudiologyTestInformation, DateTime?>((Func<AudiologyTestInformation, DateTime?>) (t => t.TestDate)).FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
    {
      AudiologyTestResult? leftEarTestResult = t.LeftEarTestResult;
      AudiologyTestResult audiologyTestResult = AudiologyTestResult.Pass;
      return leftEarTestResult.GetValueOrDefault() == audiologyTestResult & leftEarTestResult.HasValue && t.TestType == testType;
    })) ?? testInformations.OrderBy<AudiologyTestInformation, DateTime?>((Func<AudiologyTestInformation, DateTime?>) (t => t.TestDate)).FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
    {
      AudiologyTestResult? leftEarTestResult = t.LeftEarTestResult;
      AudiologyTestResult audiologyTestResult = AudiologyTestResult.Refer;
      return leftEarTestResult.GetValueOrDefault() == audiologyTestResult & leftEarTestResult.HasValue && t.TestType == testType;
    }))) ?? testInformations.OrderBy<AudiologyTestInformation, DateTime?>((Func<AudiologyTestInformation, DateTime?>) (t => t.TestDate)).FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
    {
      AudiologyTestResult? leftEarTestResult = t.LeftEarTestResult;
      AudiologyTestResult audiologyTestResult = AudiologyTestResult.Incomplete;
      return leftEarTestResult.GetValueOrDefault() == audiologyTestResult & leftEarTestResult.HasValue && t.TestType == testType;
    }))) ?? testInformations.OrderBy<AudiologyTestInformation, DateTime?>((Func<AudiologyTestInformation, DateTime?>) (t => t.TestDate)).FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
    {
      AudiologyTestResult? leftEarTestResult = t.LeftEarTestResult;
      AudiologyTestResult audiologyTestResult = AudiologyTestResult.Diagnostic;
      return leftEarTestResult.GetValueOrDefault() == audiologyTestResult & leftEarTestResult.HasValue && t.TestType == testType;
    })) : ((testInformations.OrderBy<AudiologyTestInformation, DateTime?>((Func<AudiologyTestInformation, DateTime?>) (t => t.TestDate)).FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
    {
      AudiologyTestResult? rightEarTestResult = t.RightEarTestResult;
      AudiologyTestResult audiologyTestResult = AudiologyTestResult.Pass;
      return rightEarTestResult.GetValueOrDefault() == audiologyTestResult & rightEarTestResult.HasValue && t.TestType == testType;
    })) ?? testInformations.OrderBy<AudiologyTestInformation, DateTime?>((Func<AudiologyTestInformation, DateTime?>) (t => t.TestDate)).FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
    {
      AudiologyTestResult? rightEarTestResult = t.RightEarTestResult;
      AudiologyTestResult audiologyTestResult = AudiologyTestResult.Refer;
      return rightEarTestResult.GetValueOrDefault() == audiologyTestResult & rightEarTestResult.HasValue && t.TestType == testType;
    }))) ?? testInformations.OrderBy<AudiologyTestInformation, DateTime?>((Func<AudiologyTestInformation, DateTime?>) (t => t.TestDate)).FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
    {
      AudiologyTestResult? rightEarTestResult = t.RightEarTestResult;
      AudiologyTestResult audiologyTestResult = AudiologyTestResult.Incomplete;
      return rightEarTestResult.GetValueOrDefault() == audiologyTestResult & rightEarTestResult.HasValue && t.TestType == testType;
    }))) ?? testInformations.OrderBy<AudiologyTestInformation, DateTime?>((Func<AudiologyTestInformation, DateTime?>) (t => t.TestDate)).FirstOrDefault<AudiologyTestInformation>((Func<AudiologyTestInformation, bool>) (t =>
    {
      AudiologyTestResult? rightEarTestResult = t.RightEarTestResult;
      AudiologyTestResult audiologyTestResult = AudiologyTestResult.Diagnostic;
      return rightEarTestResult.GetValueOrDefault() == audiologyTestResult & rightEarTestResult.HasValue && t.TestType == testType;
    }));
  }

  private void StoreOverallTestInformation(AudiologyTestInformation audiologyTestInformation)
  {
    OverallTestInformation entity = this.overallInformationAdapter.FetchEntities((Expression<Func<OverallTestInformation, bool>>) (o => (Guid?) o.PatientId == audiologyTestInformation.PatientId && (int) o.TestType == (int) audiologyTestInformation.TestType && (int) o.TestObject == (int) audiologyTestInformation.TestObject)).FirstOrDefault<OverallTestInformation>();
    if (entity == null)
    {
      entity = new OverallTestInformation();
      Guid? patientId = audiologyTestInformation.PatientId;
      if (patientId.HasValue)
      {
        OverallTestInformation overallTestInformation = entity;
        patientId = audiologyTestInformation.PatientId;
        Guid guid = patientId.Value;
        overallTestInformation.PatientId = guid;
      }
      entity.TestObject = audiologyTestInformation.TestObject;
      entity.TestType = audiologyTestInformation.TestType;
    }
    if (audiologyTestInformation != null)
    {
      entity.ReferenceToTestId = new Guid?(audiologyTestInformation.TestDetailId);
      AudiologyTestResult? nullable = audiologyTestInformation.TestObject == TestObject.LeftEar ? audiologyTestInformation.LeftEarTestResult : audiologyTestInformation.RightEarTestResult;
      if (nullable.HasValue && (ValueType) nullable is AudiologyTestResult)
        entity.OverallTestResult = nullable.Value;
    }
    this.overallInformationAdapter.Store(entity);
  }

  public void DeleteTests(
    List<AudiologyTestInformation> audiologyTestInformations)
  {
    if (audiologyTestInformations == null || audiologyTestInformations.Count == 0)
      return;
    using (DBScope scope = new DBScope())
    {
      List<ITestPlugin> list = SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().ToList<ITestPlugin>();
      List<Guid> guidList = new List<Guid>();
      foreach (AudiologyTestInformation audiologyTestInformation in audiologyTestInformations)
      {
        AudiologyTestInformation information = audiologyTestInformation;
        if (information != null)
        {
          list.FirstOrDefault<ITestPlugin>((Func<ITestPlugin, bool>) (p =>
          {
            Guid testTypeSignature1 = p.TestTypeSignature;
            Guid? testTypeSignature2 = information.TestTypeSignature;
            return testTypeSignature2.HasValue && testTypeSignature1 == testTypeSignature2.GetValueOrDefault();
          }))?.Delete(audiologyTestInformation.TestDetailId);
          Guid? patientId = information.PatientId;
          if (patientId.HasValue)
          {
            patientId = information.PatientId;
            Guid guid = patientId.Value;
            if (!guidList.Contains(guid))
              guidList.Add(guid);
          }
        }
      }
      TestInformationAdapter informationAdapter1 = new TestInformationAdapter(scope);
      List<AudiologyTestInformation> audiologyTestInformations1 = new List<AudiologyTestInformation>();
      foreach (Guid guid in guidList)
      {
        Guid id = guid;
        TestInformationAdapter informationAdapter2 = informationAdapter1;
        Expression<Func<AudiologyTestInformation, bool>> expression = (Expression<Func<AudiologyTestInformation, bool>>) (ti => ti.PatientId == (Guid?) id);
        foreach (AudiologyTestInformation fetchEntity in (IEnumerable<AudiologyTestInformation>) informationAdapter2.FetchEntities(expression))
        {
          if (audiologyTestInformations.Contains(fetchEntity))
            informationAdapter1.Delete(fetchEntity);
          else
            audiologyTestInformations1.Add(fetchEntity);
        }
        this.UpdateOverallTestResult(id, audiologyTestInformations1, scope);
      }
      scope.Complete();
    }
  }

  public void AssignTestToNewPatient(
    Guid newPatient,
    AudiologyTestInformation audiologyTestInformation)
  {
    using (DBScope dbScope = new DBScope())
    {
      List<ITestPlugin> list1 = SystemConfigurationManager.Instance.Plugins.OfType<ITestPlugin>().ToList<ITestPlugin>();
      Guid? patientId = audiologyTestInformation.PatientId;
      Func<ITestPlugin, bool> predicate = (Func<ITestPlugin, bool>) (p =>
      {
        Guid testTypeSignature1 = p.TestTypeSignature;
        Guid? testTypeSignature2 = audiologyTestInformation.TestTypeSignature;
        return testTypeSignature2.HasValue && testTypeSignature1 == testTypeSignature2.GetValueOrDefault();
      });
      list1.FirstOrDefault<ITestPlugin>(predicate)?.AssignTestToNewPatient(newPatient, audiologyTestInformation.TestDetailId);
      TestInformationAdapter informationAdapter = new TestInformationAdapter(dbScope);
      audiologyTestInformation.PatientId = new Guid?(newPatient);
      informationAdapter.Store(audiologyTestInformation);
      if (patientId.HasValue)
      {
        Guid id = patientId.Value;
        List<AudiologyTestInformation> list2 = informationAdapter.FetchEntities((Expression<Func<AudiologyTestInformation, bool>>) (ti => ti.PatientId == (Guid?) id)).ToList<AudiologyTestInformation>();
        this.UpdateOverallTestResult(id, list2, dbScope);
        AudiologyTestManager.Instance.UpdateOverallTestInformation(dbScope, list2);
      }
      dbScope.Complete();
    }
  }

  private void UpdateOverallTestResult(
    Guid patient,
    List<AudiologyTestInformation> audiologyTestInformations,
    DBScope scope)
  {
    OverallTestInformationAdapter informationAdapter1 = new OverallTestInformationAdapter(scope);
    Guid id = patient;
    OverallTestInformationAdapter informationAdapter2 = informationAdapter1;
    Expression<Func<OverallTestInformation, bool>> expression = (Expression<Func<OverallTestInformation, bool>>) (oti => oti.PatientId == id);
    foreach (OverallTestInformation fetchEntity in (IEnumerable<OverallTestInformation>) informationAdapter2.FetchEntities(expression))
      informationAdapter1.Delete(fetchEntity);
    AudiologyTestManager.Instance.UpdateOverallTestInformation(scope, audiologyTestInformations);
  }
}
