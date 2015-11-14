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

        public bool IsLoading { get { return Get(false); } set { Set(value); } }


        public ObservableCollection<ShelfViewModel> Shelves { get; } = new ObservableCollection<ShelfViewModel>();

        public MyBooksPageViewModel()
        {

        }

        public async Task GetUserShelves()
        {
            IsLoading = true;
            if (authService.State != AuthState.Authenticated)
            {
                authService.Authenticate();

                return;
            }

            var shelves = await shelfService.GetShelvesList();
            var except = Enumerable.Except(shelves.Select(s => s.Name), Shelves.Select(s => s.Name));

            if (except.Count() > 0)
            {
                Shelves.Clear();
                foreach (var shelf in shelves)
                    Shelves.Add(new ShelfViewModel(shelf));
            }


            await Shelves.First().LoadShelfBooks(bookService);

            IsLoading = false;

            foreach (var shelf in Shelves.Skip(1))
                shelf.LoadShelfBooks(bookService);
        }
    }
}
