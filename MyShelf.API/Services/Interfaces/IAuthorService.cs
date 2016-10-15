using Mendo.UWP.Network;
using MyShelf.API.XML;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public interface IAuthorService
    {
        /// <summary>
        /// Returns more complete author data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Author> GetAuthorInfo(string id, CacheMode cacheMode = CacheMode.Skip);

        /// <summary>
        /// Returns an author's books
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        Task<Books> GetAuthorBooks(string id/*, int page = 1*/, CacheMode cacheMode = CacheMode.Skip);
    }
}
