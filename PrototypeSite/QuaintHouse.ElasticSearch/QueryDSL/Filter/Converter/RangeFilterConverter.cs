using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Filter.Converter
{
    public class RangeFilterConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            RangeFilter filter = (RangeFilter) value;
            if (filter == null)
                return;

            /*
            * {
            *  "range" : {
            *    "field" : { 
            *        "from" : "10", 
            *        "to" : "20", 
            *        "include_lower" : true, 
            *        "include_upper" : true
            *    }
            *   }
            * } 
            */

            writer.WriteStartObject();
            writer.WritePropertyName("range");
            writer.WriteStartObject();
            writer.WritePropertyName(filter.Field);
            writer.WriteStartObject();
            writer.WritePropertyName("from");
            writer.WriteValue(filter.From);
            writer.WritePropertyName("to");
            writer.WriteValue(filter.To);
            writer.WritePropertyName("include_lower");
            writer.WriteValue(filter.IncludeLower);
            writer.WritePropertyName("include_upper");
            writer.WriteValue(filter.IncludeUpper);
            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (RangeFilter).IsAssignableFrom(objectType);
        }
    }
}
