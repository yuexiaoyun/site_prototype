using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Query.Converter;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query
{
    [JsonConverter(typeof(BoolQueryConverter))]
    public class BoolQuery : BaseQuery
    {
        private List<IQuery> mustQuery;
        private List<IQuery> mustNotQuery;
        private List<IQuery> shouldQuery;
        private int minimumNumberShouldMatch = 1;

        public BoolQuery Must(params IQuery[] queries)
        {
            if (mustQuery == null) mustQuery = new List<IQuery>();
            mustQuery.AddRange(queries);
            return this;
        }

        public BoolQuery MustNot(params IQuery[] queries)
        {
            if (mustNotQuery == null) mustNotQuery = new List<IQuery>();
            mustNotQuery.AddRange(queries);
            return this;
        }

        public BoolQuery Should(params IQuery[] queries)
        {
            if (shouldQuery == null) shouldQuery = new List<IQuery>();
            shouldQuery.AddRange(queries);
            return this;
        }

        public BoolQuery SetMinimumNumberShouldMatch(int match)
        {
            minimumNumberShouldMatch = match;
            return this;
        }

        public List<IQuery> MustQuery
        {
            get { return mustQuery; }
        }

        public List<IQuery> MustNotQuery
        {
            get { return mustNotQuery; }
        }

        public List<IQuery> ShouldQuery
        {
            get { return shouldQuery; }
        }

        public int MinimumNumberShouldMatch
        {
            get { return minimumNumberShouldMatch; }
        }
    }
}
