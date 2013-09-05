using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.Request
{
    public class GetDocRequest
    {
        private string index;
        private string type;
        private string documentId;

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

        public string DocumentId
        {
            get { return documentId; }
            set { documentId = value; }
        }
    }
}
