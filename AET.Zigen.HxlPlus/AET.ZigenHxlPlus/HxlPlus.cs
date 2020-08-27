using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AET.Unity.RestClient;
using AET.Zigen.HxlPlus.CommandObjects;

namespace AET.Zigen.HxlPlus {
  public class HxlPlus : RestClient {
    private ushort isHxl88;

    public HxlPlus() {
      SpaceBetweenCommands = 10;
      SetAudioMatrix = new SetAudioMatrix {HxlPlus = this};
      SetVideoMatrix = new SetVideoMatrix {HxlPlus = this};
    }

    public ushort IsHxl88 {
      get { return isHxl88; }
      set {
        isHxl88 = value;
        SetAudioMatrix.InitMatrix();
        SetVideoMatrix.InitMatrix();
      }
    }

    public ushort Debug {
      set { base.HttpClient.Debug = value; }
    }

    public SetAudioMatrix SetAudioMatrix { get; private set; }
    public SetVideoMatrix SetVideoMatrix { get; private set; }
  }
}
