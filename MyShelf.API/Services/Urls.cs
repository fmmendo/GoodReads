using System;

namespace MyShelf.API.Services
{
    public class Urls
    {
        /// <summary>
        /// Base Goodreads URL
        /// </summary>
        public static string BaseUrl => "http://www.goodreads.com";

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

        //public static string UpdatesFriends { get { return BaseUrl + "/updates/friends.xml"; } }
    }
}
