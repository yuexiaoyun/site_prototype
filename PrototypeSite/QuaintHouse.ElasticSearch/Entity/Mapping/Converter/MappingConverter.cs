using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.Entity.Mapping.Converter
{
    public class MappingConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Mapping mapping = (Mapping) value;
            if (mapping == null)
                return;

            writer.WriteStartObject();
            if (mapping.MappingSettings != null && mapping.MappingSettings.Count > 0)
            {
                foreach (IMappingSetting setting in mapping.MappingSettings)
                {
                    writer.WritePropertyName(setting.GetSettingName());
                    serializer.Serialize(writer, setting);
                }
            }
            if(mapping.FieldTypes != null && mapping.FieldTypes.Count > 0)
            {
                writer.WritePropertyName("properties");
                writer.WriteStartObject();
                foreach (var fieldType in mapping.FieldTypes)
                {
                    writer.WritePropertyName(fieldType.Key);
                    serializer.Serialize(writer, fieldType.Value);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (Mapping).IsAssignableFrom(objectType);
        }
    }
}
