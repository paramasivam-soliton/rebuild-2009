// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Controls.IGroup
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PathMedical.UserInterface.Controls;

public interface IGroup : IElement, INotifyPropertyChanged
{
  Alignment Alignment { get; }

  List<IElement> Elements { get; }

  void Add(IElement element);

  void Add(IGroup group);

  void Remove(IElement element);

  void Remove(IGroup group);
}
