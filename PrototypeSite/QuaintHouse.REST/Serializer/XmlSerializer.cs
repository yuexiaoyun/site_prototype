using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace QuaintHouse.REST.Serializer
{
    public class XmlSerializer : ISerializer
    {
        public string Serialize(object o)
        {
            System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(o.GetType());

            MemoryStream ms = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.IndentChars = "\t";
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.Encoding = Encoding.UTF8;

            XmlSerializerNamespaces emptyNameSpaces = new XmlSerializerNamespaces();
            emptyNameSpaces.Add("", "");

            XmlWriter writer = XmlWriter.Create(ms, xmlWriterSettings);

            try
            {
                s.Serialize(writer, o, emptyNameSpaces);
                string xmlString = Encoding.UTF8.GetString(ms.ToArray());
                return xmlString;
            }
            finally
            {
                writer.Close();
                ms.Close();
            }
        }

        public T Deserialize<T>(string xmlString)
        {
            System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(T));
            byte[] buffer = Encoding.UTF8.GetBytes(xmlString);
            MemoryStream ms = new MemoryStream(buffer);
            XmlReader reader = new XmlTextReader(ms);

            try
            {
                return this.ConvertToGenericType<T>(s.Deserialize(reader));
            }
            finally
            {
                reader.Close();
            }
        }

        private T ConvertToGenericType<T>(object obj)
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
    }
}
