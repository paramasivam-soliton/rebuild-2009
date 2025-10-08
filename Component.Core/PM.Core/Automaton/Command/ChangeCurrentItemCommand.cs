// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Command.ChangeCurrentItemCommand`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Logging;
using PathMedical.UserInterface.ModelViewController;
using System;

#nullable disable
namespace PathMedical.Automaton.Command;

public class ChangeCurrentItemCommand<TEntity> : CommandBase where TEntity : class, new()
{
  private static readonly ILogger Logger = LogFactory.Instance.Create(typeof (ChangeCurrentItemCommand<TEntity>), "$Rev: 1269 $");
  private readonly IMultiSelectionModel<TEntity> model;

  public ChangeCurrentItemCommand(IMultiSelectionModel<TEntity> model)
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (model));
    this.Name = nameof (ChangeCurrentItemCommand<TEntity>);
    this.model = model;
  }

  public override void Execute()
  {
    if (this.TriggerEventArgs == null || this.TriggerEventArgs.TriggerContext == null || !(this.TriggerEventArgs.TriggerContext is ChangeCurrentItemTriggerContext<TEntity>))
      return;
    ChangeCurrentItemTriggerContext<TEntity> triggerContext = this.TriggerEventArgs.TriggerContext as ChangeCurrentItemTriggerContext<TEntity>;
    TEntity entity = default (TEntity);
    if (triggerContext != null)
      entity = triggerContext.NewSelection;
    if ((object) entity == null)
      ChangeCurrentItemCommand<TEntity>.Logger.Info("Injecting null selection into Model {0}.", (object) this.model);
    else
      ChangeCurrentItemCommand<TEntity>.Logger.Info("Injecting single selection {0} into Model {1}.", (object) entity, (object) this.model);
    this.model.ChangeFocusedItem(entity);
  }
}
