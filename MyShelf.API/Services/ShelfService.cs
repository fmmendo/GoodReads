using Mendo.UWP.Common;
using MyShelf.API.Storage;
using MyShelf.API.Web;
using MyShelf.API.XML;
using MyShelf.API.XML.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public class ShelfService : Singleton<ShelfService>, IShelfService
    {
        private SemaphoreSlim shelfSemaphore = new SemaphoreSlim(1, 1);
        public List<UserShelf> UserShelves { get; } = new List<UserShelf>();

        private DateTime timestamp_shelves;
        private string response_shelves;

        /// <summary>
        /// Returns the shelves for the logged in user
        /// </summary>
        /// <param name="query">string to search for</param>
        /// <returns>List of Work items</returns>
        public async Task<List<UserShelf>> GetShelvesList()
        {
            if (timestamp_shelves.AddMinutes(15) <= DateTime.Now)
            {
                await shelfSemaphore.WaitAsync();
                try
                {
                    response_shelves = await ApiClient.Instance.HttpGet(String.Format(Urls.ShelvesList, MyShelfSettings.Instance.ConsumerKey));
                }
                finally
                {
                    shelfSemaphore.Release();
                    timestamp_shelves = DateTime.Now;
                }
            }

            var result = GoodReadsSerializer.DeserializeResponse(response_shelves);

            return result.Shelves.UserShelf;
        }

        /// <summary>
        /// Adds a book to the given shelf
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<bool> AddBookToShelf(string shelfName, string bookId, bool remove = false)
        {
            //client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            //await apiSemaphore.WaitAsync();

            //var request = new RestRequest("shelf/add_to_shelf.xml", Method.POST);
            //request.RequestFormat = DataFormat.Xml;
            var param = new Dictionary<string, object>();
            param.Add("name", shelfName);
            param.Add("book_id", int.Parse(bookId));
            param.Add("a", remove ? "remove" : String.Empty);

            //var response = await client.ExecuteAsync(request);
            var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync("shelf/add_to_shelf.xml", RestSharp.Method.POST, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret, param);

            //ApiCooldown();

            if (response.StatusCode == 201)
                return true;
            else
                return false;
        }
    }
}
