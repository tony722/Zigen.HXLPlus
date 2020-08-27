using System;
using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;
using AET.Zigen.HxlPlus.CommandObjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AET.Zigen.HxlPlus.Tests {
  [TestClass]
  public class SetVideoMatrixTests {
    private SetVideoMatrix api;

    [TestInitialize]
    public void TestInit() {
      Test.HxlPlus.IsHxl88 = 0;
      api = Test.HxlPlus.SetVideoMatrix;
    }


    [TestMethod]
    public void SetOutputToInput_OutputOutOfBounds_GeneratesError() {
      api.SetOutputToInput(0, 1);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus-SetVideoMatrix.SetOutputToInput(0,1): Invalid output #0.");
      api.SetOutputToInput(10, 1);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus-SetVideoMatrix.SetOutputToInput(10,1): Invalid output #10.");
    }

    [TestMethod]
    public void SetOutputToInput_InputOutOfBounds_GeneratesError() {
      api.SetOutputToInput(1,0);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus-SetVideoMatrix.SetOutputToInput(1,0): Invalid input #0.");
      api.SetOutputToInput(1, 10);
      ErrorMessage.LastErrorMessage.Should().Be("HxlPlus-SetVideoMatrix.SetOutputToInput(1,10): Invalid input #10.");
    }

    [TestMethod]
    public void Execute_GeneratesValidHttpRequest() {
      api.SetOutputToInput(2,2);
      api.SetOutputToInput(4,4);
      api.Execute();
      TestHttpClient.Url.Should().Be("http://Test/SetMatrix");
      TestHttpClient.RequestContents.Should().Be(@"{""matrix"":[0,1,0,3]}");
    }

    [TestMethod]
    public void Execute_MatrixSize8_GeneratesValidHttpRequest() {
      Test.HxlPlus.IsHxl88 = 1;
      api.SetOutputToInput(2, 2);
      api.SetOutputToInput(4, 8);
      api.SetOutputToInput(6, 4);
      api.SetOutputToInput(8, 3);
      api.Execute();
      TestHttpClient.Url.Should().Be("http://Test/SetMatrix");
      TestHttpClient.RequestContents.Should().Be(@"{""matrix"":[0,1,0,7,0,3,0,2]}");
    }
  }
}
