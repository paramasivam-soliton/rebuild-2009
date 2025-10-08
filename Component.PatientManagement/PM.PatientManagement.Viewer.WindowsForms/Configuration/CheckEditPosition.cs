// Decompiled with JetBrains decompiler
// Type: PathMedical.PatientManagement.Viewer.WindowsForms.Configuration.CheckEditPosition
// Assembly: PM.PatientManagement.Viewer.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4E00750C-D315-47E0-8E77-38DC77D96DC5
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.PatientManagement.Viewer.WindowsForms.dll

using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;

#nullable disable
namespace PathMedical.PatientManagement.Viewer.WindowsForms.Configuration;

internal class CheckEditPosition
{
  internal CheckEdit CheckEdit { get; set; }

  internal int RowHandle { get; set; }

  internal GridColumn Column { get; set; }
}
