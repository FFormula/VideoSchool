﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VideoSchool.Models.Units;
using VideoSchool.Models.Share;

namespace SchoolTest
{
    [TestClass]
    public class UserTest
    {
        string configFilename = "config.txt";
        Shared shared;

        [TestMethod]
        public void TestSelect()
        {
            shared = new Shared(configFilename);
            User user = new User(shared);
            user.Select("1");
            Assert.AreEqual("fformula@gmail.com", user.email);
        }

        [TestMethod]
        public void TestInsertAndDelete()
        {
            shared = new Shared(configFilename);

            var query = @"
		    INSERT INTO user
		        SET name = 'TEST_USER',
		            email = 'test_user@test_email.com',
		            passw = password('-----'),
		            status = '1'";
            var id = shared.db.Insert(query);

            Assert.AreEqual(true, id > 0);

            query = @"
		    DELETE FROM user
		        WHERE name = 'TEST_USER'
		            AND email = 'test_user@test_email.com'";

            shared.db.Update(query);
        }

        [TestMethod]
        public void TestUpdate()
        {
            shared = new Shared(configFilename);

            var query = @"
		    INSERT INTO user
		        SET name = 'TEST_USER',
		            email = 'test_user@test_email.com',
		            passw = password('-----'),
		            status = '1'";

            var id = shared.db.Insert(query);
            Assert.AreEqual(true, id > 0);

            query = @"
		    UPDATE user
                SET name = 'TEST_USER_RENAMED'
		        WHERE name = 'TEST_USER'
		            AND email = 'test_user@test_email.com'";

            shared.db.Update(query);

            query = @"
		    DELETE FROM user
		        WHERE name = 'TEST_USER_RENAMED'
		            AND email = 'test_user@test_email.com'";

            shared.db.Update(query);
        }
    }
}
