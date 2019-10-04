using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UpholdSdkTest
{
    [TestClass]
    public class CardsTest : TestBase
    {
        [TestClass]
        public class List : CardsTest
        {
            [TestMethod]
            public void ReturnsList()
            {
                List<UpholdSdk.Models.Card> cards = null;

                try
                {
                    cards = client.Cards.List();
                }
                catch (Exception ex)
                {
                    Assert.Fail("Cards.List failed with error: " + ex.Message);
                }

                Assert.IsNotNull(cards);
            }

        }
    }
}
