// Decompiled with JetBrains decompiler
// Type: PathMedical.DataExchange.eSP.EspWebService.DataUpload
// Assembly: PM.DataExchange.eSP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2537EFF3-92D8-4A02-ACBB-8BE86F05CEFB
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.DataExchange.eSP.dll

using PathMedical.DataExchange.eSP.Properties;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;

#nullable disable
namespace PathMedical.DataExchange.eSP.EspWebService;

[GeneratedCode("System.Web.Services", "4.0.30319.1")]
[DebuggerStepThrough]
[DesignerCategory("code")]
[WebServiceBinding(Name = "DataUploadSoap", Namespace = "http://www.myapp.org")]
public class DataUpload : SoapHttpClientProtocol
{
  private CSPCHD cSPCHDValueField;
  private SendOrPostCallback downloadSyncDataOperationCompleted;
  private SendOrPostCallback uploadDataOperationCompleted;
  private SendOrPostCallback validateEquipmentOperationCompleted;
  private bool useDefaultCredentialsSetExplicitly;

  public DataUpload()
  {
    this.Url = Settings.Default.PM_DataExchange_eSP_EspWebService_DataUpload;
    if (this.IsLocalFileSystemWebService(this.Url))
    {
      this.UseDefaultCredentials = true;
      this.useDefaultCredentialsSetExplicitly = false;
    }
    else
      this.useDefaultCredentialsSetExplicitly = true;
  }

  public CSPCHD CSPCHDValue
  {
    get => this.cSPCHDValueField;
    set => this.cSPCHDValueField = value;
  }

  public new string Url
  {
    get => base.Url;
    set
    {
      if (this.IsLocalFileSystemWebService(base.Url) && !this.useDefaultCredentialsSetExplicitly && !this.IsLocalFileSystemWebService(value))
        base.UseDefaultCredentials = false;
      base.Url = value;
    }
  }

  public new bool UseDefaultCredentials
  {
    get => base.UseDefaultCredentials;
    set
    {
      base.UseDefaultCredentials = value;
      this.useDefaultCredentialsSetExplicitly = true;
    }
  }

  public event downloadSyncDataCompletedEventHandler downloadSyncDataCompleted;

  public event uploadDataCompletedEventHandler uploadDataCompleted;

  public event validateEquipmentCompletedEventHandler validateEquipmentCompleted;

  [SoapHeader("CSPCHDValue", Direction = SoapHeaderDirection.InOut)]
  [SoapDocumentMethod("http://www.myapp.org/northgate.esp.sedq.bserv.ProxySEDQ.downloadSyncData", RequestNamespace = "http://www.myapp.org", ResponseNamespace = "http://www.myapp.org", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public downloadSyncDataResponseDownloadSyncDataResult downloadSyncData(string SyncAllXML)
  {
    return (downloadSyncDataResponseDownloadSyncDataResult) this.Invoke(nameof (downloadSyncData), new object[1]
    {
      (object) SyncAllXML
    })[0];
  }

  public void downloadSyncDataAsync(string SyncAllXML)
  {
    this.downloadSyncDataAsync(SyncAllXML, (object) null);
  }

  public void downloadSyncDataAsync(string SyncAllXML, object userState)
  {
    if (this.downloadSyncDataOperationCompleted == null)
      this.downloadSyncDataOperationCompleted = new SendOrPostCallback(this.OndownloadSyncDataOperationCompleted);
    this.InvokeAsync("downloadSyncData", new object[1]
    {
      (object) SyncAllXML
    }, this.downloadSyncDataOperationCompleted, userState);
  }

  private void OndownloadSyncDataOperationCompleted(object arg)
  {
    if (this.downloadSyncDataCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.downloadSyncDataCompleted((object) this, new downloadSyncDataCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapHeader("CSPCHDValue", Direction = SoapHeaderDirection.InOut)]
  [SoapDocumentMethod("http://www.myapp.org/northgate.esp.sedq.bserv.ProxySEDQ.uploadData", RequestNamespace = "http://www.myapp.org", ResponseNamespace = "http://www.myapp.org", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public uploadDataResponseUploadDataResult uploadData(string thisMessageXML)
  {
    return (uploadDataResponseUploadDataResult) this.Invoke(nameof (uploadData), new object[1]
    {
      (object) thisMessageXML
    })[0];
  }

  public void uploadDataAsync(string thisMessageXML)
  {
    this.uploadDataAsync(thisMessageXML, (object) null);
  }

  public void uploadDataAsync(string thisMessageXML, object userState)
  {
    if (this.uploadDataOperationCompleted == null)
      this.uploadDataOperationCompleted = new SendOrPostCallback(this.OnuploadDataOperationCompleted);
    this.InvokeAsync("uploadData", new object[1]
    {
      (object) thisMessageXML
    }, this.uploadDataOperationCompleted, userState);
  }

  private void OnuploadDataOperationCompleted(object arg)
  {
    if (this.uploadDataCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.uploadDataCompleted((object) this, new uploadDataCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  [SoapHeader("CSPCHDValue", Direction = SoapHeaderDirection.InOut)]
  [SoapDocumentMethod("http://www.myapp.org/northgate.esp.sedq.bserv.ProxySEDQ.validateEquipment", RequestNamespace = "http://www.myapp.org", ResponseNamespace = "http://www.myapp.org", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
  public validateEquipmentResponseValidateEquipmentResult validateEquipment(string equipXML)
  {
    return (validateEquipmentResponseValidateEquipmentResult) this.Invoke(nameof (validateEquipment), new object[1]
    {
      (object) equipXML
    })[0];
  }

  public void validateEquipmentAsync(string equipXML)
  {
    this.validateEquipmentAsync(equipXML, (object) null);
  }

  public void validateEquipmentAsync(string equipXML, object userState)
  {
    if (this.validateEquipmentOperationCompleted == null)
      this.validateEquipmentOperationCompleted = new SendOrPostCallback(this.OnvalidateEquipmentOperationCompleted);
    this.InvokeAsync("validateEquipment", new object[1]
    {
      (object) equipXML
    }, this.validateEquipmentOperationCompleted, userState);
  }

  private void OnvalidateEquipmentOperationCompleted(object arg)
  {
    if (this.validateEquipmentCompleted == null)
      return;
    InvokeCompletedEventArgs completedEventArgs = (InvokeCompletedEventArgs) arg;
    this.validateEquipmentCompleted((object) this, new validateEquipmentCompletedEventArgs(completedEventArgs.Results, completedEventArgs.Error, completedEventArgs.Cancelled, completedEventArgs.UserState));
  }

  public new void CancelAsync(object userState) => base.CancelAsync(userState);

  private bool IsLocalFileSystemWebService(string url)
  {
    if (url == null || url == string.Empty)
      return false;
    Uri uri = new Uri(url);
    return uri.Port >= 1024 /*0x0400*/ && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0;
  }
}
