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
        public IAsyncOperation<HttpResponse> AsGetAsync(string httpMethod)
        {
            return (IAsyncOperation<HttpResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => AsGetInternal(httpMethod));
        }
        private async Task<HttpResponse> AsGetInternal(string httpMethod)
		{
			return await GetStyleMethodInternal(httpMethod.ToUpperInvariant());
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

        private async Task<HttpResponse> GetStyleMethodInternal(string method)
        {
            var httpRequestMessage = new HttpRequestMessage(new HttpMethod(method), Url);
            var httpClient = ConfigureWebRequest(method, Url);

            //return await GetResponse(httpClient, httpRequestMessage);
            var response = new HttpResponse();
            response.ResponseStatus = ResponseStatus.None;

            try
            {
                var result = await Mendo.UWP.Network.Http.GetStringAsync(Url.ToString(), Mendo.UWP.Network.CacheMode.Skip, null, httpClient);
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

            //HttpContent httpContent = null;

            //if (HasFiles)
            //{
            //    var multipartHttpContent = new MultipartFormDataContent();

            //    foreach (var param in Parameters)
            //    {
            //        multipartHttpContent.Add(new StringContent(param.Value), param.Name);
            //    }

            //    foreach (var file in Files)
            //    {
            //        var byteContent = new ByteArrayContent(file.Content);
            //        byteContent.Headers.ContentType = string.IsNullOrEmpty(file.ContentType) ? new MediaTypeHeaderValue("application/octet-stream") : new MediaTypeHeaderValue(file.ContentType);
            //        multipartHttpContent.Add( byteContent, file.Name, file.FileName );
            //    }

            //    httpContent = multipartHttpContent;
            //}
            //else
            //{
            //   httpContent = new FormUrlEncodedContent(EncodeParameters());
            //}

            //httpRequestMessage.Content = httpContent;

            var httpClient = ConfigureWebRequest(method, Url);

            //return await GetResponse(httpClient, httpRequestMessage);
            var response = new HttpResponse();
            response.ResponseStatus = ResponseStatus.None;

            try
            {
                var p = EncodeParametersToDictionary();
                var result = await Mendo.UWP.Network.Http.PostForString(Url.ToString(), p, httpClient);
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

        //TODO: Need to figure out what this method was supposed to be doing?
		partial void AddSyncHeaderActions()
		{
			//_restrictedHeaderActions.Add("Connection", (r, v) => r.Connection = v);
			//_restrictedHeaderActions.Add("Content-Length", (r, v) => r.Content.Headers.ContentLength = Convert.ToInt64(v));
			//_restrictedHeaderActions.Add("Expect", (r, v) => r.Expect = v);
			//_restrictedHeaderActions.Add("If-Modified-Since", (r, v) => r.IfModifiedSince = Convert.ToDateTime(v));
			//_restrictedHeaderActions.Add("Referer", (r, v) => r.Referer = v);
			//_restrictedHeaderActions.Add("Transfer-Encoding", (r, v) => { r.TransferEncoding = v; r.SendChunked = true; });
			//_restrictedHeaderActions.Add("User-Agent", (r, v) => r.DefaultRequestHeaders.UserAgent = v);
		}
/*
        private async Task<HttpResponse> GetResponse(HttpClient httpClient, HttpRequestMessage httpRequestMessage)
		{
			var response = new HttpResponse();
			response.ResponseStatus = ResponseStatus.None;

			try
			{
                var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                //TODO: Move the exception handling currently in GetRawResponse here and check Result

                ExtractResponseData(response, httpResponseMessage);
            }
			catch (Exception ex)
			{
				response.ErrorMessage = ex.Message;
				response.ErrorException = ex;
				response.ResponseStatus = ResponseStatus.Error;
			}

			return response;
		}
*/
        private HttpClient ConfigureWebRequest(string method, Uri url)
		{
            //TODO: Port ContentLength, Credentials and UserAgent

            //var httpClientHandler = new HttpClientHandler();
            //httpClientHandler.UseDefaultCredentials = false;

            //AppendCookies(httpClientHandler);

            //httpClientHandler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;

            //TODO: No support for Credentials
            //if (Credentials != null)
            //{
            //    httpClientHandler.Credentials = Credentials;
            //}

            //TODO: No support for Proxy
            //if (Proxy != null)
            //{
            //    httpClientHandler.Proxy = Proxy;
            //}

            //httpClientHandler.AllowAutoRedirect = FollowRedirects;            

            //if (FollowRedirects && MaxRedirects.HasValue)
            //{
            //    httpClientHandler.MaxAutomaticRedirections = MaxRedirects.Value;
            //}

            var httpClient = Mendo.UWP.Network.Http.CreateOptimisedClient();
            //httpClient.DefaultRequestHeaders.ExpectContinue = false;
            
            AppendHeaders(httpClient);

            //if (Timeout != 0)
            //{
            //    httpClient.Timeout = new TimeSpan(0,0,0,0,Timeout);
            //}

            return httpClient;

            #region Original Code [REMOVE]
            //TODO: Remove this code once all of the problems are corrected above
            //var webRequest = (HttpWebRequest)WebRequest.Create(url);
            //webRequest.UseDefaultCredentials = false;			
            //ServicePointManager.Expect100Continue = false;

            //AppendHeaders(webRequest);
            //AppendCookies(webRequest);

            //webRequest.Method = method;

            //// make sure Content-Length header is always sent since default is -1
            //if(!HasFiles)
            //{
            //    webRequest.ContentLength = 0;
            //}

            //webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;

            //if(ClientCertificates != null)
            //{
            //    webRequest.ClientCertificates = ClientCertificates;
            //}

            //if(UserAgent.HasValue())
            //{
            //    webRequest.UserAgent = UserAgent;
            //}

            //if(Timeout != 0)
            //{
            //    webRequest.Timeout = Timeout;
            //}

            //if(Credentials != null)
            //{
            //    webRequest.Credentials = Credentials;
            //}

            //if(Proxy != null)
            //{
            //    webRequest.Proxy = Proxy;
            //}

            //webRequest.AllowAutoRedirect = FollowRedirects;
            //if(FollowRedirects && MaxRedirects.HasValue)
            //{
            //    webRequest.MaximumAutomaticRedirections = MaxRedirects.Value; 
            //}

            //return webRequest;
            #endregion
        }
	}
}