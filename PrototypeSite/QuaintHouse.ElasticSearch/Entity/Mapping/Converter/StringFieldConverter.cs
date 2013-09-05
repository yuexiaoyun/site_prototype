using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity.Mapping.Enum;

namespace QuaintHouse.ElasticSearch.Entity.Mapping.Converter
{
    public class StringFieldConverter : BaseFieldConverter
    {
        public override void WriteField(JsonWriter writer, object value, JsonSerializer serializer)
        {
            StringFieldType stringField = (StringFieldType) value;
            if (stringField == null)
                return;

            writer.WritePropertyName("type");
            writer.WriteValue(stringField.GetType());

            if (stringField.TermVector != TermVector.No)
            {
                writer.WritePropertyName("term_vector");
                writer.WriteValue(stringField.TermVector.ToString().ToLower());
            }

            writer.WritePropertyName("analyzer");
            writer.WriteValue(stringField.Analyzer);

            writer.WritePropertyName("index_analyzer");
            writer.WriteValue(stringField.IndexAnalyzer);

            writer.WritePropertyName("search_analyzer");
            writer.WriteValue(stringField.SearchAnalyzer);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof (StringFieldType).IsAssignableFrom(objectType);
        }
    }
}
