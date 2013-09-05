using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using QuaintHouse.ElasticSearch.Action.Search;
using QuaintHouse.ElasticSearch.Entity;
using QuaintHouse.ElasticSearch.QueryDSL.Filter;
using QuaintHouse.ElasticSearch.QueryDSL.Query;
using QuaintHouse.ElasticSearch.QueryDSL.Sort;
using QuaintHouse.ElasticSearch.Request;
using QuaintHouse.ElasticSearch.Response;
using QuaintHouse.ElasticSearch.Utils;
using JsonSerializer = QuaintHouse.REST.Serializer.JsonSerializer;

namespace TestProject.ElasticSearch
{
    [TestClass]
    public class JsonSerializerTest
    {
        [TestMethod]
        public void SerializerTest()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            JsonBean bean = new JsonBean() {BeanId = 1, BeanName = "first bean", CreateDate = DateTime.Now};
            string jsonString = jsonSerializer.Serialize(bean);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void DeserializerTest()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            JsonBean bean = new JsonBean() {BeanId = 1, BeanName = "first bean", CreateDate = DateTime.Now};
            string jsonString = jsonSerializer.Serialize(bean);
            JsonBeanWithProperty newBean = jsonSerializer.Deserialize<JsonBeanWithProperty>(jsonString);
            Console.WriteLine(newBean.Id + newBean.Name + newBean.Date + newBean.Error);
        }

