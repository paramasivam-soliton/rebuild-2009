// Decompiled with JetBrains decompiler
// Type: PathMedical.SystemConfiguration.Core.GlobalSystemConfiguration
// Assembly: PM.SystemConfiguration.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 48D3969F-7C1B-4635-8312-733FCC6A2713
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Base\PM.SystemConfiguration.Core.dll

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#nullable disable
namespace PathMedical.SystemConfiguration.Core;

public class GlobalSystemConfiguration
{
  public string reportPictureString;

  public DeletionConfirmation DisplayMessageAfterSuccessfullyDeletion { get; set; }

  public StorageConfirmation DisplayMessageAfterSuccessfullyStorage { get; set; }

  public TrackingSytem TrackingSystem { get; set; }

  public string DefaultSystemLanguage { get; set; }

  public DataModificationWarning DisplayDataModificationWarning { get; set; }

  public bool defaultPicture { get; set; }

  public Bitmap ReportPicture
  {
    get
    {
      return !string.IsNullOrEmpty(this.reportPictureString) ? GlobalSystemConfiguration.reportPicture(Convert.FromBase64String(this.reportPictureString)) : (Bitmap) null;
    }
    set
    {
      this.reportPictureString = GlobalSystemConfiguration.CreateBase64EncodedImage(GlobalSystemConfiguration.ImageToByte((Image) value));
    }
  }

  private static byte[] ImageToByte(Image image)
  {
    byte[] numArray = new byte[0];
    if (image != null)
    {
      MemoryStream memoryStream = new MemoryStream();
      Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
      Graphics.FromImage((Image) bitmap).DrawImage(image, new Point(0, 0));
      bitmap.Save((Stream) memoryStream, ImageFormat.Png);
      numArray = memoryStream.ToArray();
      memoryStream.Close();
    }
    return numArray;
  }

  private static string CreateBase64EncodedImage(byte[] image)
  {
    return image != null ? Convert.ToBase64String(image) : (string) null;
  }

  private static Bitmap reportPicture(byte[] imageArray)
  {
    return new Bitmap((Image) new ImageConverter().ConvertFrom((object) imageArray));
  }
}
