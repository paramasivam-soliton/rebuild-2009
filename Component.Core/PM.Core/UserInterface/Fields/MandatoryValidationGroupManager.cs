// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Fields.MandatoryValidationGroupManager
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.UserInterface.Fields;

public class MandatoryValidationGroupManager
{
  private static readonly ILogger Logger = LogFactory.Instance.Create("UserInterface");
  private readonly List<MandatoryValidationGroup> groups;
  private bool validatingGroups;

  public MandatoryValidationGroupManager() => this.groups = new List<MandatoryValidationGroup>();

  public void Add(MandatoryValidationGroup validationGroup)
  {
    if (validationGroup == null || this.groups.Contains(validationGroup))
      return;
    validationGroup.GroupValidated += new EventHandler<MandatoryGroupValidatedEventArgs>(this.OnGroupValidated);
    this.groups.Add(validationGroup);
  }

  public int Count => this.groups.Count;

  private void OnGroupValidated(object sender, MandatoryGroupValidatedEventArgs e)
  {
    if (this.validatingGroups)
      return;
    try
    {
      this.validatingGroups = true;
      MandatoryValidationGroup mandatoryValidationGroup1 = (MandatoryValidationGroup) null;
      foreach (MandatoryValidationGroup group in this.groups)
      {
        MandatoryValidationGroupManager.Logger.Debug("Validating mandatory group {0}", (object) group);
        if (group != sender)
        {
          if (group.ValidateGroup())
          {
            mandatoryValidationGroup1 = group;
            break;
          }
        }
        else if (e != null && e.IsValid && sender != null)
        {
          mandatoryValidationGroup1 = sender as MandatoryValidationGroup;
          break;
        }
      }
      IEnumerable<MandatoryValidationGroup> mandatoryValidationGroups;
      if (mandatoryValidationGroup1 != null)
        mandatoryValidationGroups = (IEnumerable<MandatoryValidationGroup>) this.groups.Except<MandatoryValidationGroup>((IEnumerable<MandatoryValidationGroup>) new List<MandatoryValidationGroup>()
        {
          mandatoryValidationGroup1
        }).ToList<MandatoryValidationGroup>();
      else
        mandatoryValidationGroups = (IEnumerable<MandatoryValidationGroup>) this.groups;
      if (mandatoryValidationGroup1 != null)
      {
        foreach (MandatoryValidationGroup mandatoryValidationGroup2 in mandatoryValidationGroups)
          mandatoryValidationGroup2.EnableGroup(false);
        mandatoryValidationGroup1.EnableGroup(true);
      }
      else
      {
        foreach (MandatoryValidationGroup mandatoryValidationGroup3 in mandatoryValidationGroups)
          mandatoryValidationGroup3.EnableGroup(true);
      }
    }
    finally
    {
      this.validatingGroups = false;
    }
  }

  public void ValidateGroups()
  {
    this.OnGroupValidated((object) null, (MandatoryGroupValidatedEventArgs) null);
  }

  public bool IsOneGroupValid
  {
    get
    {
      return this.groups.Count == 0 || this.groups.Any<MandatoryValidationGroup>((Func<MandatoryValidationGroup, bool>) (mg => mg.AreAllFieldsFilled));
    }
  }
}
