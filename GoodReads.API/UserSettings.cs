using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GoodReads.API
{
    public static class UserSettings
    {
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

        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        private static ApplicationDataCompositeValue settings;
        /// <summary>
        /// User settings storage file
        /// </summary>
        public static ApplicationDataCompositeValue Settings
        {
            get
            {
                if (settings == null)
                {
                    if (roamingSettings != null && roamingSettings.Values != null && roamingSettings.Values.ContainsKey(USER_AUTH_SETTINGS))
                        settings = (ApplicationDataCompositeValue)roamingSettings.Values[USER_AUTH_SETTINGS];
                    else
                        settings = new Windows.Storage.ApplicationDataCompositeValue();
                }
                return settings;
            }
        }

        public static string OAuthToken
        {
            get { return Settings[ACCESS_TOKEN_SETTING].ToString(); }
            set { Settings[ACCESS_TOKEN_SETTING] = value;  }
        }

        public static string OAuthTokenSecret
        {
            get { return Settings[ACCESS_TOKEN_SECRET_SETTING].ToString(); }
            set { Settings[ACCESS_TOKEN_SECRET_SETTING] = value; }
        }

        public static string OAuthAccessToken
        {
            get { return Settings[OAUTH_TOKEN_SETTING].ToString(); }
            set { Settings[OAUTH_TOKEN_SETTING] = value; }
        }

        public static string OAuthAccessTokenSecret
        {
            get { return Settings[OAUTH_TOKEN_SECRET_SETTING].ToString(); }
            set { Settings[OAUTH_TOKEN_SECRET_SETTING] = value; }
        }

        public static string GoodreadsUsername
        {
            get { return Settings[GOODREADS_USER_NAME_SETTING].ToString(); }
            set { Settings[GOODREADS_USER_NAME_SETTING] = value; }
        }

        public static string GoodreadsUserID
        {
            get { return Settings[GOODREADS_USER_ID_SETTING].ToString(); }
            set { Settings[GOODREADS_USER_ID_SETTING] = value; }
        }

        public static string GoodreadsUserLink
        {
            get { return Settings[GOODREADS_USER_LINK_SETTING].ToString(); }
            set { Settings[GOODREADS_USER_LINK_SETTING] = value; }
        }

        public static string GoodreadsUserImageUrl
        {
            get { return Settings[GOODREADS_USER_IMAGE_SETTING].ToString(); }
            set { Settings[GOODREADS_USER_IMAGE_SETTING] = value; }
        }

        public static string GoodreadsUserSmallImageUrl
        {
            get { return Settings[GOODREADS_USER_SMALLIMAGE_SETTING].ToString(); }
            set { Settings[GOODREADS_USER_SMALLIMAGE_SETTING] = value; }
        }

        public static bool IsUserAuthenticated
        {
            get { return !String.IsNullOrEmpty(OAuthAccessToken) && !String.IsNullOrEmpty(OAuthAccessTokenSecret); }
        }

        public static void StoreSettings()
        {
            roamingSettings.Values[USER_AUTH_SETTINGS] = settings;
        }
    }
}
