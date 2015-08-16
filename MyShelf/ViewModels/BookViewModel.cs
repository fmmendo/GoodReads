using Mendo.UAP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShelf.API.XML;

namespace MyShelf.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        public string BookTitle { get; set; }
        public string BookAuthor { get; set; }
        public string BookImageUrl { get; set; }

        public BookViewModel(Book book)
        {
            BookTitle = book.Title;
            BookAuthor = string.Join(", ", book.Authors.Select(a => a.Name));
            BookImageUrl = book.ImageUrl;
        }
    }
}
