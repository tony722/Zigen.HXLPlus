using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AET.Unity.SimplSharp;
using AET.Unity.SimplSharp.HttpClient;
using AET.Unity.SimplSharp.Timer;

namespace AET.Zigen.HxlPlus.Tests {
  static class Test {
    public static TestErrorMessageHandler ErrorMessage { get; } = new TestErrorMessageHandler();

    public static TestHttpClient HttpClient { get; } = new TestHttpClient();

    public static TestTimer Timer { get; } = new TestTimer() {ElapseImmediately = true};
    public static TestMutex Mutex { get; } = new TestMutex();

    public static HxlPlus HxlPlus { get; } = new HxlPlus();
  }
}
