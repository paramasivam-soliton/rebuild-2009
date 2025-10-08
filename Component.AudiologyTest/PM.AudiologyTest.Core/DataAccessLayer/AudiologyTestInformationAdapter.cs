// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.DataAccessLayer.TestInformationAdapter
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using System;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.AudiologyTest.DataAccessLayer;

public class TestInformationAdapter(DBScope scope) : AdapterBase<AudiologyTestInformation>(scope)
{
  public override void Store(AudiologyTestInformation audiologyTestInformation)
  {
    if (audiologyTestInformation == null)
      return;
    base.Store(audiologyTestInformation);
    AudiologyTestManager.Instance.UpdateOverallTestInformation(this.Scope, this.FetchEntities((Expression<Func<AudiologyTestInformation, bool>>) (t => t.PatientId == audiologyTestInformation.PatientId && (int) t.TestType == (int) audiologyTestInformation.TestType)).ToList<AudiologyTestInformation>());
  }
}
