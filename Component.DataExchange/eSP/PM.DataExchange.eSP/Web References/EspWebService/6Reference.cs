// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.EspWebService.uploadDataCompletedEventArgs
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace PathMedical.DataExchange.eSP.EspWebService;

[GeneratedCode("System.Web.Services", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
public class uploadDataCompletedEventArgs : AsyncCompletedEventArgs
{
  private object[] results;

  internal uploadDataCompletedEventArgs(
    object[] results,
    Exception exception,
    bool cancelled,
    object userState)
    : base(exception, cancelled, userState)
  {
    this.results = results;
  }

  public uploadDataResponseUploadDataResult Result
  {
    get
    {
      this.RaiseExceptionIfNecessary();
      return (uploadDataResponseUploadDataResult) this.results[0];
    }
  }
}
