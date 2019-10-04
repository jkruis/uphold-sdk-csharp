using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UpholdSdkTest
{
    [TestClass]
    public class ContactsTest : TestBase
    {
        [TestClass]
        public class List : ContactsTest
        {
            [TestMethod]
            public void ReturnsList()
            {
                List<UpholdSdk.Models.Contact> contacts = null;

                try
                {
                    contacts = client.Contacts.List();
                }
                catch (Exception ex)
                {
                    Assert.Fail("Contacts.List failed with error: " + ex.Message);
                }

                Assert.IsNotNull(contacts);
            }

        }
    }
}
