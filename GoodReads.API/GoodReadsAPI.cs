using GoodReads.API.Model;
using GoodReads.API.Utilities;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Contrib;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Xml.Serialization;
using Windows.Security.Authentication.Web;
using Windows.Storage;

namespace GoodReads.API
{
    public class GoodReadsAPI
    {
        //private const int COOLDOWN_MILLISECONDS = 1000;

        //private RestClient client;
        //private IGoodReadsAuthenticator authenticator;
        //private readonly SemaphoreSlim apiSemaphore = new SemaphoreSlim(1, 1);
        //private User authenticatedUser;
        //private bool justRefreshedUser = false;

        //public List<UserShelf> GoodreadsUserShelves { get; set; }
        //private bool justRefreshedShelves = false;

        //public Reviews GoodreadsReviews { get; set; }
        //public bool justRefreshedReviews = false;

        //public GoodReadsAPI(IGoodReadsAuthenticator authenticator)
        //{
        //    this.authenticator = authenticator;

        //    authenticator.AuthenticationCompleted += authenticator_AuthenticationCompleted;
        //    client = new RestClient(Urls.BaseUrl);
        //}

        //async void authenticator_AuthenticationCompleted(object sender, EventArgs e)
        //{
        //    await CompleteAuthentication();
        //}

        #region API Calls

        ///// <summary>
        ///// Authenticates the user using the Web Authentication Broker
        ///// </summary>
        //public async Task<bool> Authenticate()
        //{
        //    // If we have an session key already no need to do anything
        //    if (UserSettings.Settings.IsUserAuthenticated)
        //        return true;

        //    // set up get request tokens
        //    client.Authenticator = OAuth1Authenticator.ForRequestToken(API_KEY, OAUTH_SECRET);

        //    // Request token
        //    await apiSemaphore.WaitAsync();
        //    var request = new RestRequest("/oauth/request_token", Method.GET);
        //    var requestResponse = await client.ExecuteAsync(request);
        //    ApiCooldown();

        //    // Parse oauth token and token secret
        //    var querystring = HttpUtility.ParseQueryString(requestResponse.Content);
        //    if (querystring != null && querystring.Count == 2)
        //    {
        //        UserSettings.Settings.OAuthToken = querystring["oauth_token"];
        //        UserSettings.Settings.OAuthTokenSecret = querystring["oauth_token_secret"];
        //    }
        //    else return false;

        //    // authenticate
        //    string goodreadsURL = "https://www.goodreads.com/oauth/authorize?oauth_token=" + UserSettings.Settings.OAuthToken;
        //    WebAuthenticationResult result = await authenticator.Authenticate(WebAuthenticationOptions.None, new Uri(goodreadsURL), WebAuthenticationBroker.GetCurrentApplicationCallbackUri());

        //    // success
        //    if (result != null && result.ResponseStatus == WebAuthenticationStatus.Success)
        //    {
        //        return await CompleteAuthentication();
        //    }
        //    return false;
        //}

        //public async Task<bool> CompleteAuthentication()
        //{
        //    // set up get 
        //    client.Authenticator = OAuth1Authenticator.ForAccessToken(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthToken, UserSettings.Settings.OAuthTokenSecret);

        //    //request access token
        //    await apiSemaphore.WaitAsync();
        //    var request = new RestRequest("oauth/access_token", Method.GET);
        //    var accessResponse = await client.ExecuteAsync(request);
        //    ApiCooldown();

        //    // parse oauth access token and token secrets
        //    var querystring = HttpUtility.ParseQueryString(accessResponse.Content);
        //    if (querystring != null && querystring.Count == 2)
        //    {
        //        UserSettings.Settings.OAuthAccessToken = querystring["oauth_token"];
        //        UserSettings.Settings.OAuthAccessTokenSecret = querystring["oauth_token_secret"];
        //    }
        //    else return false;

        //    // if we don't have a user ID yet, go fetch it
        //    if (String.IsNullOrEmpty(UserSettings.Settings.GoodreadsUserID))
        //    {
        //        var user = await GetUserID();

        //        UserSettings.Settings.GoodreadsUserID = user.Id;
        //        UserSettings.Settings.GoodreadsUserLink = user.Link;
        //        UserSettings.Settings.GoodreadsUsername = user.Name;
        //    }

        //    authenticatedUser = await GetUserInfo(UserSettings.Settings.GoodreadsUserID);
        //    UserSettings.Settings.GoodreadsUserImageUrl = authenticatedUser.Image_url;
        //    UserSettings.Settings.GoodreadsUserSmallImageUrl = authenticatedUser.Small_image_url;
        //    justRefreshedUser = true;

