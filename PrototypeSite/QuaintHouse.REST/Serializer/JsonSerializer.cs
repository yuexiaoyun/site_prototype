using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace QuaintHouse.REST.Serializer
{
    public class JsonSerializer : ISerializer
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings();
        static JsonSerializer()
        {
            settings.ContractResolver = new LowercaseContractResolver();
            settings.Converters.Add(new CustomerDateTimeConverter());
            settings.Converters.Add(new UpperCaseStringEnumConverter());
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }


    #region Json.Net Customer Extension

    class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }

    class CustomerDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                if ((objectType == typeof(DateTime?)))
                    return null;
                else
                    throw new JsonSerializationException("Cannot convert null value to DateTime");
            }
            if (reader.TokenType == JsonToken.Date)
            {
                return reader.Value;
            }
            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException(string.Format("Unexpected token parsing date. Expected String, got {0}.", reader.TokenType));
            }
            string str = reader.Value.ToString();
            if (string.IsNullOrEmpty(str))
            {
                return DateTime.MinValue;
            }
            try
            {
                return DateTime.ParseExact(str, RESTConstants.JSON_DATETIME_FORMAT, CultureInfo.InvariantCulture);
            }
            catch (FormatException formatException)
            {
                throw new JsonSerializationException("DateFormat must be: MM/dd/yyyy'T'HH:mm:ss", formatException);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value == null)
                return;
            if (!(value is DateTime))
                throw new JsonSerializationException("Unexpected value when converting date. Expected DateTime");
            DateTime time = (DateTime)value;
            writer.WriteValue(time.ToString(RESTConstants.JSON_DATETIME_FORMAT, CultureInfo.InvariantCulture));
        }
    }

    public class UpperCaseStringEnumConverter : StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                Enum enum2 = (Enum)value;
                string first = enum2.ToString("G");
                if (char.IsNumber(first[0]) || (first[0] == '-'))
                {
                    writer.WriteValue(value);
                }
                else
                {
                    writer.WriteValue(first.ToUpper());
                }
            }
        }
    }

    #endregion
}
