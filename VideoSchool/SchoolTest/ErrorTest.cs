using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoSchool.Models;

namespace SchoolTest
{
    [TestClass]
    public class ErrorTest
    {
        [TestMethod]
        public void TestMethodClear()
        {
            Shared shared =  new Shared("config.txt");
            Error er = new Error(shared);
            er.exception = new Exception("Ой ошибочка");
            er.Clear();
            Assert.AreEqual(er.mode, ErrorMode.NoErrors);
            Assert.AreEqual(er.text, String.Empty);
            Assert.AreEqual(er.exception, null);
        }
    }
}
