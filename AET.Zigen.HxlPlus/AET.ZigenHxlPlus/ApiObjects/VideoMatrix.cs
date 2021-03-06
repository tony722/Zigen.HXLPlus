﻿using System;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using Newtonsoft.Json.Linq;

namespace AET.Zigen.HxlPlus.ApiObjects {

  public class VideoMatrix : MatrixObject {
    public VideoMatrix() : base("/SetMatrix", "/GetMatrix") { }
    internal override int OutputCount { get { return HxlPlus.IsHxl88 == 1 ? 8 : 4; } }
    internal override int InputCount { get { return HxlPlus.IsHxl88 == 1 ? 8 : 4; } }

    public void Poll() {
      var response = HxlPlus.HttpGet(GetUrl);
      ParseMatrix(response, HxlPlus.SetVideoOutF);
    }
  }
}

