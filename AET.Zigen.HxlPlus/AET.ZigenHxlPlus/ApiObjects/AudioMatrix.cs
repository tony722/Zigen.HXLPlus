using System;

namespace AET.Zigen.HxlPlus.ApiObjects {
  public class AudioMatrix : MatrixObject {

    public AudioMatrix() : base("/SetAudioMatrix", "/GetAudioSettings") { }

    internal override int OutputCount { get { return HxlPlus.IsHxl88 == 1 ? 12 : 8;  } }
    internal override int InputCount { get { return HxlPlus.IsHxl88 == 1 ? 8 : 4;}}

    public void Poll() {
      Poll(null);
    }

    /// <summary>
    /// Deprecated: This calls the same api url as AllAudioSettings. That should be used instead.
    /// </summary>
    public void Poll(Action callback) {
      var json = @"{""output"":""none""}";
      RestClient.HttpPost(GetUrl, json, (response) => {
        ParseMatrix(response, HxlPlus.SetAudioOutF);
        if (callback != null) callback();
      });
    }
  }
}

