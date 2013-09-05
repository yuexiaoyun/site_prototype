using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity.Mapping;
using QuaintHouse.ElasticSearch.Request.Converter;

namespace QuaintHouse.ElasticSearch.Request
{
    [JsonConverter(typeof(MappingRequestConverter))]
    public class MappingRequest
    {
        private string[] indexes;
        private string type;
        private Mapping mapping;

        public string[] Indexes
        {
            get { return indexes; }
            set { indexes = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public Mapping Mapping
        {
            get { return mapping; }
            set { mapping = value; }
        }
    }
}
