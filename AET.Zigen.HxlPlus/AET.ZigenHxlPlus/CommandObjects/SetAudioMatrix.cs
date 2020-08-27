using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace AET.Zigen.HxlPlus.CommandObjects {
  public class SetAudioMatrix : SetMatrix {
    public SetAudioMatrix() : base("/SetAudioMatrix") { }
    internal override void InitMatrix() {
      if (HxlPlus.IsHxl88 == 1) {
        OutputCount = 12;
        InputCount = 8;
      } else {
        OutputCount = 8;
        InputCount = 4;
      }
    }
  }
}

