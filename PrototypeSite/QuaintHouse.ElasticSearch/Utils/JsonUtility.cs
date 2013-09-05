using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace QuaintHouse.ElasticSearch.Utils
{
    public class JsonUtility
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings();
        static JsonUtility()
        {
            settings.ContractResolver = new LowercaseContractResolver();
            settings.Converters.Add(new CustomerDateTimeConverter());
            settings.Converters.Add(new UpperCaseStringEnumConverter());
        }

        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        public static T Deserialize<T>(string json)
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
                return DateTime.ParseExact(str, "MM/dd/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture);
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
            writer.WriteValue(time.ToString("MM/dd/yyyy'T'HH:mm:ss", CultureInfo.InvariantCulture));
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


    public class JsonBuilder
    {
        private StringBuilder jsonBuilder;

        private JsonBuilder()
        {
            jsonBuilder = new StringBuilder();
        }

        public static JsonBuilder Init()
        {
            return new JsonBuilder();
        }

        public JsonBuilder StartObject()
        {
            jsonBuilder.Append("{");
            return this;
        }

        public JsonBuilder EndObject()
        {
            jsonBuilder.Append("}");
            return this;
        }

        public JsonBuilder BuildPropertyName(string propertyName)
        {
            jsonBuilder.AppendFormat("\"{0}\"", propertyName);
            return this;
        }

        public JsonBuilder BuildValue(object value)
        {
            jsonBuilder.Append(":");
            if (value is int)
            {
                jsonBuilder.Append(value);
            }
            if(value is decimal)
            {
                jsonBuilder.Append(value);
            }
            if(value is double)
            {
                jsonBuilder.Append(value);
            }
            if(value is string)
            {
                jsonBuilder.AppendFormat("\"{0}\"", value);
            }
            return this;
        }

        public JsonBuilder BuildDelimiter()
        {
            jsonBuilder.Append(",");
            return this;
        }

        public JsonBuilder StartArray()
        {
            jsonBuilder.Append("[");
            return this;
        }

        public JsonBuilder EndArray()
        {
            jsonBuilder.Append("]");
            return this;
        }

        public string GetJson()
        {
            return jsonBuilder.ToString();
        }
    }
}
