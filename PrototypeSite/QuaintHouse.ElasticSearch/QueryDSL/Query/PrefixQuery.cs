using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Query.Converter;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query
{
    [JsonConverter(typeof(PrefixQueryConverter))]
    public class PrefixQuery : BaseQuery
    {
        private string field;
        private string value;

        public string Field
        {
            get { return field; }
            set { field = value; }
        }

        public string Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
