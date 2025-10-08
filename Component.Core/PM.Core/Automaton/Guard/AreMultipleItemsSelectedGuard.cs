// Decompiled with JetBrains decompiler
// Type: PathMedical.Automaton.Guard.AreMultipleItemsSelectedGuard
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.UserInterface.ModelViewController;
using System;
using System.Collections;

#nullable disable
namespace PathMedical.Automaton.Guard;

public class AreMultipleItemsSelectedGuard : GuardBase
{
  public AreMultipleItemsSelectedGuard(IMultiSelectionView view)
  {
    this.View = view != null ? view : throw ExceptionFactory.Instance.CreateException<ArgumentNullException>(nameof (view));
    this.Name = nameof (AreMultipleItemsSelectedGuard);
  }

  public IMultiSelectionView View { get; protected set; }

  public override bool Execute(TriggerEventArgs e)
  {
    ICollection selectedItems = this.View.SelectedItems;
    return selectedItems != null && selectedItems.Count > 1;
  }
}
