using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.ElasticSearch.Entity;
using QuaintHouse.ElasticSearch.Exceptions;
using QuaintHouse.ElasticSearch.Setting;

namespace QuaintHouse.ElasticSearch.Manager
{
    public static class ESClusterManager
    {
        static ESClusterManager()
        {
            
        }

        public static ESCluster TryGetESCluster(string clusterName)
        {
            ESCluster cluster;

            if(string.IsNullOrEmpty(clusterName))
            {
                 cluster = ElasticSearchClusterConfig.Instatnce.GetDefaultCluster();
            }
            else
            {
                cluster = ElasticSearchClusterConfig.Instatnce.GetCluster(clusterName);
            }

            if(cluster == null)
            {
                string message = string.IsNullOrEmpty(clusterName) ? "No default cluster found" : string.Format("Cluster:{0} not found", clusterName);
                throw new ESClusterNotFoundException(message);
            }

            return cluster;
        }

        public static string GetESNodeAddress(string clusterName)
        {
            ESNode node = ESNodeManager.TryGetHttpNode(clusterName);
            return node.ToString();
        }
    }
}
