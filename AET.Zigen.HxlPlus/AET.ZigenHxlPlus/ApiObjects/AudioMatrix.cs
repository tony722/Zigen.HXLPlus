using System;

namespace AET.Zigen.HxlPlus.ApiObjects {
  public class AudioMatrix : MatrixObject {

    public AudioMatrix() : base("/SetAudioMatrix", "/GetAudioSettings") { }

    internal override int OutputCount { get { return HxlPlus.IsHxl88 == 1 ? 12 : 8;  } }
    internal override int InputCount { get { return HxlPlus.IsHxl88 == 1 ? 8 : 4;}}

    public void Poll() {
      var json = @"{""output"":""none""}";
      var response = HxlPlus.HttpPost(GetUrl, json);
      ParseMatrix(response, HxlPlus.SetAudioOutF);
    }
  }
}

