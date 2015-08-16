using Mendo.UAP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShelf.API.XML;

namespace MyShelf.ViewModels
{
    public class UserStatusViewModel : ViewModelBase
    {
        public string BookTitle { get; set; }
        public string BookImageUrl { get; set; }
        public string BookAuthor { get; set; }
        public double Percent { get; set; }

        public UserStatusViewModel(UserStatus status)
        {
            BookTitle = status.Book.Title;
            BookImageUrl = status.Book.ImageUrl;
            BookAuthor = string.Join(", ", status.Book.Authors.Select(a => a.Name));

            Percent = double.Parse(status.Percent);
        }
    }
}
