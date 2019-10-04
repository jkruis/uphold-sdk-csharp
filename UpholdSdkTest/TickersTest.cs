using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UpholdSdkTest
{
    [TestClass]
    public class TickersTests : TestBase
    {
        [TestClass]
        public class ListForCurrency : TickersTests
        {
            [TestMethod]
            public void ReturnsList()
            {
                List<UpholdSdk.Models.Ticker> tickers = null;

                try
                {
                    tickers = client.Tickers.ListForCurrency("EUR");
                }
                catch (Exception ex)
                {
                    Assert.Fail("Tickers.ListForCurrency failed with error: " + ex.Message);
                }

                Assert.IsNotNull(tickers);
            }

        }

        [TestClass]
        public class ListForPair : TickersTests
        {
            [TestMethod]
            public void ReturnsList()
            {
                List<UpholdSdk.Models.Ticker> tickers = null;

                try
                {
                    tickers = client.Tickers.ListForPair("EUR");
                }
                catch (Exception ex)
                {
                    Assert.Fail("Tickers.ListForCurrency failed with error: " + ex.Message);
                }

                Assert.IsNotNull(tickers);
            }

        }

    }
}
