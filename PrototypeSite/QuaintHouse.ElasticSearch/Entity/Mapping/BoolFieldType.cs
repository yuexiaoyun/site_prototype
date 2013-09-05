using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity.Mapping.Converter;

namespace QuaintHouse.ElasticSearch.Entity.Mapping
{
    [JsonConverter(typeof(BoolFieldConverter))]
    public class BoolFieldType : BaseFieldType
    {
        public override string GetType()
        {
            return "boolean";
        }
    }
}
