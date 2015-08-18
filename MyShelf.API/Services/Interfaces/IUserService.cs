using MyShelf.API.XML;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public interface IUserService
    {
        bool IsUserIdAvailable { get; }

        /// <summary>
        /// Returns the logged in User
        /// </summary>
        /// <returns>User</returns>
        Task<User> GetUserID(bool refresh = false);

        /// <summary>
        /// Returns a goodreads User object for the given Id. If no ID is passed
        /// used logged in user instead.
        /// </summary>
        /// <param name="userID">goodreads user ID</param>
        /// <returns>User</returns>
        Task<User> GetUserInfo(string userID = null);

        /// <summary>
        /// Returns the friend update feed for the logged in user
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        Task<Updates> GetFriendUpdates(string type, string filter, string maxUpdates);

        Task<Friends> GetFriends(string page = null, string sort = null);

        /// <summary>
        /// Likes a resource
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        Task<bool> LikeResource(string resourceId, string resourceType);

        /// <summary>
        /// Unlike a resource
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        Task<bool> UnlikeResource(string resourceId/*, string resourceType*/);

        /// <summary>
        /// Adds a Comment
        /// </summary>
        /// <param name="type"></param>
        /// <param name="filter"></param>
        /// <param name="maxUpdates"></param>
        Task<string> AddComment(string id, string type, string comment);
    }
}
