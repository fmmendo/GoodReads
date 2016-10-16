using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections;
using Mendo.UWP.Network;

namespace RestSharp
{
	public partial class RestClient
	{
		/// <summary>
		/// Executes the specified request and downloads the response data
		/// </summary>
		/// <param name="request">Request to execute</param>
		/// <returns>Response data</returns>
        public IAsyncOperation<IEnumerable> DownloadDataAsync(IRestRequest request)
        {
            return (IAsyncOperation<IEnumerable>)AsyncInfo.Run((System.Threading.CancellationToken ct) => DownloadDataInternal(request));
        }

		private async Task<byte[]> DownloadDataInternal(IRestRequest request)
		{
			var response = await ExecuteAsync(request);
			return response.RawBytes;
		}

		/// <summary>
		/// Executes the request and returns a response, authenticating if needed
		/// </summary>
		/// <param name="request">Request to be executed</param>
		/// <returns>RestResponse</returns>
        public IAsyncOperation<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            return (IAsyncOperation<IRestResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => ExecuteAsyncInternal(request));
        }

        private async Task<IRestResponse> ExecuteAsyncInternal(IRestRequest request)
		{
			var method = Enum.GetName(typeof(Method), request.Method);
			switch (request.Method)
			{
                case Method.POST:
                case Method.PUT:
                case Method.PATCH:
                    return await ExecuteInternal(request, method, DoExecuteAsPostAsync);
				default:
					return await ExecuteInternal(request, method, DoExecuteAsGetAsync);
			}
		}

        public IAsyncOperation<IRestResponse> ExecuteAsGetAsync(IRestRequest request, string httpMethod)
        {
            return (IAsyncOperation<IRestResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => ExecuteAsGetAsyncInternal(request, httpMethod));
        }
		private async Task<IRestResponse> ExecuteAsGetAsyncInternal(IRestRequest request, string httpMethod)
		{
			return await ExecuteInternal(request, httpMethod, DoExecuteAsGetAsync);
		}

        public IAsyncOperation<IRestResponse> ExecuteAsPostAsync(IRestRequest request, string httpMethod)
        {
            return (IAsyncOperation<IRestResponse>)AsyncInfo.Run((System.Threading.CancellationToken ct) => ExecuteAsPostAsyncInternal(request, httpMethod));
        }
        private async Task<IRestResponse> ExecuteAsPostAsyncInternal(IRestRequest request, string httpMethod)
        {
            request.Method = Method.POST; // Required by RestClient.BuildUri... 
            return await ExecuteInternal(request, httpMethod, DoExecuteAsPostAsync);
        }

		private async Task<IRestResponse> ExecuteInternal(IRestRequest request, string httpMethod, Func<IHttp, string, CacheMode, TimeSpan?, Task<HttpResponse>> getResponse)
		{
			AuthenticateIfNeeded(this, request);

            // add Accept header based on registered deserializers
			this.AddDefaultParameter("Accept", "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml", ParameterType.HttpHeader);

			IRestResponse response = new RestResponse();
			try
			{
				var http = HttpFactory.Create();

				ConfigureHttp(request, http);

				response = ConvertToRestResponse(request, await getResponse(http, httpMethod, request.CacheMode, request.CacheExpiry));
				response.Request = request;
				response.Request.IncreaseNumAttempts();

			}
			catch (Exception ex)
			{
				response.ResponseStatus = ResponseStatus.Error;
				response.ErrorMessage = ex.Message;
				response.ErrorException = ex;
			}

			return response;
		}

        
		private async static Task<HttpResponse> DoExecuteAsGetAsync(IHttp http, string method, CacheMode cacheMode = CacheMode.Skip, TimeSpan? cacheExpiry = null)
		{
			return await http.AsGetAsync(method, cacheMode, cacheExpiry);
		}

        private async static Task<HttpResponse> DoExecuteAsPostAsync(IHttp http, string method, CacheMode cacheMode = CacheMode.Skip, TimeSpan? cacheExpiry = null)
        {
            return await http.AsPostAsync(method);
        }
	}
}