using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Utils;

namespace QuaintHouse.ElasticSearch.Action.Search
{
    public class SearchActionResult<T> : IActionResult
    {
        [JsonProperty("took")]
        public int TookTime { get; set; }
        [JsonProperty("timed_out")]
        public bool TimeOut { get; set; }
        [JsonProperty("_shards")]
        public ShardResult Shards { get; set; }
        [JsonProperty("hits")]
        public HitResult<T> HitResult { get; set; }

        //public List<string> GetHitsSource()
        //{
        //    List<string> sources = new List<string>();
        //    foreach (Hit hit in HitResult.Hits)
        //    {
        //        sources.Add(hit.SourceResult);
        //    }
        //    return sources;
        //}

        public List<T> GetHits()
        {
            List<T> hits = new List<T>();
            foreach (var hit in HitResult.Hits)
            {
                hits.Add(hit.SourceResult);
            }
            return hits;
        }
    }

    public class ShardResult
    {
        [JsonProperty("total")]
        public int TotalNum { get; set; }
        [JsonProperty("successful")]
        public int SuccessfulNum { get; set; }
        [JsonProperty("failed")]
        public int FailedNum { get; set; }
    }

    public class HitResult<T>
    {
        [JsonProperty("total")]
        public int TotalNum { get; set; }
        [JsonProperty("max_score")]
        public float MaxScore { get; set; }
        [JsonProperty("hits")]
        public Hit<T>[] Hits { get; set; }
    }

    public class Hit<T>
    {
        [JsonProperty("_index")]
        public string Index { get; set; }
        [JsonProperty("_type")]
        public string Type { get; set; }
        [JsonProperty("_id")]
        public string DocumentId { get; set; }
        [JsonProperty("_score")]
        public float Score { get; set; }
        [JsonProperty("_source")]
        public T SourceResult { get; set; }
    }

    [JsonConverter(typeof(SourceResultConverter))]
    public class SourceResult
    {
        public string Source { get; set; }
    }

    public class SourceResultConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return string.Empty;
            }
            if (reader.TokenType == JsonToken.None)
            {
                return string.Empty;
            }
            return reader.Value.ToString();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}
