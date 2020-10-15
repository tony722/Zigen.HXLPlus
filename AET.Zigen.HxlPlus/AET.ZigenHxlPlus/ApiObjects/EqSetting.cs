using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AET.Unity.SimplSharp;
using Newtonsoft.Json.Linq;

namespace AET.Zigen.HxlPlus.ApiObjects {
  public class EqSetting {
    private short currentValue;
    private double currentValueScaled;

    public EqSetting() {
      FeedbackDelegate = delegate { };
      TextFeedbackDelegate = delegate { };
    }

    public EqSetting(AudioSettings audioSettings, string jsonName, SetShortOutputDelegate hxlFeedbackDelegate, SetStringOutputDelegate hxlTextFeedbackDelegate) : this() {
      AudioSettings = audioSettings;
      JsonName = jsonName;
      HxlFeedbackDelegate = hxlFeedbackDelegate;
      HxlTextFeedbackDelegate = hxlTextFeedbackDelegate;
    }

    public AudioSettings AudioSettings { get; set; }
    public string JsonName { get; set; }
    
    public SetShortOutputArrayDelegate FeedbackDelegate { get; set; }
    public SetShortOutputDelegate HxlFeedbackDelegate { get; set; }

    public SetStringOutputArrayDelegate TextFeedbackDelegate { get; set; }
    public SetStringOutputDelegate HxlTextFeedbackDelegate { get; set; }
    
    public short Value {
      get { return currentValue; }
      set {
        var valueScaled = ConvertEqFrom16Bit(value);
        if (currentValueScaled == valueScaled) return;
        AudioSettings.Post(JsonName, valueScaled);
        UpdateFeedback(value, valueScaled);
      }
    }

    public void UpdateFeedback(JObject json) {
      var valueScaled = json[JsonName].Value<double>();
      var value = ConvertEqTo16Bit(valueScaled);
      UpdateFeedback(value, valueScaled);
    }

    public void UpdateFeedback(short value, double valueScaled) {
      currentValue = value;
      currentValueScaled = valueScaled;

      FeedbackDelegate(AudioSettings.Output, value);
      TextFeedbackDelegate(AudioSettings.Output, valueScaled.ToString());
      if (AudioSettings.Output != AudioSettings.HxlPlus.SelectedAudioSettings) return; 
      HxlFeedbackDelegate(value);
      HxlTextFeedbackDelegate(valueScaled.ToString());
    }

    private double ConvertEqFrom16Bit(short value) {
      double o = value;
      o /= 10;
      o = Math.Round(o * 4) / 4;
      return o;
    }
    private short ConvertEqTo16Bit(double? nullableValue) {
      double value = nullableValue ?? 0;
      return (short)(value * 10);
    }
  }
}
