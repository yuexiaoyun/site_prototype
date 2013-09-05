using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity.Mapping.Enum;

namespace QuaintHouse.ElasticSearch.Entity.Mapping.Converter
{
    public abstract class BaseFieldConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            BaseFieldType field = (BaseFieldType) value;
            if (field == null)
                return;

            writer.WriteStartObject();
            WriteField(writer, value, serializer);
            if (!string.IsNullOrEmpty(field.IndexName))
            {
                writer.WritePropertyName("index_name");
                writer.WriteValue(field.IndexName);
            }
            if (field.Store != DefaultConstants.Default_StoreType)
            {
                writer.WritePropertyName("store");
                writer.WriteValue(field.Store.ToString().ToLower());
            }
            if (field.IndexType != DefaultConstants.Default_IndexType)
            {
                writer.WritePropertyName("index");
                writer.WriteValue(field.IndexType.ToString().ToLower());
            }
            if (Math.Abs(field.Boost - DefaultConstants.Default_Boost) > 0)
            {
                writer.WritePropertyName("boost");
                writer.WriteValue(field.Boost);
            }
            if (field.IncludeInAll != DefaultConstants.Default_IncludeInAll)
            {
                writer.WritePropertyName("include_in_all");
                writer.WriteValue(field.IncludeInAll);
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public abstract void WriteField(JsonWriter writer, object value, JsonSerializer serializer);
    }
}
