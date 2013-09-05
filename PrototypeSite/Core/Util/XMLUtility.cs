using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Core.Util
{
    public class XMLUtility
    {
        public static string Serialize(object o)
        {
            XmlSerializer s = new XmlSerializer(o.GetType());

            MemoryStream ms = new MemoryStream();
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.IndentChars = "\t";
            xmlWriterSettings.Encoding = Encoding.UTF8;
            xmlWriterSettings.OmitXmlDeclaration = true;
            XmlWriter writer = XmlWriter.Create(ms, xmlWriterSettings);

            try
            {
                s.Serialize(writer, o);
                string xmlString = Encoding.UTF8.GetString(ms.ToArray());
                return xmlString.Trim();
            }
            finally
            {
                writer.Close();
                ms.Close();
            }
        }

        public static object Deserialize(string xmlString, Type type)
        {
            XmlSerializer s = new XmlSerializer(type);

            byte[] buffer = Encoding.UTF8.GetBytes(xmlString);

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
    }
}
