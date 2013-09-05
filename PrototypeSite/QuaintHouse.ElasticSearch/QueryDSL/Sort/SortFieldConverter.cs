using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Sort
{
    public class SortFieldConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            SortField field = (SortField) value;

            if (field == null)
                return;

            writer.WriteStartObject();
            writer.WritePropertyName(field.FieldName);

            writer.WriteStartObject();
            
            writer.WritePropertyName("order");
            writer.WriteValue(field.SortType.ToString().ToLower());

            if(field.SortMode != SortMode.None)
            {
                writer.WritePropertyName("mode");
                writer.WriteValue(field.SortMode.ToString().ToLower());
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
            return typeof (SortField).IsAssignableFrom(objectType);
        }
    }
}
