using System;
using QuaintHouse.ElasticSearch.Action;
using QuaintHouse.ElasticSearch.Action.Search;
using QuaintHouse.ElasticSearch.Entity;
using QuaintHouse.ElasticSearch.Request;
using QuaintHouse.ElasticSearch.Request.Builder;
using QuaintHouse.ElasticSearch.Response;
using QuaintHouse.ElasticSearch.RestfulUrl;
using QuaintHouse.ElasticSearch.Utils;
using QuaintHouse.REST;

namespace QuaintHouse.ElasticSearch
{
    public class ElasticSearchClient
    {
        private string clusterName;
        private RESTServiceClient restServiceClient;

        public ElasticSearchClient()
        {
            restServiceClient = new RESTServiceClient();
            restServiceClient.RequireClientSignature = false;
        }

        public ElasticSearchClient(string clusterName)
        {
            this.clusterName = clusterName;
        }

        #region Index

        /// <summary>
        /// C operation for Index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BaseResponse Index(string index)
        {
            string indexUrl = RESTfulESUrlBuilder.Init().Index(index).Host(clusterName).Url();
            return restServiceClient.Put<string, BaseResponse>(indexUrl, index);
        }

        /// <summary>
        /// Open index (matchs with CloseIndex)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BaseResponse OpenIndex(string index)
        {
            string indexUrl = RESTfulESUrlBuilder.Init().OpenIndex().Index(index).Host(clusterName).Url();
            return restServiceClient.Post<string, BaseResponse>(indexUrl, string.Empty);
        }

        /// <summary>
        /// Close Index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public BaseResponse CloseIndex(string index)
        {
            string indexUrl = RESTfulESUrlBuilder.Init().CloseIndex().Index(index).Host(clusterName).Url();
            return restServiceClient.Post<string, BaseResponse>(indexUrl, string.Empty);
        }

        /// <summary>
        /// Create index with settings
        /// </summary>
        /// <param name="index"></param>
        /// <param name="indexSetting"></param>
        /// <returns></returns>
        public BaseResponse Index(string index, IndexSetting indexSetting)
        {
            string indexUrl = RESTfulESUrlBuilder.Init().Index(index).Host(clusterName).Url();
            var settings = new IndexSettingWrapper(indexSetting);
            return restServiceClient.Post<IndexSettingWrapper, BaseResponse>(indexUrl, settings);
        }

        /// <summary>
        /// Update index replica number
        /// </summary>
        /// <param name="index"></param>
        /// <param name="numOfReplica"></param>
        /// <returns></returns>
        public BaseResponse UpdateIndex(string index, int numOfReplica)
        {
            string indexUrl = RESTfulESUrlBuilder.Init().UpdateSetting().Host(clusterName).Url();
            string jsonData = JsonBuilder.Init()
                                         .StartObject()
                                         .BuildPropertyName("index")
                                         .StartObject()
                                         .BuildPropertyName("number_of_replicas")
                                         .BuildValue(numOfReplica)
                                         .EndObject()
                                         .EndObject()
                                         .GetJson();
            return restServiceClient.Put<string, BaseResponse>(indexUrl, jsonData);
        }

        #endregion

        #region Mapping CURD

        /// <summary>
        /// C/U create mapping
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse Mapping(MappingRequest request)
        {
            string mappingUrl = RESTfulESUrlBuilder.Init().Mapping().Type(request.Type).Index(request.Indexes).Host(clusterName)
                                                   .Url();
            return restServiceClient.Put<MappingRequest, BaseResponse>(mappingUrl, request);
        }

        /// <summary>
        /// G get mapping
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetMapping(MappingRequest request)
        {
            string mappingUrl = RESTfulESUrlBuilder.Init().Mapping().Type(request.Type).Index(request.Indexes).Host(clusterName)
                                                   .Url();
            return restServiceClient.Get<string>(mappingUrl);
        }

        #endregion

        #region Admin

