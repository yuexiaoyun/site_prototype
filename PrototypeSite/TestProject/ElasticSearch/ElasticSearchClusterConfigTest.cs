using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuaintHouse.ElasticSearch.Entity;
using QuaintHouse.ElasticSearch.Setting;
using QuaintHouse.ElasticSearch.Utils;

namespace TestProject.ElasticSearch
{
    [TestClass]
    public class ElasticSearchClusterConfigTest
    {
        [TestMethod]
        public void SerializerConfigTest()
        {
            ElasticSearchClusterConfig config = ElasticSearchClusterConfig.Create();
            List<ESCluster> esClusters = new List<ESCluster>();
            ESCluster defaultCluster = new ESCluster();
            defaultCluster.Name = "Dev1";
            defaultCluster.Default = true;
            defaultCluster.HttpNodes = new List<ESNode>()
                                           {new ESNode() {Enabled = true, Host = "localhost", Port = 9200}};
            defaultCluster.ThriftNodes = new List<ESNode>()
                                             {
                                                 new ESNode() {Enabled = true, Host = "127.0.0.1", Port = 9500},
                                                 new ESNode() {Enabled = true, Host = "127.0.0.1", Port = 9500}
                                             };
            esClusters.Add(defaultCluster);

            ESCluster devCluster = new ESCluster();
            devCluster.Name = "Dev2";
            devCluster.Default = false;
            devCluster.HttpNodes = new List<ESNode>()
                                       {new ESNode() {Enabled = true, Host = "localhost", Port = 9200}};
            devCluster.ThriftNodes = new List<ESNode>()
                                         {
                                             new ESNode() {Enabled = true, Host = "127.0.0.1", Port = 9500},
                                         };
            esClusters.Add(devCluster);
            config.Clusters = esClusters;
            
            Console.Write(XMLUtility.Serialize(config));
        }
    }
}
