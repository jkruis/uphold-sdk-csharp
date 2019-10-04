using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UpholdSdkTest
{
    [TestClass]
    public class AccountsTest : TestBase
    {
        [TestClass]
        public class List : AccountsTest
        {
            [TestMethod]
            public void ReturnsList()
            {
                List<UpholdSdk.Models.Account> accounts = null;

                try
                {
                    accounts = client.Accounts.List();
                }
                catch (Exception ex)
                {
                    Assert.Fail("Accounts.List failed with error: " + ex.Message);
                }

                Assert.IsNotNull(accounts);
            }

        }
    }
}
