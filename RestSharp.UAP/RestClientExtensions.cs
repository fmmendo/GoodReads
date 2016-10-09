using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;

namespace RestSharp
{
	public static partial class RestClientExtensions
	{
        public static IAsyncOperation<IRestResponse> GetAsync(this IRestClient client, IRestRequest request)
        {
            return (IAsyncOperation<IRestResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => GetAsyncInternal(client, request));
        }
        private async static Task<IRestResponse> GetAsyncInternal(this IRestClient client, IRestRequest request)
        {
            request.Method = Method.GET;
            return await client.ExecuteAsync(request);
        }

        public static IAsyncOperation<IRestResponse> PostAsync(this IRestClient client, IRestRequest request)
        {
            return (IAsyncOperation<IRestResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => PostAsyncInternal(client, request));
        }
        private async static Task<IRestResponse> PostAsyncInternal(this IRestClient client, IRestRequest request)
        {
            request.Method = Method.POST;
            return await client.ExecuteAsync(request);
        }

        public static IAsyncOperation<IRestResponse> PutAsync(this IRestClient client, IRestRequest request)
        {
            return (IAsyncOperation<IRestResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => PutAsyncInternal(client, request));
        }
        private async static Task<IRestResponse> PutAsyncInternal(this IRestClient client, IRestRequest request)
        {
            request.Method = Method.PUT;
            return await client.ExecuteAsync(request);
        }

        public static IAsyncOperation<IRestResponse> HeadAsync(this IRestClient client, IRestRequest request)
        {
            return (IAsyncOperation<IRestResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => HeadAsyncInternal(client, request));
        }
        private async static Task<IRestResponse> HeadAsyncInternal(this IRestClient client, IRestRequest request)
        {
            request.Method = Method.HEAD;
            return await client.ExecuteAsync(request);
        }

        public static IAsyncOperation<IRestResponse> OptionsAsync(this IRestClient client, IRestRequest request)
        {
            return (IAsyncOperation<IRestResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => OptionsAsyncInternal(client, request));
        }
        private async static Task<IRestResponse> OptionsAsyncInternal(this IRestClient client, IRestRequest request)
        {
            request.Method = Method.OPTIONS;
            return await client.ExecuteAsync(request);
        }

        public static IAsyncOperation<IRestResponse> PatchAsync(this IRestClient client, IRestRequest request)
        {
            return (IAsyncOperation<IRestResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => PatchAsyncInternal(client, request));
        }
        private async static Task<IRestResponse> PatchAsyncInternal(this IRestClient client, IRestRequest request)
        {
            request.Method = Method.PATCH;
            return await client.ExecuteAsync(request);
        }

        public static IAsyncOperation<IRestResponse> DeleteAsync(this IRestClient client, IRestRequest request)
        {
            return (IAsyncOperation<IRestResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => DeleteAsyncInternal(client, request));
        }
        private async static Task<IRestResponse> DeleteAsyncInternal(this IRestClient client, IRestRequest request)
        {
            request.Method = Method.DELETE;
            return await client.ExecuteAsync(request);
        }

		/// <summary>
		/// Add a parameter to use on every request made with this client instance
		/// </summary>
		/// <param name="restClient">The IRestClient instance</param>
		/// <param name="p">Parameter to add</param>
		/// <returns></returns>
		public static void AddDefaultParameter(this IRestClient restClient, Parameter p)
		{
			if (p.Type == ParameterType.RequestBody)
			{
				throw new NotSupportedException(
					"Cannot set request body from default headers. Use Request.AddBody() instead.");
			}

			restClient.DefaultParameters.Add(p);
		}

		/// <summary>
		/// Adds a HTTP parameter (QueryString for GET, DELETE, OPTIONS and HEAD; Encoded form for POST and PUT)
		/// Used on every request made by this client instance
		/// </summary>
		/// <param name="restClient">The IRestClient instance</param>
		/// <param name="name">Name of the parameter</param>
		/// <param name="value">Value of the parameter</param>
		/// <returns>This request</returns>
		public static void AddDefaultParameter(this IRestClient restClient, string name, object value)
		{
			restClient.AddDefaultParameter(new Parameter { Name = name, Value = value, Type = ParameterType.GetOrPost });
		}

		/// <summary>
		/// Adds a parameter to the request. There are four types of parameters:
		///	- GetOrPost: Either a QueryString value or encoded form value based on method
		///	- HttpHeader: Adds the name/value pair to the HTTP request's Headers collection
		///	- UrlSegment: Inserted into URL if there is a matching url token e.g. {AccountId}
		///	- RequestBody: Used by AddBody() (not recommended to use directly)
		/// </summary>
		/// <param name="restClient">The IRestClient instance</param>
		/// <param name="name">Name of the parameter</param>
		/// <param name="value">Value of the parameter</param>
		/// <param name="type">The type of parameter to add</param>
		/// <returns>This request</returns>
		public static void AddDefaultParameter(this IRestClient restClient, string name, object value, ParameterType type)
		{
			restClient.AddDefaultParameter(new Parameter { Name = name, Value = value, Type = type });
		}

		/// <summary>
		/// Shortcut to AddDefaultParameter(name, value, HttpHeader) overload
		/// </summary>
		/// <param name="restClient">The IRestClient instance</param>
		/// <param name="name">Name of the header to add</param>
		/// <param name="value">Value of the header to add</param>
		/// <returns></returns>
		public static void AddDefaultHeader(this IRestClient restClient, string name, string value)
		{
			restClient.AddDefaultParameter(name, value, ParameterType.HttpHeader);
		}

		/// <summary>
		/// Shortcut to AddDefaultParameter(name, value, UrlSegment) overload
		/// </summary>
		/// <param name="restClient">The IRestClient instance</param>
		/// <param name="name">Name of the segment to add</param>
		/// <param name="value">Value of the segment to add</param>
		/// <returns></returns>
		public static void AddDefaultUrlSegment(this IRestClient restClient, string name, string value)
		{
			restClient.AddDefaultParameter(name, value, ParameterType.UrlSegment);
		}

	}
}