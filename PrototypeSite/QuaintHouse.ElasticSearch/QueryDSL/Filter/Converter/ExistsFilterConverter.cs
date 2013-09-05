using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Filter.Converter
{
    public class ExistsFilterConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            ExistsFilter existsFilter = (ExistsFilter) value;
            if (existsFilter == null)
                return;
            /*
             * {
             *   "exists" : { "field" : "user" }
             * }
             */
            writer.WriteStartObject();
            writer.WritePropertyName("exists");
            writer.WriteStartObject();
            writer.WritePropertyName("field");
            writer.WriteValue(existsFilter.Filed);
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (ExistsFilter).IsAssignableFrom(objectType);
        }
    }
}
