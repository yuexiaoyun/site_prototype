using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.Request.Converter
{
    public class BulkRequestConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            BulkRequest request = (BulkRequest) value;
            if (request == null)
                return;

            foreach (var indexItem in request.IndexItems)
            {
                writer.WriteStartObject();
                writer.WritePropertyName(indexItem.ActionType);
                writer.WriteStartObject();
                writer.WritePropertyName("_index");
                writer.WriteValue(indexItem.Index);
                writer.WritePropertyName("_type");
                writer.WriteValue(indexItem.Type);
                writer.WritePropertyName("_id");
                writer.WriteValue(indexItem.DocuemntId);
                writer.WriteEndObject();
                writer.WriteEndObject();
                writer.WriteRawValue("\n");
                if (indexItem.ActionType != "delete")
                {
                    serializer.Serialize(writer, indexItem.DocumentEntity);
                    writer.WriteRawValue("\n");
                }
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (BulkRequest).IsAssignableFrom(objectType);
        }
    }
}
