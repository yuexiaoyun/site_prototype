using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query.Converter
{
    public class TermQueryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            TermQuery termQuery = (TermQuery) value;
            if (termQuery == null)
                return;

            /*
             * {
             *    "term" : { "field" : { "term" : "value", "boost" : boost } }
             * }    
             */

            writer.WriteStartObject();
            writer.WritePropertyName("term");
            writer.WriteStartObject();
            writer.WritePropertyName(termQuery.Field);
            writer.WriteStartObject();
            writer.WritePropertyName("term");
            writer.WriteValue(termQuery.Value);
            if (Math.Abs(termQuery.Boost - 1.0) > 0)
            {
                writer.WritePropertyName("boost");
                writer.WriteValue(termQuery.Boost);
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
            return typeof (TermQuery).IsAssignableFrom(objectType);
        }
    }
}
