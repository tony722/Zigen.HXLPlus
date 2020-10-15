using System;
using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;
using AET.Zigen.HxlPlus.ApiObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.HxlPlus.Tests {
  [TestClass]
  public class VideoMatrixTests {
    
    private VideoMatrix api;
    private HxlPlus hxl;

    [TestInitialize]
    public void TestInit() {
      ErrorMessage.Clear();
      TestHttpClient.Clear();

      hxl = Test.HxlPlus;
      hxl.IsHxl88 = 1;
      api = hxl.VideoMatrix;
    }

    [TestMethod]
    public void SwitchInputToOutput_OutputOutOfBounds_GeneratesError() {
      api.SwitchInputToOutput(1, 0);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus.VideoMatrix.Output(0): Must be between 1 and 8");
      api.SwitchInputToOutput(1, 9);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus.VideoMatrix.Output(9): Must be between 1 and 8");
    }


    [TestMethod]
    public void SwitchInputToOutput_InputOutOfBounds_GeneratesError() {
      api.SwitchInputToOutput(0, 1);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus.VideoMatrix.Input(0): Must be between 1 and 8");
      api.SwitchInputToOutput(9, 1);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus.VideoMatrix.Input(9): Must be between 1 and 8");
    }


    
    [TestMethod]
    public void Execute_GeneratesValidHttpRequest() {
      api.SwitchInputToOutput(2,2);
      TestHttpClient.Url.Should().Be("http://test/SetMatrix");
      TestHttpClient.RequestContents.Should().Be(@"{""switch"":{""input"":1,""output"":1}}");
    }

    [TestMethod]
    public void Poll_PopulatesVideoMatrixCorrectly() {
      var outputs = new int[8];
      hxl.SetVideoOutF = (index, value) => outputs[index - 1] = value;
      TestHttpClient.ResponseContents = @"{""matrix"": [7,6,5,4,3,2,1,0]}";
      api.Poll();
      TestHttpClient.Url.Should().Be("http://test/GetMatrix");
      outputs.Should().BeEquivalentTo(new int[] {8, 7, 6, 5, 4, 3, 2, 1});
    }

    [TestMethod]
    public void SwitchInputToOutput_SplusDelegateIsTriggered() {
      int[] outputs = new int[8];
      hxl.SetVideoOutF = (idx, value) => outputs[idx] = value;
      hxl.SwitchVideoInputToOutput(2, 3);
      outputs[3].Should().Be(2);
    }
  }
}
