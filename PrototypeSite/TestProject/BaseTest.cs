using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Core.Ioc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using log4net.Config;

namespace TestProject
{
    public abstract class BaseTest
    {
        protected Container container;

        protected MockRepository mock;

        static BaseTest()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.xml.config"));
        }

        [TestInitialize]
        public void Prepare()
        {
            container = new Container();

            RegisterParamsInWebConfig(container);

            mock = new MockRepository();

            DoPrepare();
        }

        protected abstract void DoPrepare();

        private void RegisterParamsInWebConfig(Container container)
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            foreach (string key in appSettings)
            {
                container.RegisterInstance(typeof (string), key, appSettings.Get(key));
            }
        }
    }
}
