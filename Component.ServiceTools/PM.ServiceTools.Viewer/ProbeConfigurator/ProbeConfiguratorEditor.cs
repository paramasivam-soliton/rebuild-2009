// Decompiled with JetBrains decompiler
// Type: PathMedical.ServiceTools.WindowsForms.ProbeConfigurator.ProbeConfiguratorEditor
// Assembly: PM.ServiceTools.Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B3D9BBE7-B327-4903-9AB0-77BF495386B1
// Assembly location: C:\Users\paramasivam.g\Downloads\Release\Release\Plugin\PM.ServiceTools.Viewer.dll

using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraLayout;
using PathMedical.Automaton;
using PathMedical.Exception;
using PathMedical.InstrumentManagement;
using PathMedical.ServiceTools.Automaton;
using PathMedical.ServiceTools.WindowsForms.DeviceSelectorAsistant;
using PathMedical.ServiceTools.WindowsForms.ProbeSelectionAssistant;
using PathMedical.SystemConfiguration;
using PathMedical.Type1077;
using PathMedical.Type1077.Automaton;
using PathMedical.Type1077.Communicator.WindowsForms.FirmwareImageImport;
using PathMedical.Type1077.Firmware;
using PathMedical.UserInterface;
using PathMedical.UserInterface.Fields;
using PathMedical.UserInterface.Mapper;
using PathMedical.UserInterface.ModelViewController;
using PathMedical.UserInterface.WindowsForms.UserInterfaceControls.Controls.DevExpress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

#nullable disable
namespace PathMedical.ServiceTools.WindowsForms.ProbeConfigurator;

public class ProbeConfiguratorEditor : PathMedical.UserInterface.WindowsForms.ModelViewController.View
{
  private ModelMapper<Type1077ProbeInformation> modelMapper;
  private ModelMapper<Type1077LoopBackCableInformation> modelMapper2;
  private Type1077Instrument instrument;
  private Type1077ProbeInformation probeInformation;
  private Type1077LoopBackCableInformation loopBackCableInformation;
  private FirmwareImage firmware;
  private IContainer components;
  private LayoutControl layoutControl1;
  private LayoutControlGroup layoutControlGroup1;
  private DevExpressTextEdit txtProbeInformationProbeType;
  private DevExpressTextEdit txtProbeInformationProbeSerialNumber;
  private DevExpressTextEdit txtSpeaker1_1kHz;
  private DevExpressTextEdit txtSpeaker1_2kHz;
  private LayoutControlItem layoutControlItem1;
  private DevExpressTextEdit txtSpeaker1_4kHz;
  private DevExpressTextEdit txtSpeaker1_6kHz;
  private DevExpressTextEdit txtSpeaker2_1kHz;
  private DevExpressTextEdit txtSpeaker2_2kHz;
  private DevExpressTextEdit txtSpeaker2_6kHz;
  private DevExpressTextEdit txtSpeaker2_4kHz;
  private DevExpressTextEdit txtMicrophoneSetting;
  private DevExpressTextEdit txtCurrSpk1Value1kHz;
  private DevExpressTextEdit txtCurrSpk1Value2kHz;
  private DevExpressTextEdit txtCurrSpk1Value4kHz;
  private DevExpressTextEdit txtCurrSpk1Value6kHz;
  private DevExpressTextEdit txtCurrSpk2Value1kHz;
  private DevExpressTextEdit txtCurrSpk2Value2kHz;
  private DevExpressTextEdit txtCurrSpk2Value4kHz;
  private DevExpressTextEdit txtCurrSpk2Value6kHz;
  private CheckButton cbSpk1_1kHz;
  private CheckButton cbSpk1_2kHz;
  private CheckButton cbSpk1_6kHz;
  private CheckButton cbSpk1_4kHz;
  private CheckButton cbSpk2_6kHz;
  private CheckButton cbSpk2_4kHz;
  private CheckButton cbSpk2_2kHz;
  private CheckButton cbSpk2_1kHz;
  private SimpleButton bSetMic1Values;
  private EmptySpaceItem emptySpaceItem8;
  private DevExpressTextEdit txtLastCalDate;
  private DevExpressButton bStartLoopBackTest;
  private DevExpressCheckedEdit cbLoopBackTest2;
  private DevExpressCheckedEdit cbLoopBackTest7;
  private DevExpressCheckedEdit cbLoopBackTest6;
  private DevExpressCheckedEdit cbLoopBackTest5;
  private DevExpressCheckedEdit cbLoopBackTest4;
  private DevExpressCheckedEdit cbLoopBackTest3;
  private DevExpressCheckedEdit cbTestAbrCanels;
  private DevExpressButton btnDeleteFlash;
  private TabbedControlGroup tabbedControlGroup3;
  private LayoutControlGroup layoutDevicSettingGroup;
  private LayoutControlItem layoutControlItem19;
  private LayoutControlGroup layoutProbeConfigurationGroup;
  private LayoutControlItem layouttxtProbeInformationProbeType;
  private LayoutControlItem layoutProbeInformationProbeSerialNumber;
  private EmptySpaceItem emptySpaceItem1;
  private LayoutControlItem layoutLastCalibrationDate;
  private LayoutControlGroup layoutSpeakerSettings;
  private LayoutControlItem layoutSpeaker1_1kHz;
  private LayoutControlItem layoutSpeaker1_2kHz;
  private LayoutControlItem layoutSpeaker1_4kHz;
  private LayoutControlItem layoutSpeaker1_6kHz;
  private LayoutControlItem layoutSpeaker2_1kHz;
  private LayoutControlItem layoutSpeaker2_2kHz;
  private LayoutControlItem layoutSpeaker2_4kHz;
  private LayoutControlItem layoutSpeaker2_6kHz;
  private EmptySpaceItem emptySpaceItem2;
  private LayoutControlItem layoutMicrophoneSetting;
  private EmptySpaceItem emptySpaceItem4;
  private LayoutControlGroup layoutControlGroupSpk1Calculation;
  private LayoutControlItem layoutCurrSpk1Value1kHz;
  private LayoutControlItem layoutCurrSpk1Value2kHz;
  private LayoutControlItem layoutCurrSpk1Value4kHz;
  private LayoutControlItem layoutCurrSpk1Value6kHz;
  private LayoutControlItem layoutControlItem2;
  private LayoutControlItem layoutControlItem3;
  private LayoutControlItem layoutControlItem4;
  private LayoutControlItem layoutControlItem5;
  private EmptySpaceItem emptySpaceItem7;
  private LayoutControlGroup layoutControlGroupCurrentSpeaker2Values;
  private LayoutControlItem layoutCurrSpk2Value1kHz;
  private LayoutControlItem layoutCurrSpk2Value2kHz;
  private LayoutControlItem layoutCurrSpk2Value4kHz;
  private LayoutControlItem layoutCurrSpk2Value6kHz;
  private EmptySpaceItem emptySpaceItem5;
  private LayoutControlItem layoutControlItem6;
  private LayoutControlItem layoutControlItem7;
  private LayoutControlItem layoutControlItem8;
  private LayoutControlItem layoutControlItem9;
  private LayoutControlGroup layoutControlGroupMic1Calculation;
  private LayoutControlItem layoutSetMic1Values;
  private EmptySpaceItem emptySpaceItem6;
  private EmptySpaceItem emptySpaceItem3;
  private EmptySpaceItem emptySpaceItem10;
  private LayoutControlGroup layoutLoopBackCable;
  private LayoutControlItem layoutControlItem10;
  private EmptySpaceItem emptySpaceItem9;
  private LayoutControlItem layoutControlItem13;
  private LayoutControlItem layoutControlItem11;
  private LayoutControlItem layoutControlItem14;
  private LayoutControlItem layoutControlItem15;
  private LayoutControlItem layoutControlItem16;
  private LayoutControlItem layoutControlItem18;
  private LayoutControlItem layoutControlItem17;
  private EmptySpaceItem emptySpaceItem12;
  private EmptySpaceItem emptySpaceItem11;
  private LayoutControlGroup layoutDeviceSettingsGroup;
  private DevExpressButton btnSelectFirmware;
  private LayoutControlGroup layoutFirmwareGroup;
  private LayoutControlItem layoutControlItem20;
  private EmptySpaceItem emptySpaceItem15;
  private EmptySpaceItem emptySpaceItem14;
  private EmptySpaceItem emptySpaceItem13;
  private LayoutControlGroup layoutResultGroup;
  private EmptySpaceItem emptySpaceItem16;
  private DevExpressTextEdit tbEditFwLicence;
  private LayoutControlGroup layoutControlGroupFirmware;
  private LayoutControlGroup layoutControlLicence;
  private LayoutControlItem layoutControlItemFwLicence;
  private EmptySpaceItem emptySpaceItem18;
  private EmptySpaceItem emptySpaceItem17;
  private DevExpressButton btnSetFwLicence;
  private EmptySpaceItem emptySpaceItem19;
  private EmptySpaceItem emptySpaceItem20;
  private LayoutControlItem layoutControlItem21;
  private DevExpressButton btnUpdateFirmware;
  private LayoutControlItem layoutControlItem22;
  private EmptySpaceItem emptySpaceItem21;
  private LayoutControlGroup layoutControlGroupLoopBackCableTest;
  private LayoutControlGroup layoutControlGroupCodecTest;
  private EmptySpaceItem emptySpaceItem22;
  private DevExpressButton btnStopCodecTest;
  private DevExpressButton btnStartCodecTest1;
  private LayoutControlItem layoutControlItem12;
  private LayoutControlItem btnStopCodecTest1;
  private LayoutControlGroup layoutControlCodecTestResults;
  private EmptySpaceItem emptySpaceItem23;
  private DevExpressCheckedEdit cbCodecVoltageLevel;
  private LayoutControlItem layoutCodecVoltageLevel;

  public ProbeConfiguratorEditor()
  {
    this.InitializeComponent();
    this.CreateRibbonBarCommands();
    this.InitializeModelMapper();
    this.txtLastCalDate.Properties.DisplayFormat.FormatString = "{0:d}";
  }

  public ProbeConfiguratorEditor(IModel model)
    : this()
  {
    if (model == null)
      throw ExceptionFactory.Instance.CreateException<ArgumentNullException>("Model");
    model.Changed += new EventHandler<ModelChangedEventArgs>(this.OnModelChanged);
  }

