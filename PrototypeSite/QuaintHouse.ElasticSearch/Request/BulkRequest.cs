using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Request.Converter;

namespace QuaintHouse.ElasticSearch.Request
{
    [JsonConverter(typeof(BulkRequestConverter))]
    public class BulkRequest
    {
        public List<IndexItem> IndexItems { get; set; }

        public BulkRequest AddIndexItem(IndexItem indexItem)
        {
            if(IndexItems == null) IndexItems = new List<IndexItem>();
            IndexItems.Add(indexItem);
            return this;
        }
    }

    public class IndexItem
    {
        private string actionType = "index";
        private string index;
        private string type;
        private string docuemntId;
        private object documentEntity;

        public string ActionType
        {
            get { return actionType; }
            set { actionType = value; }
        }

        public string Index
        {
            get { return index; }
            set { index = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string DocuemntId
        {
            get { return docuemntId; }
            set { docuemntId = value; }
        }

        public object DocumentEntity
        {
            get { return documentEntity; }
            set { documentEntity = value; }
        }
    }
}
