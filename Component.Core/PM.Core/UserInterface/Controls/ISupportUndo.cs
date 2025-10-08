// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.Controls.ISupportUndo
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using System.Linq.Expressions;

#nullable disable
namespace PathMedical.UserInterface.Controls;

public interface ISupportUndo
{
  MemberExpression UniqueModelMemberIdentifier { get; set; }

  bool IsUndoDisabled { get; set; }

  bool IsUndoing { get; set; }

  bool IsModified { get; set; }

  bool IsNavigationOnly { get; set; }

  bool ShowModified { get; set; }

  void RestoreValue(object valueToRestore);

  void SetSaved();
}