  private void InitializeModelMapper()
  {
    bool isEditingEnabled = this.ViewMode != 0;
    ModelMapper<Type1077ProbeInformation> modelMapper1 = new ModelMapper<Type1077ProbeInformation>(isEditingEnabled);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => p.ProbeTypeString), (object) this.txtProbeInformationProbeType);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.SerialNumber), (object) this.txtProbeInformationProbeSerialNumber);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.CalibrationDate), (object) this.txtLastCalDate);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.Speaker1CorrectionValue1KHz), (object) this.txtSpeaker1_1kHz);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.Speaker1CorrectionValue2KHz), (object) this.txtSpeaker1_2kHz);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.Speaker1CorrectionValue4KHz), (object) this.txtSpeaker1_4kHz);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.Speaker1CorrectionValue6KHz), (object) this.txtSpeaker1_6kHz);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.Speaker2CorrectionValue1KHz), (object) this.txtSpeaker2_1kHz);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.Speaker2CorrectionValue2KHz), (object) this.txtSpeaker2_2kHz);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.Speaker2CorrectionValue4KHz), (object) this.txtSpeaker2_4kHz);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.Speaker2CorrectionValue6KHz), (object) this.txtSpeaker2_6kHz);
    modelMapper1.Add((Expression<Func<Type1077ProbeInformation, object>>) (p => (object) p.Microfone1CorrectionValue1KHz), (object) this.txtMicrophoneSetting);
    this.modelMapper = modelMapper1;
    ModelMapper<Type1077LoopBackCableInformation> modelMapper2 = new ModelMapper<Type1077LoopBackCableInformation>(isEditingEnabled);
    modelMapper2.Add((Expression<Func<Type1077LoopBackCableInformation, object>>) (p => (object) p.LoopBackTest2), (object) this.cbLoopBackTest2);
    modelMapper2.Add((Expression<Func<Type1077LoopBackCableInformation, object>>) (p => (object) p.LoopBackTest3), (object) this.cbLoopBackTest3);
    modelMapper2.Add((Expression<Func<Type1077LoopBackCableInformation, object>>) (p => (object) p.LoopBackTest4), (object) this.cbLoopBackTest4);
    modelMapper2.Add((Expression<Func<Type1077LoopBackCableInformation, object>>) (p => (object) p.LoopBackTest5), (object) this.cbLoopBackTest5);
    modelMapper2.Add((Expression<Func<Type1077LoopBackCableInformation, object>>) (p => (object) p.LoopBackTest6), (object) this.cbLoopBackTest6);
    modelMapper2.Add((Expression<Func<Type1077LoopBackCableInformation, object>>) (p => (object) p.LoopBackTest7), (object) this.cbLoopBackTest7);
    this.modelMapper2 = modelMapper2;
    this.modelMapper.SetUIEnabledForced(false, (object) this.txtProbeInformationProbeType, (object) this.txtProbeInformationProbeSerialNumber, (object) this.txtLastCalDate, (object) this.txtSpeaker1_1kHz, (object) this.txtSpeaker1_2kHz, (object) this.txtSpeaker1_4kHz, (object) this.txtSpeaker1_6kHz, (object) this.txtSpeaker2_1kHz, (object) this.txtSpeaker2_2kHz, (object) this.txtSpeaker2_4kHz, (object) this.txtSpeaker2_6kHz, (object) this.txtMicrophoneSetting);
  }

  private void OnModelChanged(object sender, ModelChangedEventArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new ProbeConfiguratorEditor.UpdateViewCallBack(this.UpdateView), (object) e.ChangeType, (object) e.Type, e.ChangedObject);
    else
      this.UpdateView(e.ChangeType, e.Type, e.ChangedObject);
  }

  private void UpdateView(ChangeType changeType, Type type, object item)
  {
    if (type == typeof (Type1077ProbeInformation))
    {
      object obj = (object) (item as ICollection<Type1077ProbeInformation>);
      if (obj == null)
        obj = (object) new Type1077ProbeInformation[1]
        {
          item as Type1077ProbeInformation
        };
      ICollection<Type1077ProbeInformation> probeInformations = (ICollection<Type1077ProbeInformation>) obj;
      this.probeInformation = probeInformations.FirstOrDefault<Type1077ProbeInformation>();
      this.FillFields(probeInformations);
      if (this.probeInformation != null)
      {
        uint? probeType = this.probeInformation.ProbeType;
        if (probeType.GetValueOrDefault() == 34577U)
        {
          this.txtSpeaker1_6kHz.Enabled = true;
          this.cbSpk1_6kHz.Enabled = true;
          this.txtSpeaker2_1kHz.Enabled = false;
          this.txtSpeaker2_2kHz.Enabled = false;
          this.txtSpeaker2_4kHz.Enabled = false;
          this.txtSpeaker2_6kHz.Enabled = false;
          this.layoutControlGroupCurrentSpeaker2Values.Enabled = false;
          this.layoutControlGroupMic1Calculation.Enabled = true;
          this.txtMicrophoneSetting.Enabled = true;
        }
        probeType = this.probeInformation.ProbeType;
        if (probeType.GetValueOrDefault() == 34833U)
        {
          this.txtSpeaker1_6kHz.Enabled = true;
          this.cbSpk1_6kHz.Enabled = true;
          this.txtSpeaker2_1kHz.Enabled = true;
          this.txtSpeaker2_2kHz.Enabled = true;
          this.txtSpeaker2_4kHz.Enabled = true;
          this.txtSpeaker2_6kHz.Enabled = true;
          this.cbSpk2_6kHz.Enabled = true;
          this.layoutControlGroupCurrentSpeaker2Values.Enabled = true;
          this.layoutControlGroupMic1Calculation.Enabled = true;
          this.txtMicrophoneSetting.Enabled = true;
        }
        probeType = this.probeInformation.ProbeType;
        if (probeType.GetValueOrDefault() == 34304U)
        {
          this.txtSpeaker1_6kHz.Enabled = false;
          this.cbSpk1_6kHz.Enabled = false;
          this.txtSpeaker2_1kHz.Enabled = true;
          this.txtSpeaker2_2kHz.Enabled = true;
          this.txtSpeaker2_4kHz.Enabled = true;
          this.txtSpeaker2_6kHz.Enabled = false;
          this.layoutControlGroupCurrentSpeaker2Values.Enabled = true;
          this.cbSpk2_6kHz.Enabled = false;
          this.layoutControlGroupMic1Calculation.Enabled = false;
          this.txtMicrophoneSetting.Enabled = false;
        }
      }
      else
      {
        this.txtSpeaker1_6kHz.Enabled = true;
        this.cbSpk1_6kHz.Enabled = true;
        this.txtSpeaker2_1kHz.Enabled = true;
        this.txtSpeaker2_2kHz.Enabled = true;
        this.txtSpeaker2_4kHz.Enabled = true;
        this.txtSpeaker2_6kHz.Enabled = true;
        this.cbSpk2_6kHz.Enabled = true;
        this.layoutControlGroupCurrentSpeaker2Values.Enabled = true;
        this.layoutControlGroupMic1Calculation.Enabled = true;
        this.txtMicrophoneSetting.Enabled = true;
      }
    }
    if (type == typeof (Type1077Instrument))
    {
      object source = (object) (item as ICollection<Type1077Instrument>);
      if (source == null)
        source = (object) new Type1077Instrument[1]
        {
          item as Type1077Instrument
        };
      this.instrument = ((IEnumerable<Type1077Instrument>) source).FirstOrDefault<Type1077Instrument>();
    }
    if (!(type == typeof (Type1077LoopBackCableInformation)))
      return;
    object obj1 = (object) (item as ICollection<Type1077LoopBackCableInformation>);
    if (obj1 == null)
      obj1 = (object) new Type1077LoopBackCableInformation[1]
      {
        item as Type1077LoopBackCableInformation
      };
    ICollection<Type1077LoopBackCableInformation> cableInformations = (ICollection<Type1077LoopBackCableInformation>) obj1;
    this.loopBackCableInformation = cableInformations.FirstOrDefault<Type1077LoopBackCableInformation>();
    this.FillFields(cableInformations);
  }

  private void FillFields(
    ICollection<Type1077ProbeInformation> probeInformation)
  {
    this.modelMapper.SetUIEnabled(this.ViewMode != ViewModeType.Viewing && probeInformation != null);
    this.modelMapper.CopyModelToUI(probeInformation);
  }

  private void FillFields(
    ICollection<Type1077LoopBackCableInformation> loopBackCableInformation)
  {
    this.modelMapper2.SetUIEnabled(this.ViewMode != ViewModeType.Viewing && loopBackCableInformation != null);
    this.modelMapper2.CopyModelToUI(loopBackCableInformation);
  }

  private void CreateRibbonBarCommands()
  {
    RibbonHelper ribbonHelper = new RibbonHelper((IView) this);
    RibbonPageGroup ribbonPageGroup1 = ribbonHelper.CreateRibbonPageGroup("Probe");
    BarButtonItem largeImageButton1 = ribbonHelper.CreateLargeImageButton((IApplicationComponentModule) new ProbeSelectorAssistantComponentModule());
    ribbonPageGroup1.ItemLinks.Add((BarItem) largeImageButton1);
    this.ToolbarElements.Add((object) ribbonPageGroup1);
    BarButtonItem largeImageButton2 = ribbonHelper.CreateLargeImageButton("Send Probe Data", "Send Probe Configuration", string.Empty, (Bitmap) null, ServiceToolsTriggers.SendProbeConfigurationTrigger, new ItemClickEventHandler(this.OnSendProbeConfigurationClick));
    ribbonPageGroup1.ItemLinks.Add((BarItem) largeImageButton2);
    this.ToolbarElements.Add((object) ribbonPageGroup1);
    RibbonPageGroup ribbonPageGroup2 = ribbonHelper.CreateRibbonPageGroup("Instrument");
    BarButtonItem largeImageButton3 = ribbonHelper.CreateLargeImageButton((IApplicationComponentModule) new DeviceSelectorAssistantComponentModule());
    ribbonPageGroup2.ItemLinks.Add((BarItem) largeImageButton3);
    this.ToolbarElements.Add((object) ribbonPageGroup2);
    RibbonPageGroup ribbonPageGroup3 = ribbonHelper.CreateRibbonPageGroup("Firmware");
    BarButtonItem largeImageButton4 = ribbonHelper.CreateLargeImageButton((IApplicationComponentModule) new FirmwareImportAssistantComponentModule());
    ribbonPageGroup3.ItemLinks.Add((BarItem) largeImageButton4);
    this.ToolbarElements.Add((object) ribbonPageGroup3);
  }

  private void toneGeneratorButtonPressed(object sender, EventArgs e)
  {
    if (this.instrument != null && this.probeInformation != null)
    {
      if (!(sender is CheckButton))
        return;
      CheckButton checkButton = sender as CheckButton;
      if (!checkButton.Checked)
      {
        checkButton.Text = "Switch on";
        this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.StopToneGenerator, (TriggerContext) new IntrumentSelectionTriggerContext(this.instrument)));
        double frequencySpeakerCorrectionValue = double.MinValue;
        int speaker = -1;
        int frequency = 0;
        if (sender.Equals((object) this.cbSpk1_1kHz))
        {
          this.txtCurrSpk1Value1kHz.Enabled = false;
          frequencySpeakerCorrectionValue = this.GetValueAsDouble(this.txtCurrSpk1Value1kHz.Text);
          speaker = 0;
          frequency = 1000;
        }
        else if (sender.Equals((object) this.cbSpk1_2kHz))
        {
          this.txtCurrSpk1Value2kHz.Enabled = false;
          frequencySpeakerCorrectionValue = this.GetValueAsDouble(this.txtCurrSpk1Value2kHz.Text);
          speaker = 0;
          frequency = 2000;
        }
        else if (sender.Equals((object) this.cbSpk1_4kHz))
        {
          this.txtCurrSpk1Value4kHz.Enabled = false;
          frequencySpeakerCorrectionValue = this.GetValueAsDouble(this.txtCurrSpk1Value4kHz.Text);
          speaker = 0;
          frequency = 4000;
        }
        else if (sender.Equals((object) this.cbSpk1_6kHz))
        {
          this.txtCurrSpk1Value6kHz.Enabled = false;
          frequencySpeakerCorrectionValue = this.GetValueAsDouble(this.txtCurrSpk1Value6kHz.Text);
          speaker = 0;
          frequency = 6000;
        }
        else if (sender.Equals((object) this.cbSpk2_1kHz))
        {
          this.txtCurrSpk2Value1kHz.Enabled = false;
          frequencySpeakerCorrectionValue = this.GetValueAsDouble(this.txtCurrSpk2Value1kHz.Text);
          speaker = 1;
          frequency = 1000;
        }
        else if (sender.Equals((object) this.cbSpk2_2kHz))
        {
          this.txtCurrSpk2Value2kHz.Enabled = false;
          frequencySpeakerCorrectionValue = this.GetValueAsDouble(this.txtCurrSpk2Value2kHz.Text);
          speaker = 1;
          frequency = 2000;
        }
        else if (sender.Equals((object) this.cbSpk2_4kHz))
        {
          this.txtCurrSpk2Value4kHz.Enabled = false;
          frequencySpeakerCorrectionValue = this.GetValueAsDouble(this.txtCurrSpk2Value4kHz.Text);
          speaker = 1;
          frequency = 4000;
        }
        else if (sender.Equals((object) this.cbSpk2_6kHz))
        {
          this.txtCurrSpk2Value6kHz.Enabled = false;
          frequencySpeakerCorrectionValue = this.GetValueAsDouble(this.txtCurrSpk2Value6kHz.Text);
          speaker = 1;
          frequency = 6000;
        }
        if (frequencySpeakerCorrectionValue != double.MinValue && speaker > -1 && frequency > 0)
        {
          this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.ComputeCorrectionValueTrigger, (TriggerContext) new ComputeProbeCorrectionTriggerContext(this.probeInformation, speaker, frequency, frequencySpeakerCorrectionValue)));
        }
        else
        {
          int num = (int) MessageBox.Show("Please enter a correction value.");
        }
      }
      else
      {
        checkButton.Text = "Switch off";
        ToneGeneratorTriggerContext context = (ToneGeneratorTriggerContext) null;
        if (this.cbSpk1_1kHz.Checked)
        {
          this.txtCurrSpk1Value1kHz.Enabled = true;
          this.txtSpeaker1_1kHz.Enabled = true;
          context = new ToneGeneratorTriggerContext(this.instrument, 0, 1000);
        }
        else if (this.cbSpk1_2kHz.Checked)
        {
          this.txtCurrSpk1Value2kHz.Enabled = true;
          this.txtSpeaker1_2kHz.Enabled = true;
          context = new ToneGeneratorTriggerContext(this.instrument, 0, 2000);
        }
        else if (this.cbSpk1_4kHz.Checked)
        {
          this.txtCurrSpk1Value4kHz.Enabled = true;
          this.txtSpeaker1_4kHz.Enabled = true;
          context = new ToneGeneratorTriggerContext(this.instrument, 0, 4000);
        }
        else if (this.cbSpk1_6kHz.Checked)
        {
          this.txtCurrSpk1Value6kHz.Enabled = true;
          this.txtSpeaker1_6kHz.Enabled = true;
          context = new ToneGeneratorTriggerContext(this.instrument, 0, 6000);
        }
        else if (this.cbSpk2_1kHz.Checked)
        {
          this.txtCurrSpk2Value1kHz.Enabled = true;
          this.txtSpeaker2_1kHz.Enabled = true;
          context = new ToneGeneratorTriggerContext(this.instrument, 1, 1000);
        }
        else if (this.cbSpk2_2kHz.Checked)
        {
          this.txtCurrSpk2Value2kHz.Enabled = true;
          this.txtSpeaker2_2kHz.Enabled = true;
          context = new ToneGeneratorTriggerContext(this.instrument, 1, 2000);
        }
        else if (this.cbSpk2_4kHz.Checked)
        {
          this.txtCurrSpk2Value4kHz.Enabled = true;
          this.txtSpeaker2_4kHz.Enabled = true;
          context = new ToneGeneratorTriggerContext(this.instrument, 1, 4000);
        }
        else if (this.cbSpk2_6kHz.Checked)
        {
          this.txtCurrSpk2Value6kHz.Enabled = true;
          this.txtSpeaker2_6kHz.Enabled = true;
          context = new ToneGeneratorTriggerContext(this.instrument, 1, 6000);
        }
        this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.StartToneGenerator, (TriggerContext) context));
      }
    }
    else
    {
      CheckButton checkButton = sender is CheckButton ? sender as CheckButton : throw ExceptionFactory.Instance.CreateException<ModelException>(string.Format("No probe connected!"));
      if (!checkButton.Checked)
        return;
      checkButton.Checked = false;
      int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("No probe connected!", "Start Tone Generator", AnswerOptionType.OK, QuestionIcon.Information);
    }
  }

  private double GetValueAsDouble(string correctionValue)
  {
    double result = 0.0;
    return !double.TryParse(correctionValue, out result) ? double.MinValue : result;
  }

  private void bSetMic1Values_Click(object sender, EventArgs e)
  {
    if (this.instrument != null && this.probeInformation != null)
    {
      ComputeProbeRmsCorrectionTriggerContext context = new ComputeProbeRmsCorrectionTriggerContext((Instrument) this.instrument, this.probeInformation, 1000, this.GetValueAsDouble(this.txtCurrSpk1Value1kHz.Text));
      this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.ComputeMicrofoneCorrectionValueTrigger, (TriggerContext) context));
    }
    else
    {
      int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("No probe selected!", "Microphone value correction", AnswerOptionType.OK, QuestionIcon.Information);
    }
  }

  private void OnSendProbeConfigurationClick(object sender, EventArgs e)
  {
    if (this.instrument != null && this.probeInformation != null)
    {
      this.ResetCurrentSpeakerValueLevels();
      SendProbeConfigurationTriggerContext context = new SendProbeConfigurationTriggerContext((Instrument) this.instrument, this.probeInformation);
      this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.SendProbeConfigurationTrigger, (TriggerContext) context));
    }
    else
    {
      int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("No probe data available", "Send Probe Information", AnswerOptionType.OK, QuestionIcon.Information);
    }
  }

  private void ResetLoopbackTests()
  {
    this.cbCodecVoltageLevel.Checked = false;
    this.cbLoopBackTest2.Checked = false;
    this.cbLoopBackTest3.Checked = false;
    this.cbLoopBackTest4.Checked = false;
    this.cbLoopBackTest5.Checked = false;
    this.cbLoopBackTest6.Checked = false;
    this.cbLoopBackTest7.Checked = false;
  }

  private void ResetCurrentSpeakerValueLevels()
  {
    this.txtCurrSpk1Value1kHz.Text = "0";
    this.txtCurrSpk1Value2kHz.Text = "0";
    this.txtCurrSpk1Value4kHz.Text = "0";
    this.txtCurrSpk1Value6kHz.Text = "0";
    this.txtCurrSpk2Value1kHz.Text = "0";
    this.txtCurrSpk2Value2kHz.Text = "0";
    this.txtCurrSpk2Value4kHz.Text = "0";
    this.txtCurrSpk2Value6kHz.Text = "0";
  }

  private void bStartLoopBackTest_Click(object sender, EventArgs e)
  {
    if (this.instrument != null)
    {
      this.loopBackCableInformation = new Type1077LoopBackCableInformation();
      this.loopBackCableInformation.TestAbrConnection = this.cbTestAbrCanels.Checked;
      this.loopBackCableInformation.CodecOutputLevel = this.cbCodecVoltageLevel.Checked;
      LoopBackCableTriggerContext context = new LoopBackCableTriggerContext((Instrument) this.instrument, this.loopBackCableInformation);
      this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.LoopBackCableTestTrigger, (TriggerContext) context));
      this.ResetLoopbackTests();
    }
    else
    {
      int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("No instrument selected!", "Loopback Cable Test", AnswerOptionType.OK, QuestionIcon.Information);
    }
  }

  private void btnStartCodecTest1_Click(object sender, EventArgs e)
  {
    if (this.instrument != null)
    {
      ToneGeneratorTriggerContext context = new ToneGeneratorTriggerContext(this.instrument, 0, 1000);
      this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.StartToneGenerator, (TriggerContext) context));
    }
    else
    {
      int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("No instrument selected!", "Start Codec Test", AnswerOptionType.OK, QuestionIcon.Information);
    }
  }

  private void btnStopCodecTest_Click(object sender, EventArgs e)
  {
    if (this.instrument != null)
    {
      this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.StopToneGenerator, (TriggerContext) new IntrumentSelectionTriggerContext(this.instrument)));
    }
    else
    {
      int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("No instrument selected!", "Stop Codec Test", AnswerOptionType.OK, QuestionIcon.Information);
    }
  }

  private void btnDeleteFlash_Click(object sender, EventArgs e)
  {
    if (AnswerType.Yes != SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("Do you really want to delete the instrument memory?", "Delete Instrument Memory", AnswerOptionType.YesNo, QuestionIcon.Question))
      return;
    if (this.instrument != null)
    {
      DeleteDeviceMemoryTriggerContext context = new DeleteDeviceMemoryTriggerContext(this.instrument);
      this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.DeleteDeviceMemoryTrigger, (TriggerContext) context));
    }
    else
    {
      int num = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("No instrument selected!", "Delete Instrument Memory", AnswerOptionType.OK, QuestionIcon.Information);
    }
  }

  private void btnUpdateFirmware_Click(object sender, EventArgs e)
  {
    this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.UpdateFirmwareTrigger, (TriggerContext) null, false, (EventHandler<TriggerExecutedEventArgs>) null));
  }

  private void btnSelectFirmware_Click(object sender, EventArgs e)
  {
    this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.UploadFirmwareFileTrigger, (TriggerContext) null, false, (EventHandler<TriggerExecutedEventArgs>) null));
  }

  private void btnSetFwLicence_Click(object sender, EventArgs e)
  {
    if (AnswerType.Yes != SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("Do you really want to set a new firmware licence?", "Set Firmware Licence", AnswerOptionType.YesNo, QuestionIcon.Question))
      return;
    if (this.instrument != null)
    {
      if (this.tbEditFwLicence.Text.Length == 16 /*0x10*/)
      {
        SetFirmwareLicenceTriggerContext context = new SetFirmwareLicenceTriggerContext((Instrument) this.instrument, this.tbEditFwLicence.Text);
        this.RequestControllerAction((object) this, new TriggerEventArgs(ServiceToolsTriggers.SetFirmwareLicenceTrigger, (TriggerContext) context));
      }
      else
      {
        int num1 = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("Licence key to short", "Set Firmware Licence", AnswerOptionType.OK, QuestionIcon.Information);
      }
    }
    else
    {
      int num2 = (int) SystemConfigurationManager.Instance.UserInterfaceManager.ShowMessageBox("No instrument selected!", "Set Firmware Licence", AnswerOptionType.OK, QuestionIcon.Information);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.layoutControl1 = new LayoutControl();
    this.cbCodecVoltageLevel = new DevExpressCheckedEdit();
    this.btnStopCodecTest = new DevExpressButton();
    this.btnStartCodecTest1 = new DevExpressButton();
    this.btnUpdateFirmware = new DevExpressButton();
    this.tbEditFwLicence = new DevExpressTextEdit();
    this.btnDeleteFlash = new DevExpressButton();
    this.cbTestAbrCanels = new DevExpressCheckedEdit();
    this.cbLoopBackTest7 = new DevExpressCheckedEdit();
    this.cbLoopBackTest6 = new DevExpressCheckedEdit();
    this.btnSelectFirmware = new DevExpressButton();
    this.cbLoopBackTest5 = new DevExpressCheckedEdit();
    this.cbLoopBackTest4 = new DevExpressCheckedEdit();
    this.btnSetFwLicence = new DevExpressButton();
    this.cbLoopBackTest3 = new DevExpressCheckedEdit();
    this.cbLoopBackTest2 = new DevExpressCheckedEdit();
    this.txtLastCalDate = new DevExpressTextEdit();
    this.cbSpk2_6kHz = new CheckButton();
    this.cbSpk2_4kHz = new CheckButton();
    this.cbSpk2_2kHz = new CheckButton();
    this.cbSpk2_1kHz = new CheckButton();
    this.cbSpk1_6kHz = new CheckButton();
    this.cbSpk1_4kHz = new CheckButton();
    this.cbSpk1_2kHz = new CheckButton();
    this.cbSpk1_1kHz = new CheckButton();
    this.bSetMic1Values = new SimpleButton();
    this.txtCurrSpk2Value6kHz = new DevExpressTextEdit();
    this.txtCurrSpk2Value4kHz = new DevExpressTextEdit();
    this.bStartLoopBackTest = new DevExpressButton();
    this.txtCurrSpk2Value2kHz = new DevExpressTextEdit();
    this.txtCurrSpk2Value1kHz = new DevExpressTextEdit();
    this.txtCurrSpk1Value6kHz = new DevExpressTextEdit();
    this.txtCurrSpk1Value4kHz = new DevExpressTextEdit();
    this.txtCurrSpk1Value1kHz = new DevExpressTextEdit();
    this.txtSpeaker2_6kHz = new DevExpressTextEdit();
    this.txtSpeaker2_4kHz = new DevExpressTextEdit();
    this.txtSpeaker2_2kHz = new DevExpressTextEdit();
    this.txtCurrSpk1Value2kHz = new DevExpressTextEdit();
    this.txtSpeaker2_1kHz = new DevExpressTextEdit();
    this.txtMicrophoneSetting = new DevExpressTextEdit();
    this.txtSpeaker1_6kHz = new DevExpressTextEdit();
    this.txtSpeaker1_4kHz = new DevExpressTextEdit();
    this.txtSpeaker1_2kHz = new DevExpressTextEdit();
    this.txtSpeaker1_1kHz = new DevExpressTextEdit();
    this.txtProbeInformationProbeSerialNumber = new DevExpressTextEdit();
    this.txtProbeInformationProbeType = new DevExpressTextEdit();
    this.layoutControlGroup1 = new LayoutControlGroup();
    this.tabbedControlGroup3 = new TabbedControlGroup();
    this.layoutProbeConfigurationGroup = new LayoutControlGroup();
    this.layouttxtProbeInformationProbeType = new LayoutControlItem();
    this.layoutProbeInformationProbeSerialNumber = new LayoutControlItem();
    this.emptySpaceItem1 = new EmptySpaceItem();
    this.layoutLastCalibrationDate = new LayoutControlItem();
    this.layoutSpeakerSettings = new LayoutControlGroup();
    this.layoutSpeaker1_1kHz = new LayoutControlItem();
    this.layoutSpeaker1_2kHz = new LayoutControlItem();
    this.layoutSpeaker1_4kHz = new LayoutControlItem();
    this.layoutSpeaker1_6kHz = new LayoutControlItem();
    this.layoutSpeaker2_1kHz = new LayoutControlItem();
    this.layoutSpeaker2_2kHz = new LayoutControlItem();
    this.layoutSpeaker2_4kHz = new LayoutControlItem();
    this.layoutSpeaker2_6kHz = new LayoutControlItem();
    this.emptySpaceItem2 = new EmptySpaceItem();
    this.layoutMicrophoneSetting = new LayoutControlItem();
    this.emptySpaceItem4 = new EmptySpaceItem();
    this.layoutControlGroupSpk1Calculation = new LayoutControlGroup();
    this.layoutCurrSpk1Value1kHz = new LayoutControlItem();
    this.layoutCurrSpk1Value2kHz = new LayoutControlItem();
    this.layoutCurrSpk1Value4kHz = new LayoutControlItem();
    this.layoutCurrSpk1Value6kHz = new LayoutControlItem();
    this.layoutControlItem2 = new LayoutControlItem();
    this.layoutControlItem3 = new LayoutControlItem();
    this.layoutControlItem4 = new LayoutControlItem();
    this.layoutControlItem5 = new LayoutControlItem();
    this.emptySpaceItem7 = new EmptySpaceItem();
    this.layoutControlGroupCurrentSpeaker2Values = new LayoutControlGroup();
    this.layoutCurrSpk2Value1kHz = new LayoutControlItem();
    this.layoutCurrSpk2Value2kHz = new LayoutControlItem();
    this.layoutCurrSpk2Value4kHz = new LayoutControlItem();
    this.layoutCurrSpk2Value6kHz = new LayoutControlItem();
    this.emptySpaceItem5 = new EmptySpaceItem();
    this.layoutControlItem6 = new LayoutControlItem();
    this.layoutControlItem7 = new LayoutControlItem();
    this.layoutControlItem8 = new LayoutControlItem();
    this.layoutControlItem9 = new LayoutControlItem();
    this.layoutControlGroupMic1Calculation = new LayoutControlGroup();
    this.layoutSetMic1Values = new LayoutControlItem();
    this.emptySpaceItem6 = new EmptySpaceItem();
    this.emptySpaceItem3 = new EmptySpaceItem();
    this.emptySpaceItem10 = new EmptySpaceItem();
    this.layoutDevicSettingGroup = new LayoutControlGroup();
    this.layoutDeviceSettingsGroup = new LayoutControlGroup();
    this.layoutControlItem19 = new LayoutControlItem();
    this.emptySpaceItem13 = new EmptySpaceItem();
    this.layoutLoopBackCable = new LayoutControlGroup();
    this.layoutControlGroupLoopBackCableTest = new LayoutControlGroup();
    this.emptySpaceItem12 = new EmptySpaceItem();
    this.layoutControlItem18 = new LayoutControlItem();
    this.layoutControlItem10 = new LayoutControlItem();
    this.emptySpaceItem16 = new EmptySpaceItem();
    this.layoutResultGroup = new LayoutControlGroup();
    this.emptySpaceItem11 = new EmptySpaceItem();
    this.layoutControlItem13 = new LayoutControlItem();
    this.layoutControlItem11 = new LayoutControlItem();
    this.layoutControlItem14 = new LayoutControlItem();
    this.layoutControlItem15 = new LayoutControlItem();
    this.layoutControlItem16 = new LayoutControlItem();
    this.layoutControlItem17 = new LayoutControlItem();
    this.emptySpaceItem22 = new EmptySpaceItem();
    this.layoutControlGroupCodecTest = new LayoutControlGroup();
    this.layoutControlItem12 = new LayoutControlItem();
    this.btnStopCodecTest1 = new LayoutControlItem();
    this.layoutControlCodecTestResults = new LayoutControlGroup();
    this.emptySpaceItem9 = new EmptySpaceItem();
    this.layoutCodecVoltageLevel = new LayoutControlItem();
    this.emptySpaceItem23 = new EmptySpaceItem();
    this.layoutFirmwareGroup = new LayoutControlGroup();
    this.layoutControlGroupFirmware = new LayoutControlGroup();
    this.layoutControlItem20 = new LayoutControlItem();
    this.emptySpaceItem18 = new EmptySpaceItem();
    this.layoutControlItem22 = new LayoutControlItem();
    this.emptySpaceItem21 = new EmptySpaceItem();
    this.emptySpaceItem15 = new EmptySpaceItem();
    this.emptySpaceItem17 = new EmptySpaceItem();
    this.layoutControlLicence = new LayoutControlGroup();
    this.emptySpaceItem14 = new EmptySpaceItem();
    this.layoutControlItemFwLicence = new LayoutControlItem();
    this.emptySpaceItem19 = new EmptySpaceItem();
    this.emptySpaceItem20 = new EmptySpaceItem();
    this.layoutControlItem21 = new LayoutControlItem();
    this.layoutControlItem1 = new LayoutControlItem();
    this.emptySpaceItem8 = new EmptySpaceItem();
    this.layoutControl1.BeginInit();
    this.layoutControl1.SuspendLayout();
    this.cbCodecVoltageLevel.Properties.BeginInit();
    this.tbEditFwLicence.Properties.BeginInit();
    this.cbTestAbrCanels.Properties.BeginInit();
    this.cbLoopBackTest7.Properties.BeginInit();
    this.cbLoopBackTest6.Properties.BeginInit();
    this.cbLoopBackTest5.Properties.BeginInit();
    this.cbLoopBackTest4.Properties.BeginInit();
    this.cbLoopBackTest3.Properties.BeginInit();
    this.cbLoopBackTest2.Properties.BeginInit();
    this.txtLastCalDate.Properties.BeginInit();
    this.txtCurrSpk2Value6kHz.Properties.BeginInit();
    this.txtCurrSpk2Value4kHz.Properties.BeginInit();
    this.txtCurrSpk2Value2kHz.Properties.BeginInit();
    this.txtCurrSpk2Value1kHz.Properties.BeginInit();
    this.txtCurrSpk1Value6kHz.Properties.BeginInit();
    this.txtCurrSpk1Value4kHz.Properties.BeginInit();
    this.txtCurrSpk1Value1kHz.Properties.BeginInit();
    this.txtSpeaker2_6kHz.Properties.BeginInit();
    this.txtSpeaker2_4kHz.Properties.BeginInit();
    this.txtSpeaker2_2kHz.Properties.BeginInit();
    this.txtCurrSpk1Value2kHz.Properties.BeginInit();
    this.txtSpeaker2_1kHz.Properties.BeginInit();
    this.txtMicrophoneSetting.Properties.BeginInit();
    this.txtSpeaker1_6kHz.Properties.BeginInit();
    this.txtSpeaker1_4kHz.Properties.BeginInit();
    this.txtSpeaker1_2kHz.Properties.BeginInit();
    this.txtSpeaker1_1kHz.Properties.BeginInit();
    this.txtProbeInformationProbeSerialNumber.Properties.BeginInit();
    this.txtProbeInformationProbeType.Properties.BeginInit();
    this.layoutControlGroup1.BeginInit();
    this.tabbedControlGroup3.BeginInit();
    this.layoutProbeConfigurationGroup.BeginInit();
    this.layouttxtProbeInformationProbeType.BeginInit();
    this.layoutProbeInformationProbeSerialNumber.BeginInit();
    this.emptySpaceItem1.BeginInit();
    this.layoutLastCalibrationDate.BeginInit();
    this.layoutSpeakerSettings.BeginInit();
    this.layoutSpeaker1_1kHz.BeginInit();
    this.layoutSpeaker1_2kHz.BeginInit();
    this.layoutSpeaker1_4kHz.BeginInit();
    this.layoutSpeaker1_6kHz.BeginInit();
    this.layoutSpeaker2_1kHz.BeginInit();
    this.layoutSpeaker2_2kHz.BeginInit();
    this.layoutSpeaker2_4kHz.BeginInit();
    this.layoutSpeaker2_6kHz.BeginInit();
    this.emptySpaceItem2.BeginInit();
    this.layoutMicrophoneSetting.BeginInit();
    this.emptySpaceItem4.BeginInit();
    this.layoutControlGroupSpk1Calculation.BeginInit();
    this.layoutCurrSpk1Value1kHz.BeginInit();
    this.layoutCurrSpk1Value2kHz.BeginInit();
    this.layoutCurrSpk1Value4kHz.BeginInit();
    this.layoutCurrSpk1Value6kHz.BeginInit();
    this.layoutControlItem2.BeginInit();
    this.layoutControlItem3.BeginInit();
    this.layoutControlItem4.BeginInit();
    this.layoutControlItem5.BeginInit();
    this.emptySpaceItem7.BeginInit();
    this.layoutControlGroupCurrentSpeaker2Values.BeginInit();
    this.layoutCurrSpk2Value1kHz.BeginInit();
    this.layoutCurrSpk2Value2kHz.BeginInit();
    this.layoutCurrSpk2Value4kHz.BeginInit();
    this.layoutCurrSpk2Value6kHz.BeginInit();
    this.emptySpaceItem5.BeginInit();
    this.layoutControlItem6.BeginInit();
    this.layoutControlItem7.BeginInit();
    this.layoutControlItem8.BeginInit();
    this.layoutControlItem9.BeginInit();
    this.layoutControlGroupMic1Calculation.BeginInit();
    this.layoutSetMic1Values.BeginInit();
    this.emptySpaceItem6.BeginInit();
    this.emptySpaceItem3.BeginInit();
    this.emptySpaceItem10.BeginInit();
    this.layoutDevicSettingGroup.BeginInit();
    this.layoutDeviceSettingsGroup.BeginInit();
    this.layoutControlItem19.BeginInit();
    this.emptySpaceItem13.BeginInit();
    this.layoutLoopBackCable.BeginInit();
    this.layoutControlGroupLoopBackCableTest.BeginInit();
    this.emptySpaceItem12.BeginInit();
    this.layoutControlItem18.BeginInit();
    this.layoutControlItem10.BeginInit();
    this.emptySpaceItem16.BeginInit();
    this.layoutResultGroup.BeginInit();
    this.emptySpaceItem11.BeginInit();
    this.layoutControlItem13.BeginInit();
    this.layoutControlItem11.BeginInit();
    this.layoutControlItem14.BeginInit();
    this.layoutControlItem15.BeginInit();
    this.layoutControlItem16.BeginInit();
    this.layoutControlItem17.BeginInit();
    this.emptySpaceItem22.BeginInit();
    this.layoutControlGroupCodecTest.BeginInit();
    this.layoutControlItem12.BeginInit();
    this.btnStopCodecTest1.BeginInit();
    this.layoutControlCodecTestResults.BeginInit();
    this.emptySpaceItem9.BeginInit();
    this.layoutCodecVoltageLevel.BeginInit();
    this.emptySpaceItem23.BeginInit();
    this.layoutFirmwareGroup.BeginInit();
    this.layoutControlGroupFirmware.BeginInit();
    this.layoutControlItem20.BeginInit();
    this.emptySpaceItem18.BeginInit();
    this.layoutControlItem22.BeginInit();
    this.emptySpaceItem21.BeginInit();
    this.emptySpaceItem15.BeginInit();
    this.emptySpaceItem17.BeginInit();
    this.layoutControlLicence.BeginInit();
    this.emptySpaceItem14.BeginInit();
    this.layoutControlItemFwLicence.BeginInit();
    this.emptySpaceItem19.BeginInit();
    this.emptySpaceItem20.BeginInit();
    this.layoutControlItem21.BeginInit();
    this.layoutControlItem1.BeginInit();
    this.emptySpaceItem8.BeginInit();
    this.SuspendLayout();
    this.layoutControl1.Controls.Add((Control) this.cbCodecVoltageLevel);
    this.layoutControl1.Controls.Add((Control) this.btnStopCodecTest);
    this.layoutControl1.Controls.Add((Control) this.btnStartCodecTest1);
    this.layoutControl1.Controls.Add((Control) this.btnUpdateFirmware);
    this.layoutControl1.Controls.Add((Control) this.tbEditFwLicence);
    this.layoutControl1.Controls.Add((Control) this.btnDeleteFlash);
    this.layoutControl1.Controls.Add((Control) this.cbTestAbrCanels);
    this.layoutControl1.Controls.Add((Control) this.cbLoopBackTest7);
    this.layoutControl1.Controls.Add((Control) this.cbLoopBackTest6);
    this.layoutControl1.Controls.Add((Control) this.btnSelectFirmware);
    this.layoutControl1.Controls.Add((Control) this.cbLoopBackTest5);
    this.layoutControl1.Controls.Add((Control) this.cbLoopBackTest4);
    this.layoutControl1.Controls.Add((Control) this.btnSetFwLicence);
    this.layoutControl1.Controls.Add((Control) this.cbLoopBackTest3);
    this.layoutControl1.Controls.Add((Control) this.cbLoopBackTest2);
    this.layoutControl1.Controls.Add((Control) this.txtLastCalDate);
    this.layoutControl1.Controls.Add((Control) this.cbSpk2_6kHz);
    this.layoutControl1.Controls.Add((Control) this.cbSpk2_4kHz);
    this.layoutControl1.Controls.Add((Control) this.cbSpk2_2kHz);
    this.layoutControl1.Controls.Add((Control) this.cbSpk2_1kHz);
    this.layoutControl1.Controls.Add((Control) this.cbSpk1_6kHz);
    this.layoutControl1.Controls.Add((Control) this.cbSpk1_4kHz);
    this.layoutControl1.Controls.Add((Control) this.cbSpk1_2kHz);
    this.layoutControl1.Controls.Add((Control) this.cbSpk1_1kHz);
    this.layoutControl1.Controls.Add((Control) this.bSetMic1Values);
    this.layoutControl1.Controls.Add((Control) this.txtCurrSpk2Value6kHz);
    this.layoutControl1.Controls.Add((Control) this.txtCurrSpk2Value4kHz);
    this.layoutControl1.Controls.Add((Control) this.bStartLoopBackTest);
    this.layoutControl1.Controls.Add((Control) this.txtCurrSpk2Value2kHz);
    this.layoutControl1.Controls.Add((Control) this.txtCurrSpk2Value1kHz);
    this.layoutControl1.Controls.Add((Control) this.txtCurrSpk1Value6kHz);
    this.layoutControl1.Controls.Add((Control) this.txtCurrSpk1Value4kHz);
    this.layoutControl1.Controls.Add((Control) this.txtCurrSpk1Value1kHz);
    this.layoutControl1.Controls.Add((Control) this.txtSpeaker2_6kHz);
    this.layoutControl1.Controls.Add((Control) this.txtSpeaker2_4kHz);
    this.layoutControl1.Controls.Add((Control) this.txtSpeaker2_2kHz);
    this.layoutControl1.Controls.Add((Control) this.txtCurrSpk1Value2kHz);
    this.layoutControl1.Controls.Add((Control) this.txtSpeaker2_1kHz);
    this.layoutControl1.Controls.Add((Control) this.txtMicrophoneSetting);
    this.layoutControl1.Controls.Add((Control) this.txtSpeaker1_6kHz);
    this.layoutControl1.Controls.Add((Control) this.txtSpeaker1_4kHz);
    this.layoutControl1.Controls.Add((Control) this.txtSpeaker1_2kHz);
    this.layoutControl1.Controls.Add((Control) this.txtSpeaker1_1kHz);
    this.layoutControl1.Controls.Add((Control) this.txtProbeInformationProbeSerialNumber);
    this.layoutControl1.Controls.Add((Control) this.txtProbeInformationProbeType);
    this.layoutControl1.Dock = DockStyle.Fill;
    this.layoutControl1.Location = new Point(0, 0);
    this.layoutControl1.Name = "layoutControl1";
    this.layoutControl1.Root = this.layoutControlGroup1;
    this.layoutControl1.Size = new Size(890, 616);
    this.layoutControl1.TabIndex = 0;
    this.layoutControl1.Text = "layoutControl1";
    this.cbCodecVoltageLevel.Caption = (string) null;
    this.cbCodecVoltageLevel.FormatString = (string) null;
    this.cbCodecVoltageLevel.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.cbCodecVoltageLevel.IsReadOnly = false;
    this.cbCodecVoltageLevel.IsUndoing = false;
    this.cbCodecVoltageLevel.Location = new Point(48 /*0x30*/, 193);
    this.cbCodecVoltageLevel.Name = "cbCodecVoltageLevel";
    this.cbCodecVoltageLevel.Properties.Appearance.BorderColor = Color.Yellow;
    this.cbCodecVoltageLevel.Properties.Appearance.Options.UseBorderColor = true;
    this.cbCodecVoltageLevel.Properties.BorderStyle = BorderStyles.Simple;
    this.cbCodecVoltageLevel.Properties.Caption = "Codec output voltage in range";
    this.cbCodecVoltageLevel.Size = new Size(171, 21);
    this.cbCodecVoltageLevel.StyleController = (IStyleController) this.layoutControl1;
    this.cbCodecVoltageLevel.TabIndex = 58;
    this.cbCodecVoltageLevel.Validator = (ICustomValidator) null;
    this.cbCodecVoltageLevel.Value = (object) false;
    this.btnStopCodecTest.FormatString = (string) null;
    this.btnStopCodecTest.IsActive = true;
    this.btnStopCodecTest.IsMandatory = false;
    this.btnStopCodecTest.IsModified = false;
    this.btnStopCodecTest.IsNavigationOnly = false;
    this.btnStopCodecTest.IsReadOnly = false;
    this.btnStopCodecTest.IsUndoDisabled = false;
    this.btnStopCodecTest.IsUndoing = false;
    this.btnStopCodecTest.Location = new Point(36, 104);
    this.btnStopCodecTest.Name = "btnStopCodecTest";
    this.btnStopCodecTest.ShowModified = false;
    this.btnStopCodecTest.Size = new Size(195, 22);
    this.btnStopCodecTest.StyleController = (IStyleController) this.layoutControl1;
    this.btnStopCodecTest.TabIndex = 57;
    this.btnStopCodecTest.Text = "Stop Codec Test";
    this.btnStopCodecTest.UniqueModelMemberIdentifier = (MemberExpression) null;
    this.btnStopCodecTest.Validator = (ICustomValidator) null;
    this.btnStopCodecTest.Value = (object) null;
    this.btnStopCodecTest.Click += new EventHandler(this.btnStopCodecTest_Click);
    this.btnStartCodecTest1.FormatString = (string) null;
    this.btnStartCodecTest1.IsActive = true;
    this.btnStartCodecTest1.IsMandatory = false;
    this.btnStartCodecTest1.IsModified = false;
    this.btnStartCodecTest1.IsNavigationOnly = false;
    this.btnStartCodecTest1.IsReadOnly = false;
    this.btnStartCodecTest1.IsUndoDisabled = false;
    this.btnStartCodecTest1.IsUndoing = false;
    this.btnStartCodecTest1.Location = new Point(36, 78);
    this.btnStartCodecTest1.Name = "btnStartCodecTest1";
    this.btnStartCodecTest1.ShowModified = false;
    this.btnStartCodecTest1.Size = new Size(195, 22);
    this.btnStartCodecTest1.StyleController = (IStyleController) this.layoutControl1;
    this.btnStartCodecTest1.TabIndex = 56;
    this.btnStartCodecTest1.Text = "Start Codec Test";
    this.btnStartCodecTest1.UniqueModelMemberIdentifier = (MemberExpression) null;
    this.btnStartCodecTest1.Validator = (ICustomValidator) null;
    this.btnStartCodecTest1.Value = (object) null;
    this.btnStartCodecTest1.Click += new EventHandler(this.btnStartCodecTest1_Click);
    this.btnUpdateFirmware.FormatString = (string) null;
    this.btnUpdateFirmware.IsActive = true;
    this.btnUpdateFirmware.IsMandatory = false;
    this.btnUpdateFirmware.IsModified = false;
    this.btnUpdateFirmware.IsNavigationOnly = false;
    this.btnUpdateFirmware.IsReadOnly = false;
    this.btnUpdateFirmware.IsUndoDisabled = false;
    this.btnUpdateFirmware.IsUndoing = false;
    this.btnUpdateFirmware.Location = new Point(36, 78);
    this.btnUpdateFirmware.Name = "btnUpdateFirmware";
    this.btnUpdateFirmware.ShowModified = false;
    this.btnUpdateFirmware.Size = new Size(119, 22);
    this.btnUpdateFirmware.StyleController = (IStyleController) this.layoutControl1;
    this.btnUpdateFirmware.TabIndex = 55;
    this.btnUpdateFirmware.Text = "Update Firmware";
    this.btnUpdateFirmware.UniqueModelMemberIdentifier = (MemberExpression) null;
    this.btnUpdateFirmware.Validator = (ICustomValidator) null;
    this.btnUpdateFirmware.Value = (object) null;
    this.btnUpdateFirmware.Click += new EventHandler(this.btnUpdateFirmware_Click);
    this.tbEditFwLicence.Caption = (string) null;
    this.tbEditFwLicence.EditValue = (object) "";
    this.tbEditFwLicence.EnterMoveNextControl = true;
    this.tbEditFwLicence.FormatString = (string) null;
    this.tbEditFwLicence.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.tbEditFwLicence.IsReadOnly = false;
    this.tbEditFwLicence.IsUndoing = false;
    this.tbEditFwLicence.Location = new Point(151, 211);
    this.tbEditFwLicence.Name = "tbEditFwLicence";
    this.tbEditFwLicence.Properties.Appearance.BorderColor = Color.Yellow;
    this.tbEditFwLicence.Properties.Appearance.Options.UseBorderColor = true;
    this.tbEditFwLicence.Properties.BorderStyle = BorderStyles.Simple;
    this.tbEditFwLicence.Properties.Mask.BeepOnError = true;
    this.tbEditFwLicence.Properties.MaxLength = 16 /*0x10*/;
    this.tbEditFwLicence.Size = new Size(133, 20);
    this.tbEditFwLicence.StyleController = (IStyleController) this.layoutControl1;
    this.tbEditFwLicence.TabIndex = 53;
    this.tbEditFwLicence.Validator = (ICustomValidator) null;
    this.tbEditFwLicence.Value = (object) "";
    this.btnDeleteFlash.FormatString = (string) null;
    this.btnDeleteFlash.IsActive = true;
    this.btnDeleteFlash.IsMandatory = false;
    this.btnDeleteFlash.IsModified = false;
    this.btnDeleteFlash.IsNavigationOnly = false;
    this.btnDeleteFlash.IsReadOnly = false;
    this.btnDeleteFlash.IsUndoDisabled = false;
    this.btnDeleteFlash.IsUndoing = false;
    this.btnDeleteFlash.Location = new Point(36, 78);
    this.btnDeleteFlash.Name = "btnDeleteFlash";
    this.btnDeleteFlash.ShowModified = false;
    this.btnDeleteFlash.Size = new Size(139, 22);
    this.btnDeleteFlash.StyleController = (IStyleController) this.layoutControl1;
    this.btnDeleteFlash.TabIndex = 51;
    this.btnDeleteFlash.Text = "Delete Instrument Memory";
    this.btnDeleteFlash.UniqueModelMemberIdentifier = (MemberExpression) null;
    this.btnDeleteFlash.Validator = (ICustomValidator) null;
    this.btnDeleteFlash.Value = (object) null;
    this.btnDeleteFlash.Click += new EventHandler(this.btnDeleteFlash_Click);
    this.cbTestAbrCanels.Caption = (string) null;
    this.cbTestAbrCanels.FormatString = (string) null;
    this.cbTestAbrCanels.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.cbTestAbrCanels.IsReadOnly = false;
    this.cbTestAbrCanels.IsUndoing = false;
    this.cbTestAbrCanels.Location = new Point(259, 78);
    this.cbTestAbrCanels.Name = "cbTestAbrCanels";
    this.cbTestAbrCanels.Properties.Appearance.BorderColor = Color.Yellow;
    this.cbTestAbrCanels.Properties.Appearance.Options.UseBorderColor = true;
    this.cbTestAbrCanels.Properties.BorderStyle = BorderStyles.Simple;
    this.cbTestAbrCanels.Properties.Caption = "Select for  ABR canal tests";
    this.cbTestAbrCanels.Size = new Size(214, 21);
    this.cbTestAbrCanels.StyleController = (IStyleController) this.layoutControl1;
    this.cbTestAbrCanels.TabIndex = 50;
    this.cbTestAbrCanels.Validator = (ICustomValidator) null;
    this.cbTestAbrCanels.Value = (object) false;
    this.cbLoopBackTest7.Caption = (string) null;
    this.cbLoopBackTest7.FormatString = (string) null;
    this.cbLoopBackTest7.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.cbLoopBackTest7.IsReadOnly = false;
    this.cbLoopBackTest7.IsUndoing = false;
    this.cbLoopBackTest7.Location = new Point(271, 317);
    this.cbLoopBackTest7.Name = "cbLoopBackTest7";
    this.cbLoopBackTest7.Properties.Appearance.BorderColor = Color.Red;
    this.cbLoopBackTest7.Properties.Appearance.Options.UseBorderColor = true;
    this.cbLoopBackTest7.Properties.BorderStyle = BorderStyles.Simple;
    this.cbLoopBackTest7.Properties.Caption = "ABR Common mode rejection";
    this.cbLoopBackTest7.Size = new Size(190, 21);
    this.cbLoopBackTest7.StyleController = (IStyleController) this.layoutControl1;
    this.cbLoopBackTest7.TabIndex = 49;
    this.cbLoopBackTest7.Validator = (ICustomValidator) null;
    this.cbLoopBackTest7.Value = (object) false;
    this.cbLoopBackTest6.Caption = (string) null;
    this.cbLoopBackTest6.FormatString = (string) null;
    this.cbLoopBackTest6.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.cbLoopBackTest6.IsReadOnly = false;
    this.cbLoopBackTest6.IsUndoing = false;
    this.cbLoopBackTest6.Location = new Point(271, 292);
    this.cbLoopBackTest6.Name = "cbLoopBackTest6";
    this.cbLoopBackTest6.Properties.Appearance.BorderColor = Color.Red;
    this.cbLoopBackTest6.Properties.Appearance.Options.UseBorderColor = true;
    this.cbLoopBackTest6.Properties.BorderStyle = BorderStyles.Simple;
    this.cbLoopBackTest6.Properties.Caption = "Speaker 2 => ABR channel";
    this.cbLoopBackTest6.Size = new Size(190, 21);
    this.cbLoopBackTest6.StyleController = (IStyleController) this.layoutControl1;
    this.cbLoopBackTest6.TabIndex = 48 /*0x30*/;
    this.cbLoopBackTest6.Validator = (ICustomValidator) null;
    this.cbLoopBackTest6.Value = (object) false;
    this.btnSelectFirmware.FormatString = (string) null;
    this.btnSelectFirmware.IsActive = true;
    this.btnSelectFirmware.IsMandatory = false;
    this.btnSelectFirmware.IsModified = false;
    this.btnSelectFirmware.IsNavigationOnly = false;
    this.btnSelectFirmware.IsReadOnly = false;
    this.btnSelectFirmware.IsUndoDisabled = false;
    this.btnSelectFirmware.IsUndoing = false;
    this.btnSelectFirmware.Location = new Point(36, (int) sbyte.MaxValue);
    this.btnSelectFirmware.Name = "btnSelectFirmware";
    this.btnSelectFirmware.ShowModified = false;
    this.btnSelectFirmware.Size = new Size(119, 22);
    this.btnSelectFirmware.StyleController = (IStyleController) this.layoutControl1;
    this.btnSelectFirmware.TabIndex = 52;
    this.btnSelectFirmware.Text = "Upload Firmware";
    this.btnSelectFirmware.UniqueModelMemberIdentifier = (MemberExpression) null;
    this.btnSelectFirmware.Validator = (ICustomValidator) null;
    this.btnSelectFirmware.Value = (object) null;
    this.btnSelectFirmware.Click += new EventHandler(this.btnSelectFirmware_Click);
    this.cbLoopBackTest5.Caption = (string) null;
    this.cbLoopBackTest5.FormatString = (string) null;
    this.cbLoopBackTest5.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.cbLoopBackTest5.IsReadOnly = false;
    this.cbLoopBackTest5.IsUndoing = false;
    this.cbLoopBackTest5.Location = new Point(271, 267);
    this.cbLoopBackTest5.Name = "cbLoopBackTest5";
    this.cbLoopBackTest5.Properties.Appearance.BorderColor = Color.Red;
    this.cbLoopBackTest5.Properties.Appearance.Options.UseBorderColor = true;
    this.cbLoopBackTest5.Properties.BorderStyle = BorderStyles.Simple;
    this.cbLoopBackTest5.Properties.Caption = "Speaker 1 => ABR channel";
    this.cbLoopBackTest5.Size = new Size(190, 21);
    this.cbLoopBackTest5.StyleController = (IStyleController) this.layoutControl1;
    this.cbLoopBackTest5.TabIndex = 47;
    this.cbLoopBackTest5.Validator = (ICustomValidator) null;
    this.cbLoopBackTest5.Value = (object) false;
    this.cbLoopBackTest4.Caption = (string) null;
    this.cbLoopBackTest4.FormatString = (string) null;
    this.cbLoopBackTest4.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.cbLoopBackTest4.IsReadOnly = false;
    this.cbLoopBackTest4.IsUndoing = false;
    this.cbLoopBackTest4.Location = new Point(271, 242);
    this.cbLoopBackTest4.Name = "cbLoopBackTest4";
    this.cbLoopBackTest4.Properties.Appearance.BorderColor = Color.Red;
    this.cbLoopBackTest4.Properties.Appearance.Options.UseBorderColor = true;
    this.cbLoopBackTest4.Properties.BorderStyle = BorderStyles.Simple;
    this.cbLoopBackTest4.Properties.Caption = "ABR amplifier";
    this.cbLoopBackTest4.Size = new Size(190, 21);
    this.cbLoopBackTest4.StyleController = (IStyleController) this.layoutControl1;
    this.cbLoopBackTest4.TabIndex = 46;
    this.cbLoopBackTest4.Validator = (ICustomValidator) null;
    this.cbLoopBackTest4.Value = (object) false;
    this.btnSetFwLicence.FormatString = (string) null;
    this.btnSetFwLicence.IsActive = true;
    this.btnSetFwLicence.IsMandatory = false;
    this.btnSetFwLicence.IsModified = false;
    this.btnSetFwLicence.IsNavigationOnly = false;
    this.btnSetFwLicence.IsReadOnly = false;
    this.btnSetFwLicence.IsUndoDisabled = false;
    this.btnSetFwLicence.IsUndoing = false;
    this.btnSetFwLicence.Location = new Point(36, 258);
    this.btnSetFwLicence.Name = "btnSetFwLicence";
    this.btnSetFwLicence.ShowModified = false;
    this.btnSetFwLicence.Size = new Size(121, 22);
    this.btnSetFwLicence.StyleController = (IStyleController) this.layoutControl1;
    this.btnSetFwLicence.TabIndex = 54;
    this.btnSetFwLicence.Text = "Set Firmware Licence";
    this.btnSetFwLicence.UniqueModelMemberIdentifier = (MemberExpression) null;
    this.btnSetFwLicence.Validator = (ICustomValidator) null;
    this.btnSetFwLicence.Value = (object) null;
    this.btnSetFwLicence.Click += new EventHandler(this.btnSetFwLicence_Click);
    this.cbLoopBackTest3.Caption = (string) null;
    this.cbLoopBackTest3.FormatString = (string) null;
    this.cbLoopBackTest3.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.cbLoopBackTest3.IsReadOnly = false;
    this.cbLoopBackTest3.IsUndoing = false;
    this.cbLoopBackTest3.Location = new Point(271, 217);
    this.cbLoopBackTest3.Name = "cbLoopBackTest3";
    this.cbLoopBackTest3.Properties.Appearance.BorderColor = Color.Red;
    this.cbLoopBackTest3.Properties.Appearance.Options.UseBorderColor = true;
    this.cbLoopBackTest3.Properties.BorderStyle = BorderStyles.Simple;
    this.cbLoopBackTest3.Properties.Caption = "Speaker 2 => Microphone channel";
    this.cbLoopBackTest3.Size = new Size(190, 21);
    this.cbLoopBackTest3.StyleController = (IStyleController) this.layoutControl1;
    this.cbLoopBackTest3.TabIndex = 45;
    this.cbLoopBackTest3.Validator = (ICustomValidator) null;
    this.cbLoopBackTest3.Value = (object) false;
    this.cbLoopBackTest2.Caption = (string) null;
    this.cbLoopBackTest2.FormatString = (string) null;
    this.cbLoopBackTest2.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.cbLoopBackTest2.IsReadOnly = true;
    this.cbLoopBackTest2.IsUndoing = false;
    this.cbLoopBackTest2.Location = new Point(271, 192 /*0xC0*/);
    this.cbLoopBackTest2.Name = "cbLoopBackTest2";
    this.cbLoopBackTest2.Properties.Appearance.BorderColor = Color.Red;
    this.cbLoopBackTest2.Properties.Appearance.Options.UseBorderColor = true;
    this.cbLoopBackTest2.Properties.BorderStyle = BorderStyles.Simple;
    this.cbLoopBackTest2.Properties.Caption = "Speaker 1 => Microphone channel";
    this.cbLoopBackTest2.Properties.ReadOnly = true;
    this.cbLoopBackTest2.Size = new Size(190, 21);
    this.cbLoopBackTest2.StyleController = (IStyleController) this.layoutControl1;
    this.cbLoopBackTest2.TabIndex = 44;
    this.cbLoopBackTest2.Validator = (ICustomValidator) null;
    this.cbLoopBackTest2.Value = (object) false;
    this.txtLastCalDate.Caption = (string) null;
    this.txtLastCalDate.EditValue = (object) "";
    this.txtLastCalDate.EnterMoveNextControl = true;
    this.txtLastCalDate.FormatString = (string) null;
    this.txtLastCalDate.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtLastCalDate.IsReadOnly = true;
    this.txtLastCalDate.IsUndoing = false;
    this.txtLastCalDate.Location = new Point(399, 46);
    this.txtLastCalDate.Name = "txtLastCalDate";
    this.txtLastCalDate.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtLastCalDate.Properties.Appearance.Options.UseBorderColor = true;
    this.txtLastCalDate.Properties.BorderStyle = BorderStyles.Simple;
    this.txtLastCalDate.Properties.DisplayFormat.FormatString = "\"DD/MM/YYYY\"";
    this.txtLastCalDate.Properties.DisplayFormat.FormatType = FormatType.DateTime;
    this.txtLastCalDate.Properties.EditFormat.FormatString = "\"DD/MM/YYYY\"";
    this.txtLastCalDate.Properties.EditFormat.FormatType = FormatType.DateTime;
    this.txtLastCalDate.Properties.ReadOnly = true;
    this.txtLastCalDate.Size = new Size(159, 20);
    this.txtLastCalDate.StyleController = (IStyleController) this.layoutControl1;
    this.txtLastCalDate.TabIndex = 40;
    this.txtLastCalDate.Validator = (ICustomValidator) null;
    this.txtLastCalDate.Value = (object) "";
    this.cbSpk2_6kHz.Location = new Point(388, 416);
    this.cbSpk2_6kHz.Name = "cbSpk2_6kHz";
    this.cbSpk2_6kHz.Size = new Size(113, 22);
    this.cbSpk2_6kHz.StyleController = (IStyleController) this.layoutControl1;
    this.cbSpk2_6kHz.TabIndex = 30;
    this.cbSpk2_6kHz.Text = "Switch on";
    this.cbSpk2_6kHz.CheckedChanged += new EventHandler(this.toneGeneratorButtonPressed);
    this.cbSpk2_4kHz.Location = new Point(270, 416);
    this.cbSpk2_4kHz.Name = "cbSpk2_4kHz";
    this.cbSpk2_4kHz.Size = new Size(114, 22);
    this.cbSpk2_4kHz.StyleController = (IStyleController) this.layoutControl1;
    this.cbSpk2_4kHz.TabIndex = 29;
    this.cbSpk2_4kHz.Text = "Switch on";
    this.cbSpk2_4kHz.CheckedChanged += new EventHandler(this.toneGeneratorButtonPressed);
    this.cbSpk2_2kHz.Location = new Point(153, 416);
    this.cbSpk2_2kHz.Name = "cbSpk2_2kHz";
    this.cbSpk2_2kHz.Size = new Size(113, 22);
    this.cbSpk2_2kHz.StyleController = (IStyleController) this.layoutControl1;
    this.cbSpk2_2kHz.TabIndex = 28;
    this.cbSpk2_2kHz.Text = "Switch on";
    this.cbSpk2_2kHz.CheckedChanged += new EventHandler(this.toneGeneratorButtonPressed);
    this.cbSpk2_1kHz.Location = new Point(36, 416);
    this.cbSpk2_1kHz.Name = "cbSpk2_1kHz";
    this.cbSpk2_1kHz.Size = new Size(113, 22);
    this.cbSpk2_1kHz.StyleController = (IStyleController) this.layoutControl1;
    this.cbSpk2_1kHz.TabIndex = 27;
    this.cbSpk2_1kHz.Text = "Switch on";
    this.cbSpk2_1kHz.CheckedChanged += new EventHandler(this.toneGeneratorButtonPressed);
    this.cbSpk1_6kHz.Location = new Point(388, 306);
    this.cbSpk1_6kHz.Name = "cbSpk1_6kHz";
    this.cbSpk1_6kHz.Size = new Size(113, 22);
    this.cbSpk1_6kHz.StyleController = (IStyleController) this.layoutControl1;
    this.cbSpk1_6kHz.TabIndex = 26;
    this.cbSpk1_6kHz.Text = "Switch on";
    this.cbSpk1_6kHz.CheckedChanged += new EventHandler(this.toneGeneratorButtonPressed);
    this.cbSpk1_4kHz.Location = new Point(270, 306);
    this.cbSpk1_4kHz.Name = "cbSpk1_4kHz";
    this.cbSpk1_4kHz.Size = new Size(114, 22);
    this.cbSpk1_4kHz.StyleController = (IStyleController) this.layoutControl1;
    this.cbSpk1_4kHz.TabIndex = 25;
    this.cbSpk1_4kHz.Text = "Switch on";
    this.cbSpk1_4kHz.CheckedChanged += new EventHandler(this.toneGeneratorButtonPressed);
    this.cbSpk1_2kHz.Location = new Point(153, 306);
    this.cbSpk1_2kHz.Name = "cbSpk1_2kHz";
    this.cbSpk1_2kHz.Size = new Size(113, 22);
    this.cbSpk1_2kHz.StyleController = (IStyleController) this.layoutControl1;
    this.cbSpk1_2kHz.TabIndex = 24;
    this.cbSpk1_2kHz.Text = "Switch on";
    this.cbSpk1_2kHz.CheckedChanged += new EventHandler(this.toneGeneratorButtonPressed);
    this.cbSpk1_1kHz.Location = new Point(36, 306);
    this.cbSpk1_1kHz.Name = "cbSpk1_1kHz";
    this.cbSpk1_1kHz.Size = new Size(113, 22);
    this.cbSpk1_1kHz.StyleController = (IStyleController) this.layoutControl1;
    this.cbSpk1_1kHz.TabIndex = 23;
    this.cbSpk1_1kHz.Text = "Switch on";
    this.cbSpk1_1kHz.CheckedChanged += new EventHandler(this.toneGeneratorButtonPressed);
    this.bSetMic1Values.Location = new Point(36, 486);
    this.bSetMic1Values.Name = "bSetMic1Values";
    this.bSetMic1Values.Size = new Size(171, 22);
    this.bSetMic1Values.StyleController = (IStyleController) this.layoutControl1;
    this.bSetMic1Values.TabIndex = 39;
    this.bSetMic1Values.Text = "Calculate Microfone Value";
    this.bSetMic1Values.Click += new EventHandler(this.bSetMic1Values_Click);
    this.txtCurrSpk2Value6kHz.Caption = (string) null;
    this.txtCurrSpk2Value6kHz.EditValue = (object) "0,00";
    this.txtCurrSpk2Value6kHz.Enabled = false;
    this.txtCurrSpk2Value6kHz.EnterMoveNextControl = true;
    this.txtCurrSpk2Value6kHz.FormatString = (string) null;
    this.txtCurrSpk2Value6kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtCurrSpk2Value6kHz.IsReadOnly = false;
    this.txtCurrSpk2Value6kHz.IsUndoing = false;
    this.txtCurrSpk2Value6kHz.Location = new Point(388, 392);
    this.txtCurrSpk2Value6kHz.Name = "txtCurrSpk2Value6kHz";
    this.txtCurrSpk2Value6kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtCurrSpk2Value6kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtCurrSpk2Value6kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtCurrSpk2Value6kHz.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk2Value6kHz.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk2Value6kHz.Properties.Mask.BeepOnError = true;
    this.txtCurrSpk2Value6kHz.Properties.Mask.EditMask = "n2";
    this.txtCurrSpk2Value6kHz.Properties.Mask.MaskType = MaskType.Numeric;
    this.txtCurrSpk2Value6kHz.Properties.Mask.UseMaskAsDisplayFormat = true;
    this.txtCurrSpk2Value6kHz.Size = new Size(113, 20);
    this.txtCurrSpk2Value6kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtCurrSpk2Value6kHz.TabIndex = 22;
    this.txtCurrSpk2Value6kHz.Validator = (ICustomValidator) null;
    this.txtCurrSpk2Value6kHz.Value = (object) "0,00";
    this.txtCurrSpk2Value4kHz.Caption = (string) null;
    this.txtCurrSpk2Value4kHz.EditValue = (object) "0,00";
    this.txtCurrSpk2Value4kHz.Enabled = false;
    this.txtCurrSpk2Value4kHz.EnterMoveNextControl = true;
    this.txtCurrSpk2Value4kHz.FormatString = (string) null;
    this.txtCurrSpk2Value4kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtCurrSpk2Value4kHz.IsReadOnly = false;
    this.txtCurrSpk2Value4kHz.IsUndoing = false;
    this.txtCurrSpk2Value4kHz.Location = new Point(270, 392);
    this.txtCurrSpk2Value4kHz.Name = "txtCurrSpk2Value4kHz";
    this.txtCurrSpk2Value4kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtCurrSpk2Value4kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtCurrSpk2Value4kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtCurrSpk2Value4kHz.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk2Value4kHz.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk2Value4kHz.Properties.Mask.BeepOnError = true;
    this.txtCurrSpk2Value4kHz.Properties.Mask.EditMask = "n2";
    this.txtCurrSpk2Value4kHz.Properties.Mask.MaskType = MaskType.Numeric;
    this.txtCurrSpk2Value4kHz.Properties.Mask.UseMaskAsDisplayFormat = true;
    this.txtCurrSpk2Value4kHz.Size = new Size(114, 20);
    this.txtCurrSpk2Value4kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtCurrSpk2Value4kHz.TabIndex = 21;
    this.txtCurrSpk2Value4kHz.Validator = (ICustomValidator) null;
    this.txtCurrSpk2Value4kHz.Value = (object) "0,00";
    this.bStartLoopBackTest.FormatString = (string) null;
    this.bStartLoopBackTest.IsActive = true;
    this.bStartLoopBackTest.IsMandatory = false;
    this.bStartLoopBackTest.IsModified = false;
    this.bStartLoopBackTest.IsNavigationOnly = false;
    this.bStartLoopBackTest.IsReadOnly = false;
    this.bStartLoopBackTest.IsUndoDisabled = false;
    this.bStartLoopBackTest.IsUndoing = false;
    this.bStartLoopBackTest.Location = new Point(259, 103);
    this.bStartLoopBackTest.Name = "bStartLoopBackTest";
    this.bStartLoopBackTest.ShowModified = false;
    this.bStartLoopBackTest.Size = new Size(214, 22);
    this.bStartLoopBackTest.StyleController = (IStyleController) this.layoutControl1;
    this.bStartLoopBackTest.TabIndex = 41;
    this.bStartLoopBackTest.Text = "Start Loopback Cable Test";
    this.bStartLoopBackTest.UniqueModelMemberIdentifier = (MemberExpression) null;
    this.bStartLoopBackTest.Validator = (ICustomValidator) null;
    this.bStartLoopBackTest.Value = (object) null;
    this.bStartLoopBackTest.Click += new EventHandler(this.bStartLoopBackTest_Click);
    this.txtCurrSpk2Value2kHz.Caption = (string) null;
    this.txtCurrSpk2Value2kHz.EditValue = (object) "0,00";
    this.txtCurrSpk2Value2kHz.Enabled = false;
    this.txtCurrSpk2Value2kHz.EnterMoveNextControl = true;
    this.txtCurrSpk2Value2kHz.FormatString = (string) null;
    this.txtCurrSpk2Value2kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtCurrSpk2Value2kHz.IsReadOnly = false;
    this.txtCurrSpk2Value2kHz.IsUndoing = false;
    this.txtCurrSpk2Value2kHz.Location = new Point(153, 392);
    this.txtCurrSpk2Value2kHz.Name = "txtCurrSpk2Value2kHz";
    this.txtCurrSpk2Value2kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtCurrSpk2Value2kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtCurrSpk2Value2kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtCurrSpk2Value2kHz.Properties.DisplayFormat.FormatString = "n2";
    this.txtCurrSpk2Value2kHz.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk2Value2kHz.Properties.EditFormat.FormatString = "n2";
    this.txtCurrSpk2Value2kHz.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk2Value2kHz.Properties.Mask.BeepOnError = true;
    this.txtCurrSpk2Value2kHz.Properties.Mask.EditMask = "n2";
    this.txtCurrSpk2Value2kHz.Properties.Mask.MaskType = MaskType.Numeric;
    this.txtCurrSpk2Value2kHz.Properties.Mask.UseMaskAsDisplayFormat = true;
    this.txtCurrSpk2Value2kHz.Size = new Size(113, 20);
    this.txtCurrSpk2Value2kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtCurrSpk2Value2kHz.TabIndex = 20;
    this.txtCurrSpk2Value2kHz.Validator = (ICustomValidator) null;
    this.txtCurrSpk2Value2kHz.Value = (object) "0,00";
    this.txtCurrSpk2Value1kHz.Caption = (string) null;
    this.txtCurrSpk2Value1kHz.EditValue = (object) "0,00";
    this.txtCurrSpk2Value1kHz.Enabled = false;
    this.txtCurrSpk2Value1kHz.EnterMoveNextControl = true;
    this.txtCurrSpk2Value1kHz.FormatString = (string) null;
    this.txtCurrSpk2Value1kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtCurrSpk2Value1kHz.IsReadOnly = false;
    this.txtCurrSpk2Value1kHz.IsUndoing = false;
    this.txtCurrSpk2Value1kHz.Location = new Point(36, 392);
    this.txtCurrSpk2Value1kHz.Name = "txtCurrSpk2Value1kHz";
    this.txtCurrSpk2Value1kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtCurrSpk2Value1kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtCurrSpk2Value1kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtCurrSpk2Value1kHz.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk2Value1kHz.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk2Value1kHz.Properties.Mask.BeepOnError = true;
    this.txtCurrSpk2Value1kHz.Properties.Mask.EditMask = "n2";
    this.txtCurrSpk2Value1kHz.Properties.Mask.MaskType = MaskType.Numeric;
    this.txtCurrSpk2Value1kHz.Properties.Mask.UseMaskAsDisplayFormat = true;
    this.txtCurrSpk2Value1kHz.Size = new Size(113, 20);
    this.txtCurrSpk2Value1kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtCurrSpk2Value1kHz.TabIndex = 19;
    this.txtCurrSpk2Value1kHz.Validator = (ICustomValidator) null;
    this.txtCurrSpk2Value1kHz.Value = (object) "0,00";
    this.txtCurrSpk1Value6kHz.Caption = (string) null;
    this.txtCurrSpk1Value6kHz.EditValue = (object) "0,00";
    this.txtCurrSpk1Value6kHz.Enabled = false;
    this.txtCurrSpk1Value6kHz.EnterMoveNextControl = true;
    this.txtCurrSpk1Value6kHz.FormatString = (string) null;
    this.txtCurrSpk1Value6kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtCurrSpk1Value6kHz.IsReadOnly = false;
    this.txtCurrSpk1Value6kHz.IsUndoing = false;
    this.txtCurrSpk1Value6kHz.Location = new Point(388, 282);
    this.txtCurrSpk1Value6kHz.Name = "txtCurrSpk1Value6kHz";
    this.txtCurrSpk1Value6kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtCurrSpk1Value6kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtCurrSpk1Value6kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtCurrSpk1Value6kHz.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk1Value6kHz.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk1Value6kHz.Properties.Mask.BeepOnError = true;
    this.txtCurrSpk1Value6kHz.Properties.Mask.EditMask = "n2";
    this.txtCurrSpk1Value6kHz.Properties.Mask.MaskType = MaskType.Numeric;
    this.txtCurrSpk1Value6kHz.Properties.Mask.UseMaskAsDisplayFormat = true;
    this.txtCurrSpk1Value6kHz.Size = new Size(113, 20);
    this.txtCurrSpk1Value6kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtCurrSpk1Value6kHz.TabIndex = 18;
    this.txtCurrSpk1Value6kHz.Validator = (ICustomValidator) null;
    this.txtCurrSpk1Value6kHz.Value = (object) "0,00";
    this.txtCurrSpk1Value4kHz.Caption = (string) null;
    this.txtCurrSpk1Value4kHz.EditValue = (object) "0,00";
    this.txtCurrSpk1Value4kHz.Enabled = false;
    this.txtCurrSpk1Value4kHz.EnterMoveNextControl = true;
    this.txtCurrSpk1Value4kHz.FormatString = (string) null;
    this.txtCurrSpk1Value4kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtCurrSpk1Value4kHz.IsReadOnly = false;
    this.txtCurrSpk1Value4kHz.IsUndoing = false;
    this.txtCurrSpk1Value4kHz.Location = new Point(270, 282);
    this.txtCurrSpk1Value4kHz.Name = "txtCurrSpk1Value4kHz";
    this.txtCurrSpk1Value4kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtCurrSpk1Value4kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtCurrSpk1Value4kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtCurrSpk1Value4kHz.Properties.DisplayFormat.FormatString = "n2";
    this.txtCurrSpk1Value4kHz.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk1Value4kHz.Properties.EditFormat.FormatString = "n2";
    this.txtCurrSpk1Value4kHz.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk1Value4kHz.Properties.Mask.BeepOnError = true;
    this.txtCurrSpk1Value4kHz.Properties.Mask.EditMask = "n2";
    this.txtCurrSpk1Value4kHz.Properties.Mask.MaskType = MaskType.Numeric;
    this.txtCurrSpk1Value4kHz.Properties.Mask.UseMaskAsDisplayFormat = true;
    this.txtCurrSpk1Value4kHz.Size = new Size(114, 20);
    this.txtCurrSpk1Value4kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtCurrSpk1Value4kHz.TabIndex = 17;
    this.txtCurrSpk1Value4kHz.Validator = (ICustomValidator) null;
    this.txtCurrSpk1Value4kHz.Value = (object) "0,00";
    this.txtCurrSpk1Value1kHz.Caption = (string) null;
    this.txtCurrSpk1Value1kHz.EditValue = (object) "0,00";
    this.txtCurrSpk1Value1kHz.Enabled = false;
    this.txtCurrSpk1Value1kHz.EnterMoveNextControl = true;
    this.txtCurrSpk1Value1kHz.FormatString = (string) null;
    this.txtCurrSpk1Value1kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtCurrSpk1Value1kHz.IsReadOnly = false;
    this.txtCurrSpk1Value1kHz.IsUndoing = false;
    this.txtCurrSpk1Value1kHz.Location = new Point(36, 282);
    this.txtCurrSpk1Value1kHz.Name = "txtCurrSpk1Value1kHz";
    this.txtCurrSpk1Value1kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtCurrSpk1Value1kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtCurrSpk1Value1kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtCurrSpk1Value1kHz.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk1Value1kHz.Properties.EditFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk1Value1kHz.Properties.Mask.BeepOnError = true;
    this.txtCurrSpk1Value1kHz.Properties.Mask.EditMask = "n2";
    this.txtCurrSpk1Value1kHz.Properties.Mask.MaskType = MaskType.Numeric;
    this.txtCurrSpk1Value1kHz.Properties.Mask.UseMaskAsDisplayFormat = true;
    this.txtCurrSpk1Value1kHz.Size = new Size(113, 20);
    this.txtCurrSpk1Value1kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtCurrSpk1Value1kHz.TabIndex = 15;
    this.txtCurrSpk1Value1kHz.Validator = (ICustomValidator) null;
    this.txtCurrSpk1Value1kHz.Value = (object) "0,00";
    this.txtSpeaker2_6kHz.Caption = (string) null;
    this.txtSpeaker2_6kHz.EditValue = (object) "";
    this.txtSpeaker2_6kHz.EnterMoveNextControl = true;
    this.txtSpeaker2_6kHz.FormatString = (string) null;
    this.txtSpeaker2_6kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtSpeaker2_6kHz.IsReadOnly = true;
    this.txtSpeaker2_6kHz.IsUndoing = false;
    this.txtSpeaker2_6kHz.Location = new Point(320, 198);
    this.txtSpeaker2_6kHz.Name = "txtSpeaker2_6kHz";
    this.txtSpeaker2_6kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtSpeaker2_6kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtSpeaker2_6kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtSpeaker2_6kHz.Properties.ReadOnly = true;
    this.txtSpeaker2_6kHz.Size = new Size(50, 20);
    this.txtSpeaker2_6kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtSpeaker2_6kHz.TabIndex = 13;
    this.txtSpeaker2_6kHz.Validator = (ICustomValidator) null;
    this.txtSpeaker2_6kHz.Value = (object) "";
    this.txtSpeaker2_4kHz.Caption = (string) null;
    this.txtSpeaker2_4kHz.EditValue = (object) "";
    this.txtSpeaker2_4kHz.EnterMoveNextControl = true;
    this.txtSpeaker2_4kHz.FormatString = (string) null;
    this.txtSpeaker2_4kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtSpeaker2_4kHz.IsReadOnly = true;
    this.txtSpeaker2_4kHz.IsUndoing = false;
    this.txtSpeaker2_4kHz.Location = new Point(320, 174);
    this.txtSpeaker2_4kHz.Name = "txtSpeaker2_4kHz";
    this.txtSpeaker2_4kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtSpeaker2_4kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtSpeaker2_4kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtSpeaker2_4kHz.Properties.ReadOnly = true;
    this.txtSpeaker2_4kHz.Size = new Size(50, 20);
    this.txtSpeaker2_4kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtSpeaker2_4kHz.TabIndex = 12;
    this.txtSpeaker2_4kHz.Validator = (ICustomValidator) null;
    this.txtSpeaker2_4kHz.Value = (object) "";
    this.txtSpeaker2_2kHz.Caption = (string) null;
    this.txtSpeaker2_2kHz.EditValue = (object) "";
    this.txtSpeaker2_2kHz.EnterMoveNextControl = true;
    this.txtSpeaker2_2kHz.FormatString = (string) null;
    this.txtSpeaker2_2kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtSpeaker2_2kHz.IsReadOnly = true;
    this.txtSpeaker2_2kHz.IsUndoing = false;
    this.txtSpeaker2_2kHz.Location = new Point(320, 150);
    this.txtSpeaker2_2kHz.Name = "txtSpeaker2_2kHz";
    this.txtSpeaker2_2kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtSpeaker2_2kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtSpeaker2_2kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtSpeaker2_2kHz.Properties.ReadOnly = true;
    this.txtSpeaker2_2kHz.Size = new Size(50, 20);
    this.txtSpeaker2_2kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtSpeaker2_2kHz.TabIndex = 11;
    this.txtSpeaker2_2kHz.Validator = (ICustomValidator) null;
    this.txtSpeaker2_2kHz.Value = (object) "";
    this.txtCurrSpk1Value2kHz.Caption = (string) null;
    this.txtCurrSpk1Value2kHz.EditValue = (object) "0,00";
    this.txtCurrSpk1Value2kHz.Enabled = false;
    this.txtCurrSpk1Value2kHz.EnterMoveNextControl = true;
    this.txtCurrSpk1Value2kHz.FormatString = (string) null;
    this.txtCurrSpk1Value2kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtCurrSpk1Value2kHz.IsReadOnly = false;
    this.txtCurrSpk1Value2kHz.IsUndoing = false;
    this.txtCurrSpk1Value2kHz.Location = new Point(153, 282);
    this.txtCurrSpk1Value2kHz.Name = "txtCurrSpk1Value2kHz";
    this.txtCurrSpk1Value2kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtCurrSpk1Value2kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtCurrSpk1Value2kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtCurrSpk1Value2kHz.Properties.DisplayFormat.FormatType = FormatType.Numeric;
    this.txtCurrSpk1Value2kHz.Properties.EditFormat.FormatString = "d";
    this.txtCurrSpk1Value2kHz.Properties.EditFormat.FormatType = FormatType.DateTime;
    this.txtCurrSpk1Value2kHz.Properties.Mask.BeepOnError = true;
    this.txtCurrSpk1Value2kHz.Properties.Mask.EditMask = "n2";
    this.txtCurrSpk1Value2kHz.Properties.Mask.MaskType = MaskType.Numeric;
    this.txtCurrSpk1Value2kHz.Properties.Mask.UseMaskAsDisplayFormat = true;
    this.txtCurrSpk1Value2kHz.Size = new Size(113, 20);
    this.txtCurrSpk1Value2kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtCurrSpk1Value2kHz.TabIndex = 16 /*0x10*/;
    this.txtCurrSpk1Value2kHz.Validator = (ICustomValidator) null;
    this.txtCurrSpk1Value2kHz.Value = (object) "0,00";
    this.txtSpeaker2_1kHz.Caption = (string) null;
    this.txtSpeaker2_1kHz.EditValue = (object) "";
    this.txtSpeaker2_1kHz.EnterMoveNextControl = true;
    this.txtSpeaker2_1kHz.FormatString = (string) null;
    this.txtSpeaker2_1kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtSpeaker2_1kHz.IsReadOnly = true;
    this.txtSpeaker2_1kHz.IsUndoing = false;
    this.txtSpeaker2_1kHz.Location = new Point(320, 126);
    this.txtSpeaker2_1kHz.Name = "txtSpeaker2_1kHz";
    this.txtSpeaker2_1kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtSpeaker2_1kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtSpeaker2_1kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtSpeaker2_1kHz.Properties.ReadOnly = true;
    this.txtSpeaker2_1kHz.Size = new Size(50, 20);
    this.txtSpeaker2_1kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtSpeaker2_1kHz.TabIndex = 10;
    this.txtSpeaker2_1kHz.Validator = (ICustomValidator) null;
    this.txtSpeaker2_1kHz.Value = (object) "";
    this.txtMicrophoneSetting.Caption = (string) null;
    this.txtMicrophoneSetting.EditValue = (object) "";
    this.txtMicrophoneSetting.EnterMoveNextControl = true;
    this.txtMicrophoneSetting.FormatString = (string) null;
    this.txtMicrophoneSetting.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtMicrophoneSetting.IsReadOnly = true;
    this.txtMicrophoneSetting.IsUndoing = false;
    this.txtMicrophoneSetting.Location = new Point(489, 126);
    this.txtMicrophoneSetting.Name = "txtMicrophoneSetting";
    this.txtMicrophoneSetting.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtMicrophoneSetting.Properties.Appearance.Options.UseBorderColor = true;
    this.txtMicrophoneSetting.Properties.BorderStyle = BorderStyles.Simple;
    this.txtMicrophoneSetting.Properties.ReadOnly = true;
    this.txtMicrophoneSetting.Size = new Size(50, 20);
    this.txtMicrophoneSetting.StyleController = (IStyleController) this.layoutControl1;
    this.txtMicrophoneSetting.TabIndex = 14;
    this.txtMicrophoneSetting.Validator = (ICustomValidator) null;
    this.txtMicrophoneSetting.Value = (object) "";
    this.txtSpeaker1_6kHz.Caption = (string) null;
    this.txtSpeaker1_6kHz.EditValue = (object) "";
    this.txtSpeaker1_6kHz.EnterMoveNextControl = true;
    this.txtSpeaker1_6kHz.FormatString = (string) null;
    this.txtSpeaker1_6kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtSpeaker1_6kHz.IsReadOnly = true;
    this.txtSpeaker1_6kHz.IsUndoing = false;
    this.txtSpeaker1_6kHz.Location = new Point(151, 198);
    this.txtSpeaker1_6kHz.Name = "txtSpeaker1_6kHz";
    this.txtSpeaker1_6kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtSpeaker1_6kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtSpeaker1_6kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtSpeaker1_6kHz.Properties.ReadOnly = true;
    this.txtSpeaker1_6kHz.Size = new Size(50, 20);
    this.txtSpeaker1_6kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtSpeaker1_6kHz.TabIndex = 9;
    this.txtSpeaker1_6kHz.Validator = (ICustomValidator) null;
    this.txtSpeaker1_6kHz.Value = (object) "";
    this.txtSpeaker1_4kHz.Caption = (string) null;
    this.txtSpeaker1_4kHz.EditValue = (object) "";
    this.txtSpeaker1_4kHz.EnterMoveNextControl = true;
    this.txtSpeaker1_4kHz.FormatString = (string) null;
    this.txtSpeaker1_4kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtSpeaker1_4kHz.IsReadOnly = true;
    this.txtSpeaker1_4kHz.IsUndoing = false;
    this.txtSpeaker1_4kHz.Location = new Point(151, 174);
    this.txtSpeaker1_4kHz.Name = "txtSpeaker1_4kHz";
    this.txtSpeaker1_4kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtSpeaker1_4kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtSpeaker1_4kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtSpeaker1_4kHz.Properties.ReadOnly = true;
    this.txtSpeaker1_4kHz.Size = new Size(50, 20);
    this.txtSpeaker1_4kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtSpeaker1_4kHz.TabIndex = 8;
    this.txtSpeaker1_4kHz.Validator = (ICustomValidator) null;
    this.txtSpeaker1_4kHz.Value = (object) "";
    this.txtSpeaker1_2kHz.Caption = (string) null;
    this.txtSpeaker1_2kHz.EditValue = (object) "";
    this.txtSpeaker1_2kHz.EnterMoveNextControl = true;
    this.txtSpeaker1_2kHz.FormatString = (string) null;
    this.txtSpeaker1_2kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtSpeaker1_2kHz.IsReadOnly = true;
    this.txtSpeaker1_2kHz.IsUndoing = false;
    this.txtSpeaker1_2kHz.Location = new Point(151, 150);
    this.txtSpeaker1_2kHz.Name = "txtSpeaker1_2kHz";
    this.txtSpeaker1_2kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtSpeaker1_2kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtSpeaker1_2kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtSpeaker1_2kHz.Properties.ReadOnly = true;
    this.txtSpeaker1_2kHz.Size = new Size(50, 20);
    this.txtSpeaker1_2kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtSpeaker1_2kHz.TabIndex = 7;
    this.txtSpeaker1_2kHz.Validator = (ICustomValidator) null;
    this.txtSpeaker1_2kHz.Value = (object) "";
    this.txtSpeaker1_1kHz.Caption = (string) null;
    this.txtSpeaker1_1kHz.EditValue = (object) "";
    this.txtSpeaker1_1kHz.EnterMoveNextControl = true;
    this.txtSpeaker1_1kHz.FormatString = (string) null;
    this.txtSpeaker1_1kHz.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtSpeaker1_1kHz.IsReadOnly = true;
    this.txtSpeaker1_1kHz.IsUndoing = false;
    this.txtSpeaker1_1kHz.Location = new Point(151, 126);
    this.txtSpeaker1_1kHz.Name = "txtSpeaker1_1kHz";
    this.txtSpeaker1_1kHz.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtSpeaker1_1kHz.Properties.Appearance.Options.UseBorderColor = true;
    this.txtSpeaker1_1kHz.Properties.BorderStyle = BorderStyles.Simple;
    this.txtSpeaker1_1kHz.Properties.ReadOnly = true;
    this.txtSpeaker1_1kHz.Size = new Size(50, 20);
    this.txtSpeaker1_1kHz.StyleController = (IStyleController) this.layoutControl1;
    this.txtSpeaker1_1kHz.TabIndex = 6;
    this.txtSpeaker1_1kHz.Validator = (ICustomValidator) null;
    this.txtSpeaker1_1kHz.Value = (object) "";
    this.txtProbeInformationProbeSerialNumber.Caption = (string) null;
    this.txtProbeInformationProbeSerialNumber.EditValue = (object) "";
    this.txtProbeInformationProbeSerialNumber.EnterMoveNextControl = true;
    this.txtProbeInformationProbeSerialNumber.FormatString = (string) null;
    this.txtProbeInformationProbeSerialNumber.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtProbeInformationProbeSerialNumber.IsReadOnly = true;
    this.txtProbeInformationProbeSerialNumber.IsUndoing = false;
    this.txtProbeInformationProbeSerialNumber.Location = new Point(139, 70);
    this.txtProbeInformationProbeSerialNumber.Name = "txtProbeInformationProbeSerialNumber";
    this.txtProbeInformationProbeSerialNumber.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtProbeInformationProbeSerialNumber.Properties.Appearance.Options.UseBorderColor = true;
    this.txtProbeInformationProbeSerialNumber.Properties.BorderStyle = BorderStyles.Simple;
    this.txtProbeInformationProbeSerialNumber.Properties.ReadOnly = true;
    this.txtProbeInformationProbeSerialNumber.Size = new Size(141, 20);
    this.txtProbeInformationProbeSerialNumber.StyleController = (IStyleController) this.layoutControl1;
    this.txtProbeInformationProbeSerialNumber.TabIndex = 5;
    this.txtProbeInformationProbeSerialNumber.Validator = (ICustomValidator) null;
    this.txtProbeInformationProbeSerialNumber.Value = (object) "";
    this.txtProbeInformationProbeType.Caption = (string) null;
    this.txtProbeInformationProbeType.EditValue = (object) "";
    this.txtProbeInformationProbeType.EnterMoveNextControl = true;
    this.txtProbeInformationProbeType.FormatString = (string) null;
    this.txtProbeInformationProbeType.Id = new Guid("00000000-0000-0000-0000-000000000000");
    this.txtProbeInformationProbeType.IsReadOnly = true;
    this.txtProbeInformationProbeType.IsUndoing = false;
    this.txtProbeInformationProbeType.Location = new Point(139, 46);
    this.txtProbeInformationProbeType.Name = "txtProbeInformationProbeType";
    this.txtProbeInformationProbeType.Properties.Appearance.BorderColor = Color.Yellow;
    this.txtProbeInformationProbeType.Properties.Appearance.Options.UseBorderColor = true;
    this.txtProbeInformationProbeType.Properties.BorderStyle = BorderStyles.Simple;
    this.txtProbeInformationProbeType.Properties.ReadOnly = true;
    this.txtProbeInformationProbeType.Size = new Size(141, 20);
    this.txtProbeInformationProbeType.StyleController = (IStyleController) this.layoutControl1;
    this.txtProbeInformationProbeType.TabIndex = 4;
    this.txtProbeInformationProbeType.Validator = (ICustomValidator) null;
    this.txtProbeInformationProbeType.Value = (object) "";
    this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
    this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
    this.layoutControlGroup1.GroupBordersVisible = false;
    this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.tabbedControlGroup3
    });
    this.layoutControlGroup1.Location = new Point(0, 0);
    this.layoutControlGroup1.Name = "layoutControlGroup1";
    this.layoutControlGroup1.Size = new Size(890, 616);
    this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
    this.layoutControlGroup1.Text = "layoutControlGroup1";
    this.layoutControlGroup1.TextVisible = false;
    this.tabbedControlGroup3.CustomizationFormText = "tabbedControlGroup3";
    this.tabbedControlGroup3.Location = new Point(0, 0);
    this.tabbedControlGroup3.Name = "tabbedControlGroup3";
    this.tabbedControlGroup3.SelectedTabPage = (LayoutGroup) this.layoutLoopBackCable;
    this.tabbedControlGroup3.SelectedTabPageIndex = 2;
    this.tabbedControlGroup3.Size = new Size(870, 596);
    this.tabbedControlGroup3.TabPages.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutProbeConfigurationGroup,
      (BaseLayoutItem) this.layoutDevicSettingGroup,
      (BaseLayoutItem) this.layoutLoopBackCable,
      (BaseLayoutItem) this.layoutFirmwareGroup
    });
    this.tabbedControlGroup3.Text = "tabbedControlGroup3";
    this.layoutProbeConfigurationGroup.CustomizationFormText = "Probe Configuration";
    this.layoutProbeConfigurationGroup.Items.AddRange(new BaseLayoutItem[10]
    {
      (BaseLayoutItem) this.layouttxtProbeInformationProbeType,
      (BaseLayoutItem) this.layoutProbeInformationProbeSerialNumber,
      (BaseLayoutItem) this.emptySpaceItem1,
      (BaseLayoutItem) this.layoutLastCalibrationDate,
      (BaseLayoutItem) this.layoutSpeakerSettings,
      (BaseLayoutItem) this.layoutControlGroupSpk1Calculation,
      (BaseLayoutItem) this.layoutControlGroupCurrentSpeaker2Values,
      (BaseLayoutItem) this.layoutControlGroupMic1Calculation,
      (BaseLayoutItem) this.emptySpaceItem3,
      (BaseLayoutItem) this.emptySpaceItem10
    });
    this.layoutProbeConfigurationGroup.Location = new Point(0, 0);
    this.layoutProbeConfigurationGroup.Name = "layoutProbeConfigurationGroup";
    this.layoutProbeConfigurationGroup.Size = new Size(846, 550);
    this.layoutProbeConfigurationGroup.Text = "Probe Configuration";
    this.layouttxtProbeInformationProbeType.Control = (Control) this.txtProbeInformationProbeType;
    this.layouttxtProbeInformationProbeType.CustomizationFormText = "Information Probe Type";
    this.layouttxtProbeInformationProbeType.Location = new Point(0, 0);
    this.layouttxtProbeInformationProbeType.Name = "layouttxtProbeInformationProbeType";
    this.layouttxtProbeInformationProbeType.Size = new Size(260, 24);
    this.layouttxtProbeInformationProbeType.Text = "Probe Type";
    this.layouttxtProbeInformationProbeType.TextSize = new Size(111, 13);
    this.layoutProbeInformationProbeSerialNumber.Control = (Control) this.txtProbeInformationProbeSerialNumber;
    this.layoutProbeInformationProbeSerialNumber.CustomizationFormText = "Serial Number";
    this.layoutProbeInformationProbeSerialNumber.Location = new Point(0, 24);
    this.layoutProbeInformationProbeSerialNumber.Name = "layoutProbeInformationProbeSerialNumber";
    this.layoutProbeInformationProbeSerialNumber.Size = new Size(260, 24);
    this.layoutProbeInformationProbeSerialNumber.Text = "Serial Number";
    this.layoutProbeInformationProbeSerialNumber.TextSize = new Size(111, 13);
    this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
    this.emptySpaceItem1.Location = new Point(538, 0);
    this.emptySpaceItem1.Name = "emptySpaceItem1";
    this.emptySpaceItem1.Size = new Size(13, 48 /*0x30*/);
    this.emptySpaceItem1.Text = "emptySpaceItem1";
    this.emptySpaceItem1.TextSize = new Size(0, 0);
    this.layoutLastCalibrationDate.Control = (Control) this.txtLastCalDate;
    this.layoutLastCalibrationDate.CustomizationFormText = "Information Calibration Date";
    this.layoutLastCalibrationDate.Location = new Point(260, 0);
    this.layoutLastCalibrationDate.Name = "layoutLastCalibrationDate";
    this.layoutLastCalibrationDate.Size = new Size(278, 48 /*0x30*/);
    this.layoutLastCalibrationDate.Text = "Calibration Date";
    this.layoutLastCalibrationDate.TextSize = new Size(111, 13);
    this.layoutSpeakerSettings.CustomizationFormText = "Speaker/Microfon  Calibration Values";
    this.layoutSpeakerSettings.Items.AddRange(new BaseLayoutItem[11]
    {
      (BaseLayoutItem) this.layoutSpeaker1_1kHz,
      (BaseLayoutItem) this.layoutSpeaker1_2kHz,
      (BaseLayoutItem) this.layoutSpeaker1_4kHz,
      (BaseLayoutItem) this.layoutSpeaker1_6kHz,
      (BaseLayoutItem) this.layoutSpeaker2_1kHz,
      (BaseLayoutItem) this.layoutSpeaker2_2kHz,
      (BaseLayoutItem) this.layoutSpeaker2_4kHz,
      (BaseLayoutItem) this.layoutSpeaker2_6kHz,
      (BaseLayoutItem) this.emptySpaceItem2,
      (BaseLayoutItem) this.layoutMicrophoneSetting,
      (BaseLayoutItem) this.emptySpaceItem4
    });
    this.layoutSpeakerSettings.Location = new Point(0, 48 /*0x30*/);
    this.layoutSpeakerSettings.Name = "layoutSpeakerSettings";
    this.layoutSpeakerSettings.Size = new Size(551, 140);
    this.layoutSpeakerSettings.Text = "Speaker/Microfon  Calibration Values";
    this.layoutSpeaker1_1kHz.Control = (Control) this.txtSpeaker1_1kHz;
    this.layoutSpeaker1_1kHz.CustomizationFormText = "Speaker1 1kHz";
    this.layoutSpeaker1_1kHz.Location = new Point(0, 0);
    this.layoutSpeaker1_1kHz.Name = "layoutSpeaker1_1kHz";
    this.layoutSpeaker1_1kHz.Size = new Size(169, 24);
    this.layoutSpeaker1_1kHz.Text = "Speaker1 1kHz";
    this.layoutSpeaker1_1kHz.TextSize = new Size(111, 13);
    this.layoutSpeaker1_2kHz.Control = (Control) this.txtSpeaker1_2kHz;
    this.layoutSpeaker1_2kHz.CustomizationFormText = "Speaker1 2kHz";
    this.layoutSpeaker1_2kHz.Location = new Point(0, 24);
    this.layoutSpeaker1_2kHz.Name = "layoutSpeaker1_2kHz";
    this.layoutSpeaker1_2kHz.Size = new Size(169, 24);
    this.layoutSpeaker1_2kHz.Text = "Speaker1 2kHz";
    this.layoutSpeaker1_2kHz.TextSize = new Size(111, 13);
    this.layoutSpeaker1_4kHz.Control = (Control) this.txtSpeaker1_4kHz;
    this.layoutSpeaker1_4kHz.CustomizationFormText = "Speaker1 4kHz";
    this.layoutSpeaker1_4kHz.Location = new Point(0, 48 /*0x30*/);
    this.layoutSpeaker1_4kHz.Name = "layoutSpeaker1_4kHz";
    this.layoutSpeaker1_4kHz.Size = new Size(169, 24);
    this.layoutSpeaker1_4kHz.Text = "Speaker1 4kHz";
    this.layoutSpeaker1_4kHz.TextSize = new Size(111, 13);
    this.layoutSpeaker1_6kHz.Control = (Control) this.txtSpeaker1_6kHz;
    this.layoutSpeaker1_6kHz.CustomizationFormText = "Speaker1 6kHz";
    this.layoutSpeaker1_6kHz.Location = new Point(0, 72);
    this.layoutSpeaker1_6kHz.Name = "layoutSpeaker1_6kHz";
    this.layoutSpeaker1_6kHz.Size = new Size(169, 24);
    this.layoutSpeaker1_6kHz.Text = "Speaker1 6kHz";
    this.layoutSpeaker1_6kHz.TextSize = new Size(111, 13);
    this.layoutSpeaker2_1kHz.Control = (Control) this.txtSpeaker2_1kHz;
    this.layoutSpeaker2_1kHz.CustomizationFormText = "Speaker2 1kHz";
    this.layoutSpeaker2_1kHz.Location = new Point(169, 0);
    this.layoutSpeaker2_1kHz.Name = "layoutSpeaker2_1kHz";
    this.layoutSpeaker2_1kHz.Size = new Size(169, 24);
    this.layoutSpeaker2_1kHz.Text = "Speaker2 1kHz";
    this.layoutSpeaker2_1kHz.TextSize = new Size(111, 13);
    this.layoutSpeaker2_2kHz.Control = (Control) this.txtSpeaker2_2kHz;
    this.layoutSpeaker2_2kHz.CustomizationFormText = "Speaker2 2kHz";
    this.layoutSpeaker2_2kHz.Location = new Point(169, 24);
    this.layoutSpeaker2_2kHz.Name = "layoutSpeaker2_2kHz";
    this.layoutSpeaker2_2kHz.Size = new Size(169, 24);
    this.layoutSpeaker2_2kHz.Text = "Speaker2 2kHz";
    this.layoutSpeaker2_2kHz.TextSize = new Size(111, 13);
    this.layoutSpeaker2_4kHz.Control = (Control) this.txtSpeaker2_4kHz;
    this.layoutSpeaker2_4kHz.CustomizationFormText = "Speaker2 4kHz";
    this.layoutSpeaker2_4kHz.Location = new Point(169, 48 /*0x30*/);
    this.layoutSpeaker2_4kHz.Name = "layoutSpeaker2_4kHz";
    this.layoutSpeaker2_4kHz.Size = new Size(169, 24);
    this.layoutSpeaker2_4kHz.Text = "Speaker2 4kHz";
    this.layoutSpeaker2_4kHz.TextSize = new Size(111, 13);
    this.layoutSpeaker2_6kHz.Control = (Control) this.txtSpeaker2_6kHz;
    this.layoutSpeaker2_6kHz.CustomizationFormText = "Speaker2 6kHz";
    this.layoutSpeaker2_6kHz.Location = new Point(169, 72);
    this.layoutSpeaker2_6kHz.Name = "layoutSpeaker2_6kHz";
    this.layoutSpeaker2_6kHz.Size = new Size(169, 24);
    this.layoutSpeaker2_6kHz.Text = "Speaker2 6kHz";
    this.layoutSpeaker2_6kHz.TextSize = new Size(111, 13);
    this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
    this.emptySpaceItem2.Location = new Point(338, 24);
    this.emptySpaceItem2.Name = "emptySpaceItem2";
    this.emptySpaceItem2.Size = new Size(169, 72);
    this.emptySpaceItem2.Text = "emptySpaceItem2";
    this.emptySpaceItem2.TextSize = new Size(0, 0);
    this.layoutMicrophoneSetting.Control = (Control) this.txtMicrophoneSetting;
    this.layoutMicrophoneSetting.CustomizationFormText = "Microfon 1kHz";
    this.layoutMicrophoneSetting.Location = new Point(338, 0);
    this.layoutMicrophoneSetting.Name = "layoutMicrophoneSetting";
    this.layoutMicrophoneSetting.Size = new Size(169, 24);
    this.layoutMicrophoneSetting.Text = "Microfon 1kHz";
    this.layoutMicrophoneSetting.TextSize = new Size(111, 13);
    this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
    this.emptySpaceItem4.Location = new Point(507, 0);
    this.emptySpaceItem4.Name = "emptySpaceItem4";
    this.emptySpaceItem4.Size = new Size(20, 96 /*0x60*/);
    this.emptySpaceItem4.Text = "emptySpaceItem4";
    this.emptySpaceItem4.TextSize = new Size(0, 0);
    this.layoutControlGroupSpk1Calculation.CustomizationFormText = "Current Speaker 1 Values";
    this.layoutControlGroupSpk1Calculation.Items.AddRange(new BaseLayoutItem[9]
    {
      (BaseLayoutItem) this.layoutCurrSpk1Value1kHz,
      (BaseLayoutItem) this.layoutCurrSpk1Value2kHz,
      (BaseLayoutItem) this.layoutCurrSpk1Value4kHz,
      (BaseLayoutItem) this.layoutCurrSpk1Value6kHz,
      (BaseLayoutItem) this.layoutControlItem2,
      (BaseLayoutItem) this.layoutControlItem3,
      (BaseLayoutItem) this.layoutControlItem4,
      (BaseLayoutItem) this.layoutControlItem5,
      (BaseLayoutItem) this.emptySpaceItem7
    });
    this.layoutControlGroupSpk1Calculation.Location = new Point(0, 188);
    this.layoutControlGroupSpk1Calculation.Name = "layoutControlGroupSpk1Calculation";
    this.layoutControlGroupSpk1Calculation.Size = new Size(551, 110);
    this.layoutControlGroupSpk1Calculation.Text = "Speaker 1 Calibration Calculation";
    this.layoutCurrSpk1Value1kHz.Control = (Control) this.txtCurrSpk1Value1kHz;
    this.layoutCurrSpk1Value1kHz.CustomizationFormText = "1kHz";
    this.layoutCurrSpk1Value1kHz.Location = new Point(0, 0);
    this.layoutCurrSpk1Value1kHz.Name = "layoutCurrSpk1Value1kHz";
    this.layoutCurrSpk1Value1kHz.Size = new Size(117, 40);
    this.layoutCurrSpk1Value1kHz.Text = "Level at 1kHz";
    this.layoutCurrSpk1Value1kHz.TextLocation = Locations.Top;
    this.layoutCurrSpk1Value1kHz.TextSize = new Size(111, 13);
    this.layoutCurrSpk1Value2kHz.Control = (Control) this.txtCurrSpk1Value2kHz;
    this.layoutCurrSpk1Value2kHz.CustomizationFormText = "2kHz";
    this.layoutCurrSpk1Value2kHz.Location = new Point(117, 0);
    this.layoutCurrSpk1Value2kHz.Name = "layoutCurrSpk1Value2kHz";
    this.layoutCurrSpk1Value2kHz.Size = new Size(117, 40);
    this.layoutCurrSpk1Value2kHz.Text = "Level at 2kHz";
    this.layoutCurrSpk1Value2kHz.TextLocation = Locations.Top;
    this.layoutCurrSpk1Value2kHz.TextSize = new Size(111, 13);
    this.layoutCurrSpk1Value4kHz.Control = (Control) this.txtCurrSpk1Value4kHz;
    this.layoutCurrSpk1Value4kHz.CustomizationFormText = "4kHz";
    this.layoutCurrSpk1Value4kHz.Location = new Point(234, 0);
    this.layoutCurrSpk1Value4kHz.Name = "layoutCurrSpk1Value4kHz";
    this.layoutCurrSpk1Value4kHz.Size = new Size(118, 40);
    this.layoutCurrSpk1Value4kHz.Text = "Level at 4kHz";
    this.layoutCurrSpk1Value4kHz.TextLocation = Locations.Top;
    this.layoutCurrSpk1Value4kHz.TextSize = new Size(111, 13);
    this.layoutCurrSpk1Value6kHz.Control = (Control) this.txtCurrSpk1Value6kHz;
    this.layoutCurrSpk1Value6kHz.CustomizationFormText = "6kHz";
    this.layoutCurrSpk1Value6kHz.Location = new Point(352, 0);
    this.layoutCurrSpk1Value6kHz.Name = "layoutCurrSpk1Value6kHz";
    this.layoutCurrSpk1Value6kHz.Size = new Size(117, 40);
    this.layoutCurrSpk1Value6kHz.Text = "Level at 6kHz";
    this.layoutCurrSpk1Value6kHz.TextLocation = Locations.Top;
    this.layoutCurrSpk1Value6kHz.TextSize = new Size(111, 13);
    this.layoutControlItem2.Control = (Control) this.cbSpk1_1kHz;
    this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
    this.layoutControlItem2.Location = new Point(0, 40);
    this.layoutControlItem2.Name = "layoutControlItem2";
    this.layoutControlItem2.Size = new Size(117, 26);
    this.layoutControlItem2.Text = "layoutControlItem2";
    this.layoutControlItem2.TextSize = new Size(0, 0);
    this.layoutControlItem2.TextToControlDistance = 0;
    this.layoutControlItem2.TextVisible = false;
    this.layoutControlItem3.Control = (Control) this.cbSpk1_2kHz;
    this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
    this.layoutControlItem3.Location = new Point(117, 40);
    this.layoutControlItem3.Name = "layoutControlItem3";
    this.layoutControlItem3.Size = new Size(117, 26);
    this.layoutControlItem3.Text = "layoutControlItem3";
    this.layoutControlItem3.TextSize = new Size(0, 0);
    this.layoutControlItem3.TextToControlDistance = 0;
    this.layoutControlItem3.TextVisible = false;
    this.layoutControlItem4.Control = (Control) this.cbSpk1_4kHz;
    this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
    this.layoutControlItem4.Location = new Point(234, 40);
    this.layoutControlItem4.Name = "layoutControlItem4";
    this.layoutControlItem4.Size = new Size(118, 26);
    this.layoutControlItem4.Text = "layoutControlItem4";
    this.layoutControlItem4.TextSize = new Size(0, 0);
    this.layoutControlItem4.TextToControlDistance = 0;
    this.layoutControlItem4.TextVisible = false;
    this.layoutControlItem5.Control = (Control) this.cbSpk1_6kHz;
    this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
    this.layoutControlItem5.Location = new Point(352, 40);
    this.layoutControlItem5.Name = "layoutControlItem5";
    this.layoutControlItem5.Size = new Size(117, 26);
    this.layoutControlItem5.Text = "layoutControlItem5";
    this.layoutControlItem5.TextSize = new Size(0, 0);
    this.layoutControlItem5.TextToControlDistance = 0;
    this.layoutControlItem5.TextVisible = false;
    this.emptySpaceItem7.CustomizationFormText = "emptySpaceItem7";
    this.emptySpaceItem7.Location = new Point(469, 0);
    this.emptySpaceItem7.Name = "emptySpaceItem7";
    this.emptySpaceItem7.Size = new Size(58, 66);
    this.emptySpaceItem7.Text = "emptySpaceItem7";
    this.emptySpaceItem7.TextSize = new Size(0, 0);
    this.layoutControlGroupCurrentSpeaker2Values.CustomizationFormText = "Current Speaker 2 Values";
    this.layoutControlGroupCurrentSpeaker2Values.Items.AddRange(new BaseLayoutItem[9]
    {
      (BaseLayoutItem) this.layoutCurrSpk2Value1kHz,
      (BaseLayoutItem) this.layoutCurrSpk2Value2kHz,
      (BaseLayoutItem) this.layoutCurrSpk2Value4kHz,
      (BaseLayoutItem) this.layoutCurrSpk2Value6kHz,
      (BaseLayoutItem) this.emptySpaceItem5,
      (BaseLayoutItem) this.layoutControlItem6,
      (BaseLayoutItem) this.layoutControlItem7,
      (BaseLayoutItem) this.layoutControlItem8,
      (BaseLayoutItem) this.layoutControlItem9
    });
    this.layoutControlGroupCurrentSpeaker2Values.Location = new Point(0, 298);
    this.layoutControlGroupCurrentSpeaker2Values.Name = "layoutControlGroupCurrentSpeaker2Values";
    this.layoutControlGroupCurrentSpeaker2Values.Size = new Size(551, 110);
    this.layoutControlGroupCurrentSpeaker2Values.Text = "Speaker 2 Calibration Calculation";
    this.layoutCurrSpk2Value1kHz.Control = (Control) this.txtCurrSpk2Value1kHz;
    this.layoutCurrSpk2Value1kHz.CustomizationFormText = "1kHz";
    this.layoutCurrSpk2Value1kHz.Location = new Point(0, 0);
    this.layoutCurrSpk2Value1kHz.Name = "layoutCurrSpk2Value1kHz";
    this.layoutCurrSpk2Value1kHz.Size = new Size(117, 40);
    this.layoutCurrSpk2Value1kHz.Text = "Level at 1kHz";
    this.layoutCurrSpk2Value1kHz.TextLocation = Locations.Top;
    this.layoutCurrSpk2Value1kHz.TextSize = new Size(111, 13);
    this.layoutCurrSpk2Value2kHz.Control = (Control) this.txtCurrSpk2Value2kHz;
    this.layoutCurrSpk2Value2kHz.CustomizationFormText = "2kHz";
    this.layoutCurrSpk2Value2kHz.Location = new Point(117, 0);
    this.layoutCurrSpk2Value2kHz.Name = "layoutCurrSpk2Value2kHz";
    this.layoutCurrSpk2Value2kHz.Size = new Size(117, 40);
    this.layoutCurrSpk2Value2kHz.Text = "Level at 2kHz";
    this.layoutCurrSpk2Value2kHz.TextLocation = Locations.Top;
    this.layoutCurrSpk2Value2kHz.TextSize = new Size(111, 13);
    this.layoutCurrSpk2Value4kHz.Control = (Control) this.txtCurrSpk2Value4kHz;
    this.layoutCurrSpk2Value4kHz.CustomizationFormText = "4kHz";
    this.layoutCurrSpk2Value4kHz.Location = new Point(234, 0);
    this.layoutCurrSpk2Value4kHz.Name = "layoutCurrSpk2Value4kHz";
    this.layoutCurrSpk2Value4kHz.Size = new Size(118, 40);
    this.layoutCurrSpk2Value4kHz.Text = "Level at 4kHz";
    this.layoutCurrSpk2Value4kHz.TextLocation = Locations.Top;
    this.layoutCurrSpk2Value4kHz.TextSize = new Size(111, 13);
    this.layoutCurrSpk2Value6kHz.Control = (Control) this.txtCurrSpk2Value6kHz;
    this.layoutCurrSpk2Value6kHz.CustomizationFormText = "6kHz";
    this.layoutCurrSpk2Value6kHz.Location = new Point(352, 0);
    this.layoutCurrSpk2Value6kHz.Name = "layoutCurrSpk2Value6kHz";
    this.layoutCurrSpk2Value6kHz.Size = new Size(117, 40);
    this.layoutCurrSpk2Value6kHz.Text = "Level at 6kHz";
    this.layoutCurrSpk2Value6kHz.TextLocation = Locations.Top;
    this.layoutCurrSpk2Value6kHz.TextSize = new Size(111, 13);
    this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
    this.emptySpaceItem5.Location = new Point(469, 0);
    this.emptySpaceItem5.Name = "emptySpaceItem5";
    this.emptySpaceItem5.Size = new Size(58, 66);
    this.emptySpaceItem5.Text = "emptySpaceItem5";
    this.emptySpaceItem5.TextSize = new Size(0, 0);
    this.layoutControlItem6.Control = (Control) this.cbSpk2_1kHz;
    this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
    this.layoutControlItem6.Location = new Point(0, 40);
    this.layoutControlItem6.Name = "layoutControlItem6";
    this.layoutControlItem6.Size = new Size(117, 26);
    this.layoutControlItem6.Text = "layoutControlItem6";
    this.layoutControlItem6.TextSize = new Size(0, 0);
    this.layoutControlItem6.TextToControlDistance = 0;
    this.layoutControlItem6.TextVisible = false;
    this.layoutControlItem7.Control = (Control) this.cbSpk2_2kHz;
    this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
    this.layoutControlItem7.Location = new Point(117, 40);
    this.layoutControlItem7.Name = "layoutControlItem7";
    this.layoutControlItem7.Size = new Size(117, 26);
    this.layoutControlItem7.Text = "layoutControlItem7";
    this.layoutControlItem7.TextSize = new Size(0, 0);
    this.layoutControlItem7.TextToControlDistance = 0;
    this.layoutControlItem7.TextVisible = false;
    this.layoutControlItem8.Control = (Control) this.cbSpk2_4kHz;
    this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
    this.layoutControlItem8.Location = new Point(234, 40);
    this.layoutControlItem8.Name = "layoutControlItem8";
    this.layoutControlItem8.Size = new Size(118, 26);
    this.layoutControlItem8.Text = "layoutControlItem8";
    this.layoutControlItem8.TextSize = new Size(0, 0);
    this.layoutControlItem8.TextToControlDistance = 0;
    this.layoutControlItem8.TextVisible = false;
    this.layoutControlItem9.Control = (Control) this.cbSpk2_6kHz;
    this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
    this.layoutControlItem9.Location = new Point(352, 40);
    this.layoutControlItem9.Name = "layoutControlItem9";
    this.layoutControlItem9.Size = new Size(117, 26);
    this.layoutControlItem9.Text = "layoutControlItem9";
    this.layoutControlItem9.TextSize = new Size(0, 0);
    this.layoutControlItem9.TextToControlDistance = 0;
    this.layoutControlItem9.TextVisible = false;
    this.layoutControlGroupMic1Calculation.CustomizationFormText = "layoutControlGroupMic1Calculation";
    this.layoutControlGroupMic1Calculation.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutSetMic1Values,
      (BaseLayoutItem) this.emptySpaceItem6
    });
    this.layoutControlGroupMic1Calculation.Location = new Point(0, 408);
    this.layoutControlGroupMic1Calculation.Name = "layoutControlGroupMic1Calculation";
    this.layoutControlGroupMic1Calculation.Size = new Size(551, 70);
    this.layoutControlGroupMic1Calculation.Text = "Microfone Calibration Calculation";
    this.layoutSetMic1Values.Control = (Control) this.bSetMic1Values;
    this.layoutSetMic1Values.CustomizationFormText = "layoutControlItem18";
    this.layoutSetMic1Values.Location = new Point(0, 0);
    this.layoutSetMic1Values.Name = "layoutSetMic1Values";
    this.layoutSetMic1Values.Size = new Size(175, 26);
    this.layoutSetMic1Values.Text = "layoutSetMic1Values";
    this.layoutSetMic1Values.TextSize = new Size(0, 0);
    this.layoutSetMic1Values.TextToControlDistance = 0;
    this.layoutSetMic1Values.TextVisible = false;
    this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
    this.emptySpaceItem6.Location = new Point(175, 0);
    this.emptySpaceItem6.Name = "emptySpaceItem6";
    this.emptySpaceItem6.Size = new Size(352, 26);
    this.emptySpaceItem6.Text = "emptySpaceItem6";
    this.emptySpaceItem6.TextSize = new Size(0, 0);
    this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
    this.emptySpaceItem3.Location = new Point(0, 478);
    this.emptySpaceItem3.Name = "emptySpaceItem3";
    this.emptySpaceItem3.Size = new Size(551, 72);
    this.emptySpaceItem3.Text = "emptySpaceItem3";
    this.emptySpaceItem3.TextSize = new Size(0, 0);
    this.emptySpaceItem10.CustomizationFormText = "emptySpaceItem10";
    this.emptySpaceItem10.Location = new Point(551, 0);
    this.emptySpaceItem10.Name = "emptySpaceItem10";
    this.emptySpaceItem10.Size = new Size(295, 550);
    this.emptySpaceItem10.Text = "emptySpaceItem10";
    this.emptySpaceItem10.TextSize = new Size(0, 0);
    this.layoutDevicSettingGroup.CustomizationFormText = "Instrument Settings";
    this.layoutDevicSettingGroup.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.layoutDeviceSettingsGroup,
      (BaseLayoutItem) this.emptySpaceItem13
    });
    this.layoutDevicSettingGroup.Location = new Point(0, 0);
    this.layoutDevicSettingGroup.Name = "layoutDevicSettingGroup";
    this.layoutDevicSettingGroup.Size = new Size(846, 550);
    this.layoutDevicSettingGroup.Text = "Instrument Settings";
    this.layoutDeviceSettingsGroup.CustomizationFormText = "Settings";
    this.layoutDeviceSettingsGroup.Items.AddRange(new BaseLayoutItem[1]
    {
      (BaseLayoutItem) this.layoutControlItem19
    });
    this.layoutDeviceSettingsGroup.Location = new Point(0, 0);
    this.layoutDeviceSettingsGroup.Name = "layoutDeviceSettingsGroup";
    this.layoutDeviceSettingsGroup.Size = new Size(167, 550);
    this.layoutDeviceSettingsGroup.Text = "Settings";
    this.layoutControlItem19.Control = (Control) this.btnDeleteFlash;
    this.layoutControlItem19.CustomizationFormText = "layoutControlItem19";
    this.layoutControlItem19.Location = new Point(0, 0);
    this.layoutControlItem19.Name = "layoutControlItem19";
    this.layoutControlItem19.Size = new Size(143, 506);
    this.layoutControlItem19.Text = "layoutControlItem19";
    this.layoutControlItem19.TextSize = new Size(0, 0);
    this.layoutControlItem19.TextToControlDistance = 0;
    this.layoutControlItem19.TextVisible = false;
    this.emptySpaceItem13.CustomizationFormText = "emptySpaceItem13";
    this.emptySpaceItem13.Location = new Point(167, 0);
    this.emptySpaceItem13.Name = "emptySpaceItem13";
    this.emptySpaceItem13.Size = new Size(679, 550);
    this.emptySpaceItem13.Text = "emptySpaceItem13";
    this.emptySpaceItem13.TextSize = new Size(0, 0);
    this.layoutLoopBackCable.CustomizationFormText = "Loop Back Cable Test";
    this.layoutLoopBackCable.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutControlGroupLoopBackCableTest,
      (BaseLayoutItem) this.emptySpaceItem22,
      (BaseLayoutItem) this.layoutControlGroupCodecTest
    });
    this.layoutLoopBackCable.Location = new Point(0, 0);
    this.layoutLoopBackCable.Name = "layoutLoopBackCable";
    this.layoutLoopBackCable.Size = new Size(846, 550);
    this.layoutLoopBackCable.Text = "Loopback Cable Test";
    this.layoutControlGroupLoopBackCableTest.CustomizationFormText = "Loop Back Cable";
    this.layoutControlGroupLoopBackCableTest.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.emptySpaceItem12,
      (BaseLayoutItem) this.layoutControlItem18,
      (BaseLayoutItem) this.layoutControlItem10,
      (BaseLayoutItem) this.emptySpaceItem16,
      (BaseLayoutItem) this.layoutResultGroup
    });
    this.layoutControlGroupLoopBackCableTest.Location = new Point(223, 0);
    this.layoutControlGroupLoopBackCableTest.Name = "layoutControlGroupLoopBackCableTest";
    this.layoutControlGroupLoopBackCableTest.Size = new Size(242, 550);
    this.layoutControlGroupLoopBackCableTest.Text = "(2) Loopback Cable";
    this.emptySpaceItem12.CustomizationFormText = "emptySpaceItem12";
    this.emptySpaceItem12.Location = new Point(0, 318);
    this.emptySpaceItem12.Name = "emptySpaceItem12";
    this.emptySpaceItem12.Size = new Size(218, 188);
    this.emptySpaceItem12.Text = "emptySpaceItem12";
    this.emptySpaceItem12.TextSize = new Size(0, 0);
    this.layoutControlItem18.Control = (Control) this.cbTestAbrCanels;
    this.layoutControlItem18.CustomizationFormText = "layoutControlItem18";
    this.layoutControlItem18.Location = new Point(0, 0);
    this.layoutControlItem18.Name = "layoutControlItem18";
    this.layoutControlItem18.Size = new Size(218, 25);
    this.layoutControlItem18.Text = "layoutControlItem18";
    this.layoutControlItem18.TextSize = new Size(0, 0);
    this.layoutControlItem18.TextToControlDistance = 0;
    this.layoutControlItem18.TextVisible = false;
    this.layoutControlItem10.Control = (Control) this.bStartLoopBackTest;
    this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
    this.layoutControlItem10.Location = new Point(0, 25);
    this.layoutControlItem10.Name = "layoutControlItem10";
    this.layoutControlItem10.Size = new Size(218, 26);
    this.layoutControlItem10.Text = "layoutControlItem10";
    this.layoutControlItem10.TextSize = new Size(0, 0);
    this.layoutControlItem10.TextToControlDistance = 0;
    this.layoutControlItem10.TextVisible = false;
    this.emptySpaceItem16.CustomizationFormText = "emptySpaceItem16";
    this.emptySpaceItem16.Location = new Point(0, 51);
    this.emptySpaceItem16.Name = "emptySpaceItem16";
    this.emptySpaceItem16.Size = new Size(218, 31 /*0x1F*/);
    this.emptySpaceItem16.Text = "emptySpaceItem16";
    this.emptySpaceItem16.TextSize = new Size(0, 0);
    this.layoutResultGroup.CustomizationFormText = "Results";
    this.layoutResultGroup.Items.AddRange(new BaseLayoutItem[7]
    {
      (BaseLayoutItem) this.emptySpaceItem11,
      (BaseLayoutItem) this.layoutControlItem13,
      (BaseLayoutItem) this.layoutControlItem11,
      (BaseLayoutItem) this.layoutControlItem14,
      (BaseLayoutItem) this.layoutControlItem15,
      (BaseLayoutItem) this.layoutControlItem16,
      (BaseLayoutItem) this.layoutControlItem17
    });
    this.layoutResultGroup.Location = new Point(0, 82);
    this.layoutResultGroup.Name = "layoutResultGroup";
    this.layoutResultGroup.Size = new Size(218, 236);
    this.layoutResultGroup.Text = "Results";
    this.emptySpaceItem11.CustomizationFormText = "emptySpaceItem11";
    this.emptySpaceItem11.Location = new Point(0, 150);
    this.emptySpaceItem11.Name = "emptySpaceItem11";
    this.emptySpaceItem11.Size = new Size(194, 42);
    this.emptySpaceItem11.Text = "emptySpaceItem11";
    this.emptySpaceItem11.TextSize = new Size(0, 0);
    this.layoutControlItem13.Control = (Control) this.cbLoopBackTest2;
    this.layoutControlItem13.CustomizationFormText = "layoutControlItem13";
    this.layoutControlItem13.Location = new Point(0, 0);
    this.layoutControlItem13.Name = "layoutControlItem13";
    this.layoutControlItem13.Size = new Size(194, 25);
    this.layoutControlItem13.Text = "layoutControlItem13";
    this.layoutControlItem13.TextSize = new Size(0, 0);
    this.layoutControlItem13.TextToControlDistance = 0;
    this.layoutControlItem13.TextVisible = false;
    this.layoutControlItem11.Control = (Control) this.cbLoopBackTest3;
    this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
    this.layoutControlItem11.Location = new Point(0, 25);
    this.layoutControlItem11.Name = "layoutControlItem11";
    this.layoutControlItem11.Size = new Size(194, 25);
    this.layoutControlItem11.Text = "layoutControlItem11";
    this.layoutControlItem11.TextSize = new Size(0, 0);
    this.layoutControlItem11.TextToControlDistance = 0;
    this.layoutControlItem11.TextVisible = false;
    this.layoutControlItem14.Control = (Control) this.cbLoopBackTest4;
    this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
    this.layoutControlItem14.Location = new Point(0, 50);
    this.layoutControlItem14.Name = "layoutControlItem14";
    this.layoutControlItem14.Size = new Size(194, 25);
    this.layoutControlItem14.Text = "layoutControlItem14";
    this.layoutControlItem14.TextSize = new Size(0, 0);
    this.layoutControlItem14.TextToControlDistance = 0;
    this.layoutControlItem14.TextVisible = false;
    this.layoutControlItem15.Control = (Control) this.cbLoopBackTest5;
    this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
    this.layoutControlItem15.Location = new Point(0, 75);
    this.layoutControlItem15.Name = "layoutControlItem15";
    this.layoutControlItem15.Size = new Size(194, 25);
    this.layoutControlItem15.Text = "layoutControlItem15";
    this.layoutControlItem15.TextSize = new Size(0, 0);
    this.layoutControlItem15.TextToControlDistance = 0;
    this.layoutControlItem15.TextVisible = false;
    this.layoutControlItem16.Control = (Control) this.cbLoopBackTest6;
    this.layoutControlItem16.CustomizationFormText = "layoutControlItem16";
    this.layoutControlItem16.Location = new Point(0, 100);
    this.layoutControlItem16.Name = "layoutControlItem16";
    this.layoutControlItem16.Size = new Size(194, 25);
    this.layoutControlItem16.Text = "layoutControlItem16";
    this.layoutControlItem16.TextSize = new Size(0, 0);
    this.layoutControlItem16.TextToControlDistance = 0;
    this.layoutControlItem16.TextVisible = false;
    this.layoutControlItem17.Control = (Control) this.cbLoopBackTest7;
    this.layoutControlItem17.CustomizationFormText = "layoutControlItem17";
    this.layoutControlItem17.Location = new Point(0, 125);
    this.layoutControlItem17.Name = "layoutControlItem17";
    this.layoutControlItem17.Size = new Size(194, 25);
    this.layoutControlItem17.Text = "layoutControlItem17";
    this.layoutControlItem17.TextSize = new Size(0, 0);
    this.layoutControlItem17.TextToControlDistance = 0;
    this.layoutControlItem17.TextVisible = false;
    this.emptySpaceItem22.CustomizationFormText = "emptySpaceItem22";
    this.emptySpaceItem22.Location = new Point(465, 0);
    this.emptySpaceItem22.Name = "emptySpaceItem22";
    this.emptySpaceItem22.Size = new Size(381, 550);
    this.emptySpaceItem22.Text = "emptySpaceItem22";
    this.emptySpaceItem22.TextSize = new Size(0, 0);
    this.layoutControlGroupCodecTest.CustomizationFormText = "Codec Test";
    this.layoutControlGroupCodecTest.Items.AddRange(new BaseLayoutItem[4]
    {
      (BaseLayoutItem) this.layoutControlItem12,
      (BaseLayoutItem) this.btnStopCodecTest1,
      (BaseLayoutItem) this.layoutControlCodecTestResults,
      (BaseLayoutItem) this.emptySpaceItem23
    });
    this.layoutControlGroupCodecTest.Location = new Point(0, 0);
    this.layoutControlGroupCodecTest.Name = "layoutControlGroupCodecTest";
    this.layoutControlGroupCodecTest.Size = new Size(223, 550);
    this.layoutControlGroupCodecTest.Text = "(1) Codec Test";
    this.layoutControlItem12.Control = (Control) this.btnStartCodecTest1;
    this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
    this.layoutControlItem12.Location = new Point(0, 0);
    this.layoutControlItem12.Name = "layoutControlItem12";
    this.layoutControlItem12.Size = new Size(199, 26);
    this.layoutControlItem12.Text = "layoutControlItem12";
    this.layoutControlItem12.TextSize = new Size(0, 0);
    this.layoutControlItem12.TextToControlDistance = 0;
    this.layoutControlItem12.TextVisible = false;
    this.btnStopCodecTest1.Control = (Control) this.btnStopCodecTest;
    this.btnStopCodecTest1.CustomizationFormText = "Stop Codec Test";
    this.btnStopCodecTest1.Location = new Point(0, 26);
    this.btnStopCodecTest1.Name = "btnStopCodecTest1";
    this.btnStopCodecTest1.Size = new Size(199, 26);
    this.btnStopCodecTest1.Text = "Stop Codec Test";
    this.btnStopCodecTest1.TextSize = new Size(0, 0);
    this.btnStopCodecTest1.TextToControlDistance = 0;
    this.btnStopCodecTest1.TextVisible = false;
    this.layoutControlCodecTestResults.CustomizationFormText = "Results";
    this.layoutControlCodecTestResults.Items.AddRange(new BaseLayoutItem[2]
    {
      (BaseLayoutItem) this.emptySpaceItem9,
      (BaseLayoutItem) this.layoutCodecVoltageLevel
    });
    this.layoutControlCodecTestResults.Location = new Point(0, 83);
    this.layoutControlCodecTestResults.Name = "layoutControlCodecTestResults";
    this.layoutControlCodecTestResults.Size = new Size(199, 423);
    this.layoutControlCodecTestResults.Text = "Select Results";
    this.emptySpaceItem9.CustomizationFormText = "emptySpaceItem9";
    this.emptySpaceItem9.Location = new Point(0, 25);
    this.emptySpaceItem9.Name = "emptySpaceItem9";
    this.emptySpaceItem9.Size = new Size(175, 354);
    this.emptySpaceItem9.Text = "emptySpaceItem9";
    this.emptySpaceItem9.TextSize = new Size(0, 0);
    this.layoutCodecVoltageLevel.Control = (Control) this.cbCodecVoltageLevel;
    this.layoutCodecVoltageLevel.CustomizationFormText = "layoutCodecVoltageLevel";
    this.layoutCodecVoltageLevel.Location = new Point(0, 0);
    this.layoutCodecVoltageLevel.Name = "layoutCodecVoltageLevel";
    this.layoutCodecVoltageLevel.Size = new Size(175, 25);
    this.layoutCodecVoltageLevel.Text = "layoutCodecVoltageLevel";
    this.layoutCodecVoltageLevel.TextSize = new Size(0, 0);
    this.layoutCodecVoltageLevel.TextToControlDistance = 0;
    this.layoutCodecVoltageLevel.TextVisible = false;
    this.emptySpaceItem23.CustomizationFormText = "emptySpaceItem23";
    this.emptySpaceItem23.Location = new Point(0, 52);
    this.emptySpaceItem23.Name = "emptySpaceItem23";
    this.emptySpaceItem23.Size = new Size(199, 31 /*0x1F*/);
    this.emptySpaceItem23.Text = "emptySpaceItem23";
    this.emptySpaceItem23.TextSize = new Size(0, 0);
    this.layoutFirmwareGroup.CustomizationFormText = "Firmware";
    this.layoutFirmwareGroup.Items.AddRange(new BaseLayoutItem[3]
    {
      (BaseLayoutItem) this.layoutControlGroupFirmware,
      (BaseLayoutItem) this.emptySpaceItem17,
      (BaseLayoutItem) this.layoutControlLicence
    });
    this.layoutFirmwareGroup.Location = new Point(0, 0);
    this.layoutFirmwareGroup.Name = "layoutFirmwareGroup";
    this.layoutFirmwareGroup.Size = new Size(846, 550);
    this.layoutFirmwareGroup.Text = "Firmware";
    this.layoutControlGroupFirmware.CustomizationFormText = "Firmware";
    this.layoutControlGroupFirmware.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.layoutControlItem20,
      (BaseLayoutItem) this.emptySpaceItem18,
      (BaseLayoutItem) this.layoutControlItem22,
      (BaseLayoutItem) this.emptySpaceItem21,
      (BaseLayoutItem) this.emptySpaceItem15
    });
    this.layoutControlGroupFirmware.Location = new Point(0, 0);
    this.layoutControlGroupFirmware.Name = "layoutControlGroupFirmware";
    this.layoutControlGroupFirmware.Size = new Size(276, 133);
    this.layoutControlGroupFirmware.Text = "Firmware";
    this.layoutControlItem20.Control = (Control) this.btnSelectFirmware;
    this.layoutControlItem20.CustomizationFormText = "layoutControlItem20";
    this.layoutControlItem20.Location = new Point(0, 49);
    this.layoutControlItem20.Name = "layoutControlItem20";
    this.layoutControlItem20.Size = new Size(123, 26);
    this.layoutControlItem20.Text = "layoutControlItem20";
    this.layoutControlItem20.TextSize = new Size(0, 0);
    this.layoutControlItem20.TextToControlDistance = 0;
    this.layoutControlItem20.TextVisible = false;
    this.emptySpaceItem18.CustomizationFormText = "emptySpaceItem18";
    this.emptySpaceItem18.Location = new Point(123, 0);
    this.emptySpaceItem18.Name = "emptySpaceItem18";
    this.emptySpaceItem18.Size = new Size(129, 75);
    this.emptySpaceItem18.Text = "emptySpaceItem18";
    this.emptySpaceItem18.TextSize = new Size(0, 0);
    this.layoutControlItem22.Control = (Control) this.btnUpdateFirmware;
    this.layoutControlItem22.CustomizationFormText = "layoutControlItem22";
    this.layoutControlItem22.Location = new Point(0, 0);
    this.layoutControlItem22.Name = "layoutControlItem22";
    this.layoutControlItem22.Size = new Size(123, 26);
    this.layoutControlItem22.Text = "layoutControlItem22";
    this.layoutControlItem22.TextSize = new Size(0, 0);
    this.layoutControlItem22.TextToControlDistance = 0;
    this.layoutControlItem22.TextVisible = false;
    this.emptySpaceItem21.CustomizationFormText = "emptySpaceItem21";
    this.emptySpaceItem21.Location = new Point(0, 75);
    this.emptySpaceItem21.Name = "emptySpaceItem21";
    this.emptySpaceItem21.Size = new Size(252, 14);
    this.emptySpaceItem21.Text = "emptySpaceItem21";
    this.emptySpaceItem21.TextSize = new Size(0, 0);
    this.emptySpaceItem15.CustomizationFormText = "emptySpaceItem15";
    this.emptySpaceItem15.Location = new Point(0, 26);
    this.emptySpaceItem15.Name = "emptySpaceItem15";
    this.emptySpaceItem15.Size = new Size(123, 23);
    this.emptySpaceItem15.Text = "emptySpaceItem15";
    this.emptySpaceItem15.TextSize = new Size(0, 0);
    this.emptySpaceItem17.CustomizationFormText = "emptySpaceItem17";
    this.emptySpaceItem17.Location = new Point(276, 0);
    this.emptySpaceItem17.Name = "emptySpaceItem17";
    this.emptySpaceItem17.Size = new Size(570, 550);
    this.emptySpaceItem17.Text = "emptySpaceItem17";
    this.emptySpaceItem17.TextSize = new Size(0, 0);
    this.layoutControlLicence.CustomizationFormText = "Licence";
    this.layoutControlLicence.Items.AddRange(new BaseLayoutItem[5]
    {
      (BaseLayoutItem) this.emptySpaceItem14,
      (BaseLayoutItem) this.layoutControlItemFwLicence,
      (BaseLayoutItem) this.emptySpaceItem19,
      (BaseLayoutItem) this.emptySpaceItem20,
      (BaseLayoutItem) this.layoutControlItem21
    });
    this.layoutControlLicence.Location = new Point(0, 133);
    this.layoutControlLicence.Name = "layoutControlLicence";
    this.layoutControlLicence.Size = new Size(276, 417);
    this.layoutControlLicence.Text = "Licence";
    this.emptySpaceItem14.CustomizationFormText = "emptySpaceItem14";
    this.emptySpaceItem14.Location = new Point(0, 73);
    this.emptySpaceItem14.Name = "emptySpaceItem14";
    this.emptySpaceItem14.Size = new Size(125, 300);
    this.emptySpaceItem14.Text = "emptySpaceItem14";
    this.emptySpaceItem14.TextSize = new Size(0, 0);
    this.layoutControlItemFwLicence.Control = (Control) this.tbEditFwLicence;
    this.layoutControlItemFwLicence.CustomizationFormText = "Enter Firmware Licence";
    this.layoutControlItemFwLicence.Location = new Point(0, 0);
    this.layoutControlItemFwLicence.Name = "layoutControlItemFwLicence";
    this.layoutControlItemFwLicence.Size = new Size(252, 24);
    this.layoutControlItemFwLicence.Text = "Enter Firmware Licence";
    this.layoutControlItemFwLicence.TextSize = new Size(111, 13);
    this.emptySpaceItem19.CustomizationFormText = "emptySpaceItem19";
    this.emptySpaceItem19.Location = new Point(0, 24);
    this.emptySpaceItem19.Name = "emptySpaceItem19";
    this.emptySpaceItem19.Size = new Size(252, 23);
    this.emptySpaceItem19.Text = "emptySpaceItem19";
    this.emptySpaceItem19.TextSize = new Size(0, 0);
    this.emptySpaceItem20.CustomizationFormText = "emptySpaceItem20";
    this.emptySpaceItem20.Location = new Point(125, 47);
    this.emptySpaceItem20.Name = "emptySpaceItem20";
    this.emptySpaceItem20.Size = new Size((int) sbyte.MaxValue, 326);
    this.emptySpaceItem20.Text = "emptySpaceItem20";
    this.emptySpaceItem20.TextSize = new Size(0, 0);
    this.layoutControlItem21.Control = (Control) this.btnSetFwLicence;
    this.layoutControlItem21.CustomizationFormText = "layoutControlItem21";
    this.layoutControlItem21.Location = new Point(0, 47);
    this.layoutControlItem21.Name = "layoutControlItem21";
    this.layoutControlItem21.Size = new Size(125, 26);
    this.layoutControlItem21.Text = "layoutControlItem21";
    this.layoutControlItem21.TextSize = new Size(0, 0);
    this.layoutControlItem21.TextToControlDistance = 0;
    this.layoutControlItem21.TextVisible = false;
    this.layoutControlItem1.Control = (Control) this.txtSpeaker1_1kHz;
    this.layoutControlItem1.CustomizationFormText = "Spk1 1kHz";
    this.layoutControlItem1.Location = new Point(0, 0);
    this.layoutControlItem1.Name = "layoutSpeaker1_1kHz";
    this.layoutControlItem1.Size = new Size(697, 24);
    this.layoutControlItem1.Text = "Spk1 1kHz";
    this.layoutControlItem1.TextSize = new Size(66, 13);
    this.layoutControlItem1.TextToControlDistance = 5;
    this.emptySpaceItem8.CustomizationFormText = "emptySpaceItem8";
    this.emptySpaceItem8.Location = new Point(860, 92);
    this.emptySpaceItem8.Name = "emptySpaceItem8";
    this.emptySpaceItem8.Size = new Size(10, 504);
    this.emptySpaceItem8.Text = "emptySpaceItem8";
    this.emptySpaceItem8.TextSize = new Size(0, 0);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.layoutControl1);
    this.Name = nameof (ProbeConfiguratorEditor);
    this.Size = new Size(890, 616);
    this.layoutControl1.EndInit();
    this.layoutControl1.ResumeLayout(false);
    this.cbCodecVoltageLevel.Properties.EndInit();
    this.tbEditFwLicence.Properties.EndInit();
    this.cbTestAbrCanels.Properties.EndInit();
    this.cbLoopBackTest7.Properties.EndInit();
    this.cbLoopBackTest6.Properties.EndInit();
    this.cbLoopBackTest5.Properties.EndInit();
    this.cbLoopBackTest4.Properties.EndInit();
    this.cbLoopBackTest3.Properties.EndInit();
    this.cbLoopBackTest2.Properties.EndInit();
    this.txtLastCalDate.Properties.EndInit();
    this.txtCurrSpk2Value6kHz.Properties.EndInit();
    this.txtCurrSpk2Value4kHz.Properties.EndInit();
    this.txtCurrSpk2Value2kHz.Properties.EndInit();
    this.txtCurrSpk2Value1kHz.Properties.EndInit();
    this.txtCurrSpk1Value6kHz.Properties.EndInit();
    this.txtCurrSpk1Value4kHz.Properties.EndInit();
    this.txtCurrSpk1Value1kHz.Properties.EndInit();
    this.txtSpeaker2_6kHz.Properties.EndInit();
    this.txtSpeaker2_4kHz.Properties.EndInit();
    this.txtSpeaker2_2kHz.Properties.EndInit();
    this.txtCurrSpk1Value2kHz.Properties.EndInit();
    this.txtSpeaker2_1kHz.Properties.EndInit();
    this.txtMicrophoneSetting.Properties.EndInit();
    this.txtSpeaker1_6kHz.Properties.EndInit();
    this.txtSpeaker1_4kHz.Properties.EndInit();
    this.txtSpeaker1_2kHz.Properties.EndInit();
    this.txtSpeaker1_1kHz.Properties.EndInit();
    this.txtProbeInformationProbeSerialNumber.Properties.EndInit();
    this.txtProbeInformationProbeType.Properties.EndInit();
    this.layoutControlGroup1.EndInit();
    this.tabbedControlGroup3.EndInit();
    this.layoutProbeConfigurationGroup.EndInit();
    this.layouttxtProbeInformationProbeType.EndInit();
    this.layoutProbeInformationProbeSerialNumber.EndInit();
    this.emptySpaceItem1.EndInit();
    this.layoutLastCalibrationDate.EndInit();
    this.layoutSpeakerSettings.EndInit();
    this.layoutSpeaker1_1kHz.EndInit();
    this.layoutSpeaker1_2kHz.EndInit();
    this.layoutSpeaker1_4kHz.EndInit();
    this.layoutSpeaker1_6kHz.EndInit();
    this.layoutSpeaker2_1kHz.EndInit();
    this.layoutSpeaker2_2kHz.EndInit();
    this.layoutSpeaker2_4kHz.EndInit();
    this.layoutSpeaker2_6kHz.EndInit();
    this.emptySpaceItem2.EndInit();
    this.layoutMicrophoneSetting.EndInit();
    this.emptySpaceItem4.EndInit();
    this.layoutControlGroupSpk1Calculation.EndInit();
    this.layoutCurrSpk1Value1kHz.EndInit();
    this.layoutCurrSpk1Value2kHz.EndInit();
    this.layoutCurrSpk1Value4kHz.EndInit();
    this.layoutCurrSpk1Value6kHz.EndInit();
    this.layoutControlItem2.EndInit();
    this.layoutControlItem3.EndInit();
    this.layoutControlItem4.EndInit();
    this.layoutControlItem5.EndInit();
    this.emptySpaceItem7.EndInit();
    this.layoutControlGroupCurrentSpeaker2Values.EndInit();
    this.layoutCurrSpk2Value1kHz.EndInit();
    this.layoutCurrSpk2Value2kHz.EndInit();
    this.layoutCurrSpk2Value4kHz.EndInit();
    this.layoutCurrSpk2Value6kHz.EndInit();
    this.emptySpaceItem5.EndInit();
    this.layoutControlItem6.EndInit();
    this.layoutControlItem7.EndInit();
    this.layoutControlItem8.EndInit();
    this.layoutControlItem9.EndInit();
    this.layoutControlGroupMic1Calculation.EndInit();
    this.layoutSetMic1Values.EndInit();
    this.emptySpaceItem6.EndInit();
    this.emptySpaceItem3.EndInit();
    this.emptySpaceItem10.EndInit();
    this.layoutDevicSettingGroup.EndInit();
    this.layoutDeviceSettingsGroup.EndInit();
    this.layoutControlItem19.EndInit();
    this.emptySpaceItem13.EndInit();
    this.layoutLoopBackCable.EndInit();
    this.layoutControlGroupLoopBackCableTest.EndInit();
    this.emptySpaceItem12.EndInit();
    this.layoutControlItem18.EndInit();
    this.layoutControlItem10.EndInit();
    this.emptySpaceItem16.EndInit();
    this.layoutResultGroup.EndInit();
    this.emptySpaceItem11.EndInit();
    this.layoutControlItem13.EndInit();
    this.layoutControlItem11.EndInit();
    this.layoutControlItem14.EndInit();
    this.layoutControlItem15.EndInit();
    this.layoutControlItem16.EndInit();
    this.layoutControlItem17.EndInit();
    this.emptySpaceItem22.EndInit();
    this.layoutControlGroupCodecTest.EndInit();
    this.layoutControlItem12.EndInit();
    this.btnStopCodecTest1.EndInit();
    this.layoutControlCodecTestResults.EndInit();
    this.emptySpaceItem9.EndInit();
    this.layoutCodecVoltageLevel.EndInit();
    this.emptySpaceItem23.EndInit();
    this.layoutFirmwareGroup.EndInit();
    this.layoutControlGroupFirmware.EndInit();
    this.layoutControlItem20.EndInit();
    this.emptySpaceItem18.EndInit();
    this.layoutControlItem22.EndInit();
    this.emptySpaceItem21.EndInit();
    this.emptySpaceItem15.EndInit();
    this.emptySpaceItem17.EndInit();
    this.layoutControlLicence.EndInit();
    this.emptySpaceItem14.EndInit();
    this.layoutControlItemFwLicence.EndInit();
    this.emptySpaceItem19.EndInit();
    this.emptySpaceItem20.EndInit();
    this.layoutControlItem21.EndInit();
    this.layoutControlItem1.EndInit();
    this.emptySpaceItem8.EndInit();
    this.ResumeLayout(false);
  }

  private delegate void UpdateViewCallBack(ChangeType changeType, Type type, object item);
}
