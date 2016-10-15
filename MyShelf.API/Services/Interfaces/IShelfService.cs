using Mendo.UWP.Network;
using MyShelf.API.XML;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public interface IShelfService
    {
        Task<List<UserShelf>> GetShelvesList(CacheMode cacheMode = CacheMode.Skip);
        Task<bool> AddBookToShelf(string shelfName, string bookId, bool remove = false, CacheMode cacheMode = CacheMode.Skip);
    }
}
