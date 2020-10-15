using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using AET.Unity.RestClient;
using Crestron.SimplSharp.Net.Http;
using AET.Unity.SimplSharp;

namespace AET.Zigen.HxlPlus.ApiObjects {
  public abstract class HxlPlusObject {
    protected HxlPlusObject (string setUrl, string getUrl) {
      SetUrl = setUrl;
      GetUrl = getUrl;
    }

    protected string GetUrl { get; private set; }
    protected string SetUrl { get; private set; }

    public HxlPlus HxlPlus { get; set; }

    internal string Post(string contents) {
      return HxlPlus.HttpPost(SetUrl, contents);
    }

    internal string PostFormatted(string contents, params object[] args) {
      var postContents = string.Format(contents, args);
      return Post(postContents);
    }
  }
}
