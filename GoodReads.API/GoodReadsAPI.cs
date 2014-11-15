using GoodReads.API.Model;
using GoodReads.API.Utilities;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Security.Authentication.Web;
using Windows.Storage;

namespace GoodReads.API
{
    public class GoodReadsAPI
    {
        public bool IsUserAuthenticated { get { return isUserAuthenticated; } }


        private const string BASEURL = "http://www.goodreads.com";
        private const string SEARCH = "/search.xml";
        private const string AUTHOR = "author/";
        private const string LIST = "list/";
        private const string SHOW = "show/";
        private const string BOOK = "book/";
        private const string ISBNID = "isbn_to_id/";
        private const string UPDATES = "updates/";
        private const string FRIENDS = "friends.xml";
        private bool isUserAuthenticated;

        private const int COOLDOWN_MILLISECONDS = 1000;

        #region Storage Settings
        private const string USER_AUTH_SETTINGS = "GoodReads.UserAuthenticationSettings";

        private const string ACCESS_TOKEN_SETTING = "accessToken";
        private const string ACCESS_TOKEN_SECRET_SETTING = "accessTokenSecret";
        private const string OAUTH_TOKEN_SETTING = "oauthToken";
        private const string OAUTH_TOKEN_SECRET_SETTING = "oauthTokenSecret";

        private const string GOODREADS_USER_ID_SETTING = "grUserID";
        private const string GOODREADS_USER_NAME_SETTING = "grUserName";
        private const string GOODREADS_USER_LINK_SETTING = "grUserLink";
        private const string GOODREADS_USER_IMAGE_SETTING = "grUserImage";
        private const string GOODREADS_USER_SMALLIMAGE_SETTING = "grUserSmallImage";
        #endregion

        #region Settings
        private ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        private string oauthToken;
        private string oauthTokenSecret;
        private string oauthAccessToken;
        private string oauthAccessTokenSecret;

        public string GoodreadsUsername { get; private set; }
        public string GoodreadsUserID { get; private set; }
        public string GoodreadsUserLink { get; private set; }
        public string GoodreadsUserImageUrl { get; private set; }
        public string GoodreadsUserSmallImageUrl { get; private set; }
        #endregion

        private RestClient client;
        private IGoodReadsAuthenticator authenticator;
        private readonly SemaphoreSlim apiSemaphore = new SemaphoreSlim(1, 1);
        private User authenticatedUser;
        private bool justRefreshedUser = false;

        public List<UserShelf> GoodreadsUserShelves { get; set; }
        private bool justRefreshedShelves = false;

        public Reviews GoodreadsReviews { get; set; }
        public bool justRefreshedReviews = false;

        public GoodReadsAPI(IGoodReadsAuthenticator authenticator)
        {
            isUserAuthenticated = false;

            this.authenticator = authenticator;
            client = new RestClient(BASEURL);

            RetrieveCredentials();
        }

        #region Persistance
        /// <summary>
        /// Stores the OAuth data in the Roaming folder
        /// </summary>
        private void StoreCredentials()
        {
            ApplicationDataCompositeValue composite = null;
            if (roamingSettings != null && roamingSettings.Values != null && roamingSettings.Values.ContainsKey(USER_AUTH_SETTINGS))
                composite = (ApplicationDataCompositeValue)roamingSettings.Values[USER_AUTH_SETTINGS];
            else
                composite = new Windows.Storage.ApplicationDataCompositeValue();

            composite[ACCESS_TOKEN_SETTING] = oauthAccessToken;
            composite[ACCESS_TOKEN_SECRET_SETTING] = oauthAccessTokenSecret;
            composite[OAUTH_TOKEN_SETTING] = oauthToken;
            composite[OAUTH_TOKEN_SECRET_SETTING] = oauthTokenSecret;

            composite[GOODREADS_USER_ID_SETTING] = GoodreadsUserID;
            composite[GOODREADS_USER_LINK_SETTING] = GoodreadsUserLink;
            composite[GOODREADS_USER_NAME_SETTING] = GoodreadsUsername;

            composite[GOODREADS_USER_IMAGE_SETTING] = GoodreadsUserImageUrl;
            composite[GOODREADS_USER_SMALLIMAGE_SETTING] = GoodreadsUserSmallImageUrl;

            roamingSettings.Values[USER_AUTH_SETTINGS] = composite;
        }

