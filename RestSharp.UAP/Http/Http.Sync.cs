#region License
//   Copyright 2010 John Sheehan
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Net;
//using System.Net.Http;
using Windows.Web.Http;

//using RestSharp.Extensions;
using System.Threading.Tasks;
using System.Collections.Generic;
//using System.Net.Http.Headers;
using Windows.Foundation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Linq;
using Mendo.UWP.Network;

namespace RestSharp
{
	/// <summary>
	/// HttpWebRequest wrapper (sync methods)
	/// </summary>
	public partial class Http
	{
		/// <summary>
		/// Execute a POST request
		/// </summary>
        public IAsyncOperation<HttpResponse> PostAsync()
        {
            return (IAsyncOperation<HttpResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => PostInternal());
        }
        
        private async Task<HttpResponse> PostInternal()
        {
            return await PostPutInternal("POST");
        }

		/// <summary>
		/// Execute a PUT request
		/// </summary>
        public IAsyncOperation<HttpResponse> PutAsync()
        {
            return (IAsyncOperation<HttpResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => PutInternal());
        }
        private async Task<HttpResponse> PutInternal()
        {
            return await PostPutInternal("PUT");
        }

		/// <summary>
		/// Execute a GET request
		/// </summary>
        public IAsyncOperation<HttpResponse> GetAsync()
        {
            return (IAsyncOperation<HttpResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => GetInternal());
        }
        private async Task<HttpResponse> GetInternal()
		{
			return await GetStyleMethodInternal("GET");
		}

		/// <summary>
		/// Execute a HEAD request
		/// </summary>
        public IAsyncOperation<HttpResponse> HeadAsync()
        {
            return (IAsyncOperation<HttpResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => HeadInternal());
        }
        private async Task<HttpResponse> HeadInternal()
        {
            return await GetStyleMethodInternal("HEAD");
        }

		/// <summary>
		/// Execute an OPTIONS request
		/// </summary>
        public IAsyncOperation<HttpResponse> OptionsAsync()
        {
            return (IAsyncOperation<HttpResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => OptionsInternal());
        }
        private async Task<HttpResponse> OptionsInternal()
        {
            return await GetStyleMethodInternal("OPTIONS");
        }

		/// <summary>
		/// Execute a DELETE request
		/// </summary>
        public IAsyncOperation<HttpResponse> DeleteAsync()
        {
            return (IAsyncOperation<HttpResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => DeleteInternal());
        }
        private async Task<HttpResponse> DeleteInternal()
        {
            return await GetStyleMethodInternal("DELETE");
        }

		/// <summary>
		/// Execute a PATCH request
		/// </summary>
        public IAsyncOperation<HttpResponse> PatchAsync()
        {
            return (IAsyncOperation<HttpResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => PatchInternal());
        }
        private async Task<HttpResponse> PatchInternal()
        {
            return await PostPutInternal("PATCH");
        }

		/// <summary>
		/// Execute a GET-style request with the specified HTTP Method.  
		/// </summary>
		/// <param name="httpMethod">The HTTP method to execute.</param>
		/// <returns></returns>
        public IAsyncOperation<HttpResponse> AsGetAsync(string httpMethod, CacheMode cacheMode = CacheMode.Skip, TimeSpan? cacheExpiry = null)
        {
            return (IAsyncOperation<HttpResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => AsGetInternal(httpMethod, cacheMode, cacheExpiry));
        }
        private async Task<HttpResponse> AsGetInternal(string httpMethod, CacheMode cacheMode = CacheMode.Skip, TimeSpan? cacheExpiry = null)
		{
			return await GetStyleMethodInternal(httpMethod.ToUpperInvariant(), cacheMode, cacheExpiry);
		}

		/// <summary>
		/// Execute a POST-style request with the specified HTTP Method.  
		/// </summary>
		/// <param name="httpMethod">The HTTP method to execute.</param>
		/// <returns></returns>
        public IAsyncOperation<HttpResponse> AsPostAsync(string httpMethod)
        {
            return (IAsyncOperation<HttpResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => AsPostInternal(httpMethod));
        }
        private async Task<HttpResponse> AsPostInternal(string httpMethod)
        {
            return await PostPutInternal(httpMethod.ToUpperInvariant());
        }

        private async Task<HttpResponse> GetStyleMethodInternal(string method, CacheMode cacheMode = CacheMode.Skip, TimeSpan? cacheExpiry = null)
        {
            var httpRequestMessage = new HttpRequestMessage(new HttpMethod(method), Url);
            var httpClient = ConfigureWebRequest(method, Url);

            //return await GetResponse(httpClient, httpRequestMessage);
            var response = new HttpResponse();
            response.ResponseStatus = ResponseStatus.None;

            try
            {
                var result = await Mendo.UWP.Network.Http.GetStringAsync(Url.ToString(), cacheMode, cacheExpiry, httpClient);
                //var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                //TODO: Move the exception handling currently in GetRawResponse here and check Result

                ExtractResponseData(response, result);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.ErrorException = ex;
                response.ResponseStatus = ResponseStatus.Error;
            }

            return response;
        }

        private async Task<HttpResponse> PostPutInternal(string method)
        {
            var httpRequestMessage = new HttpRequestMessage(new HttpMethod(method), Url);

            var httpClient = ConfigureWebRequest(method, Url);
            
            var response = new HttpResponse();
            response.ResponseStatus = ResponseStatus.None;

            try
            {
                var p = EncodeParametersToDictionary();
                var result = await Mendo.UWP.Network.Http.PostForString(Url.ToString(), p, httpClient);


                //TODO: Move the exception handling currently in GetRawResponse here and check Result

                ExtractResponseData(response, result);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.ErrorException = ex;
                response.ResponseStatus = ResponseStatus.Error;
            }

            return response;
        }

        //TODO: Need to figure out what this method was supposed to be doing?
		partial void AddSyncHeaderActions()
		{
		}

        private HttpClient ConfigureWebRequest(string method, Uri url)
		{
            var httpClient = Mendo.UWP.Network.Http.CreateOptimisedClient();
           
            AppendHeaders(httpClient);

            return httpClient;
        }
	}
}