using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuaintHouse.ElasticSearch.RestfulUrl;

namespace TestProject.ElasticSearch
{
    [TestClass]
    public class RESTfulUrlBuilderTest
    {
        [TestMethod]
        public void SearchTest()
        {
            string searchUrl = RESTfulESUrlBuilder.Init().Search().Type("Type").Index("Index").Host().Url();
            Assert.AreEqual(searchUrl, "localhost:9200/Index/Type/_search");
        }

        [TestMethod]
        public void MappingTest()
        {
            string mappingUrl = RESTfulESUrlBuilder.Init().Mapping().Type("type").Index("index").Host().Url();
            Assert.AreEqual(mappingUrl, "localhost:9200/index/type/_mapping");
        }

        [TestMethod]
        public void UpdateTest()
        {
            string updateUrl = RESTfulESUrlBuilder.Init().Update().Type("type").Index("index").Host().Url();
            Assert.AreEqual(updateUrl, "localhost:9200/index/type/_update");
        }

        [TestMethod]
        public void DocumentTest()
        {
            string documentUrl = RESTfulESUrlBuilder.Init().Document("doc").Type("type").Index("index").Host().Url();
            Assert.AreEqual(documentUrl, "localhost:9200/index/type/doc");
        }
    }
}
