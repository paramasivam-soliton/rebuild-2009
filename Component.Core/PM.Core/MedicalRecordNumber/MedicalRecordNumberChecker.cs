// Decompiled with JetBrains decompiler
// Type: PathMedical.MedicalRecordNumberChecker
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Collections.Generic;

#nullable disable
namespace PathMedical;

public class MedicalRecordNumberChecker
{
  public static MedicalRecordNumberChecker Instance
  {
    get => PathMedical.Singleton.Singleton<MedicalRecordNumberChecker>.Instance;
  }

  private MedicalRecordNumberChecker()
  {
  }

  public bool CheckMrn(string mrn, MedicalRecordTypes type, bool highSeverityCheck)
  {
    bool flag = false;
    switch (type)
    {
      case MedicalRecordTypes.MRC:
        flag = MedicalRecordNumberChecker.ValidateMedicalRecordNumberMrc(mrn);
        break;
      case MedicalRecordTypes.CPR_DK:
        flag = this.CheckCprDk(mrn, highSeverityCheck);
        break;
      case MedicalRecordTypes.GERMANY:
        flag = this.CheckGermany(mrn);
        break;
      case MedicalRecordTypes.TURKEY:
        flag = this.CheckTurkey(mrn);
        break;
    }
    return flag;
  }

  private static bool ValidateMedicalRecordNumberMrc(string medicalRecordNumber)
  {
    if (string.IsNullOrEmpty(medicalRecordNumber))
      return false;
    int[] numArray = new int[9]
    {
      10,
      9,
      8,
      7,
      6,
      5,
      4,
      3,
      2
    };
    if (medicalRecordNumber.Contains("-"))
      medicalRecordNumber = medicalRecordNumber.Replace("-", string.Empty);
    if (medicalRecordNumber.Contains(" "))
      medicalRecordNumber = medicalRecordNumber.Replace(" ", string.Empty);
    if (medicalRecordNumber.Length != 10)
      return false;
    List<int> intList = new List<int>();
    foreach (char c in medicalRecordNumber)
    {
      if (!char.IsDigit(c))
        return false;
      intList.Add((int) short.Parse(c.ToString()));
    }
    int num1 = 0;
    for (int index = 0; index < 9; ++index)
    {
      int num2 = numArray[index];
      num1 += intList[index] * num2;
    }
    int num3 = 11 - num1 % 11;
    switch (num3)
    {
      case 10:
        return false;
      case 11:
        num3 = 0;
        break;
    }
    int num4 = intList[intList.Count - 1];
    if (num3 != num4)
      return false;
    for (int index = 0; index < intList.Count - 1; ++index)
    {
      if (intList[index] != intList[index + 1])
        return true;
    }
    return false;
  }

  private bool CheckCprDk(string medicalRecordNumber, bool highSeverityCheck)
  {
    if (string.IsNullOrEmpty(medicalRecordNumber))
      return false;
    int[] numArray = new int[9]{ 4, 3, 2, 7, 6, 5, 4, 3, 2 };
    if (medicalRecordNumber.Contains("-"))
      medicalRecordNumber = medicalRecordNumber.Replace("-", string.Empty);
    if (medicalRecordNumber.Trim().Length != 10)
      return false;
    List<int> intList = new List<int>();
    foreach (char c in medicalRecordNumber)
    {
      if (!char.IsDigit(c))
        return false;
      intList.Add((int) short.Parse(c.ToString()));
    }
    if (!highSeverityCheck)
      return true;
    int num1 = 0;
    for (int index = 0; index < 9; ++index)
    {
      int num2 = numArray[index];
      num1 += intList[index] * num2;
    }
    int num3 = 11 - num1 % 11;
    int num4 = intList[intList.Count - 1];
    if (num3 == 11)
      num3 = 0;
    if (num3 != num4)
      return false;
    int num5 = 0;
    for (int index = 0; index < 9; ++index)
    {
      int num6 = numArray[index];
      num5 += intList[index] * num6;
    }
    return (num5 + intList[9]) % 11 == 0;
  }

  private bool CheckGermany(string medicalRecordNumber)
  {
    ushort[] numArray = new ushort[11]
    {
      (ushort) 3,
      (ushort) 1,
      (ushort) 3,
      (ushort) 1,
      (ushort) 3,
      (ushort) 1,
      (ushort) 3,
      (ushort) 1,
      (ushort) 3,
      (ushort) 1,
      (ushort) 3
    };
    if (string.IsNullOrEmpty(medicalRecordNumber))
      return false;
    if (medicalRecordNumber.Contains("-"))
      medicalRecordNumber = medicalRecordNumber.Replace("-", "");
    if (medicalRecordNumber.Contains(" "))
      medicalRecordNumber = medicalRecordNumber.Replace(" ", "");
    if (medicalRecordNumber.Trim().Length != 12)
      return false;
    List<int> intList = new List<int>();
    foreach (char c in medicalRecordNumber)
    {
      if (!char.IsDigit(c))
        return false;
      intList.Add((int) short.Parse(c.ToString()));
    }
    int num1 = 0;
    for (int index = 0; index < 11; ++index)
    {
      int num2 = (int) numArray[index];
      num1 += intList[index] * num2;
    }
    int num3 = 10 - num1 % 10;
    return intList[11] == num3;
  }

  private bool CheckTurkey(string mrn)
  {
    bool flag = false;
    if (string.IsNullOrEmpty(mrn))
      return false;
    if (mrn.Length == 11)
    {
      long num1 = long.Parse(mrn);
      long num2 = num1 / 100L;
      long num3 = num1 / 100L;
      long num4 = num2 % 10L;
      long num5 = num2 / 10L;
      long num6 = num5 % 10L;
      long num7 = num5 / 10L;
      long num8 = num7 % 10L;
      long num9 = num7 / 10L;
      long num10 = num9 % 10L;
      long num11 = num9 / 10L;
      long num12 = num11 % 10L;
      long num13 = num11 / 10L;
      long num14 = num13 % 10L;
      long num15 = num13 / 10L;
      long num16 = num15 % 10L;
      long num17 = num15 / 10L;
      long num18 = num17 % 10L;
      long num19 = num17 / 10L;
      long num20 = num19 % 10L;
      long num21 = num19 / 10L;
      long num22 = (10L - ((num4 + num8 + num12 + num16 + num20) * 3L + (num6 + num10 + num14 + num18)) % 10L) % 10L;
      long num23 = (10L - ((num6 + num10 + num14 + num18 + num22) * 3L + (num4 + num8 + num12 + num16 + num20)) % 10L) % 10L;
      flag = num3 * 100L + num22 * 10L + num23 == num1;
    }
    return flag;
  }
}
