using MyShelf.API.Web;
using RestSharp;
using RestSharp.Authenticators;
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
    public class AuthenticationService : IAuthenticationService
    {
        private AuthState _state;

        private string apiKey = "JRjTYygQzUjkodkHuqfOjg";
            private string oauthSecret = "nEQ6pRIdWTY27jsIYHXW9regO4aCIPDuozjUls8FASk";

        //private Uri baseUri;
        //private Uri requestTokenUri;
        //private Uri authorizeUri;
        //private Uri accessTokenUri;
        //private Uri callbackUri;

        private IApiClient _apiClient;
        private RestClient client;
        private readonly SemaphoreSlim apiSemaphore = new SemaphoreSlim(1, 1);

        #region Auth Settings

        private const string AUTH_SETTINGS = "MyShelf.AuthenticationSettings";

        private const string ACCESS_TOKEN_SETTING = "MyShelf.AccessToken";
        private const string ACCESS_TOKEN_SECRET_SETTING = "MyShelf.AccessTokenSecret";
        private const string OAUTH_TOKEN_SETTING = "MyShelf.OauthToken";
        private const string OAUTH_TOKEN_SECRET_SETTING = "MyShelf.OauthTokenSecret";

        // Roaming settings
        private ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        private ApplicationDataCompositeValue authSettings;
        /// <summary>
        /// User settings storage file
        /// </summary>
        public ApplicationDataCompositeValue AuthSettings
        {
            get
            {
                if (authSettings == null)
                {
                    if (roamingSettings != null && roamingSettings.Values != null && roamingSettings.Values.ContainsKey(AUTH_SETTINGS))
                        authSettings = (ApplicationDataCompositeValue)roamingSettings.Values[AUTH_SETTINGS];
                    else
                        authSettings = new Windows.Storage.ApplicationDataCompositeValue();
                }

                return authSettings;
            }
        }

        public string OAuthToken
        {
            get { return AuthSettings[OAUTH_TOKEN_SETTING] != null ? AuthSettings[OAUTH_TOKEN_SETTING].ToString() : String.Empty; }
            set { AuthSettings[OAUTH_TOKEN_SETTING] = value; StoreSettings(); }
        }

        public string OAuthTokenSecret
        {
            get { return AuthSettings[OAUTH_TOKEN_SECRET_SETTING] != null ? AuthSettings[OAUTH_TOKEN_SECRET_SETTING].ToString() : String.Empty; }
            set { AuthSettings[OAUTH_TOKEN_SECRET_SETTING] = value; StoreSettings(); }
        }

        public string OAuthAccessToken
        {
            get { return AuthSettings[ACCESS_TOKEN_SETTING] != null ? AuthSettings[ACCESS_TOKEN_SETTING].ToString() : String.Empty; }
            set { AuthSettings[ACCESS_TOKEN_SETTING] = value; StoreSettings(); }
        }

        public string OAuthAccessTokenSecret
        {
            get { return AuthSettings[ACCESS_TOKEN_SECRET_SETTING] != null ? AuthSettings[ACCESS_TOKEN_SECRET_SETTING].ToString() : String.Empty; }
            set { AuthSettings[ACCESS_TOKEN_SECRET_SETTING] = value; StoreSettings(); }
        }

        private void StoreSettings()
        {
            roamingSettings.Values[AUTH_SETTINGS] = authSettings;
        }

        #endregion

        #region Properties

        public bool IsTokenAvailable => !String.IsNullOrEmpty(OAuthAccessToken);

        public bool IsTokenSecretAvailable => !String.IsNullOrEmpty(OAuthAccessTokenSecret);


        /// <summary>
        /// Contains the current authentication state of the user
        /// </summary>
        public AuthState State
        {
            get { return _state; }
            private set { _state = value; }
        }
        #endregion

        public AuthenticationService(IApiClient apiClient)
        {
            State = AuthState.NotAuthenticated;

            _apiClient = apiClient;
            client = new RestClient("http://www.goodreads.com");

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

            IRestResponse requestResponse = await RequestToken();

            // Parse oauth token and token secret
            var querystring = HttpUtility.ParseQueryString(requestResponse.Content);
            if (querystring != null && querystring.Count == 2)
            {
                OAuthToken = querystring["oauth_token"];
                OAuthTokenSecret = querystring["oauth_token_secret"];
            }
            else return false;

            // authenticate
            string goodreadsURL = String.Format("https://www.goodreads.com/oauth/authorize?oauth_token={0}", OAuthToken);
            WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, new Uri(goodreadsURL), WebAuthenticationBroker.GetCurrentApplicationCallbackUri());

            // success
            if (result != null && result.ResponseStatus == WebAuthenticationStatus.Success)
            {
                return await CompleteAuthentication();
            }
            return false;
        }

        public async Task<bool> CompleteAuthentication()
        {
            IRestResponse accessResponse = await RequestAccessToken();

            // parse oauth access token and token secrets
            var querystring = HttpUtility.ParseQueryString(accessResponse.Content);
            if (querystring != null && querystring.Count == 2)
            {
                OAuthAccessToken = querystring["oauth_token"];
                OAuthAccessTokenSecret = querystring["oauth_token_secret"];
            }
            else return false;

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

        public async Task<IRestResponse> RequestToken()
        {
            // set up get request tokens
            client.Authenticator = OAuth1Authenticator.ForRequestToken(apiKey, oauthSecret);

            // Request token
            await apiSemaphore.WaitAsync();
            var request = new RestRequest("/oauth/request_token", Method.GET);
            var requestResponse = await client.ExecuteAsync(request);

            // Wait 1 second between calls
            await Task.Delay(1000);
            return requestResponse;
        }

        public async Task<IRestResponse> RequestAccessToken()
        {
            // set up get 
            client.Authenticator = OAuth1Authenticator.ForAccessToken(apiKey, oauthSecret, OAuthToken, OAuthTokenSecret);

            //request access token
            await apiSemaphore.WaitAsync();
            var request = new RestRequest("oauth/access_token", Method.GET);
            var accessResponse = await client.ExecuteAsync(request);

            // Wait 1 second between calls
            await Task.Delay(1000);
            return accessResponse;
        }
    }
}
