using Mendo.UAP.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp.Authenticators;
using System.Net;
using System.IO;

namespace MyShelf.API.Web
{
    public class ApiClient : Singleton<ApiClient>, IApiClient
    {
        private readonly SemaphoreSlim _apiSemaphore = new SemaphoreSlim(1, 1);
        private const int COOLDOWN = 1000;



        public ApiClient()
        {
            _client = new RestClient("http://www.goodreads.com");
        }

        /// <summary>
        /// Executes a REST request.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<IRestResponse> ExecuteForRequestTokenAsync(string url, Method method, string consumerKey, string consumerSecret)
        {
            await _apiSemaphore.WaitAsync();

            _client.Authenticator = GetRequestTokenAuthenticator(consumerKey, consumerSecret);

            var request = new RestRequest(url, method);
            var requestResponse = await _client.ExecuteAsync(request);

            ApiCooldown();

            return requestResponse;
        }        /// <summary>
                 /// Executes a REST request.
                 /// </summary>
                 /// <param name="url"></param>
                 /// <param name="method"></param>
                 /// <returns></returns>
        public async Task<IRestResponse> ExecuteForAccessTokenAsync(string url, Method method, string consumerKey, string consumerSecret, string token, string tokenSecret)
        {
            await _apiSemaphore.WaitAsync();

            _client.Authenticator = GetAccessTokenAuthenticator(consumerKey, consumerSecret, token, tokenSecret);

            var request = new RestRequest(url, method);
            var requestResponse = await _client.ExecuteAsync(request);

            ApiCooldown();

            return requestResponse;
        }        /// <summary>
                 /// Executes a REST request.
                 /// </summary>
                 /// <param name="url"></param>
                 /// <param name="method"></param>
                 /// <returns></returns>
        public async Task<IRestResponse> ExecuteForProtectedResourceAsync(string url, Method method, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            await _apiSemaphore.WaitAsync();

            _client.Authenticator = GetProtectedResourceAuthenticator(consumerKey, consumerSecret, accessToken, accessTokenSecret);

            var request = new RestRequest(url, method);
            var requestResponse = await _client.ExecuteAsync(request);

            ApiCooldown();

            return requestResponse;
        }

        /// <summary>
        /// Performs an HTTP GET request to the given URL and returns the result.
        /// </summary>
        /// <param name="url">Target URL</param>
        /// <returns>Text returned by the response.</returns>
        public async Task<string> HttpGet(string url)
        {
            string httpResponse = null;

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            Request.Method = "GET";
            Request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                await _apiSemaphore.WaitAsync();

                HttpWebResponse response = (HttpWebResponse)await Request.GetResponseAsync();
                if (response != null)
                {
                    StreamReader data = new StreamReader(response.GetResponseStream());
                    httpResponse = await data.ReadToEndAsync();
                }
            }
            catch (WebException)
            {
            }
            finally
            {
                ApiCooldown();
            }

            return httpResponse;
        }

        /// <summary>
        /// Adds a 1second delay before releasing the api call semaphore
        /// </summary>
        /// <returns></returns>
        private async Task ApiCooldown()
        {
            await Task.Delay(COOLDOWN);

            _apiSemaphore.Release();
        }

        private OAuth1Authenticator GetRequestTokenAuthenticator(string consumerKey, string consumerSecret) => OAuth1Authenticator.ForRequestToken(consumerKey, consumerSecret);
        private OAuth1Authenticator GetAccessTokenAuthenticator(string consumerKey, string consumerSecret, string token, string tokenSecret) => OAuth1Authenticator.ForAccessToken(consumerKey, consumerSecret, token, tokenSecret);
        private OAuth1Authenticator GetProtectedResourceAuthenticator(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret) => OAuth1Authenticator.ForProtectedResource(consumerKey, consumerSecret, accessToken, accessTokenSecret);

        private RestClient _client;
    }
}
