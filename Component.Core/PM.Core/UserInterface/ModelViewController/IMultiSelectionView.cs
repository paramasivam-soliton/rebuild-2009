// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.ModelViewController.IMultiSelectionView
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System;
using System.Collections;

#nullable disable
namespace PathMedical.UserInterface.ModelViewController;

public interface IMultiSelectionView : 
  IView,
  IDisposable,
  ISupportControllerAction,
  ISupportUserInterfaceManager
{
  ICollection SelectedItems { get; }
}
