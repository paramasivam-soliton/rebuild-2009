// Decompiled with JetBrains decompiler
// Type: PathMedical.SiteAndFacilityManagement.Automaton.AssignLocationToAllFacilitiesCommand
// Assembly: PM.SiteAndFacilityManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F9CADFD-E9C9-4783-BA9E-8D20FAD3C075
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SiteAndFacilityManagement.dll

using PathMedical.Automaton.Command;
using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.SiteAndFacilityManagement.Automaton;

public class AssignLocationToAllFacilitiesCommand : CommandBase
{
  private readonly LocationTypeManager locationTypeManager;
  private readonly IView view;

  public AssignLocationToAllFacilitiesCommand(IView view, LocationTypeManager model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (AssignLocationToAllFacilitiesCommand);
    this.locationTypeManager = model;
    this.view = view;
  }

  public override void Execute()
  {
    if (this.locationTypeManager == null || this.view == null || this.view.DisplayQuestion("Assign location to all facilities?", AnswerOptionType.YesNo, QuestionIcon.Question) != AnswerType.Yes)
      return;
    FacilityManager.Instance.AssignDefaultLocationTypes(this.locationTypeManager.SelectedLocationType);
  }
}
