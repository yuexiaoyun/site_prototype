using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Query.Converter;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query
{
    /// <summary>
    /// Exact Match
    /// </summary>
    [JsonConverter(typeof(TermQueryConverter))]
    public class TermQuery : BaseQuery
    {
        private string field;
        private string value;

        public TermQuery(string field, string value)
        {
            this.field = field;
            this.value = value;
        }

        public TermQuery SetBoost(double boost)
        {
            Boost = boost;
            return this;
        }

        public string Field
        {
            get { return field; }
        }

        public string Value
        {
            get { return value; }
        }
    }
}
