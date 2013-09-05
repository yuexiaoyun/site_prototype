using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.ElasticSearch.Entity.Mapping.Enum;

namespace QuaintHouse.ElasticSearch.Entity.Mapping
{
    public class DefaultConstants
    {
        public static int Number_PrecisionStep = 4;
        public static bool Number_IgnoreMalformed = false;

        public static TermVector String_TermVector = TermVector.No;

        public static int Date_PrecisionStep = 4;
        public static bool Date_IgnoreMalformed = false;

        public static StoreType Default_StoreType = StoreType.No;
        public static IndexType Default_IndexType = IndexType.Analyzed;
        public static bool Default_IncludeInAll = true;
        public static double Default_Boost = 1.0;
    }
}
