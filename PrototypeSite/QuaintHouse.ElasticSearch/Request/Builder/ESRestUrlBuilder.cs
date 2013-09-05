using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.ElasticSearch.Manager;

namespace QuaintHouse.ElasticSearch.Request.Builder
{
    [Obsolete]
    public class ESRestUrlBuilder
    {
        private const string Slash = "/";
        private StringBuilder builder;

        private ESRestUrlBuilder()
        {
            builder = new StringBuilder();
        }

        public static ESRestUrlBuilder Create()
        {
            return new ESRestUrlBuilder();
        }

        public string GetUrl()
        {
            return builder.ToString();
        }

        public ESRestUrlBuilder BuildCluster(string clusterName)
        {
            string esNodes = ESClusterManager.GetESNodeAddress(clusterName);
            builder.Append(esNodes);
            return this;
        }

        public ESRestUrlBuilder BuildIndex(string index)
        {
            builder.Append(Slash).Append(index);
            return this;
        }

        public ESRestUrlBuilder BuildType(string type)
        {
            builder.Append(Slash).Append(type);
            return this;
        }

        public ESRestUrlBuilder BuildIndexes(List<string> index)
        {
            builder.Append(Slash).Append(string.Join(",", index.ToArray()));
            return this;
        }

        public ESRestUrlBuilder BuildTypes(List<string> types)
        {
            builder.Append(Slash).Append(string.Join(",", types.ToArray()));
            return this;
        }

        #region Index

        public static string BuildIndexRestUrl(string clusterName, DeleteRequest request)
        {
            return BuildIndexRestUrl(clusterName, request.IndexName, request.TypeName, request.DocumentId);
        }

        public static string BuildIndexRestUrl(string clusterName, string index, string type, string document, string parentDocument = null)
        {
            string esNodes = ESClusterManager.GetESNodeAddress(clusterName);

            string indexUrl = BuildIndexUrl(index, type, document, parentDocument);

            return string.Format("{0}{1}", esNodes, indexUrl);
        }

        private static string BuildIndexUrl(string index, string type, string document, string parentDocument)
        {
            if (string.IsNullOrEmpty(document))
            {
                return string.Format("/{0}/{1}/", index, type);
            }
            else if (string.IsNullOrEmpty(parentDocument))
            {
                return string.Format("/{0}/{1}/{2}", index, type, document);
            }
            else
            {
                return string.Format("/{0}/{1}/{2}?parent={3}", index, type, document, parentDocument);
            }
        }

        #endregion
    }
}
