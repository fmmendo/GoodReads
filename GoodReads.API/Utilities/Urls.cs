using System;
using System.Collections.Generic;
using System.Text;

namespace GoodReads.API.Utilities
{
    public class Urls
    {
        public static string BaseUrl { get { return "http://www.goodreads.com"; } }

        /// <summary>
        /// 1. API KEY
        /// 2. Query
        /// </summary>
        public static string Search { get { return BaseUrl + "/search.xml?key={0}&q={1}"; } }

        /// <summary>
        /// 1. User Id
        /// 2. API KEY
        /// </summary>
        public static string UserShow { get { return BaseUrl + "/user/show/{0}.xml?key={1}"; } }

        /// <summary>
        /// 1. Status Id
        /// 2. API KEY
        /// </summary>
        public static string StatusShow { get { return BaseUrl + "/user_status/show/{0}.xml?key={1}"; } }

        /// <summary>
        /// 1. User Id
        /// 2. API KEY
        /// </summary>
        public static string ReadStatus { get { return BaseUrl + "/read_statuses/{0}?format=xml&key={1}"; } }

        /// <summary>
        /// 1. Type:  'author_blog_post', 'blog', 'book_news_post', 'chapter', 'comment', 'community_answer', 
        /// 'event_response', 'fanship', 'friend', 'giveaway', 'giveaway_request', 'group_user', 'interview', 
        /// 'librarian_note', 'link_collection', 'list', 'owned_book', 'photo', 'poll', 'poll_vote', 'queued_item', 
        /// 'question', 'question_user_stat', 'quiz', 'quiz_score', 'rating', 'read_status', 'recommendation', 
        /// 'recommendation_request', 'review', 'topic', 'user', 'user_challenge', 'user_following', 
        /// 'user_list_challenge', 'user_list_vote', 'user_quote', 'user_status', 'video'
        /// 2. Resource id
        /// 3. API KEY
        /// </summary>
        public static string CommentList { get { return BaseUrl + "/comment.xml?type={0}&id={1}&page={2}"; } }

        /// <summary>
        /// 1. Book Id
        /// 2. API KEY
        /// </summary>
        public static string BookShow { get { return BaseUrl + "/book/show/{0}?format=xml&key={1}"; } }

        /// <summary>
        /// 1. Author Id
        /// 2. API KEY
        /// </summary>
        public static string AuthorShow { get { return BaseUrl + "/author/show/{0}?format=xml&key={1}"; } }

        /// <summary>
        /// 1. Author Id
        /// 2. API KEY
        /// 3. Page
        /// </summary>
        public static string AuthorBooks { get { return BaseUrl + "/author/list/{0}?format=xml&key={1}"; } } //&page={2}

        public static string UpdatesFriends { get { return BaseUrl + "/updates/friends.xml"; } }

        public static string ShelvesList { get { return BaseUrl + "/shelf/list.xml"; } }
    }
}
