using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.RestfulUrl
{
    public interface IESActionBuilder
    {
        //-- Document
        IESPathUrlBuilder Search();
        IESPathUrlBuilder Update();
        IESPathUrlBuilder Bulk();
        //-- Mapping
        IESPathUrlBuilder Mapping();
        //-- Amdin
        IESPathUrlBuilder Refresh();
        IESPathUrlBuilder Optimize();
        IESPathUrlBuilder Flush();
        IESPathUrlBuilder OpenIndex();
        IESPathUrlBuilder CloseIndex();
        IESPathUrlBuilder UpdateSetting();
        IESPathUrlBuilder Status();
    }

    public interface IESPathUrlBuilder : IESDocumentBuiler
    {

    }

    public interface IESDocumentBuiler : IESTypeBuilder
    {
        IESTypeBuilder Document(string documentId);
    }

    public interface IESTypeBuilder : IESIndexBuilder
    {
        IESIndexBuilder Type(params string[] typeValues);
        IESIndexBuilder Type(List<string> typeValues);
    }

    public interface IESIndexBuilder : IESParamBuilder
    {
        IESHostBuilder Index(params string[] indexValues);
        IESHostBuilder Index(List<string> indexValues);
    }

    public interface IESParamBuilder : IESHostBuilder
    {
        IESParamBuilder Param(string paramName, string paramValue);
    }

    public interface IESHostBuilder : IESUrlGenerator
    {
        IESUrlGenerator Host(string clusterName);
        IESUrlGenerator Host();
    }

    public interface IESUrlGenerator
    {
        string Url();
    }
}
