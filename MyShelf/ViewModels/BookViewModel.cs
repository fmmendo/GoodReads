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
        private Book book;

        public BookViewModel(Book book)
        {
            this.book = book;
        }
    }
}
