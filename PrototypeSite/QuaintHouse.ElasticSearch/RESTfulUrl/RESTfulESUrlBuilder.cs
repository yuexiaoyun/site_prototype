using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.ElasticSearch.Manager;
using QuaintHouse.ElasticSearch.Utils;

namespace QuaintHouse.ElasticSearch.RestfulUrl
{
    public class RESTfulESUrlBuilder : IESActionBuilder, IESPathUrlBuilder
    {
        #region example
        //search
        //http://localhost:9200/twitter/tweet/_search       index,index,../type,type,../[_search]
        //params: timeout, from, size, search_type

        //index (put)
        //http://localhost:9200/twitter     index

        //type (put)
        //http://localhost:9200/twitter/tweet/_mapping      index/type/[_mapping]
        
        //document (put/get/delete) CRD
        //http://localhost:9200/twitter/tweet/1   index/type/documentId
        //params: versioin, opeartion_type, routing, parent, timestamp, ttl, percolate, timeout
        //automatic document id (post) C
        //http://localhost:9200/twitter/tweet/      index/type

        //update (post)
        //http://localhost:9200/twitter/tweet/1/_update   index/type/documentId/[_update]
        #endregion

        private string url;
        private const string Url_Slash = "/";
        private const string Url_Question = "?";
        private const string Url_Equal = "=";
        private const string Url_And = "&";

        private RESTfulESUrlBuilder()
        {
            
        }

        public static RESTfulESUrlBuilder Init()
        {
            return new RESTfulESUrlBuilder();
        }

        #region ES Action Builder

        public IESPathUrlBuilder Search()
        {
            url = url.Append(Url_Slash).Append("_search");
            return this;
        }

        public IESPathUrlBuilder Update()
        {
            url = url.Append(Url_Slash).Append("_update");
            return this;
        }

        public IESPathUrlBuilder Mapping()
        {
            url = url.Append(Url_Slash).Append("_mapping");
            return this;
        }

        public IESPathUrlBuilder Refresh()
        {
            url = url.Append(Url_Slash).Append("_refresh");
            return this;
        }

        public IESPathUrlBuilder Optimize()
        {
            url = url.Append(Url_Slash).Append("_optimize");
            return this;
        }

        public IESPathUrlBuilder Flush()
        {
            url = url.Append(Url_Slash).Append("_flush");
            return this;
        }

        public IESPathUrlBuilder Bulk()
        {
            url = url.Append(Url_Slash).Append("_bulk");
            return this;
        }

        public IESPathUrlBuilder OpenIndex()
        {
            url = url.Append(Url_Slash).Append("_open");
            return this;
        }

        public IESPathUrlBuilder CloseIndex()
        {
            url = url.Append(Url_Slash).Append("_close");
            return this;
        }

        public IESPathUrlBuilder UpdateSetting()
        {
            url = url.Append(Url_Slash).Append("_settings");
            return this;
        }

        public IESPathUrlBuilder Status()
        {
            url = url.Append(Url_Slash).Append("_status");
            return this;
        }

        #endregion

        #region ES Path Builder

        public string Url()
        {
            return url;
        }

        public IESUrlGenerator Host(string clusterName)
        {
            string nodeAddress = ESClusterManager.GetESNodeAddress(clusterName);
            url = url.Prefix(nodeAddress);
            return this;
        }

        public IESUrlGenerator Host()
        {
            //default cluster
            string nodeAddress = ESClusterManager.GetESNodeAddress(string.Empty);
            url = url.Prefix(nodeAddress);
            return this;
        }

        public IESParamBuilder Param(string paramName, string paramValue)
        {
            if (!string.IsNullOrEmpty(paramValue) && !string.IsNullOrEmpty(paramName))
            {
                string delimiter = url.IndexOf(Url_Question) >= 0 ? Url_And : Url_Question;
                url = url.Append(delimiter)
                         .Append(paramName)
                         .Append(Url_Equal)
                         .Append(paramValue);
            }
            return this;
        }

        public IESHostBuilder Index(params string[] indexValues)
        {
            if (indexValues != null)
            {
                string indexes = string.Join(",", indexValues);
                url = url.Prefix(indexes).Prefix(Url_Slash);
            }
            return this;
        }

        public IESHostBuilder Index(List<string> indexValues)
        {
            if (indexValues != null)
            {
                string indexes = string.Join(",", indexValues.ToArray());
                url = url.Prefix(indexes).Prefix(Url_Slash);
            }
            return this;
        }

        public IESIndexBuilder Type(params string[] typeValues)
        {
            if(typeValues != null)
            {
                string types = string.Join(",", typeValues);
                url = url.Prefix(types).Prefix(Url_Slash);
            }
            return this;
        }

        public IESIndexBuilder Type(List<string> typeValues)
        {
            if(typeValues != null)
            {
                string types = string.Join(",", typeValues.ToArray());
                url = url.Prefix(types).Prefix(Url_Slash);
            }
            return this;
        }

        public IESTypeBuilder Document(string documentId)
        {
            if(!string.IsNullOrEmpty(documentId))
            {
                url = url.Append(Url_Slash).Append(documentId.Trim());    
            }
            return this;
        }

        #endregion
    }
}
