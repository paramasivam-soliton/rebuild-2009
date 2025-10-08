// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Mapper.IModelMapper`1
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace PathMedical.UserInterface.Mapper;

public interface IModelMapper<TEntity> : IEnumerable<ModelMapItem<TEntity>>, IEnumerable where TEntity : class
{
  void Add(
    Expression<Func<TEntity, object>> entityPropertyAccess,
    object ui,
    Expression<Func<object>> uiPropertyAccess);

  void CopyModelToUI(ICollection<TEntity> entities);

  void CopyModelToUI(TEntity entity);

  void CopyUIToModel();

  void SetUIEnabled(bool isUIEnabled);
}
