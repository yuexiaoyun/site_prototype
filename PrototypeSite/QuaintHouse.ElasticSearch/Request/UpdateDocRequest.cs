using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity;
using QuaintHouse.ElasticSearch.Request.Converter;

namespace QuaintHouse.ElasticSearch.Request
{
    [JsonConverter(typeof(UpdateDocRequestConverter))]
    public class UpdateDocRequest
    {
        private string index;
        private string type;
        private string documentId;
        private List<Field> fields;

        public string Index
        {
            get { return index; }
            set { index = value; }
        }

        public string DocumentId
        {
            get { return documentId; }
            set { documentId = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public List<Field> Fields
        {
            get { return fields; }
            set { fields = value; }
        }

        public UpdateDocRequest AddField(Field field)
        {
            if (fields == null) fields = new List<Field>();
            fields.Add(field);
            return this;
        }
    }
}
