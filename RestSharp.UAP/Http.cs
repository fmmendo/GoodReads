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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using RestSharp.Extensions;
//using System.Net.Http;
using Windows.Web.Http;
using System.Net;
using Mendo.UWP.Extensions;

namespace RestSharp
{
	/// <summary>
	/// HttpWebRequest wrapper
	/// </summary>
	public sealed partial class Http : IHttp, IHttpFactory
	{
		//private static readonly Encoding _defaultEncoding = Encoding.UTF8;

		///<summary>
		/// Creates an IHttp
		///</summary>
		///<returns></returns>
		public IHttp Create()
		{
			return new Http();
		}

		/// <summary>
		/// True if this HTTP request has any HTTP parameters
		/// </summary>
		protected bool HasParameters
		{
			get
			{
				return Parameters.Any();
			}
		}

		/// <summary>
		/// True if this HTTP request has any HTTP cookies
		/// </summary>
		protected bool HasCookies
		{
			get
			{
				return Cookies.Any();
			}
		}

		/// <summary>
		/// True if a request body has been specified
		/// </summary>
		protected bool HasBody
		{
			get
			{
				return !string.IsNullOrEmpty(RequestBody);
			}
		}

		/// <summary>
		/// True if files have been set to be uploaded
		/// </summary>
		protected bool HasFiles
		{
			get
			{
				return Files.Any();
			}
		}

		/// <summary>
		/// UserAgent to be sent with request
		/// </summary>
		public string UserAgent { get; set; }
		/// <summary>
		/// Timeout in milliseconds to be used for the request
		/// </summary>
		public int Timeout { get; set; }

		/// <summary>
		/// System.Net.ICredentials to be sent with request
		/// </summary>
        //TODO: Add support for ICredentials
		//public ICredentials Credentials { get; set; }

		/// <summary>
		/// The System.Net.CookieContainer to be used for the request
		/// </summary>
        //TODO: Add support for Cookies
		//public CookieContainer CookieContainer { get; set; }
        
		/// <summary>
		/// Collection of files to be sent with request
		/// </summary>
		public IList<HttpFile> Files { get; private set; }

        /// <summary>
        /// Whether or not HTTP 3xx response redirects should be automatically followed
        /// </summary>
        public bool FollowRedirects { get; set; }

		/// <summary>
		/// X509CertificateCollection to be sent with request
		/// </summary>
        //TODO: Add support for X509 Certificates
		//public X509CertificateCollection ClientCertificates { get; set; }
		
        /// <summary>
		/// Maximum number of automatic redirects to follow if FollowRedirects is true
		/// </summary>
		public int? MaxRedirects { get; set; }

		/// <summary>
		/// HTTP headers to be sent with request
		/// </summary>
		public IList<HttpHeader> Headers { get; private set; }
		/// <summary>
		/// HTTP parameters (QueryString or Form values) to be sent with request
		/// </summary>
		public IList<HttpParameter> Parameters { get; private set; }
		/// <summary>
		/// HTTP cookies to be sent with request
		/// </summary>
		public IList<HttpCookie> Cookies { get; private set; }
		/// <summary>
		/// Request body to be sent with request
		/// </summary>
		public string RequestBody { get; set; }
		/// <summary>
		/// Content type of the request body.
		/// </summary>
		public string RequestContentType { get; set; }
		/// <summary>
		/// URL to call for this request
		/// </summary>
		public Uri Url { get; set; }

		/// <summary>
		/// Proxy info to be sent with request
		/// </summary>
        //TODO: Add support for Proxy
		//public IWebProxy Proxy { get; set; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public Http()
		{
			Headers = new List<HttpHeader>();
			Files = new List<HttpFile>();
			Parameters = new List<HttpParameter>();
			Cookies = new List<HttpCookie>();

			_restrictedHeaderActions = new Dictionary<string, Action<HttpRequestMessage, string>>(StringComparer.OrdinalIgnoreCase);

			AddSharedHeaderActions();
			AddSyncHeaderActions();
		}

		partial void AddSyncHeaderActions();
		partial void AddAsyncHeaderActions();
		private void AddSharedHeaderActions()
		{
			//_restrictedHeaderActions.Add("Accept", (r, v) => r.Headers.Accept = v);
			//_restrictedHeaderActions.Add("Content-Type", (r, v) => r.Content.Headers.ContentType = v);

			_restrictedHeaderActions.Add("Date", (r, v) =>
				{
					DateTime parsed;
					if (DateTime.TryParse(v, out parsed))
					{
						r.Headers.Date = parsed;
					}
				});
			_restrictedHeaderActions.Add("Host", (r, v) => r.Headers.Host = new Windows.Networking.HostName(v));
            _restrictedHeaderActions.Add("Range", (r, v) => { AddRange(r, v); });
		}

        private readonly IDictionary<string, Action<HttpRequestMessage, string>> _restrictedHeaderActions;

		// handle restricted headers the .NET way - thanks @dimebrain!
		// http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.headers.aspx
		//private void AppendHeaders(HttpWebRequest webRequest)
        //TODO: figure out where this is called from and what should actually be added here
        private void AppendHeaders(HttpClient httpClient)
		{
			foreach (var header in Headers)
			{
				if (_restrictedHeaderActions.ContainsKey(header.Name))
				{
					//_restrictedHeaderActions[header.Name].Invoke(httpClient, header.Value);
				}
				else
				{
                    httpClient.DefaultRequestHeaders.Add(header.Name, header.Value);
				}
			}
		}

