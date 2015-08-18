using Mendo.UAP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShelf.API.XML;
using System.Collections.ObjectModel;
using MyShelf.API.Services;

namespace MyShelf.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        public string BookId { get; set; }
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string BookImageUrlLarge { get; set; }
        public string BookImageUrl { get; set; } = string.Empty;
        public string BookImageUrlSmall { get; set; }
        public string Stats { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public double MyRating { get; set; }
        public ObservableCollection<Detail> Details { get; set; } = new ObservableCollection<Detail>();
        public ObservableCollection<Review> Reviews { get; set; } = new ObservableCollection<Review>();

        public BookViewModel(Book book)
        {
            BookId = book.Id;
            BookTitle = book.Title;
            BookAuthor = string.Join(", ", book.Authors.Select(a => a.Name));
            BookImageUrlLarge = book.LargeImageUrl;
            BookImageUrl = book.ImageUrl;
            BookImageUrlSmall = book.SmallImageUrl;

            Link = book.Link;
            Description = book.Description;
        }

        public BookViewModel(string id)
        {
            GetBookInfo(id);
        }

        private async Task GetBookInfo(string id)
        {
            var book = await BookService.Instance.GetBookInfo(id);

            BookTitle = book.Title;
            BookAuthor = string.Join(", ", book.Authors.Select(a => a.Name));
            BookImageUrlLarge = book.LargeImageUrl;
            BookImageUrl = book.ImageUrl;
            BookImageUrlSmall = book.SmallImageUrl;

            Link = book.Link;
            Description = book.Description;

            OnPropertyChanged("BookTitle");
            OnPropertyChanged("BookAuthor");
            OnPropertyChanged("BookImageUrlLarge");
            OnPropertyChanged("BookImageUrl");
            OnPropertyChanged("BookImageUrlSmall");
            OnPropertyChanged("Link");
            OnPropertyChanged("Description");
        }
    }

    public class Detail
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
