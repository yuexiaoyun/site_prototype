using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity.Mapping.Converter;

namespace QuaintHouse.ElasticSearch.Entity.Mapping
{
    [JsonConverter(typeof(DateFieldConverter))]
    public class DateFieldType : BaseFieldType
    {
        private string format;
        private int precisionStep = DefaultConstants.Date_PrecisionStep;
        private bool ignoreMalformed = DefaultConstants.Date_IgnoreMalformed;

        public override string GetType()
        {
            return "date";
        }

        /// <summary>
        /// The date format.
        /// </summary>
        [JsonProperty("format")]
        public string Format
        {
            get { return format; }
            set { format = value; }
        }

        /// <summary>
        /// The precision step (number of terms generated for each number value). Defaults to 4.
        /// </summary>
        [JsonProperty("precision_step")]
        public int PrecisionStep
        {
            get { return precisionStep; }
            set { precisionStep = value; }
        }

        /// <summary>
        /// Ignored a malformed number. Defaults to false.
        /// </summary>
        [JsonProperty("ignore_malformed")]
        public bool IgnoreMalformed
        {
            get { return ignoreMalformed; }
            set { ignoreMalformed = value; }
        }
    }
}
