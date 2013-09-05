using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.Response
{
    public class BaseResponse
    {
        [JsonProperty("ok")]
        public bool Success { get; set; }

        [JsonProperty("error")]
        public string ErrorMessage { get; set; }

        [JsonProperty("acknowledged")]
        public bool Acknowledged { get; set; }
    }
}