        /// <summary>
        /// Refresh Indexes
        /// </summary>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public BaseResponse Refresh(string[] indexes)
        {
            string refreshUrl = RESTfulESUrlBuilder.Init().Refresh().Index(indexes).Host(clusterName).Url();
            return restServiceClient.Get<BaseResponse>(refreshUrl);
        }

        /// <summary>
        /// Optimize Indexes
        /// </summary>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public BaseResponse Optimize(string[] indexes)
        {
            string optimizeUrl = RESTfulESUrlBuilder.Init().Optimize().Index(indexes).Host(clusterName).Url();
            return restServiceClient.Get<BaseResponse>(optimizeUrl);
        }

        /// <summary>
        /// Flush indexes data to storage and free memory
        /// </summary>
        /// <param name="indexes"></param>
        /// <returns></returns>
        public BaseResponse Flush(string[] indexes)
        {
            string flushUrl = RESTfulESUrlBuilder.Init().Flush().Index(indexes).Host(clusterName).Url();
            return restServiceClient.Get<BaseResponse>(flushUrl);
        }

        /// <summary>
        /// Get index status
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string Status(string index)
        {
            string statusUrl = RESTfulESUrlBuilder.Init().Status().Index(index).Host(clusterName).Url();
            return restServiceClient.Get<string>(statusUrl);
        }
        

        #endregion

        #region Document CURD

        /// <summary>
        /// C and U operations for Document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public DocumentResponse IndexDocument<T>(IndexDocRequest<T> request)
        {
            string documentUrl = RESTfulESUrlBuilder.Init().Document(request.DocumentId).Type(request.Type).Index(request.Index).Host(clusterName)
                                                    .Url();
            return restServiceClient.Post<T, DocumentResponse>(documentUrl, request.DocumentEntity);
        }

        /// <summary>
        /// R operation fro Document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetResponse<T> GetDocument<T>(GetDocRequest request)
        {
            string getUrl = RESTfulESUrlBuilder.Init().Document(request.DocumentId).Type(request.Type).Index(request.Index).Host(clusterName)
                                               .Url();
            return restServiceClient.Get<GetResponse<T>>(getUrl);
        }

        /// <summary>
        /// Partial Update Doc
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse UpdateDocument(UpdateDocRequest request)
        {
            string updateUrl = RESTfulESUrlBuilder.Init().Update().Document(request.DocumentId).Type(request.Type).Index(request.Index).Host(clusterName)
                                                  .Url();
            return restServiceClient.Post<UpdateDocRequest, BaseResponse>(updateUrl, request);
        }

        /// <summary>
        /// D operation for Index/Type/Document
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DeleteResponse Delete(DeleteRequest request)
        {
            string deleteUrl = BuildDeleteUrl(request);
            return restServiceClient.Delete<DeleteResponse>(deleteUrl);
        }

        private string BuildDeleteUrl(DeleteRequest request)
        {
            if (!string.IsNullOrEmpty(request.DocumentId))
            {
                return RESTfulESUrlBuilder.Init()
                    .Document(request.DocumentId)
                    .Type(request.TypeName)
                    .Index(request.IndexName)
                    .Host(clusterName)
                    .Url();
            }

            if (!string.IsNullOrEmpty(request.TypeName))
            {
                return RESTfulESUrlBuilder.Init()
                    .Type(request.TypeName)
                    .Index(request.IndexName)
                    .Host(clusterName)
                    .Url();
            }

            return RESTfulESUrlBuilder.Init()
                .Index(request.IndexName)
                .Host(clusterName)
                .Url();
        }

        /// <summary>
        /// Bulk index
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BaseResponse BulkIndex(BulkRequest request)
        {
            string bulkUrl = RESTfulESUrlBuilder.Init().Bulk().Host(clusterName).Url();
            return restServiceClient.Post<BulkRequest, BaseResponse>(bulkUrl, request);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IActionResult Search<T>(SearchRequest request)
        {
            return SearchAction.Prepare().SetSearchRequest(request).Execute<T>();
        }
    }
}
