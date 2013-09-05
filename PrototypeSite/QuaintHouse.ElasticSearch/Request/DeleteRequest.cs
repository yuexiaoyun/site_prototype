using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.Request
{
    public class DeleteRequest
    {
        private string indexName;
        private string typeName;
        private string documentId;

        public string IndexName
        {
            get { return indexName; }
            set { indexName = value; }
        }

        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        public string DocumentId
        {
            get { return documentId; }
            set { documentId = value; }
        }
    }
}
