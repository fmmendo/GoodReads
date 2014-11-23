using GoodReads.API.Model;
using GoodReads.API.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace GoodReads.API
{
    public class GoodReadsService
    {
        private GoodReadsAPI _api;

        private User user;
        /// <summary>
        /// Authenticated User
        /// </summary>
        public User User
        {
            get { return user; }
            private set { user = value; }
        }

        public GoodReadsService(IGoodReadsAuthenticator authenticator)
        {
            _api = new GoodReadsAPI(authenticator);

            LoadCache();
        }

        /// <summary>
        /// Authenticates the user using the API
        /// </summary>
        /// <returns></returns>
        public async Task Authenticate()
        {
            await _api.Authenticate();
        }

        /// <summary>
        /// Returns the ID of the authenticated user, or empty if something goes wrong
        /// </summary>
        /// <returns>user id</returns>
        public async Task<String> GetUserId()
        {
            if (!String.IsNullOrEmpty(UserSettings.Settings.GoodreadsUserID))
            {
                var user = await _api.GetUserID();

                UserSettings.Settings.GoodreadsUserID = user.Id;
                UserSettings.Settings.GoodreadsUserLink = user.Link;
                UserSettings.Settings.GoodreadsUsername = user.Name;

                return user.Id;    
            }

            return String.Empty;
        }

        /// <summary>
        /// Returns the User info for the provided user, or authenticated user if none provided.
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>User</returns>
        public async Task<User> GetUserInfo(string userId = null)
        {
            // if no ID provided and no ID stored, nothing to do here
            if (String.IsNullOrEmpty(userId) && String.IsNullOrEmpty(UserSettings.Settings.GoodreadsUserID))
                return null;

            if (String.IsNullOrEmpty(userId))
            {
                // if no ID provided, use stored one and refresh authenticated user's info
                user = await GetUserInfo(UserSettings.Settings.GoodreadsUserID);

                return user;
            }                
            else 
            {
                //otherwise return provided user's info
                return await GetUserInfo(userId);
            }
        }

        public async Task<Updates> GetFriendUpdates()
        {
            return await _api.GetFriendUpdates("", "", "");
        }

        /// <summary>
        /// Load all data we have from storage
        /// </summary>
        private void LoadCache()
        {
            user = UserSettings.Settings.UserInfo;
        }
    }
}
