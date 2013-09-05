using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.Entity.Mapping.Converter
{
    public class BoolFieldConverter : BaseFieldConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof (BoolFieldType).IsAssignableFrom(objectType);
        }

        public override void WriteField(JsonWriter writer, object value, JsonSerializer serializer)
        {
            BoolFieldType boolField = (BoolFieldType) value;
            if (boolField == null)
                return;

            writer.WritePropertyName("type");
            writer.WriteValue(boolField.GetType());
        }
    }
}
