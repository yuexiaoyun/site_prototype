using System;
using System.Security.Cryptography;
using System.Text;

namespace Util
{
    public class TripleDes
    {
        public string GenerateKeyInBase64()
        {
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            provider.GenerateKey();
            return Convert.ToBase64String(provider.Key);
        }

        public byte[] Encrypt(string plainText)
        {
            byte[] plainBytes = new UTF8Encoding().GetBytes(plainText);
            return Encrypt(plainBytes);
        }

        public byte[] Encrypt(byte[] byteContent)
        {
            TripleDESCryptoServiceProvider provider = Create3DesProvider();
            ICryptoTransform encryptor = provider.CreateEncryptor();
            return encryptor.TransformFinalBlock(byteContent, 0, byteContent.Length);
        }

        public byte [] Decrypt(string cipherText)
        {
            UTF8Encoding utf8Encoding = new UTF8Encoding();
            return Decrypt(utf8Encoding.GetBytes(cipherText));
        }

        public byte[] Decrypt(byte[] cipherBytes)
        {
            TripleDESCryptoServiceProvider provider = Create3DesProvider();
            ICryptoTransform decryptor = provider.CreateDecryptor();
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            return plainBytes;
        }

        private TripleDESCryptoServiceProvider Create3DesProvider()
        {
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            provider.Key = Convert.FromBase64String(KeyInBase64);
            provider.IV = new byte[provider.BlockSize / 8];
            return provider;
        }

        public string KeyInBase64;
    }
}
