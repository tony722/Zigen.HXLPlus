using System;
using System.CodeDom;
using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;
using AET.Zigen.HxlPlus.CommandObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.HxlPlus.Tests {
  [TestClass]
  public class SetAudioMatrixTests {
    private SetAudioMatrix api;

    [TestInitialize]
    public void TestInit() {
      Test.HxlPlus.IsHxl88 = 0;
      api = Test.HxlPlus.SetAudioMatrix;
    }

    [TestMethod]
    public void SetOutputToInput_OutputOutOfBounds_GeneratesError() {
      api.SetOutputToInput(0, 1);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus-SetAudioMatrix.SetOutputToInput(0,1): Invalid output #0.");
      api.SetOutputToInput(13, 1);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus-SetAudioMatrix.SetOutputToInput(13,1): Invalid output #13.");
    }

    [TestMethod]
    public void SetOutputToInput_InputOutOfBounds_GeneratesError() {
      api.SetOutputToInput(1, 0);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus-SetAudioMatrix.SetOutputToInput(1,0): Invalid input #0.");
      api.SetOutputToInput(1, 9);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus-SetAudioMatrix.SetOutputToInput(1,9): Invalid input #9.");
    }

    [TestMethod]
    public void Execute_GeneratesValidHttpRequest() {
      api.SetOutputToInput(2, 2);
      api.SetOutputToInput(4, 4);
      api.Execute();
      TestHttpClient.Url.Should().Be("http://Test/SetAudioMatrix");
      TestHttpClient.RequestContents.Should().Be(@"{""matrix"":[0,1,0,3,0,0,0,0]}");
    }

    [TestMethod]
    public void Execute_MatrixSize8_GeneratesValidHttpRequest() {
      Test.HxlPlus.IsHxl88 = 1;
      api.SetOutputToInput(2, 2);
      api.SetOutputToInput(4, 8);
      api.SetOutputToInput(6, 4);
      api.SetOutputToInput(8, 3);
      api.SetOutputToInput(10, 3);
      api.SetOutputToInput(12, 3);
      api.Execute();
      TestHttpClient.Url.Should().Be("http://Test/SetAudioMatrix");
      TestHttpClient.RequestContents.Should().Be(@"{""matrix"":[0,1,0,7,0,3,0,2,0,2,0,2]}");
    }

    [TestMethod]
    public void MatrixNeedsSending_ValuesUnchanged_DoesNotResend() {
      api.SwitchOutputToInput(2, 2);
      TestHttpClient.RequestContents.Should().Be(@"{""matrix"":[0,1,0,0,0,0,0,0]}");
      TestHttpClient.RequestContents = null;
      api.SwitchOutputToInput(2, 2);
      TestHttpClient.RequestContents.Should().BeNullOrEmpty();
    }
  }
}