using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.Entity
{
    public class ESNode
    {
        private string host;
        private int port;
        private bool enabled;

        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public override string ToString()
        {
            return host + ":" + port;
        }

        public string GetAddress()
        {
            return host + ":" + port;
        }
    }
}
