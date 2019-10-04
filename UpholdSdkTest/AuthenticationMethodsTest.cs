using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UpholdSdkTest
{
    [TestClass]
    public class AuthenticationMethodsTest : TestBase
    {
        [TestClass]
        public class List : AuthenticationMethodsTest
        {
            [TestMethod]
            public void ReturnsList()
            {
                List<UpholdSdk.Models.AuthenticationMethod> methods = null;

                try
                {
                    methods = client.AuthenticationMethods.List();
                }
                catch (Exception ex)
                {
                    Assert.Fail("AuthenticationMethod.List failed with error: " + ex.Message);
                }

                Assert.IsNotNull(methods);
            }

        }
    }
}
