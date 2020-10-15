using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using AET.Unity.RestClient;
using Crestron.SimplSharp.Net.Http;
using AET.Unity.SimplSharp;

namespace AET.Zigen.HxlPlus.ApiObjects {
  public abstract class HxlPlusObject {
    protected HxlPlusObject (string setUrl, string getUrl) {
      SetUrl = setUrl;
      GetUrl = getUrl;
    }

    protected string GetUrl { get; private set; }
    protected string SetUrl { get; private set; }

    public HxlPlus HxlPlus { get; set; }

    internal string Post(string contents) {
      return HxlPlus.HttpPost(SetUrl, contents);
    }

    internal string PostFormatted(string contents, params object[] args) {
      var postContents = string.Format(contents, args);
      return Post(postContents);
    }
    /*
    protected class EQSetting {
      private double scaledValue;
      private short value;

      public string JsonName { get; set; }
      public ushort Output { get; set; }
      public HxlPlus HxlPlus { get; set; }
      public short Value {
        get { return value; }
        set {
          var newScaledValue = ConvertEqFrom16Bit(value);
          if (scaledValue == newScaledValue) return;
          Post(JsonName, newScaledValue);
          ValueF = value;
        }
      }

      public short ValueF {
        set {
          value = value;
          scaledValue = ConvertEqFrom16Bit(value);
          SetAnalogFb(Output, value);
          if (Output == HxlPlus.SelectedAudioSettings) 
          ShowFeedback(value, SetTrebleF, HxlPlus.SetTrebleF);
          ShowFeedback(value.ToString(), SetTrebleText, HxlPlus.SetTrebleText);
        }
      }

      public SetShortOutputArrayDelegate SetAnalogFb { get; set; }
      public SetStringOutputArrayDelegate SetSerialFb { get; set; }

      private void ShowFeedback(short value, SetShortOutputArrayDelegate localDelegate, SetShortOutputDelegate hxlDelegate) {
        localDelegate(Output, value);
        if (Output == HxlPlus.SelectedAudioSettings) hxlDelegate(value);
      }

      private void ShowFeedback(string value, SetStringOutputArrayDelegate localDelegate, SetStringOutputDelegate hxlDelegate) {
        localDelegate(Output, value);
        if (Output == HxlPlus.SelectedAudioSettings) hxlDelegate(value);
      }
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

    }*/
  }
}
