using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity.Mapping.Enum;

namespace QuaintHouse.ElasticSearch.Entity.Mapping
{
    [JsonConverter(typeof(StringFieldType))]
    public class StringFieldType : BaseFieldType
    {
        private TermVector termVector = DefaultConstants.String_TermVector;
        private bool omitNorms;
        private bool omitTermFreqAndPositions;
        private string analyzer;
        private string indexAnalyzer;
        private string searchAnalyzer;

        public override string GetType()
        {
            return "string";
        }

        /// <summary>
        /// Possible values are no, yes, with_offsets, with_positions, with_positions_offsets. Defaults to no.
        /// </summary>
        [JsonProperty("term_vector")]
        public TermVector TermVector
        {
            get { return termVector; }
            set { termVector = value; }
        }

        /// <summary>
        /// Boolean value if norms should be omitted or not. 
        /// Defaults to false for analyzed fields, and to true for not_analyzed fields.
        /// </summary>
        [JsonProperty("omit_norms")]
        public bool OmitNorms
        {
            get { return omitNorms; }

            set { omitNorms = value; }
        }

        /// <summary>
        /// Boolean value if term freq and positions should be omitted. 
        /// Defaults to false. Deprecated since 0.20, see index_options.
        /// </summary>
        [JsonProperty("omit_term_freq_and_positions")]
        public bool OmitTermFreqAndPositions
        {
            get { return omitTermFreqAndPositions; }
            set { omitTermFreqAndPositions = value; }
        }

        /// <summary>
        /// The analyzer used to analyze the text contents when analyzed during indexing and when searching using a query string. 
        /// Defaults to the globally configured analyzer.
        /// </summary>
        [JsonProperty("analyzer")]
        public string Analyzer
        {
            get { return analyzer; }
            set { analyzer = value; }
        }

        /// <summary>
        /// The analyzer used to analyze the text contents when analyzed during indexing.
        /// </summary>
        [JsonProperty("index_analyzer")]
        public string IndexAnalyzer
        {
            get { return indexAnalyzer; }
            set { indexAnalyzer = value; }
        }

        /// <summary>
        /// The analyzer used to analyze the field when part of a query string. Can be updated on an existing field.
        /// </summary>
        [JsonProperty("search_analyzer")]
        public string SearchAnalyzer
        {
            get { return searchAnalyzer; }
            set { searchAnalyzer = value; }
        }

    }
}
