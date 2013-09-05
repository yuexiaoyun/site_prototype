using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Filter.Converter;

namespace QuaintHouse.ElasticSearch.QueryDSL.Filter
{
    [JsonConverter(typeof(BoolFilterConverter))]
    public class BoolFilter : BaseFilter
    {
        private List<IFilter> mustFilters;
        private List<IFilter> mustNotFilter;
        private List<IFilter> shouldFilter;

        public BoolFilter Must(IFilter filter)
        {
            if (mustFilters == null) mustFilters = new List<IFilter>();
            mustFilters.Add(filter);
            return this;
        }

        public BoolFilter MustNot(IFilter filter)
        {
            if (mustNotFilter == null) mustNotFilter = new List<IFilter>();
            mustNotFilter.Add(filter);
            return this;
        }

        public BoolFilter Should(IFilter filter)
        {
            if (shouldFilter == null) shouldFilter = new List<IFilter>();
            shouldFilter.Add(filter);
            return this;
        }

        public List<IFilter> MustFilters
        {
            get { return mustFilters; }
        }

        public List<IFilter> MustNotFilter
        {
            get { return mustNotFilter; }
        }

        public List<IFilter> ShouldFilter
        {
            get { return shouldFilter; }
        }
    }
}
