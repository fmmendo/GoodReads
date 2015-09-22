using MyShelf.API.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public interface IShelfService
    {
        Task<List<UserShelf>> GetShelvesList();
        Task<bool> AddBookToShelf(string shelfName, string bookId, bool remove = false);
    }
}
