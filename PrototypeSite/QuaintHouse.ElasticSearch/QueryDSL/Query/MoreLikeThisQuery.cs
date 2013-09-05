using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.QueryDSL.Query.Converter;

namespace QuaintHouse.ElasticSearch.QueryDSL.Query
{
    [JsonConverter(typeof(MoreLikeThisQueryConverter))]
    public class MoreLikeThisQuery : BaseQuery
    {
        private List<string> fields;
        private string likeText;
        private float percentTermsToMatch = 0.3f;
        private int minTermFreq = 2;
        private int maxQueryTerm = 25;
        private string[] stopWords;
        private int minDocFreq = 5;
        private int maxDocFreq;
        private int minWordLen;
        private int maxWordLen;
        private int boostTerms = 1;
        private string analyzer;

        public MoreLikeThisQuery(List<string> fields,  string likeText, int minTermFreq = 2, int maxQueryTerm = 25)
        {
            this.fields = fields;
            this.likeText = likeText;
            this.minTermFreq = minTermFreq;
            this.maxQueryTerm = maxQueryTerm;
        }

        public List<string> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public string LikeText
        {
            get { return likeText; }
            set { likeText = value; }
        }

        public float PercentTermsToMatch
        {
            get { return percentTermsToMatch; }
            set { percentTermsToMatch = value; }
        }

        public int MinTermFreq
        {
            get { return minTermFreq; }
            set { minTermFreq = value; }
        }

        public int MaxQueryTerm
        {
            get { return maxQueryTerm; }
            set { maxQueryTerm = value; }
        }

        public string[] StopWords
        {
            get { return stopWords; }
            set { stopWords = value; }
        }

        public int MinDocFreq
        {
            get { return minDocFreq; }
            set { minDocFreq = value; }
        }

        public int MaxDocFreq
        {
            get { return maxDocFreq; }
            set { maxDocFreq = value; }
        }

        public int MinWordLen
        {
            get { return minWordLen; }
            set { minWordLen = value; }
        }

        public int MaxWordLen
        {
            get { return maxWordLen; }
            set { maxWordLen = value; }
        }

        public int BoostTerms
        {
            get { return boostTerms; }
            set { boostTerms = value; }
        }

        public string Analyzer
        {
            get { return analyzer; }
            set { analyzer = value; }
        }
    }
}