        /// <summary>
        /// Retrieves the OAuth data from the Roaming folder
        /// </summary>
        public bool RetrieveCredentials()
        {
            ApplicationDataCompositeValue composite = null;
            if (roamingSettings != null && roamingSettings.Values != null && roamingSettings.Values.ContainsKey(USER_AUTH_SETTINGS))
                composite = (ApplicationDataCompositeValue)roamingSettings.Values[USER_AUTH_SETTINGS];
            else
                return false;

            if (composite == null)
            {
                isUserAuthenticated = false;
            }
            else
            {
                oauthAccessToken = composite[ACCESS_TOKEN_SETTING].ToString();
                oauthAccessTokenSecret = composite[ACCESS_TOKEN_SECRET_SETTING].ToString();
                oauthToken = composite[OAUTH_TOKEN_SETTING].ToString();
                oauthTokenSecret = composite[OAUTH_TOKEN_SECRET_SETTING].ToString();

                GoodreadsUserID = composite[GOODREADS_USER_ID_SETTING].ToString();
                GoodreadsUserLink = composite[GOODREADS_USER_LINK_SETTING].ToString();
                GoodreadsUsername = composite[GOODREADS_USER_NAME_SETTING].ToString();

                GoodreadsUserImageUrl = composite[GOODREADS_USER_IMAGE_SETTING].ToString();
                GoodreadsUserSmallImageUrl = composite[GOODREADS_USER_SMALLIMAGE_SETTING].ToString();

                isUserAuthenticated = true;
            }

            return isUserAuthenticated;
        }
        #endregion

        #region API Calls

        /// <summary>
        /// Authenticates the user using the Web Authentication Broker
        /// </summary>
        public async Task Authenticate()
        {
            // If we have an session key already no need to do anything
            if (isUserAuthenticated)
                return;

            // set up get request tokens
            client.Authenticator = OAuth1Authenticator.ForRequestToken(API_KEY, OAUTH_SECRET);

            // Request token
            await apiSemaphore.WaitAsync();
            var request = new RestRequest("/oauth/request_token", Method.GET);
            var requestResponse = await client.ExecuteAsync(request);
            ApiCooldown();

            // Parse oauth token and token secret
            var querystring = HttpUtility.ParseQueryString(requestResponse.Content);
            if (querystring != null && querystring.Count > 0)
            {
                oauthToken = querystring["oauth_token"];
                oauthTokenSecret = querystring["oauth_token_secret"];
            }

            // authenticate
            string goodreadsURL = "https://www.goodreads.com/oauth/authorize?oauth_token=" + oauthToken;
            WebAuthenticationResult result = await authenticator.Authenticate(WebAuthenticationOptions.None, new Uri(goodreadsURL), WebAuthenticationBroker.GetCurrentApplicationCallbackUri());

            // success
            if (result != null && result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                // set up get 
                client.Authenticator = OAuth1Authenticator.ForAccessToken(API_KEY, OAUTH_SECRET, oauthToken, oauthTokenSecret);

                //request access token
                await apiSemaphore.WaitAsync();
                request = new RestRequest("oauth/access_token", Method.GET);
                var accessResponse = await client.ExecuteAsync(request);
                ApiCooldown();

                // parse oauth access token and token secrets
                querystring = HttpUtility.ParseQueryString(accessResponse.Content);
                if (querystring != null && querystring.Count > 0)
                {
                    oauthAccessToken = querystring["oauth_token"];
                    oauthAccessTokenSecret = querystring["oauth_token_secret"];
                }

                // if we don't have a user ID yet, go fetch it
                if (String.IsNullOrEmpty(GoodreadsUserID))
                {
                    var user = await GetUserID();

                    GoodreadsUserID = user.Id;
                    GoodreadsUserLink = user.Link;
                    GoodreadsUsername = user.Name;
                }

                authenticatedUser = await GetUserInfo(GoodreadsUserID);
                GoodreadsUserImageUrl = authenticatedUser.Image_url;
                GoodreadsUserSmallImageUrl = authenticatedUser.Small_image_url;
                justRefreshedUser = true;

                GoodreadsUserShelves = await GetShelvesList();
                justRefreshedShelves = true;

                GoodreadsReviews = await GetShelfBooks();
                justRefreshedReviews = true;

                // store tokens and goodreads data
                StoreCredentials();
            }
        }

