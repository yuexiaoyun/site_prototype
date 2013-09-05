using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.Entity.Mapping.Enum
{
    public enum TermVector
    {
        No,
        Yes,
        With_Offsets,
        With_Positions, 
        With_Positions_Offsets
    }
}
