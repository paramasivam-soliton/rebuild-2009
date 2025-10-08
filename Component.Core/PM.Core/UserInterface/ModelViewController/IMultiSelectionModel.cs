// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ModelViewController.IMultiSelectionModel`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Collections.Generic;

#nullable disable
namespace PathMedical.UserInterface.ModelViewController;

public interface IMultiSelectionModel<TEntity> : IModel where TEntity : class, new()
{
  void ChangeSelectedItems(ICollection<TEntity> multiSelection);

  bool IsOneItemSelected<T>() where T : TEntity;

  void ChangeFocusedItem(TEntity item);

  bool IsOneItemFocused<T>() where T : TEntity;
}
