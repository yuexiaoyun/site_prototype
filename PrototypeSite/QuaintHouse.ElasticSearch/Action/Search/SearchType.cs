using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.Action.Search
{
    public enum SearchType
    {
        QueryAndFetch,
        QueryThenFetch,
        DfsQueryAndFetch,
        DfsQueryThenFetch,
        Count,
        Scan
    }
}