        [TestMethod]
        public void SerializerWithJsonPropertyTest()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            JsonBeanWithProperty bean = new JsonBeanWithProperty()
                                            {Id = 2, Name = "second bean", Date = DateTime.Now, Error = "error"};
            string jsonString = jsonSerializer.Serialize(bean);
            Console.WriteLine(jsonString);
            JsonBean newBean = jsonSerializer.Deserialize<JsonBean>(jsonString);
            Console.WriteLine(newBean.BeanId + newBean.BeanName + newBean.CreateDate);
        }

        [TestMethod]
        public void SerializerByConverter()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            SearchRequest request = new SearchRequest()
                                        {
                                            Size = 20,
                                            From = 1,
                                            Fields = new List<string>() {"Name", "Age"},
                                            Explain = true,
                                            SortFields =
                                                new List<SortField>()
                                                    {new SortField() {FieldName = "Name", SortType = SortType.Asc}}
                                        };
            string jsonString = jsonSerializer.Serialize(request);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void SerializerSortField()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            SortField sortField = new SortField() {FieldName = "Name", SortType = SortType.Asc, SortMode = SortMode.Avg};
            string jsonString = jsonSerializer.Serialize(sortField);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void SerializerTermQuery()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            TermQuery termQuery = new TermQuery("fieldname", "fieldvalue");
            termQuery.SetBoost(2.0);
            string jsonString = jsonSerializer.Serialize(termQuery);
            Console.WriteLine(jsonString);
            Assert.AreEqual(jsonString, "{\"term\":{\"fieldname\":{\"term\":\"fieldvalue\",\"boost\":2.0}}}");
        }

        [TestMethod]
        public void SerializerMatchQuery()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            MatchQuery matchQuery = new MatchQuery("fieldname", "fieldvalue");
            string jsonString = jsonSerializer.Serialize(matchQuery);
            Console.WriteLine(jsonString);
            Assert.AreEqual(jsonString, "{\"match\":{\"fieldname\":\"fieldvalue\"}}");
        }

        [TestMethod]
        public void SerializerBoolQuery()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            BoolQuery boolQuery = new BoolQuery();
            boolQuery.Must(new TermQuery("termfield", "termvalue"));
            boolQuery.Should(new MatchQuery("matchfield", "matchvalue"));
            string jsonString = jsonSerializer.Serialize(boolQuery);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void SerializerTermFilter()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            TermFilter termFilter = new TermFilter("filterfield", "filtervalue");
            
            string jsonString = jsonSerializer.Serialize(termFilter);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void SerializerExistsFilter()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            ExistsFilter existsFilter = new ExistsFilter("field");
            string jsonString = jsonSerializer.Serialize(existsFilter);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void SerializerBoolFilter()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            BoolFilter boolFilter = new BoolFilter();
            boolFilter.Must(new TermFilter("term1", "value1"));
            boolFilter.Should(new TermFilter("term2", "value2"));
            boolFilter.Should(new TermFilter("term3", "value3"));
            string jsonString = jsonSerializer.Serialize(boolFilter);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void SerializerAndFilter()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            AndFilter andFilter = new AndFilter();
            andFilter.AddFilter(new TermFilter("term1", "value1"));
            andFilter.AddFilter(new ExistsFilter("field"));
            string jsonString = jsonSerializer.Serialize(andFilter);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void SerializerSearchRequest()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            SearchRequest request = new SearchRequest();
            request.SetIndexes("index");
            request.SetTypes("type1", "type2");
            request.SetFields("field1", "field2", "field3");
            request.SetSortField(new SortField(){FieldName = "sortfield", SortType = SortType.Asc});
            request.SearchType = SearchType.QueryAndFetch;
            request.Explain = false;
            request.Query = new MatchQuery("matchfield", "matchvalue");
            request.Filter = new ExistsFilter("field");

            string jsonString = jsonSerializer.Serialize(request);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void SerializerSouce()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            //string jsonString = "{\"_source\": {\"name\": \"4\", \"date\": \"08/02/2013T09:50:56\"}}";

            string sString = jsonSerializer.Serialize(new Document() {Name = "4", Date = DateTime.Now});
            Console.WriteLine(sString);
            Document document = jsonSerializer.Deserialize<Document>(sString);
            Console.WriteLine(document.Name + ":" + document.Date);
            Assert.AreEqual(document.Name, "4");
        }

        [TestMethod]
        public void SerializerGetResponseTest()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            string jsonString = "{ \"_index\": \"delete_index\", \"_type\": \"delete_type\", \"_id\": 3, \"_version\": 2, \"exists\": true, \"_source\": { \"name\": 4, \"date\": \"08/02/2013T09:50:56\"}}";
            GetResponse<Document> response = jsonSerializer.Deserialize<GetResponse<Document>>(jsonString);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Exists);
            Console.WriteLine(response.Source.Name + ":" + response.Source.Date);
            string sString = jsonSerializer.Serialize(response);
            Console.WriteLine(sString);
        }

        [TestMethod]
        public void SerializeBulkRequestTest()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            BulkRequest request = new BulkRequest();
            request.AddIndexItem(new IndexItem()
                                     {
                                         Index = "index1",
                                         ActionType = "index",
                                         Type = "type1",
                                         DocuemntId = "1",
                                         DocumentEntity =
                                             new JsonBean() {BeanId = 1, BeanName = "bean1", CreateDate = DateTime.Now}
                                     });
            request.AddIndexItem(new IndexItem()
                                     {Index = "index1", ActionType = "delete", Type = "type1", DocuemntId = "2"});
            request.AddIndexItem(new IndexItem()
                                     {
                                         Index = "index1",
                                         ActionType = "update",
                                         Type = "type1",
                                         DocuemntId = "3",
                                         DocumentEntity =
                                             new JsonBean() {BeanId = 3, BeanName = "bean3", CreateDate = DateTime.Now}
                                     });
            request.AddIndexItem(new IndexItem()
                                     {
                                         Index = "index2",
                                         ActionType = "create",
                                         Type = "type2",
                                         DocuemntId = "10",
                                         DocumentEntity =
                                             new JsonBean() {BeanId = 10, BeanName = "bean10", CreateDate = DateTime.Now}
                                     });
            string jsonString = jsonSerializer.Serialize(request);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void SerializeUpdateRequestTest()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            UpdateDocRequest request = new UpdateDocRequest()
                                           {
                                               Index = "index1",
                                               Type = "type1",
                                               DocumentId = "1",
                                               Fields = new List<Field>()
                                                            {
                                                                new Field() {Name = "beanname", Value = "bean1"},
                                                                new Field() {Name = "beanid", Value = "2"},
                                                                new Field()
                                                                    {
                                                                        Name = "nested",
                                                                        Value =
                                                                            new JsonBean()
                                                                                {
                                                                                    BeanId = 1,
                                                                                    BeanName = "name1",
                                                                                    CreateDate = DateTime.Now
                                                                                }
                                                                    }
                                                            }
                                           };
            string jsonString = jsonSerializer.Serialize(request);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void SerializeIndexSetting()
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            IndexSetting indexSetting = new IndexSetting() {Replicas = 1, Shards = 5};
            string jsonString = jsonSerializer.Serialize(indexSetting);
            var setting = new {Settings = indexSetting};
            jsonString = jsonSerializer.Serialize(setting);
            Console.WriteLine(jsonString);
        }

        [TestMethod]
        public void JsonBuilderTest()
        {
            string jsonData = JsonBuilder.Init()
                                         .StartObject()
                                         .BuildPropertyName("index")
                                         .StartObject()
                                         .BuildPropertyName("number_of_replicas")
                                         .BuildValue(4)
                                         .EndObject()
                                         .EndObject()
                                         .GetJson();
            Console.WriteLine(jsonData);
        }

        [TestMethod]
        public void DeserializeSearchResult()
        {
            string searchResult =
                "{\"took\":12,\"timed_out\":false,\"_shards\":{\"total\":5,\"successful\":5,\"failed\":0},\"hits\":{\"total\":4,\"max_score\":8.130499,\"hits\":[{\"_index\":\"product\",\"_type\":\"product\",\"_id\":\"5929\",\"_score\":8.130499,\"_source\":{\"IsFreeShipping\":\"N\",\"Personalization\":\"N\",\"CategoryId\":6,\"CatalogIds\":null,\"ShortName\":\"Original Ointment - 1 lb\",\"ProductName\":\"A+D Original Ointment - 1 lb\",\"BrandCode\":\"AD\",\"BrandUrl\":null,\"SubBrandName\":null,\"SubBrandCode\":\"\",\"BrandName\":\"A+D\"}},{\"_index\":\"product\",\"_type\":\"product\",\"_id\":\"5928\",\"_score\":7.440149,\"_source\":{\"IsFreeShipping\":\"N\",\"Personalization\":\"N\",\"CategoryId\":6,\"CatalogIds\":null,\"ShortName\":\"Original Ointment - 4 oz\",\"ProductName\":\"A+D Original Ointment - 4 oz\",\"BrandCode\":\"AD\",\"BrandUrl\":null,\"SubBrandName\":null,\"SubBrandCode\":\"\",\"BrandName\":\"A+D\"}},{\"_index\":\"product\",\"_type\":\"product\",\"_id\":\"5930\",\"_score\":7.440149,\"_source\":{\"IsFreeShipping\":\"N\",\"Personalization\":\"N\",\"CategoryId\":6,\"CatalogIds\":null,\"ShortName\":\"Zinc Oxide Cream - 4 oz\",\"ProductName\":\"A+D Zinc Oxide Cream - 4 oz\",\"BrandCode\":\"AD\",\"BrandUrl\":null,\"SubBrandName\":null,\"SubBrandCode\":\"\",\"BrandName\":\"A+D\"}},{\"_index\":\"product\",\"_type\":\"product\",\"_id\":\"15571\",\"_score\":4.650093,\"_source\":{\"IsFreeShipping\":\"N\",\"Personalization\":\"N\",\"CategoryId\":18,\"CatalogIds\":null,\"ShortName\":\"Imodium A-D Children's Liquid Mint - 4oz\",\"ProductName\":\"Imodium A-D Anti-Diarrheal - Children's Liquid Mint Flavor - 4oz\",\"BrandCode\":\"ImodiumA-D\",\"BrandUrl\":null,\"SubBrandName\":null,\"SubBrandCode\":\"\",\"BrandName\":\"Imodium A-D\"}}]}}";
            JsonSerializer jsonSerializer = new JsonSerializer();
            //string stringResult = jsonSerializer.Deserialize<string>(searchResult);
            //Assert.AreEqual(stringResult, searchResult);
            SearchActionResult<Product> result = jsonSerializer.Deserialize<SearchActionResult<Product>>(searchResult);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.HitResult);
            Assert.IsNotNull(result.HitResult.Hits);

            List<Product> productResult = result.GetHits();
            Assert.IsNotNull(productResult);
            Assert.AreEqual(productResult.Count, 4);
        }
    }

    public class Product
    {
        public string ProductName { get; set; }
        public string ShortName { get; set; }
        public string CatalogIds { get; set; }
        public string BrandCode { get; set; }
        public string BrandName { get; set; }
        public string BrandUrl { get; set; }
        public string SubBrandName { get; set; }
        public string SubBrandCode { get; set; }
        public string IsFreeShipping { get; set; }
        public string Personalization { get; set; }
        public int CategoryId { get; set; }
    }

    public class JsonBean
    {
        private int beanId;
        private string beanName;
        private DateTime createDate;

        public int BeanId
        {
            get { return beanId; }
            set { beanId = value; }
        }

        public string BeanName
        {
            get { return beanName; }
            set { beanName = value; }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
    }

    public class JsonBeanWithProperty
    {
        private int id;
        private string name;
        private DateTime date;

        [JsonProperty("beanId")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty("beanName")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonProperty("createDate")]
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class Document
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }
    }

    
}
