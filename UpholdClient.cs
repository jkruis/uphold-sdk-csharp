using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace UpholdSdk
{
    public class UpholdClient
    {
        string _baseUrl;
        string _clientId;
        string _clientSecret;
        string _token;

        public UpholdAccounts Accounts { get; private set; }
        public UpholdCards Cards { get; private set; }
        public UpholdCardAddresses CardAddresses { get; private set; }
        public UpholdTickers Tickers { get; private set; }
        public UpholdTransactions Transactions { get; private set; }

        public UpholdClient(string clientId, string clientSecret, bool sandbox)
        {
            _baseUrl = sandbox ? "https://api-sandbox.uphold.com" : "https://api.uphold.com";

            _clientId = clientId;
            _clientSecret = clientSecret;

            Accounts = new UpholdAccounts(this);
            Cards = new UpholdCards(this);
            CardAddresses = new UpholdCardAddresses(this);
            Tickers = new UpholdTickers(this);
            Transactions = new UpholdTransactions(this);
        }

        public UpholdClient(string clientId, string clientSecret) : this(clientId, clientSecret, false) { }

        string GetToken()
        {
            // todo : test if basic auth works, because uphold recommends it.
            bool useBasicAuthorization = false;

            var client = new RestClient($"{_baseUrl}/oauth2/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            // "We recommend encoding the clientId and clientSecret with the HTTP Basic Authentication scheme, instead of authenticating via the request body."
            //  -- https://uphold.com/en/developer/api/documentation/#client-credentials-flow
            if (useBasicAuthorization)
            {
                String auth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(_clientId + ":" + _clientSecret));
                request.AddHeader("Authorization", "Basic " + auth);
                request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials", ParameterType.RequestBody);
            }
            else
            {
                request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials&client_id={_clientId}&client_secret={_clientSecret}", ParameterType.RequestBody);
            }

            IRestResponse response = client.Execute(request);
            Models.Token result = JsonConvert.DeserializeObject<Models.Token>(response.Content);

            if (!String.IsNullOrEmpty(result.Error))
            {
                throw new AccessViolationException(result.Error);
            }

            return result.Access_Token;
        }

        string RequestGetContent(string method, bool authorise)
        {
            return RequestGetContent(method, authorise, null, null);
        }

        string RequestGetContent(string endpoint, bool authorise, int? start, int? length)
        {
            // todo : does the token expire? the docs do not mention it, but it is not unusual for client credentials tokens. (1st testresult: ttl > 5 hours)
            if (authorise && String.IsNullOrEmpty(_token))
            {
                _token = GetToken();
            }

            var client = new RestClient($"{_baseUrl}{endpoint}");
            var request = new RestRequest(Method.GET);
            if (authorise)
            {
                request.AddHeader("authorization", "Bearer " + _token);
            }
            request.AddHeader("cache-control", "no-cache");
            if (start.HasValue && length.HasValue)
            {
                request.AddHeader("range", $"items={start}-{start + length}");
            }

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public T RequestGet<T>(string endpoint, bool authorise) where T : Models.BaseUpholdEntity
        {
            string content = RequestGetContent(endpoint, authorise);
            T result = JsonConvert.DeserializeObject<T>(content);
            return result;
        }

        public List<T> RequestGetList<T>(string endpoint, bool authorise) where T : Models.BaseUpholdEntity
        {
            string content = RequestGetContent(endpoint, authorise);
            List<T> result = JsonConvert.DeserializeObject<List<T>>(content);
            return result;
        }

        public List<T> RequestGetList<T>(string endpoint, bool authorise, int start, int length) where T : Models.BaseUpholdEntity
        {
            string content = RequestGetContent(endpoint, authorise, start, length);
            List<T> result = JsonConvert.DeserializeObject<List<T>>(content);
            return result;
        }

        public TOut RequestPost<TIn, TOut>(string endpoint, TIn input) where TOut : Models.BaseUpholdEntity
        {
            if (String.IsNullOrEmpty(_token))
            {
                _token = GetToken();
            }

            var client = new RestClient($"{_baseUrl}{endpoint}");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddHeader("authorization", "Bearer " + _token);
            request.AddHeader("cache-control", "no-cache");
            request.AddJsonBody(input);

            IRestResponse response = client.Execute(request);
            TOut result = JsonConvert.DeserializeObject<TOut>(response.Content);

            if (!String.IsNullOrEmpty(result.Code))
            {
                throw new InvalidOperationException(result.Code);
            }

            return result;
        }

        public sealed class UpholdAccounts
        {
            UpholdClient _client;

            internal UpholdAccounts(UpholdClient client)
            {
                _client = client;
            }

            public List<Models.Account> List()
            {
                return _client.RequestGetList<Models.Account>("/v0/me/accounts", true);
            }

            public Models.Account Details(string accountId)
            {
                return _client.RequestGet<Models.Account>($"/v0/me/accounts/{accountId}", true);
            }

        }

        public sealed class UpholdCards
        {
            UpholdClient _client;

            internal UpholdCards(UpholdClient client)
            {
                _client = client;
            }

            public List<Models.Card> List()
            {
                return _client.RequestGetList<Models.Card>("/v0/me/cards", true);
            }

            public Models.Card Details(string cardId)
            {
                return _client.RequestGet<Models.Card>($"/v0/me/cards/{cardId}", true);
            }

            public Models.Card Create(string label, string currency)
            {
                var card = new Models.NewCard() { label = label, currency = currency };
                return _client.RequestPost<Models.NewCard, Models.Card>("/v0/me/cards", card);
            }

        }

        public sealed class UpholdCardAddresses
        {
            UpholdClient _client;

            internal UpholdCardAddresses(UpholdClient client)
            {
                _client = client;
            }

            public List<Models.CardAddress> List(string cardId)
            {
                return _client.RequestGetList<Models.CardAddress>($"/v0/me/cards/{cardId}/addresses", true);
            }

            public Models.CardAddress Create(string cardId, Models.AddressNetwork network)
            {
                var address = new Models.NewCardAddress() { network = network.ToString() };
                return _client.RequestPost<Models.NewCardAddress, Models.CardAddress>($"/v0/me/cards/{cardId}/addresses", address);
            }

        }

        public sealed class UpholdTickers
        {
            UpholdClient _client;

            internal UpholdTickers(UpholdClient client)
            {
                _client = client;
            }

            public List<Models.Ticker> List(string currency)
            {
                return _client.RequestGetList<Models.Ticker>($"/v0/ticker/{currency}", false);
            }

        }

        public sealed class UpholdTransactions
        {
            UpholdClient _client;

            internal UpholdTransactions(UpholdClient client)
            {
                _client = client;
            }

            public List<Models.Transaction> ListForUser(int start, int length)
            {
                return _client.RequestGetList<Models.Transaction>("/v0/me/transactions", true, start, length);
            }

            public List<Models.Transaction> ListForCard(string cardId, int start, int length)
            {
                return _client.RequestGetList<Models.Transaction>($"/v0/me/cards/{cardId}/transactions", true, start, length);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="cardId"></param>
            /// <param name="currency"></param>
            /// <param name="amount"></param>
            /// <param name="destination">The destination of the transaction, which can be in the form of a bitcoin address, an email address, an account id, an application id or an Uphold username.</param>
            /// <returns></returns>
            public Models.Transaction Create(string originCardId, string currency, decimal amount, string destination, string message)
            {
                // create
                var trn = new Models.NewTransaction() { denomination = new Models.NewTransactionDenomination() { amount = amount, currency = currency }, destination = destination };
                var newTrn = _client.RequestPost<Models.NewTransaction, Models.Transaction>($"/v0/me/cards/{originCardId}/transactions", trn);

                //commit
                var cmt = new Models.TransactionCommit() { Message = message };
                return _client.RequestPost<Models.TransactionCommit, Models.Transaction>($"/v0/me/cards/{originCardId}/transactions/{newTrn.Id}/commit", cmt);
            }

        }

    }
}
