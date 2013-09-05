using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Action.Search;
using QuaintHouse.ElasticSearch.QueryDSL;
using QuaintHouse.ElasticSearch.QueryDSL.Sort;
using QuaintHouse.ElasticSearch.Request.Converter;

namespace QuaintHouse.ElasticSearch.Request
{
    [JsonConverter(typeof(SearchRequestConverter))]
    public class SearchRequest
    {
        private List<string> indexes = new List<string>();
        private List<string> types = new List<string>();
        
        private IQuery query;
        private IFilter filter;
        private IFacet facet;

        private List<string> fields = new List<string>();
        private List<SortField> sortFields = new List<SortField>();
        
        //defaul from is 0
        private int from;
        //defaul reslult size is 10
        private int size = 10;
        //default search type is Query and Fetch
        private SearchType searchType = SearchType.QueryAndFetch;
        //default explainable is false
        private bool explain;
        //defalt versionable is false
        private bool version;

        #region Properties

        public List<string> Indexes
        {
            get { return indexes; }
        }

        public List<string> Types
        {
            get { return types; }
        }

        public int From
        {
            get { return from; }
            set { from = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public IQuery Query
        {
            get { return query; }
            set { query = value; }
        }

        public IFilter Filter
        {
            get { return filter; }
            set { filter = value; }
        }

        public List<string> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public List<SortField> SortFields
        {
            get { return sortFields; }
            set { sortFields = value; }
        }

        public SearchType SearchType
        {
            get { return searchType; }
            set { searchType = value; }
        }

        public bool Explain
        {
            get { return explain; }
            set { explain = value; }
        }

        public IFacet Facet
        {
            get { return facet; }
            set { facet = value; }
        }

        public bool Version
        {
            get { return version; }
            set { version = value; }
        }

        #endregion

        public void SetIndexes(params string[] indexes)
        {
            this.indexes.AddRange(indexes);
        }

        public void SetFields(params string[] fields)
        {
            this.fields.AddRange(fields);
        }

        public void SetSortField(params SortField[] sortFields)
        {
            this.sortFields.AddRange(sortFields);
        }

        public void SetTypes(params string[] types)
        {
            this.types.AddRange(types);
        }
    }
}
