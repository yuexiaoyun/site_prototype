using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.Entity.Mapping.Converter
{
    public class DateFieldConverter : BaseFieldConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof (DateFieldType).IsAssignableFrom(objectType);
        }

        public override void WriteField(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DateFieldType dateField = (DateFieldType) value;
            if (dateField == null)
                return;

            writer.WritePropertyName("type");
            writer.WriteValue(dateField.GetType());
            if (dateField.PrecisionStep != DefaultConstants.Date_PrecisionStep)
            {
                writer.WritePropertyName("precision_step");
                writer.WriteValue(dateField.PrecisionStep);
            }
            if (dateField.IgnoreMalformed != DefaultConstants.Date_IgnoreMalformed)
            {
                writer.WritePropertyName("ignore_malformed");
                writer.WriteValue(dateField.IgnoreMalformed);
            }
            if (!string.IsNullOrEmpty(dateField.Format))
            {
                writer.WritePropertyName("format");
                writer.WriteValue(dateField.Format);
            }
        }
    }
}
