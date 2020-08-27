using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AET.Unity.RestClient;
using AET.Zigen.HxlPlus.CommandObjects;

namespace AET.Zigen.HxlPlus {
  public class Config {
    private ushort isHxl88;

    public Config() {
      RestClient = new RestClient();
      RestClient.SpaceBetweenCommands = 0;
    }

    public RestClient RestClient { get; private set; }

    public ushort IsHxl88 {
      get { return isHxl88; }
      set {
        isHxl88 = value;
        SetAudioMatrix.InitMatrix();
        SetVideoMatrix.InitMatrix();
      }
    }
  }
}
