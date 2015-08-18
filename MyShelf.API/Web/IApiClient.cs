using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.API.Web
{
    public interface IApiClient
    {
        /// <summary>
        /// Executes a REST request for requesting a token.
        /// /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        Task<IRestResponse> ExecuteForRequestTokenAsync(string url,
                                                        Method method,
                                                        string consumerKey,
                                                        string consumerSecret);

        /// <summary>
        /// Executes a REST request for requesting an access token.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="token"></param>
        /// <param name="tokenSecret"></param>
        /// <returns></returns>
        Task<IRestResponse> ExecuteForAccessTokenAsync(string url,
                                                       Method method,
                                                       string consumerKey,
                                                       string consumerSecret,
                                                       string token,
                                                       string tokenSecret);

        /// <summary>
        /// Executes a REST request for accessing a protected resource.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        /// <param name="accessToken"></param>
        /// <param name="accessTokenSecret"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IRestResponse> ExecuteForProtectedResourceAsync(string url,
                                                             Method method,
                                                             string consumerKey,
                                                             string consumerSecret,
                                                             string accessToken,
                                                             string accessTokenSecret,
                                                             Dictionary<string, object> parameters = null);

        /// <summary>
        /// Performs an HTTP GET request to the given URL and returns the result.
        /// </summary>
        /// <param name="url">Target URL</param>
        /// <returns>Text returned by the response.</returns>
        Task<string> HttpGet(string url);
    }
}
