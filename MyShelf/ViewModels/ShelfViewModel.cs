using Mendo.UAP.Common;
using MyShelf.API.Services;
using MyShelf.API.XML;
using MyShelf.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShelf.ViewModels
{
    public class ShelfViewModel : ViewModelBase
    {
        public string Name { get; set; }
        //public string BookCount { get; set; }
        //public string Description { get; set; }
        //public string DisplayFields { get; set; }
        //public string ExclusiveFlag { get; set; }
        //public string Featured { get; set; }
        //public string Id { get; set; }
        //public string Order { get; set; }
        //public string PerPage { get; set; }
        //public string RecommendFor { get; set; }
        //public string Sort { get; set; }
        //public string Sticky { get; set; }

        public ObservableCollection<BookViewModel> ShelfBooks { get; set; } = new ObservableCollection<BookViewModel>();

        private BookViewModel selectedBook;
        public BookViewModel SelectedBook
        {
            get { return selectedBook; }
            set
            {
                if (value == null) return;

                selectedBook = value;
                OnPropertyChanged();

                NavigationService.Navigate(typeof(BookPage), SelectedBook.BookId);
            }
        }


        public ShelfViewModel(UserShelf userShelf)
        {
            Name = userShelf.Name;
        }

        public async Task LoadShelfBooks(IBookService bookService)
        {
            ShelfBooks.Clear();

            var reviews = await bookService.GetBooks(Name);
            foreach (var r in reviews.Review)
                ShelfBooks.Add(new BookViewModel(r.Book));
        }

        private async Task GetBooks(IBookService bookService)
        {
            
        }
    }
}
