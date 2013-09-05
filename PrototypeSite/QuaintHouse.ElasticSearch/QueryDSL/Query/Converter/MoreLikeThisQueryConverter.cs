using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query.Converter
{
    public class MoreLikeThisQueryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            MoreLikeThisQuery query = (MoreLikeThisQuery) value;
            if (query == null)
                return;

            writer.WriteStartObject();
            writer.WritePropertyName("more_like_this");
            writer.WriteStartObject();
            writer.WritePropertyName("fields");
            writer.WriteStartArray();
            foreach (var field in query.Fields)
            {
                writer.WriteValue(field);
            }
            writer.WriteEndArray();
            writer.WritePropertyName("like_text");
            writer.WriteValue(query.LikeText);
            if (query.MinTermFreq != 2)
            {
                writer.WritePropertyName("min_term_freq");
                writer.WriteValue(query.MinTermFreq);
            }
            if (query.MaxQueryTerm != 25)
            {
                writer.WritePropertyName("max_query_terms");
                writer.WriteValue(query.MaxQueryTerm);
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
            return typeof (MoreLikeThisQuery).IsAssignableFrom(objectType);
        }
    }
}
