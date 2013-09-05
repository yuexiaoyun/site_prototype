using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Core.AOP;
using Core.Interceptor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject.Core.AOP
{
    [TestClass]
    public class TransactionCallHandlerTest : BaseTest
    {
        protected override void DoPrepare()
        {
            
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TransactionUitlTest()
        {
            container.AddInterceptor<TransactionBean>();

            TransactionBean transactionBean = container.GetInstance<TransactionBean>();

            Assert.AreEqual(0, transactionBean.Result);

            transactionBean.Do();

            Assert.AreEqual(0, transactionBean.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TransactionUitlWithoutAttributeTest()
        {
            container.AddInterceptor<TransactionBean>();

            TransactionBean transactionBean = container.GetInstance<TransactionBean>();

            Assert.AreEqual(0, transactionBean.Result);

            transactionBean.DoWithoutTransaction();

            Assert.AreEqual(1, transactionBean.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TransactionUitlWithSpecificAttributeTest()
        {
            container.AddInterceptor<TransactionBean>();

            TransactionBean transactionBean = container.GetInstance<TransactionBean>();

            Assert.AreEqual(0, transactionBean.Result);

            transactionBean.DoWithSpecificTransaction();

            Assert.AreEqual(0, transactionBean.Result);
        }

    }

    public class TransactionBean
    {
        private int result;

        public int Result
        {
            get { return result; }
        }

        [Transaction]
        public virtual void Do()
        {
            result = DoAction();

            DoException();
        }

        public virtual void DoWithoutTransaction()
        {
            result = DoAction();

            DoException();
        }

        [Transaction(IsolationLevel.ReadCommitted, TransactionScopeOption.Required)]
        public virtual void DoWithSpecificTransaction()
        {
            result = DoAction();

            DoException();
        }

        private int DoAction()
        {
            Console.WriteLine("Set result as 1");
            return 1;
        }

        private void DoException()
        {
            throw new Exception("Rollback transaction.");
        }
    }
}
