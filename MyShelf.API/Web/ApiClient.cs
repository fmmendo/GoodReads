using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyShelf.API.Web
{
    public class ApiClient : IApiClient
    {
        private readonly SemaphoreSlim _apiSemaphore = new SemaphoreSlim(1, 1);
        private const int COOLDOWN_MILLISECONDS = 1000;

        private RestClient _client;

        public async Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            await _apiSemaphore.WaitAsync();

            var requestResponse = await _client.ExecuteAsync(request);

            ApiCooldown();

            return requestResponse;
        }

        /// <summary>
        /// Adds a 1second delay before releasing the api call semaphore
        /// </summary>
        /// <returns></returns>
        private async Task ApiCooldown()
        {
            await Task.Delay(COOLDOWN_MILLISECONDS);
            _apiSemaphore.Release();
        }
    }
}
