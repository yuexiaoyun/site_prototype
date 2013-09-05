using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.REST.Serializer
{
    public interface ISerializer
    {
        string Serialize(object o);
        T Deserialize<T>(string s);
    }
}
