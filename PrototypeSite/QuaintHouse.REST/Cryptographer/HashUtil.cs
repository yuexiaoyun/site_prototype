using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace QuaintHouse.REST.Cryptographer
{
    public static class HashUtil
    {
        public static string SHA1Sign(string message, string key, Encoding encoding)
        {
            HMACSHA1 hamc = new HMACSHA1();
            hamc.Key = Convert.FromBase64String(key);
            byte[] bytes = hamc.ComputeHash(encoding.GetBytes(message));
            return Convert.ToBase64String(bytes);
        }

    }
}
