using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.Response
{
    public class DocumentResponse : BaseResponse
    {
        [JsonProperty("_index")]
        public string Index { get; set; }
        [JsonProperty("_type")]
        public string Type { get; set; }
        [JsonProperty("_id")]
        public int Id { get; set; }
        [JsonProperty("_version")]
        public long Version { get; set; }
    }
}