        /// <summary>
        /// Returns the logged in User object
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<User> GetUserID()
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("api/auth_user", Method.GET);
            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            var result = DeserializeResponse(response.Content.ToString());

            return result.User;
        }

        /// <summary>
        /// Returns a users notifications
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Notifications> GetNotifications(int page = 1)
        {
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

            await apiSemaphore.WaitAsync();

            var request = new RestRequest("notifications.xml", Method.GET);
            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            var result = DeserializeResponse(response.Content.ToString());

            return result.Notifications;
        }

        /// <summary>
        /// Returns info for the given user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<User> GetUserInfo(string userID = null)
        {
            if (String.IsNullOrEmpty(userID))
                userID = GoodreadsUserID;

            if (userID == GoodreadsUserID && justRefreshedUser)
                return authenticatedUser;

            await apiSemaphore.WaitAsync();
            string results = await HttpGet(String.Format(Urls.UserShow, userID, API_KEY));
            ApiCooldown();

            var result = DeserializeResponse(results);

            return result.User;
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

        /// <summary>
        /// Returns the shelves for the logged in user
        /// </summary>
        /// <param name="query">string to search for</param>
        /// <returns>List of Work items</returns>
        public async Task<List<UserShelf>> GetShelvesList()
        {
            if (justRefreshedShelves)
            {
                // update it in the background
                Task.Run(async () =>
                {
                    await apiSemaphore.WaitAsync();
                    string results = await HttpGet("https://www.goodreads.com/shelf/list.xml?key=" + API_KEY);
                    ApiCooldown();

                    var result = DeserializeResponse(results);

                    GoodreadsUserShelves = result.Shelves.UserShelf;
                });
            }
            else
            {
                await apiSemaphore.WaitAsync();
                string results = await HttpGet("https://www.goodreads.com/shelf/list.xml?key=" + API_KEY);
                ApiCooldown();

                var result = DeserializeResponse(results);

                GoodreadsUserShelves = result.Shelves.UserShelf;
            }
            return GoodreadsUserShelves;
        }

        /// <summary>
        /// Returns the friend update feed for the logged in user
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Updates> GetFriendUpdates(string type, string filter, string maxUpdates)
        {

            string url = Urls.UpdatesFriends;// BASEURL + UPDATES + FRIENDS;// +type + filter + "&max_updates=" + maxUpdates + "&access_token=" + OAuthAccessToken;

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

            await apiSemaphore.WaitAsync();
            var request = new RestRequest(UPDATES + FRIENDS, Method.GET);
            var response = await client.ExecuteAsync(request);
            ApiCooldown();

            GoodreadsResponse result = DeserializeResponse(response.Content.ToString());

            return result.Updates;
        }

        /// <summary>
        /// Returns the books for the logged in user
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Reviews> GetShelfBooks(string shelf = null, string sort = null, string query = null, string order = null, string page = null, string per_page = "200")
        {
            if (shelf == null && justRefreshedReviews)
            {
                Task.Run(async () =>
                {
                    client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);
                    string url = "review/list/" + GoodreadsUserID + ".xml?key=" + API_KEY + "&format=xml&v=2";

                    //TODO: more params, probably enums
                    if (!String.IsNullOrEmpty(shelf))
                        url += "&shelf=" + shelf;
                    if (!String.IsNullOrEmpty(per_page))
                        url += "&per_page=" + per_page;

                    await apiSemaphore.WaitAsync();
                    var request = new RestRequest(url, Method.GET);
                    var response = await client.ExecuteAsync(request);

                    ApiCooldown();

                    var result = DeserializeResponse(response.Content.ToString());
                    GoodreadsReviews = result.Reviews;
                });
            }
            else
            { 

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);
            string url = "review/list/" + GoodreadsUserID + ".xml?key=" + API_KEY + "&format=xml&v=2";

            //TODO: more params, probably enums
            if (!String.IsNullOrEmpty(shelf))
                url += "&shelf=" + shelf;
            if (!String.IsNullOrEmpty(per_page))
                url += "&per_page=" + per_page;

            await apiSemaphore.WaitAsync();
            var request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteAsync(request);

            ApiCooldown();

            var result = DeserializeResponse(response.Content.ToString());
            GoodreadsReviews = result.Reviews;
            }

