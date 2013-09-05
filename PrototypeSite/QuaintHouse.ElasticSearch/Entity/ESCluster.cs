using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.Entity
{
    public class ESCluster
    {
        private string name;
        private bool _default;
        private List<ESNode> httpNodes;
        private List<ESNode> thriftNodes;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Default
        {
            get { return _default; }
            set { _default = value; }
        }

        public List<ESNode> HttpNodes
        {
            get { return httpNodes; }
            set { httpNodes = value; }
        }

        public List<ESNode> ThriftNodes
        {
            get { return thriftNodes; }
            set { thriftNodes = value; }
        }
    }
}
