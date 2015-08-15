using Mendo.UAP.Common;
using MyShelf.API.Storage;
using MyShelf.API.Web;
using MyShelf.API.XML;
using MyShelf.API.XML.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public class BookService : Singleton<BookService>, IBookService
    {

        /// <summary>
        /// Returns the books for the logged in user
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Reviews> GetBooks(string shelf = null, string sort = null, string query = null, string order = null, string page = null, string per_page = "200")
        {
            //if (shelf == null && justRefreshedReviews)
            //{
            //    Task.Run(async () =>
            //    {
            //        client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);
            //        string url = "review/list/" + UserSettings.Settings.GoodreadsUserID + ".xml?key=" + API_KEY + "&format=xml&v=2";

            //        //TODO: more params, probably enums
            //        if (!String.IsNullOrEmpty(shelf))
            //            url += "&shelf=" + shelf;
            //        if (!String.IsNullOrEmpty(per_page))
            //            url += "&per_page=" + per_page;

            //        await apiSemaphore.WaitAsync();
            //        var request = new RestRequest(url, Method.GET);
            //        var response = await client.ExecuteAsync(request);

            //        ApiCooldown();

            //        var result = DeserializeResponse(response.Content.ToString());
            //        GoodreadsReviews = result.Reviews;
            //    });
            //}
            //else
            //{
                //client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);
                string url = Urls.ShelfBooks + Settings.Instance.GoodreadsUserID + ".xml?key=" + Settings.Instance.ConsumerKey + "&format=xml&v=2";

                //TODO: more params, probably enums
                if (!String.IsNullOrEmpty(shelf))
                    url += "&shelf=" + shelf;
                if (!String.IsNullOrEmpty(per_page))
                    url += "&per_page=" + per_page;

                var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync(url, RestSharp.Method.GET, Settings.Instance.ConsumerKey, Settings.Instance.ConsumerSecret, Settings.Instance.OAuthAccessToken, Settings.Instance.OAuthAccessTokenSecret);

                //await apiSemaphore.WaitAsync();
                //var request = new RestRequest(url, Method.GET);
                //var response = await client.ExecuteAsync(request);

                //ApiCooldown();

                var result = GoodReadsSerializer.DeserializeResponse(response.Content.ToString());
                //GoodreadsReviews = result.Reviews;
            //}

            return result.Reviews;
        }

        /// <summary>
        /// Returns more complete book data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Book> GetBookInfo(string id)
        {
            string results = await ApiClient.Instance.HttpGet(String.Format(Urls.BookShow, id, Settings.Instance.ConsumerKey));

            var result = GoodReadsSerializer.DeserializeResponse(results);

            return result.Book;
        }
    }
}
