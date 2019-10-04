using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UpholdSdkTest
{
    [TestClass]
    public class CardAddressesTest : TestBase
    {
        [TestClass]
        public class List : CardAddressesTest
        {
            [TestMethod]
            public void ReturnsList()
            {
                List<UpholdSdk.Models.CardAddress> addresses = null;

                try
                {
                    List<UpholdSdk.Models.Card> cards = client.Cards.List();
                    if (!cards.Any())
                        Assert.Fail("There are no cards to test addresses with.");

                    addresses = client.CardAddresses.List(cards.First().Id);
                }
                catch (Exception ex)
                {
                    Assert.Fail("CardAddresses.List failed with error: " + ex.Message);
                }

                Assert.IsNotNull(addresses);
            }

        }
    }
}
