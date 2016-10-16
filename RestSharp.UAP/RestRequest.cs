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
using System.Net;
//using RestSharp.Extension
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Foundation;
using Mendo.UWP.Network;

namespace RestSharp
{
    /// <summary>
	/// Container for data used to make requests
	/// </summary>
	public sealed class RestRequest : IRestRequest
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public RestRequest()
		{
			Parameters = new List<Parameter>();
		}

		/// <summary>
		/// Sets Resource property
		/// </summary>
		/// <param name="resource">Resource to use for this request</param>
        public RestRequest(string resource)
            : this(resource, Method.GET)
        {
        }

		/// <summary>
		/// Sets Resource and Method properties
		/// </summary>
		/// <param name="resource">Resource to use for this request</param>
		/// <param name="method">Method to use for this request</param>
        public RestRequest(string resource, Method method)
            : this()
        {
            Resource = resource;
            Method = method;
        }

		/// <summary>
		/// Calls AddParameter() for all public, readable properties specified in the white list
		/// </summary>
		/// <example>
		/// request.AddObject(product, "ProductId", "Price", ...);
		/// </example>
		/// <param name="obj">The object with properties to add as parameters</param>
		/// <param name="whitelist">The names of the properties to include</param>
		/// <returns>This request</returns>
		public IRestRequest AddObject (object obj, params string[] whitelist)
		{
			// automatically create parameters from object props
			var type = obj.GetType();

            var props = System.Reflection.IntrospectionExtensions.GetTypeInfo(type).DeclaredProperties;

			foreach (var prop in props)
			{
				bool isAllowed = whitelist.Length == 0 || (whitelist.Length > 0 && whitelist.Contains(prop.Name));

				if (isAllowed)
				{
					var propType = prop.PropertyType;
					var val = prop.GetValue(obj, null);

					if (val != null)
					{
						if (propType.IsArray)
						{
							val = string.Join(",", (string[])val);
						}

						AddParameter(prop.Name, val);
					}
				}
			}

			return this;
		}

		/// <summary>
		/// Calls AddParameter() for all public, readable properties of obj
		/// </summary>
		/// <param name="obj">The object with properties to add as parameters</param>
		/// <returns>This request</returns>
		public IRestRequest AddObject (object obj)
		{
			AddObject(obj, new string[] { });
			return this;
		}

		/// <summary>
		/// Add the parameter to the request
		/// </summary>
		/// <param name="p">Parameter to add</param>
		/// <returns></returns>
		public IRestRequest AddParameter (Parameter p)
		{
			Parameters.Add(p);
			return this;
		}

		/// <summary>
		/// Adds a HTTP parameter to the request (QueryString for GET, DELETE, OPTIONS and HEAD; Encoded form for POST and PUT)
		/// </summary>
		/// <param name="name">Name of the parameter</param>
		/// <param name="value">Value of the parameter</param>
		/// <returns>This request</returns>
		public IRestRequest AddParameter (string name, object val)
		{
			return AddParameter(new Parameter { Name = name, Value = val, Type = ParameterType.GetOrPost });
		}

		/// <summary>
		/// Adds a parameter to the request. There are four types of parameters:
		///	- GetOrPost: Either a QueryString value or encoded form value based on method
		///	- HttpHeader: Adds the name/value pair to the HTTP request's Headers collection
		///	- UrlSegment: Inserted into URL if there is a matching url token e.g. {AccountId}
		///	- RequestBody: Used by AddBody() (not recommended to use directly)
		/// </summary>
		/// <param name="name">Name of the parameter</param>
		/// <param name="value">Value of the parameter</param>
		/// <param name="type">The type of parameter to add</param>
		/// <returns>This request</returns>
		public IRestRequest AddParameter (string name, object val, ParameterType type)
		{
			return AddParameter(new Parameter { Name = name, Value = val, Type = type });
		}

		/// <summary>
		/// Shortcut to AddParameter(name, value, HttpHeader) overload
		/// </summary>
		/// <param name="name">Name of the header to add</param>
		/// <param name="value">Value of the header to add</param>
		/// <returns></returns>
		public IRestRequest AddHeader (string name, string val)
		{
			return AddParameter(name, val, ParameterType.HttpHeader);
		}

		/// <summary>
		/// Shortcut to AddParameter(name, value, Cookie) overload
		/// </summary>
		/// <param name="name">Name of the cookie to add</param>
		/// <param name="value">Value of the cookie to add</param>
		/// <returns></returns>
		public IRestRequest AddCookie (string name, string val)
		{
			return AddParameter(name, val, ParameterType.Cookie);
		}

		/// <summary>
		/// Shortcut to AddParameter(name, value, UrlSegment) overload
		/// </summary>
		/// <param name="name">Name of the segment to add</param>
		/// <param name="value">Value of the segment to add</param>
		/// <returns></returns>
		public IRestRequest AddUrlSegment (string name, string val)
		{
			return AddParameter(name, val, ParameterType.UrlSegment);
		}

		/// <summary>
		/// Container of all HTTP parameters to be passed with the request. 
		/// See AddParameter() for explanation of the types of parameters that can be passed
		/// </summary>
		public IList<Parameter> Parameters { get; private set; }

		private Method _method = Method.GET;
		/// <summary>
		/// Determines what HTTP method to use for this request. Supported methods: GET, POST, PUT, DELETE, HEAD, OPTIONS
		/// Default is GET
		/// </summary>
		public Method Method
		{
			get { return _method; }
			set { _method = value; }
		}

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
		public string Resource { get; set; }

		private DataFormat _requestFormat = DataFormat.Xml;
		/// <summary>
		/// Serializer to use when writing XML request bodies. Used if RequestFormat is Xml.
		/// By default XmlSerializer is used.
		/// </summary>
		public DataFormat RequestFormat
		{
			get
			{
				return _requestFormat;
			}
			set
			{
				_requestFormat = value;
			}
		}

		/// <summary>
		/// Used by the default deserializers to determine where to start deserializing from.
		/// Can be used to skip container or root elements that do not have corresponding deserialzation targets.
		/// </summary>
		public string RootElement { get; set; }

		/// <summary>
		/// Used by the default deserializers to explicitly set which date format string to use when parsing dates.
		/// </summary>
		public string DateFormat { get; set; }

		/// <summary>
		/// Used by XmlDeserializer. If not specified, XmlDeserializer will flatten response by removing namespaces from element names.
		/// </summary>
		public string XmlNamespace { get; set; }

		/// <summary>
		/// Gets or sets a user-defined state object that contains information about a request and which can be later 
		/// retrieved when the request completes.
		/// </summary>
		public object UserState { get; set; }

		/// <summary>
		/// Timeout in milliseconds to be used for the request. This timeout value overrides a timeout set on the RestClient.
		/// </summary>
		public int Timeout { get; set; }

		private int _attempts;

		/// <summary>
		/// Internal Method so that RestClient can increase the number of attempts
		/// </summary>
		public void IncreaseNumAttempts()
		{
			_attempts++;
		}

		/// <summary>
		/// How many attempts were made to send this Request?
		/// </summary>
		/// <remarks>
		/// This Number is incremented each time the RestClient sends the request.
		/// Useful when using Asynchronous Execution with Callbacks
		/// </remarks>
		public int Attempts
		{
			get { return _attempts; }
		}

        public CacheMode CacheMode { get; set; }
        public TimeSpan? CacheExpiry { get; set; }
	}
}
