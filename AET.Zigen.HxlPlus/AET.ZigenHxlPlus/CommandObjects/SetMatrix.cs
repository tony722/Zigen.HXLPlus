using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AET.Unity.RestClient;
using AET.Unity.SimplSharp;
using Newtonsoft.Json;

namespace AET.Zigen.HxlPlus.CommandObjects {
  public abstract class SetMatrix : ApiCommandObject {
    private IMutex mutex = new CrestronMutex();
    private ushort outputCount;
    private int[] lastSentMatrix;
    private HxlPlus hxlPlus;

    protected SetMatrix(string url) : base(url) { }

    public HxlPlus HxlPlus {
      get { return hxlPlus; }
      set {
        hxlPlus = value;
        base.RestClient = value;
      }
    }

    abstract internal void InitMatrix();
  
    public ushort MatrixNeedsSending {
      get { return (ushort)(Matrix == lastSentMatrix ? 0 : 1); }
    }

    protected ushort OutputCount {
      set {
        Matrix = new int[value];
        outputCount = value;
      }
      get { return outputCount; }
    }

    protected ushort InputCount { set; get; }

    public void SwitchOutputToInput(ushort output, ushort input) {
      try {
        mutex.Enter();
        if (SetOutputToInput(output, input) == 0) return;
        if (lastSentMatrix != null && lastSentMatrix.SequenceEqual(Matrix)) return;
        lastSentMatrix = (int[])Matrix.Clone();
        Execute();
      } catch (Exception ex) {
        ErrorMessage.Error("HxlPlus-{0}.SwitchOutputToInput({1},{2}): {3}.", this.GetType().Name, output, input, ex.Message);
      }
      finally {
        mutex.Exit();
      }
    }

    public ushort SetOutputToInput(ushort output, ushort input) {
      if (output < 1 || output > OutputCount) {
        ErrorMessage.Warn("HxlPlus-{0}.SetOutputToInput({1},{2}): Invalid output #{1}.", this.GetType().Name, output, input);
        return 0;
      }

      if (input < 1 || input > InputCount) {
        ErrorMessage.Warn("HxlPlus-{0}.SetOutputToInput({1},{2}): Invalid input #{2}.", this.GetType().Name, output, input);
        return 0;
      }
      Matrix[output - 1] = input - 1;
      return 1;
    }

    [JsonProperty("matrix")]
    internal int[] Matrix { get; set; }

    public override bool RequiredFieldsAreValid() {
      return true;
    }
  }
}
