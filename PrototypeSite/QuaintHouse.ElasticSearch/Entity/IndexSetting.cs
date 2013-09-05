using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.Entity
{
    public class IndexSetting
    {
        private int shards;
        private int replicas;

        [JsonProperty("number_of_shards")]
        public int Shards
        {
            get { return shards; }
            set { shards = value; }
        }

        [JsonProperty("number_of_replicas")]
        public int Replicas
        {
            get { return replicas; }
            set { replicas = value; }
        }
    }

    public class IndexSettingWrapper
    {
        [JsonProperty("settings")]
        public IndexSetting Settings { get; set; }

        public IndexSettingWrapper(IndexSetting settings)
        {
            Settings = settings;
        }
    }
}
