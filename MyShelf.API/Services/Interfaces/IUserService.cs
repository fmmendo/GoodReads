using MyShelf.API.XML;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Returns the logged in User
        /// </summary>
        /// <returns>User</returns>
        Task<User> GetUserID();

        /// <summary>
        /// Returns a goodreads User object for the given Id. If no ID is passed
        /// used logged in user instead.
        /// </summary>
        /// <param name="userID">goodreads user ID</param>
        /// <returns>User</returns>
        Task<User> GetUserInfo(string userID = null);
    }
}
