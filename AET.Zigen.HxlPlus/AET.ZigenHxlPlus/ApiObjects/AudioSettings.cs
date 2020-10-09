using System;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using Newtonsoft.Json.Linq;

namespace AET.Zigen.HxlPlus.ApiObjects {
  public class AudioSettings : ApiObject {

    public AudioSettings() : base("/SetAudioSettings", "/GetAudioSettings") {
      AddEmptyDelegatesToSplusOutputs();
    }

    private HxlPlus HxlPlus { get { return RestClient as HxlPlus; } }

    public ushort Output { get; set; }

    #region Mute
    public ushort Mute {
      get { return GetBool("mute").ToUshort(); }
      set {
        Json["mute"] = value.ToBool();
        SetMuteF(Output, value);
        if (HxlPlus.SelectedAudioSettings == Output) HxlPlus.SetMuteF(value);
      }
    }

    public void MuteToggle() { Mute = (ushort)(Mute == 0 ? 1 : 0); }

    #endregion

    public ushort Volume {
      get { return ConvertTo16Bit((long?)Json["volume"], 100); }
      set {
        Json["volume"] = ConvertFrom16Bit(value, 100);
        SetVolumeF(Output, value);
        if (HxlPlus.SelectedAudioSettings == Output) HxlPlus.SetVolumeF(value);
      }
    }

    #region TuneMode
    internal string TuneMode {
      get { return (string)Json["tune mode"]; }
      set {
        value = Clean(value);
        Json["tune mode"] = value;
        UpdateTuneModeFb(value);
      }
    }

    private void UpdateTuneModeFb(string value) {
      SetTuneModeDisabledF(Output, (value == "disabled").ToUshort());
      SetTuneModePresetsF(Output, (value == "presets").ToUshort());
      SetTuneModeEqualizerF(Output, (value == "equalizer").ToUshort());
      SetTuneModeToneControlF(Output, (value == "tonecontrol").ToUshort());
      if (HxlPlus.SelectedAudioSettings != Output) return;
      HxlPlus.SetTuneModeDisabledF((value == "disabled").ToUshort());
      HxlPlus.SetTuneModePresetsF((value == "presets").ToUshort());
      HxlPlus.SetTuneModeEqualizerF((value == "equalizer").ToUshort());
      HxlPlus.SetTuneModeToneControlF((value == "tonecontrol").ToUshort());
    }

    public void TuneModeDisabled() { TuneMode = "disabled"; }
    public void TuneModePresets() { TuneMode = "presets"; }
    public void TuneModeEqualizer() { TuneMode = "equalizer"; }
    public void TuneModeToneControl() { TuneMode = "tonecontrol"; }
    #endregion

    #region Preset
    internal string Preset {
      get { return (string)Json["preset"]; }
      set {
        value = Clean(value);
        Json["preset"] = value;
        UpdatePresetFb(value);
      }
    }

    private void UpdatePresetFb(string value) {
      SetPresetFlatF(Output, (value == "flat").ToUshort());
      SetPresetRockF(Output, (value == "rock").ToUshort());
      SetPresetClassicalF(Output, (value == "classical").ToUshort());
      SetPresetDanceF(Output, (value == "dance").ToUshort());
      SetPresetAcousticF(Output, (value == "acoustic").ToUshort());
      if (HxlPlus.SelectedAudioSettings != Output) return;
      HxlPlus.SetPresetFlatF((value == "flat").ToUshort());
      HxlPlus.SetPresetRockF((value == "rock").ToUshort());
      HxlPlus.SetPresetClassicalF((value == "classical").ToUshort());
      HxlPlus.SetPresetDanceF((value == "dance").ToUshort());
      HxlPlus.SetPresetAcousticF((value == "acoustic").ToUshort());

    }

    public void PresetFlat() { Preset = "flat"; }
    public void PresetRock() { Preset = "rock"; }
    public void PresetClassical() { Preset = "classical"; }
    public void PresetDance() { Preset = "dance"; }
    public void PresetAcoustic() { Preset = "acoustic"; }
    #endregion
    
    #region EQ Bands

    public short Band115 {
      get { return ConvertEqTo16Bit(GetDouble("band0")); }
      set {
        Json["band0"] = ConvertEqFrom16Bit(value);
        HxlPlus.SetBand115F(value);
        SetBand115F(Output, value);
        SetBand115Text(Output, Json["band0"].ToString());
        if (HxlPlus.SelectedAudioSettings != Output) return;
        HxlPlus.SetBand115Text(Json["band0"].ToString());
        HxlPlus.SetBand115F(value);
      }
    }


