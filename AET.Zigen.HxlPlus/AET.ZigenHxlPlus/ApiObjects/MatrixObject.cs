using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using Newtonsoft.Json.Linq;

namespace AET.Zigen.HxlPlus.ApiObjects {
  public abstract class MatrixObject : HxlObject {

    protected MatrixObject(string setUrl, string getUrl) : base(setUrl, getUrl) { }

    internal abstract int OutputCount { get; }
    internal abstract int InputCount { get; }


    public void SwitchInputToOutput(int input, int output) {
      if (IOsAreValid(input, output)) {
        string json = string.Format(@"{{""switch"":{{""input"":{0},""output"":{1}}}}}", input - 1, output - 1);
        RestClient.HttpPost(SetUrl, json, null);
      }
    }

    public bool IOsAreValid(int input, int output) {
      if (input < 1 || input > InputCount) return ApiObject.FalseWithErrorMessage("HxlPlus.{0}.Input({1}): Must be between 1 and {2}", this.GetType().Name, input, InputCount);
      if (output < 1 || output > OutputCount) return ApiObject.FalseWithErrorMessage("HxlPlus.{0}.Output({1}): Must be between 1 and {2}", this.GetType().Name, output, OutputCount);
      return true;
    }

    protected void ParseMatrix(string response, SetUshortOutputArrayDelegate splusOutputArray) {
      var json = JObject.Parse(response);
      var matrix = json["matrix"] as JArray;
      if (matrix == null) {
        ErrorMessage.Warn("HxlPlus.{0}.Poll() HxlPlus did not return a 'matrix' object in response to {0}", GetType().Name, GetUrl);
        return;
      }
      for (ushort i = 1; i <= OutputCount; i++) splusOutputArray(i, (ushort)(matrix[i - 1].Value<int>() + 1));
    }
  }
}
