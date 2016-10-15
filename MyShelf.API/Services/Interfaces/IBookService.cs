using Mendo.UWP.Network;
using MyShelf.API.XML;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public interface IBookService
    {
        Task<Reviews> GetBooks(string shelf = null, string sort = null, string query = null, string order = null, string page = null, string per_page = "200", CacheMode cacheMode = CacheMode.Skip);
        Task<Book> GetBookInfo(string id, CacheMode cacheMode = CacheMode.Skip);
        Task<Search> Search(string query, CacheMode cacheMode = CacheMode.Skip);
        Task<bool> EditReview(string reviewId, string finished, string body, string rating, string readAt, string shelf, CacheMode cacheMode = CacheMode.Skip);
        Task<string> CreateReview(string bookId, string body, string rating, string readAt, string shelf, CacheMode cacheMode = CacheMode.Skip);
        Task<Review> GetUserReview(string bookId, string userId = null, CacheMode cacheMode = CacheMode.Skip);
    }
}
