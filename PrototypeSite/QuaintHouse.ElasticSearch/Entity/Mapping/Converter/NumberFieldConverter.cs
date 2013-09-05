using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.Entity.Mapping.Converter
{
    public class NumberFieldConverter : BaseFieldConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof (NumberFieldType).IsAssignableFrom(objectType);
        }

        public override void WriteField(JsonWriter writer, object value, JsonSerializer serializer)
        {
            NumberFieldType numberField = (NumberFieldType) value;
            if (numberField == null)
                return;
            
            writer.WritePropertyName("type");
            writer.WriteValue(numberField.GetType());
            if (numberField.PrecisionStep != DefaultConstants.Number_PrecisionStep)
            {
                writer.WritePropertyName("precision_step");
                writer.WriteValue(numberField.PrecisionStep);
            }
            if (numberField.IgnoreMalformed != DefaultConstants.Number_IgnoreMalformed)
            {
                writer.WritePropertyName("ignore_malformed");
                writer.WriteValue(numberField.IgnoreMalformed);
            }
        }
    }
}
