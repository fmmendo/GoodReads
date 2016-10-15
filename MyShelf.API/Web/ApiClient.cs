using Mendo.UWP.Common;
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
        private SemaphoreSlim _apiSemaphore;
        private const int COOLDOWN = 1000;

        private CancellationTokenSource _cts;

        public ApiClient()
        {
            _client = new RestClient("http://www.goodreads.com");

            _apiSemaphore = new SemaphoreSlim(1, 1);
            _cts = new CancellationTokenSource();
        }

        public async Task ResetQueue()
        {
            _cts.Cancel();

            //while (_apiSemaphore.CurrentCount > 0)
            //{
            //    _apiSemaphore.Release();
            //    await Task.Delay(100);
            //}

            //_apiSemaphore = new SemaphoreSlim(1, 1);
            _cts = new CancellationTokenSource();
        }

        /// <summary>
        /// Executes a REST request.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<IRestResponse> ExecuteForRequestTokenAsync(string url, Method method, string consumerKey, string consumerSecret)
        {
            IRestResponse requestResponse = null;
            await _apiSemaphore.WaitAsync(_cts.Token);
            try
            {
                _client.Authenticator = GetRequestTokenAuthenticator(consumerKey, consumerSecret);

                var request = new RestRequest(url, method);
                requestResponse = await _client.ExecuteAsync(request);
            }
            catch (Exception)
            {
            }
            finally
            {
                ApiCooldown();
            }

            return requestResponse;
        }

        /// <summary>
        /// Executes a REST request.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<IRestResponse> ExecuteForAccessTokenAsync(string url, Method method, string consumerKey, string consumerSecret, string token, string tokenSecret)
        {
            IRestResponse requestResponse = null;
            await _apiSemaphore.WaitAsync(_cts.Token);

            try
            {
                _client.Authenticator = GetAccessTokenAuthenticator(consumerKey, consumerSecret, token, tokenSecret);

                var request = new RestRequest(url, method);
                requestResponse = await _client.ExecuteAsync(request);

            }
            catch (Exception)
            {
            }
            finally
            {
                ApiCooldown();
            }

            return requestResponse;
        }

        /// <summary>
        /// Executes a REST request.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<IRestResponse> ExecuteForProtectedResourceAsync(string url, Method method, string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret, Dictionary<string, object> parameters = null)
        {
            IRestResponse requestResponse = null;
            await _apiSemaphore.WaitAsync(_cts.Token);

            try
            {
                _client.Authenticator = GetProtectedResourceAuthenticator(consumerKey, consumerSecret, accessToken, accessTokenSecret);

                var request = new RestRequest(url, method);

                if (parameters != null)
                {
                    request.RequestFormat = DataFormat.Xml;
                    foreach (var param in parameters)
                        request.AddParameter(param.Key, param.Value);
                }

                requestResponse = await _client.ExecuteAsync(request);
            }
            catch (Exception)
            {
            }
            finally
            {
                ApiCooldown();
            }

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

            await _apiSemaphore.WaitAsync(_cts.Token);

            var result = await Mendo.UWP.Network.Http.GetStringAsync(url);
            if (result != null && result.Success)
            {
                httpResponse = result.Content;
            }
            
            ApiCooldown();

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
