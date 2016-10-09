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
using System.Collections.Generic;
using Windows.Foundation;
//using System.Security.Cryptography.X509Certificates;

namespace RestSharp
{
	/// <summary>
	/// 
	/// </summary>
	public interface IRestClient
	{
		/// <summary>
		/// 
		/// </summary>
        //TODO: Add support for Cookies
		//CookieContainer CookieContainer { get; set; }
		/// <summary>
		/// 
		/// </summary>
		string UserAgent { get; set; }
		/// <summary>
		/// 
		/// </summary>
		int Timeout { get; set; }
		/// <summary>
		/// 
		/// </summary>
		bool UseSynchronizationContext { get; set; }
		/// <summary>
		/// 
		/// </summary>
		IAuthenticator Authenticator { get; set; }
		/// <summary>
		/// 
		/// </summary>
		string BaseUrl { get; set; }
		/// <summary>
		/// 
		/// </summary>
		IList<Parameter> DefaultParameters { get; }
		/// <summary>
		/// X509CertificateCollection to be sent with request
		/// </summary>
        //TODO: Add support for X509 Certificates
//		X509CertificateCollection ClientCertificates { get; set; }

		IAsyncOperation<IRestResponse> ExecuteAsync(IRestRequest request);
		
        //TODO: Add support for Proxy
//		IWebProxy Proxy { get; set; }

		Uri BuildUri(IRestRequest request);

		IAsyncOperation<IRestResponse> ExecuteAsGetAsync(IRestRequest request, string httpMethod);
		IAsyncOperation<IRestResponse> ExecuteAsPostAsync(IRestRequest request, string httpMethod);
	}
}
