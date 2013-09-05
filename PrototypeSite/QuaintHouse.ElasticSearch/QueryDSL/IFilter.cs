using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.QueryDSL
{
    public interface IFilter
    {
    }

    public class BaseFilter : IFilter
    {
        private bool cache = true;

        public bool Cache
        {
            get { return cache; }
            set { cache = value; }
        }
    }
}
