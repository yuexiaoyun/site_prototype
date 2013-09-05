using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Filter.Converter
{
    public class BoolFilterConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            BoolFilter boolFilter = (BoolFilter) value;
            if (boolFilter == null)
                return;

            /*
             * "bool" : {
             *  "must" : {},
             *  "must_not" : {},
             *  "should" : [{},{}]
             *  }
             */

            writer.WriteStartObject();
            writer.WritePropertyName("bool");
            writer.WriteStartObject();
            if (boolFilter.MustFilters != null && boolFilter.MustFilters.Count > 0)
            {
                writer.WritePropertyName("must");
                writer.WriteStartArray();
                foreach (var mustFilter in boolFilter.MustFilters)
                {
                    serializer.Serialize(writer, mustFilter);
                }
                writer.WriteEndArray();
            }
            if(boolFilter.MustNotFilter != null && boolFilter.MustNotFilter.Count > 0)
            {
                writer.WritePropertyName("must_not");
                writer.WriteStartArray();
                foreach (var mustNotFilter in boolFilter.MustNotFilter)
                {
                    serializer.Serialize(writer, mustNotFilter);
                }
                writer.WriteEndArray();
            }
            if(boolFilter.ShouldFilter != null && boolFilter.ShouldFilter.Count > 0)
            {
                writer.WritePropertyName("should");
                writer.WriteStartArray();
                foreach (var shouldFilter in boolFilter.ShouldFilter)
                {
                    serializer.Serialize(writer, shouldFilter);
                }
                writer.WriteEndArray();
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
            return typeof (BoolFilter).IsAssignableFrom(objectType);
        }
    }
}
