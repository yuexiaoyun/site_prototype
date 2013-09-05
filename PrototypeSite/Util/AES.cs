using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Util
{
    public class AES
    {
        public string KeyInBase64
        {
            get { return "QoyPMd/+ZKQ4HuwVTIqmDg=="; }
        }
        private static int KeySize
        {
            get { return 128; }
        }
        public byte[] Encrypt(string plainText)
        {
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            return Encrypt(plainBytes);
        }
        public byte[] Encrypt(byte[] byteContent)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Convert.FromBase64String(KeyInBase64);

            ICryptoTransform cryptoTransform = aes.CreateEncryptor();
            byte[] cipherBytes = cryptoTransform.TransformFinalBlock(byteContent, 0, byteContent.Length);
            return cipherBytes;
        }

        public byte[] Decrypt(byte[] cipherBytes)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Convert.FromBase64String(KeyInBase64);

            ICryptoTransform cryptoTransform = aes.CreateDecryptor();
            byte[] decryptedBytes = cryptoTransform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return decryptedBytes;
        }
    }
}
