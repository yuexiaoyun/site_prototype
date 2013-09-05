using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity.Mapping.Converter;
using QuaintHouse.ElasticSearch.Entity.Mapping.Enum;

namespace QuaintHouse.ElasticSearch.Entity.Mapping
{
    [JsonConverter(typeof(NumberFieldConverter))]
    public class NumberFieldType : BaseFieldType
    {
        private NumberType type;
        private int precisionStep = DefaultConstants.Number_PrecisionStep;
        private bool ignoreMalformed = DefaultConstants.Number_IgnoreMalformed;

        public override string GetType()
        {
            return type.ToString();
        }

        /// <summary>
        /// The type of the number. Can be float, double, integer, long, short, byte. Required.
        /// </summary>
        [JsonProperty("type")]
        public NumberType Type
        {
            get { return type; }
            set { type = value; }
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
