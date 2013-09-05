using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query.Converter
{
    public class PrefixQueryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            PrefixQuery query = (PrefixQuery) value;
            if (query == null)
                return;

            /*
             * { "prefix" : { "user" :  { "value" : "ki", "boost" : 2.0 } } }
             */

            writer.WriteStartObject();
            writer.WritePropertyName("prefix");
            writer.WriteStartObject();
            writer.WritePropertyName(query.Field);
            writer.WriteStartObject();
            writer.WritePropertyName("prefix");
            writer.WriteValue(query.Value);
            if (Math.Abs(query.Boost - Constants.DF_Boost) > 0)
            {
                writer.WritePropertyName("boost");
                writer.WriteValue(query.Boost);
            }
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
            return typeof (PrefixQuery).IsAssignableFrom(objectType);
        }
    }
}
