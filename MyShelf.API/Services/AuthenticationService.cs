using Mendo.UAP.Common;
using MyShelf.API.Storage;
using MyShelf.API.Web;
using RestSharp;
using RestSharp.Contrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.Storage;

namespace MyShelf.API.Services
{
    public class AuthenticationService : Singleton<AuthenticationService>, IAuthenticationService
    {
        private AuthState _state;

        public event EventHandler<AuthState> AuthStateChanged;

        private void OnAuthStateChanged()
        {
            if (AuthStateChanged != null)
                AuthStateChanged(this, State);
        }

        #region Properties

        public bool IsTokenAvailable => !String.IsNullOrEmpty(MyShelfSettings.Instance.OAuthAccessToken);

        public bool IsTokenSecretAvailable => !String.IsNullOrEmpty(MyShelfSettings.Instance.OAuthAccessTokenSecret);
        
        /// <summary>
        /// Contains the current authentication state of the user
        /// </summary>
        public AuthState State
        {
            get { return _state; }
            private set { _state = value; OnAuthStateChanged(); }
        }
        #endregion

        public AuthenticationService()
        {
            State = AuthState.NotAuthenticated;

            if (IsTokenAvailable && IsTokenSecretAvailable)
                State = AuthState.Authenticated;
        }

        /// <summary>
        /// Authenticates the user using the Web Authentication Broker
        /// </summary>
        public async Task<bool> Authenticate()
        {
            // If we have an session key already no need to do anything
            if (State == AuthState.Authenticated)
                return true;

            // request token and secret
            IRestResponse requestResponse = await RequestToken();

            // Parse oauth token and token secret
            var querystring = HttpUtility.ParseQueryString(requestResponse.Content);
            if (querystring == null || querystring.Count != 2)
                return false;

            MyShelfSettings.Instance.OAuthToken = querystring["oauth_token"];
            MyShelfSettings.Instance.OAuthTokenSecret = querystring["oauth_token_secret"];

            // authenticate
            string goodreadsURL = String.Format(Urls.AuthUrl, MyShelfSettings.Instance.OAuthToken);
            WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, new Uri(goodreadsURL), WebAuthenticationBroker.GetCurrentApplicationCallbackUri());

            if (result == null || result.ResponseStatus != WebAuthenticationStatus.Success)
                return false;

            // request access token and secret
            IRestResponse accessResponse = await RequestAccessToken();

            // parse oauth access token and token secrets
            var querystring2 = HttpUtility.ParseQueryString(accessResponse.Content);
            if (querystring2 == null || querystring2.Count != 2)
                return false;

            MyShelfSettings.Instance.OAuthAccessToken = querystring2["oauth_token"];
            MyShelfSettings.Instance.OAuthAccessTokenSecret = querystring2["oauth_token_secret"];

            //// if we don't have a user ID yet, go fetch it
            //if (String.IsNullOrEmpty(UserSettings.Settings.GoodreadsUserID))
            //{
            //    var user = await GetUserID();

            //    UserSettings.Settings.GoodreadsUserID = user.Id;
            //    UserSettings.Settings.GoodreadsUserLink = user.Link;
            //    UserSettings.Settings.GoodreadsUsername = user.Name;
            //}

            //authenticatedUser = await GetUserInfo(UserSettings.Settings.GoodreadsUserID);
            //UserSettings.Settings.GoodreadsUserImageUrl = authenticatedUser.Image_url;
            //UserSettings.Settings.GoodreadsUserSmallImageUrl = authenticatedUser.Small_image_url;
            //justRefreshedUser = true;

            //GoodreadsUserShelves = await GetShelvesList();
            //justRefreshedShelves = true;

            //GoodreadsReviews = await GetShelfBooks();
            //justRefreshedReviews = true;
            State = AuthState.Authenticated;

            return true;
        }

        /// <summary>
        /// Requests a Token from the Api Client
        /// </summary>
        /// <returns></returns>
        public async Task<IRestResponse> RequestToken() => await ApiClient.Instance.ExecuteForRequestTokenAsync(Urls.RequestToken, Method.GET, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret);

        /// <summary>
        /// Requests an Access Token from the ApiClient
        /// </summary>
        /// <returns></returns>
        public async Task<IRestResponse> RequestAccessToken() => await ApiClient.Instance.ExecuteForAccessTokenAsync(Urls.AccessToken, Method.GET, MyShelfSettings.Instance.ConsumerKey, MyShelfSettings.Instance.ConsumerSecret, MyShelfSettings.Instance.OAuthToken, MyShelfSettings.Instance.OAuthTokenSecret);
    }
}
