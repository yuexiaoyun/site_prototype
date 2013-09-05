using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Util
{
    public class ObjectUtility
    {
        public static object DeepClone(object obj)
        {
            if (obj == null)
                return null;
            using (MemoryStream memory = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memory, obj);
                memory.Position = 0;
                return binaryFormatter.Deserialize(memory);
            }
        }

        public static T DeepClone<T>(T sourceObject) where T : class
        {
            if (sourceObject == null)
                return null;
            return DeepClone((object) sourceObject) as T;
        }

        public static long ObjectSize(object obj)
        {
            if (obj == null)
                return 0;
            using (MemoryStream memory = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memory, obj);
                return memory.Length;
            }
        }

        public static string ToBase64(object obj)
        {
            if (obj == null) return string.Empty;
            using (MemoryStream memory = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memory, obj);
                return Convert.ToBase64String(memory.ToArray());
            }
        }

        public static T DeserializeFromBase64<T>(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return default(T);
            using (MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64String)))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T) formatter.Deserialize(memory);
            }
        }
    }
}
