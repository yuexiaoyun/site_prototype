using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Filter.Converter
{
    public class AndFilterConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            AndFilter andFilter = (AndFilter) value;
            if (andFilter == null)
                return;
            /*
             * {
             *  "and":{
             *     "filters":[],
             *     "_cache":true
             *   }
             * } 
             */
            writer.WriteStartObject();
            writer.WritePropertyName("and");
            writer.WriteStartObject();
            writer.WritePropertyName("filters");
            writer.WriteStartArray();
            foreach (IFilter filter in andFilter.Filters)
            {
                serializer.Serialize(writer, filter);
            }
            writer.WriteEndArray();
            if (andFilter.Cache)
            {
                writer.WritePropertyName("_cache");
                writer.WriteValue(true);
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (AndFilter).IsAssignableFrom(objectType);
        }
    }
}