        //    GoodreadsUserShelves = await GetShelvesList();
        //    justRefreshedShelves = true;

        //    GoodreadsReviews = await GetShelfBooks();
        //    justRefreshedReviews = true;

        //    return true;
        //}

        ///// <summary>
        ///// Returns the logged in User object
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="filter"></param>
        ///// <param name="maxUpdates"></param>
        //public async Task<User> GetUserID()
        //{
        //    client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

        //    await apiSemaphore.WaitAsync();

        //    var request = new RestRequest("api/auth_user", Method.GET);
        //    var response = await client.ExecuteAsync(request);

        //    ApiCooldown();

        //    var result = DeserializeResponse(response.Content.ToString());

        //    return result.User;
        //}

        /// <summary>
        /// Returns a users notifications
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Notifications> GetNotifications(int page = 1)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("notifications.xml", Method.GET);
            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            var result = DeserializeResponse(response.Content.ToString());

            return result.Notifications;
        }

        ///// <summary>
        ///// Returns info for the given user
        ///// </summary>
        ///// <param name="userID"></param>
        ///// <returns></returns>
        //public async Task<User> GetUserInfo(string userID = null)
        //{
        //    if (String.IsNullOrEmpty(userID))
        //        userID = UserSettings.Settings.GoodreadsUserID;

        //    if (userID == UserSettings.Settings.GoodreadsUserID && justRefreshedUser)
        //        return authenticatedUser;

        //    await apiSemaphore.WaitAsync();
        //    string results = await HttpGet(String.Format(Urls.UserShow, userID, API_KEY));
        //    ApiCooldown();

        //    var result = DeserializeResponse(results);

        //    return result.User;
        //}

        /// <summary>
        /// Performs a GoodReads search for the given query
        /// </summary>
        /// <param name="query">string to search for</param>
        /// <returns>List of Work items</returns>
        public async Task<Search> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
                return null;