    public short Band330 {
      get { return ConvertEqTo16Bit(GetDouble("band1")); }
      set {
        Json["band1"] = ConvertEqFrom16Bit(value);
        HxlPlus.SetBand330F(value);
        HxlPlus.SetBand330Text(Json["band1"].ToString());
      }
    }



    public short Band990 {
      get { return ConvertEqTo16Bit(GetDouble("band2")); }
      set {
        Json["band2"] = ConvertEqFrom16Bit(value);
        HxlPlus.SetBand990F(value);
        HxlPlus.SetBand990Text(Json["band2"].ToString());
      }
    }


    public short Band3000 {
      get { return ConvertEqTo16Bit(GetDouble("band3")); }
      set {
        Json["band3"] = ConvertEqFrom16Bit(value);
        HxlPlus.SetBand3000F(value);
        HxlPlus.SetBand3000Text(Json["band3"].ToString());
      }
    }

    public short Band9900 {
      get { return ConvertEqTo16Bit(GetDouble("band4")); }
      set {
        Json["band4"] = ConvertEqFrom16Bit(value);
        HxlPlus.SetBand9900F(value);
        HxlPlus.SetBand9900Text(Json["band4"].ToString());
      }
    }


    #endregion

    #region Bass/Treble

    public short Bass {
      get { return ConvertEqTo16Bit(GetDouble("basstone")); }
      set {
        Json["basstone"] = ConvertEqFrom16Bit(value);
        HxlPlus.SetBassF(value);
        HxlPlus.SetBassText(Json["basstone"].ToString());
      }

    }

    public short Treble {
      get { return ConvertEqTo16Bit(GetDouble("treble")); }
      set {
        Json["treble"] = ConvertEqFrom16Bit(value);
        HxlPlus.SetTrebleF(value);
        HxlPlus.SetTrebleText(Json["treble"].ToString());
      }
    }

    #endregion 

    #region Surround

    public ushort Surround {
      get { return GetBool("surround").ToUshort(); }
      set {
        Json["surround"] = value.ToBool();
        HxlPlus.SetSurroundF(value);
      }
    }
    public void SurroundToggle() { Surround = (ushort)(Surround == 0 ? 1 : 0); }

    public ushort SurroundLevel {
      get { return ConvertTo16Bit(GetInt("surrlevel"), 7); }
      set {
        Json["surrlevel"] = ConvertFrom16Bit(value, 7);
        HxlPlus.SetSurroundLevelF(value);
      }
    }

    #endregion

    #region BassEnhancement

    public void BassEnhancementToggle() { BassEnhancement = (ushort)(BassEnhancement == 0 ? 1 : 0); }
    public ushort BassEnhancement {
      get { return GetBool("bass").ToUshort(); }
      set {
        Json["bass"] = value.ToBool();
        HxlPlus.SetBassEnhancementF(value);
      }
    }

    public ushort BassLevel {
      get { return ConvertTo16Bit(GetInt("basslevel"), 127); }
      set {
        Json["basslevel"] = ConvertFrom16Bit(value, 127);
        HxlPlus.SetBassLevelF(value);
      }
    }

    public void BassCutFreq80() { BassCutoff = 80; }
    public void BassCutFreq100() { BassCutoff = 100; }
    public void BassCutFreq125() { BassCutoff = 125; }
    public void BassCutFreq150() { BassCutoff = 150; }
    public void BassCutFreq175() { BassCutoff = 175; }
    public void BassCutFreq200() { BassCutoff = 200; }
    public void BassCutFreq225() { BassCutoff = 225; }

    internal ushort BassCutoff {
      get { return (ushort)GetInt("bassfreq"); }
      set {
        Json["bassfreq"] = value;
        UpdateBassCutFreqFb(value);
      }
    }

    private void UpdateBassCutFreqFb(ushort value) {
      SetBassCFreq80F(Output, (value == 80).ToUshort());
      SetBassCFreq100F(Output, (value == 100).ToUshort());
      SetBassCFreq125F(Output, (value == 125).ToUshort());
      SetBassCFreq150F(Output, (value == 150).ToUshort());
      SetBassCFreq175F(Output, (value == 175).ToUshort());
      SetBassCFreq200F(Output, (value == 200).ToUshort());
      SetBassCFreq225F(Output, (value == 225).ToUshort());
      if (HxlPlus.SelectedAudioSettings != Output) return;
      HxlPlus.SetBassCFreq80F((value == 80).ToUshort());
      HxlPlus.SetBassCFreq100F((value == 100).ToUshort());
      HxlPlus.SetBassCFreq125F((value == 125).ToUshort());
      HxlPlus.SetBassCFreq150F((value == 150).ToUshort());
      HxlPlus.SetBassCFreq175F((value == 175).ToUshort());
      HxlPlus.SetBassCFreq200F((value == 200).ToUshort());
      HxlPlus.SetBassCFreq225F((value == 225).ToUshort());
    }

