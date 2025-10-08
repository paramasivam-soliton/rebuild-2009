// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.IPresentationLayer
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.UserInterface.ModelViewController;

#nullable disable
namespace PathMedical.UserInterface;

public interface IPresentationLayer
{
  void AddApplicationComponent(IApplicationComponent applicationComponent);

  void RemoveApplicationComponent(IApplicationComponent applicationComponent);

  void ChangeApplicationComponentModule(IApplicationComponentModule module);

  void ShowActiveModule();

  void StartAssistant(IView assistant);

  void CloseAssistant();
}
