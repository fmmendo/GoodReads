using Mendo.UAP.Common;
using MyShelf.API.Storage;
using MyShelf.API.Web;
using MyShelf.API.XML;
using MyShelf.API.XML.Utilities;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public class UserService : Singleton<UserService>, IUserService
    {
        public bool IsUserIdAvailable => !String.IsNullOrEmpty(MyShelfSettings.Instance.GoodreadsUserID);

        public User _currentUser = null;

        /// <summary>
        /// Returns the logged in User
        /// </summary>
        /// <returns>User</returns>
        public async Task<User> GetUserID(bool refresh = false)
        {
            if (_currentUser == null || refresh == true)
            {
                //ApiClient.Instance.Authenticator = ApiClient.GetProtectedResourceAuthenticator(MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthToken, MyShelfSettings.Instance.OAuthTokenSecret);

                var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync(Urls.AuthUser, Method.GET, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret);
                //var response2 = await ApiClient.Instance.HttpGet(@"https://www.goodreads.com/api/auth_user");

                //ApiClient.Instance.Authenticator = OAuth1Authenticator.ForProtectedResource(MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthToken, MyShelfSettings.Instance.OAuthTokenSecret);

                ////    await apiSemaphore.WaitAsync();
                //RestClient _client = new RestClient("http://www.goodreads.com");
                //_client.Authenticator = OAuth1Authenticator.ForProtectedResource(MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthToken, MyShelfSettings.Instance.OAuthTokenSecret);

                //var request = new RestRequest("api/auth_user", Method.GET);
                //    var response3 = await _client.ExecuteAsync(request);

                var result = GoodReadsSerializer.DeserializeResponse(response.Content.ToString());
                //var result2 = GoodReadsSerializer.DeserializeResponse(response2);

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

        public async Task<Friends> GetFriends(string page = null, string sort = null)
        {
            var response = await ApiClient.Instance.ExecuteForProtectedResourceAsync(String.Format(Urls.FriendList, MyShelfSettings.Instance.GoodreadsUserID), Method.GET, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthAccessToken, MyShelfSettings.Instance.OAuthAccessTokenSecret);

            GoodreadsResponse result = GoodReadsSerializer.DeserializeResponse(response.Content.ToString());

            return result.Friends;
        }
    }
}
