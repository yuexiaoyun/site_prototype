using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using log4net;
using Microsoft.Practices.Unity;

namespace Util
{
    public class RSACryptoService
    {
        private readonly ILog logger = LogManager.GetLogger(typeof(RSACryptoService));

        private string keyInfo;

        [Dependency("RSAKeyFile")]
        public string KeyFile
        {
            set
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value);
                keyInfo = File.ReadAllText(path);
            }
        }

        public byte[] SignData(string input)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            try
            {
                rsa.FromXmlString(keyInfo);
                return rsa.SignData(Encoding.UTF8.GetBytes(input), CryptoConfig.MapNameToOID("SHA1"));
            }
            catch (Exception ex)
            {
                logger.Error("RSA sign data failed", ex);
                return null;
            }
            finally
            {
                rsa.Clear();
            }
        }

        public bool VerifyData(string input, byte[] hash)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            try
            {
                rsa.FromXmlString(keyInfo);
                return rsa.VerifyData(Encoding.UTF8.GetBytes(input), CryptoConfig.MapNameToOID("SHA1"), hash);
            }
            catch (Exception ex)
            {
                logger.Error("RSA verify data failed", ex);
                return false;
            }
            finally
            {
                rsa.Clear();
            }
        }
    }
}
