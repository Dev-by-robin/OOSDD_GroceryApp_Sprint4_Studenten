using Grocery.Core.Helpers;
using Grocery.Core.Models;

namespace TestCore
{
    public class TestHelpers
    {
        [SetUp]
        public void Setup()
        {
        }


        //Happy flow
        [Test]
        public void TestPasswordHelperReturnsTrue()
        {
            string password = "user3";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsTrue(string password, string passwordHash)
        {
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }


        //Unhappy flow
        [Test]
        public void TestPasswordHelperReturnsFalse()
        {
            //Assert.Pass(); //Zelf uitwerken
            string password = "user3";
            string passwordHash = "notvalid123";
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [TestCase("user1", "notvalid1233408")]
        [TestCase("user3", "notvalid123")]
        public void TestPasswordHelperReturnsFalse(string password, string passwordHash)
        {
            //Assert.Fail(); //Zelf uitwerken zodat de test slaagt!
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }




        // Happy Flow
        [Test]
        public void CanUserSeeBoughtProductsViewTrue()
        {
            Client.Role userRole = Client.Role.Admin;
            Assert.IsTrue(userRole == Client.Role.Admin);
        }

        [TestCase(Client.Role.Admin)]
        public void CanUserSeeBoughtProductsViewTrue(Client.Role userRole)
        {
            Assert.IsTrue(userRole == Client.Role.Admin);
        }

        // unhappy Flow
        [Test]
        public void CanUserSeeBoughtProductsViewFalse()
        {
            Client.Role userRole = Client.Role.None;
            Assert.IsFalse(userRole == Client.Role.Admin);
        }

        [TestCase(Client.Role.None)]
        public void CanUserSeeBoughtProductsViewFalse(Client.Role userRole)
        {
            Assert.IsFalse(userRole == Client.Role.Admin);
        }
    }
}