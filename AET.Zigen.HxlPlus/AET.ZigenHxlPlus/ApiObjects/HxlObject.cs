using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AET.Unity.RestClient;

namespace AET.Zigen.HxlPlus.ApiObjects {
  public abstract class HxlObject {
    protected HxlObject (string setUrl, string getUrl) {
      SetUrl = setUrl;
      GetUrl = getUrl;
    }

    protected string GetUrl { get; private set; }
    protected string SetUrl { get; private set; }


    public RestClient RestClient { get; set; }

    protected HxlPlus HxlPlus { get { return RestClient as HxlPlus; } }





  }
}
