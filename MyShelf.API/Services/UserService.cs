using Mendo.UAP.Common;
using MyShelf.API.Storage;
using MyShelf.API.Web;
using MyShelf.API.XML;
using MyShelf.API.XML.Utilities;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public class UserService : Singleton<UserService>, IUserService
    {
        public bool IsUserIdAvailable => !String.IsNullOrEmpty(MyShelfSettings.Instance.GoodreadsUserID);

        private User _currentUser = null;

        /// <summary>
        /// Returns the logged in User
        /// </summary>
        /// <returns>User</returns>
        public async Task<User> GetUserID(bool refresh = false)
        {
            if (_currentUser == null || refresh == true)
            {
                var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync(Urls.AuthUser, Method.GET, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret);

                var result = GoodReadsSerializer.DeserializeResponse(response.Content.ToString());

                _currentUser = result.User;

                MyShelfSettings.Instance.GoodreadsUserID = _currentUser.Id;
                MyShelfSettings.Instance.GoodreadsUsername = _currentUser.UserName;
                MyShelfSettings.Instance.GoodreadsUserLink = _currentUser.Link;
            }

            return _currentUser;
        }

        /// <summary>
        /// Returns a goodreads User object for the given Id. If no ID is passed
        /// used logged in user instead.
        /// </summary>
        /// <param name="userID">goodreads user ID</param>
        /// <returns>User</returns>
        public async Task<User> GetUserInfo(string userID = null)
        {
            if (String.IsNullOrEmpty(userID))
            {
                //we want the logged in user
                if (IsUserIdAvailable)
                {
                    userID = MyShelfSettings.Instance.GoodreadsUserID;
                }
                else
                {
                }
            }
            else
            {
                //we want someone else
            }


            //if (userID == MyShelfSettings.Instance.GoodreadsUserID)
            //    return authenticatedUser;

            string results = await ApiClient.Instance.HttpGet(String.Format(Urls.UserShow, userID, MyShelfSettings.Instance.ConsumerKey));

            var result = GoodReadsSerializer.DeserializeResponse(results);

            return result.User;
        }

        /// <summary>
        /// Returns the friend update feed for the logged in user
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<Updates> GetFriendUpdates(string type, string filter, string maxUpdates)
        {
            var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync(Urls.FriendUpdates, Method.GET, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret);

            GoodreadsResponse result = GoodReadsSerializer.DeserializeResponse(response.Content.ToString());

            return result.Updates;
        }

        /// <summary>
        /// Returns a user's friends
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public async Task<Friends> GetFriends(string page = null, string sort = null)
        {
            var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync(string.Format(Urls.FriendList, MyShelfSettings.Instance.GoodreadsUserID), Method.GET, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret);

            GoodreadsResponse result = GoodReadsSerializer.DeserializeResponse(response.Content.ToString());

            return result.Friends;
        }

        /// <summary>
        /// Likes a resource
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<bool> LikeResource(string resourceId, string resourceType)
        {
            var param = new Dictionary<string, object>();
            param.Add("rating[rating]", 1);
            param.Add("rating[resource_id]", resourceId);
            param.Add("rating[resource_type]", resourceType);

            var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync("rating", Method.POST, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret, param);

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
        public async Task<bool> UnlikeResource(string resourceId/*, string resourceType*/)
        {
            var param = new Dictionary<string, object>();
            //param.Add("rating[rating]", 1);
            param.Add("id", resourceId);
            //param.Add("rating[resource_type]", resourceType);

            var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync("rating", Method.DELETE, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret, param);

            if (response.StatusCode == 200 && response.ResponseStatus == ResponseStatus.Completed)
                return true;
            else
                return false;

            //client.Authenticator = OAuth1Authenticator.ForProtectedResource(API_KEY, OAUTH_SECRET, UserSettings.Settings.OAuthAccessToken, UserSettings.Settings.OAuthAccessTokenSecret);

            //await apiSemaphore.WaitAsync();

            //var request = new RestRequest("rating", Method.POST);
            //request.RequestFormat = DataFormat.Xml;
            //request.AddParameter("rating[rating]", 1);
            //request.AddParameter("rating[resource_id]", resourceId);
            //request.AddParameter("rating[resource_type]", resourceType);

            //var response = await client.ExecuteAsync(request);

            //ApiCooldown();

            ////TODO: This is a quick workaround
            //if (response.StatusCode == 201 && response.StatusDescription == "Created" && response.ResponseStatus == ResponseStatus.Completed)
            //{
            //    var result = response.Content.ToString();
            //    var start = result.IndexOf("<id type=\"integer\">") + 19;
            //    var end = result.IndexOf("</id>", start);
            //    string id = result.Substring(start, end - start);
            //    //var status = DeserializeResponse<UserStatus>(response.Content.ToString());

            //    //return status;


            //    return id;
            //}
            //else return String.Empty;
            ////else
            ////    return false;
        }

        /// <summary>
        /// Adds a Comment
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<string> AddComment(string id, string type, string comment)
        {
            var param = new Dictionary<string, object>();
            param.Add("type", type);
            param.Add("id", id);
            param.Add("comment[body]", comment);

            var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync("comment.xml", Method.POST, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret, param);

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
        /// Posts a new user status
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        public async Task<String> PostStatusUpdate(string bookId, string page, string percent, string body)
        {
            var param = new Dictionary<string, object>();
            if (!String.IsNullOrEmpty(bookId)) param.Add("user_status[book_id]", bookId);
            if (!String.IsNullOrEmpty(page))  param.Add("user_status[page]", page);
            if (!String.IsNullOrEmpty(percent))  param.Add("user_status[percent]", percent);
            if (!String.IsNullOrEmpty(body)) param.Add("user_status[body]", body);

            var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync("user_status.xml", Method.POST, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret, param);

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
    }
}
