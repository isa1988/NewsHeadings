using System;
using System.Collections.Generic;
using DataBase.DataModel;
using DataBase.Working;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace UnitTestDataBase
{
    [TestClass]
    public class HeadingTest
    {
        [TestMethod]
        public void GetFullAll()
        {
            var dp = new MainWorker();
            try
            {

            List<HeadingInfo> e1 = dp.Heading.GetAll();
            Console.WriteLine(e1.Count);
            Assert.IsNotNull(e1);
            Assert.AreEqual(1, e1.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
