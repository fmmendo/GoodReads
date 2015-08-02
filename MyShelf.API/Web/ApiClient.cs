using Mendo.UAP.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp.Authenticators;

namespace MyShelf.API.Web
{
    public class ApiClient : Singleton<ApiClient>, IApiClient
    {
        private readonly SemaphoreSlim _apiSemaphore = new SemaphoreSlim(1, 1);
        private const int COOLDOWN_MILLISECONDS = 1000;

        public IAuthenticator Authenticator
        {
            get { return _client?.Authenticator; }
            set { _client.Authenticator = value; }
        }

        public ApiClient()
        {
            _client = new RestClient("http://www.goodreads.com");
        }

        public async Task<IRestResponse> ExecuteAsync(string url, Method method)
        {
            await _apiSemaphore.WaitAsync();

            var request = new RestRequest(url, method);
            var requestResponse = await _client.ExecuteAsync(request);

            ApiCooldown();

            return requestResponse;
        }

        /// <summary>
        /// Adds a 1second delay before releasing the api call semaphore
        /// </summary>
        /// <returns></returns>
        private async Task ApiCooldown()
        {
            await Task.Delay(COOLDOWN_MILLISECONDS);
            _apiSemaphore.Release();
        }

        public static OAuth1Authenticator GetRequestTokenAuthenticator(string consumerKey, string consumerSecret) => OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret);
        public static OAuth1Authenticator GetAccessTokenAuthenticator(string consumerKey, string consumerSecret, string token, string tokenSecret) => OAuth1Authenticator.ForAccessToken(consumerKey, consumerSecret, token, tokenSecret);

        private RestClient _client;
    }
}
