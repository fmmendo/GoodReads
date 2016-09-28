using Mendo.UWP.Common;
using MyShelf.API.Storage;
using MyShelf.API.Web;
using MyShelf.API.XML;
using MyShelf.API.XML.Utilities;
using System;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public class AuthorService : Singleton<AuthorService>, IAuthorService
    {
        /// <summary>
        /// Returns more complete author data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Author> GetAuthorInfo(string id)
        {
            string results = await ApiClient.Instance.HttpGet(String.Format(Urls.AuthorShow, id, MyShelfSettings.Instance.ConsumerKey));

            var result = GoodReadsSerializer.DeserializeResponse(results);

            return result.Author;
        }

        /// <summary>
        /// Returns an author's books
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<Books> GetAuthorBooks(string id/*, int page = 1*/)
        {
            string results = await ApiClient.Instance.HttpGet(String.Format(Urls.AuthorBooks, id, MyShelfSettings.Instance.ConsumerKey)); /*, page.ToString()*/

            var result = GoodReadsSerializer.DeserializeResponse(results);

            return result.Author.Books;
        }
    }
}
