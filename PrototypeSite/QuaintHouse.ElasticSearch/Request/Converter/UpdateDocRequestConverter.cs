using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.Request.Converter
{
    public class UpdateDocRequestConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            UpdateDocRequest request = (UpdateDocRequest) value;
            if (request == null)
                return;

            writer.WriteStartObject();
            writer.WritePropertyName("doc");
            writer.WriteStartObject();
            foreach (var field in request.Fields)
            {
                writer.WritePropertyName(field.Name);
                serializer.Serialize(writer, field.Value);
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (UpdateDocRequest).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