            return GoodreadsReviews;
        }

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

        /// <summary>
        /// Returns more complete author data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Author> GetAuthorInfo(string id)
        {
            await apiSemaphore.WaitAsync();
            string results = await HttpGet(String.Format(Urls.AuthorShow, id, API_KEY));
            ApiCooldown();

            var result = DeserializeResponse(results);

            return result.Author;
        }

        /// <summary>
        /// Returns an author's books
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<Books> GetAuthorBooks(string id/*, int page = 1*/)
        {
            await apiSemaphore.WaitAsync();
            string results = await HttpGet(String.Format(Urls.AuthorBooks, id, API_KEY/*, page.ToString()*/));
            ApiCooldown();

            var result = DeserializeResponse(results);

            return result.Author.Books;
        }

        /// <summary>
        /// Returns infor for the given user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<UserStatus> GetReadStatus(string userID = null)
        {
            if (String.IsNullOrEmpty(userID))
                userID = GoodreadsUserID;

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
            client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, oauthAccessToken, oauthAccessTokenSecret);

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
        /// <summary>
        /// Deserializes the response XML
        /// </summary>
        /// <param name="results"></param>
        /// <returns>GoodreadsResponse object</returns>
        private static GoodreadsResponse DeserializeResponse(string results)
        {
            GoodreadsResponse response = null;

            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(results)))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(GoodreadsResponse));

                    response = (GoodreadsResponse)serializer.Deserialize(stream);
                }
                catch (Exception)
                {
                }
            }
            return response;
        }

        /// <summary>
        /// Deserializes the response XML
        /// </summary>
        /// <param name="results"></param>
        /// <returns>GoodreadsResponse object</returns>
        private static T DeserializeResponse<T>(string results)
        {
            T response = default(T);

            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(results)))
            {
                try
                {
                    var serializer = new XmlSerializer(typeof(T));

                    response = (T)serializer.Deserialize(stream);
                }
                catch (Exception)
                {
                }
            }
            return response;
        }

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

        /// <summary>
        /// Performs an HTTP GET request to the given URL and returns the result.
        /// </summary>
        /// <param name="url">Target URL.</param>
        /// <returns>Text returned by the response.</returns>
        private async static Task<string> HttpGet(string url)
        {
            string httpResponse = null;

            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            Request.Method = "GET";
            Request.ContentType = "application/x-www-form-urlencoded";

            try
            {
                HttpWebResponse response = (HttpWebResponse)await Request.GetResponseAsync();
                if (response != null)
                {
                    StreamReader data = new StreamReader(response.GetResponseStream());
                    httpResponse = await data.ReadToEndAsync();
                }
            }
            catch (WebException e) { }
            return httpResponse;
        }

        /// <summary>
        /// Adds a 1second delay before releasing the api call semaphore
        /// </summary>
        /// <returns></returns>
        public async Task ApiCooldown()
        {
            await Task.Delay(COOLDOWN_MILLISECONDS);
            apiSemaphore.Release();
        }
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