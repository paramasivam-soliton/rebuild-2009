// Decompiled with JetBrains decompiler
// Type: PathMedical.AudiologyTest.TestResultImageBuilder
// Assembly: PM.AudiologyTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4AC78FDC-35AE-4317-B326-17A5F32D3D2E
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.AudiologyTest.dll

using PathMedical.AudiologyTest.Properties;
using System.Collections.Generic;
using System.Drawing;

#nullable disable
namespace PathMedical.AudiologyTest;

public class TestResultImageBuilder
{
  private static readonly TestResultImageBuilder instance = new TestResultImageBuilder();
  private Dictionary<string, Bitmap> imageCache;

  public static TestResultImageBuilder Instance => TestResultImageBuilder.instance;

  private TestResultImageBuilder() => this.imageCache = new Dictionary<string, Bitmap>();

  public Bitmap GetTestResultImage(
    AudiologyTestResult? resultLeftEar,
    AudiologyTestResult? resultRightEar)
  {
    string key = $"{(resultLeftEar.HasValue ? (int) resultLeftEar.Value : 9999):D4}{(resultRightEar.HasValue ? (int) resultRightEar.Value : 9999):D4}";
    Bitmap testResultImage;
    if (!this.imageCache.TryGetValue(key, out testResultImage))
    {
      testResultImage = this.MergeImages(this.GetTestResultImage(resultLeftEar), this.GetTestResultImage(resultRightEar), 5);
      this.imageCache.Add(key, testResultImage);
    }
    return testResultImage;
  }

  private Bitmap GetTestResultImage(AudiologyTestResult? result)
  {
    Bitmap testResultImage = (Bitmap) null;
    if (result.HasValue)
    {
      switch (result.GetValueOrDefault())
      {
        case AudiologyTestResult.Pass:
          testResultImage = Resources.GN_TestPass;
          break;
        case AudiologyTestResult.Refer:
          testResultImage = Resources.GN_TestRefer;
          break;
        case AudiologyTestResult.Incomplete:
          testResultImage = Resources.GN_TestIncomplete;
          break;
        case AudiologyTestResult.Diagnostic:
          testResultImage = Resources.TestDiagnostic;
          break;
      }
    }
    return testResultImage;
  }

  private Bitmap MergeImages(Bitmap image1, Bitmap image2, int spacing)
  {
    Bitmap bitmap1 = image1 != null ? image1 : new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    Bitmap bitmap2 = image2 != null ? image2 : new Bitmap(16 /*0x10*/, 16 /*0x10*/);
    int num = 5;
    if (spacing >= 0)
      num = spacing;
    Bitmap bitmap3 = new Bitmap(bitmap1.Width + num + bitmap2.Width, bitmap1.Height + bitmap2.Height);
    int x1 = 0;
    if (image1 != null)
    {
      for (int x2 = 0; x2 < bitmap1.Width; ++x2)
      {
        for (int y = 0; y < bitmap1.Height; ++y)
          bitmap3.SetPixel(x1, y, bitmap1.GetPixel(x2, y));
        ++x1;
      }
    }
    else
      x1 = bitmap1.Width;
    int x3 = x1 + num;
    if (image2 != null)
    {
      for (int x4 = 0; x4 < bitmap2.Width; ++x4)
      {
        for (int y = 0; y < bitmap2.Height; ++y)
          bitmap3.SetPixel(x3, y, bitmap2.GetPixel(x4, y));
        ++x3;
      }
    }
    return bitmap3;
  }
}
