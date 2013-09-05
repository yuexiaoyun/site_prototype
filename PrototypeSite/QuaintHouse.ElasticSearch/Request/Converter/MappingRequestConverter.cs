using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.Request.Converter
{
    public class MappingRequestConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            MappingRequest request = (MappingRequest) value;
            if (request == null)
                return;

            writer.WriteStartObject();
            writer.WritePropertyName(request.Type);
            serializer.Serialize(writer, request.Mapping);
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (MappingRequest).IsAssignableFrom(objectType);
        }
    }
}
