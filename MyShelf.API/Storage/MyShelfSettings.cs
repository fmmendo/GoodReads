using Mendo.UAP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MyShelf.API.Storage
{
    public class MyShelfSettings : Singleton<MyShelfSettings>
    {
        public string ConsumerKey => "JRjTYygQzUjkodkHuqfOjg";
        public string ConsumerSecret => "nEQ6pRIdWTY27jsIYHXW9regO4aCIPDuozjUls8FASk";

        public string InAppProductKey => "RemoveAds";

        /// <summary>
        /// Roaming settings
        /// </summary>
        public ApplicationDataContainer RoamingSettings => ApplicationData.Current.RoamingSettings;

        #region User Settings
        private const string SETTINGS_USER = "MyShelf.Settings.User";

        private readonly string GOODREADS_USER_ID = $"{SETTINGS_USER}.UserId";
        private readonly string GOODREADS_USERNAME = $"{SETTINGS_USER}.UserName";
        private readonly string GOODREADS_USERLINK = $"{SETTINGS_USER}.UserLink";

        private ApplicationDataCompositeValue userSettings;
        /// <summary>
        /// User settings storage file
        /// </summary>
        public ApplicationDataCompositeValue UserSettings
        {
            get
            {
                if (userSettings == null)
                {
                    if (RoamingSettings != null && RoamingSettings.Values != null && RoamingSettings.Values.ContainsKey(SETTINGS_USER))
                        userSettings = (ApplicationDataCompositeValue)RoamingSettings.Values[SETTINGS_USER];
                    else
                        userSettings = new ApplicationDataCompositeValue();
                }

                return userSettings;
            }
        }

        /// <summary>
        /// User Identifier
        /// </summary>
        public string GoodreadsUserID
        {
            get { return UserSettings[GOODREADS_USER_ID] != null ? UserSettings[GOODREADS_USER_ID].ToString() : String.Empty; }
            set { UserSettings[GOODREADS_USER_ID] = value; StoreRoamingSettings(SETTINGS_USER, userSettings); }
        }

        /// <summary>
        /// User name
        /// </summary>
        public string GoodreadsUsername
        {
            get { return UserSettings[GOODREADS_USERNAME] != null ? UserSettings[GOODREADS_USERNAME].ToString() : String.Empty; }
            set { UserSettings[GOODREADS_USERNAME] = value; StoreRoamingSettings(SETTINGS_USER, userSettings); }
        }

        /// <summary>
        /// Link to the user's profile (on goodreads website)
        /// </summary>
        public string GoodreadsUserLink
        {
            get { return UserSettings[GOODREADS_USERLINK] != null ? UserSettings[GOODREADS_USERLINK].ToString() : String.Empty; }
            set { UserSettings[GOODREADS_USERLINK] = value; StoreRoamingSettings(SETTINGS_USER, userSettings); }
        }

        #endregion

        #region Auth Settings
        private const string SETTINGS_AUTHENTICATION = "MyShelf.Settings.Authentication";

        private readonly string ACCESS_TOKEN_SETTING = $"{SETTINGS_AUTHENTICATION}.AccessToken";
        private readonly string ACCESS_TOKEN_SECRET_SETTING = $"{SETTINGS_AUTHENTICATION}.AccessTokenSecret";
        private readonly string OAUTH_TOKEN_SETTING = $"{SETTINGS_AUTHENTICATION}.OauthToken";
        private readonly string OAUTH_TOKEN_SECRET_SETTING = $"{SETTINGS_AUTHENTICATION}.OauthTokenSecret";

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
                    if (RoamingSettings != null && RoamingSettings.Values != null && RoamingSettings.Values.ContainsKey(SETTINGS_AUTHENTICATION))
                        authSettings = (ApplicationDataCompositeValue)RoamingSettings.Values[SETTINGS_AUTHENTICATION];
                    else
                        authSettings = new ApplicationDataCompositeValue();
                }

                return authSettings;
            }
        }

        public string OAuthToken
        {
            get { return AuthSettings[OAUTH_TOKEN_SETTING] != null ? AuthSettings[OAUTH_TOKEN_SETTING].ToString() : String.Empty; }
            set { AuthSettings[OAUTH_TOKEN_SETTING] = value; StoreRoamingSettings(SETTINGS_AUTHENTICATION, authSettings); }
        }

        public string OAuthTokenSecret
        {
            get { return AuthSettings[OAUTH_TOKEN_SECRET_SETTING] != null ? AuthSettings[OAUTH_TOKEN_SECRET_SETTING].ToString() : String.Empty; }
            set { AuthSettings[OAUTH_TOKEN_SECRET_SETTING] = value; StoreRoamingSettings(SETTINGS_AUTHENTICATION, authSettings); }
        }

        public string OAuthAccessToken
        {
            get { return AuthSettings[ACCESS_TOKEN_SETTING] != null ? AuthSettings[ACCESS_TOKEN_SETTING].ToString() : String.Empty; }
            set { AuthSettings[ACCESS_TOKEN_SETTING] = value; StoreRoamingSettings(SETTINGS_AUTHENTICATION, authSettings); }
        }

        public string OAuthAccessTokenSecret
        {
            get { return AuthSettings[ACCESS_TOKEN_SECRET_SETTING] != null ? AuthSettings[ACCESS_TOKEN_SECRET_SETTING].ToString() : String.Empty; }
            set { AuthSettings[ACCESS_TOKEN_SECRET_SETTING] = value; StoreRoamingSettings(SETTINGS_AUTHENTICATION, authSettings); }
        }

        #endregion

        private void StoreRoamingSettings(string key, ApplicationDataCompositeValue value)
        {
            RoamingSettings.Values[key] = value;
        }
    }
}
