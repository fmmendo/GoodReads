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

        //private Uri baseUri;
        //private Uri requestTokenUri;
        //private Uri authorizeUri;
        //private Uri accessTokenUri;
        //private Uri callbackUri;

        private readonly SemaphoreSlim apiSemaphore = new SemaphoreSlim(1, 1);

        #region Properties

        public bool IsTokenAvailable => !String.IsNullOrEmpty(Settings.Instance.OAuthAccessToken);

        public bool IsTokenSecretAvailable => !String.IsNullOrEmpty(Settings.Instance.OAuthAccessTokenSecret);
        
        /// <summary>
        /// Contains the current authentication state of the user
        /// </summary>
        public AuthState State
        {
            get { return _state; }
            private set { _state = value; }
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

            Settings.Instance.OAuthToken = querystring["oauth_token"];
            Settings.Instance.OAuthTokenSecret = querystring["oauth_token_secret"];

            // authenticate
            string goodreadsURL = String.Format("https://www.goodreads.com/oauth/authorize?oauth_token={0}", Settings.Instance.OAuthToken);
            WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, new Uri(goodreadsURL), WebAuthenticationBroker.GetCurrentApplicationCallbackUri());

            if (result == null || result.ResponseStatus != WebAuthenticationStatus.Success)
                return false;

            // request access token and secret
            IRestResponse accessResponse = await RequestAccessToken();

            // parse oauth access token and token secrets
            var querystring2 = HttpUtility.ParseQueryString(accessResponse.Content);
            if (querystring2 == null || querystring2.Count != 2)
                return false;

            Settings.Instance.OAuthAccessToken = querystring2["oauth_token"];
            Settings.Instance.OAuthAccessTokenSecret = querystring2["oauth_token_secret"];

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

            return true;
        }

        //public async Task<bool> CompleteAuthentication()
        //{
        //    IRestResponse accessResponse = await RequestAccessToken();

        //    // parse oauth access token and token secrets
        //    var querystring = HttpUtility.ParseQueryString(accessResponse.Content);
        //    if (querystring != null && querystring.Count == 2)
        //    {
        //        OAuthAccessToken = querystring["oauth_token"];
        //        OAuthAccessTokenSecret = querystring["oauth_token_secret"];
        //    }
        //    else return false;

        //    //// if we don't have a user ID yet, go fetch it
        //    //if (String.IsNullOrEmpty(UserSettings.Settings.GoodreadsUserID))
        //    //{
        //    //    var user = await GetUserID();

        //    //    UserSettings.Settings.GoodreadsUserID = user.Id;
        //    //    UserSettings.Settings.GoodreadsUserLink = user.Link;
        //    //    UserSettings.Settings.GoodreadsUsername = user.Name;
        //    //}

        //    //authenticatedUser = await GetUserInfo(UserSettings.Settings.GoodreadsUserID);
        //    //UserSettings.Settings.GoodreadsUserImageUrl = authenticatedUser.Image_url;
        //    //UserSettings.Settings.GoodreadsUserSmallImageUrl = authenticatedUser.Small_image_url;
        //    //justRefreshedUser = true;

        //    //GoodreadsUserShelves = await GetShelvesList();
        //    //justRefreshedShelves = true;

        //    //GoodreadsReviews = await GetShelfBooks();
        //    //justRefreshedReviews = true;

        //    return true;
        //}

        public async Task<IRestResponse> RequestToken()
        {
            // set up get request tokens
            //ApiClient.Instance.Authenticator = ApiClient.GetRequestTokenAuthenticator(Settings.Instance.ConsumerKey, Settings.Instance.ConsumerSecret);

            // Request token
            return await ApiClient.Instance.ExecuteForRequestTokenAsync("/oauth/request_token", Method.GET, Settings.Instance.ConsumerKey, Settings.Instance.ConsumerSecret);
        }

        public async Task<IRestResponse> RequestAccessToken()
        {
            // set up get 
            //ApiClient.Instance.Authenticator = ApiClient.GetAccessTokenAuthenticator(Settings.Instance.ConsumerKey, Settings.Instance.ConsumerSecret, Settings.Instance.OAuthToken, Settings.Instance.OAuthTokenSecret);

            //request access token
            return await ApiClient.Instance.ExecuteForAccessTokenAsync("oauth/access_token", Method.GET, Settings.Instance.ConsumerKey, Settings.Instance.ConsumerSecret, Settings.Instance.OAuthToken, Settings.Instance.OAuthTokenSecret);
        }
    }
}
