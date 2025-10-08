// Decompiled with JetBrains decompiler
// Type: PathMedical.UserInterface.WindowsForms.ResourceManager.ResourceTransformer
// Assembly: PM.UserInterface.WindowsForms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A6299FCD-E4D5-49FB-8CD8-CF9B3D2A3E11
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.UserInterface.WindowsForms.dll

using PathMedical.DatabaseManagement;
using PathMedical.DatabaseManagement.Adapter;
using PathMedical.Exception;
using PathMedical.ResourceManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Resources;

#nullable disable
namespace PathMedical.UserInterface.WindowsForms.ResourceManager;

public class ResourceTransformer
{
  private const string Separator = "_____!_____";

  public void DatabaseToResx(string fileName)
  {
    ResXResourceWriter resXresourceWriter = (ResXResourceWriter) null;
    ICollection<ResourceTranslation> all;
    using (DBScope scope = new DBScope())
      all = new AdapterBase<ResourceTranslation>(scope).All;
    if (all == null)
      return;
    foreach (IGrouping<string, ResourceTranslation> grouping in all.GroupBy<ResourceTranslation, string>((Func<ResourceTranslation, string>) (e => e.Culture)).ToArray<IGrouping<string, ResourceTranslation>>())
    {
      try
      {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        resXresourceWriter = new ResXResourceWriter(string.Format("{0}\\resources.{1:yyyy}{1:MM}{1:dd}{1:HH}{1:mm}{1:ss}.{2}.{3}", (object) fileName, (object) DateTime.Now, (object) grouping.Key, (object) "resx"));
        foreach (ResourceTranslation resourceTranslation in (IEnumerable<ResourceTranslation>) grouping)
        {
          if (dictionary.ContainsKey(resourceTranslation.ResourceName) && dictionary.ContainsValue(resourceTranslation.Culture))
            throw ExceptionFactory.Instance.CreateException<InvalidOperationException>($"Entry {resourceTranslation.ResourceName} exists twice.");
          dictionary.Add(resourceTranslation.ResourceName, resourceTranslation.Culture);
          ResXDataNode node = new ResXDataNode($"{resourceTranslation.ResourceSet}{"_____!_____"}{resourceTranslation.ResourceName}", (object) resourceTranslation.ResourceText);
          resXresourceWriter.AddResource(node);
        }
        resXresourceWriter.Generate();
      }
      finally
      {
        resXresourceWriter?.Close();
      }
    }
  }

  public void ResxToDatabase(string resourceFileName)
  {
    AssemblyName[] names = new AssemblyName[0];
    string cultureName = ((IEnumerable<string>) Path.GetFileName(resourceFileName).Split('.')).ElementAt<string>(2);
    ResXResourceReader resXresourceReader = new ResXResourceReader(resourceFileName)
    {
      UseResXDataNodes = true
    };
    using (DBScope scope = new DBScope())
    {
      AdapterBase<ResourceTranslation> adapterBase = new AdapterBase<ResourceTranslation>(scope);
      foreach (DictionaryEntry dictionaryEntry in resXresourceReader)
      {
        if (dictionaryEntry.Value is ResXDataNode resXdataNode)
        {
          string dbResourceName = resXdataNode.Name;
          string dbResourceSet = string.Empty;
          if (resXdataNode.Name.Contains("_____!_____"))
          {
            string[] source = resXdataNode.Name.Split(new string[1]
            {
              "_____!_____"
            }, StringSplitOptions.None);
            if (((IEnumerable<string>) source).Count<string>() > 0)
            {
              dbResourceName = source[1];
              dbResourceSet = source[0];
            }
          }
          ResourceTranslation entity = adapterBase.FetchEntities((Expression<Func<ResourceTranslation, bool>>) (t => t.ResourceName == dbResourceName && t.ResourceSet == dbResourceSet && t.Culture == cultureName)).FirstOrDefault<ResourceTranslation>();
          if (entity != null)
          {
            string str = resXdataNode.GetValue(names) as string;
            entity.ResourceText = str ?? string.Empty;
          }
          else
          {
            string str = resXdataNode.GetValue(names) as string;
            entity = new ResourceTranslation()
            {
              ResourceName = dbResourceName,
              ResourceSet = dbResourceSet,
              Culture = cultureName,
              ResourceText = str ?? string.Empty
            };
          }
          adapterBase.Store(entity);
        }
      }
      scope.Complete();
    }
  }
}
