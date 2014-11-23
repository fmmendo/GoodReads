using GoodReads.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GoodReads.API
{
    public class UserSettings
    {
        #region Singleton
        private static UserSettings _settings;
        public static UserSettings Settings
        {
            get
            {
                if (_settings == null)
                    _settings = new UserSettings();

                return _settings;
            }
        }

        private UserSettings()
        {
            if (authSettings == null)
            {
                if (roamingSettings != null && roamingSettings.Values != null && roamingSettings.Values.ContainsKey(USER_AUTH_SETTINGS))
                    authSettings = (ApplicationDataCompositeValue)roamingSettings.Values[USER_AUTH_SETTINGS];
                else
                    authSettings = new Windows.Storage.ApplicationDataCompositeValue();
            }
        }
        #endregion

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
        private const string GOODREADS_USER_INFO = "grUserInfo";

        private ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        private ApplicationDataCompositeValue authSettings;
        /// <summary>
        /// User settings storage file
        /// </summary>
        public ApplicationDataCompositeValue AuthSettings
        {
            get
            {

                return authSettings;
            }
        }

        public string OAuthToken
        {
            get { return AuthSettings[ACCESS_TOKEN_SETTING] != null ? AuthSettings[ACCESS_TOKEN_SETTING].ToString() : String.Empty; }
            set { AuthSettings[ACCESS_TOKEN_SETTING] = value; StoreSettings(); }
        }

        public string OAuthTokenSecret
        {
            get { return AuthSettings[ACCESS_TOKEN_SECRET_SETTING] != null ? AuthSettings[ACCESS_TOKEN_SECRET_SETTING].ToString() : String.Empty; }
            set { AuthSettings[ACCESS_TOKEN_SECRET_SETTING] = value; StoreSettings(); }
        }

        public string OAuthAccessToken
        {
            get { return AuthSettings[OAUTH_TOKEN_SETTING] != null ? AuthSettings[OAUTH_TOKEN_SETTING].ToString() : String.Empty; }
            set { AuthSettings[OAUTH_TOKEN_SETTING] = value; StoreSettings(); }
        }

        public string OAuthAccessTokenSecret
        {
            get { return AuthSettings[OAUTH_TOKEN_SECRET_SETTING] != null ? AuthSettings[OAUTH_TOKEN_SECRET_SETTING].ToString() : String.Empty; }
            set { AuthSettings[OAUTH_TOKEN_SECRET_SETTING] = value; StoreSettings(); }
        }

        public string GoodreadsUsername
        {
            get { return AuthSettings[GOODREADS_USER_NAME_SETTING] != null ? AuthSettings[GOODREADS_USER_NAME_SETTING].ToString() : String.Empty; }
            set { AuthSettings[GOODREADS_USER_NAME_SETTING] = value; StoreSettings(); }
        }

        public string GoodreadsUserID
        {
            get { return AuthSettings[GOODREADS_USER_ID_SETTING] != null ? AuthSettings[GOODREADS_USER_ID_SETTING].ToString() : String.Empty; }
            set { AuthSettings[GOODREADS_USER_ID_SETTING] = value; StoreSettings(); }
        }

        public string GoodreadsUserLink
        {
            get { return AuthSettings[GOODREADS_USER_LINK_SETTING] != null ? AuthSettings[GOODREADS_USER_LINK_SETTING].ToString() : String.Empty; }
            set { AuthSettings[GOODREADS_USER_LINK_SETTING] = value; StoreSettings(); }
        }

        public string GoodreadsUserImageUrl
        {
            get { return AuthSettings[GOODREADS_USER_IMAGE_SETTING] != null ? AuthSettings[GOODREADS_USER_IMAGE_SETTING].ToString() : String.Empty; }
            set { AuthSettings[GOODREADS_USER_IMAGE_SETTING] = value; StoreSettings(); }
        }

        public string GoodreadsUserSmallImageUrl
        {
            get { return AuthSettings[GOODREADS_USER_SMALLIMAGE_SETTING] != null ? AuthSettings[GOODREADS_USER_SMALLIMAGE_SETTING].ToString() : String.Empty; }
            set { AuthSettings[GOODREADS_USER_SMALLIMAGE_SETTING] = value; StoreSettings(); }
        }

        public User UserInfo
        {
            get { return AuthSettings[GOODREADS_USER_INFO] != null ? (User)AuthSettings[GOODREADS_USER_INFO] : null; }
            set { AuthSettings[GOODREADS_USER_INFO] = value; StoreSettings(); }
        }

        public bool IsUserAuthenticated
        {
            get { return false; }// !String.IsNullOrEmpty(OAuthAccessToken) && !String.IsNullOrEmpty(OAuthAccessTokenSecret); }
        }

        private void StoreSettings()
        {
            roamingSettings.Values[USER_AUTH_SETTINGS] = authSettings;
        }
    }
}
