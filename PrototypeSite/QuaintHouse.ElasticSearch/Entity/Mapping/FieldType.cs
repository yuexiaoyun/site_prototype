using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity.Mapping.Enum;

namespace QuaintHouse.ElasticSearch.Entity.Mapping
{
    public interface IFieldType
    {
        string GetType();
    }

    public abstract class BaseFieldType : IFieldType
    {
        public abstract string GetType();

        private string indexName;
        private StoreType store = DefaultConstants.Default_StoreType;
        private IndexType indexType = DefaultConstants.Default_IndexType;
        private double boost = DefaultConstants.Default_Boost;
        private object nullValue;
        private bool includeInAll = DefaultConstants.Default_IncludeInAll;

        /// <summary>
        /// The name of the field that will be stored in the index. Defaults to the property/field name.
        /// </summary>
        [JsonProperty("index_name")]
        public string IndexName
        {
            get { return indexName; }
            set { indexName = value; }
        }

        /// <summary>
        /// Set to yes to store actual field in the index, no to not store it. 
        /// Defaults to no (note, the JSON document itself is stored, and it can be retrieved from it).
        /// </summary>
        [JsonProperty("store")]
        public StoreType Store
        {
            get { return store; }
            set { store = value; }
        }

        /// <summary>
        /// Set to analyzed for the field to be indexed and searchable after being broken down into token using an analyzer. 
        /// not_analyzed means that its still searchable, but does not go through any analysis process or broken down into tokens. 
        /// no means that it won’t be searchable at all (as an individual field; it may still be included in _all). 
        /// Setting to no disables include_in_all. Defaults to analyzed.
        /// </summary>
        [JsonProperty("index")]
        public IndexType IndexType
        {
            get { return indexType; }
            set { indexType = value; }
        }

        /// <summary>
        /// The boost value. Defaults to 1.0.
        /// </summary>
        [JsonProperty("boost")]
        public double Boost
        {
            get { return boost; }
            set { boost = value; }
        }

        /// <summary>
        /// When there is a (JSON) null value for the field, use the null_value as the field value. 
        /// Defaults to not adding the field at all.
        /// </summary>
        [JsonProperty("null_value")]
        public object NullValue
        {
            get { return nullValue; }
            set { nullValue = value; }
        }

        /// <summary>
        /// Should the field be included in the _all field (if enabled). 
        /// If index is set to no this defaults to false, otherwise, defaults to true or to the parent object type setting.
        /// </summary>
        [JsonProperty("include_in_all")]
        public bool IncludeInAll
        {
            get { return includeInAll; }
            set { includeInAll = value; }
        }
    }
}
