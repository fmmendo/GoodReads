using Mendo.UAP.Common;
using MyShelf.API.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class MyBooksPageViewModel : SingletonViewModelBase<MyBooksPageViewModel>
    {
        private IAuthenticationService authService = AuthenticationService.Instance;
        private IShelfService shelfService = ShelfService.Instance;
        private IBookService bookService = BookService.Instance;

        public ObservableCollection<ShelfViewModel> Shelves { get; } = new ObservableCollection<ShelfViewModel>();

        public MyBooksPageViewModel()
        {

        }

        public async Task GetUserShelves()
        {
            Shelves.Clear();
            var shelves = await shelfService.GetShelvesList();
            foreach (var shelf in shelves)
                Shelves.Add(new ShelfViewModel(shelf));

            foreach (var shelf in Shelves)
                shelf.LoadShelfBooks(bookService);
        }
    }
}
