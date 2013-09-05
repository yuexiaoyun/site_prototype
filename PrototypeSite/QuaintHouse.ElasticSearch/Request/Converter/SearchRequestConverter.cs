using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Sort;
using QuaintHouse.ElasticSearch.Utils;

namespace QuaintHouse.ElasticSearch.Request.Converter
{
    public class SearchRequestConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            SearchRequest request = (SearchRequest) value;
            
            if (request == null)
                return;

            writer.WriteStartObject();

            writer.WritePropertyName("from");
            writer.WriteValue(request.From);

            writer.WritePropertyName("size");
            writer.WriteValue(request.Size);

            if (request.Filter != null)
            {
                writer.WritePropertyName("filtered");
                writer.WriteStartObject();
                writer.WritePropertyName("query");
                serializer.Serialize(writer, request.Query);
                writer.WritePropertyName("filter");
                serializer.Serialize(writer, request.Filter);
                writer.WriteEndObject();
            }
            else
            {
                writer.WritePropertyName("query");
                serializer.Serialize(writer, request.Query);
            }

            if (request.Fields != null && request.Fields.Count > 0)
            {
                writer.WritePropertyName("fields");
                writer.WriteStartArray();
                foreach (string field in request.Fields)
                {
                    writer.WriteValue(field);
                }
                writer.WriteEndArray();
            }

            if (request.SortFields != null && request.SortFields.Count > 0)
            {
                writer.WritePropertyName("sort");
                writer.WriteStartArray();
                foreach (SortField field in request.SortFields)
                {
                    serializer.Serialize(writer, field);
                }
                writer.WriteEndArray();
            }

            if (request.Facet != null)
            {
                writer.WritePropertyName("facets");
                serializer.Serialize(writer, request.Facet);
            }

            writer.WritePropertyName("explain");
            writer.WriteValue(request.Explain);

            writer.WritePropertyName("version");
            writer.WriteValue(request.Version);

            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (SearchRequest).IsAssignableFrom(objectType);
        }
    }
}