    public void HighPassToggle() { HighPass = (ushort)(HighPass == 0 ? 1 : 0); }
    public ushort HighPass {
      get { return GetBool("highpass").ToUshort(); }
      set {
        Json["highpass"] = value.ToBool();
        HxlPlus.SetHighPassF(value);
      }
    }
    #endregion

    public override void Poll() {
      Poll(null);
    }

    public void Poll(Action callback) {
      var postContents = string.Format(@"{{""output"":{0}}}", Output);
      FillJsonFromPost(postContents,() => {
        Json = Json["audioInfo"] as JObject;
        Json["preset"] = Json["presets"];
        Json.Remove("presets");
        FillFromJsonObject();
        if (callback != null) callback();
      });
    }

    public void Refresh() {
      FillFromJsonObject();
    }

    private void FillFromJsonObject() {
      SetMuteF(Output, Mute);
      SetVolumeF(Output, Volume);
      UpdateTuneModeFb(TuneMode);
      UpdatePresetFb(Preset);
      UpdateBassCutFreqFb(BassCutoff);

      SetBand115F(Output, Band115);
      SetBand330F(Output, Band330);
      SetBand990F(Output, Band990);
      SetBand3000F(Output, Band3000);
      SetBand9900F(Output, Band9900);
      SetBassF(Output, Bass);
      SetTrebleF(Output, Treble);
      SetSurroundF(Output, Surround);
      SetSurroundLevelF(Output, SurroundLevel);
      SetBassEnhancementF(Output, BassEnhancement);
      SetBassLevelF(Output, BassLevel);
      SetHighPassF(Output, HighPass);

      if (HxlPlus.SelectedAudioSettings != Output) return;
      HxlPlus.SetMuteF(Mute);
      HxlPlus.SetVolumeF(Volume);
      HxlPlus.SetBand115F(Band115);
      HxlPlus.SetBand330F(Band330);
      HxlPlus.SetBand990F(Band990);
      HxlPlus.SetBand3000F(Band3000);
      HxlPlus.SetBand9900F(Band9900);
      HxlPlus.SetBassF(Bass);
      HxlPlus.SetTrebleF(Treble);
      HxlPlus.SetSurroundF(Surround);
      HxlPlus.SetSurroundLevelF(SurroundLevel);
      HxlPlus.SetBassEnhancementF(BassEnhancement);
      HxlPlus.SetBassLevelF(BassLevel);
      HxlPlus.SetHighPassF(HighPass);
    }

    private void UpdateMatrix() {
      var matrix = Json["matrix"] as JArray;
      if (matrix == null) {
        ErrorMessage.Warn("HxlPlus.AllAudioSettings.Poll() HxlPlus did not return a 'matrix' object in response.");
        return;
      }

      var audioMatrix = HxlPlus.AudioMatrix;
      for (ushort i = 1; i <= audioMatrix.OutputCount; i++) HxlPlus.SetAudioOutF(i, (ushort)(matrix[i - 1].Value<int>() + 1));
    }

    #region Conversion Routines
    private short ConvertEqTo16Bit(double? nullableValue) {
      double value = nullableValue ?? 0;
      return (short)(value * 10);
    }
    private double ConvertEqFrom16Bit(short value) {
      double o = value;
      o /= 10;
      o = Math.Round(o * 4) / 4;
      return o;
    }
    #endregion

    #region IOsAreValid
    public override bool RequiredFieldsAreValid() {
      if (!TuneModeIsValid()) return false;
      if (!PresetIsValid()) return false;
      if (!ValueIsValid((ushort?)(long?)Json["bassfreq"], "BassCutoff", new ushort?[] { 80, 100, 125, 150, 175, 200, 225 })) return false;
      if (!ValueIsValid(Output, "Output", 1, (ushort) HxlPlus.AudioMatrix.OutputCount)) return false;
      return true;
    }

    private bool TuneModeIsValid() {
      return ValueIsValid(TuneMode, "TuneMode", new[] { "disabled", "presets", "equalizer", "tonecontrol" });
    }

    private bool PresetIsValid() {
      return ValueIsValid(Preset, "Preset", new[] { "flat", "rock", "classical", "dance", "acoustic" });
    }
    #endregion

    public override void Send() {
      if (lastSent != null) lastSent["output"] = null;
      Json["output"] = Output - 1;
      base.Send();
    }

