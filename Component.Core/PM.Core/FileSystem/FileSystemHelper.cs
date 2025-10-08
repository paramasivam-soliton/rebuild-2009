// Decompiled with JetBrains decompiler
// Type: PathMedical.FileSystem.FileSystemHelper
// Assembly: PM.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CB0027D9-775D-477D-8A47-69E286781E10
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.Core.dll

using PathMedical.Exception;
using PathMedical.Properties;
using System.IO;
using System.Security;
using System.Security.Permissions;

#nullable disable
namespace PathMedical.FileSystem;

public static class FileSystemHelper
{
  public static bool DoesFileExists(string fileName)
  {
    if (string.IsNullOrEmpty(fileName))
      return false;
    FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, fileName);
    try
    {
      fileIoPermission.Demand();
    }
    catch (SecurityException ex)
    {
      return false;
    }
    try
    {
      if (File.Exists(fileName))
        return true;
    }
    catch (System.Exception ex)
    {
      return false;
    }
    return false;
  }

  public static bool DoesFolderExists(string folderName)
  {
    if (string.IsNullOrEmpty(folderName))
      return false;
    FileIOPermission fileIoPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, folderName);
    try
    {
      fileIoPermission.Demand();
    }
    catch (SecurityException ex)
    {
      return false;
    }
    return Directory.Exists(folderName);
  }

  public static void CopyFile(string fileName, string destinationFolder)
  {
    if (string.IsNullOrEmpty(fileName))
      return;
    if (!FileSystemHelper.DoesFileExists(fileName))
      throw ExceptionFactory.Instance.CreateException<FileNotFoundException>(string.Format(Resources.FileSystemHelper_FileMissing, (object) fileName));
    string destFileName = Path.Combine(destinationFolder, new FileInfo(fileName).Name);
    File.Copy(fileName, destFileName);
  }

  public static void DeleteFile(string fileName)
  {
    if (string.IsNullOrEmpty(fileName))
      return;
    if (!FileSystemHelper.DoesFileExists(fileName))
      throw ExceptionFactory.Instance.CreateException<FileNotFoundException>(string.Format(Resources.FileSystemHelper_FileMissing, (object) fileName));
    File.Delete(fileName);
  }
}
