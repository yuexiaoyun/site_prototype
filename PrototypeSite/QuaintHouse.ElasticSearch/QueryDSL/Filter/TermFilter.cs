using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Filter.Converter;

namespace QuaintHouse.ElasticSearch.QueryDSL.Filter
{
    [JsonConverter(typeof(TermFilterConverter))]
    public class TermFilter : BaseFilter
    {
        private string field;
        private string value;

        public TermFilter(string field, string value)
        {
            this.field = field;
            this.value = value;
        }

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
