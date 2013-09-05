using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Filter.Converter;

namespace QuaintHouse.ElasticSearch.QueryDSL.Filter
{
    [JsonConverter(typeof(ExistsFilterConverter))]
    public class ExistsFilter : BaseFilter
    {
        private string filed;

        public ExistsFilter(string filed)
        {
            this.filed = filed;
        }

        public string Filed
        {
            get { return filed; }
            set { filed = value; }
        }
    }
}
