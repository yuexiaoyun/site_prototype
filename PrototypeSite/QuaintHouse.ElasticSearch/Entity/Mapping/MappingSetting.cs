using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity.Mapping.Enum;

namespace QuaintHouse.ElasticSearch.Entity.Mapping
{
    public interface IMappingSetting
    {
        string GetSettingName();
    }


    public class AllSetting : IMappingSetting
    {
        private bool store;
        private TermVector termVector;
        private string indexAnalyzer;
        public string searchAnalyzer;

        public string GetSettingName()
        {
            return "_all";
        }

        [JsonProperty("store")]
        public bool Store
        {
            get { return store; }
            set { store = value; }
        }

        [JsonProperty("term_vector")]
        public TermVector TermVector
        {
            get { return termVector; }
            set { termVector = value; }
        }

        [JsonProperty("index_analyzer")]
        public string IndexAnalyzer
        {
            get { return indexAnalyzer; }
            set { indexAnalyzer = value; }
        }

        [JsonProperty("search_analyzer")]
        public string SearchAnalyzer
        {
            get { return searchAnalyzer; }
            set { searchAnalyzer = value; }
        }
    }
}
