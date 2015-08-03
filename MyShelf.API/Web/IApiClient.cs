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
        /// 
        /// </summary>
        IAuthenticator Authenticator { get; set; }

        /// <summary>
        /// Executes a REST request.
        ///         /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        Task<IRestResponse> ExecuteAsync(string url, Method method);
        
        /// <summary>
        /// Performs an HTTP GET request to the given URL and returns the result.
        /// </summary>
        /// <param name="url">Target URL</param>
        /// <returns>Text returned by the response.</returns>
        Task<string> HttpGet(string url);
    }
}
