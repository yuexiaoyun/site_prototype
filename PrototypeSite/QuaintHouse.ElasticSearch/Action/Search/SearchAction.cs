using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.ElasticSearch.QueryDSL;
using QuaintHouse.ElasticSearch.QueryDSL.Sort;
using QuaintHouse.ElasticSearch.Request;
using QuaintHouse.ElasticSearch.RestfulUrl;
using QuaintHouse.REST;
using log4net;

namespace QuaintHouse.ElasticSearch.Action.Search
{
    public class SearchAction : IAction
    {
        private readonly ILog logger = LogManager.GetLogger(typeof (SearchAction));
        private SearchRequest searchRequest;
        private RESTServiceClient restServiceClient;

        private SearchAction()
        {
            searchRequest = new SearchRequest();
            restServiceClient = new RESTServiceClient();
            restServiceClient.RequireClientSignature = false;
        }

        public static SearchAction Prepare(params string[] indexes)
        {
            SearchAction searchAction = new SearchAction();
            searchAction.SetIndexes(indexes);
            return searchAction;
        }

        public SearchAction SetIndexes(params string[] indexes)
        {
            searchRequest.SetIndexes(indexes);
            return this;
        }

        public SearchAction SetTypes(params string[] types)
        {
            searchRequest.SetTypes(types);
            return this;
        }

        public SearchAction SetFrom(int from)
        {
            searchRequest.From = from;
            return this;
        }

        public SearchAction SetSize(int size)
        {
            searchRequest.Size = size;
            return this;
        }

        public SearchAction SetQuery(IQuery query)
        {
            searchRequest.Query = query;
            return this;
        }

        public SearchAction SetFilters(IFilter filter)
        {
            searchRequest.Filter = filter;
            return this;
        }

        public SearchAction Field(params string[] fields)
        {
            searchRequest.SetFields(fields);
            return this;
        }

        public SearchAction Sort(params SortField[] sortFields)
        {
            searchRequest.SetSortField(sortFields);
            return this;
        }

        public SearchAction SetSearchType(SearchType searchType)
        {
            searchRequest.SearchType = searchType;
            return this;
        }

        public SearchAction SetSearchRequest(SearchRequest request)
        {
            searchRequest = request;
            return this;
        }

        public SearchAction SetVersion(bool version)
        {
            searchRequest.Version = version;
            return this;
        }

        public IActionResult Execute<T>()
        {
            string searchUrl = RESTfulESUrlBuilder.Init().Search().Type(searchRequest.Types).Index(searchRequest.Indexes).Host().Url();
            logger.Info("Search Url: " + searchUrl);
            SearchActionResult<T> result = restServiceClient.Post<SearchRequest, SearchActionResult<T>>(searchUrl, searchRequest);
            return result;
        }
    }
}
