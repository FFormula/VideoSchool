using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoSchool.Models;

namespace SchoolTest
{
    [TestClass]
    public class DataBaseTest
    {
        Shared shared;

        [TestMethod]
        public void TestScalar()
        {
            shared = new Shared(RunMode.UnitTest);
            string result = shared.db.Scalar("SELECT 2 + 2 * 2");
            Assert.AreEqual("6", result);
        }
    }
}
