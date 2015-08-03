using MyShelf.API.Storage;
using MyShelf.API.Web;
using MyShelf.API.XML;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public class UserService : ServiceBase, IUserService
    {
        /// <summary>
        /// Returns the logged in User
        /// </summary>
        /// <returns>User</returns>
        public async Task<User> GetUserID()
        {
            ApiClient.Instance.Authenticator = ApiClient.GetProtectedResourceAuthenticator(Settings.Instance.ConsumerKey, Settings.Instance.ConsumerSecret, Settings.Instance.OAuthToken, Settings.Instance.OAuthTokenSecret);

            var response = await ApiClient.Instance.ExecuteAsync("api/auth_user", Method.GET);

            var result = DeserializeResponse(response.Content.ToString());

            return result.User;
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
                userID = Settings.Instance.GoodreadsUserID;

            //if (userID == Settings.Instance.GoodreadsUserID)
            //    return authenticatedUser;

            string results = await ApiClient.Instance.HttpGet(String.Format(Urls.UserShow, userID, Settings.Instance.ConsumerKey));

            var result = DeserializeResponse(results);

            return result.User;
        }
    }
}
