using Mendo.UAP.Common;
using MyShelf.API.Storage;
using MyShelf.API.Web;
using MyShelf.API.XML;
using MyShelf.API.XML.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.API.Services
{
    public class ShelfService : Singleton<ShelfService>, IShelfService
    {
        /// <summary>
        /// Returns the shelves for the logged in user
        /// </summary>
        /// <param name="query">string to search for</param>
        /// <returns>List of Work items</returns>
        public async Task<List<UserShelf>> GetShelvesList()
        {
            //if (justRefreshedShelves)
            //{
            //    // update it in the background
            //    Task.Run(async () =>
            //    {
            //        await apiSemaphore.WaitAsync();
            //        string results = await HttpGet("https://www.goodreads.com/shelf/list.xml?key=" + API_KEY);
            //        ApiCooldown();

            //        var result = DeserializeResponse(results);

            //        GoodreadsUserShelves = result.Shelves.UserShelf;
            //    });
            //}
            //else
            //{
                var results = await ApiClient.Instance.HttpGet(String.Format(Urls.ShelvesList, MyShelfSettings.Instance.ConsumerKey));
                //await apiSemaphore.WaitAsync();
                //string results = await HttpGet("https://www.goodreads.com/shelf/list.xml?key=" + API_KEY);
                //ApiCooldown();

                var result = GoodReadsSerializer.DeserializeResponse(results);

                //GoodreadsUserShelves = result.Shelves.UserShelf;
            //}
            return result.Shelves.UserShelf;
        }
    }
}