            if (apiSemaphore.CurrentCount > 0)
            {
                if (await apiSemaphore.WaitAsync(250))
                {
                    string results = await HttpGet(String.Format(Urls.Search, API_KEY, query.Replace(" ", "+")));
                    ApiCooldown();
                    var result = DeserializeResponse(results);

                    return result.Search;
                }
            }
            return null;
        }

        ///// <summary>
        ///// Returns the shelves for the logged in user
        ///// </summary>
        ///// <param name="query">string to search for</param>
        ///// <returns>List of Work items</returns>
        //public async Task<List<UserShelf>> GetShelvesList()
        //{
        //    if (justRefreshedShelves)
        //    {
        //        // update it in the background
        //        Task.Run(async () =>
        //        {
        //            await apiSemaphore.WaitAsync();
        //            string results = await HttpGet("https://www.goodreads.com/shelf/list.xml?key=" + API_KEY);
        //            ApiCooldown();

        //            var result = DeserializeResponse(results);

        //            GoodreadsUserShelves = result.Shelves.UserShelf;
        //        });
        //    }
        //    else
        //    {
        //        await apiSemaphore.WaitAsync();
        //        string results = await HttpGet("https://www.goodreads.com/shelf/list.xml?key=" + API_KEY);
        //        ApiCooldown();

        //        var result = DeserializeResponse(results);

        //        GoodreadsUserShelves = result.Shelves.UserShelf;
        //    }
        //    return GoodreadsUserShelves;
        //}

        ///// <summary>
        ///// Returns the friend update feed for the logged in user
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="filter"></param>
        ///// <param name="maxUpdates"></param>
        //public async Task<Updates> GetFriendUpdates(string type, string filter, string maxUpdates)
        //{

        //    string url = Urls.UpdatesFriends;// BASEURL + UPDATES + FRIENDS;// +type + filter + "&max_updates=" + maxUpdates + "&access_token=" + OAuthAccessToken;

        //    client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

        //    await apiSemaphore.WaitAsync();
        //    var request = new RestRequest("updates/friends.xml", Method.GET);
        //    var response = await client.ExecuteAsync(request);
        //    ApiCooldown();

        //    GoodreadsResponse result = DeserializeResponse(response.Content.ToString());

        //    return result.Updates;
        //}

        ///// <summary>
        ///// Returns the books for the logged in user
        ///// </summary>
        ///// <param name="type"></param>
        ///// <param name="filter"></param>
        ///// <param name="maxUpdates"></param>
        //public async Task<Reviews> GetShelfBooks(string shelf = null, string sort = null, string query = null, string order = null, string page = null, string per_page = "200")
        //{
        //    if (shelf == null && justRefreshedReviews)
        //    {
        //        Task.Run(async () =>
        //        {
        //            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);
        //            string url = "review/list/" + UserSettings.Settings.GoodreadsUserID + ".xml?key=" + API_KEY + "&format=xml&v=2";

        //            //TODO: more params, probably enums
        //            if (!String.IsNullOrEmpty(shelf))
        //                url += "&shelf=" + shelf;
        //            if (!String.IsNullOrEmpty(per_page))
        //                url += "&per_page=" + per_page;

        //            await apiSemaphore.WaitAsync();
        //            var request = new RestRequest(url, Method.GET);
        //            var response = await client.ExecuteAsync(request);

        //            ApiCooldown();

        //            var result = DeserializeResponse(response.Content.ToString());
        //            GoodreadsReviews = result.Reviews;
        //        });
        //    }
        //    else
        //    { 

        //    client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);
        //    string url = "review/list/" + UserSettings.Settings.GoodreadsUserID + ".xml?key=" + API_KEY + "&format=xml&v=2";

        //    //TODO: more params, probably enums
        //    if (!String.IsNullOrEmpty(shelf))
        //        url += "&shelf=" + shelf;
        //    if (!String.IsNullOrEmpty(per_page))
        //        url += "&per_page=" + per_page;

        //    await apiSemaphore.WaitAsync();
        //    var request = new RestRequest(url, Method.GET);
        //    var response = await client.ExecuteAsync(request);

        //    ApiCooldown();

        //    var result = DeserializeResponse(response.Content.ToString());
        //    GoodreadsReviews = result.Reviews;
        //    }

        //    return GoodreadsReviews;
        //}

        /// <summary>
        /// Returns more complete book data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Book> GetBookInfo(string id)
        {
            await apiSemaphore.WaitAsync();
            string results = await HttpGet(String.Format(Urls.BookShow, id, API_KEY));
            ApiCooldown();

            var result = DeserializeResponse(results);

            return result.Book;
        }

        ///// <summary>
        ///// Returns more complete author data
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<Author> GetAuthorInfo(string id)
        //{
        //    await apiSemaphore.WaitAsync();
        //    string results = await HttpGet(String.Format(Urls.AuthorShow, id, API_KEY));
        //    ApiCooldown();

        //    var result = DeserializeResponse(results);

        //    return result.Author;
        //}

        ///// <summary>
        ///// Returns an author's books
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="page"></param>
        ///// <returns></returns>
        //public async Task<Books> GetAuthorBooks(string id/*, int page = 1*/)
        //{
        //    await apiSemaphore.WaitAsync();
        //    string results = await HttpGet(String.Format(Urls.AuthorBooks, id, API_KEY/*, page.ToString()*/));
        //    ApiCooldown();

        //    var result = DeserializeResponse(results);

        //    return result.Author.Books;
        //}

        /// <summary>
        /// Returns infor for the given user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<UserStatus> GetReadStatus(string userID = null)
        {
            if (String.IsNullOrEmpty(userID))
                userID = UserSettings.Settings.GoodreadsUserID;

            await apiSemaphore.WaitAsync();
            string results = await HttpGet(String.Format(Urls.ReadStatus, userID, API_KEY));
            ApiCooldown();

            var result = DeserializeResponse(results);

            return result.UserStatus;
        }

        /// <summary>
        /// Returns infor for the given user
        /// </summary>
        /// <param name="statusID"></param>
        /// <returns></returns>
        public async Task<UserStatus> GetStatusUpdate(string statusID = null)
        {
            if (String.IsNullOrEmpty(statusID))
                return null;

            await apiSemaphore.WaitAsync();
            string results = await HttpGet(String.Format(Urls.StatusShow, statusID, API_KEY));
            ApiCooldown();

            var result = DeserializeResponse(results);

            return result.UserStatus;
        }

        /// <summary>
        /// Posts a new user status
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<String> PostStatusUpdate(string bookId, string page, string percent, string body)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("user_status.xml", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            if (!String.IsNullOrEmpty(bookId))
                request.AddParameter("user_status[book_id]", bookId); ;
            if (!String.IsNullOrEmpty(page))
                request.AddParameter("user_status[page]", page);
            if (!String.IsNullOrEmpty(percent))
                request.AddParameter("user_status[percent]", percent);
            if (!String.IsNullOrEmpty(bookId))
                request.AddParameter("user_status[body]", body);

            var response = await client.ExecuteAsync(request);

            ApiCooldown();

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

        /// <summary>
        /// Deletes a new user status
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Boolean> DeleteStatusUpdate(string id)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();
            var request = new RestRequest("user_status/destroy/" + id + ".xml", Method.POST);
            request.RequestFormat = DataFormat.Xml;

            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            if (response.StatusCode == 200 && response.StatusDescription == "OK" && response.ResponseStatus == ResponseStatus.Completed)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Show association between the user and the given author
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Boolean> ShowFanship(string fanshipId)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("fanships/show/" + fanshipId + ".xml", Method.GET);
            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            //TODO: This is a quick workaround
            if (response.StatusCode == 201 && response.StatusDescription == "Created" && response.ResponseStatus == ResponseStatus.Completed)
            {
                return true;
                //var result = response.Content.ToString();
                //var start = result.IndexOf("<id type=\"integer\">") + 19;
                //var end = result.IndexOf("</id>", start);
                //string id = result.Substring(start, end - start);
                //var status = DeserializeResponse<UserStatus>(response.Content.ToString());
                //return status;
                //return id;
            }
            else return false;
            // String.Empty;
            //else
            //    return false;
        }

        /// <summary>
        /// Creates a fanship between the user and the given author
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Boolean> CreateFanship(string authorId)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("fanships.xml", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddParameter("fanship[author_id]", authorId);

            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            //TODO: This is a quick workaround
            if (response.StatusCode == 201 && response.StatusDescription == "Created" && response.ResponseStatus == ResponseStatus.Completed)
            {
                return true;
                //var result = response.Content.ToString();
                //var start = result.IndexOf("<id type=\"integer\">") + 19;
                //var end = result.IndexOf("</id>", start);
                //string id = result.Substring(start, end - start);
                //var status = DeserializeResponse<UserStatus>(response.Content.ToString());
                //return status;
                //return id;
            }
            else return false;
            // String.Empty;
            //else
            //    return false;
        }

        /// <summary>
        /// Deletes the fanship between the user and the author
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Boolean> DeleteFanship(string fanshipId)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();
            var request = new RestRequest("fanships/" + fanshipId + ".xml", Method.DELETE);
            request.RequestFormat = DataFormat.Xml;

            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            if (response.StatusCode == 200 && response.StatusDescription == "OK" && response.ResponseStatus == ResponseStatus.Completed)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Start following the given user
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Boolean> FollowUser(string userId)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("user/" + userId + "/followers.xml", Method.POST);
            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            //TODO: This is a quick workaround
            if (response.StatusCode == 201 && response.StatusDescription == "Created" && response.ResponseStatus == ResponseStatus.Completed)
            {
                return true;
                //var result = response.Content.ToString();
                //var start = result.IndexOf("<id type=\"integer\">") + 19;
                //var end = result.IndexOf("</id>", start);
                //string id = result.Substring(start, end - start);
                //var status = DeserializeResponse<UserStatus>(response.Content.ToString());
                //return status;
                //return id;
            }
            else return false;
            // String.Empty;
            //else
            //    return false;
        }

        /// <summary>
        /// Stop following the given user
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Boolean> UnfollowUser(string userId)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("user/" + userId + "/followers/stop_following.xml ", Method.DELETE);
            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            if (response.StatusCode == 200 && response.StatusDescription == "OK" && response.ResponseStatus == ResponseStatus.Completed)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Likes a resource
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<String> AddComment(string id, string type, string comment)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("comment.xml", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddParameter("type", type);
            request.AddParameter("id", id);
            request.AddParameter("comment[body]", comment);

            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            //TODO: This is a quick workaround
            if (response.StatusCode == 201 && response.StatusDescription == "Created" && response.ResponseStatus == ResponseStatus.Completed)
            {
                var result = response.Content.ToString();
                var start = result.IndexOf("<id type=\"integer\">") + 19;
                var end = result.IndexOf("</id>", start);
                string commentId = result.Substring(start, end - start);
                //var status = DeserializeResponse<UserStatus>(response.Content.ToString());

                //return status;


                return commentId;
            }
            else return String.Empty;
            //else
            //    return false;
        }

        /// <summary>
        /// Likes a resource
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<bool> LikeResource(string resourceId, string resourceType)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("rating", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddParameter("rating[rating]", 1);
            request.AddParameter("rating[resource_id]", resourceId);
            request.AddParameter("rating[resource_type]", resourceType);

            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            if (response.StatusCode == 200 && response.ResponseStatus == ResponseStatus.Completed)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Unlikes a resource
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<String> UnlikeResource(string resourceId, string resourceType)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("rating", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddParameter("rating[rating]", 1);
            request.AddParameter("rating[resource_id]", resourceId);
            request.AddParameter("rating[resource_type]", resourceType);

            var response = await client.ExecuteAsync(request);

            ApiCooldown();

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


        /// <summary>
        /// Adds a book to the given shelf
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<bool> AddBookToShelf(string shelfName, string bookId, bool remove = false)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("shelf/add_to_shelf.xml", Method.POST);
            request.RequestFormat = DataFormat.Xml;
            request.AddParameter("name", shelfName);
            request.AddParameter("book_id", bookId);
            request.AddParameter("a", remove ? "remove" : String.Empty);

            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            if (response.StatusCode == 201)
                return true;
            else
                return false;
        }
        #endregion

        #region Request/Response Handling
        ///// <summary>
        ///// Deserializes the response XML
        ///// </summary>
        ///// <param name="results"></param>
        ///// <returns>GoodreadsResponse object</returns>
        //private static GoodreadsResponse DeserializeResponse(string results)
        //{
        //    GoodreadsResponse response = null;

        //    using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(results)))
        //    {
        //        try
        //        {
        //            var serializer = new XmlSerializer(typeof(GoodreadsResponse));

        //            response = (GoodreadsResponse)serializer.Deserialize(stream);
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //    return response;
        //}

        ///// <summary>
        ///// Deserializes the response XML
        ///// </summary>
        ///// <param name="results"></param>
        ///// <returns>GoodreadsResponse object</returns>
        //private static T DeserializeResponse<T>(string results)
        //{
        //    T response = default(T);

        //    using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(results)))
        //    {
        //        try
        //        {
        //            var serializer = new XmlSerializer(typeof(T));

        //            response = (T)serializer.Deserialize(stream);
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //    return response;
        //}

        /// <summary>
        /// Deserializes the response XML
        /// </summary>
        /// <param name="results"></param>
        /// <returns>GoodreadsResponse object</returns>
        //private static GoodreadsResponse SerializerRequest(string results)
        //{
        //    GoodreadsResponse response = null;

        //    using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(results)))
        //    {
        //        try
        //        {
        //            var serializer = new XmlSerializer(typeof(GoodreadsResponse));

        //            response = (GoodreadsResponse)serializer.Deserialize(stream);
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //    return response;
        //}

        ///// <summary>
        ///// Performs an HTTP GET request to the given URL and returns the result.
        ///// </summary>
        ///// <param name="url">Target URL.</param>
        ///// <returns>Text returned by the response.</returns>
        //private async static Task<string> HttpGet(string url)
        //{
        //    string httpResponse = null;

        //    HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
        //    Request.Method = "GET";
        //    Request.ContentType = "application/x-www-form-urlencoded";

        //    try
        //    {
        //        HttpWebResponse response = (HttpWebResponse)await Request.GetResponseAsync();
        //        if (response != null)
        //        {
        //            StreamReader data = new StreamReader(response.GetResponseStream());
        //            httpResponse = await data.ReadToEndAsync();
        //        }
        //    }
        //    catch (WebException e) { }
        //    return httpResponse;
        //}

        /// <summary>
        /// Adds a 1second delay before releasing the api call semaphore
        /// </summary>
        /// <returns></returns>
        //public async Task ApiCooldown()
        //{
        //    await Task.Delay(COOLDOWN_MILLISECONDS);
        //    apiSemaphore.Release();
        //}
        #endregion

        #region untested
        ///// <summary>
        ///// Returns comments for the given resource
        ///// </summary>
        ///// <param name="statusID"></param>
        ///// <returns></returns>
        //public async Task<UserStatus> GetStatusUpdate(string type, string id, int page = 1)
        //{
        //    await apiSemaphore.WaitAsync();
        //    string results = await HttpGet(String.Format(Urls.CommentList, type, id, page.ToString()));
        //    ApiCooldown();

        //    var result = DeserializeResponse(results);

        //    return result.UserStatus;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="authorID"></param>
        //public async void AuthorBookList(string authorID)
        //{
        //    await apiSemaphore.WaitAsync();
        //    string results = await HttpGet(BASEURL + AUTHOR + LIST + authorID + ".xml" + "&key" + API_KEY);
        //    ApiCooldown();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="authorID"></param>
        //public async void AuthorInfoShow(string authorID)
        //{
        //    await apiSemaphore.WaitAsync();
        //    string results = await HttpGet(BASEURL + AUTHOR + SHOW + authorID + ".xml" + "&key" + API_KEY);
        //    ApiCooldown();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="isbn"></param>
        //public async Task<String> ISBNtoID(string isbn)
        //{
        //    await apiSemaphore.WaitAsync();
        //    string results = await HttpGet(BASEURL + "/" + BOOK + ISBNID + isbn + "&key" + API_KEY);
        //    ApiCooldown();

        //    return results;
        //}
        #endregion
    }
}