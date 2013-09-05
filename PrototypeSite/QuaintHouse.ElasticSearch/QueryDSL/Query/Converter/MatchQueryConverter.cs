using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query.Converter
{
    public class MatchQueryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            MatchQuery matchQuery = (MatchQuery) value;
            if (matchQuery == null)
                return;

            /*
             *  {"match":{"fieldname":"fieldvalue"}}
             */

            writer.WriteStartObject();
            writer.WritePropertyName("match");
            writer.WriteStartObject();
            writer.WritePropertyName(matchQuery.Field);
            writer.WriteStartObject();
            writer.WritePropertyName("query");
            writer.WriteValue(matchQuery.Value);
            if (matchQuery.QueryType == QueryType.Boolean && matchQuery.Op != Operator.Or)
            {
                writer.WritePropertyName("operator");
                writer.WriteValue(matchQuery.Op.ToString().ToLower());   
            }
            if (matchQuery.QueryType != QueryType.Boolean)
            {
                writer.WritePropertyName("type");
                writer.WriteValue(matchQuery.QueryType.ToString().ToLower());
            }
            if (string.IsNullOrEmpty(matchQuery.Analyzer))
            {
                writer.WritePropertyName("analyzer");
                writer.WriteValue(matchQuery.Analyzer);
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
            return typeof (MatchQuery).IsAssignableFrom(objectType);
        }
    }
}
