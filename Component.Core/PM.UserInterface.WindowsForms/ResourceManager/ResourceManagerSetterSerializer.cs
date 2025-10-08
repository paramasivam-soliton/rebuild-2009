// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ResourceManager.ResourceManagerSetterSerializer
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using System.CodeDom;
using System.ComponentModel.Design.Serialization;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.ResourceManager;

public class ResourceManagerSetterSerializer : CodeDomSerializer
{
  public override object Deserialize(IDesignerSerializationManager manager, object codeDomObject)
  {
    return ((CodeDomSerializer) manager.GetSerializer(typeof (ResourceManagerSetter).BaseType, typeof (CodeDomSerializer))).Deserialize(manager, codeDomObject);
  }

  public override object Serialize(IDesignerSerializationManager manager, object value)
  {
    object obj = ((CodeDomSerializer) manager.GetSerializer(typeof (ResourceManagerSetter).BaseType, typeof (CodeDomSerializer))).Serialize(manager, value);
    if (obj is CodeStatementCollection)
      ((CodeStatementCollection) obj).Insert(0, (CodeStatement) new CodeExpressionStatement((CodeExpression) new CodeMethodInvokeExpression((CodeExpression) new CodeTypeReferenceExpression("PathMedical.ResourceManager.ResourceManagerProvider"), "GetResourceManager", new CodeExpression[2]
      {
        (CodeExpression) new CodeTypeOfExpression(((CodeTypeMember) manager.GetService(typeof (CodeTypeDeclaration))).Name),
        (CodeExpression) new CodeDirectionExpression(FieldDirection.Out, (CodeExpression) new CodeVariableReferenceExpression("resources"))
      })));
    return obj;
  }
}
