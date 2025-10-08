// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.ProbeConfigurationUpload.ProbeConfigurationUploadComponentModule
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using PathMedical.ResourceManager;
using PathMedical.ServiceTools.WindowsForms.ProbeSelectionAssistant;
using PathMedical.ServiceTools.WindowsForms.Properties;
using PathMedical.Type1077;
using PathMedical.UserInterface;
using System;
using System.Drawing;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.ProbeConfigurationUpload;

internal class ProbeConfigurationUploadComponentModule : 
  ApplicationComponentModuleBase<Type1077Manager, ProbeSelectorAssistant>
{
  public static Guid ProbeSelectorComponentModuleId = new Guid("E498C46B-1CDA-426d-8C53-5A6DDAD92B0E");

  public ProbeConfigurationUploadComponentModule()
  {
    this.Id = new Guid("E498C46B-1CDA-426d-8C53-5A6DDAD92B0E");
    this.Name = "Upload Configuration";
    this.ShortcutName = "Upload Configuration";
    this.Image = (object) (ComponentResourceManagementBase<Resources>.Instance.ResourceManager.GetObject("GN_UploadProbeConfiguration") as Bitmap);
  }
}
