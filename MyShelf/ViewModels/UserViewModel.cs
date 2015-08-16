using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShelf.API.XML;
using Mendo.UAP.Common;

namespace MyShelf.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string FriendsCount { get; set; }
        public string BooksCount { get; set; }

        public UserViewModel(User friend)
        {
            Name = friend.Name;
            ImageUrl = friend.ImageUrl;
            FriendsCount = $"{friend.FriendsCount} Friends";
            BooksCount = $"{friend.ReviewsCount} Books";
        }

        public void UserClick()
        {
        }

        public void BooksClick()
        {
        }
        
        public void FriendsClick()
        {
        }
    }
}