        //private void AppendCookies(HttpClientHandler httpClientHandler)
        //{

        //    //webRequest.CookieContainer = this.CookieContainer ?? new CookieContainer();
        //    //httpClientHandler.CookieContainer = this.CookieContainer ?? new CookieContainer();

        //    foreach (var httpCookie in Cookies)
        //    {
        //        var cookie = new Cookie
        //        {
        //            Name = httpCookie.Name,
        //            Value = httpCookie.Value,
        //            Domain = Url.Host
        //        };
        //        httpClientHandler.CookieContainer.Add(Url, cookie);
        //    }
        //}

        private List<KeyValuePair<string, string>> EncodeParameters()
        {
            var parameters = new List<KeyValuePair<string, string>>();

			foreach (var p in Parameters)
			{
                parameters.Add(new KeyValuePair<string, string>(p.Name, p.Value));
			}
            return parameters;
        }

        private Dictionary<string, string> EncodeParametersToDictionary()
        {
            var parameters = new Dictionary<string, string>();

            foreach (var p in Parameters)
            {
                parameters.Add(p.Name, p.Value);
            }
            return parameters;
        }

        //private static void WriteStringTo(Stream stream, string toWrite)
        //{
        //    var bytes = _defaultEncoding.GetBytes(toWrite);
        //    stream.Write(bytes, 0, bytes.Length);
        //}

        private static void ExtractResponseData(HttpResponse response, Mendo.UWP.Network.HttpResult<string> httpResponseMessage)
		{
            //using (httpResponseMessage)
            {
                //response.ContentEncoding = httpResponseMessage.Content.Headers.ContentEncoding;
                //response.Server = httpResponseMessage.Headers.Server;
                response.ContentType = "json";// httpResponseMessage.Content.Headers.ContentType.MediaType;
                //response.ContentLength = httpResponseMessage.Content.Headers.ContentLength.Value;

                response.RawBytes = httpResponseMessage.Content.GetBytes();// ReadAsByteArrayAsync().Result;

                // NOTE: Commented out in original code
                //response.Content = GetString(response.RawBytes);

                response.StatusCode = (int)httpResponseMessage.StatusCode;
                response.StatusDescription = "OK";// httpResponseMessage.ReasonPhrase;
                //response.ResponseUri = httpResponseMessage
                response.ResponseStatus = ResponseStatus.Completed;

                // cookies

                //foreach (var header in httpResponseMessage.Headers.ToArray() )
                {
                    //var headerValue = httpResponseMessage.Headers.GetValues(header.Key);
                    //response.Headers.Add(new HttpHeader { Name = header.Key, Value = header.Value });
                }
            }

            #region Original Code [REMOVE]
            //TODO: Remove this code once we resolved all conflicts
            //using (webResponse)
            //{
            //    response.ContentEncoding = webResponse.ContentEncoding;
            //    response.Server = webResponse.Server;
            //    response.ContentType = webResponse.ContentType;
            //    response.ContentLength = webResponse.ContentLength;

            //    response.RawBytes = webResponse.GetResponseStream().ReadAsBytes();

            //    //response.Content = GetString(response.RawBytes);

            //    response.StatusCode = webResponse.StatusCode;
            //    response.StatusDescription = webResponse.StatusDescription;
            //    response.ResponseUri = webResponse.ResponseUri;
            //    response.ResponseStatus = ResponseStatus.Completed;

            //    if (webResponse.Cookies != null)
            //    {
            //        foreach (Cookie cookie in webResponse.Cookies)
            //        {
            //            response.Cookies.Add(new HttpCookie {
            //                Comment = cookie.Comment,
            //                CommentUri = cookie.CommentUri,
            //                Discard = cookie.Discard,
            //                Domain = cookie.Domain,
            //                Expired = cookie.Expired,
            //                Expires = cookie.Expires,
            //                HttpOnly = cookie.HttpOnly,
            //                Name = cookie.Name,
            //                Path = cookie.Path,
            //                Port = cookie.Port,
            //                Secure = cookie.Secure,
            //                TimeStamp = cookie.TimeStamp,
            //                Value = cookie.Value,
            //                Version = cookie.Version
            //            });
            //        }
            //    }

            //    foreach (var headerName in webResponse.Headers.AllKeys)
            //    {
            //        var headerValue = webResponse.Headers[headerName];
            //        response.Headers.Add(new HttpHeader { Name = headerName, Value = headerValue });
            //    }

            //    webResponse.Close();
            //}
            #endregion
        }

        private void AddRange(HttpRequestMessage r, string range)
		{
			System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(range, "=(\\d+)-(\\d+)$");
			if (!m.Success)
			{
				return;
			}

			int from = Convert.ToInt32(m.Groups[1].Value);
			int to = Convert.ToInt32(m.Groups[2].Value);
            //r.Headers.Range.Ranges.Add(new System.Net.Http.Headers.RangeItemHeaderValue(from, to));
		}
	}
}