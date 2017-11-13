using System;

namespace MyShelf.API.Services
{
    public class Urls
    {
        /// <summary>
        /// Base Goodreads URL
        /// </summary>
        public static string BaseUrl => "https://www.goodreads.com";

        #region Auth
        /// <summary>
        /// 1. OAuth Token
        /// </summary>
        public static string AuthUrl => "https://www.goodreads.com/oauth/authorize?oauth_token={0}";

        public static string RequestToken => "/oauth/request_token";

        public static string AccessToken => "oauth/access_token";
        #endregion

        /// <summary>
        /// 1. API KEY
        /// 2. Query
        /// </summary>
        public static string Search => String.Format("{0}{1}", BaseUrl, "/search.xml?key={0}&q={1}");

        public static string AuthUser => "api/auth_user";
        /// <summary>
        /// 1. User Id
        /// 2. API KEY
        /// </summary>
        public static string UserShow => String.Format("{0}{1}", BaseUrl, "/user/show/{0}.xml?key={1}");

        #region Author
        /// <summary>
        /// 1. Author Id
        /// 2. API KEY
        /// </summary>
        public static string AuthorShow => String.Format("{0}{1}", BaseUrl, "/author/show/{0}?format=xml&key={1}");

        /// <summary>
        /// 1. Author Id
        /// 2. API KEY
        /// 3. Page
        /// </summary>
        public static string AuthorBooks => String.Format("{0}{1}", BaseUrl, "/author/list/{0}?format=xml&key={1}");  //&page={2}
        #endregion

        /// <summary>
        /// 1. Book Id
        /// 2. API KEY
        /// </summary>
        public static string BookShow => String.Format("{0}{1}", BaseUrl, "/book/show/{0}?format=xml&key={1}");

        /// <summary>
        /// 1. Book Id
        /// 2. API KEY
        /// 3. User Id
        /// </summary>
        public static string UserReview => String.Format("{0}{1}", BaseUrl, "/review/show_by_user_and_book.xml?book_id={0}&key={1}&user_id={2}");

        public static string FriendUpdates => "updates/friends.xml";

        public static string FriendList => "/friend/user/{0}.xml?";// => "friend/user.xml";

        public static string ShelfBooks => "review/list/";

        public static string ShelvesList => "https://www.goodreads.com/shelf/list.xml?key={0}";
        //public static string UpdatesFriends { get { return BaseUrl + "/updates/friends.xml"; } }
    }
}
