using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeIT.MemCached;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject.Core.Cache
{
    [TestClass]
    public class MemcacheHelperTest : BaseTest
    {
        private const string cacheGroup = "TestCache";
        private MemcachedClient client;
        protected override void DoPrepare()
        {
            if (!MemcachedClient.Exists(cacheGroup))
                MemcachedClient.Setup(cacheGroup, new[] {"172.16.85.52"});
            client = MemcachedClient.GetInstance(cacheGroup);
            client.KeyPrefix = cacheGroup + "_";
            client.ConnectTimeout = 5000;
            client.SendReceiveTimeout = 10000;
            client.MinPoolSize = 1;
            client.MaxPoolSize = 1;
        }

        [TestMethod]
        public void MemcachedClientTest()
        {
            client.Add("FirstCache", 1, DateTime.Now.AddSeconds(3600));
            var value = client.Get("FirstCache");

            Assert.IsNotNull(value);
            Assert.AreEqual(value, 1);
        }

    }


}
