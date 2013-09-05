using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.Entity
{
    public class Field
    {
        private string name;
        private object value;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
