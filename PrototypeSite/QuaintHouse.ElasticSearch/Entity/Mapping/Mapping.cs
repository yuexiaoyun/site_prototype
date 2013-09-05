using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Entity.Mapping.Converter;

namespace QuaintHouse.ElasticSearch.Entity.Mapping
{
    [JsonConverter(typeof(MappingConverter))]
    public class Mapping
    {
        private Dictionary<string, IFieldType> fieldTypes;

        public Dictionary<string, IFieldType> FieldTypes
        {
            get { return fieldTypes; }
        }

        public Mapping AddFieldType(string fieldName, IFieldType fieldType)
        {
            if(fieldTypes == null) fieldTypes = new Dictionary<string, IFieldType>();
            fieldTypes.Add(fieldName, fieldType);
            return this;
        }

        private List<IMappingSetting> mappingSettings;

        public List<IMappingSetting> MappingSettings
        {
            get { return mappingSettings; }
        }

        public Mapping AddMappingSetting(IMappingSetting mappingSetting)
        {
            if (mappingSettings == null)
                mappingSettings = new List<IMappingSetting>();
            mappingSettings.Add(mappingSetting);
            return this;
        }
    }

    
}

