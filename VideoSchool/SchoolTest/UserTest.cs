using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoSchool.Models;

namespace SchoolTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestSelect()
        {
            Shared shared = new Shared(RunMode.UnitTest);
            //shared.config.SaveFromTest();
            User user = new User(shared);
            user.Select("1");
            Assert.AreEqual("fformula@gmail.com", user.email);
        }
    }
}
