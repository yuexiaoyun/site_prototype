using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace QuaintHouse.ElasticSearch.Utils
{
    public static class XMLUtility
    {
        public static string Serialize(object o)
        {
            return Serialize(o, Encoding.UTF8);
        }

        public static string Serialize(object o, Encoding encoding)
        {
            XmlSerializer s = new XmlSerializer(o.GetType());

            MemoryStream ms = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.IndentChars = "\t";
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.Encoding = encoding;
            xmlWriterSettings.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(ms, xmlWriterSettings);

            try
            {
                s.Serialize(writer, o);
                string xmlString = Encoding.UTF8.GetString(ms.ToArray());
                return xmlString;
            }
            finally
            {
                writer.Close();
                ms.Close();
            }

        }

        public static void Serialize(object o, TextWriter writer)
        {
            XmlSerializer s = new XmlSerializer(o.GetType());
            s.Serialize(writer, o);
        }

        public static object Deserialize(string xmlString, Type type)
        {
            return Deserialize(xmlString, type, Encoding.UTF8);
        }

        public static object Deserialize(string xmlString, Type type, Encoding encoding)
        {
            XmlSerializer s = new XmlSerializer(type);
            byte[] buffer = encoding.GetBytes(xmlString);
            MemoryStream ms = new MemoryStream(buffer);
            XmlReader reader = new XmlTextReader(ms);

            try
            {
                object o = s.Deserialize(reader);
                return o;
            }
            finally
            {
                reader.Close();
            }
        }

        public static T Deserialize<T>(string xmlString)
        {
            System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(T));
            byte[] buffer = Encoding.UTF8.GetBytes(xmlString);
            MemoryStream ms = new MemoryStream(buffer);
            XmlReader reader = new XmlTextReader(ms);

            try
            {
                return ConvertToGenericType<T>(s.Deserialize(reader));
            }
            finally
            {
                reader.Close();
            }
        }

        private static T ConvertToGenericType<T>(object obj)
        {
            if (obj is T)
                return (T)obj;
            else
            {
                try
                {
                    return (T)Convert.ChangeType(obj, typeof(T));
                }
                catch (InvalidCastException)
                {
                    return default(T);
                }
            }
        }

        public static T CreateInstanceFromXml<T>(string filename) where T : new()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            XmlReader reader = new XmlTextReader(filename);
            try
            {
                return (T)xmlSerializer.Deserialize(reader);
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
