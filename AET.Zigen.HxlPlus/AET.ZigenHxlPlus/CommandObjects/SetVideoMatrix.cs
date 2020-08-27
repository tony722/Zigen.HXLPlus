 using System;
using System.Data.Common;
using AET.Unity.SimplSharp;
using AET.Unity.RestClient;
using Newtonsoft.Json;
using System.Linq;

namespace AET.Zigen.HxlPlus.CommandObjects {

  public class SetVideoMatrix : SetMatrix {
    public SetVideoMatrix() : base("/SetMatrix") { }

    internal override void InitMatrix() {
      if (HxlPlus.IsHxl88 == 1) {
        InputCount = 8;
        OutputCount = 8;
      }
      else {
        InputCount = 4;
        OutputCount = 4;
      }
    }
  }
}

