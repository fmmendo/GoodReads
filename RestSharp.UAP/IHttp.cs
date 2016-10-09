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
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;

namespace RestSharp
{
	public interface IHttp
	{
        //TODO: Add support for Cookies
		//CookieContainer CookieContainer { get; set; }
        //TODO: Add support for Credentials
		//ICredentials Credentials { get; set; }

		string UserAgent { get; set; }
		int Timeout { get; set; }

		int? MaxRedirects { get; set; }

		IList<HttpHeader> Headers { get; }
		IList<HttpParameter> Parameters { get; }
		IList<HttpFile> Files { get; }
		IList<HttpCookie> Cookies { get; }
		string RequestBody { get; set; }
		string RequestContentType { get; set; }

		Uri Url { get; set; }

		IAsyncOperation<HttpResponse> DeleteAsync();
        IAsyncOperation<HttpResponse> GetAsync();
        IAsyncOperation<HttpResponse> HeadAsync();
        IAsyncOperation<HttpResponse> OptionsAsync();
        IAsyncOperation<HttpResponse> PostAsync();
        IAsyncOperation<HttpResponse> PutAsync();
        IAsyncOperation<HttpResponse> PatchAsync();
        IAsyncOperation<HttpResponse> AsPostAsync(string httpMethod);
        IAsyncOperation<HttpResponse> AsGetAsync(string httpMethod);

        //TODO: Add support for Proxy
		//IWebProxy Proxy { get; set; }
	}
}