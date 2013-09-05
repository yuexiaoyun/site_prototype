using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using QuaintHouse.ElasticSearch.Entity;
using QuaintHouse.ElasticSearch.Utils;

namespace QuaintHouse.ElasticSearch.Setting
{
    public class ElasticSearchClusterConfig
    {
        private static ElasticSearchClusterConfig _instance;

        static ElasticSearchClusterConfig()
        {
            LoadConfig();
        }

        public static ElasticSearchClusterConfig Instatnce
        {
            get { return _instance; }
        }

        public static ElasticSearchClusterConfig Create()
        {
            return new ElasticSearchClusterConfig();
        }

        private static void LoadConfig()
        {
            string configFilePath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["ElasticSearchClusterConfig"];
            string xml = File.ReadAllText(configFilePath);
            _instance = XMLUtility.Deserialize<ElasticSearchClusterConfig>(xml);
        }

        private ElasticSearchClusterConfig()
        {
        }

        private List<ESCluster> clusters;

        public List<ESCluster> Clusters
        {
            get { return clusters; }
            set { clusters = value; }
        }

        public ESCluster GetDefaultCluster()
        {
            return clusters.Find(e => e.Default);
        }

        public ESCluster GetCluster(string clusterName)
        {
            return clusters.Find(e => e.Name.Equals(clusterName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
