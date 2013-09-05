using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.ElasticSearch.Entity;
using QuaintHouse.ElasticSearch.Exceptions;
using QuaintHouse.ElasticSearch.Setting;

namespace QuaintHouse.ElasticSearch.Manager
{
    public static class ESNodeManager
    {
        static ESNodeManager()
        {
            
        }

        public static ESNode TryGetHttpNode(string clusterName)
        {
            ESCluster cluster = ESClusterManager.TryGetESCluster(clusterName);
            if(cluster.HttpNodes == null || cluster.HttpNodes.Count <= 0)
            {
                throw new ESNodeNotFoundException(string.Format("cluster:{0} has no http nodes", clusterName));
            }
            if(cluster.HttpNodes.Count > 1)
            {
                return cluster.HttpNodes[new Random().Next(cluster.HttpNodes.Count)];
            }
            return cluster.HttpNodes[0];
        }
    }
}
