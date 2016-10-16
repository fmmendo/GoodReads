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
using Windows.Foundation;
using System.Runtime.InteropServices.WindowsRuntime;
using Mendo.UWP.Network;

namespace RestSharp
{
	public interface IRestRequest
	{
		/// <summary>
		/// Container of all HTTP parameters to be passed with the request. 
		/// See AddParameter() for explanation of the types of parameters that can be passed
		/// </summary>
		IList<Parameter> Parameters { get; }

		/// <summary>
		/// Determines what HTTP method to use for this request. Supported methods: GET, POST, PUT, DELETE, HEAD, OPTIONS
		/// Default is GET
		/// </summary>
		Method Method { get; set; }

        CacheMode CacheMode { get; set; }
        TimeSpan? CacheExpiry { get; set; }

		/// <summary>
		/// The Resource URL to make the request against.
		/// Tokens are substituted with UrlSegment parameters and match by name.
		/// Should not include the scheme or domain. Do not include leading slash.
		/// Combined with RestClient.BaseUrl to assemble final URL:
		/// {BaseUrl}/{Resource} (BaseUrl is scheme + domain, e.g. http://example.com)
		/// </summary>
		/// <example>
		/// // example for url token replacement
		/// request.Resource = "Products/{ProductId}";
		///	request.AddParameter("ProductId", 123, ParameterType.UrlSegment);
		/// </example>
		string Resource { get; set; }

		/// <summary>
		/// Serializer to use when writing XML request bodies. Used if RequestFormat is Xml.
		/// By default XmlSerializer is used.
		/// </summary>
		DataFormat RequestFormat { get; set; }

		/// <summary>
		/// Used by the default deserializers to determine where to start deserializing from.
		/// Can be used to skip container or root elements that do not have corresponding deserialzation targets.
		/// </summary>
		string RootElement { get; set; }

		/// <summary>
		/// Used by the default deserializers to explicitly set which date format string to use when parsing dates.
		/// </summary>
		string DateFormat { get; set; }

		/// <summary>
		/// Used by XmlDeserializer. If not specified, XmlDeserializer will flatten response by removing namespaces from element names.
		/// </summary>
		string XmlNamespace { get; set; }

		/// <summary>
		/// Timeout in milliseconds to be used for the request. This timeout value overrides a timeout set on the RestClient.
		/// </summary>
		int Timeout { get; set; }

		/// <summary>
		/// How many attempts were made to send this Request?
		/// </summary>
		/// <remarks>
		/// This Number is incremented each time the RestClient sends the request.
		/// Useful when using Asynchronous Execution with Callbacks
		/// </remarks>
		int Attempts { get; }

		/// <summary>
		/// Calls AddParameter() for all public, readable properties specified in the white list
		/// </summary>
		/// <example>
		/// request.AddObject(product, "ProductId", "Price", ...);
		/// </example>
		/// <param name="obj">The object with properties to add as parameters</param>
		/// <param name="whitelist">The names of the properties to include</param>
		/// <returns>This request</returns>
		IRestRequest AddObject (object obj, params string[] whitelist);

		/// <summary>
		/// Calls AddParameter() for all public, readable properties of obj
		/// </summary>
		/// <param name="obj">The object with properties to add as parameters</param>
		/// <returns>This request</returns>
		IRestRequest AddObject (object obj);

		/// <summary>
		/// Add the parameter to the request
		/// </summary>
		/// <param name="p">Parameter to add</param>
		/// <returns></returns>
		IRestRequest AddParameter (Parameter p);

		/// <summary>
		/// Adds a HTTP parameter to the request (QueryString for GET, DELETE, OPTIONS and HEAD; Encoded form for POST and PUT)
		/// </summary>
		/// <param name="name">Name of the parameter</param>
		/// <param name="value">Value of the parameter</param>
		/// <returns>This request</returns>
		IRestRequest AddParameter (string name, object val);

		/// <summary>
		/// Adds a parameter to the request. There are five types of parameters:
		///	- GetOrPost: Either a QueryString value or encoded form value based on method
		///	- HttpHeader: Adds the name/value pair to the HTTP request's Headers collection
		///	- UrlSegment: Inserted into URL if there is a matching url token e.g. {AccountId}
		///	- Cookie: Adds the name/value pair to the HTTP request's Cookies collection
		///	- RequestBody: Used by AddBody() (not recommended to use directly)
		/// </summary>
		/// <param name="name">Name of the parameter</param>
		/// <param name="value">Value of the parameter</param>
		/// <param name="type">The type of parameter to add</param>
		/// <returns>This request</returns>
		IRestRequest AddParameter (string name, object val, ParameterType type);

		/// <summary>
		/// Shortcut to AddParameter(name, value, HttpHeader) overload
		/// </summary>
		/// <param name="name">Name of the header to add</param>
		/// <param name="value">Value of the header to add</param>
		/// <returns></returns>
		IRestRequest AddHeader (string name, string val);

		/// <summary>
		/// Shortcut to AddParameter(name, value, Cookie) overload
		/// </summary>
		/// <param name="name">Name of the cookie to add</param>
		/// <param name="value">Value of the cookie to add</param>
		/// <returns></returns>
		IRestRequest AddCookie (string name, string val);

		/// <summary>
		/// Shortcut to AddParameter(name, value, UrlSegment) overload
		/// </summary>
		/// <param name="name">Name of the segment to add</param>
		/// <param name="value">Value of the segment to add</param>
		/// <returns></returns>
		IRestRequest AddUrlSegment(string name, string val);

		void IncreaseNumAttempts();
	}
}