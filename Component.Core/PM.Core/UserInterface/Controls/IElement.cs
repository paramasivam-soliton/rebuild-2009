// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Controls.IElement
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Automaton;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace PathMedical.UserInterface.Controls;

public interface IElement : INotifyPropertyChanged
{
  string Caption { get; set; }

  string Description { get; set; }

  string ToolTip { get; set; }

  Image Image { get; set; }

  bool Enabled { get; set; }

  LocationType LocationType { get; set; }

  Trigger Trigger { get; set; }

  object Parent { get; }
}
