using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace UpholdSdkTest
{
    public class TestBase
    {
        protected UpholdSdk.UpholdClient client = null;

        public TestBase()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("client-secrets.json")
                .Build();

            var clientId = config["UpholdClientId"];
            var clientSecret = config["UpholdClientSecret"];

            if (String.IsNullOrEmpty(clientId) || String.IsNullOrEmpty(clientSecret))
                throw new InvalidOperationException("ClientId or ClientSecret not set.");

            client = new UpholdSdk.UpholdClient(clientId, clientSecret, true);
        }

    }
}
