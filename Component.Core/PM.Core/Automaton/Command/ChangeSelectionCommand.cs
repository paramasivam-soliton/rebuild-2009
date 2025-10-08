// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.ChangeSelectionCommand`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PathMedical.Automaton.Command;

public class ChangeSelectionCommand<TEntity> : CommandBase where TEntity : class, new()
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (ChangeSelectionCommand<TEntity>), "$Rev: 1269 $");
  private readonly IModel model;

  public ChangeSelectionCommand(IModel model)
  {
    switch (model)
    {
      case null:
        throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
      case ISingleSelectionModel<TEntity> _:
      case IMultiSelectionModel<TEntity> _:
        this.Name = nameof (ChangeSelectionCommand<TEntity>);
        this.model = model;
        break;
      default:
        throw ExceptionFactory.Instance.CreateException<InvalidOperationException>("Invalid model to support change selections");
    }
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || this.TriggerEventArgs.TriggerContext == null || !(this.TriggerEventArgs.TriggerContext is ChangeSelectionTriggerContext<TEntity>))
      return;
    ChangeSelectionTriggerContext<TEntity> triggerContext = this.TriggerEventArgs.TriggerContext as ChangeSelectionTriggerContext<TEntity>;
    ICollection<TEntity> entities = (ICollection<TEntity>) null;
    if (triggerContext != null)
      entities = triggerContext.NewSelection;
    if (this.model is ISingleSelectionModel<TEntity>)
    {
      if (entities != null)
        this.ChangeSingleSelection(entities.FirstOrDefault<TEntity>());
      else
        this.ChangeSingleSelection(default (TEntity));
    }
    if (!(this.model is IMultiSelectionModel<TEntity>))
      return;
    this.ChangeMultiSelection(entities);
  }

  private void ChangeSingleSelection(TEntity selection)
  {
    if (!(this.model is ISingleSelectionModel<TEntity> model))
      return;
    if ((object) selection == null)
    {
      ChangeSelectionCommand<TEntity>.Logger.Info("Injecting null selection into Model {0}.", (object) model);
      model.ChangeSingleSelection(default (TEntity));
    }
    else
    {
      ChangeSelectionCommand<TEntity>.Logger.Info("Injecting single selection {0} into Model {1}.", (object) selection, (object) this.model);
      model.ChangeSingleSelection(selection);
    }
  }

  private void ChangeMultiSelection(ICollection<TEntity> selection)
  {
    if (!(this.model is IMultiSelectionModel<TEntity> model))
      return;
    if (selection == null)
    {
      ChangeSelectionCommand<TEntity>.Logger.Info("Injecting null selection into Model {0}.", (object) model);
      model.ChangeSelectedItems(selection);
    }
    else
    {
      ChangeSelectionCommand<TEntity>.Logger.Info("Injecting single selection {0} into Model {1}.", (object) selection, (object) this.model);
      model.ChangeSelectedItems(selection);
    }
  }
}
