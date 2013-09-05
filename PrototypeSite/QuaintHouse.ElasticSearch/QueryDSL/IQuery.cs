using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.ElasticSearch.QueryDSL.Query;

namespace QuaintHouse.ElasticSearch.QueryDSL
{
    public interface IQuery
    {
    }

    public class BaseQuery : IQuery
    {
        private double boost = Constants.DF_Boost;

        public double Boost
        {
            get { return boost; }
            protected set { boost = value; }
        }
    }
}
