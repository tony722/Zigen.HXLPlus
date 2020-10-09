using System;
using System.Threading;
using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace AET.Zigen.HxlPlus.Tests {
  [TestClass]
  public class AudioSettingsTests {
    private int GetInt(string key, JObject Json) {
      JToken jToken;
      if (!Json.TryGetValue(key, out jToken)) return 0;
      return jToken.Value<string>().SafeParseInt();
    }
    [TestMethod]
    public void JsonHelperGetInt_InvalidKey_Returns0() {
      var o = JObject.Parse(@"{""mykey"":""eee"",""myOtherKey"":5}");
      GetInt("missingKey", o).Should().Be(0);
      GetInt("mykey", o).Should().Be(0);
      GetInt("myOtherKey", o).Should().Be(5);
    }

    [TestMethod]
    public void Volume_SendsCorrectJson() {
      Test.HxlPlus.AllAudioSettings[3].Volume = 65535;
      Test.HxlPlus.AllAudioSettings[3].Send();
      TestHttpClient.Url.Should().Be("http://test/SetAudioSettings");
      TestHttpClient.RequestContents.Should().Be(@"{""output"":3,""volume"":100}");
    }

    [TestMethod]
    public void Mute_TriggersDelegateCorrectly() {
      ushort muteState = 999;
      Test.HxlPlus.AllAudioSettings[3].SetMuteF = (index, value) => muteState = value;
      Test.HxlPlus.AllAudioSettings[3].Mute = 1;
      Test.HxlPlus.AllAudioSettings[3].Send();
      muteState.Should().Be(1);
    }

    [TestMethod]
    public void Deserialize_ValidData_ReturnsCorrectlyPopulatedObject() {
      var outputs = new int[8];
      Test.HxlPlus.SetAudioOutF = (index, value) => outputs[index - 1] = value;
      var responseString =
        @"{""matrix"":[3,2,1,0,3,2,1,0],""status"":""success"",""audioInfo"":{""audiosel"":""local"",""mute"":true,""volume"":50,""tune mode"":""presets"",""preset"":""flat"",""band0"":5,""band1"":6,""band2"":7,""band3"":8,""band4"":9,""basstone"":10,""treble"":11,""surround"":true,""surrlevel"":1,""basslevel"":31,""bass"":true,""bassfreq"":100,""highpass"":true}}";
      TestHttpClient.ResponseContents = responseString;
      var api = Test.HxlPlus.AllAudioSettings[0];
      ErrorMessage.Clear();
      var wait = new AutoResetEvent(true);
      api.Poll(() => wait.Set());
      wait.WaitOne();
      using (new AssertionScope()) {
        api.TuneMode.Should().Be("presets");
        api.Band115.Should().Be(50);
        api.Band330.Should().Be(60);
        api.Band990.Should().Be(70);
        api.Band3000.Should().Be(80);
        api.Band9900.Should().Be(90);
        api.Bass.Should().Be(100);
        api.Treble.Should().Be(110);
        api.Surround.Should().Be(1);
        api.SurroundLevel.Should().Be(9362);
        api.BassEnhancement.Should().Be(1);
        api.BassCutoff.Should().Be(100);
        api.BassLevel.Should().Be(15996);
        api.HighPass.Should().Be(1);
      }
    }
  }
}
