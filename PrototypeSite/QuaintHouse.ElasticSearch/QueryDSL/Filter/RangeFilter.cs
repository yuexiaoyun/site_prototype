using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Filter.Converter;

namespace QuaintHouse.ElasticSearch.QueryDSL.Filter
{
    [JsonConverter(typeof(RangeFilterConverter))]
    public class RangeFilter : BaseFilter
    {
        private string field;
        private string from;
        private string to;
        private bool includeLower = true;
        private bool includeUpper = true;

        public RangeFilter(string field, string from, string to)
        {
            this.field = field;
            this.from = from;
            this.to = to;
        }

        public string Field
        {
            get { return field; }
            set { field = value; }
        }

        public string From
        {
            get { return from; }
            set { from = value; }
        }

        public string To
        {
            get { return to; }
            set { to = value; }
        }

        public bool IncludeLower
        {
            get { return includeLower; }
            set { includeLower = value; }
        }

        public bool IncludeUpper
        {
            get { return includeUpper; }
            set { includeUpper = value; }
        }
    }
}
