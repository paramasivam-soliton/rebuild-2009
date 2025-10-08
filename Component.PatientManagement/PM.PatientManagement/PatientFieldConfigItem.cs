// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.PatientFieldConfigItem
// Assembly: PM.PatientManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8132C915-2847-4A62-9EDC-EAE2A0AF1229
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.dll

using PathMedical.UserInterface.Fields;
using System.Reflection;

#nullable disable
namespace PathMedical.PatientManagement;

public class PatientFieldConfigItem
{
  private readonly PropertyInfo property;

  public PatientFieldConfigItem(string fieldId, string fieldName, string propertyName)
  {
    this.FieldId = fieldId;
    this.FieldName = fieldName;
    this.property = typeof (PatientManagementConfiguration).GetProperty(propertyName);
    FieldConfigurationType configurationType = (FieldConfigurationType) this.property.GetValue((object) PatientManagementConfiguration.Instance, (object[]) null);
    this.IsActive = (configurationType & FieldConfigurationType.Inactive) == FieldConfigurationType.Default;
    this.IsInMandatoryGroup1 = (configurationType & FieldConfigurationType.MandatoryGroup1) != 0;
    this.IsInMandatoryGroup2 = (configurationType & FieldConfigurationType.MandatoryGroup2) != 0;
    this.IsInMandatoryGroup3 = (configurationType & FieldConfigurationType.MandatoryGroup3) != 0;
  }

  public string FieldId { get; private set; }

  public string FieldName { get; private set; }

  public bool IsActive { get; set; }

  public bool IsInMandatoryGroup1 { get; set; }

  public bool IsInMandatoryGroup2 { get; set; }

  public bool IsInMandatoryGroup3 { get; set; }

  public void ApplyChanges()
  {
    FieldConfigurationType configurationType = FieldConfigurationType.Default;
    if (this.IsActive)
    {
      if (this.IsInMandatoryGroup1)
        configurationType |= FieldConfigurationType.MandatoryGroup1;
      if (this.IsInMandatoryGroup2)
        configurationType |= FieldConfigurationType.MandatoryGroup2;
      if (this.IsInMandatoryGroup3)
        configurationType |= FieldConfigurationType.MandatoryGroup3;
    }
    else
      configurationType = FieldConfigurationType.Inactive;
    this.property.SetValue((object) PatientManagementConfiguration.Instance, (object) configurationType, (object[]) null);
  }

  public override bool Equals(object obj)
  {
    return obj is PatientFieldConfigItem patientFieldConfigItem && this.FieldId.Equals(patientFieldConfigItem.FieldId);
  }

  public override int GetHashCode() => this.GetType().GetHashCode() + this.FieldId.GetHashCode();
}
