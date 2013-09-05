using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.ElasticSearch.QueryDSL.Sort
{
    [JsonConverter(typeof(SortFieldConverter))]
    public class SortField
    {
        private string fieldName;
        private SortType sortType;
        private SortMode sortMode = SortMode.None;

        public string FieldName
        {
            get { return fieldName; }
            set { fieldName = value; }
        }

        public SortType SortType
        {
            get { return sortType; }
            set { sortType = value; }
        }

        public SortMode SortMode
        {
            get { return sortMode; }
            set { sortMode = value; }
        }
    }

    public enum SortType
    {
        Asc,
        Desc
    }

    public enum SortMode
    {
        None,
        Min,
        Max,
        Sum,
        Avg
    }
}
