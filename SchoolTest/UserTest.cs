using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoSchool.Models;

namespace SchoolTest
{
    [TestClass]
    public class UserTest
    {
        Shared shared;

        public UserTest ()
        {
            shared = new Shared();
        }

        [TestMethod]
        public void TestSelect()
        {
            User user = new User(shared);
            user.Select("1");
            Assert.AreEqual("fformula@gmail.com", user.email);
        }
    }
}