    #region SPlus Feedback Delegates
    public void AddEmptyDelegatesToSplusOutputs() {
      SetMuteF = delegate { };
      SetVolumeF = delegate { };
      SetTuneModeDisabledF = delegate { };
      SetTuneModePresetsF = delegate { };
      SetTuneModeEqualizerF = delegate { };
      SetTuneModeToneControlF = delegate { };
      SetPresetFlatF = delegate { };
      SetPresetRockF = delegate { };
      SetPresetClassicalF = delegate { };
      SetPresetDanceF = delegate { };
      SetPresetAcousticF = delegate { };
      SetBand115F = delegate { };
      SetBand330F = delegate { };
      SetBand990F = delegate { };
      SetBand3000F = delegate { };
      SetBand9900F = delegate { };
      SetBand115Text = delegate { };
      SetBand330Text = delegate { };
      SetBand990Text = delegate { };
      SetBand3000Text = delegate { };
      SetBand9900Text = delegate { };
      SetBassF = delegate { };
      SetTrebleF = delegate { };
      SetBassText = delegate { };
      SetTrebleText = delegate { };
      SetSurroundF = delegate { };
      SetSurroundLevelF = delegate { };
      SetBassEnhancementF = delegate { };
      SetBassLevelF = delegate { };
      SetBassCFreq80F = delegate { };
      SetBassCFreq100F = delegate { };
      SetBassCFreq125F = delegate { };
      SetBassCFreq150F = delegate { };
      SetBassCFreq175F = delegate { };
      SetBassCFreq200F = delegate { };
      SetBassCFreq225F = delegate { };
      SetHighPassF = delegate { };
      SetMuteF = delegate { };
      SetVolumeF = delegate { };
    }
    public SetUshortOutputArrayDelegate SetTuneModeDisabledF { get; set; }
    public SetUshortOutputArrayDelegate SetTuneModePresetsF { get; set; }
    public SetUshortOutputArrayDelegate SetTuneModeEqualizerF { get; set; }
    public SetUshortOutputArrayDelegate SetTuneModeToneControlF { get; set; }
    public SetUshortOutputArrayDelegate SetPresetFlatF { get; set; }
    public SetUshortOutputArrayDelegate SetPresetRockF { get; set; }
    public SetUshortOutputArrayDelegate SetPresetClassicalF { get; set; }
    public SetUshortOutputArrayDelegate SetPresetDanceF { get; set; }
    public SetUshortOutputArrayDelegate SetPresetAcousticF { get; set; }
    public SetShortOutputArrayDelegate SetBand115F { get; set; }
    public SetShortOutputArrayDelegate SetBand330F { get; set; }
    public SetShortOutputArrayDelegate SetBand990F { get; set; }
    public SetShortOutputArrayDelegate SetBand3000F { get; set; }
    public SetShortOutputArrayDelegate SetBand9900F { get; set; }
    public SetShortOutputArrayDelegate SetBassF { get; set; }
    public SetShortOutputArrayDelegate SetTrebleF { get; set; }
    public SetStringOutputArrayDelegate SetBand115Text { get; set; }
    public SetStringOutputArrayDelegate SetBand330Text { get; set; }
    public SetStringOutputArrayDelegate SetBand990Text { get; set; }
    public SetStringOutputArrayDelegate SetBand3000Text { get; set; }
    public SetStringOutputArrayDelegate SetBand9900Text { get; set; }
    public SetStringOutputArrayDelegate SetBassText { get; set; }
    public SetStringOutputArrayDelegate SetTrebleText { get; set; }
    public SetUshortOutputArrayDelegate SetSurroundF { get; set; }
    public SetUshortOutputArrayDelegate SetSurroundLevelF { get; set; }
    public SetUshortOutputArrayDelegate SetBassEnhancementF { get; set; }
    public SetUshortOutputArrayDelegate SetBassLevelF { get; set; }
    public SetUshortOutputArrayDelegate SetBassCFreq80F { get; set; }
    public SetUshortOutputArrayDelegate SetBassCFreq100F { get; set; }
    public SetUshortOutputArrayDelegate SetBassCFreq125F { get; set; }
    public SetUshortOutputArrayDelegate SetBassCFreq150F { get; set; }
    public SetUshortOutputArrayDelegate SetBassCFreq175F { get; set; }
    public SetUshortOutputArrayDelegate SetBassCFreq200F { get; set; }
    public SetUshortOutputArrayDelegate SetBassCFreq225F { get; set; }
    public SetUshortOutputArrayDelegate SetHighPassF { get; set; }
    public SetUshortOutputArrayDelegate SetMuteF { get; set; }
    public SetUshortOutputArrayDelegate SetVolumeF { get; set; }
    #endregion
  }
}
