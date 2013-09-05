using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Filter.Converter;

namespace QuaintHouse.ElasticSearch.QueryDSL.Filter
{
    [JsonConverter(typeof(AndFilterConverter))]
    public class AndFilter : IFilter
    {
        private List<IFilter> filters;
        private bool cache;

        public List<IFilter> Filters
        {
            get { return filters; }
        }

        public bool Cache
        {
            get { return cache; }
            set { cache = value; }
        }

        public AndFilter()
        {
        }

        public AndFilter(params IFilter[] filters)
        {
            foreach (IFilter filter in filters)
            {
                if(filter is AndFilter)
                {
                    AndFilter andFilter = (AndFilter) filter;
                    this.filters.AddRange(andFilter.Filters);
                }
                else
                {
                    this.filters.Add(filter);
                }
            }
        }

        public AndFilter AddFilter(IFilter filter)
        {
            if (filters == null) filters = new List<IFilter>();
            filters.Add(filter);
            return this;
        }
    }
}
