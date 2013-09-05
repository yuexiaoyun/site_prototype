using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Filter.Converter
{
    public class TermFilterConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            TermFilter termFilter = (TermFilter) value;
            if (termFilter == null)
                return;

            /*
             * {
             *  "term" : { "user" : "kimchy"}
             *  }
             */

            writer.WriteStartObject();
            writer.WritePropertyName("term");
            writer.WriteStartObject();
            writer.WritePropertyName(termFilter.Field);
            writer.WriteValue(termFilter.Value);
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (TermFilter).IsAssignableFrom(objectType);
        }
    }
}
