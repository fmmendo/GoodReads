using Mendo.UAP.Common;
using MyShelf.API.Storage;
using MyShelf.API.Web;
using MyShelf.API.XML;
using MyShelf.API.XML.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public class BookService : Singleton<BookService>, IBookService
    {
        private Dictionary<string, DateTime> timestamp_shelfbooks = new Dictionary<string, DateTime>();
        private Dictionary<string, string> response_shelfbooks = new Dictionary<string, string>();

        /// <summary>
        /// Returns the books for the logged in user
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Reviews> GetBooks(string shelf = null, string sort = null, string query = null, string order = null, string page = null, string per_page = "200")
        {

            if (!timestamp_shelfbooks.ContainsKey(shelf) || timestamp_shelfbooks[shelf].AddMinutes(15) <= DateTime.Now)
            {
                string url = Urls.ShelfBooks + MyShelfSettings.Instance.GoodreadsUserID + ".xml?key=" + MyShelfSettings.Instance.ConsumerKey + "&format=xml&v=2";

                if (!String.IsNullOrEmpty(shelf))
                    url += "&shelf=" + shelf;
                if (!String.IsNullOrEmpty(per_page))
                    url += "&per_page=" + per_page;

                var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync(url, RestSharp.Method.GET, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret);

                response_shelfbooks[shelf] = response.Content.ToString();
                timestamp_shelfbooks[shelf] = DateTime.Now;
            }


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
            //string url = Urls.ShelfBooks + MyShelfSettings.Instance.GoodreadsUserID + ".xml?key=" + MyShelfSettings.Instance.ConsumerKey + "&format=xml&v=2";

            //TODO: more params, probably enums
            //if (!String.IsNullOrEmpty(shelf))
            //    url += "&shelf=" + shelf;
            //if (!String.IsNullOrEmpty(per_page))
            //    url += "&per_page=" + per_page;

            //var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync(url, RestSharp.Method.GET, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret);

            //await apiSemaphore.WaitAsync();
            //var request = new RestRequest(url, Method.GET);
            //var response = await client.ExecuteAsync(request);

            //ApiCooldown();

            var result = GoodReadsSerializer.DeserializeResponse(response_shelfbooks[shelf]);
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
            string results = await ApiClient.Instance.HttpGet(String.Format(Urls.BookShow, id, MyShelfSettings.Instance.ConsumerKey));

            var result = GoodReadsSerializer.DeserializeResponse(results);

            return result.Book;
        }

        /// <summary>
        /// Performs a GoodReads search for the given query
        /// </summary>
        /// <param name="query">string to search for</param>
        /// <returns>List of Work items</returns>
        public async Task<Search> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            string results = await ApiClient.Instance.HttpGet(String.Format(Urls.Search, MyShelfSettings.Instance.ConsumerKey, query.Replace(" ", "+")));

            var result = GoodReadsSerializer.DeserializeResponse(results);

            return result.Search;
        }

        public async Task<string> CreateReview(string bookId, string body, string rating, string readAt, string shelf)
        {
            var param = new Dictionary<string, object>();
            if (!String.IsNullOrEmpty(bookId)) param.Add("book_id", bookId);
            if (!String.IsNullOrEmpty(body)) param.Add("review[review]", body);
            if (!String.IsNullOrEmpty(rating)) param.Add("review[rating]", rating);
            if (!String.IsNullOrEmpty(readAt)) param.Add("review[read_at]", readAt);
            if (!String.IsNullOrEmpty(shelf)) param.Add("shelf", shelf);

            var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync("review.xml ", Method.POST, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret, param);

            //TODO: This is a quick workaround
            if (response.StatusCode == 201 && response.StatusDescription == "Created" && response.ResponseStatus == ResponseStatus.Completed)
            {
                var result = response.Content.ToString();
                var start = result.IndexOf("<id type=\"integer\">") + 19;
                var end = result.IndexOf("</id>", start);
                string id = result.Substring(start, end - start);
                //var status = DeserializeResponse<UserStatus>(response.Content.ToString());

                //return status;


                return id;
            }
            else return String.Empty;
            //else
            //    return false;
        }

        public async Task<bool> EditReview(string reviewId, string finished, string body, string rating, string readAt, string shelf)
        {

            var param = new Dictionary<string, object>();
            if (!String.IsNullOrEmpty(reviewId)) param.Add("id", reviewId);
            if (!String.IsNullOrEmpty(body)) param.Add("review[review]", body);
            if (!String.IsNullOrEmpty(rating)) param.Add("review[rating]", rating);
            if (!String.IsNullOrEmpty(readAt)) param.Add("review[read_at]", readAt);
            if (!String.IsNullOrEmpty(finished)) param.Add("finished", finished);
            if (!String.IsNullOrEmpty(shelf)) param.Add("shelf", shelf);

            var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync($"review/{reviewId}.xml ", Method.POST, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret, param);


            return (response.StatusCode == 200 && response.StatusDescription == "OK" && response.ResponseStatus == ResponseStatus.Completed);
        }

        public async Task<Review> GetUserReview(string bookId, string userId = null)
        {
            if (string.IsNullOrEmpty(userId))
                userId = MyShelfSettings.Instance.GoodreadsUserID;
            
            string results = await ApiClient.Instance.HttpGet(String.Format(Urls.UserReview, bookId, MyShelfSettings.Instance.ConsumerKey, userId));

            var result = GoodReadsSerializer.DeserializeResponse(results);

            return result.Review;
        }
    }
}
