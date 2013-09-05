using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Query.Converter;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof (MatchQueryConverter))]
    public class MatchQuery : BaseQuery
    {
        private string field;
        private string value;
        private QueryType queryType = QueryType.Boolean;
        private Operator op;
        private string analyzer;

        public MatchQuery(string field, string value, Operator op = Operator.Or)
        {
            this.field = field;
            this.value = value;
            this.op = op;
        }

        public string Field
        {
            get { return field; }
        }

        public string Value
        {
            get { return value; }
        }

        public Operator Op
        {
            get { return op; }
        }

        public QueryType QueryType
        {
            get { return queryType; }
            set { queryType = value; }
        }

        public string Analyzer
        {
            get { return analyzer; }
            set { analyzer = value; }
        }
    }

    public enum QueryType
    {
        Boolean,
        Phrase,
        Phrase_Prefix
    }

    public enum Operator
    {
        Or,
        And
    }
}
