using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query.Converter
{
    public class BoolQueryConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            BoolQuery boolQuery = (BoolQuery) value;
            if (boolQuery == null)
                return;
            /*
             {
                "bool" : {
                    "must" : [{}],
                    "must_not" : [{}],
                    "should" : [],
                    "minimum_should_match" : mini,
                    "boost" : boostvalue
                }
             }
            */

            writer.WriteStartObject();
            writer.WritePropertyName("bool");
            writer.WriteStartObject();
            if (boolQuery.MustQuery != null && boolQuery.MustQuery.Count > 0)
            {
                writer.WritePropertyName("must");
                writer.WriteStartArray();
                foreach (IQuery query in boolQuery.MustQuery)
                {
                    serializer.Serialize(writer, query);
                }
                writer.WriteEndArray();
            }
            if (boolQuery.MustNotQuery != null && boolQuery.MustNotQuery.Count > 0)
            {
                writer.WritePropertyName("must_not");
                writer.WriteStartArray();
                foreach (IQuery query in boolQuery.MustNotQuery)
                {
                    serializer.Serialize(writer, query);
                }
                writer.WriteEndArray();
            }
            if (boolQuery.ShouldQuery != null && boolQuery.ShouldQuery.Count > 0)
            {
                writer.WritePropertyName("should");
                writer.WriteStartArray();
                foreach (IQuery query in boolQuery.ShouldQuery)
                {
                    serializer.Serialize(writer, query);
                }
                writer.WriteEndArray();
            }
            if(Math.Abs(boolQuery.Boost - Constants.DF_Boost) > 0)
            {
                writer.WritePropertyName("boost");
                writer.WriteValue(boolQuery.Boost);
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
            return typeof (BoolQuery).IsAssignableFrom(objectType);
        }
    }
}
