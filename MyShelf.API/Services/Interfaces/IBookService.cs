using MyShelf.API.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public interface IBookService
    {
        Task<Reviews> GetBooks(string shelf = null, string sort = null, string query = null, string order = null, string page = null, string per_page = "200");
        Task<Book> GetBookInfo(string id);
        Task<Search> Search(string query);
    }
}
