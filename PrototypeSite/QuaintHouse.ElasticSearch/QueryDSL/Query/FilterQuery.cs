using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query
{
    public class FilterQuery
    {
        public IQuery Query { get; set; }
        public IFilter Filter { get; set; }
    }
}
